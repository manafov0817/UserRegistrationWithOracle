using OracleTask.Data.Abstract;
using OracleTask.Entity.Entities;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace OracleTask.Data.Concrete.ODP.NET
{
    public class UserRepository : IUserRepository
    {
        public bool Create(User entity)
        {

            OracleConnection connection = new
                 OracleConnection(ConnectionString.GetConnectionString());

            bool result = false;

            try
            {

                OracleCommand command = new OracleCommand("PKC_USER.ADD_USER", connection);

                command.Connection = connection;


                command.Parameters.Add("usr_name", OracleDbType.NVarchar2).Value = entity.Name;
                command.Parameters.Add("usr_surname", OracleDbType.NVarchar2).Value = entity.Surname;
                command.Parameters.Add("usr_username", OracleDbType.NVarchar2).Value = entity.Username;
                command.Parameters.Add("usr_email", OracleDbType.NVarchar2).Value = entity.Email;
                command.Parameters.Add("usr_password", OracleDbType.NVarchar2).Value = entity.Password;

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

        public ICollection<User> GetAll()
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

            ICollection<User> users = new List<User>();

            // only call our methods if we are connected
            // to the database
            if (con.State == ConnectionState.Open)
            {
                // create the command object and set attributes
                OracleCommand cmd = new OracleCommand("PKC_USER.GET_ALL_USERS", con)
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
                        User user = new User()
                        {
                            Id = Convert.ToInt32(row[0]),
                            Name = row[1].ToString(),
                            Surname = row[2].ToString(),
                            Username = row[3].ToString(),
                            Email = row[4].ToString(),
                            Password = row[5].ToString(),
                        };

                        users.Add(user);

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

            return users;
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
                OracleCommand cmd = new OracleCommand("PKC_USER.DELETE_USER", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("isDeleted", OracleDbType.Decimal, ParameterDirection.ReturnValue);

                cmd.Parameters.Add("usr_id", id);

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

        public User GetById(int id)
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

            User user = new User();


            if (con.State == ConnectionState.Open)
            {
                OracleCommand cmd = new OracleCommand("PKC_USER.GET_BY_ID", con);

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
                        user = new User()
                        {
                            Id = Convert.ToInt32(row[0]),
                            Name = row[1].ToString(),
                            Surname = row[2].ToString(),
                            Username = row[3].ToString(),
                            Email = row[4].ToString(),
                            Password = row[5].ToString(),
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

            return user;
        }

        public void Update(User user)
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
                OracleCommand cmd = new OracleCommand("PKC_USER.EDIT", con);

                cmd.CommandType = CommandType.StoredProcedure;

                //return 
                OracleParameter output = new OracleParameter();
                output.ParameterName = "isChanged";
                output.OracleDbType = OracleDbType.Decimal;
                output.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(output);

                cmd.Parameters.Add("usr_id", OracleDbType.Decimal, ParameterDirection.Input).Value = user.Id;
                cmd.Parameters.Add("usr_name", OracleDbType.Varchar2, 10, ParameterDirection.Input).Value = user.Name;
                cmd.Parameters.Add("usr_surname", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = user.Surname;
                cmd.Parameters.Add("usr_username", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = user.Username;
                cmd.Parameters.Add("usr_email", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = user.Email;
                cmd.Parameters.Add("usr_password", OracleDbType.Varchar2, 50, ParameterDirection.Input).Value = user.Password;

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
                OracleCommand cmd = new OracleCommand("PKC_USER.EXITS_BY_ID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                //return 
                OracleParameter output = new OracleParameter();
                output.ParameterName = "USER_EXIST";
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

        public ICollection<User> GetAllWithProperties()
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

            ICollection<User> users = new List<User>();

            // only call our methods if we are connected
            // to the database
            if (con.State == ConnectionState.Open)
            {
                // create the command object and set attributes
                OracleCommand cmd = new OracleCommand("PKC_USER.GET_ALL_USERS_WITH_PROPERTIES", con)
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
                        User user = new User()
                        {
                            Id = Convert.ToInt32(row[0]),
                            Name = row[1].ToString(),
                            Surname = row[2].ToString(),
                            Username = row[3].ToString(),
                            Email = row[4].ToString(),
                            Password = row[5].ToString(),
                            LocationLatitude = row[6].ToString(),
                            LocationLongitude = row[7].ToString(),
                            LocationMarkAs = row[8].ToString(),
                            ImageName = row[9].ToString()
                        };

                        users.Add(user);

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

            return users;
        }

        public int GetIdByUsername(string username)
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


            int id = 0;
            if (con.State == ConnectionState.Open)
            {
                OracleCommand cmd = new OracleCommand("PKC_USER.GET_ID_BY_USERNAME", con);

                cmd.CommandType = CommandType.StoredProcedure;


                //return 
                OracleParameter output = new OracleParameter();
                output.ParameterName = "RETURN_ID";
                output.Direction = ParameterDirection.ReturnValue;
                output.OracleDbType = OracleDbType.Int64;


                cmd.Parameters.Add(output);

                // input
                OracleParameter input = new OracleParameter();
                input.Value = username;
                input.ParameterName = "USERNAME";
                input.OracleDbType = OracleDbType.Varchar2;

                cmd.Parameters.Add(input);


                try
                {
                    cmd.ExecuteNonQuery();
                    id = Convert.ToInt32(output.Value.ToString());

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }



                output.Dispose();
                cmd.Dispose();
            }

            con.Dispose();

            return id;
        }

        public User GetWithPropertiesById(int id)
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

            User user = new User();

            // only call our methods if we are connected
            // to the database
            if (con.State == ConnectionState.Open)
            {
                // create the command object and set attributes
                OracleCommand cmd = new OracleCommand("PKC_USER.GET_USER_WITH_PROPERTIES_BY_ID", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.CommandType = CommandType.StoredProcedure;

                //return 
                OracleParameter output = new OracleParameter();
                output.OracleDbType = OracleDbType.RefCursor;
                output.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(output);

                // input
                OracleParameter input = new OracleParameter();
                input.Value = id;
                input.ParameterName = "ID";

                cmd.Parameters.Add(input);

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
                        user = new User()
                        {
                            Id = Convert.ToInt32(row[0]),
                            Name = row[1].ToString(),
                            Surname = row[2].ToString(),
                            Username = row[3].ToString(),
                            Email = row[4].ToString(),
                            Password = row[5].ToString(),
                            LocationLatitude = row[6].ToString(),
                            LocationLongitude = row[7].ToString(),
                            LocationMarkAs = row[8].ToString(),
                            ImageName = row[9].ToString()
                        };
                        break;
                    }
                }

                // clean up our objects release resources
                ds.Dispose();
                da.Dispose();
                output.Dispose();
                cmd.Dispose();
            }

            // clean up the connection object
            con.Dispose();

            return user;
        }
    }
}
