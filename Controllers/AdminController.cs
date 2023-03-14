using EMS_Api.Models;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EMS_Api.Controllers
{
    public class AdminController : ApiController
    {

        MySqlConnection con = new MySqlConnection(@"server=localhost;username=root;password=admin;database=mas_employee");

        [EnableCors(origins: "*", headers: " * ", methods: " GET ")]
        [HttpGet]
        public HttpResponseMessage GetAllAdmin()
        {
            try
            {
                con.Open();
                string query = "select * from Admin";
                var cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                var reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(dt));
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAdminById(int Id)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from Admin where Id = " + Id;
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();
                if (dt == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, Id);
                }
                return Request.CreateResponse(HttpStatusCode.OK, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [EnableCors(origins: "*", headers: " * ", methods: " POST ")]
        [Route("api/PostAdmin")]
        [HttpPost]
        public HttpResponseMessage PostAdmin(Admin admin)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Name", admin.Name);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.Parameters.AddWithValue("@Phone", admin.Phone);
                cmd.Parameters.AddWithValue("@Password", admin.Password);
                cmd.CommandText = "Insert into Admin(Name,Email,Phone,Password) value(@Name,@Email,@Phone,@Password)";
                cmd.ExecuteNonQuery();
                con.Close();

                return Request.CreateResponse(HttpStatusCode.OK,"Data Insert Successfull");

            }
            catch (Exception ex )
            {
                if (ex.Message.Contains("Duplicate entry"))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Already exists");
                }
                return Request.CreateResponse(ex);
             }

        }

        [EnableCors(origins: "*", headers: " * ", methods: " PUT ")]

        [HttpPut]
        public HttpResponseMessage Putadmin(int id, Admin admin)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Name", admin.Name);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.Parameters.AddWithValue("@Phone", admin.Phone);
                cmd.Parameters.AddWithValue("@Password", admin.Password);
                cmd.CommandText = "UPDATE Admin SET  Name = @Name,Email = @Email,Phone = @Phone,Password = @Password WHERE Id = " + id;
                cmd.ExecuteNonQuery();
                con.Close();

                return Request.CreateResponse(HttpStatusCode.OK, "Data Update Successfull");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Already exists");
                }
                return Request.CreateResponse(ex);
            }
        }


        [EnableCors(origins: "*", headers: " * ", methods: " DELETE      ")]

        [HttpDelete]
        public HttpResponseMessage Deleteadmin(int id)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = con;
                cmd.CommandText = "Delete from Admin where Id = " + id;

                cmd.ExecuteNonQuery();

                con.Close();

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception)
            {
                throw;
            }

        }

        [EnableCors(origins: "*", headers: " * ", methods: " POST ")]

        [HttpPost]

        [Route("api/AdminLogin")]
        public HttpResponseMessage Loginadmin(Admin admin)
        {

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.Parameters.AddWithValue("@Password", admin.Password);
                cmd.CommandText = "select * from Admin where Email = @Email AND Password = @Password";
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                if (dt.Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, dt);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, dt);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}

