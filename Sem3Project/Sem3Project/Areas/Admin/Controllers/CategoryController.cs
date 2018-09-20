using Sem3Project.Entites;
using Sem3Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sem3Project.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetCategory(DTParameters param)
        {

            CategoryPageStore category = new CategoryPageStore();
            category.PageIndex = param.Start / param.Length + 1;
            category.PageSize = param.Length;
            if (param.Search.Value == null)
            {
                category.Search = "%%";
            }
            else
            {
                category.Search = "%" + param.Search.Value + "%";
            }
            category.Order = param.SortOrder;
            CategoryPageStore categories = new CategoryModel().GetCategoryByPage(category.Search, category.Order, category.PageIndex, category.PageSize);

            DTResult<CategoryDTO> final = new DTResult<CategoryDTO>()
            {
                draw = param.Draw,
                data = categories.Categories.ToList(),
                recordsFiltered = categories.RecordCount,
                recordsTotal = categories.Categories.Count
            };
            return Json(final);

        }

        public ActionResult Details(int id)
        {
            return PartialView(new CategoryModel().GetCategoryById(id));
        }
        public ActionResult Edit(int id)
        {

            ViewBag.ID = id;
            ViewBag.listAdmin = new CategoryModel().GetAllCategory().ToList();

            return PartialView();
        }

        public JsonResult GetCategoryByID(int id)
        {
            CategoryDTO category = new CategoryModel().GetCategoryById(id);

            return Json(new
            {
                category.CategoryID,
                category.CategoryName,
                category.Description,
                category.Picture,
                category.CategoryImage

            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {

            ViewBag.listAdmin = new CategoryModel().GetAllCategory().ToList();
            return PartialView();
        }

        [HttpPost]
        public ActionResult Insert()
        {
            HttpPostedFileBase file = Request.Files[0];
            CategoryDTO category = new CategoryDTO();
            category.CategoryName = Request.Params["CategoryName"];
            category.Description = Request.Params["Description"];

            bool check = new CategoryModel().PostNewCategory(category);

            if (check)
            {
                CategoryDTO lastCate = new CategoryModel().GetAllCategory().LastOrDefault();
                var fileName = "";
                var imageLink = @"~/Upload/Employee/";

                if (file != null)
                {

                    fileName = Path.GetFileName(file.FileName);
                    string[] splitName = fileName.Split('.');
                    fileName = "employee" + lastCate.CategoryID + "." + splitName[1];
                    file.SaveAs(HttpContext.Server.MapPath(imageLink) + fileName);
                }
                lastCate.CategoryImage = fileName;
                bool checkImage = new CategoryModel().PutCategory(lastCate);
                return Json(new { Ok = true });
            }
            else
            {
                return Json(new { Ok = false });
            }
        }


        public ActionResult Update()
        {

            int cateId = int.Parse(Request.Form["CategoryID"]);
            CategoryDTO category = new CategoryModel().GetCategoryById(cateId);

            category.CategoryName = Request.Params["CategoryName"];
            category.Description = Request.Params["Description"];

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                var fileName = "";
                var imageLink = @"~/Upload/Employee/";
                fileName = Path.GetFileName(file.FileName);
                string[] splitName = fileName.Split('.');
                fileName = "employee" + category.CategoryID + "." + splitName[1];
                file.SaveAs(HttpContext.Server.MapPath(imageLink) + fileName);
                category.CategoryImage = fileName;
            }
            bool check = new CategoryModel().PostNewCategory(category);
            if (check)
            {
                return Json(new { Ok = true });
            }
            else
            {
                return Json(new { Ok = false });
            }
        }
        public ActionResult Delete(int id)
        {

            string imageLink = new CategoryModel().GetCategoryById(id).CategoryImage;
            bool check = new CategoryModel().DeleteCategory(id);
            if (check)
            {

                var filePath = Server.MapPath("~/Upload/Employee/" + imageLink);
                FileInfo file = new FileInfo(filePath);
                if (file.Exists)
                {
                    file.Delete();
                }
                return Json(new { Ok = true });
            }
            else
            {
                return Json(new { Ok = false });
            }
        }

    }
}