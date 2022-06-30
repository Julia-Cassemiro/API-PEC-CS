using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
                            select ID_Produto, Pontos from
                            PEC.Brindes
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
    }
}
