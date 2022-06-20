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
    public class CampanhaProdutoController : ControllerBase
    {
        private IConfiguration _configuration;

        public CampanhaProdutoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                           select ID, ID_InvestCampanha, Nome, Fl_Ativo, DT_Criacao, DT_Alteracao, FL_Removido from
                            PEC.InvestCampanhaProduto
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
                            select ID, ID_InvestCampanha, Nome, Fl_Ativo, DT_Criacao, DT_Alteracao, FL_Removido from
                            PEC.InvestCampanhaProduto
                            where ID=@ID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(CampanhaProduto camp)
        {
            string query = @"
                            insert into PEC.InvestCampanhaProduto
                            (ID_InvestCampanha, Nome, Fl_Ativo, DT_Criacao, DT_Alteracao, FL_Removido)
                            values (@ID_InvestCampanha, @Nome, @Fl_Ativo, @DT_Criacao, @DT_Alteracao, @FL_Removido)
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
                    myCommand.Parameters.AddWithValue("@Nome", camp.Nome);
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

        [HttpDelete("{id}/{id_ic}")]
        public JsonResult GetId(int id_ic)
        {
            string query = @"
                            delete from PEC.InvestCampanhaProduto
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
                    myCommand.Parameters.AddWithValue("@ID_InvestCampanha", id_ic);
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
