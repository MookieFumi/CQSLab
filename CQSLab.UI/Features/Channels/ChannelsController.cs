using System;
using System.Web.Mvc;
using AutoMapper;
using CQSLab.CrossCutting;
using CQSLab.Entities;
using CQSLab.Entities.Queries.Configuration;
using CQSLab.Services;
using CQSLab.UI.Features.Channels.ViewModels;
using CQSLab.UI.Infrastructure.Attributes;

namespace CQSLab.UI.Features.Channels
{
    [Authorize]
    [LogActionFilter]
    public class ChannelsController : Controller
    {
        private readonly IChannelsService _channelsService;

        public ChannelsController(IChannelsService channelsService)
        {
            _channelsService = channelsService;
        }

        // GET: Channels
        public ActionResult Index(int page = Constants.DEFAULT_PAGE_INDEX)
        {
            var channels = _channelsService.GetChannels(new QueryConfiguration() { Paging = { PageIndex = page } });

            return View(channels);
        }

        // GET: Channels/Create
        public ActionResult Create()
        {
            var viewModel = new ChannelVM();

            return View(viewModel);
        }

        // POST: Channels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChannelVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.CreateMap<ChannelVM, Channel>();
                    var channel = Mapper.Map<ChannelVM, Channel>(viewModel);
                    _channelsService.AddChannel(channel);

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

        // GET: Channels/Edit/4
        public ActionResult Edit(int id, bool success = false)
        {
            if (success)
            {
                ViewBag.Success = true;
            }

            var channel = _channelsService.GetChannel(id);
            if (channel == null)
            {
                return HttpNotFound();
            }

            Mapper.CreateMap<Channel, ChannelEditVM>();
            var viewModel = Mapper.Map<Channel, ChannelEditVM>(channel);

            viewModel.Budgets = _channelsService.GetBudgets(viewModel.ChannelId);
            return View(viewModel);
        }

        // POST: Channels/Edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChannelEditVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var channel = _channelsService.GetChannel(viewModel.ChannelId);

                    Mapper.CreateMap<ChannelEditVM, Channel>();
                    Mapper.Map(viewModel, channel);
                    _channelsService.UpdateChannel(channel);

                    return RedirectToAction("Edit", new { id = viewModel.ChannelId, success = true });
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

        // GET: Channels/Budget/4?
        public ActionResult Budget(int id, int accountantPeriod, bool success = false)
        {
            if (success)
            {
                ViewBag.Success = true;
            }

            var budget = _channelsService.GetBudget(id, accountantPeriod);
            if (budget == null)
            {
                return HttpNotFound();
            }

            Mapper.CreateMap<BudgetChannel, BudgetEditVM>();
            var viewModel = Mapper.Map<BudgetChannel, BudgetEditVM>(budget);

            return View(viewModel);
        }

        // POST: Channels/Budget/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Budget(BudgetEditVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var budget = _channelsService.GetBudget(viewModel.ChannelId, viewModel.AccountantPeriod);

                    Mapper.CreateMap<BudgetEditVM, BudgetChannel>();
                    Mapper.Map(viewModel, budget);
                    _channelsService.UpdateBudget(budget);

                    return RedirectToAction("Budget", new { id = viewModel.ChannelId, viewModel.AccountantPeriod, success = true });
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