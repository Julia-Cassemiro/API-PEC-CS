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
    public class CampanhaBrindesClienteController : ControllerBase
    {
        private IConfiguration _configuration;

        public CampanhaBrindesClienteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ID, ID_Campanha, ID_Brinde, ID_Cliente, DT_Brinde, Qtde, Motivo from
                            PEC.CampanhaBrindesCliente
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
                            select ID, ID_Campanha, ID_Brinde, ID_Cliente, DT_Brinde, Qtde, Motivo from
                            PEC.CampanhaBrindesCliente
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
        public JsonResult Post(CampanhaBrindesCliente camp)
        {
            string query = @"
                            insert into PEC.CampanhaBrindesCliente
                            (ID_Campanha, ID_Brinde, ID_Cliente, DT_Brinde, Qtde, Motivo)
                            values (@ID_Campanha, @ID_Brinde, @ID_Cliente, @DT_Brinde, @Qtde,  @Motivo)
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
                    myCommand.Parameters.AddWithValue("@ID_Brinde", camp.ID_Brinde);
                    myCommand.Parameters.AddWithValue("@ID_Cliente", camp.ID_Cliente);
                    myCommand.Parameters.AddWithValue("@DT_Brinde", camp.DT_Brinde);
                    myCommand.Parameters.AddWithValue("@Qtde", camp.Qtde);
                    myCommand.Parameters.AddWithValue("@Motivo", camp.Motivo);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPost("put/{id}")]
        public JsonResult Put(CampanhaBrindesCliente camp, int id)
        {
            string query = @"
                            update PEC.CampanhaBrindesCliente
                            set ID_Campanha= (@ID_Campanha),
                            ID_Brinde= (@ID_Brinde),
                            ID_Cliente= (@ID_Cliente),
                            DT_Brinde= (@DT_Brinde),
                            Qtde= (@Qtde),
                            Motivo= (@Motivo)
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
                    myCommand.Parameters.AddWithValue("@ID_Brinde", camp.ID_Brinde);
                    myCommand.Parameters.AddWithValue("@ID_Cliente", camp.ID_Cliente);
                    myCommand.Parameters.AddWithValue("@DT_Brinde", camp.DT_Brinde);
                    myCommand.Parameters.AddWithValue("@Qtde", camp.Qtde);
                    myCommand.Parameters.AddWithValue("@Motivo", camp.Motivo);


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
                            delete from PEC.CampanhaBrindesCliente
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
