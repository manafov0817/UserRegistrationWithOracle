using Oracle.ManagedDataAccess.Client;
using OracleTask.Data.Abstract;
using OracleTask.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OracleTask.Data.Concrete.ODP.NET
{
    public class LocationRepository : ILocationRepository
    {
        public bool Create(Location entity)
        {

            OracleConnection connection = new
                 OracleConnection(ConnectionString.GetConnectionString());

            bool result = false;

            try
            {

                OracleCommand command = new OracleCommand("PKC_LOACTION.ADD_LOCATION", connection);

                command.Connection = connection;

                command.Parameters.Add("location_latitude", OracleDbType.NVarchar2).Value = entity.Latitude;
                command.Parameters.Add("location_longitude", OracleDbType.NVarchar2).Value = entity.Longitude;
                command.Parameters.Add("location_markas", OracleDbType.NVarchar2).Value = entity.MarkAs;
                command.Parameters.Add("location_userid", OracleDbType.NVarchar2).Value = entity.UserId;

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

            }

            catch (Exception ex)
            {
                connection.Close();

                Console.WriteLine("Error: " + ex.Message);
            }
            return result;
        }

        public void Delete(int id)
        {

            OracleConnection con = new OracleConnection(ConnectionString.GetConnectionString());

            try
            {
                con.Open();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }


            if (con.State == ConnectionState.Open)
            {
                OracleCommand cmd = new OracleCommand("PKC_LOACTION.DELETE_LOCATION", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("isDeleted", OracleDbType.Decimal, ParameterDirection.ReturnValue);

                cmd.Parameters.Add("location_id", id);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    cmd.Dispose();
                }
                cmd.Dispose();
            }

            con.Dispose();

        }

        public bool ExistById(int id)
        {
            OracleConnection con = new OracleConnection(ConnectionString.GetConnectionString());

            try
            {
                con.Open();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }

            bool exists = false;

            if (con.State == ConnectionState.Open)
            {
                OracleCommand cmd = new OracleCommand("PKC_LOACTION.EXITS_BY_ID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                //return 
                OracleParameter output = new OracleParameter();
                output.ParameterName = "LOCATION_EXIST";
                output.OracleDbType = OracleDbType.Decimal;
                output.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(output);

                // input
                OracleParameter input = new OracleParameter();

                input.Value = id;

                input.ParameterName = "ID";

                cmd.Parameters.Add(input);

                cmd.ExecuteReader();

                exists = Convert.ToDecimal(output.Value.ToString()) == 1 ? true : false;

                output.Dispose();

                cmd.Dispose();
            }
            return exists;
        }

        public ICollection<Location> GetAll()
        {
            // create a connection to the database
            // change values as needed for your environment
            OracleConnection con = new OracleConnection(ConnectionString.GetConnectionString());

            // attempt to open the connection
            try
            {
                con.Open();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }

            ICollection<Location> locations = new List<Location>();

            // only call our methods if we are connected
            // to the database
            if (con.State == ConnectionState.Open)
            {
                // create the command object and set attributes
                OracleCommand cmd = new OracleCommand("PKC_LOACTION.GET_ALL_LOCATIONS", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                OracleParameter p_refcursor = new OracleParameter();

                p_refcursor.OracleDbType = OracleDbType.RefCursor;

                p_refcursor.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(p_refcursor);

                OracleDataAdapter da = new OracleDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);


                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Location location = new Location()
                        {
                            Id = Convert.ToInt32(row[0]),
                            Latitude = row[1].ToString(),
                            Longitude = row[2].ToString(),
                            MarkAs=row[3].ToString(),
                            UserUsername = row[4].ToString()
                        };
                        locations.Add(location);
                    }
                }

                // clean up our objects release resources
                ds.Dispose();
                da.Dispose();
                p_refcursor.Dispose();
                cmd.Dispose();
            }

            // clean up the connection object
            con.Dispose();

            return locations;
        }

        public Location GetById(int id)
        {

            OracleConnection con = new OracleConnection(ConnectionString.GetConnectionString());

            try
            {
                con.Open();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Location location = new Location();


            if (con.State == ConnectionState.Open)
            {
                OracleCommand cmd = new OracleCommand("PKC_LOACTION.GET_BY_ID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                //return 
                OracleParameter output = new OracleParameter();
                output.ParameterName = "U_CURSOR";
                output.OracleDbType = OracleDbType.RefCursor;
                output.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(output);

                // input
                OracleParameter input = new OracleParameter();
                input.Value = id;
                input.ParameterName = "ID";

                cmd.Parameters.Add(input);


                OracleDataAdapter da = new OracleDataAdapter(cmd);

                DataSet ds = new DataSet();

                try
                {
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ds.Dispose();
                    da.Dispose();
                    output.Dispose();
                    cmd.Dispose();
                }

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        location = new Location()
                        {
                            Id = Convert.ToInt32(row[0]),
                            Latitude = row[1].ToString(),
                            Longitude = row[2].ToString(),
                            MarkAs = row[3].ToString(),
                            UserUsername = row[4].ToString()
                        };

                        break;
                    }
                    break;
                }

                ds.Dispose();
                da.Dispose();
                output.Dispose();
                cmd.Dispose();
            }

            con.Dispose();

            return location;
        }

        public Location GetByUserId(int id)
        {

            OracleConnection con = new OracleConnection(ConnectionString.GetConnectionString());

            try
            {
                con.Open();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Location location = new Location();


            if (con.State == ConnectionState.Open)
            {
                OracleCommand cmd = new OracleCommand("PKC_LOACTION.GET_BY_USER_ID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                //return 
                OracleParameter output = new OracleParameter();
                output.ParameterName = "U_CURSOR";
                output.OracleDbType = OracleDbType.RefCursor;
                output.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(output);

                // input
                OracleParameter input = new OracleParameter();
                input.Value = id;
                input.ParameterName = "user_select_id";

                cmd.Parameters.Add(input);


                OracleDataAdapter da = new OracleDataAdapter(cmd);

                DataSet ds = new DataSet();

                try
                {
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ds.Dispose();
                    da.Dispose();
                    output.Dispose();
                    cmd.Dispose();
                }

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        location = new Location()
                        {
                            Id = Convert.ToInt32(row[0]),
                            Latitude = row[1].ToString(),
                            Longitude = row[2].ToString(),
                            MarkAs = row[3].ToString()
                         };

                        break;
                    }
                    break;
                }

                ds.Dispose();
                da.Dispose();
                output.Dispose();
                cmd.Dispose();
            }

            con.Dispose();

            return location;
        }

        public void Update(Location location)
        {

            OracleConnection con = new OracleConnection(ConnectionString.GetConnectionString());

            try
            {
                con.Open();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (con.State == ConnectionState.Open)
            {
                OracleCommand cmd = new OracleCommand("PKC_LOACTION.EDIT", con);

                cmd.CommandType = CommandType.StoredProcedure;

                //return 
                OracleParameter output = new OracleParameter();
                output.ParameterName = "isChanged";
                output.OracleDbType = OracleDbType.Decimal;
                output.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(output);


                cmd.Parameters.Add("location_id", OracleDbType.NVarchar2).Value = location.Id;
                cmd.Parameters.Add("location_latitude", OracleDbType.NVarchar2).Value = location.Latitude;
                cmd.Parameters.Add("location_longitude", OracleDbType.NVarchar2).Value = location.Longitude;
                cmd.Parameters.Add("location_markas", OracleDbType.NVarchar2).Value = location.MarkAs;
    

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    output.Dispose();
                    cmd.Dispose();
                }


                output.Dispose();
                cmd.Dispose();
            }

            con.Dispose();

        }

    }
}
