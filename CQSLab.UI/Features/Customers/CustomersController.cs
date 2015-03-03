using System;
using System.Web.Mvc;
using AutoMapper;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries.Configuration;
using CQSLab.CrossCutting;
using CQSLab.Services;
using CQSLab.UI.Features.Customers.ViewModels;
using CQSLab.UI.Infrastructure.Attributes;

namespace CQSLab.UI.Features.Customers
{
    [LogActionFilter]
    public class CustomersController : Controller
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        // GET: Customers
        public ActionResult Index(int page = Constants.DEFAULT_PAGE_INDEX)
        {
            var customers = _customersService.GetCustomers(new QueryConfiguration() { Paging = { PageIndex = page } });

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
                    Mapper.CreateMap<CustomerVM, Customer>();
                    var customer = Mapper.Map<CustomerVM, Customer>(viewModel);
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
        public ActionResult Edit(int id, bool success = false)
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

            Mapper.CreateMap<Customer, CustomerEditVM>();
            var viewModel = Mapper.Map<Customer, CustomerEditVM>(customer);

            return View(viewModel);
        }

        // POST: Customers/Edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerEditVM editVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = _customersService.GetCustomer(editVM.CustomerId);

                    Mapper.CreateMap<CustomerEditVM, Customer>();
                    Mapper.Map(editVM, customer);
                    _customersService.UpdateCustomer(customer);

                    return RedirectToAction("Edit", new { id = editVM.CustomerId, success = true });
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

            return View(editVM);
        }
    }


}