using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries.Configuration;
using CQSLab.CrossCutting;
using CQSLab.Business;
using CQSLab.Services;
using CQSLab.UI.Features.Products.ViewModels;
using CQSLab.UI.Features.Stores.ViewModels;
using CQSLab.UI.Infrastructure.Attributes;

namespace CQSLab.UI.Features.Stores
{
    [LogActionFilter]
    [Authorize]
    [AccessControlFilter(ControllerName = "StoresController")]
    public class StoresController : Controller
    {
        private readonly IStoresService _storesService;

        public StoresController(IStoresService storesService)
        {
            _storesService = storesService;
        }

        // GET: Stores
        public async Task<ViewResult> Index(int page = Constants.DEFAULT_PAGE_INDEX)
        {
            var stores = await _storesService.GetStores(new QueryConfiguration() { Paging = { PageIndex = page } });

            return View(stores);
        }

        // GET: Stores/Create
        public async Task<ViewResult> Create()
        {
            var channels = await _storesService.GetChannels();
            var viewModel = new StoreVM()
            {
                Channels = SelectListHelper.GetFromDictionary(channels)
            };

            return View(viewModel);
        }

        // POST: Stores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StoreVM viewModel)
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

            var channels = await _storesService.GetChannels();
            viewModel.Channels = SelectListHelper.GetFromDictionary(channels);
            return View(viewModel);
        }

        // GET: Stores/Edit/4
        public async Task<ActionResult> Edit(int id, bool success = false)
        {
            if (success)
            {
                ViewBag.Success = true;
            }

            var store = await _storesService.GetStore(id);
            if (store == null)
            {
                return HttpNotFound();
            }

            Mapper.CreateMap<Store, StoreEditVM>();
            var viewModel = Mapper.Map<Store, StoreEditVM>(store);

            var channels = await _storesService.GetChannels();
            viewModel.Channels = SelectListHelper.GetFromDictionary(channels);
            return View(viewModel);
        }

        // POST: Stores/Edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StoreEditVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var store = await _storesService.GetStore(viewModel.StoreId);

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

            var channels = await _storesService.GetChannels();
            viewModel.Channels = SelectListHelper.GetFromDictionary(channels);
            return View(viewModel);
        }
    }


}