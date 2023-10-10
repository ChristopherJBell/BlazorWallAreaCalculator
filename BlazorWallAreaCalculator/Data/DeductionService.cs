using Dapper;
using System.Data.SQLite;
using System.Data;

namespace BlazorWallAreaCalculator.Data
{
    public class DeductionService : IDeductionService
    {
        // Database connection
        private readonly IConfiguration _configuration;
        public DeductionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string connectionId = "Default";
        public string sqlCommand = "";
        private Deduction? deduction;

        // DeductionCreate
        public async Task<bool> DeductionCreate(Deduction deduction)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@WallID", deduction.WallID, DbType.Int32);
            parameters.Add("@DeductionName", deduction.DeductionName, DbType.String);
            parameters.Add("@DeductionWidth", deduction.DeductionWidth, DbType.Int32);
            parameters.Add("@DeductionHeight", deduction.DeductionHeight, DbType.Int32);

            sqlCommand = "Insert into Deduction (WallID, DeductionName, DeductionWidth, DeductionHeight) ";
            sqlCommand += "values(@WallID, @DeductionName, @DeductionWidth, @DeductionHeight )";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);                
            }
            return true;
        }


        // DeductionRead
        public async Task<IEnumerable<Deduction>> DeductionReadAll()
        {
            IEnumerable<Deduction> deductions;

            sqlCommand = "Select * from Deduction ORDER BY DeductionName";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                deductions = await conn.QueryAsync<Deduction>(sqlCommand);
            }
            return deductions;
        }

        public async Task<int> CountDeductionsByName(string DeductionName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DeductionName", DeductionName, DbType.String);

            sqlCommand = "Select Count(*) from Deduction ";
            sqlCommand += "where Upper(DeductionName) = Upper(@DeductionName)";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                var countDeduction = await conn.QueryFirstOrDefaultAsync<int>(sqlCommand, parameters);
                return countDeduction;
            }
        }

        public async Task<int> CountDeductionsByNameAndId(string DeductionName, int DeductionID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DeductionName", DeductionName, DbType.String);
            parameters.Add("@DeductionID", DeductionID, DbType.Int32);

            sqlCommand = "Select Count(*) from Deduction ";
            sqlCommand += "where Upper(DeductionName) = Upper(@DeductionName) ";
            sqlCommand += "and DeductionID <> @DeductionID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                var countDeduction = await conn.QueryFirstOrDefaultAsync<int>(sqlCommand, parameters);
                return countDeduction;
            }
        }


        //DeductionUpdate
        public async Task<bool> DeductionUpdate(Deduction deduction)
        {
            var parameters = new DynamicParameters();

            parameters.Add("DeductionID", deduction.DeductionID, DbType.Int32);
            parameters.Add("DeductionName", deduction.DeductionName, DbType.String);
            parameters.Add("@DeductionWidth", deduction.DeductionWidth, DbType.Int32);
            parameters.Add("@DeductionHeight", deduction.DeductionHeight, DbType.Int32);

            sqlCommand = "Update Deduction ";
            sqlCommand += "SET DeductionName = @DeductionName, ";
            sqlCommand += "DeductionWidth = @DeductionWidth, ";
            sqlCommand += "DeductionHeight = @DeductionHeight ";
            sqlCommand += "WHERE DeductionID  = @DeductionID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);                
            }
            return true;
        }


        //DeductionDelete
        public async Task<bool> DeductionDelete(Int32 DeductionID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DeductionID", DeductionID, DbType.Int32);

            //PRAGMA is specific to SQLite and this command is required for DELETE CASCADE to work
            sqlCommand = "PRAGMA foreign_keys = ON;";
            sqlCommand += "Delete from Deduction ";
            sqlCommand += "WHERE DeductionID  = @DeductionID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);                
            }
            return true;
        }


    }
}
