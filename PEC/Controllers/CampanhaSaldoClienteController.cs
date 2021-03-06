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
    public class CampanhaSaldoClienteController : ControllerBase
    {
        private IConfiguration _configuration;

        public CampanhaSaldoClienteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ID, ID_Campanha, DT_Inicio, ID_Cliente, Saldo, Saldo_Apropriado, Saldo_Disponivel from
                            PEC.CampanhaSaldoCliente
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
                            select ID, ID_Campanha, DT_Inicio, ID_Cliente, Saldo, Saldo_Apropriado, Saldo_Disponivel from
                            PEC.CampanhaSaldoCliente
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

        [HttpGet("{id}/{id_cli}")]
        public JsonResult GetIDCliente(int id, int id_cli)
        {
            string query = @"
                            select ID, ID_Campanha, DT_Inicio, ID_Cliente, Saldo, Saldo_Apropriado, Saldo_Disponivel from
                            PEC.CampanhaSaldoCliente
                            where ID_Campanha=@ID_Campanha and ID_Cliente=@ID_Cliente
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
                    myCommand.Parameters.AddWithValue("@ID_Cliente", id_cli);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(CampanhaSaldoCliente camp)
        {

            string query = @"
                            insert into PEC.CampanhaSaldoCliente
                            (ID_Campanha, DT_Inicio, ID_Cliente, Saldo_Apropriado, Saldo_Disponivel)
                            values (@ID_Campanha, @DT_Inicio, @ID_Cliente, @Saldo_Apropriado,  @Saldo_Disponivel)
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
                    myCommand.Parameters.AddWithValue("@DT_Inicio", camp.DT_Inicio);
                    myCommand.Parameters.AddWithValue("@ID_Cliente", camp.ID_Cliente);
                    myCommand.Parameters.AddWithValue("@Saldo_Apropriado", camp.Saldo_Apropriado);
                    myCommand.Parameters.AddWithValue("@Saldo_Disponivel", camp.Saldo_Disponivel);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut("{id}/{id_cli}")]
        public JsonResult Put(CampanhaSaldoCliente camp, int id, int id_cli)
        {
            string query = @"
                            update PEC.CampanhaSaldoCliente
                            set
                            Saldo_Disponivel= (@Saldo_Disponivel)
                            where ID_Campanha=@ID_Campanha and ID_Cliente=(@ID_Cliente)
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
                    myCommand.Parameters.AddWithValue("@ID_Cliente", id_cli);
                    myCommand.Parameters.AddWithValue("@Saldo_Disponivel", camp.Saldo_Disponivel);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}/{id_cli}")]
        public JsonResult GetId(int id, int id_cli)
        {
            string query = @"
                            delete from PEC.CampanhaSaldoCliente
                            where ID_Campanha=@ID_Campanha and ID_Cliente=@ID_Cliente
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
                    myCommand.Parameters.AddWithValue("@ID_Cliente", id_cli);
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
