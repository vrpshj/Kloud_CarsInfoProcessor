using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace codingtest.kloud.com.au
{
    public class CarsService : ICarsService
    {
        private ILogger<CarsController> logger;
        private string carJsonData;
        private List<OwnersCarInventory> ownerCarsInventory;

        public CarsService(ILogger<CarsController> _logger)
        {
            this.logger = _logger;
            try
            {
                carJsonData = File.ReadAllText("cars.json");
                ownerCarsInventory = JsonConvert.DeserializeObject<List<OwnersCarInventory>>(carJsonData);
            }
            catch(Exception e)
            {
                logger.LogError(e, e.Message);
            }
        }
        /// <summary>
        /// Returns the content of cars.json
        /// </summary>
        /// <returns></returns>
        public string GetCarsJson()
        {
            return carJsonData;
        }
        /// <summary>
        /// Returns json for specified owner
        /// </summary>
        /// <param name="Ownername"></param>
        /// <returns>Owner</returns>
        public string GetCarsJsonByOwner(string Ownername)
        {
            OwnersCarInventory ownerInventory = (from owner in ownerCarsInventory where String.Equals(owner.name, Ownername, StringComparison.OrdinalIgnoreCase) select owner).FirstOrDefault();
            string jsonData;
            if (ownerInventory != null)
            {
                jsonData = JsonConvert.SerializeObject(ownerInventory);
                return jsonData;
            }
            return null;
        }
        /// <summary>
        /// Returns owners list for specified car colour 
        /// </summary>
        /// <param name="Colourname"></param>
        /// <returns>Owner name list</returns>
        public string[] GetOwnersByColour(string Colourname)
        {
            List<string> owners = new List<string>();

           var ownerlist = ownerCarsInventory.Where(q => q.cars.Any(a => String.Equals(a.colour, Colourname, StringComparison.OrdinalIgnoreCase) ));
           owners = (from owner in ownerlist select owner.name).ToList();
            return owners.ToArray();
        }

        /// <summary>
        /// Returns owners list for specified car brand 
        /// </summary>
        /// <param name="Brandname"></param>
        /// <returns>Owner name list</returns>
        public string[] GetOwnersByBrand(string Brandname)
        {
            List<string> owners = new List<string>();

            var ownerlist = ownerCarsInventory.Where(q => q.cars.Any(a => String.Equals(a.brand, Brandname, StringComparison.OrdinalIgnoreCase)));
            owners = (from owner in ownerlist select owner.name).ToList();
            return owners.ToArray();
        }

    }
}
