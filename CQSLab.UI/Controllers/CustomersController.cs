using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using CQS.Entities;
using CQSLab.Services;
using CQSLab.UI.ViewModels;

namespace CQSLab.UI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _customersService.GetCustomers();

            return View(customers);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var viewModel = new CustomerVM();

            return View(viewModel);
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AutoMapper.Mapper.CreateMap<CustomerVM, Customer>();
                    var customer = AutoMapper.Mapper.Map<CustomerVM, Customer>(viewModel);
                    _customersService.AddCustomer(customer);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ViewBag.Error = exception.Message;
                }
            }
            else
            {
                ViewBag.Error = Helper.GetErrorsFromModelState(ModelState);
            }

            return View(viewModel);
        }

        // GET: Customers/Edit/4
        public ActionResult Edit(int id, bool success=false)
        {
            if (success)
            {
                ViewBag.Success = true;
            }

            var customer = _customersService.GetCustomer(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            AutoMapper.Mapper.CreateMap<Customer, CustomerEditVM>();
            var viewModel = AutoMapper.Mapper.Map<Customer, CustomerEditVM>(customer);

            return View(viewModel);
        }

        // POST: Customers/Edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerEditVM viewModel)
        {
            ModelStateDictionary a = ModelState;
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = _customersService.GetCustomer(viewModel.CustomerId);

                    AutoMapper.Mapper.CreateMap<CustomerEditVM, Customer>();
                    Mapper.Map(viewModel, customer);
                    _customersService.UpdateCustomer(customer);

                    return RedirectToAction("Edit", new { id = viewModel.CustomerId, success=true });
                }
                catch (Exception exception)
                {
                    ViewBag.Error = exception.Message;
                }
            }
            else
            {
                ViewBag.Error = Helper.GetErrorsFromModelState(ModelState);
            }

            return View(viewModel);
        }
    }

   
}