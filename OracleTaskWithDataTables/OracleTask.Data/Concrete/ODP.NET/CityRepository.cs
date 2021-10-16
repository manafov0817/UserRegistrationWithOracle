using Oracle.ManagedDataAccess.Client;
using OracleTask.Data.Abstract;
using OracleTask.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace OracleTask.Data.Concrete.ODP.NET
{
    public class CityRepository : ICityRepository
    {
        public bool Create(City entity)
        {

            OracleConnection connection = new
                 OracleConnection(ConnectionString.GetConnectionString());

            bool result = false;

            try
            {

                OracleCommand command = new OracleCommand("PKC_CITY.ADD_CITY", connection);

                command.Connection = connection;

                command.Parameters.Add("city_name", OracleDbType.NVarchar2).Value = entity.Name;
                command.Parameters.Add("country_name", OracleDbType.NVarchar2).Value = entity.CountryName;

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
                OracleCommand cmd = new OracleCommand("PKC_CITY.DELETE_CITY", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("isDeleted", OracleDbType.Decimal, ParameterDirection.ReturnValue);

                cmd.Parameters.Add("city_id", id);

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
                OracleCommand cmd = new OracleCommand("PKC_CITY.EXITS_BY_ID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                //return 
                OracleParameter output = new OracleParameter();
                output.ParameterName = "CITY_EXIST";
                output.OracleDbType = OracleDbType.Decimal;
                output.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(output);

                // input
                OracleParameter input = new OracleParameter();

                input.Value = id;

                input.ParameterName = "city_id";

                cmd.Parameters.Add(input);

                cmd.ExecuteReader();

                exists = Convert.ToDecimal(output.Value.ToString()) == 1 ? true : false;

                output.Dispose();

                cmd.Dispose();
            }
            return exists;
        }

        public ICollection<City> GetAll()
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

            ICollection<City> cities = new List<City>();

            // only call our methods if we are connected
            // to the database
            if (con.State == ConnectionState.Open)
            {
                // create the command object and set attributes
                OracleCommand cmd = new OracleCommand("PKC_CITY.GET_ALL_CITIES", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // create parameter object for the cursor
                OracleParameter p_refcursor = new OracleParameter();

                // this is vital to set when using ref cursors
                p_refcursor.OracleDbType = OracleDbType.RefCursor;

                // this is a function return value so we must indicate that fact
                p_refcursor.Direction = ParameterDirection.ReturnValue;

                // add the parameter to the collection
                cmd.Parameters.Add(p_refcursor);

                // create a data adapter to use with the data set
                OracleDataAdapter da = new OracleDataAdapter(cmd);

                // create the data set
                DataSet ds = new DataSet();

                // fill the data set
                da.Fill(ds);


                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        City city = new City()
                        {
                            Id = Convert.ToInt32(row[0]),
                            Name = row[1].ToString(),
                            CountryName = row[2].ToString()

                        };
                        cities.Add(city);
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

            return cities;
        }

        public City GetById(int id)
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

            City city = new City();


            if (con.State == ConnectionState.Open)
            {
                OracleCommand cmd = new OracleCommand("PKC_CITY.GET_BY_ID", con);

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
                input.ParameterName = "city_id";

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
                        city = new City()
                        {
                            Id = Convert.ToInt32(row[0]),
                            Name = row[1].ToString(),
                            CountryName = row[2].ToString()
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

            return city;
        }

        public void Update(City city)
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
                OracleCommand cmd = new OracleCommand("PKC_CITY.EDIT", con);

                cmd.CommandType = CommandType.StoredProcedure;

                //return 
                OracleParameter output = new OracleParameter();
                output.ParameterName = "isChanged";
                output.OracleDbType = OracleDbType.Decimal;
                output.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(output);

                cmd.Parameters.Add("city_id", OracleDbType.Decimal, ParameterDirection.Input).Value = city.Id;
                cmd.Parameters.Add("city_name", OracleDbType.Varchar2, 10, ParameterDirection.Input).Value = city.Name;
                cmd.Parameters.Add("country_name", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = city.CountryName;

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
