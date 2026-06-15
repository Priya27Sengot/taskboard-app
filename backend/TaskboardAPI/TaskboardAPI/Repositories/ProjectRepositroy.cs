using Dapper;
using TaskboardAPI.Data;
using TaskboardAPI.Models;

namespace TaskboardAPI.Repositories
{
    public class ProjectRepositroy
    {
        private readonly DbConnectionFactory _factory;
        public ProjectRepositroy(DbConnectionFactory factory)
        {
          _factory = factory;
        }

        public async Task<int> CreateProject(Project project)
        {
            using var connection=_factory.CreateConnection();

            string query = @"INSERT INTO Project (ProjectTitle,Description,WorkspaceId,IsActive,CreatedDate,CreatedBy) VALUES
                            (@ProjectTitle,@Description,@WorkspaceId,1,GETDATE(),@CreatedBy);
                            SELECT CAST(SCOPE_IDENTITY() as int)";
           var result= await connection.ExecuteScalarAsync<int>(query, project);
            return result;
           
        }

        public async Task<List<Project>> GetProjectByWorkspace(int workspaceId)
        {
            using var connection = _factory.CreateConnection();
            string query = "SELECT * FROM Project  where WorkspaceId =@WorkspaceId AND IsActive = 1";
            var projectlist = await connection.QueryAsync<Project>(query, new
            {
                WorkspaceId = workspaceId
            });

            return projectlist.ToList();
        }

        public async Task<int> DeleteProject(int projectId,int workspaceId,int userId) 
        { 
          using var connection= _factory.CreateConnection();
            string query = "UPDATE Project SET IsActive = 0 ,ModifiedBy = @UserId, ModifiedDate =GETDATE() WHERE ProjectId=@ProjectId AND WorkspaceId = @WorkspaceId";
          var result=  await connection.ExecuteAsync(query, new
            {
                ProjectId = projectId,
                WorkspaceId = workspaceId,
                UserId = userId
            });
            return result;
        }

        public async Task<int> UpdateProject(Project updateprj)
        {
            using var connection = _factory.CreateConnection();
            string query = "Update Project SET ProjectTitle=@ProjectTitle,Description = @Description, IsActive =@IsActive , ModifiedBy = @ModifiedBy, ModifiedDate =GETDATE() WHERE ProjectId = @ProjectId";
            var result= await connection.ExecuteAsync(query, updateprj);
            return result;
        }
        public async Task<Project?> GetProjectById(int projId)
        {
            using var connection =_factory.CreateConnection();
            string query = "SELECT * FROM Project where ProjectId=@ProjectId";
            var project = await connection.QueryFirstOrDefaultAsync<Project>(query, new
            {
                ProjectId = projId
            });
            return project;
        }
       
    }
}
