using BabySounds.Contracts.Requests;
using BabySounds.Contracts.Responses;
using ErrorOr;

namespace BabySounds.Client.Services;

internal interface IAuthService
{
    Task<ErrorOr<RegisterResponse>> Register(RegisterRequest request);
    Task<ErrorOr<LoginResponse>> Login(LoginRequest request);
    Task Logout();
}
