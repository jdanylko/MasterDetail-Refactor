using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MasterDetailExample.Models;
using MasterDetailExample.Repository;
using MasterDetailExample.ViewModel;

namespace MasterDetailExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;

        public HomeController(ICustomerRepository customerRepository,
            ICartItemRepository cartItemRepository,
            IProductRepository productRepository)
        {
            _customerRepository = customerRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var customer = _customerRepository.First(e => true);
            var items = _cartItemRepository.Find(e => e.CartId == customer.CustomerId).ToList();
            var products = _productRepository.GetAll();

            foreach (var cartItem in items)
            {
                cartItem.Product = products.FirstOrDefault(e => e.ProductId == cartItem.ProductId);
            }

            var viewModel = new CartViewModel
            {
                Customer = customer,
                CartItems = items.ToList()
            };

            return View(viewModel);
        }

        // For our Product Dialog
        [HttpPost]
        public ActionResult ProductGrid()
        {
            return PartialView("_productGrid", new ProductViewModel
            {
                // create our DTOs
                Products = _productRepository
                    .GetAll()
                    .Select(t => new Product {ProductId = t.ProductId, Title = t.Title, Price = t.Price})
                    .ToList()
            });
        }

        [HttpPost]
        public PartialViewResult OrderGrid(Cart cart)
        {
            var model = new CartViewModel
            {
                Customer = null,
                Cart = null
            };

            // Get existing items.
            var cartList = _cartItemRepository
                .GetCartItems(cart.CartId);

            // Add the Items NOT in the list.
            var newItems = cart.Items
                .Where(item => !cartList.Select(p=> p.ProductId).Contains(item.ProductId))
                .ToList();
            cartList.AddRange(newItems);

            // Save the new added items.
            foreach (var item in newItems)
            {
                _cartItemRepository.Add(new CartItem
                {
                    ProductId = item.ProductId,
                    Quantity = 1,
                    CartId = 1
                });
            }
            _cartItemRepository.SaveChanges();
            
            // Use the new cartList to load the products and defaults
            model.CartItems = cartList
                .Select(e =>
                {
                    e.Product = _productRepository.First(product => product.ProductId == e.ProductId);
                    e.Quantity = e.Quantity == 0 ? 1 : e.Quantity;
                    return e;
                })
                .ToList();

            return PartialView("_childGrid", model);
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
