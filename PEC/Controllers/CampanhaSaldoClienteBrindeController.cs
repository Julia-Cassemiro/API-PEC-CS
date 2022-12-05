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
    public class CampanhaSaldoClienteBrindeController : ControllerBase
    {
        private IConfiguration _configuration;

        public CampanhaSaldoClienteBrindeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                      select  C.*, M.DS_ITEM, CL.NM_Guerra from 
		PEC.CampanhaSaldoClienteBrinde as C
                            inner join PEC.maters as M on C.ID_Brinde= M.CD_ITEM 
							inner join PEC.CampanhaSaldoCliente as CSC on C.ID_CampanhaSaldoCliente=CSC.ID
							inner join PEC.CLIENTES as CL on CSC.ID_Cliente= CL.CD_Pessoa


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
                            select  * from PEC.CampanhaSaldoClienteBrinde as C
                            inner join PEC.maters as M on C.ID_Brinde= M.CD_ITEM
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
        public JsonResult Post(CampanhaSaldoClienteBrinde camp)
        {
            string query = @"
                            insert into PEC.CampanhaSaldoClienteBrinde
                            (ID_CampanhaSaldoCliente, ID_Brinde, DT_Brinde, Qtde, Pontos, FL_End_Repres, VL_Unitario)
                            values (@ID_CampanhaSaldoCliente, @ID_Brinde, @DT_Brinde, @Qtde,  @Pontos, @FL_End_Repres, @VL_Unitario)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PEC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID_CampanhaSaldoCliente", camp.ID_CampanhaSaldoCliente);
                    myCommand.Parameters.AddWithValue("@ID_Brinde", camp.ID_Brinde);
                    myCommand.Parameters.AddWithValue("@DT_Brinde", camp.DT_Brinde);
                    myCommand.Parameters.AddWithValue("@Qtde", camp.Qtde);
                    myCommand.Parameters.AddWithValue("@Pontos", camp.Pontos);
                    myCommand.Parameters.AddWithValue("@FL_End_Repres", camp.FL_End_Repres);
                    myCommand.Parameters.AddWithValue("@VL_Unitario", camp.VL_Unitario);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPost("put/{id}")]
        public JsonResult Put(CampanhaSaldoClienteBrinde camp, int id)
        {
            string query = @"
                            update PEC.CampanhaSaldoClienteBrinde
                            set ID_CampanhaSaldoCliente= (@ID_CampanhaSaldoCliente),
                            ID_Brinde= (@ID_Brinde),
                            DT_Brinde= (@DT_Brinde),
                            Qtde= (@Qtde),
                            Pontos= (@Pontos),
                            FL_End_Repres=(@FL_End_Repres),
                            VL_Unitario=(@VL_Unitario)
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
                    myCommand.Parameters.AddWithValue("@ID_CampanhaSaldoCliente", camp.ID_CampanhaSaldoCliente);
                    myCommand.Parameters.AddWithValue("@ID_Brinde", camp.ID_Brinde);
                    myCommand.Parameters.AddWithValue("@DT_Brinde", camp.DT_Brinde);
                    myCommand.Parameters.AddWithValue("@Qtde", camp.Qtde);
                    myCommand.Parameters.AddWithValue("@Pontos", camp.Pontos);
                    myCommand.Parameters.AddWithValue("@FL_End_Repres", camp.FL_End_Repres);
                    myCommand.Parameters.AddWithValue("@VL_Unitario", camp.VL_Unitario);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpPost("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from PEC.CampanhaSaldoClienteBrinde
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
