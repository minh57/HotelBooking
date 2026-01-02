using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelBooking.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace HotelBooking.Controllers
{
    public class NewsController : Controller
    {
        // Lấy connection string từ webconfig
        string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        public ActionResult Index()
        {
            // Tạo 1 list mới để lưu dữ liệu kéo từ sql sv về
            List<NewsModel> news = new List<NewsModel>();

            // Tạo 1 cổng kết nối đến sql sv với chuỗi kết nối là connectionString
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT * FROM tb_News";

                // Tạo đối tượng để thực hiện câu lệnh sql trên database
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Mở cổng
                conn.Open();
                // Đọc kết quả và lưu vào biến reader
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    news.Add(new NewsModel
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        Thumbnail = reader["Thumbnail"].ToString(),
                        Detail = reader["Detail"].ToString(),
                        Keywords = reader["Keywords"].ToString(),
                        isPublic = Convert.ToBoolean(reader["isPublic"])
                    });
                }
            }
            return View(news);
        }
    }
}