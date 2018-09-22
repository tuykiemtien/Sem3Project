using Sem3Project.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sem3Project.Models
{
    public class ProductModel
    {
        private NorthwindEntities db = new NorthwindEntities();

        public List<ProductDTO> GetAllProducts()
        {
            List<ProductDTO> list = db.Products.Select(p => new ProductDTO()
            {
                ProductID = p.ProductID,
                ProductName  = p.ProductName,
                SupplierID = p.SupplierID,
                CategoryID = p.CategoryID,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                UnitsOnOrder = p.UnitsOnOrder,
                ReorderLevel = p.ReorderLevel,
                Discontinued = p.Discontinued,
                ProductImage = p.ProductImage

    }).ToList();

            return list;
        }

        public ProductDTO GetProductById(int id)
        {
            ProductDTO list = db.Products.Where(s => s.ProductID == id).Select(p => new ProductDTO()
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                SupplierID = p.SupplierID,
                CategoryID = p.CategoryID,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                UnitsOnOrder = p.UnitsOnOrder,
                ReorderLevel = p.ReorderLevel,
                Discontinued = p.Discontinued,
                ProductImage = p.ProductImage


            }).FirstOrDefault();
            return list;
        }

        public ProductViewStore GetProductViewPage(string search, string order, int pageIndex, int pageSize)
        {
            ProductViewStore product = new ProductViewStore();
            product.PageIndex = pageIndex;
            product.PageSize = pageSize;
            product.Search = search;
            product.Order = order;

        
            SqlParameter paramPageSearch = new SqlParameter("@PageSearch", search);
            SqlParameter paramOrderColumn = new SqlParameter("@SortOrder", order);
            SqlParameter paramPageIndex = new SqlParameter("@PageIndex", pageIndex);
            SqlParameter paramPageSize = new SqlParameter("@PageSize", pageSize);
            SqlParameter paramRecordCount = new SqlParameter("@RecordCount", product.RecordCount);
            paramRecordCount.Direction = ParameterDirection.Output;
            var listResult = db.Database.SqlQuery<Product>("EXEC [dbo].[usp_MaintainProductPage] @PageSearch, @SortOrder, @PageIndex, @PageSize, @RecordCount = @RecordCount OUTPUT", paramPageSearch, paramOrderColumn, paramPageIndex, paramPageSize, paramRecordCount).ToList();
            product.RecordCount = (int)paramRecordCount.Value;
            product.Products = listResult.Select(p => new ProductDTO()
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                SupplierID = p.SupplierID,
                CategoryID = p.CategoryID,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                UnitsOnOrder = p.UnitsOnOrder,
                ReorderLevel = p.ReorderLevel,
                Discontinued = p.Discontinued,
                ProductImage = p.ProductImage
            }).ToList();

            return product;
        }

        public bool PostNewProduct(ProductDTO p)
        {
            Product productInsert = new Product()
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                SupplierID = p.SupplierID,
                CategoryID = p.CategoryID,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                UnitsOnOrder = p.UnitsOnOrder,
                ReorderLevel = p.ReorderLevel,
                Discontinued = p.Discontinued,
                ProductImage = p.ProductImage

            };
            db.Products.Add(productInsert);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool PutProduct(ProductDTO p)
        {
            Product productInsert = db.Products.FirstOrDefault(s => s.ProductID == p.ProductID);


            productInsert.ProductID = p.ProductID;
            productInsert.ProductName = p.ProductName;
            productInsert.SupplierID = p.SupplierID;
            productInsert.CategoryID = p.CategoryID;
            productInsert.QuantityPerUnit = p.QuantityPerUnit;
            productInsert.UnitPrice = p.UnitPrice;
            productInsert.UnitsInStock = p.UnitsInStock;
            productInsert.UnitsOnOrder = p.UnitsOnOrder;
            productInsert.ReorderLevel = p.ReorderLevel;
            productInsert.Discontinued = p.Discontinued;
            productInsert.ProductImage = p.ProductImage;


            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            Product product = db.Products.FirstOrDefault(s => s.ProductID == id);
            if (product != null)
            {
               
                db.Products.Remove(product);
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