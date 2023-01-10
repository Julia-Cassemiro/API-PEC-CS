    
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;
using PEC.Models;
using System.Data;
using System.Data.SqlClient;

namespace PEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetasRepresClienteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MetasRepresClienteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet("{CD_REPRES}")]
        public JsonResult Get(string CD_REPRES)
        {
            string query = @"
                           select * from pec.VW_Repr_Cliente 
                                where CD_REPRES=@CD_REPRES
                          
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CD_REPRES", CD_REPRES);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        // Grupo de produtos para o metas repesentantes------------------------------------------------
        [HttpGet("View/{ID_Repres}/{ID_Regional}")]
        public JsonResult GetID(int ID_Repres, string ID_Regional)
        {
            string query = @"
                             select * from PEC.vw_Metas_por_RegionalRepres
                                 where ID_Repres=@ID_Repres and ID_Regional=@ID_Regional


                          
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Repres", ID_Repres);
                    myCommand.Parameters.AddWithValue("@ID_Regional", ID_Regional);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        //------------------------------------------------------------------------------------------------

        // Executar procedure de clientes para representantes ------------------------------------------------------------------------------------------
        public JsonResult Post(MetasRepreClientes mpc)
        {
            string query = @"
                             insert into PEC.Metas_Regionais_Repres_Grupo_Cliente values (@ID_Meta_Regional, @ID_Repres, @ID_Grupo_Cliente, @Qtde, @Valor )
                                exec PEC.usp_Cria_Grupo_Produto_Regional_RepresGrupoCliente   @ID_Meta_Regional,  @ID_Repres, @ID_Grupo_Cliente
    
                       
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Meta_Regional", mpc.ID_Meta_Regional);
                    myCommand.Parameters.AddWithValue("@ID_Repres", mpc.ID_Repres);
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Cliente", mpc.ID_Grupo_Cliente);
                    myCommand.Parameters.AddWithValue("@Qtde", mpc.Qtde);
                    myCommand.Parameters.AddWithValue("@Valor", mpc.Valor);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        // visuliazar clientes --------------------------------------------------------------------------------------------------------------------------
        [HttpGet("ViewCli/{ID_Repres}")]
        public JsonResult GetCLI(int ID_Repres, string ID_Regional)
        {
            string query = @"
                             select * from PEC.vw_Metas_por_RegionalRepresGrupoCliente
                                 where ID_Repres=@ID_Repres 


                          
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Repres", ID_Repres);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        } 
        //------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost("{ID_Grupo_Cliente}/{ID_Repres}")]
        public object Delete(int ID_Grupo_Cliente, int ID_Repres)
        {
            var status = true;
            string query = @"     delete PEC.Metas_Regionais_Repres_Grupo_Cliente
                               where ID_Grupo_Cliente=@ID_Grupo_Cliente and ID_Repres=@ID_Repres
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
                        myCommand.Parameters.AddWithValue("@ID_Grupo_Cliente", ID_Grupo_Cliente);
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





        [HttpGet("ViewGPR/{ID_Meta_Regional}/{ID_Repres}/{ID_Grupo_Cliente}")]
        public JsonResult GetGPR(int ID_Meta_Regional, int ID_Repres, int ID_Grupo_Cliente)
        {
            string query = @"
                          select Nome, Qtde, ID_Grupo_Produto,ID_Grupo_Cliente, ID_Meta_Regional, ID_Repres from PEC.vw_Metas_por_RegionalRepresGrupoCliente_GrupoProduto
                                where ID_Meta_Regional=@ID_Meta_Regional and ID_Repres=@ID_Repres and ID_Grupo_Cliente=@ID_Grupo_Cliente


                          
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
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Cliente", ID_Grupo_Cliente);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpPost("UPD/{ID_Meta_Regional}/{ID_Repres}/{ID_Grupo_Produto}/{ID_Grupo_Cliente}")]
        public JsonResult PutGPR(MetasRegionais mr, int ID_Meta_Regional, int ID_Repres, int ID_Grupo_Produto, int ID_Grupo_Cliente)
        {
            string query = @"
                                update  PEC.Metas_Regionais_Repres_Grupo_Cliente_Grupo_Produtos
                                set
                                Qtde= (@Qtde)
                                 where ID_Meta_Regional=@ID_Meta_Regional and ID_Repres=@ID_Repres and ID_Grupo_Produto=@ID_Grupo_Produto and ID_Grupo_Cliente=@ID_Grupo_Cliente
                                 select qtde from   PEC.Metas_Regionais_Repres_Grupo_Cliente_Grupo_Produtos  where ID_Meta_Regional=@ID_Meta_Regional and ID_Repres=@ID_Repres and ID_Grupo_Produto=@ID_Grupo_Produto and ID_Grupo_Cliente=@ID_Grupo_Cliente

                             ";
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Meta_Regional", ID_Meta_Regional);
                    myCommand.Parameters.AddWithValue("@ID_Repres", ID_Repres);
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", ID_Grupo_Produto);
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Cliente", ID_Grupo_Cliente);
                    myCommand.Parameters.AddWithValue("@Qtde", mr.Qtde);
                    myReader = myCommand.ExecuteReader();

                    DataTable table = new DataTable();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(mr.Qtde);
        }

        [HttpPost("DelGPR/{ID_Meta_Regional}/{ID_Repres}/{ID_Grupo_Produto}/{ID_Grupo_Cliente}")]
        public JsonResult DeleteVW(int ID_Meta_Regional, int ID_Grupo_Produto, int ID_Repres, int ID_Grupo_Cliente)
        {

            string query = @"
                               delete from  PEC.Metas_Regionais_Repres_Grupo_Cliente_Grupo_Produtos
                               where ID_Meta_Regional=@ID_Meta_Regional and ID_Repres=@ID_Repres and ID_Grupo_Produto=@ID_Grupo_Produto and ID_Grupo_Cliente=@ID_Grupo_Cliente
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
                    myCommand.Parameters.AddWithValue("@ID_Repres ", ID_Repres);
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Produto", ID_Grupo_Produto);
                    myCommand.Parameters.AddWithValue("@ID_Grupo_Cliente", ID_Grupo_Cliente);
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
