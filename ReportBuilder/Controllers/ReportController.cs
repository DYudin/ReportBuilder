﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using ReportBuilder.Infrastructure.Repositories.Abstract;
using ReportBuilder.Infrastructure.Services.Abstract;
using ReportBuilder.Models;
using ReportBuilder.Services.UnitOfWork.Abstract;
using ReportBuilder.ViewModels;

namespace ReportBuilder.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IOrderRepository _orderRepository;
        private readonly IReportBuilder _reportBuilder;
        private readonly IEmailService _emailService;
     
        public ReportController()
        {
        }

        public ReportController(
            IUnitOfWorkFactory unitOfWorkFactory,
            IOrderRepository orderRepository,
            IReportBuilder reportBuilder,
            IEmailService emailService
            )
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException("unitOfWorkFactory is null");
            if (orderRepository == null)
                throw new ArgumentNullException("orderRepository is null");
            if (reportBuilder == null)
                throw new ArgumentNullException("reportBuilderService is null");
            if (emailService == null)
                throw new ArgumentNullException("emailService is null");

            _unitOfWorkFactory = unitOfWorkFactory;
            _orderRepository = orderRepository;
            _reportBuilder = reportBuilder;
            _emailService = emailService;
        }

        //[Route("api/report/selectedorders")]
        public async Task<IHttpActionResult> GetSelectedOrders(DateTime startDate, DateTime endDate)
        {
            List<OrderViewModel> ordersVM;

            using (_unitOfWorkFactory.Create())
            {
                List<Order> orders = null;

                await Task.Run(
                    () =>
                        orders =
                            _orderRepository.FindByIncluding(x => x.OrderDate < endDate && x.OrderDate > startDate)
                                .ToList());

                Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderViewModel>()
                    .ForMember(
                        dest => dest.OrderId,
                        opt => opt.MapFrom(src => src.ID))
                    .ForMember(
                        dest => dest.ProductName,
                        opt => opt.MapFrom(src => src.OrderDetail.FirstOrDefault().Product.Name))
                    .ForMember(
                        dest => dest.Quantity,
                        opt => opt.MapFrom(src => src.OrderDetail.FirstOrDefault().Quantity))
                    .ForMember(
                        dest => dest.UnitPrice,
                        opt => opt.MapFrom(src => src.OrderDetail.FirstOrDefault().UnitPrice)));

                ordersVM = Mapper.Map<IEnumerable<Order>, List<OrderViewModel>>(orders);
            }

            return Ok(ordersVM);
        }

        [HttpPost]
        public async Task<IHttpActionResult> SendReport(string email)
        {
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
    }
}