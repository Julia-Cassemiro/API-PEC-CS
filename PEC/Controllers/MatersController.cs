﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MatersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("execMaters/{nm}")]
        public JsonResult Get(string nm)
        {
            string query = @"
                           exec pec.usp_ListaMaters @DS_ITEM
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DS_ITEM", nm);
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
                             select CD_ITEM, DS_ITEM, UN from
                            siavdf.dbo.maters
                            where CD_ITEM=@CD_ITEM
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CD_ITEM", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

}
}
