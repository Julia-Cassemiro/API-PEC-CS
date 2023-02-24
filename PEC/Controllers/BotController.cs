
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


            var especialidades = _context.Especialidade.ToList();

            var items = new List<dynamic>();


            foreach (var especialidade in especialidades)
            {
                var item = new
                {
                    number = especialidade.IdEspecialidade,
                    text = especialidade.NmEspecialidade, // supondo que o nome da especialidade está na propriedade "NmEspecialidade"
                    callback = new Callback()
                    {
                        endpoint = "https://vaxxinova.ind.br/pecapp/api/bot/especialidade",
                        data = new Data()
                        {
                            example = $"Especialidade {especialidade.NmEspecialidade} (text, text text..)",
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



        [HttpPost("especialidade")]
        public object Post7(Credentials_Request bot)
        {
            var id_especialidade = bot.Text;

            var especialidades = _context.Especialidade.Single(e => e.IdEspecialidade.ToString() == id_especialidade);


            var response = new Credentials_Response
            {
                Attachments = new Attachments
                {
                    Position = "AFTER",  //copiei pdf
                    Type = "DOCUMENT",
                    Name = "invoice.pdf",
                    Url = "https/teste.com.br"

                },
                Type = "INFORMATION",
                Text = "\nVocê escolheu: " + especialidades.NmEspecialidade.Trim() + ", por favor aguarde.",

            };

            return new JsonResult(response);
        }


    }
}
