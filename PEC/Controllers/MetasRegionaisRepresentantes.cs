using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PEC.Models;
using System.Data;
using System.Data.SqlClient;

namespace PEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetasRegionaisRepresentantes : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MetasRegionaisRepresentantes(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet("{CD_REGIONAL}")]
        public JsonResult Get(string CD_REGIONAL)
        {
            string query = @"
                            select * from PEC.vw_Lista_Regional_Repres 
                                where CD_REGIONAL=@CD_REGIONAL
                          
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
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("View/{ID_Regional}/{ID_Metas_Regional}")]
        public JsonResult GetID(string ID_Regional, int ID_Metas_Regional)
        {
            string query = @"
                             select * from PEC.vw_Metas_por_RegionalRepres
                                 where ID_Regional=@ID_Regional and ID_Metas_Regional=@ID_Metas_Regional


                          
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
                    myCommand.Parameters.AddWithValue("@ID_Metas_Regional", ID_Metas_Regional);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        public JsonResult Post(MetasRegionaisRepre mrp)
        {
            string query = @"
                            insert into PEC.Metas_Regionais_Repres  values (@ID_Meta_Regional, @ID_Repres, @Qtde, @Valor )
                       
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Meta_Regional", mrp.ID_Meta_Regional);
                    myCommand.Parameters.AddWithValue("@ID_Repres", mrp.ID_Repres);
                    myCommand.Parameters.AddWithValue("@Qtde", mrp.Qtde);
                    myCommand.Parameters.AddWithValue("@Valor", mrp.Valor);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost("View/{ID_Meta_Regional}/{ID_Grupo_Produto}")]
        public JsonResult Put(MetasRegionais mr, int ID_Meta_Regional, int ID_Grupo_Produto)
        {
            string query = @"
                                update PEC.Metas_Regionais_Grupo_Produtos
                                set
                                Qtde= (@Qtde)
                                where ID_Meta_Regional=@ID_Meta_Regional and ID_Grupo_Produto=@ID_Grupo_Produto
                             ";
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Meta_Regional", ID_Meta_Regional);
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", ID_Grupo_Produto);
                    myCommand.Parameters.AddWithValue("@Qtde", mr.Qtde);
                    myReader = myCommand.ExecuteReader();

                    DataTable table = new DataTable();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpPost("{ID_Meta_Regional}/{ID_Repres}")]
        public object Delete(int ID_Meta_Regional, int ID_Repres)
        {
            var status = true;
            string query = @"     delete from PEC.Metas_Regionais_Repres
                               where ID_Meta_Regional=@ID_Meta_Regional and ID_Repres=@ID_Repres
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
                        myCommand.Parameters.AddWithValue("@ID_Meta_Regional", ID_Meta_Regional);
                        myCommand.Parameters.AddWithValue("@ID_Repres", ID_Repres);
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



        //executar procedure de criar grupos de produtos em Representantes

        [HttpGet("CriaGPRepres/{ID_Meta_Regional}/{ID_Regional}")]
        public JsonResult ExecPost(int ID_Meta_Regional, int ID_Repres)
        {
            string query = @"
                            exec PEC.usp_Cria_Grupo_Produto_Regional_Repres @ID_Meta_Regional,ID_Repres
                                                              
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Meta_Regional", ID_Meta_Regional);
                    myCommand.Parameters.AddWithValue("@ID_Repres", ID_Repres);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpGet("ViewGPR/{ID_Meta_Regional}/{ID_Repres}")]
        public JsonResult GetGPR(int ID_Meta_Regional, int ID_Repres)
        {
            string query = @"
                            select * from PEC.vw_Metas_por_RegionalRepres_GrupoProduto
                                 where ID_Meta_Regional=@ID_Meta_Regional and ID_Repres=@ID_Repres


                          
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Meta_Regional", ID_Meta_Regional);
                    myCommand.Parameters.AddWithValue("@ID_Repres", ID_Repres);
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
