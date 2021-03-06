﻿using System;
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
            //var orderView = new OrderView();
            //orderView.Customer = new Customer();
            //orderView.Products = new List<ProductOrder>();
            ////se guarda la orden en session por medio de la clase generica para el manejo de session.
            //SessionHelper.Set(Session, SessionKey.ORDER_VIEW, orderView);

            var orderView = InicializarOrderView();

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
            orderView = SessionHelper.Get<OrderView>(Session, SessionKey.ORDER_VIEW);
            var customerID = int.Parse(Request["CustomerID"]);
            if (customerID == 0)
            {
                ViewBag.Error = "Debe seleccionar un cliente";
                return View(orderView);
            }
            var customer = db.Customers.Find(customerID);
            if (customer == null)
            {
                ViewBag.Error = "El cliente no existe.";
                return View(orderView);
            }

            if (orderView.Products.Count == 0)
            {
                ViewBag.Error = "Debe ingresar detalle";
                return View(orderView);
            }

            //inicio de la transaccion.
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var order = new Order
                    {
                        CustomerID = customerID,
                        DateOrder = DateTime.Now,
                        OrderStatus = OrderStatus.Creada
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();
                    //obtener identity del ultimo registro guardado.
                    int ultimoID = order.OrderID;
                    List<OrderDetail> listOrderDetail = new List<OrderDetail>();
                    foreach (var item in orderView.Products)
                    {
                        var orderDetail = new OrderDetail
                        {
                            Description = item.Description,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            OrderID = ultimoID,
                            ProductID = item.ProductID
                        };
                        listOrderDetail.Add(orderDetail);
                    }
                    //enviar todos lor registros en una sola operación a la base de datos.
                    db.OrderDetails.AddRange(listOrderDetail);
                    db.SaveChanges();
                    transaccion.Commit();
                    ViewBag.Message = string.Format("La orden: {0}, registrada con exito.", ultimoID);

                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    ViewBag.Error = "Error :" + ex.Message;
                    return View(orderView);
                }
            }
            orderView = InicializarOrderView();
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
            int productID;
            int.TryParse(Request["ProductID"], out productID);
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

        private void AgregarProductoCarrito()
        {
            var orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();
            //se guarda la orden en session por medio de la clase generica para el manejo de session.
            SessionHelper.Set(Session, SessionKey.ORDER_VIEW, orderView);
        }

        /// <summary>
        /// metodo que se utiliza para agregar los producto que el cliente selecciona al carrito
        /// </summary>
        /// <returns></returns>
        private OrderView InicializarOrderView()
        {
            OrderView orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();
            SessionHelper.Set(Session, SessionKey.ORDER_VIEW, orderView);
            return orderView;
        }

        /// <summary>
        /// permite agregar a los datos de clietes un registre con la descripcion seleccione
        /// </summary>
        /// <returns></returns>
        private List<Customer> ObtenerCliente()
        {
            var list = db.Customers.ToList();
            list.Add(new Customer { CustomerID = 0, FirstName = "[-Seleccione cliente..]" });
            list.OrderBy(o => o.FirstName).ToList();
            return list;
        }

        private List<Product> ObtenerProducto()
        {
            var list = db.Products.ToList();
            list.Add(new Product { ProductID = 0, Description = "[-Seleccione producto..]" });
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