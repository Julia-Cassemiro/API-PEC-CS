using Newtonsoft.Json;


namespace API_Clinica.Model
{
    // R E Q U E S T    C L A S
    public class Credentials_Request
    {

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("clienteId")]
        public int ClienteId { get; set; }
        [JsonProperty("text")]

        public string Text { get; set; }
        public Contact Contact { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }

        public string origin { get; set; }


    }

    public class Contact
    {
        [JsonProperty("uid")]
        public int Uid { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        public Fields Fields { get; set; }
    }

    public class Fields
    {
        [JsonProperty("cpf")]
        public string Cpf { get; set; }
        [JsonProperty("celular")]

        public string Celular { get; set; }

        [JsonProperty("dataDeNascimento")]
        public string DataDeNascimento { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

    }

    public class Data
    {
        [JsonProperty("example")]
        public string example { get; set; }
        public string id_especialidade { get; set; }
        public string id_medico { get; set; }

    }


}
