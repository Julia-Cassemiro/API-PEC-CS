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
    public class CampanhaGrupoProdutoController : ControllerBase
    {
        private IConfiguration _configuration;

        public CampanhaGrupoProdutoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                           select ID, ID_Campanha, ID_Grupo_Produto, Fl_Ativo, DT_Criacao from
                            PEC.CampanhaGrupoProduto
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
                            select ID, ID_Campanha, ID_Grupo_Produto, Fl_Ativo, DT_Criacao from
                            PEC.CampanhaGrupoProduto
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
        public JsonResult Post(CampanhaGrupoProduto camp)
        {
            string query = @"
                            insert into PEC.CampanhaGrupoProduto
                            (ID_Campanha, ID_Grupo_Produto, Fl_Ativo, DT_Criacao)
                            values (@ID_Campanha, @ID_Grupo_Produto, @Fl_Ativo, @DT_Criacao)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Campanha", camp.ID_Campanha);
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", camp.ID_Grupo_Produto);
                    myCommand.Parameters.AddWithValue("@Fl_Ativo", camp.FL_Ativo);
                    myCommand.Parameters.AddWithValue("@DT_Criacao", camp.DT_Criacao);

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
                            delete from PEC.CampanhaGrupoProduto
                            where ID_Campanha=@ID_Campanha
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Campanha", id_ic);
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
