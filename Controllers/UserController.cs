using EMS_Api.Models;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EMS_Api.Controllers
{
    public class UserController : ApiController
    {

        MySqlConnection con = new MySqlConnection(@"server=localhost;username=root;password=admin;database=mas_employee");

        [EnableCors(origins: "*", headers: " * ", methods: " GET ")]
        [HttpGet]
        public HttpResponseMessage GetAllUser()
        {
            try
            {
                con.Open();
                string query = "select * from User";
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

        [EnableCors(origins: "*", headers: " * ", methods: " GET ")]

        [HttpGet]
        public HttpResponseMessage GetUserById(int Id)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from User where Id = " + Id;
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
        [Route("api/PostUser")]
        [HttpPost]
        public HttpResponseMessage PostUser(User user)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Department", user.Department);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Birth_Date", user.Birth_Date);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@City", user.City);
                cmd.Parameters.AddWithValue("@State", user.State);
                cmd.Parameters.AddWithValue("@Country", user.Country);
                cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@Hobbies", user.Hobbies);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.CommandText = "Insert into User(Department,Name,Email,Birth_Date,Gender,Phone,City,State,Country,Address,Hobbies,Password) value(@Department,@Name,@Email,@Birth_Date,@Gender,@Phone,@City,@State,@Country,@Address,@Hobbies,@Password)";
                cmd.ExecuteNonQuery();
                con.Close();

                return Request.CreateResponse(HttpStatusCode.OK, "Data Insert Successfull");
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

        [EnableCors(origins: "*", headers: " * ", methods: " PUT ")]

        [HttpPut]
        public HttpResponseMessage PutUser(int id, User user)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Department", user.Department);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Birth_Date", user.Birth_Date);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@City", user.City);
                cmd.Parameters.AddWithValue("@State", user.State);
                cmd.Parameters.AddWithValue("@Country", user.Country);
                cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@Hobbies", user.Hobbies);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.CommandText = "UPDATE User SET Department = @Department, Name = @Name,Email = @Email,Birth_Date = @Birth_Date,Gender = @Gender,Phone = @Phone,City = @City,State = @State,Country = @Country,Address = @Address,Hobbies = @Hobbies,Password = @Password WHERE Id = " + id;
                cmd.ExecuteNonQuery();
                con.Close();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [EnableCors(origins: "*", headers: " * ", methods: " DELETE      ")]

        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = con;
                cmd.CommandText = "Delete from User where Id = " + id;

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

        [Route("api/Login")]
        public HttpResponseMessage LoginUser(Login login)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Email", login.Email);
                cmd.Parameters.AddWithValue("@Password", login.Password);
                cmd.CommandText = "select * from User where Email = @Email AND Password = @Password";
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