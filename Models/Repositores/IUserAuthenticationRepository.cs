using Microsoft.AspNetCore.Identity;

using SportsClub.Models;
using SportsClub.Core.Requests;

namespace SportsClub.Models.Repositores;

public interface IUserAuthenticationRepository
{
    Task<IdentityResult> RegisterUserAsync(RegisterRequest userForRegistration);
    Task<bool> ValidateUserAsync(LoginRequest loginDto); 
    Task<string> CreateTokenAsync(); 
}

