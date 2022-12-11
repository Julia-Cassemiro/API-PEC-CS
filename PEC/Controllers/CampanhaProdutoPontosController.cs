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
    public class CampanhaProdutoPontosController : ControllerBase
    {
        private IConfiguration _configuration;

        public CampanhaProdutoPontosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                           select ID, ID_Campanha, ID_Produto, Fl_Ativo, DT_Criacao, Pontos from
                            PEC.CampanhaProdutoPontos
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
                          select ID_Campanha,ID_Produto, m.DS_ITEM, CPP.DT_Criacao,Fl_Ativo,Pontos
		                    from		PEC.CampanhaProdutoPontos as CPP 
		                        inner join  PEC. MATERS as M 
		                         on  M.CD_ITEM = CPP.ID_Produto
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
                    myCommand.Parameters.AddWithValue("@ID_Campanha", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(CampanhaProdutoPontos camp)
        {
            string query = @"
                            insert into PEC.CampanhaProdutoPontos
                            (ID_Campanha, ID_Produto, Fl_Ativo, DT_Criacao, Pontos)
                            values (@ID_Campanha, @ID_Produto, @Fl_Ativo, @DT_Criacao, @Pontos)
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
                    myCommand.Parameters.AddWithValue("@ID_Produto", camp.ID_Produto);
                    myCommand.Parameters.AddWithValue("@Fl_Ativo", camp.FL_Ativo);
                    myCommand.Parameters.AddWithValue("@DT_Criacao", camp.DT_Criacao);
                    myCommand.Parameters.AddWithValue("@Pontos", camp.Pontos);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPost("{id_ic}/{id_prod}")]
        public JsonResult GetId(int id_ic, int id_prod)
        {
            string query = @"
                            delete from PEC.CampanhaProdutoPontos
                            where ID_Campanha=@ID_Campanha and ID_Produto=@ID_Produto
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
                    myCommand.Parameters.AddWithValue("@ID_Produto", id_prod);
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
