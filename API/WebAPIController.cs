using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DataAccesLibrary;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using Newtonsoft.Json;
using AutoMapper;
using DataAccesLibrary.Models;

namespace Raumklima.API
{
    public class WebAPIController : ControllerBase
    {
        RaumklimaManager raumklimaManager { get; set; }
        private readonly IMapper _mapper;

        public WebAPIController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Route("api/myroute")]
        [HttpGet]
        public IEnumerable<string> SimpleGet(int id)
        {
            return new string[] { "da", "dwa" };
        }

        [Route("api/addMesswert")]
        [HttpPost]
        public string addMesswert([FromBody] dynamic messwert,[FromServices] RaumklimaManager raumklimaManager)
        {
            JsonElement jsonElement = messwert;
            Messwerte messwertModel = new Messwerte();
            messwertModel.Temperatur = (float)jsonElement.GetProperty("Temperatur").GetDouble();
            messwertModel.Feuchte = jsonElement.GetProperty("Feuchte").GetInt32();
            messwertModel.CO2 = jsonElement.GetProperty("CO2").GetInt32();
            messwertModel.SensorID = jsonElement.GetProperty("SensorID").GetInt32();

            raumklimaManager.AddNewMesswert(messwertModel);
            Console.WriteLine("HTTP POST Came In Succesfully. Saving Measurements to DB!");

            return "HTTP POST Came In Succesfully!";
        }

        [HttpPost("api/postTest")]
        public string postTest([FromBody] dynamic messwert, [FromServices] RaumklimaManager raumklimaManager)
        {
            JsonElement messwertJson = messwert;
            return messwertJson.GetProperty("Key").GetString();
        }


        [Route("api/getMesswerte")]
        [HttpGet]
        public ActionResult<List<MesswerteDTO>> getMesswerte([FromServices] RaumklimaManager raumklimaManager)
        {
            var mappedList = 
                _mapper.Map<List<MesswerteDTO>>(raumklimaManager.messwerte);
            return mappedList;
        }

        [Route("api/getLastMesswert")]
        [HttpGet]
        public ActionResult<MesswerteDTO> getLatestMesswerte([FromServices] RaumklimaManager raumklimaManager)
        {
            var mappedList =
                _mapper.Map<MesswerteDTO>(raumklimaManager.messwerte.Last<Messwerte>());
            return mappedList;
        }

        [Route("api/getMesswerte/{id}")]
        [HttpGet]
        public ActionResult<List<MesswerteDTO>> getMesswerteByID(int id,[FromServices] RaumklimaManager raumklimaManager)
        {
            var mappedList =
                _mapper.Map<List<MesswerteDTO>>(raumklimaManager.GetMesswerteByID(id));
            return mappedList;
        }

        [Route("api/getSensoren")]
        [HttpGet]
        public Dictionary<string, MesswerteDTO> getSensoren([FromServices] RaumklimaManager raumklimaManager)
        {
            List<Sensor> listOfSensoren = raumklimaManager.sensoren;
            Dictionary<string, MesswerteDTO> mapOfSensor = new Dictionary<string, MesswerteDTO>();
            foreach (var sensor in listOfSensoren)
            {   
                Messwerte messwerte = 
                    raumklimaManager.GetLastMesswerteBySensorID(sensor.SensorID);
                MesswerteDTO dto = _mapper.Map<MesswerteDTO>(messwerte);
                mapOfSensor.Add(sensor.RaumBezeichnung,dto);
            }
            return mapOfSensor;
        }

    }
}
