using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PEC.Context;
using PEC.Models;
using DirectoryEntry = System.DirectoryServices.DirectoryEntry;

namespace PEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public object GetIniciarSession(Usuarios_Repres UR)
        {
            var login = UR.CPF_CNPJ;
            var senha = UR.NM_Senha;

            if (login != null && login != "")
            {
                //var teste = _context.usuario_Repres.Where(q => (login.Length <= 9 && q.ID_Repres == int.Parse(login.ToString())) || (login.Length > 9 && q.CPF_CNPJ == login.ToString())).Take(1).ToList();
                var tbUsuarios = _context.usuario_Repres.Where(q => ((bool)IsNumeric(login) && login.Length <= 9) ?
                                    q.ID_Repres.ToString() == login : (q.CPF_CNPJ == login)).Take(1).ToList();

                if (tbUsuarios.Count() != 0)
                {
                    if (tbUsuarios.Single().NM_Senha == Classes.Cryptografia.Criptografia(senha, Classes.Cryptografia.Tipo_Operacao.Cifra))
                    {
                        var user = _context.usuario_Repres.Single(q => q.ID_Usuario == tbUsuarios.Single().ID_Usuario);
                        var authenticate = true;

                        if (authenticate)
                        {
                            try
                            {
                                return (authenticate, user.ID_Repres);
                            }
                            catch
                            {
                                return ("Usuario não cadastrado");
                            }
                        }
                        else
                            return ("error1");
                    }
                    else
                        return ("error2");
                }
                else
                    return ("error3");
            }
            else
                return ("error4");
        }

        protected  IsNumeric(this string text)
        {
            return double.TryParse(text, out double test);
        }
    }
}

