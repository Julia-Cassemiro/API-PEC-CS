using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PEC.Models;
using System.Data;
using System.Data.SqlClient;

namespace PEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampanhaClienteController : ControllerBase
    {
        private IConfiguration _configuration;

        public CampanhaClienteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                           select ID, ID_InvestCampanha, ID_Cliente, Fl_Ativo, DT_Criacao, DT_Alteracao, FL_Removido from
                            PEC.InvestCampanhaCliente
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpGet("{id}")]
        public JsonResult GetID(int id)
        {
            string query = @"
                            select ID, ID_InvestCampanha, ID_Cliente, Fl_Ativo, DT_Criacao, DT_Alteracao, FL_Removido from
                            PEC.InvestCampanhaCliente
                            where ID_InvestCampanha=@ID_InvestCampanha
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_InvestCampanha", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(CampanhaCliente camp)
        {
            string query = @"
                            insert into PEC.InvestCampanhaCliente
                            (ID_InvestCampanha, ID_Cliente, Fl_Ativo, DT_Criacao, DT_Alteracao, FL_Removido)
                            values (@ID_InvestCampanha, @ID_Cliente, @Fl_Ativo, @DT_Criacao, @DT_Alteracao, @FL_Removido)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_InvestCampanha", camp.ID_InvestCampanha);
                    myCommand.Parameters.AddWithValue("@ID_Cliente", camp.ID_Cliente);
                    myCommand.Parameters.AddWithValue("@Fl_Ativo", camp.Fl_Ativo);
                    myCommand.Parameters.AddWithValue("@DT_Criacao", camp.DT_Criacao);
                    myCommand.Parameters.AddWithValue("@DT_Alteracao", camp.DT_Alteracao);
                    myCommand.Parameters.AddWithValue("@FL_Removido", camp.FL_Removido);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpDelete("{id_ic}/{id_cliente}")]
        public JsonResult GetId(int id_ic, int id_cliente)
        {
            string query = @"
                            delete from PEC.InvestCampanhaCliente
                            where ID_InvestCampanha=@ID_InvestCampanha and ID_Cliente=@ID_Cliente
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_InvestCampanha", id_ic);
                    myCommand.Parameters.AddWithValue("@ID_Cliente", id_cliente);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
