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
    public class BrindesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public BrindesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                           Select B.ID_Produto, M.DS_ITEM, B.Pontos 
                           from PEC.Brindes as B
                           Inner Join PEC.MATERS as M
	                                  on M.CD_ITEM = B.ID_Produto
                           Order by M.DS_ITEM";

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
                            select ID_Produto, Pontos from
                            PEC.Brindes
                            where ID_Produto=@ID_Produto
                            ";

        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("PEC");
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@ID_Produto", id);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
        }

        return new JsonResult(table);
    }

    [HttpPost]
    public JsonResult Post(Brindes brin)
    {
        string query = @"
                            insert into PEC.Brindes
                            (ID_Produto, Pontos)
                            values (@ID_Produto, @Pontos)
                            ";

        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("PEC");
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@ID_Produto", brin.ID_Produto);
                    myCommand.Parameters.AddWithValue("@Pontos", brin.Pontos);
                    myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
        }

        return new JsonResult("Added Successfully");
    }

    [HttpPut("{id}")]
    public JsonResult PutId(Brindes brin, int id)
    {
        string query = @"
                            update PEC.Brindes
                            set
                               Pontos= (@Pontos)
                            where ID_Produto=@ID_Produto
                            ";

        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("PEC");
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
               
                myCommand.Parameters.AddWithValue("@ID_Produto", id);
                    myCommand.Parameters.AddWithValue("@Pontos", brin.Pontos);
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
                            delete from PEC.Brindes
                            where ID_Produto=@ID_Produto
                            ";

        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("PEC");
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@ID_Produto", id);
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
