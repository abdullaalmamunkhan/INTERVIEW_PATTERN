using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_DAL.DataBaseMecanism
{
    public interface IStoredProcedure
    {
        public void AddInputParameter(string paramName, object Value, SqlDbType oracleType);
        public void AddInputParameter(string paramName, object Value, SqlDbType oracleType, int size);
        public void ExecuteNonQuery();
        public void ExecuteNonQuery(DbTransaction transaction);
        public DataTable ExecuteQueryToDataTable();

        public string ProcedureName
        {
            get;
            set;
        }
        public SqlParameter[] ParameterList
        {
            get;
        }
        public string ReturnMessage
        {
            get;
        }
        public int ErrorCode
        {
            get;
        }
    }


    public class StoredProcedure : IStoredProcedure
    {
        private readonly ISPCommand _spCommand;

        public StoredProcedure(ISPCommand spCommand)
        {
            _spCommand = spCommand;
        }

        private string procedureName;

        public string ProcedureName
        {
            get { return procedureName; }
            set
            {
                this.parameterList = new ArrayList();
                procedureName = value;
            }
        }

        private ArrayList parameterList = new ArrayList();

        public SqlParameter[] ParameterList
        {
            get
            {
                return parameterList.ToArray(typeof(SqlParameter)) as SqlParameter[];
            }
        }

        public string ReturnMessage
        {
            get
            {
                return (parameterList[1] as SqlParameter).Value.ToString();
            }
        }

        public int ErrorCode
        {
            get
            {
                return Convert.ToInt32((parameterList[0] as SqlParameter).Value.ToString());
            }
        }

        public void AddInputParameter(string paramName, object Value, SqlDbType oracleType)
        {
            SqlParameter param = new SqlParameter(paramName, oracleType);
            if (oracleType == SqlDbType.Date)
            {
                if (Convert.ToDateTime(Value) == DateTime.MinValue)
                    Value = DBNull.Value;

                else if (Convert.ToDateTime(Value) == DateTime.MaxValue)
                    Value = DBNull.Value;
            }
            else if (oracleType == SqlDbType.Int && paramName.EndsWith("ID") && this.procedureName.ToUpper().Contains(".SAVE_"))
            {
                if (Convert.ToInt32(Value) <= 0)
                {
                    Value = DBNull.Value;
                }
            }
            param.Value = Value;
            parameterList.Add(param);
        }

        public void AddInputParameter(string paramName, object Value, SqlDbType oracleType, int size)
        {
            SqlParameter param = new SqlParameter(paramName, oracleType, size);
            if (oracleType == SqlDbType.Date)
            {
                if (Convert.ToDateTime(Value) == DateTime.MinValue)
                    Value = DBNull.Value;
            }
            param.Value = Value;
            parameterList.Add(param);
        }

        public void ExecuteNonQuery()
        {
            _spCommand.ExecuteNonQueryStoredProcedure(this.ProcedureName, this.ParameterList);
        }

        public void ExecuteNonQuery(DbTransaction transaction)
        {
            if (transaction == null)
            {
                ExecuteNonQuery();
            }
            else
            {
                //_spCommand.ExecuteNonQueryStoredProcedure(this.ProcedureName, this.ParameterList,transaction.CurrentTransaction.Connection, transaction.CurrentTransaction);
            }
        }

        public DataTable ExecuteQueryToDataTable()
        {

            return _spCommand.ExecuteStoredProcedureDataTable(this.ProcedureName, this.ParameterList);
        }
    }
}
