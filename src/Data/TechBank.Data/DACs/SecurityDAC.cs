using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBank.DomainModel;

namespace TechBank.Data
{
    public class SecurityDAC
    {
        private SqlTransaction Transaction { get; set; }
        private Database Database;

        #region Constructor        
        public SecurityDAC()
        {
            Database = DatabaseFactory.CreateDatabase();
        }

        public SecurityDAC(SqlTransaction transaction)
        {            
            Transaction = transaction;
            Database = DatabaseFactory.CreateDatabase();
        }
        #endregion

        #region Private Const String
        private string SELECT_BY_EMAIL = "SELECT * FROM [dbo].[AspNetUsers] WHERE [Email] = @email";
        #endregion

        #region Query Commands
        #endregion

        #region Public Methods
        public User GetUserByEmail(string email)
        {
            try
            {
                User user = null;

                using (var connection = Database.OpenConnection())
                {
                    var command = Database.GetSqlStringCommand(connection, SELECT_BY_EMAIL);
                    Database.AddInParameter(command, "@email", DbType.String, email);
                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow | CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            user = new User();
                            FillUser(user, reader);
                        }
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                if (Transaction != null && Transaction.Connection != null)
                    Transaction.Rollback();
                throw ex;
            }
        }
        #endregion

        #region Private Methods
        public void FillUser(User user, SqlDataReader reader)
        {
            user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
        }
        #endregion
    }
}
