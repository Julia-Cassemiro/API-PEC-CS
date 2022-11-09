using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PEC.Context;
using PEC.Models;

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
                var tbUsuarios = _context.usuario_Repres.Where(q => (login.Length <= 9 && q.ID_Repres == int.Parse(login.ToString())) || (login.Length > 9 && q.CPF_CNPJ == login.ToString())).Take(1).ToList();

                if (tbUsuarios.Count() != 0)
                {
                    if (tbUsuarios.Single().NM_Senha == Classes.Cryptografia.Criptografia(senha, Classes.Cryptografia.Tipo_Operacao.Cifra))
                    {
                        var user = _context.usuario_Repres.Single(q => q.ID_Usuario == tbUsuarios.Single().ID_Usuario);
                        var authenticate = true;

                        if (authenticate)
                        {
                            var ID_Usuario = user.ID_Usuario;
                            //var Tela = G.NR_Url;
                            int VE_SISTEMA = 37;
                            //var teste = false;
                            if ((ID_Usuario == null || ID_Usuario.ToString() == "" ? 0 : int.Parse(ID_Usuario.ToString())) > 0)
                            {
                                //var Menu = _context.menu.Where(M => M.NM_Url.Contains(Tela) && M.ID_Sistema == VE_SISTEMA).ToList();
                                var Menu = _context.VwPecMenuUsuarios.Where(v => v.IdUsuario == ID_Usuario && v.IdSistema == VE_SISTEMA).Select(v => new { v.NM_Url });
                                if (Menu.Count() > 0)
                                {
                                    //var Menu_U = _context.MEN
                                    //if (Menu_U.Count() > 0)
                                    //{
                                    //    teste = true;
                                    //    //return (true);
                                    //}
                                    try
                                    {
                                        return (authenticate, user.ID_Usuario, Menu);
                                    }
                                    catch
                                    {
                                        return ("Usuario não cadastrado");
                                    }
                                }
                                else
                                    return ("error0");
                            }
                            else
                                return ("error0.2");

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

        //protected  IsNumeric(this string text)
        //{
        //    return double.TryParse(text, out double test);
        //}

        [HttpPost("{Menu}")]
        public object GetMenu(Get G)
        {
            var ID_Usuario = G.ID_Usuario;
            var Tela = G.NR_Url;
            int VE_SISTEMA = 37;
            var teste = false;

            if ((ID_Usuario == null || ID_Usuario.ToString() == "" ? 0 : int.Parse(ID_Usuario.ToString())) > 0)
            {
                //var Menu = _context.menu.Where(M => M.NM_Url.Contains(Tela) && M.ID_Sistema == VE_SISTEMA).ToList();
                var Menu = _context.menu.Where(M => M.NM_Url.Contains(Tela) && M.ID_Sistema == VE_SISTEMA).ToList();
                if (Menu.Count() > 0)
                {
                    var Menu_U = _context.menu_usuario.Where(U => U.ID_Menu == Menu.Single().ID_Menu && U.ID_Usuario == int.Parse(ID_Usuario.ToString()));
                    if (Menu_U.Count() > 0)
                    {
                        teste = true;
                        //return (true);
                    }
                }
            }
            return(teste);
        }


    }
}

