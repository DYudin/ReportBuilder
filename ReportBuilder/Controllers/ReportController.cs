using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
//using System.Web.Mvc;
using AutoMapper;
using ReportBuilder.Infrastructure.Services.Abstract;
using ReportBuilder.Models;
using ReportBuilder.Services.UnitOfWork.Abstract;
using ReportBuilder.ViewModel;
using ReportBuilder.ViewModels;

namespace ReportBuilder.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IReportBuilder _reportBuilder;
        private readonly IEmailService _emailService;
     
        public ReportController()
        {
        }

        public ReportController(
            IUnitOfWorkFactory unitOfWorkFactory,
            IReportBuilder reportBuilder,
            IEmailService emailService)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException("unitOfWorkFactory is null");
            if (reportBuilder == null)
                throw new ArgumentNullException("reportBuilderService is null");
            if (emailService == null)
                throw new ArgumentNullException("emailService is null");

            _unitOfWorkFactory = unitOfWorkFactory;
            _reportBuilder = reportBuilder;
            _emailService = emailService;
            // todo
            emailService.Configure("reportbuildertest@yandex.com", "Dmitriy", "Abstraction1991");
        }

        //[Route("api/report/selectedorders")]
        public async Task<IHttpActionResult> GetOrdersByPeriod(DateTime startDate, DateTime endDate)
        {
            HttpContext.Current.Cache.Remove("orders");
            List<OrderViewModel> ordersVM;

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                List<Order> orders = null;

                //var te = unitOfWork.Test();
                //var te2 = te.ToList();

                await Task.Run(
                    () => orders = unitOfWork.OrderRepository.FindBy(x => x.OrderDate < endDate && x.OrderDate > startDate).ToList());

                ordersVM = Mapper.Map<IEnumerable<Order>, List<OrderViewModel>>(orders);
                HttpContext.Current.Cache.Add("orders", ordersVM, null, Cache.NoAbsoluteExpiration, new TimeSpan(0,15,0), CacheItemPriority.Normal, null );
            }

            return Ok(ordersVM);
        }

        [HttpPost]
        public async Task<IHttpActionResult> SendReport(ReportRequestViewModel vm)
        {
            var cacheItems = HttpContext.Current.Cache["orders"];

            if (cacheItems != null)
            {
                _reportBuilder.CreateReportFile((IEnumerable<OrderViewModel>)cacheItems);
                _emailService.SendFile(vm.Email, "C://Users/Public/unity.txt");

                //_reportBuilder.DeleteLastReportFile();

            }

            //using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            //{
            //    string userName = RequestContext.Principal.Identity.Name;

            //    User recepient = null;
            //    User currentUser = null;
            //    await Task.Run(() =>
            //    {
            //        recepient = _userProvider.GetUserByName(transactionVM.CorrespondedUser);
            //        currentUser = currentUser = _userProvider.GetUserByName(userName);
            //    });

            //    if (recepient == null)
            //        return BadRequest($"User with name: '{transactionVM.CorrespondedUser}' not found");

            //    if (currentUser == null)
            //        return BadRequest("Internal server error. Please, contact with developers");

            //    Mapper.Initialize(cfg => cfg.CreateMap<TransactionViewModel, Transaction>()
            //        .ForMember(
            //            dest => dest.Recepient,
            //            opt => opt.MapFrom(
            //                src => recepient)));
            //    var transaction = Mapper.Map<TransactionViewModel, Transaction>(transactionVM);

            //    await Task.Run(() => currentUser.ExecuteTransaction(transaction));

            //    unitOfWork.Commit();
            //}

            return Ok();
        }

        public void Dispose()
        {
            if (_reportBuilder != null)
            {
                _reportBuilder.Dispose();
            }

            if (_emailService != null)
            {
                _emailService.Dispose();
            }
        }
    }
}