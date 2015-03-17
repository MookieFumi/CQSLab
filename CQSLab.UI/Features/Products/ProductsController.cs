using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries.Configuration;
using CQSLab.CrossCutting;
using CQSLab.Services;
using CQSLab.UI.Features.Products.ViewModels;
using CQSLab.UI.Infrastructure.Attributes;

namespace CQSLab.UI.Features.Products
{
    [LogActionFilter]
    [AccessControlFilter(ControllerName = "ProductsController")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        // GET: Products
        public async Task<ViewResult> Index(int page = Constants.DEFAULT_PAGE_INDEX)
        {
            var products = await _productsService.GetProducts(new QueryConfiguration() { Paging = { PageIndex = page } });

            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var viewModel = new ProductVM();

            return View(viewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.CreateMap<ProductVM, Product>();
                    var product = Mapper.Map<ProductVM, Product>(viewModel);
                    _productsService.AddProduct(product);

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

        // GET: Products/Edit/4
        public async Task<ActionResult> Edit(int id, bool success = false)
        {
            if (success)
            {
                ViewBag.Success = true;
            }

            var product = await _productsService.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            Mapper.CreateMap<Product, ProductEditVM>();
            var viewModel = Mapper.Map<Product, ProductEditVM>(product);

            return View(viewModel);
        }

        // POST: Products/Edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductEditVM editVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = await _productsService.GetProduct(editVM.ProductId);

                    Mapper.CreateMap<ProductEditVM, Product>();
                    Mapper.Map(editVM, product);
                    _productsService.UpdateProduct(product);

                    return RedirectToAction("Edit", new { id = editVM.ProductId, success = true });
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