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
    public class RepresController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RepresController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("ListaRepre/{nm}")]
        public JsonResult ExecGPR(string nm)
        {
            string query = @"
                            exec PEC.usp_Lista_Repre  @Nome
                            
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

        [HttpPost]
        public JsonResult Post(Repres R)
        {
            string query = @"
                            insert into pec.REPREG
                            values (@ID_REPRES, @CD_REGIONAL)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_REPRES", R.ID_REPRES);
                    myCommand.Parameters.AddWithValue("@CD_REGIONAL", R.CD_REGIONAL);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpGet("{nm}")]
        public JsonResult Get(string nm)
        {
            string query = @"
                           select CD_PESSOA, 
		                               NM_GUERRA,
		                               NM_RAZAO,
		                               R.CD_REGIONAL,
		                               R.ID_REPRES
                            from pec.REPREG as R
                            inner join SIAVDF.dbo.CLIENTES as C
                                on R.ID_REPRES = C.CD_PESSOA

                                     WHERE CD_REGIONAL = @Nome
                            
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

        [HttpPost("{ID_REPRES}/{CD_REGIONAL}")]
        public JsonResult Delete( string CD_REGIONAL, string ID_REPRES)
        {
            string query = @"
                            DELETE pec.REPREG WHERE 
                                ID_REPRES = @ID_REPRES AND  CD_REGIONAL = @CD_REGIONAL


                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CD_REGIONAL", CD_REGIONAL);
                    myCommand.Parameters.AddWithValue("@ID_REPRES", ID_REPRES);
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
