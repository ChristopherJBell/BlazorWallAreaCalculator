using Dapper;
using System.Data.SQLite;
using System.Data;

namespace BlazorWallAreaCalculator.Data
{
    public class WallService : IWallService
    {
        // Database connection
        private readonly IConfiguration _configuration;
        public WallService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string connectionId = "Default";
        public string sqlCommand = "";
        private Wall? wall;

        // WallCreate
        public async Task<bool> WallCreate(Wall wall)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@RoomID", wall.RoomID, DbType.Int32);
            parameters.Add("@WallName", wall.WallName, DbType.String);
            parameters.Add("@WallTypeID", wall.WallTypeID, DbType.Int32);
            parameters.Add("@WallTypeName", wall.WallTypeName, DbType.String);
            parameters.Add("@WallLengthMax", wall.WallLengthMax, DbType.Int32);
            parameters.Add("@WallLengthMin", wall.WallLengthMin, DbType.Int32);
            parameters.Add("@WallHeightMax", wall.WallHeightMax, DbType.Int32);
            parameters.Add("@WallHeightMin", wall.WallHeightMin, DbType.Int32);
            parameters.Add("@WallSqM", wall.WallSqM, DbType.Decimal);

            sqlCommand = "Insert into Wall (RoomID, WallName, WallTypeID, WallTypeName, " +
                "WallLengthMax, WallLengthMin, WallHeightMax, WallHeightMin, WallSqM) ";
            sqlCommand += "values(@RoomID, @WallName, @WallTypeID, @WallTypeName, " +
                "@WallLengthMax, @WallLengthMin, @WallHeightMax, @WallHeightMin, @WallSqM) ";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return true;
        }


        // WallRead
        public async Task<IEnumerable<Wall>> WallsReadByRoom(int RoomID)
        {
            IEnumerable<Wall> walls;
            var parameters = new DynamicParameters();
            parameters.Add("RoomID", RoomID, DbType.Int32);

            sqlCommand = "Select * from Wall ";
            sqlCommand += "WHERE RoomID  = @RoomID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                walls = await conn.QueryAsync<Wall>(sqlCommand, parameters);
            }
            return walls;
        }


        #region CountWalls
        public async Task<int> CountWallsByNameAndRoom(string WallName, int RoomID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WallName", WallName, DbType.String);
            parameters.Add("@RoomID", RoomID, DbType.Int32);

            sqlCommand = "Select Count(*) from Wall ";
            sqlCommand += "where Upper(WallName) = Upper(@WallName) ";
            sqlCommand += "and RoomID = @RoomID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                var countWall = await conn.QueryFirstOrDefaultAsync<int>(sqlCommand, parameters);
                return countWall;
            }
        }

        public async Task<int> CountWallsByNameAndRoomAndId(string WallName, int WallID, int RoomID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WallName", WallName, DbType.String);
            parameters.Add("@WallID", WallID, DbType.Int32);
            parameters.Add("@RoomID", RoomID, DbType.Int32);
            sqlCommand = "Select Count(*) from Wall ";
            sqlCommand += "where Upper(WallName) = Upper(@WallName) ";
            sqlCommand += "and RoomID = @RoomID ";
            sqlCommand += "and WallID <> @WallID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                var countWall = await conn.QueryFirstOrDefaultAsync<int>(sqlCommand, parameters);
                return countWall;
            }
        }
        #endregion



        //WallUpdate
        public async Task<bool> WallUpdate(Wall wall)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@WallID", wall.WallID, DbType.Int32);
            parameters.Add("@WallName", wall.WallName, DbType.String);
            parameters.Add("@WallTypeID", wall.WallTypeID, DbType.Int32);
            parameters.Add("@WallTypeName", wall.WallTypeName, DbType.String);
            parameters.Add("@WallLengthMax", wall.WallLengthMax, DbType.Int32);
            parameters.Add("@WallLengthMin", wall.WallLengthMin, DbType.Int32);
            parameters.Add("@WallHeightMax", wall.WallHeightMax, DbType.Int32);
            parameters.Add("@WallHeightMin", wall.WallHeightMin, DbType.Int32);
            parameters.Add("@WallSqM", wall.WallSqM, DbType.Decimal);

            sqlCommand = "Update Wall ";
            sqlCommand += "SET WallName = @WallName, " +
                "WallTypeID = @WallTypeID, " +
                "WallTypeName = @WallTypeName, " +
                "WallLengthMax = @WallLengthMax, " +
                "WallLengthMin = @WallLengthMin, " +
                "WallHeightMax = @WallHeightMax, " +
                "WallHeightMin = @WallHeightMin, " +
                "WallSqM = @WallSqM ";
            sqlCommand += "WHERE WallID  = @WallID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return true;
        }



        //WallDelete
        public async Task<bool> WallDelete(Int32 WallID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WallID", WallID, DbType.Int32);

            //PRAGMA is specific to SQLite and this command is required for DELETE CASCADE to work
            sqlCommand = "PRAGMA foreign_keys = ON;";
            sqlCommand += "Delete from Wall ";
            sqlCommand += "WHERE WallID  = @WallID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);                
            }
            return true;
        }

    }
}
