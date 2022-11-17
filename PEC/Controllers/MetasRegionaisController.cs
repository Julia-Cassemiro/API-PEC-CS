﻿using Microsoft.AspNetCore.Http;
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
    public class MetasRegionaisController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MetasRegionaisController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet("NomeRegionais")]
        public JsonResult Get()
        {
            string query = @"
                            select * from PEC.VW_Regionais
                          
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


        // regionais por metas 
        [HttpGet]
        public JsonResult GetR()
        {
            string query = @"
                           select * from PEC.VW_Regionais_por_Metas
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
                            select * from PEC.VW_Regionais_por_Metas
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
        //****************Aqui é onde eu estou pegando a view de Produtos*********************
        [HttpGet("RegionaisMes")]
        public JsonResult GetRM()
        {
            string query = @"
                           select * from PEC.vw_Metas_por_RegionalMes

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

        [HttpGet("RegionaisMes/{ID_Regional}/{ID_Metas}")]
        public JsonResult GetPMID(string ID_Regional, int ID_Metas)
        {
            string query = @"
                           select * from PEC.vw_Metas_por_RegionalMes
                            where ID_Regional=@ID_Regional and ID_Metas = @ID_Metas
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Regional", ID_Regional);
                    myCommand.Parameters.AddWithValue("@ID_Metas", ID_Metas);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }//FINAL PRODUTOS***********************************

        public JsonResult Post(MetasRegionais mr)
        {
            string query = @"
                            insert into PEC.Metas_Regionais values (@ID_Metas, @ID_Regional, @Qtde, @Valor )
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Metas", mr.ID_Metas);
                    myCommand.Parameters.AddWithValue("@ID_Regional", mr.ID_Regional);
                    myCommand.Parameters.AddWithValue("@Qtde", mr.Qtde);
                    myCommand.Parameters.AddWithValue("@Valor", mr.Valor);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }



    [HttpDelete("{ID_Regional}/{ID_Metas}")]
    public object Delete(string ID_Regional, int ID_Metas)
    {
        var status = true;
        string query = @"
                               delete from PEC.Metas_Regionais
                               where ID_Regional=@ID_Regional and ID_Metas=@ID_Metas
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
                    myCommand.Parameters.AddWithValue("@ID_Regional", ID_Regional);
                    myCommand.Parameters.AddWithValue("@ID_Metas", ID_Metas);
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
