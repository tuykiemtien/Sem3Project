using Sem3Project.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sem3Project.Models
{
    public class CategoryModel
    {
        private NorthwindEntities db = new NorthwindEntities();

        public List<CategoryDTO> GetAllCategory()
        {
            List<CategoryDTO> list = db.Categories.Select(e => new CategoryDTO
            {
                CategoryID = e.CategoryID,
                CategoryName = e.CategoryName,
                Description = e.Description,
                Picture = e.Picture,
                CategoryImage = e.CategoryImage
    }).ToList();

            return list;
        }

        public CategoryDTO GetCategoryById(int id)
        {
            CategoryDTO list = db.Categories.Where(s => s.CategoryID == id).Select(e => new CategoryDTO()
            {
                CategoryID = e.CategoryID,
                CategoryName = e.CategoryName,
                Description = e.Description,
                Picture = e.Picture,
                CategoryImage = e.CategoryImage

            }).FirstOrDefault();
            return list;
        }

        public CategoryPageStore GetCategoryByPage(string search, string order, int pageIndex, int pageSize)
        {
            CategoryPageStore category = new CategoryPageStore();
            category.PageIndex = pageIndex;
            category.PageSize = pageSize;
            category.Search = search;
            category.Order = order;

            SqlParameter paramPageSearch = new SqlParameter("@PageSearch", search);
            SqlParameter paramOrderColumn = new SqlParameter("@SortOrder", order);
            SqlParameter paramPageIndex = new SqlParameter("@PageIndex", pageIndex);
            SqlParameter paramPageSize = new SqlParameter("@PageSize", pageSize);
            SqlParameter paramRecordCount = new SqlParameter("@RecordCount", category.RecordCount);
            paramRecordCount.Direction = ParameterDirection.Output;
            var listResult = db.Database.SqlQuery<CategoryDTO>("EXEC [dbo].[usp_MaintainCategoryPage] @PageSearch, @SortOrder, @PageIndex, @PageSize, @RecordCount = @RecordCount OUTPUT", paramPageSearch, paramOrderColumn, paramPageIndex, paramPageSize, paramRecordCount).ToList();
            category.RecordCount = (int)paramRecordCount.Value;
            category.Categories = listResult.Select(e => new CategoryDTO()
            {
                CategoryID = e.CategoryID,
                CategoryName = e.CategoryName,
                Description = e.Description,
                Picture = e.Picture,
                CategoryImage = e.CategoryImage
            }).ToList();

            return category;
        }

        public bool PostNewCategory(CategoryDTO category)
        {
            byte[] byteValue2 = { 0x00C9 };
            Category CategoryInsert = new Category()
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description,
             //   Picture = byteValue2,
                CategoryImage = "kokokookok"

            };
            db.Categories.Add(CategoryInsert);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool PutCategory(CategoryDTO category)
        {
            Category categoryInsert = db.Categories.FirstOrDefault(s => s.CategoryID == category.CategoryID);

            categoryInsert.CategoryID = category.CategoryID;
            categoryInsert.CategoryName= category.CategoryName;
            categoryInsert.Description = category.Description;
            categoryInsert.Picture = category.Picture;
            categoryInsert.CategoryImage = category.CategoryImage;


            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool DeleteCategory(int id)
        {
            Category category = db.Categories.FirstOrDefault(s => s.CategoryID == id);
            if (category != null)
            {
                Product product = db.Products.FirstOrDefault(s => s.CategoryID == id);
                if (product != null)
                {
                    db.Products.Remove(product);
                }
                db.Categories.Remove(category);
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}