using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        public object GetIniciarSessionCNPJ(Usuarios_Repres UR)
        {
            var login = UR.CPF_CNPJ;
            var senha = UR.NM_Senha;
            var authenticate = false;


            if (login != null && login != "")
            {

                try
                {
                    var ad = new DirectoryEntry("LDAP://biovetvaxxinova.vgp.local", login, senha);
                    var nativeObject = ad.NativeObject;
                    var auth = _context.usuarios.Where(u => u.NmUsuario.Equals(login)).ToList();
                    var user = _context.usuarios.Single(u => u.NmUsuario.Equals(login));

                    if (auth == null)
                    {
                        return NotFound();
                    }
                    if (auth.Count() > 0)
                    {
                        authenticate = true;
                    }
                    else
                        return authenticate;
                    if (authenticate)
                    {
                        var ID_Usuario = user.IdUsuario;
                        int VE_SISTEMA = 37;
                        if ((ID_Usuario.ToString() == "" ? 0 : int.Parse(ID_Usuario.ToString())) > 0)
                        {
                            var Menu = _context.VwPecMenuUsuarios.Where(v => v.IdUsuario == ID_Usuario && v.IdSistema == VE_SISTEMA).Select(v => new { v.NM_Url });
                            if (Menu.Count() > 0)
                            {
                                try
                                {
                                    return (authenticate, user.IdUsuario, Menu);
                                }
                                catch
                                {
                                    return ("Usuario não cadastrado");
                                }
                            }
                        }
                    }

                    return user;
                }
                catch
                {
                    login = login.Replace(".", "").Replace("/", "").Replace("-", "");
                    var tbUsuarios = _context.usuario_Repres.Where(q => (login.Length <= 9 && q.ID_Repres == int.Parse(login.ToString())) || (login.Length > 9 && q.CPF_CNPJ == login.ToString())).Take(1).ToList();

                    if (tbUsuarios.Count() != 0)
                    {
                        if (tbUsuarios.Single().NM_Senha == Classes.Cryptografia.Criptografia(senha, Classes.Cryptografia.Tipo_Operacao.Cifra))
                        {

                            var auth = _context.usuario_Repres.Where(q => q.ID_Usuario == tbUsuarios.Single().ID_Usuario).ToList();
                            var user = _context.usuario_Repres.Single(q => q.ID_Usuario == tbUsuarios.Single().ID_Usuario);
                            if (auth.Count() > 0)
                            {
                                authenticate = true;
                            }
                            else
                                return authenticate;
                            if (authenticate)
                            {
                                var ID_Usuario = user.ID_Usuario;
                                int VE_SISTEMA = 37;
                                if ((ID_Usuario.ToString() == "" ? 0 : int.Parse(ID_Usuario.ToString())) > 0)
                                {
                                    var Menu = _context.VwPecMenuUsuarios.Where(v => v.IdUsuario == ID_Usuario && v.IdSistema == VE_SISTEMA).Select(v => new { v.NM_Url });
                                    if (Menu.Count() > 0)
                                    {
                                        try
                                        {
                                            return (authenticate, user.ID_Usuario, Menu);
                                        }
                                        catch
                                        {
                                            return ("Usuario não cadastrado");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return (authenticate);
        }




        [HttpPost("{Menu}")]
        public object GetMenu(Get G)
        {
            var ID_Usuario = G.ID_Usuario;
            var Tela = G.NR_Url;
            int VE_SISTEMA = 37;
            var teste = false;

            if ((ID_Usuario.ToString() == "" ? 0 : int.Parse(ID_Usuario.ToString())) > 0)
            {
                var Menu = _context.menu.Where(M => M.NM_Url.Contains(Tela) && M.ID_Sistema == VE_SISTEMA).ToList();
                if (Menu.Count() > 0)
                {
                    var Menu_U = _context.menu_usuario.Where(U => U.ID_Menu == Menu.Single().ID_Menu && U.ID_Usuario == int.Parse(ID_Usuario.ToString()));
                    if (Menu_U.Count() > 0)
                    {
                        teste = true;
                    }
                }
            }
            return (teste);
        }


    }
}

