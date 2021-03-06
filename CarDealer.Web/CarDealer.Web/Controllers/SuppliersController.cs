﻿
namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Suppliers;
    using Services;

    public class SuppliersController : Controller
    {
        private const string SuppliersView="Suppliers";
        private readonly ISupplierService suppliers;

        public SuppliersController(ISupplierService suppliers)
        {
            this.suppliers = suppliers;
        }

        public IActionResult Local()
        => this.View(SuppliersView, this.GetSupplierModel(false));


        public IActionResult Importers()
        => this.View(SuppliersView, this.GetSupplierModel(true));
        

        private SuppliersModel GetSupplierModel (bool importers)
        {
            var type = importers ? "Imporer" : "Local";
            var suppliers = this.suppliers.All(importers);

            return new SuppliersModel
            {
                Type = type,
                Suppliers = suppliers
            };

        }
    }
}