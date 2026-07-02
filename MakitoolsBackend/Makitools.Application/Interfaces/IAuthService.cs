using Makitools.Application.DTOs.Requests;
using Makitools.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UsuarioResponseDto> LoginAsync(LoginRequestDto request);
    }
}
