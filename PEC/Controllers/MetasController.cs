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
    public class MetasController : ControllerBase
    {
        private IConfiguration _configuration;

        public MetasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ID, DS_Meta, DT_Inicial, DT_Final, Qtde, Valor, Seq from
                            PEC.Metas
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
                            select ID, DS_Meta, DT_Inicial, DT_Final, Qtde, Valor, Seq from
                            PEC.Metas
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
        public JsonResult Post(Metas Met)
        {
            string query = @"
                            insert into PEC.Metas
                            (DS_Meta, DT_Inicial, DT_Final, Qtde, Valor, Seq)
                            values (@DS_Meta, @DT_Inicial, @DT_Final, @Qtde, @Valor, @Seq)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DS_Meta", Met.DS_Meta);
                    myCommand.Parameters.AddWithValue("@DT_Inicial", Met.DT_Inicial);
                    myCommand.Parameters.AddWithValue("@DT_Final", Met.DT_Final);
                    myCommand.Parameters.AddWithValue("@Qtde", Met.Qtde);
                    myCommand.Parameters.AddWithValue("@Valor", Met.Valor);
                    myCommand.Parameters.AddWithValue("@Seq", Met.Seq);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut("{id}")]
        public JsonResult Put(Metas Met, int id)
        {
            string query = @"
                            update PEC.Metas
                            set DS_Meta= (@DS_Meta),
                                DT_Inicial= (@DT_Inicial),
                                DT_Final= (@DT_Final),
                                Qtde= (@Qtde),
                                Valor= (@Valor),
                                Seq= (@Seq)
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
                    myCommand.Parameters.AddWithValue("@DS_Meta", Met.DS_Meta);
                    myCommand.Parameters.AddWithValue("@DT_Inicial", Met.DT_Inicial);
                    myCommand.Parameters.AddWithValue("@DT_Final", Met.DT_Final);
                    myCommand.Parameters.AddWithValue("@Qtde", Met.Qtde);
                    myCommand.Parameters.AddWithValue("@Valor", Met.Valor);
                    myCommand.Parameters.AddWithValue("@Seq", Met.Seq);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from PEC.Metas
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