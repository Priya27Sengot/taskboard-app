using Dapper;
using System.Data;
using TaskboardAPI.Data;
using TaskboardAPI.Models;

namespace TaskboardAPI.Repositories
{
    public class WorkspaceRepository
    {
        private readonly DbConnectionFactory _factory;
        public WorkspaceRepository(DbConnectionFactory factory)
        {
          _factory = factory;
        }

        public async Task<int> CreateWorkspace(Workspace workspace)
        {
            using var connection=_factory.CreateConnection();

            string query = @"INSERT INTO Workspace (WorkspaceName,Description,UserId,CreatedBy,CreatedDate) VALUES (@WorkspaceName,@Description,@UserId,@CreatedBy,GETDATE());
                SELECT CAST(SCOPE_IDENTITY() as int)";
            var result= await connection.ExecuteScalarAsync<int>(query, workspace);
            return result;
        
        }

        public async Task<List<WorkspaceList>> GetWorkspaceByUserId(int userId)
        {
            using var connection=_factory.CreateConnection();          
            var workspaces= await connection.QueryAsync<WorkspaceList>("GetWorkspaceByUser",
                new
                {
                    UserId = userId
                },
                commandType:CommandType.StoredProcedure);
            return workspaces.ToList();
        }

        public async Task<int> DeleteWorkspace(int workspaceId,int userId)
        {
            using var connection = _factory.CreateConnection();          
           var result =await connection.ExecuteAsync("DeleteWorkspace", new
            {
                WorkspaceId = workspaceId,
                UserId = userId
            },
            commandType:CommandType.StoredProcedure);
            return result;
        }

        public async Task<Workspace?> GetWorkSpaceById(int workspaceId,int userId)
        {
            using var connection=_factory.CreateConnection();
            string query = "SELECT * from Workspace Where WorkspaceId =@WorkspaceId AND UserId=@UserId";
            var workspace = await connection.QueryFirstOrDefaultAsync<Workspace>(query, new
            {
                WorkspaceId = workspaceId,
                UserId = userId
            });

            return workspace;
        }
        public async Task<int> UpdateWorkspace(Workspace updateObj)
        {
            using var connection = _factory.CreateConnection();
            string query = "Update Workspace SET WorkspaceName=@WorkspaceName,Description = @Description, ModifiedBy = @ModifiedBy, ModifiedDate =GETDATE() WHERE WorkspaceId = @WorkspaceId";
            var result = await connection.ExecuteAsync(query, updateObj);
            return result;
        }

    }
}
