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
    public class GrupoClienteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public GrupoClienteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ID_Grupo,ID_Cliente, Status, DT_Criacao from
                            PEC.Grupo_Cliente
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
        public JsonResult GetId(int id)
        {
            string query = @"
                            select ID_Grupo,ID_Cliente, Status, DT_Criacao, NM_RAZAO from  PEC.Grupo_Cliente as GC
	                            inner join siavdf.dbo.clientes as C
	                            on C.CD_PESSOA = GC.ID_Cliente
                                    where ID_Grupo=@ID_Grupo
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Grupo", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(GrupoCliente cli)
        {
            string query = @"
                            insert into PEC.Grupo_Cliente
                            (ID_Grupo,ID_Cliente, Status, DT_Criacao)
                            values (@ID_Grupo,@ID_Cliente, @Status, @DT_Criacao)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Grupo", cli.ID_Grupo);
                    myCommand.Parameters.AddWithValue("@ID_Cliente", cli.ID_Cliente);
                    myCommand.Parameters.AddWithValue("@Status", cli.Status);
                    myCommand.Parameters.AddWithValue("@DT_Criacao", cli.DT_Criacao);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPost("put/{id}")]
        public JsonResult Put(GrupoCliente cli, int id_grupo, int id_cliente)
        {
            string query = @"
                            update PEC.Grupo_Cliente
                            set ID_Grupo= (@ID_Grupo),
                                ID_Cliente= (@ID_Cliente),
                                Status= (@Status),
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
                    myCommand.Parameters.AddWithValue("@ID_Grupo", id_grupo);
                    myCommand.Parameters.AddWithValue("@ID_Cliente", id_cliente);
                    myCommand.Parameters.AddWithValue("@Status", cli.Status);
                    myCommand.Parameters.AddWithValue("@DT_Criacao", cli.DT_Criacao);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpPost("{id}/{id_cliente}")]
        public JsonResult GetId(int id, int id_cliente)
        {
            string query = @"
                            delete from PEC.Grupo_Cliente
                            where ID_Grupo=@ID_Grupo and ID_Cliente=@ID_Cliente
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Grupo", id);
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
