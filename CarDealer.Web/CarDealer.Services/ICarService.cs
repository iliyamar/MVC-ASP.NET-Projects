namespace CarDealer.Services
{
    using CarDealer.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models.Cars;

    public interface ICarService
    {
        IEnumerable<CarModel> ByMake(string make);

        IEnumerable<CarWithPartsModel> WithParts();
    }
}
