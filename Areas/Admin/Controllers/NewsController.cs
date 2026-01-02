using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelBooking.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Antlr.Runtime;

namespace HotelBooking.Areas.Admin.Controllers
{

    public class NewsController : Controller
    {
        // Lấy connection string từ webconfig
        string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        // GET: Admin/News
        public ActionResult Index()
        {
            List<NewsModel> list = new List<NewsModel>();
            // Tạo 1 cổng kết nối đến sql sv với chuỗi kết nối là connectionString
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT  a.ID, a.Title, a.[Description], a.[Thumbnail], a.[Detail], a.[Keywords], a.[isPublic], a.[ID_Category], b.[Name] From [tb_News] As a INNER JOIN [db_BookingHotel].[dbo].[tb_NewsCategories] AS b ON a.ID_Category = b.ID ORDER BY ID ASC";

                // Tạo đối tượng để thực hiện câu lệnh sql trên database
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Mở cổng
                conn.Open();
                // Đọc kết quả và lưu vào biến reader

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NewsModel news = new NewsModel
                        
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader.GetString(reader.GetOrdinal("Title")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                            Thumbnail = reader.IsDBNull(reader.GetOrdinal("Thumbnail")) ? null : reader.GetString(reader.GetOrdinal("Thumbnail")),
                            Detail = reader.IsDBNull(reader.GetOrdinal("Detail")) ? null : reader.GetString(reader.GetOrdinal("Detail")),
                            Keywords = reader.IsDBNull(reader.GetOrdinal("Keywords")) ? null : reader.GetString(reader.GetOrdinal("Keywords")),
                            isPublic = reader.IsDBNull(reader.GetOrdinal("isPublic")) ? false : reader.GetBoolean(reader.GetOrdinal("isPublic")),
                            ID_Category = reader.IsDBNull(reader.GetOrdinal("ID_Category")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ID_Category")),
                            Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                        };
                        list.Add(news);
                    }
                }

               
            }
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var model = new NewsCreateVM();
            model.News = new NewsModel();
            model.Categories = new List<SelectListItem>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT * FROM [tb_NewsCategories]";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model.Categories.Add(new SelectListItem
                            {
                                Value = reader.GetInt32(reader.GetOrdinal("ID")).ToString(),
                                Text = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                            });
                        }

                    }

                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NewsCreateVM model)
        {
            string sql = "INSERT INTO tb_News(Title,Description,Thumbnail,Detail,Keywords,isPublic,ID_Category) VALUES('"
                 + model.News.Title + "','"
                 + model.News.Description + "','"
                 +model.News.Thumbnail + "','"
                 +model.News.Detail + "','"
                 +model.News.Keywords + "',"
                 +(model.News.isPublic == true ? 1:0) + ","
                 +(model.News.ID_Category == null ? 1 : model.News.ID_Category) + ")" ;


            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql,conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            string sql = $"DELETE FROM tb_News WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
                return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            NewsModel model = null;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT * FROM tb_News WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    model = new NewsModel
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                        Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader.GetString(reader.GetOrdinal("Title")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                        Thumbnail = reader.IsDBNull(reader.GetOrdinal("Thumbnail")) ? null : reader.GetString(reader.GetOrdinal("Thumbnail")),
                        Detail = reader.IsDBNull(reader.GetOrdinal("Detail")) ? null : reader.GetString(reader.GetOrdinal("Detail")),
                        Keywords = reader.IsDBNull(reader.GetOrdinal("Keywords")) ? null : reader.GetString(reader.GetOrdinal("Keywords")),
                        isPublic = reader.IsDBNull(reader.GetOrdinal("isPublic")) ? false : reader.GetBoolean(reader.GetOrdinal("isPublic")),
                        ID_Category = reader.IsDBNull(reader.GetOrdinal("ID_Category")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ID_Category")),
                    };
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        
        public ActionResult Edit(NewsModel model)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                
                string sql = "UPDATE tb_News SET " +                    
                    " Title = N'" + model.Title + 
                    "', Description = N'" + model.Description + 
                    "', Thumbnail = N'" +model.Thumbnail +
                    "', Detail = N'" +model.Detail +
                    "', Keywords = N'" + model.Keywords +
                    "', isPublic = " +  (model.isPublic == true ? 1:0 )  +
                    ", ID_Category = " + model.ID_Category + " WHERE ID = " + model.ID;
                
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    conn.Open();    
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
    }
}