using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Z_Market.Models;
using Z_Market.ViewModels;
using System.Web.SessionState;

namespace Z_Market.Controllers
{
    public class OrdersController : Controller
    {
        Z_MarketContext db = new Z_MarketContext();
        // GET: Orders
        public ActionResult NewOrder()
        {
            var orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();
            //se guarda la orden en session por medio de la clase generica para el manejo de session.
            SessionHelper.Set(Session, SessionKey.ORDER_VIEW, orderView);

            //registros para llenar el combo

            ViewBag.CustomerID = new SelectList(ObtenerCliente(), "CustomerID", "FullName");
            //fin llenar combo

            return View(orderView);
        }

        [HttpPost]
        public ActionResult NewOrder(OrderView orderView)
        {

            //if (orderView != null && orderView.Customer != null && orderView.Customer.CustomerID != 0)
            //{
            //    ViewBag.CustomerID = new SelectList(ObtenerCliente(), "CustomerID", "FullName", orderView.Customer.CustomerID);
            //}
            //else
            //{
            ViewBag.CustomerID = new SelectList(ObtenerCliente(), "CustomerID", "FullName");
            //}

            return View(orderView);
        }


        public ActionResult AddProduct()
        {
            ViewBag.ProductID = new SelectList(ObtenerProducto(), "ProductID", "Description");
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
        {
            var orderView = SessionHelper.Get<OrderView>(Session, SessionKey.ORDER_VIEW);
            ViewBag.ProductID = new SelectList(ObtenerProducto(), "ProductID", "Description");
            ViewBag.CustomerID = new SelectList(ObtenerCliente(), "CustomerID", "FullName");
            var productID = int.Parse(Request["ProductID"]);
            if (productID == 0)
            {
                ViewBag.Error = "Debe seleccionar un producto.";
                return View(productOrder);
            }
            var product = db.Products.Find(productID);
            if (product == null)
            {
                ViewBag.Error = "Producto no existe.";
                return View(productOrder);
            }
            var producOrder = orderView.Products.Find(b => b.ProductID == productID);
            if (producOrder == null)
            {
                productOrder = new ProductOrder
                {
                    Description = product.Description,
                    Price = product.Price,
                    ProductID = product.ProductID,
                    Quantity = float.Parse(Request["Quantity"])
                };
                orderView.Products.Add(productOrder);
            }
            else
            {
                producOrder.Quantity += float.Parse(Request["Quantity"]);
            }
            return View("NewOrder", orderView);
        }




        /// <summary>
        /// permite agregar a los datos de clietes un registre con la descripcion seleccione
        /// </summary>
        /// <returns></returns>
        private List<Customer> ObtenerCliente()
        {
            var list = db.Customers.ToList();
            list.Add(new Customer { CustomerID = 0, FirstName = ".[Seleccione cliente..]" });
            list.OrderBy(o => o.FullName).ToList();
            return list;
        }

        private List<Product> ObtenerProducto()
        {
            var list = db.Products.ToList();
            list.Add(new Product { ProductID = 0, Description = ".[Seleccione producto..]" });
            list.OrderBy(o => o.Description).ToList();
            return list;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}