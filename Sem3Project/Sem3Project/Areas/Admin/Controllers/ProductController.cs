using Sem3Project.Entites;
using Sem3Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Sem3Project.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();


        }

        [HttpPost]
        public JsonResult GetProduct(DTParameters param)
        {

            ProductViewStore product = new ProductViewStore();
            product.PageIndex = param.Start / param.Length + 1;
            product.PageSize = param.Length;
            if (param.Search.Value == null)
            {
                product.Search = "%%";
            }
            else
            {
                product.Search = "%" + param.Search.Value + "%";
            }
            product.Order = param.SortOrder;
            ProductViewStore categories = new ProductModel().GetProductViewPage(product.Search, product.Order, product.PageIndex, product.PageSize);

            DTResult<ProductDTO> final = new DTResult<ProductDTO>()
            {
                draw = param.Draw,
                data = categories.Products.ToList(),
                recordsFiltered = categories.RecordCount,
                recordsTotal = categories.Products.Count
            };
            return Json(final);

        }

        public ActionResult Details(int id)
        {
            return PartialView(new ProductModel().GetProductById(id));
        }
        public ActionResult Edit(int id)
        {

            ViewBag.ID = id;
            ViewBag.listAdmin = new ProductModel().GetAllProducts().Where(s => !s.ProductName.Contains("Product Representative")).ToList();

            return PartialView();
        }

        public JsonResult GetProductByID(int id)
        {
            ProductDTO p = new ProductModel().GetProductById(id);

            return Json(new
            {
                p.ProductID,
                p.ProductName,
                 p.SupplierID,
                 p.CategoryID,
             p.QuantityPerUnit,
                 p.UnitPrice,
               p.UnitsInStock,
                 p.UnitsOnOrder,
                 p.ReorderLevel,
                p.Discontinued,
                p.ProductImage

            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {

            ViewBag.listAdmin = new ProductModel().GetAllProducts().Where(s => !s.ProductName.Contains("Product  Representative")).ToList();
            return PartialView();
        }

        [HttpPost]
        public ActionResult Insert()
        {
            HttpPostedFileBase file = Request.Files[0];
            ProductDTO p = new ProductDTO();
          
            p.ProductName = Request.Params["ProductName"];

            p.QuantityPerUnit = Request.Params["QuantityPerUnit"];
            p.ProductImage = Request.Params["ProductImage"];
            string strSupplierID = Request.Params["SupplierID"];
            string strCategoryID = Request.Params["CategoryID"];
            string strUnitPrice = Request.Params["UnitPrice"];
            string strUnitsInStock = Request.Params["UnitsInStock"];
            string strUnitsOnOrder = Request.Params["UnitsOnOrder"];
            string strReorderLevel = Request.Params["ReorderLevel"];
            string strDiscontinued = Request.Params["Discontinued"];


            bool check = new ProductModel().PostNewProduct(p);

            if (check)
            {
                ProductDTO lastCate = new ProductModel().GetAllProducts().LastOrDefault();
                var fileName = "";
                var imageLink = @"~/Upload/Product/";

                if (file != null)
                {

                    fileName = Path.GetFileName(file.FileName);
                    string[] splitName = fileName.Split('.');
                    fileName = "p" + lastCate.ProductID + "." + splitName[1];
                    file.SaveAs(HttpContext.Server.MapPath(imageLink) + fileName);
                }
              
                bool checkImage = new ProductModel().PutProduct(lastCate);
                return Json(new { Ok = true });
            }
            else
            {
                return Json(new { Ok = false });
            }
        }


        public ActionResult Update()
        {

            int cateId = int.Parse(Request.Form["ProductID"]);
            ProductDTO p = new ProductDTO();

            p.ProductName = Request.Params["ProductName"];

            p.QuantityPerUnit = Request.Params["QuantityPerUnit"];
            p.ProductImage = Request.Params["ProductImage"];
            string strSupplierID = Request.Params["SupplierID"];
            string strCategoryID = Request.Params["CategoryID"];
            string strUnitPrice = Request.Params["UnitPrice"];
            string strUnitsInStock = Request.Params["UnitsInStock"];
            string strUnitsOnOrder = Request.Params["UnitsOnOrder"];
            string strReorderLevel = Request.Params["ReorderLevel"];
            string strDiscontinued = Request.Params["Discontinued"];
            p.SupplierID = Int16.Parse(strSupplierID);
            p.CategoryID = Int16.Parse(strCategoryID);
            p.UnitPrice = Int16.Parse(strUnitPrice);
            p.UnitsInStock = Int16.Parse(strUnitsInStock);
            p.UnitsOnOrder = Int16.Parse(strUnitsOnOrder);
            p.ReorderLevel = Int16.Parse(strReorderLevel);
            p.Discontinued = Int32.Parse(strDiscontinued) !=0;

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                var fileName = "";
                var imageLink = @"~/Upload/Product/";
                fileName = Path.GetFileName(file.FileName);
                string[] splitName = fileName.Split('.');
                fileName = "product" + p.ProductID + "." + splitName[1];
                file.SaveAs(HttpContext.Server.MapPath(imageLink) + fileName);
               
            }
            bool check = new ProductModel().PutProduct(p);
            if (check)
            {
                return Json(new { Ok = true });
            }
            else
            {
                return Json(new { Ok = false });
            }
        }

    }
}