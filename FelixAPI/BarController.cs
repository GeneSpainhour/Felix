using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Felix.Interfaces;
using Felix.Library.BLL;
using FelixAPI.Models;

namespace FelixAPI
{
    public partial class BarController: ApiController
    {
        private IBarDomainObject BarDomainObject { get; set; }

        public BarController()
        {
            BarDomainObject = new BarDomainObject();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] BarCreationRequest request)
        {
            int barId = BarDomainObject.Save(request.Symbol, (IBar)request.Bar).Result;

            return Ok(barId);
        }
    }
}
