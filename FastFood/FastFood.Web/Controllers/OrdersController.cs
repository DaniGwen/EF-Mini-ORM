namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    using Data;
    using ViewModels.Orders;
    using FastFood.Models;
    using AutoMapper.QueryableExtensions;
    using FastFood.Models.Enums;

    public class OrdersController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public OrdersController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var viewOrder = new CreateOrderViewModel
            {
                Items = this.context.Items.Select(x => x.Name).ToList(),
                Employees = this.context.Employees.Select(x => x.Name).ToList(),
            };

            return this.View(viewOrder);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        {
            var employee = this.context.Employees
                .FirstOrDefault(e => e.Name == model.EmployeeName);

            var item = this.context.Items
                .FirstOrDefault(i => i.Name == model.ItemName);

            var order = mapper.Map<Order>(model);

            order.Type = Enum.Parse<OrderType>(model.OrderType);
            order.Employee = employee;
            order.EmployeeId = employee.Id;
            order.DateTime = DateTime.Now;
            order.TotalPrice = model.Quantity * item.Price;
            order.OrderItems.Add(new OrderItem
            {
                ItemId = item.Id,
                Quantity = model.Quantity,
                Order = order
            });

            this.context.Orders.Add(order);
            this.context.SaveChanges();

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            var orders = this.context.Orders
                 .ProjectTo<OrderAllViewModel>(mapper.ConfigurationProvider)
                 .ToList();

            return this.View(orders);
        }
    }
}
