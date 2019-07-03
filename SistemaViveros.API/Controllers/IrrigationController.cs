using Newtonsoft.Json;
using SistemaViveros.API.Models;
using SistemaViveros.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SistemaViveros.API.Controllers
{
    [Route("api/irrigation")]
    public class IrrigationController : ApiController
    {
        private BackendDbContext db = new BackendDbContext();


        [HttpGet]
        public IQueryable<Irrigation> GetIrrigation()
        {
            return db.Irrigations;
        }

        [HttpGet]
        [Route("api/irrigation/{id}")]
        public IQueryable<Irrigation> GetIrrigation(int id)
        {
            
            return db.Irrigations.Where(d => d.WallId.Equals(id));
            
        }

        [HttpGet]
        [Route("api/irrigationWeather")]
        [ResponseType(typeof(List<IrrigationWeather>))]
        public HttpResponseMessage GetIrrigationWeather()
        {
            var query = "Select i.Day, i.WallId, w.Description, w.Humidity,w.Icon, w.IrrigationId, w.Pressure,w.Temperature, w.WeatherId from dbo.Irrigations i left join dbo.Weathers w on i.IrrigationId = w.IrrigationId";
            List<IrrigationWeather> result = db.Database.SqlQuery<IrrigationWeather>(query).ToList();
            var jsonResult = JsonConvert.SerializeObject(result);
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new System.Net.Http.StringContent(jsonResult, Encoding.UTF8);
            return response;
        }


        [HttpGet]
        [Route("api/irrigationWeather/{id}")]
        [ResponseType(typeof(List<IrrigationWeather>))]
        public HttpResponseMessage GetIrrigationWeather(int id)
        {
            var query = $"Select i.Day, i.WallId, w.Description, w.Humidity,w.Icon, w.IrrigationId, w.Pressure,w.Temperature, w.WeatherId from dbo.Irrigations i left join dbo.Weathers w on i.IrrigationId = w.IrrigationId where i.WallId = {id}";
            List<IrrigationWeather> result = db.Database.SqlQuery<IrrigationWeather>(query).ToList();
            var jsonResult = JsonConvert.SerializeObject(result);
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new System.Net.Http.StringContent(jsonResult, Encoding.UTF8);
            return response;
        }







        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IrrigationExists(int id)
        {
            return db.Irrigations.Count(e => e.IrrigationId == id) > 0;
        }




    }
}
