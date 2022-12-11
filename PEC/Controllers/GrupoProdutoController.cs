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
                            select ID, Nome from
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

        [HttpGet("execReg/{nm}")]
        public JsonResult ExecGPR(string nm)
        {
            string query = @"
                            exec PEC.usp_ListaGPRegionais  @Nome
                            
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Nome", nm);
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
                            select ID, Nome from
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
                            (Nome)
                            values (@Nome)
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
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Added Successfully");
            }

            [HttpPost("put/{id}")]
            public JsonResult Put(GrupoProduto grp, int id)
            {
                string query = @"
                                update PEC.Grupo_Produto
                                set
                                Nome= (@Nome)
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
                    myReader = myCommand.ExecuteReader();

                    DataTable table = new DataTable();
                    table.Load(myReader);
                    myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Updated Successfully");
            }

            [HttpPost("{id}")]
            public object delete(int id)
            {
            var status = true;
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
                try
                {

                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@ID", id);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message.ToString();
                    status = false;
                    return (error, status);
                }
            }

            return (status);
        }
        }
}
