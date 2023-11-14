using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;


namespace JewarPortal
{
    public class SqlDataAccessLayer
    {

        private SqlConnection connection, connectionTAVL, connectionCRMTrasur;
        private SqlDataReader reader;
        private SqlCommand command;

        public SqlDataAccessLayer()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RetailProDB"].ConnectionString.ToString());

        }

        public void OpenConnection()
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)//|| connectionTAVL.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (SqlException ex) { connection.Close(); ex.ToString(); }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Closed) connection.Close();

            }
            catch (SqlException ex) { connection.Close(); ex.ToString(); }
        }

        public DataTable ReadData(string stringSql)
        {
            DataTable tempTable = new DataTable();
            command = new SqlCommand();

            try
            {
                this.OpenConnection();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = stringSql;
                //connection.Open();
                tempTable.Load(command.ExecuteReader(CommandBehavior.CloseConnection));
                CloseConnection();
                return tempTable;
            }
            catch (SqlException ex)
            {
                this.CloseConnection();
                ex.ToString();
                return null;
            }
        }

        public DataTable GetData(string spName, SqlParameter[] param)
        {
            DataTable tempTable = new DataTable();
            command = new SqlCommand();

            try
            {
                this.OpenConnection();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
              
                if (param != null)
                    if (param.Length > 0)
                        command.Parameters.AddRange(param.ToArray());

                tempTable.Load(command.ExecuteReader(CommandBehavior.CloseConnection));
                CloseConnection();
                return tempTable;
            }
            catch (SqlException ex)
            {
                this.CloseConnection();
                ex.ToString();
                return null;
            }
        }

        public DataTable ReadMemberDataFromSP(string MemberNo)
        {
            DataTable tempTable = new DataTable();
            command = new SqlCommand();

            try
            {
                this.OpenConnection();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SC_GetMemberDetails";
                command.Parameters.Add("@MemberNo", SqlDbType.VarChar, 25).Value = MemberNo;
                tempTable.Load(command.ExecuteReader(CommandBehavior.CloseConnection));
                CloseConnection();
                return tempTable;
            }
            catch (SqlException ex)
            {
                this.CloseConnection();
                ex.ToString();
                return null;
            }
        }

        public DataTable ReadDailyVisitFromSP(string MemberNo)
        {
            DataTable tempTable = new DataTable();
            command = new SqlCommand();

            try
            {
                this.OpenConnection();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SC_GetMemberDailyVisit";
                command.Parameters.Add("@MemberNo", SqlDbType.VarChar, 25).Value = MemberNo;

                tempTable.Load(command.ExecuteReader(CommandBehavior.CloseConnection));
                CloseConnection();
                return tempTable;
            }
            catch (SqlException ex)
            {
                this.CloseConnection();
                ex.ToString();
                return null;
            }
        }

        public DataTable ReadServiceDataFromSP(string spName)
        {
            DataTable tempTable = new DataTable();
            command = new SqlCommand();

            try
            {
                this.OpenConnection();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                tempTable.Load(command.ExecuteReader(CommandBehavior.CloseConnection));
                CloseConnection();
                return tempTable;
            }
            catch (SqlException ex)
            {
                this.CloseConnection();
                ex.ToString();
                return null;
            }
        }

        public int InsertDailyVisits(DataTable dt)
        {
            try
            {
                this.OpenConnection();
                command = new SqlCommand();

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SC_InsertDailyVisits";
                command.Parameters.Add("@DailyVisitTvp", SqlDbType.Structured).Value = dt;

                int i = command.ExecuteNonQuery();
                CloseConnection();
                return i;
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                return 0;
            }
        }
        public int InsertValue(string spName, SqlParameter[] param)
        {
            try
            {
                this.OpenConnection();
                command = new SqlCommand();

                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                
                if (param != null)
                    if (param.Length > 0)
                        command.Parameters.AddRange(param.ToArray());

                int i = command.ExecuteNonQuery();
                CloseConnection();
                return i;
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                return 0;
            }
        }
        
        public int InsertGuests(string spName, DataTable dt)
        {
            try
            {
                this.OpenConnection();
                command = new SqlCommand();

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                command.Parameters.Add("@GuestsTvp", SqlDbType.Structured).Value = dt;

                int i = command.ExecuteNonQuery();
                CloseConnection();
                return i;
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                return 0;
            }
        }

        public int InsertTransactionalData(string stringSql)
        {
            DataTable tempTable = new DataTable();
            command = new SqlCommand();
            try
            {

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = stringSql;
                connection.Open();
                int i = command.ExecuteNonQuery();
                connection.Close();
                return i;

            }
            catch (SqlException ex)
            {
                ex.ToString();
                return 0;
            }
        }
        public SqlTransaction trans;

        public void OpenTransaction()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                    trans = connection.BeginTransaction();
                else
                {
                    connection.Open();
                    trans = connection.BeginTransaction();
                }
            }
            catch (Exception ex) { }
        }
        public void EndTransaction()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close(); if (trans != null) trans.Commit(); trans = null;
                }
                else trans.Commit();
            }
            catch (Exception ex) { }
        }
    }
}