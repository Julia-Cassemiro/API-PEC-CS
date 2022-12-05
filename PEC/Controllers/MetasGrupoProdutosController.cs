using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PEC.Models;
using System;
using System.Data;
using System.Data.SqlClient;


namespace PEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetasGrupoProdutosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MetasGrupoProdutosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select * from Pec.vw_Produtos_por_Meta
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
                            select * from Pec.vw_Produtos_por_Meta
                            where ID_Metas=@ID_Metas
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Metas", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(MetasGrupoProdutos mg)
        {
            string query = @"
                            insert into PEC.Metas_Grupo_Produtos values (@ID_Metas, @ID_Grupo_Produto, @Qtde, @Valor )
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Metas", mg.ID_Metas);
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", mg.ID_Grupo_Produto);
                    myCommand.Parameters.AddWithValue("@Qtde", mg.Qtde);
                    myCommand.Parameters.AddWithValue("@Valor", mg.Valor);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPost("put/{id}")]
        public JsonResult Put(Grupo gru, int id)
        {
            string query = @"
                            update PEC.Grupo
                            set 
                                ID_Class_Pec= (@ID_Class_Pec),
                                DS_Grupo= (@DS_Grupo),
                                Status= (@Status)
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
                    myCommand.Parameters.AddWithValue("@ID_Class_Pec", gru.ID_Class_Pec);
                    myCommand.Parameters.AddWithValue("@DS_Grupo", gru.DS_Grupo);
                    myCommand.Parameters.AddWithValue("@Status", gru.Status);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpPost("{ID_Grupo_Produto}/{ID_Metas}")]
        public object Delete(int ID_Grupo_Produto, int ID_Metas)
        {
            var status = true;
            string query = @"
                            delete from PEC.Metas_Grupo_Produtos
                            where ID_Grupo_Produto=@ID_Grupo_Produto and ID_Metas=@ID_Metas
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                try
                {
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", ID_Grupo_Produto);
                        myCommand.Parameters.AddWithValue("@ID_Metas", ID_Metas);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message.ToString() ;
                    status = false;
                    return (error, status);
                }
            }

            return (status);
        }
    }
}
