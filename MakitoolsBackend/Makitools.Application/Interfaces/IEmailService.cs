using System;
using System.Collections.Generic;
using System.Text;

namespace Makitools.Application.Interfaces
{
    public interface IEmailService
    {
        Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpoHTML);
    }
}
