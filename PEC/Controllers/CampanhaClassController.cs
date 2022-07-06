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
    public class CampanhaClassController : ControllerBase
    {
        private IConfiguration _configuration;

        public CampanhaClassController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ID, ID_Campanha, ID_Class_Pec, Fl_Ativo, DT_Criacao from
                            PEC.CampanhaClass
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
                            select ID, ID_Campanha, ID_Class_Pec, Fl_Ativo, DT_Criacao from
                            PEC.CampanhaClass
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
        public JsonResult Post(CampanhaClass camp)
        {
            string query = @"
                            insert into PEC.CampanhaClass
                           (ID_Campanha, ID_Class_Pec, Fl_Ativo, DT_Criacao)
                            values (@ID_Campanha, @ID_Class_Pec, @Fl_Ativo, @DT_Criacao)
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
                    myCommand.Parameters.AddWithValue("@ID_Class_Pec", camp.ID_Class_Pec);
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
        public JsonResult Put(CampanhaClass camp, int id)
        {
            string query = @"
                            update PEC.CampanhaClass
                            set ID_Campanha= (@ID_Campanha),
                            ID_Class_Pec= (@ID_Class_Pec),
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
                    myCommand.Parameters.AddWithValue("@ID_Campanha", camp.ID_Campanha);
                    myCommand.Parameters.AddWithValue("@ID_Class_Pec", camp.ID_Class_Pec);
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

        [HttpDelete("{id}/{id_class}")]
        public JsonResult GetId(int id, int id_class)
        {
            string query = @"
                            delete from PEC.CampanhaClass
                            where ID_Campanha=@ID_Campanha and ID_Class_Pec=@ID_Class_Pec
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
                    myCommand.Parameters.AddWithValue("@ID_Class_Pec", id_class);
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
