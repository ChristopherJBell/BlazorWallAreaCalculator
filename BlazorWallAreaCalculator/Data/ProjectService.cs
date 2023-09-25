using Dapper;
using System.Data.SQLite;
using System.Data;


namespace BlazorWallAreaCalculator.Data
{
    public class ProjectService : IProjectService
    {
        // Database connection
        private readonly IConfiguration _configuration;
        public ProjectService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string connectionId = "Default";
        public string sqlCommand = "";
        private Project? project;

        // ProjectCreate
        public async Task<bool> ProjectCreate(Project project)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@ProjectName", project.ProjectName, DbType.String);
            parameters.Add("@UserEmail", project.UserEmail, DbType.String);

            sqlCommand = "Insert into Project (ProjectName, UserEmail) ";
            sqlCommand += "values(@ProjectName, @UserEmail)";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return true;

        }


        // ProjectRead
        public async Task<IEnumerable<Project>> ProjectReadAll()
        {
            IEnumerable<Project> projects;

            sqlCommand = "Select * from Project ORDER BY ProjectName";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                projects = await conn.QueryAsync<Project>(sqlCommand);
            }
            return projects;
        }


        //ProjectUpdate
        public async Task<bool> ProjectUpdate(Project project)
        {
            var parameters = new DynamicParameters();

            parameters.Add("ProjectID", project.ProjectID, DbType.Int32);
            parameters.Add("ProjectName", project.ProjectName, DbType.String);
            parameters.Add("@UserEmail", project.UserEmail, DbType.String);

            sqlCommand = "Update Project ";
            sqlCommand += "SET ProjectName = @ProjectName, UserEmail = @UserEmail ";
            sqlCommand += "WHERE ProjectID  = @ProjectID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return true;
        }


        //ProjectDelete
        public async Task<bool> ProjectDelete(Int32 ProjectID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProjectID", ProjectID, DbType.Int32);

            //PRAGMA is specific to SQLite and this command is required for DELETE CASCADE to work
            sqlCommand = "PRAGMA foreign_keys = ON;";
            sqlCommand += "Delete from Project ";
            sqlCommand += "WHERE ProjectID  = @ProjectID";

            using IDbConnection conn = new SQLiteConnection(_configuration.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return true;
        }


    }
}
