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
    public class GrupoProdutoController : ControllerBase
    { 
            private readonly IConfiguration _configuration;
            public GrupoProdutoController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            [HttpGet]
            public JsonResult Get()
            {
                string query = @"
                            select ID, Nome, Status from
                            PEC.Grupo_Produto
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
                            select ID, Nome, Status from from
                            PEC.Grupo_Produto
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
            public JsonResult Post(GrupoProduto grp)
            {
                string query = @"
                            insert into PEC.Grupo_Produto
                            (Nome, Status)
                            values (@Nome, @Status)
                            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("PEC");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Nome", grp.Nome);
                        myCommand.Parameters.AddWithValue("@Status", grp.Status);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Added Successfully");
            }

            [HttpPut("{id}")]
            public JsonResult Put(GrupoProduto grp, int id)
            {
                string query = @"
                            update PEC.Grupo_Produto
                            set ID= (@ID),                                
                                Nome= (@Nome)
                                Status= (@Status),
                            where ID=@ID
                            ";
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@ID", id);
                    myCommand.Parameters.AddWithValue("@Nome", grp.Nome);
                    myCommand.Parameters.AddWithValue("@Status", grp.Status);
                    myReader = myCommand.ExecuteReader();

                    DataTable table = new DataTable();
                    table.Load(myReader);
                    myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Updated Successfully");
            }

            [HttpDelete("{id}")]
            public JsonResult delete(int id)
            {
                string query = @"
                            delete from PEC.Grupo_Produto
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

                return new JsonResult("Deleted Successfully");
            }
        }
}
