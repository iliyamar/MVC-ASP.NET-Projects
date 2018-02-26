namespace CarDealer.Services.Models.Cars
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CarWithPartsModel :CarModel
    {
        public IEnumerable<PartModel> Parts { get; set; }
    }
}
