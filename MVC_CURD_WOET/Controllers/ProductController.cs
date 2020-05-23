using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MVC_CURD_WOET.Models;

namespace MVC_CURD_WOET.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = @"Data source=.; initial catalog=MVC_CurdDB; integrated security=true";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter sqlda = new SqlDataAdapter("Select * from Product", con);
                sqlda.Fill(dtblProduct);

            }


            return View(dtblProduct);
        }



        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "insert into Product values (@ProductName,@Price,@Count)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                cmd.Parameters.AddWithValue("@Price", productModel.Price);
                cmd.Parameters.AddWithValue("@Count", productModel.Count);
                cmd.ExecuteNonQuery();
            }


            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from Product where ProductID=@ProductID";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sda.Fill(dt);

            }
            if (dt.Rows.Count == 1)
            {
                productModel.ProductID = Convert.ToInt32(dt.Rows[0][0].ToString());
                productModel.ProductName = dt.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dt.Rows[0][2].ToString());
                productModel.Count = Convert.ToInt32(dt.Rows[0][3].ToString());

                return View(productModel);
            }
            else
            {
                return RedirectToAction("Index");
            }



        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "update Product set ProductName=@ProductName,Price=@Price,Count=@Count where ProductID=@ProductID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                    cmd.Parameters.AddWithValue("@Price", productModel.Price);
                    cmd.Parameters.AddWithValue("@Count", productModel.Count);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "delete from  Product  where ProductID=@ProductID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductID", id);

                cmd.ExecuteNonQuery();
                
            }
            return RedirectToAction("Index");
        }
    }
}
