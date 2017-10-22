using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtWeb.Controllers
{
    [Route("api/[controller]")]
    public class Token2Controller : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var symmetricKeyBytes = Base64UrlEncoder.DecodeBytes(Startup.Getkey);
             SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(symmetricKeyBytes);
             SigningCredentials signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            JwtHeader head = new JwtHeader(signingCredentials);
            string iis = "gh_iss";
            string aud = "zhaojinhai";
            List<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>();
            claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "admin"));
            claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "zhaojinhai"));
            JwtPayload playload = new JwtPayload(iis,aud, claims, DateTime.UtcNow,DateTime.UtcNow.AddDays(1));
            JwtSecurityToken token = new JwtSecurityToken(head, playload);
            string jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
            
            return new string[] { jwttoken, Startup.Getkey };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
