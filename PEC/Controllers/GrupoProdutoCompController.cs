﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PEC.Models;
using System.Data;
using System.Data.SqlClient;

namespace PEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoProdutoCompController : ControllerBase
    {
        private readonly IConfiguration _configuration;


        public GrupoProdutoCompController (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ID_Grupo_Produto,ID_Produto, Composicao, Status  from
                            PEC.Grupo_Produto_Composicao
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
                            select ID_Grupo_Produto,ID_Produto, Composicao, Status from
                            PEC.Grupo_Produto_Composicao
                            where ID_Grupo_Produto=@ID_Grupo_Produto
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(GrupoProdutoComp gpc)
        {
            string query = @"
                            insert into PEC.Grupo_Produto_Composicao
                            (ID_Grupo_Produto,ID_Produto, Composicao, Status)
                            values (@ID_Grupo_Produto,@ID_Produto, @Composicao, @Status)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", gpc.ID_Grupo_Produto);
                    myCommand.Parameters.AddWithValue("@ID_Produto", gpc.ID_Produto);
                    myCommand.Parameters.AddWithValue("@Status", gpc.Status);
                    myCommand.Parameters.AddWithValue("@Composicao", gpc.Composicao);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut("{id}")]
        public JsonResult Put(GrupoProdutoComp gpc, int id, int id_Produto)
        {
            string query = @"
                            update PEC.Grupo_Produto_Composicao
                            set ID_Grupo_Produto= (@ID_Grupo_Produto),
                                ID_Produto= (@ID_Produto),
                                Status= (@Status),
                                Composicao= (@Composicao)
                            where ID_Grupo_Produto=@ID_Grupo_Produto
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", id);
                    myCommand.Parameters.AddWithValue("@ID_Produto", id_Produto);
                    myCommand.Parameters.AddWithValue("@Status", gpc.Status);
                    myCommand.Parameters.AddWithValue("@Composicao", gpc.Composicao);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}/{id_produto}")]
        public JsonResult GetId(int id, int id_produto)
        {
            string query = @"
                            delete from PEC.Grupo_Produto_Composicao
                            where ID_Grupo_Produto=@ID_Grupo_Produto and ID_Produto=@ID_Produto
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", id);
                    myCommand.Parameters.AddWithValue("@ID_Produto", id_produto);
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
