using SistemaViveros.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace SistemaViveros.API.Controllers
{
    
    public class WallsController : ApiController
    {

        private DataContext db = new DataContext();


        
        [HttpGet]
       public IHttpActionResult GetAll()
        {
            var results = from wall in db.Walls
                          select new
                          {
                              wall.Id,
                              wall.Name,
                              wall.Latitude,
                              wall.Longitude,
                              wall.Place,
                              Irrigation = from irrigation in db.Irrigations
                                           where irrigation.WallId == wall.Id
                                           select new
                                           {

                                               irrigation.Id,
                                               irrigation.Day,
                                               Weather = from weather in db.Weathers
                                                         where weather.IrrigationId == irrigation.Id
                                                         select new
                                                         {
                                                             weather.Id,
                                                             weather.Description,
                                                             weather.Humidity,
                                                             weather.Pressure,
                                                             weather.Temperature
                                                         }
                                           }
                          };

           return Json(results);
        }
            
    }
}

