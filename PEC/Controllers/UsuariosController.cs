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


        // POST: api/Usuarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public object GetIniciarSession(Usuarios_Repres UR)
        {
            var login = UR.CPF_CNPJ;
            var senha = UR.NM_Senha;

            if (login != null && login != "")
            {
                var tbUsuarios = _context.usuario_Repres.Where(q => (login.Length <= 9 && q.ID_Repres == int.Parse(login.ToString())) || (login.Length > 9 && q.CPF_CNPJ == login.ToString())).Take(1).ToList();
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

            //    if (login != null && login != "")
            //    {

            //        var tbUsuarios = usuarios_Repres.Where(u => (login.Trim().Length <= 9 && u.ID_Repres == int.Parse(login.ToString()) || login.Trim().Length > 9 && u.CPF_CNPJ == login.ToString())).Take(1).ToList();
            //        if (tbUsuarios.Count() != 0)
            //        {
            //            //if (tbUsuarios.Single().NM_Senha == Classes.Cryptografia.Criptografia(Autenticar.Password, Classes.Cryptografia.Tipo_Operacao.Cifra))
            //            //{
            //            var user = usuarios.Single(q => q.ID_Usuario == tbUsuarios.Single().ID_Usuario);

            //            var authenticate = true;

            //            if (authenticate)
            //            {
            //                return (user);
            //            }
            //            else
            //            {
            //                return ("error1");
            //            }

            //        }
            //        else
            //        {
            //            return ("error2");
            //        }
            //    }
            //    else
            //    {
            //        return ("error3");
            //    }
            //}
            //var nome = u.NM_Usuario;
            //var senha = u.NM_Senha;
            //var Autenticado = false;
            //try
            //{
            //    var ad = new DirectoryEntry("LDAP://biovetvaxxinova.vgp.local", nome, senha);
            //    var nativeObject = ad.NativeObject;
            //    Autenticado = true;
            //    var user = _context.usuario.Where(u => u.NM_Usuario.Equals(nome)).ToList();

            //    if (user == null)
            //    {
            //        return NotFound();
            //    }

            //    return user;
            //}
            //catch (Exception ex)
            //{
            //    return NotFound();
            //}
        }


    }
}

