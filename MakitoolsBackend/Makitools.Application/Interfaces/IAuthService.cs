using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UsuarioAuthResponseDto> LoginAsync(LoginRequestDto request);
        Task<bool> SolicitarRecuperacionPasswordAsync(ForgotPasswordRequestDto request);
        Task<bool> ReestablecerPasswordAsync(ResetPasswordRequestDto request);
    }
}
