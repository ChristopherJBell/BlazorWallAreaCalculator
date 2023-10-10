using Dapper;
using System.Data.SQLite;
using System.Data;

namespace BlazorWallAreaCalculator.Data
{
    public class RoomService : IRoomService
    {
        // Database connection
        private readonly IConfiguration _configuration;
        public RoomService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string connectionId = "Default";
        public string sqlCommand = "";
        private Room? room;

        // RoomCreate
        public async Task<bool> RoomCreate(Room room)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@RoomName", room.RoomName, DbType.String);
            parameters.Add("@ProjectID", room.ProjectID, DbType.Int32);

            sqlCommand = "Insert into Room (RoomName, ProjectID) ";
            sqlCommand += "values(@RoomName, @ProjectID)";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);                
            }
            return true;
        }

        // RoomRead
        public async Task<IEnumerable<Room>> RoomsReadByProject(int ProjectID)
        {
            IEnumerable<Room> rooms;

            var parameters = new DynamicParameters();
            parameters.Add("@ProjectID", ProjectID, DbType.Int32);

            sqlCommand = "Select * from Room ";
            sqlCommand += "WHERE ProjectID  = @ProjectID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                rooms = await conn.QueryAsync<Room>(sqlCommand, parameters);
            }
            return rooms;
        }

        //RoomUpdate
        public async Task<bool> RoomUpdate(Room room)
        {
            var parameters = new DynamicParameters();

            parameters.Add("RoomID", room.RoomID, DbType.Int32);
            parameters.Add("RoomName", room.RoomName, DbType.String);

            sqlCommand = "Update Room ";
            sqlCommand += "SET RoomName = @RoomName ";
            sqlCommand += "WHERE RoomID  = @RoomID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);                
            }
            return true;
        }


        //RoomDelete
        public async Task<bool> RoomDelete(Int32 RoomID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoomID", RoomID, DbType.Int32);

            //PRAGMA is specific to SQLite and this command is required for DELETE CASCADE to work
            sqlCommand = "PRAGMA foreign_keys = ON;";
            sqlCommand += "Delete from Room ";
            sqlCommand += "WHERE RoomID  = @RoomID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);                
            }
            return true;
        }

            #region CountRooms
            public async Task<int> CountRoomsByNameAndProject(string RoomName, int ProjectID)
            {
                var parameters = new DynamicParameters();

                parameters.Add("@RoomName", RoomName, DbType.String);
                parameters.Add("@ProjectID", ProjectID, DbType.Int32);

                sqlCommand = "Select Count(*) from Room ";
                sqlCommand += "where Upper(RoomName) = Upper(@RoomName) ";
                sqlCommand += "and ProjectID = @ProjectID";

                using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
                {
                    var countRoom = await conn.QueryFirstOrDefaultAsync<int>(sqlCommand, parameters);
                    return countRoom;
                }
            }

            public async Task<int> CountRoomsByNameAndProjectAndId(string RoomName, int RoomID, int ProjectID)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@RoomName", RoomName, DbType.String);
                parameters.Add("@RoomID", RoomID, DbType.Int32);
                parameters.Add("@ProjectID", ProjectID, DbType.Int32);

                sqlCommand = "Select Count(*) from Room ";
                sqlCommand += "where Upper(RoomName) = Upper(@RoomName) ";
                sqlCommand += "and ProjectID = @ProjectID ";
                sqlCommand += "and RoomID <> @RoomID";

                using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
                {
                    var countRoom = await conn.QueryFirstOrDefaultAsync<int>(sqlCommand, parameters);
                    return countRoom;
                }
            }
            #endregion

    }
}
