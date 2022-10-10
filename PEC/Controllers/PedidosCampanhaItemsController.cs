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
    public class PedidosCampanhaItemsController : ControllerBase
    {

        private IConfiguration _configuration;

        public PedidosCampanhaItemsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


  //      [HttpGet]
  //      public JsonResult Get()
  //      {
  //          string query = @"
  //    Select P.CD_Status, C.CD_PESSOA, C.NM_GUERRA, Sum(1) as Qt_Pedido,  Sum(IT.Pontos) as Pontos  from PEC.Pedido_Campanha as P
  //      	Inner Join PEC.CLIENTES as C
  //      		on C.CD_PESSOA = P.ID_Cliente
  //      	Inner Join PEC.Pedido_CampanhaItem as IT
		//on IT.ID_Pedido = P.ID_Pedido
  //      Group by P.CD_Status, C.CD_PESSOA, C.NM_GUERRA
  //      order by P.CD_Status, C.NM_GUERRA
  //                          ";

  //          DataTable table = new DataTable();
  //          string sqlDataSource = _configuration.GetConnectionString("PEC");
  //          SqlDataReader myReader;
  //          using (SqlConnection myCon = new SqlConnection(sqlDataSource))
  //          {
  //              myCon.Open();
  //              using (SqlCommand myCommand = new SqlCommand(query, myCon))
  //              {
  //                  myReader = myCommand.ExecuteReader();
  //                  table.Load(myReader);
  //                  myReader.Close();
  //                  myCon.Close();
  //              }
  //          }

  //          return new JsonResult(table);
  //      }



        [HttpGet("execPedidoItems/{id}")]
        public JsonResult Execped(int id)
        {
            string query = @"
                            exec PEC.usp_Retorna_PedidosItens  @ID
                            
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


        [HttpGet("AprovPedido/{id}")]
        public JsonResult AprovPed(int id)
        {
            string query = @"
                           exec PEC.usp_Aprova_Pedidos  @ID_Pedido
                              
                            
                           
                            
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_Pedido", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("ReprovPedido/{id}")]
        public JsonResult ReprovPed(int id)
        {
            string query = @"
                           exec PEC.usp_Reprova_PedidosItem   @ID
                            
                           
                            
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


    }
}
