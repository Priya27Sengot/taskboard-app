using TaskboardAPI.Repositories;
using BCrypt;
using TaskboardAPI.Models;

namespace TaskboardAPI.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepo;
        private readonly JWTService _jwtService;
        public AuthService(UserRepository userRepo,JWTService jwtService)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;

        }

        public async Task<APIResponse> RegisterUser(string name, string email, string password)
        {

            APIResponse response =new APIResponse();
            try
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    response.Success = false;
                    response.Description = "Input values cannot be null";
                    return response;
                }
                var existinguser = await _userRepo.GetUserByEmail(email);

                if (existinguser != null)
                {
                    response.Success = false;
                    response.Description = "User already Exists";
                    return response;
                }
               

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var user = new Users
                {
                    UserName = name,
                    Email = email,
                    PasswordHash = hashedPassword,
                    IsActive = true
                };
                var result = await _userRepo.CreateUser(user);


                if (result>0)
                {
                    response.Success = true;
                    response.Description = "User Created successfully";                    
                }
                else
                {
                    response.Success = true;
                    response.Description = "User Creation Failed";
                }

               
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> Login(string email,string password)
        {
            APIResponse response = new APIResponse();
            UserDetails userDetails = new UserDetails();
            try
            {
                var user = await _userRepo.GetUserByEmail(email);               
                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    response.Success = false;
                    response.Description = "Invalid Username or password";
                    return response;
                }
                var token = _jwtService.GenerateToken(user.UserId, user.Email);

                userDetails.Token = token;
                userDetails.UserId=user.UserId;
                userDetails.Username = user.UserName;

                response.Success = true;
                response.Data = userDetails;
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            

            return response;
        }
    }
}