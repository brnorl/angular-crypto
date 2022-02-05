using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly IMapper mapper;

        public CurrencyController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet]
        public List<Btc> GetBtcs()
        {
            using (var service = new CryptoContext())
            {
                var btcList = service.Btcs.ToList();
                return btcList;
            }
        }
    }
}
