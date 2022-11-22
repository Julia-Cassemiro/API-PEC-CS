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
    public class CampanhaController : ControllerBase
    {
        private IConfiguration _configuration;

        public CampanhaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ID, ID_Tipo_Campanha, Nome, Fl_Ativo, DT_Criacao from
                            PEC.Campanha
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

        [HttpGet("ativo")]
        public JsonResult GetAtivo()
        {
            string query = @"
                           select ID, ID_Tipo_Campanha, Nome, Fl_Ativo, DT_Criacao from
                            PEC.Campanha where FL_Ativo=1
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
                            select ID, ID_Tipo_Campanha, Nome, Fl_Ativo, DT_Criacao from
                            PEC.Campanha
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
        public JsonResult Post(Campanha camp)
        {
            string query = @"
                            insert into PEC.Campanha
                            (ID_Tipo_Campanha, Nome, Fl_Ativo, DT_Criacao)
                            values (@ID_Tipo_Campanha, @Nome, @Fl_Ativo, @DT_Criacao)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Tipo_Campanha", camp.ID_Tipo_Campanha);
                    myCommand.Parameters.AddWithValue("@Nome", camp.Nome);
                    myCommand.Parameters.AddWithValue("@Fl_Ativo", camp.Fl_Ativo);
                    myCommand.Parameters.AddWithValue("@DT_Criacao", camp.DT_Criacao);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut("{id}")]
        public JsonResult Put(Campanha camp, int id)
        {
            string query = @"
                            update PEC.Campanha
                            set ID_Tipo_Campanha= (@ID_Tipo_Campanha),
                                Nome= (@Nome),
                                Fl_Ativo= (@Fl_Ativo),
                                DT_Criacao= (@DT_Criacao)
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
                    myCommand.Parameters.AddWithValue("@ID_Tipo_Campanha", camp.ID_Tipo_Campanha);
                    myCommand.Parameters.AddWithValue("@Nome", camp.Nome);
                    myCommand.Parameters.AddWithValue("@Fl_Ativo", camp.Fl_Ativo);
                    myCommand.Parameters.AddWithValue("@DT_Criacao", camp.DT_Criacao);
                     
                    
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpPost("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from PEC.Campanha
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

            return new JsonResult("Updated Successfully");
        }
    }
}
