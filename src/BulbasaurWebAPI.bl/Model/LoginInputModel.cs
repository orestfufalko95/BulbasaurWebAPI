using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BulbasaurWebAPI.bl.Model
{
    public class LoginInputDTO
    {
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("ClientSecret")]
        public string ClientSecret { get; set; }

        [JsonProperty("ClientId")]
        public string ClientId { get; set; }

        [JsonProperty("Scope")]
        public string Scope { get; set; }

        [JsonProperty("ReturnUrl")]
        public string ReturnUrl { get; set; }        
    }
}
