using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace BookMyDoctor.Repository
{
    public class MySQL_DB_Connect
    {

        MySqlConnection sqlCon;
        MySqlCommand sqlCmd;
        MySqlDataAdapter sqlDA;
        MySqlTransaction sqlTrans = null;

        public MySQL_DB_Connect()
        {
            sqlCon = new MySqlConnection(ConfigurationManager.AppSettings["db_connect_mysql"]);
            

        }
        public DataTable SelectQuery(string strQuery)
        {
            DataTable dt = new DataTable();
            try
            {

                sqlCmd = new MySqlCommand(strQuery, sqlCon)
                {
                    CommandType = CommandType.Text
                };
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlDA = new MySqlDataAdapter(sqlCmd);
                sqlDA.Fill(dt);
                return dt;
            }
            catch (MySqlException)
            {
                
                return dt;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public string SelectScalar(string strQuery)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlCmd = new MySqlCommand(strQuery, sqlCon)
                {
                    CommandType = CommandType.Text
                };
                sqlCon.Open();
                return Convert.ToString(sqlCmd.ExecuteScalar());
            }
            catch (MySqlException)
            {
                return null;
            }
            finally
            {
                sqlCon.Close();
            }
        }


        public DataTable SelectSP(string strSP, params IDataParameter[] commandParameters)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlCmd = new MySqlCommand(strSP, sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                foreach (MySqlParameter par in commandParameters)
                    sqlCmd.Parameters.Add(par);
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlDA = new MySqlDataAdapter(sqlCmd);
                sqlDA.Fill(dt);
                return dt;
            }
            catch (MySqlException)
            {
                return null;
            }
            finally
            {
                sqlCmd.Parameters.Clear();
                sqlCon.Close();
            }
        }

        public bool ExecuteQuery(string strQuery)
        {
            try
            {
                sqlCmd = new MySqlCommand(strQuery, sqlCon)
                {
                    CommandType = CommandType.Text
                };
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException )
            {
                //throw exc;
                return false;
            }
            finally
            {
                sqlCmd.Parameters.Clear();
                sqlCon.Close();
            }
        }



        public bool ExecuteSP(string strSP, params IDataParameter[] commandParameters)
        {
            try
            {
                sqlCmd = new MySqlCommand(strSP, sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                foreach (MySqlParameter par in commandParameters)
                    sqlCmd.Parameters.Add(par);
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException )
            {
                return false;
            }
            finally
            {
                sqlCmd.Parameters.Clear();
                sqlCon.Close();
            }
        }
        public void OpenConnection()
        {
            try
            {
                sqlCon.Open();
            }
            catch (MySqlException exc)
            {
                throw new Exception(exc.Message + "OpenConnection()sqlexception");
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message + "OpenConnection()");
            }
        }
        public void CloseConnection()
        {
            try
            {
                sqlCon.Close();
            }
            catch (MySqlException exc)
            {
                throw new Exception(exc.StackTrace + ". DB Close Connection Problem");
            }
        }
        public void BeginTransaction()
        {
            try
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                sqlTrans = sqlCon.BeginTransaction();
            }
            catch (MySqlException exc)
            {
                throw new Exception(exc.Message);
            }
        }
        public void CommitTransaction()
        {
            try
            {
                sqlTrans.Commit();
                sqlTrans.Dispose();
                sqlCon.Close();
            }
            catch (MySqlException exc)
            {
                throw new Exception(exc.Message);
            }
        }
        public void RollBackTransaction()
        {
            try
            {
                if (sqlTrans != null)
                {
                    sqlTrans.Rollback();
                }
            }
            catch (MySqlException exc)
            {
                throw new Exception(exc.Message);
            }
        }

        public bool ExecuteQueryForTransaction(string strQuery)
        {
            try
            {
                sqlCmd = new MySqlCommand(strQuery, sqlCon, sqlTrans)
                {
                    CommandType = CommandType.Text
                };
                if (sqlCon != null && sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                sqlCmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException exc)
            {
                throw exc;
                //return false;
            }
            finally
            {
                sqlCmd.Parameters.Clear();
                //sqlCon.Close();
            }
        }

        /// <summary>
        /// execute a crud operation with sql command
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public bool ExecuteQueryForTransaction(MySqlCommand cmd)
        {
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlCon;
                cmd.Transaction = sqlTrans;
                if (sqlCon != null && sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException exc)
            {
                throw exc;
            }
            finally
            {
            }
        }

        public string SelectScalarForTransaction(string strQuery)
        {
            try
            {
                DataTable dt = new DataTable();

                sqlCmd = new MySqlCommand(strQuery, sqlCon, sqlTrans)
                {
                    CommandType = CommandType.Text
                };
                if (sqlCon != null && sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                return Convert.ToString(sqlCmd.ExecuteScalar());

            }
            catch (MySqlException exc)
            {
                throw exc;
            }
            finally
            {
                //sqlCon.Close();
            }
        }

        /// <summary>
        /// execute scalar with sqlcommand as passing argument
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public string SelectScalarForTransaction(MySqlCommand cmd)
        {
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlCon;
                cmd.Transaction = sqlTrans;
                if (sqlCon != null && sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                return Convert.ToString(cmd.ExecuteScalar());

            }
            catch (MySqlException exc)
            {
                throw exc;
            }
            finally
            {
                //sqlCon.Close();
            }
        }

        public DataTable SelectQueryForTransaction(string strQuery)
        {
            try
            {
                DataTable dt = new DataTable();

                sqlCmd = new MySqlCommand(strQuery, sqlCon, sqlTrans)
                {
                    CommandType = CommandType.Text
                };
                if (sqlCon != null && sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                sqlCmd.ExecuteNonQuery();
                sqlDA = new MySqlDataAdapter(sqlCmd);
                sqlDA.Fill(dt);
                return dt;
            }
            catch (MySqlException exc)
            {
                throw exc;
            }
            finally
            {

            }
        }

        /// <summary>
        /// execute select with sqlcommand as passing argument
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>

        public DataTable SelectQueryForTransaction(MySqlCommand cmd)
        {
            try
            {
                DataTable dt = new DataTable();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlCon;
                cmd.Transaction = sqlTrans;
                if (sqlCon != null && sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                cmd.ExecuteNonQuery();
                sqlDA = new MySqlDataAdapter(cmd);
                sqlDA.Fill(dt);
                return dt;
                // return Convert.ToString(cmd.ExecuteNonQuery());
            }
            catch (MySqlException exc)
            {
                throw exc;
            }
            finally
            {

            }
        }

    }
}