using Dapper;
using TaskboardAPI.Data;
using TaskboardAPI.Models;

namespace TaskboardAPI.Repositories
{
    public class TaskRepository
    {
        private readonly DbConnectionFactory _factory;

        public TaskRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<int> CreateTask(TaskItem task)
        {
            using var connection =_factory.CreateConnection();

            string query = @"INSERT INTO TASK(TaskName,ProjectId,AssignedToUser,IsActive,Status,DueDate,CreatedBy,CreatedDate) VALUES (@TaskName,@ProjectId,@AssignedToUser,1,@Status,@DueDate,@CreatedBy,GETDATE())
                            SELECT CAST(SCOPE_IDENTITY() as int)";

            var result= await connection.ExecuteScalarAsync<int>(query, task);
            return result;
        }

        public async Task<List<TaskList>> GetTasksByProject(int projectId)
        {
            using var connection=_factory.CreateConnection();

            string query = "select T.*,P.ProjectTitle,S.Description As Status,U.UserName,T.IsActive,T.DueDate from Task T Inner join Project P on T.ProjectId = P.ProjectId Inner  Join Status S on S.Id =T.Status inner join Users U on U.UserId=T.AssignedToUser where T.ProjectId =@ProjectId and T.IsActive=1 ";

            var taskList = await connection.QueryAsync<TaskList>(query, new
            {
                ProjectId = projectId
            });

            return taskList.ToList();
        }

        public async Task<int> DeleteTask(int taskId, int projectId,int userId)
        {
            using var connection = _factory.CreateConnection();

            string query = "UPDATE TASK SET IsActive = 0 ,ModifiedBy = @UserId, ModifiedDate =GETDATE() WHERE TaskId=@TaskId AND ProjectId=@ProjectId";

            var result= await connection.ExecuteAsync(query, new
            {
                TaskId = taskId,
                ProjectId = projectId,
                UserId=userId
            });
            return result;
        }

        public async Task<int> UpdateTask(TaskItem updateTask,int userId)
        {
            using var connection = _factory.CreateConnection();

            string query = "UPDATE Task SET TaskName=@TaskName,ProjectId = @ProjectId,AssignedToUser =@AssignedToUser,Status = @Status,DueDate=@DueDate,ModifiedBy=@ModifiedBy,ModifiedDate=GETDATE() WHERE TaskId=@TaskId";

            var result= await connection.ExecuteAsync(query, updateTask);
            return result;

        }

        public async Task<Project?> ValidateProject(int projectId,int userId)
        {
            using var connection = _factory.CreateConnection();

            string query = "SELECT P.* FROM Project P INNER JOIN Workspace W ON P.WorkspaceId = W.WorkspaceId WHERE P.IsActive =1 AND P.ProjectId=@ProjectId AND W.UserId=@UserId";

            var project= await connection.QueryFirstOrDefaultAsync<Project>(query, new
            {
                ProjectId = projectId,
                UserId = userId
            });
            return project;
        }

        public async Task<TaskItem> GetTaskById(int taskId)
        {
            using var connection = _factory.CreateConnection();
            string query = "SELECT * from TASK WHERE TaskId=@TaskId AND IsActive= 1";
            var project = await connection.QueryFirstOrDefaultAsync<TaskItem>(query, new
            {
                TaskId = taskId
            });
            return project;
        }

        public async Task<List<Project>> GetProjectsByUserId(int userId)
        {
            using var connection = _factory.CreateConnection();
            string query = "SELECT * FROM Project P INNER JOIN Workspace W  on P.WorkspaceId= W.WorkspaceId WHERE W.UserId = @UserId and P.IsActive=1";
            var projectList = await connection.QueryAsync<Project>(query, new
            {                
                    UserId=userId
                
            });
            return projectList.ToList();
        }

        public async Task<List<Members>> GetOtherUsers(int userId)
        {
            using var connection = _factory.CreateConnection();
            string query = "SELECT * FROM Users";
            var userList = await connection.QueryAsync<Members>(query, new
            {
                userId = userId
            });
            return userList.ToList();
            
        }

        public async Task<List<Status>> GetStatus()
        {
            using var connection = _factory.CreateConnection();
            string query = "SELECT * FROM Status";
            var statusList = await connection.QueryAsync<Status>(query);
            return statusList.ToList();
        }

    }
}
