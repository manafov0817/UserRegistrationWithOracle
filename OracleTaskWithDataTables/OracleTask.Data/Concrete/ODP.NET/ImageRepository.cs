using Oracle.ManagedDataAccess.Client;
using OracleTask.Data.Abstract;
using OracleTask.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OracleTask.Data.Concrete.ODP.NET
{
    public class ImageRepository : IImageRepository
    {
        public bool Create(Image entity)
        {

            OracleConnection connection = new
                 OracleConnection(ConnectionString.GetConnectionString());

            bool result = false;

            try
            {

                OracleCommand command = new OracleCommand("PKC_IMAGE.ADD_IMAGE", connection);

                command.Connection = connection;

                command.Parameters.Add("image_imagename", OracleDbType.NVarchar2).Value = entity.Name;
                command.Parameters.Add("image_userid", OracleDbType.NVarchar2).Value = entity.UserId;

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
                OracleCommand cmd = new OracleCommand("PKC_IMAGE.DELETE_IMAGE", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("isDeleted", OracleDbType.Decimal, ParameterDirection.ReturnValue);

                cmd.Parameters.Add("image_id", id);

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
            throw new NotImplementedException();
        }

        public ICollection<Image> GetAll()
        {
            throw new NotImplementedException();
        }

        public Image GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Image GetByUserId(int id)
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

            Image image = new Image();

            if (con.State == ConnectionState.Open)
            {
                OracleCommand cmd = new OracleCommand("PKC_IMAGE.GET_BY_USER_ID", con);

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
                        image = new Image()
                        {
                            Id = Convert.ToInt32(row[0]),
                            Name = row[1].ToString()
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

            return image;
        }

        public void Update(Image entity)
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
                OracleCommand cmd = new OracleCommand("PKC_IMAGE.EDIT", con);

                cmd.CommandType = CommandType.StoredProcedure;

                //return 
                OracleParameter output = new OracleParameter();
                output.ParameterName = "isChanged";
                output.OracleDbType = OracleDbType.Decimal;
                output.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(output);


                cmd.Parameters.Add("image_id", OracleDbType.NVarchar2).Value = entity.Id;
                cmd.Parameters.Add("image_imagename", OracleDbType.NVarchar2).Value = entity.Name;

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

