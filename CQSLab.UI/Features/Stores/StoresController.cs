using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using CQSLab.CrossCutting;
using CQSLab.Entities;
using CQSLab.Entities.Queries.Configuration;
using CQSLab.Services;
using CQSLab.UI.Features.Products.ViewModels;
using CQSLab.UI.Features.Stores.ViewModels;
using CQSLab.UI.Infrastructure.Attributes;

namespace CQSLab.UI.Features.Stores
{
    [LogActionFilter]
    public class StoresController : Controller
    {
        private readonly IStoresService _storesService;

        public StoresController(IStoresService storesService)
        {
            _storesService = storesService;
        }

        // GET: Stores
        public ActionResult Index(int page = Constants.DEFAULT_PAGE_INDEX)
        {
            var stores = _storesService.GetStores(new QueryConfiguration() { Paging = { PageIndex = page } });

            return View(stores);
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            var viewModel = new StoreVM()
            {
                Channels = SelectListHelper.GetFromDictionary(_storesService.GetChannels())
            };

            return View(viewModel);
        }

        // POST: Stores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.CreateMap<StoreVM, Store>();
                    var store = Mapper.Map<StoreVM, Store>(viewModel);
                    _storesService.AddStore(store);

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

            viewModel.Channels = SelectListHelper.GetFromDictionary(_storesService.GetChannels());
            return View(viewModel);
        }

        // GET: Stores/Edit/4
        public ActionResult Edit(int id, bool success = false)
        {
            if (success)
            {
                ViewBag.Success = true;
            }

            var store = _storesService.GetStore(id);
            if (store == null)
            {
                return HttpNotFound();
            }

            Mapper.CreateMap<Store, StoreEditVM>();
            var viewModel = Mapper.Map<Store, StoreEditVM>(store);

            viewModel.Channels = SelectListHelper.GetFromDictionary(_storesService.GetChannels());
            return View(viewModel);
        }

        // POST: Stores/Edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoreEditVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var store = _storesService.GetStore(viewModel.StoreId);

                    Mapper.CreateMap<StoreEditVM, Store>();
                    Mapper.Map(viewModel, store);
                    _storesService.UpdateStore(store);

                    return RedirectToAction("Edit", new { id = viewModel.StoreId, success = true });
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

            viewModel.Channels = SelectListHelper.GetFromDictionary(_storesService.GetChannels());
            return View(viewModel);
        }
    }


}