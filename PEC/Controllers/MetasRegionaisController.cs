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
        //Regionais Por Metas 
        [HttpGet("regPM/{ID_Regional}")]
        public JsonResult GetRPM(string ID_Regional)
        {
            string query = @"
                            select * from PEC.VW_Regionais_por_Metas 
                            where ID_Regional =@ID_Regional
                          
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
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("ViewRGPM/{ID_Regional}/{ID_Meta_Regional}")]
        public JsonResult RGPMGet(string ID_Regional, int ID_Meta_Regional)
        {
            string query = @"
                             select * from pec.vw_Metas_por_RegionalGrupoProduto
                                 where ID_Regional=@ID_Regional and ID_Meta_Regional=@ID_Meta_Regional


                          
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
                    myCommand.Parameters.AddWithValue("@ID_Regional", ID_Regional);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //fim


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

            return new JsonResult(table);
        }


        //executar procedure de criar grupos de produtos em regionais

        [HttpGet("CriarGP/{ID_Metas}/{ID_Regional}")]
        public JsonResult ExecPost(int ID_Metas, string ID_Regional)
        {
            string query = @"
                             Exec PEC.usp_Cria_Grupo_Produto_Regional    @ID_Metas,  @ID_Regional
                                                              
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Metas", ID_Metas);
                    myCommand.Parameters.AddWithValue("@ID_Regional", ID_Regional);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        // VIEW MetasregionaisGP

        [HttpPost("View/{ID_Meta_Regional}/{ID_Grupo_Produto}")]
        public JsonResult Put(MetasRegionais mr, int ID_Meta_Regional, int ID_Grupo_Produto)
        {
            string query = @"
                                update  PEC.Metas_Regionais_Grupo_Produtos
                                set  Qtde= (@Qtde)
                                where ID_Meta_Regional=@ID_Meta_Regional and ID_Grupo_Produto=@ID_Grupo_Produto

                                 select qtde from   PEC.Metas_Regionais_Grupo_Produtos  where  ID_Meta_Regional=@ID_Meta_Regional and ID_Grupo_Produto=@ID_Grupo_Produto
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
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", ID_Grupo_Produto);
                    myCommand.Parameters.AddWithValue("@Qtde", mr.Qtde);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(mr.Qtde );
        }

        [HttpGet("ViewGPR/{ID_Regional}/{ID_Metas}")]
        public JsonResult MGPGet(string ID_Regional, int ID_Metas)
        {
            string query = @"
                             select * from pec.vw_Metas_por_RegionalGrupoProduto
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
                    myCommand.Parameters.AddWithValue("@ID_Metas", ID_Metas);
                    myCommand.Parameters.AddWithValue("@ID_Regional", ID_Regional);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpPost("ViewGPR/{ID_Meta_Regional}/{ID_Grupo_Produto}")]
        public JsonResult DeleteVW(int ID_Meta_Regional, int ID_Grupo_Produto)
        {

            string query = @"
                               delete from PEC.Metas_Regionais_Grupo_Produtos
                               where ID_Meta_Regional=@ID_Meta_Regional and ID_Grupo_Produto=@ID_Grupo_Produto
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
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", ID_Grupo_Produto);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult("Deleted Successfully");
        }

        [HttpPost("insertGP")]
        public JsonResult PostGP(MetasRegionais mr)
        {

            string query = @"
                                insert into PEC.Metas_Regionais_Grupo_Produtos values (@ID_Meta_Regional, @ID_Grupo_Produto, @Qtde, @Valor )
                          
                            
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Meta_Regional", mr.ID_Meta_Regional);
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", mr.ID_Grupo_Produto);
                    myCommand.Parameters.AddWithValue("@Qtde", mr.Qtde);
                    myCommand.Parameters.AddWithValue("@Valor", mr.Valor);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }



        [HttpPost("{ID_Regional}/{ID_Metas}")]
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
