
using API_Clinica.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PEC.Context;
using PEC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly ADSCentralContext _context;
        private readonly ADSCentralContextProcedures _contextProcedures;

        public BotController(ADSCentralContext context, ADSCentralContextProcedures contextProcedures)
        {
            _context = context;
            _contextProcedures = contextProcedures;
        }


        [HttpGet]
        public async Task<object> GetAsync()
        {
            var teste = await _contextProcedures.usp_Medico_Espec_ConvenioChatAsync(90, 3);

            return new JsonResult(teste);
        }

        public class Callback
        {
            public string endpoint { get; set; }
            public Data data { get; set; }
        }
        public class Data
        {
            public string example { get; set; }
            public string id_especialidade { get; set; }
            public string id_medico { get; set; }
            public string nome { get; set; }

        }
        public class Items
        {
            public int number { get; set; }
            public string text { get; set; }
            public Callback callback { get; set; }
        }

        // POST api/<BotController>
        [HttpPost]
        public object Post(Credentials_Request bot)
        {
            var Id = bot.Id;
            var Text = bot.Text;
            var Data = bot.Data;


            var Contact = bot.Contact;
            var uid = Contact.Uid;
            var type = Contact.Type;
            var Key = Contact.Key;
            var Name = Contact.Name;


            var Fields = Contact.Fields;
            var CPF = Fields.Cpf;
            var celular = Fields.Celular;


            //_context.Especialidade.ToList();
            var especialidades = _context.VwEspecialidadeConvenioChat.ToList().Where(e => e.IdConvenio == 90);


            var items = new List<dynamic>();


            foreach (var especialidade in especialidades)
            {
                var item = new
                {
                    number = especialidade.Codigo,
                    text = especialidade.NmEspecialidade, // supondo que o nome da especialidade está na propriedade "NmEspecialidade"
                    callback = new Callback()
                    {
                        endpoint = "https://vaxxinova.ind.br/centralcadastro/api/bot/medico",
                        data = new Data()
                        {
                            example = "example: "+ especialidade.IdEspecialidade,
                        },
                    },
                };

                items.Add(item);
            }

            var body = new
            {
                type = "MENU",
                text = "Para selecionar uma especialidade",
                attachments = new[]
                {
                    new
                    {
                        position = "BEFORE",
                        type = "IMAGE",
                        name = "image.png",
                        url = "https://yourdomain.com/cdn/logo.png"
                    }
                },
                items = items.ToArray()
            };

            return new JsonResult(body);
        }



        [HttpPost("medico")]
        public object Post7(Credentials_Request bot)
        {
            var id_especialidade = bot.Text;

            //var especialidades = _context.Especialidade.Single(e => e.IdEspecialidade.ToString() == id_especialidade);
            var medicos = _contextProcedures.usp_Medico_Espec_ConvenioChatAsync(90, int.Parse(id_especialidade)).Result;
       

            var items = new List<dynamic>();
            foreach (var medico in medicos)
            {
                var item = new
                {
                    number = medico.ID_Medico,
                    text = medico.NM_Medico, // supondo que o nome da especialidade está na propriedade "NmEspecialidade"
                    callback = new Callback()
                    {
                        endpoint = "https://vaxxinova.ind.br/centralcadastro/api/bot/horario",
                        data = new Data()
                        {
                            id_medico = medico.ID_Medico.ToString(),
                            id_especialidade = id_especialidade,
                        },
                    },
                };

                items.Add(item);
            }

            var body = new
            {
                type = "MENU",
                text = "Para selecionar um médico",
                attachments = new[]
                {
                    new
                    {
                        position = "BEFORE",
                        type = "IMAGE",
                        name = "image.png",
                        url = "https://yourdomain.com/cdn/logo.png"
                    }
                },
                items = items.ToArray()
            };

            return new JsonResult(body);
        }



        [HttpPost("horario")]
        public object horario(Credentials_Request bot)
        {
            var id_especialidade = bot.Data.id_especialidade;
            //var id_medico = bot.Data.id_medico;
            var id_medico = "4557";
            var date = DateTime.Now;


            //var especialidades = _context.Especialidade.Single(e => e.IdEspecialidade.ToString() == id_especialidade);
            var horarios = _contextProcedures.usp_Separar_Consulta_AgendaChatAsync(int.Parse(id_especialidade), int.Parse(id_medico), date, 90).Result;
            //exec usp_Separar_Consulta_AgendaChat idespecialidade, idMedico, data,90

            var items = new List<dynamic>();
            foreach (var horario in horarios)
            {
                var item = new
                {
                    number = horario.Ordem,
                    text = horario.Data + "\n" + horario.HS_Consulta, // supondo que o nome da especialidade está na propriedade "NmEspecialidade"
                    callback = new Callback()
                    {
                        endpoint = "https://vaxxinova.ind.br/pecapp/api/bot/horario",
                        data = new Data()
                        {
                            example = horario.TipoConsulta,
                        },
                    },
                };

                items.Add(item);
            }

            var body = new
            {
                type = "MENU",
                text = "Para selecionar um horário",
                attachments = new[]
                {
                    new
                    {
                        position = "BEFORE",
                        type = "IMAGE",
                        name = "image.png",
                        url = "https://yourdomain.com/cdn/logo.png"
                    }
                },
                items = items.ToArray()
            };

            return new JsonResult(horarios);
        }

    }
}
