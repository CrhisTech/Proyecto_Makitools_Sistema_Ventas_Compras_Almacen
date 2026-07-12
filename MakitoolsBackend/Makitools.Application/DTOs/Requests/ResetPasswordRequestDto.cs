using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.DTOs.Requests
{
    public class ResetPasswordRequestDto
    {
        public string Token { get; set; } = null!;
        public string NuevaClave { get; set; } = null!;
    }
}
