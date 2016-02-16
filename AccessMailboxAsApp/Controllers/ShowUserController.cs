using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccessMailboxAsApp.Models;

namespace AccessMailboxAsApp.Controllers
{
    public class ShowUserController : Controller
    {
        // GET: ShowUser
        [ActionName("ShowData")]
        public ActionResult Index(string userId)
        {
            ViewData["PersonName"] = userId;
            
            showData objProductModel = new showData();
            objProductModel.ProductData = new Product();
            objProductModel.ProductData = GetChartData(userId);
            objProductModel.ReceiveMail = "ReceiveMail";
            objProductModel.SendMail = "SendMail";
            objProductModel.OutstandingMail = "OutstandingMail";
            return View(objProductModel);
        }
        public ActionResult Index()
        {
            return View();
        }
        public Product GetChartData(string userid)
        {
             Product objproduct = new Product();
         
            string test=getReceivedMail(userid, DateTime.Now, DateTime.Now);
             test = getSendMail(userid, DateTime.Now, DateTime.Now);
            objproduct.ReceivedCount = getReceivedMail(userid, DateTime.Now, DateTime.Now) + "," + getReceivedMail(userid, DateTime.Now.AddDays(-7), DateTime.Now.AddDays(-1)) + "," + getReceivedMail(userid, DateTime.Now.AddDays(-14), DateTime.Now.AddDays(-8));
            objproduct.SendCount = getSendMail(userid, DateTime.Now, DateTime.Now) + "," + getSendMail(userid, DateTime.Now.AddDays(-7), DateTime.Now.AddDays(-1)) + "," + getSendMail(userid, DateTime.Now.AddDays(-14), DateTime.Now.AddDays(-8));
            objproduct.TotalReceiveCom = getReceivedMailTotal(DateTime.Now, DateTime.Now) + "," + getReceivedMailTotal( DateTime.Now.AddDays(-7), DateTime.Now.AddDays(-1)) + "," + getReceivedMailTotal(DateTime.Now.AddDays(-14), DateTime.Now.AddDays(-8));
            objproduct.TotalSendCom = getSendMailTotal( DateTime.Now, DateTime.Now) + "," + getSendMailTotal(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(-1)) + "," + getSendMailTotal( DateTime.Now.AddDays(-14), DateTime.Now.AddDays(-8));
            objproduct.TotalReceive = getTotalReceived(userid);
            objproduct.TotalSend = getTotalSend(userid);
           objproduct.userResponse=getResponseTime(userid);
            objproduct.TotalReceiveComWhole = getTotalReceivedCompany();
            objproduct.TotalSendComWhole = getTotalSendCompany();
            return objproduct;
        }
     public string getReceivedMail(string userid,DateTime startDate,DateTime endDate)
        {
         
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConn"]);
            int count = 0;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    con.Open();

                    Debug.WriteLine("USER:" + userid);

                    string str = "select count(*),'Date' as date from ReceivedEmail where  ReceivedDate >= '"+startDate.ToString("yyyy-MM-dd")+ "' AND ReceivedDate<='" + endDate.ToString("yyyy-MM-dd") + "' AND userName like '" + userid+"%'";
                    SqlCommand sqlCmd = new SqlCommand(str, con);
                    SqlDataReader rdr = sqlCmd.ExecuteReader();
                   
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            count = Int32.Parse(rdr[0].ToString());
                        }
                    }
                }
                catch (SqlException ex)
                {


                }
                finally
                {
                    con.Close();
                }

            }
            return count.ToString();
        }
        public string getReceivedMailTotal( DateTime startDate, DateTime endDate)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConn"]);
            int count = 0;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    con.Open();

                  

                    string str = "select count(*),'Date' as date from ReceivedEmail where  ReceivedDate >= '" + startDate.ToString("yyyy-MM-dd") + "' AND ReceivedDate<='" + endDate.ToString("yyyy-MM-dd") + "'";
                    SqlCommand sqlCmd = new SqlCommand(str, con);
                    SqlDataReader rdr = sqlCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            count = Int32.Parse(rdr[0].ToString());
                        }
                    }
                }
                catch (SqlException ex)
                {


                }
                finally
                {
                    con.Close();
                }

            }
            return count.ToString();
        }
        public string getSendMail(string userid, DateTime startDate, DateTime endDate)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConn"]);
            int count = 0;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    con.Open();

                    Debug.WriteLine("USER:" + userid);

                    string str = "select count(*),'Date' as date  from SendEmail where SendEmail.Subject like(select distinct SendEmail.Subject  from ReceivedEmail,SendEmail  where  SendEmail.Subject like 'Re:%'+ReceivedEmail.Subject and SendEmail.userName like '" + userid + "%' and SendEmail.SendDate >= '" + startDate.ToString("yyyy-MM-dd") + "' AND SendEmail.SendDate<='" + endDate.ToString("yyyy-MM-dd") + "' )";
                    SqlCommand sqlCmd = new SqlCommand(str, con);
                    SqlDataReader rdr = sqlCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                           count = Int32.Parse(rdr[0].ToString());
                        }
                    }
                }
                catch (SqlException ex)
                {


                }
                finally
                {
                    con.Close();
                }

            }
            return count.ToString();
        }
        public string getSendMailTotal(DateTime startDate, DateTime endDate)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConn"]);
            int count = 0;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    con.Open();

                  
                    string str = "select count(*),'Date' as date  from SendEmail where SendEmail.Subject like(select distinct SendEmail.Subject  from ReceivedEmail,SendEmail  where  SendEmail.Subject like 'Re:%'+ReceivedEmail.Subject  and SendEmail.SendDate >= '" + startDate.ToString("yyyy-MM-dd") + "' AND SendEmail.SendDate<='" + endDate.ToString("yyyy-MM-dd") + "' )";
                    SqlCommand sqlCmd = new SqlCommand(str, con);
                    SqlDataReader rdr = sqlCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            count = Int32.Parse(rdr[0].ToString());
                        }
                    }
                }
                catch (SqlException ex)
                {


                }
                finally
                {
                    con.Close();
                }

            }
            return count.ToString();
        }

        public string getResponseTime(String userID)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConn"]);
            string data=null;
          
            if (con.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    con.Open();

                    List<String> subject = new List<String>();
                    List<DateTime> send = new List<DateTime>();
                    List<DateTime> receive = new List<DateTime>();
                    string str = "select distinct  SendEmail.subject,concat(SendEmail.SendDate,' ',SendEmail.SendTime) as sendDate from SendEmail,ReceivedEmail where  SendEmail.Subject in (select  distinct SendEmail.Subject  from ReceivedEmail,SendEmail  where  SendEmail.Subject like 'Re:%'+ReceivedEmail.Subject and SendEmail.userName like '" + userID + "') ";
                    SqlCommand sqlCmd = new SqlCommand(str, con);
                    SqlDataReader rdr = sqlCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {

                        while (rdr.Read())
                        {
                            string test = rdr[0].ToString();
                            send.Add(Convert.ToDateTime(rdr[1].ToString()));
                            Debug.WriteLine(test.Substring(4, test.Length - 4));
                            subject.Add(test.Substring(4, test.Length - 4));

                        }
                    }
                    con.Close();
                    
                   foreach(string  sub in subject)
                    {
                        str = "select CONCAT(ReceivedDate,' ', ReceivedTime) from ReceivedEmail where userName like '" + userID + "' and Subject like '%" + sub + "'";
                        sqlCmd = new SqlCommand(str, con);
                        con.Open();
                        rdr = sqlCmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                receive.Add(Convert.ToDateTime(rdr[0].ToString()));
                            }
                        }
                        con.Close();
                    }
                    int index = 0;
                    int zeroto2, twoto4, fourto8, over24, nextday;
                    zeroto2 = twoto4 = fourto8 =over24 = nextday = 0;
                   foreach(DateTime rec in receive)
                    {
                        TimeSpan diff = send[index] - rec;
                        Debug.WriteLine(diff);
                        index++;
                        if (diff.TotalHours >= 0 && diff.TotalHours <= 2)
                            zeroto2++;
                        else if (diff.TotalHours > 2 && diff.TotalHours <= 4)
                            twoto4++;
                        else if (diff.TotalHours > 4 && diff.TotalHours <= 8)
                            fourto8++;
                        else if (diff.TotalHours > 8 && diff.TotalHours <= 24)
                            nextday++;
                        else if (diff.TotalHours > 24)
                            over24++;
                    }
                    switch((zeroto2 + twoto4 + fourto8 + nextday + over24) / 5)
                    {
                        case 1:
                            ViewData["UserResponse"] = "0 to 2  Hour per received Email";
                             break;
                        case 2:
                            ViewData["UserResponse"] = "2 to 4  Hour per received Email";
                            break;
                        case 3:
                            ViewData["UserResponse"] = "4 to 8  Hour per received Email";
                            break;
                        case 4:
                            ViewData["UserResponse"] = "Over 8  Hour per received Email";
                            break;
                        case 5:
                            ViewData["UserResponse"] = "Over 24  Hour per received Email";
                            break;
                        default:
                            ViewData["UserResponse"] = "Not Messured";
                            break;

                    }
                    data = zeroto2.ToString() + "," + twoto4.ToString() + "," + fourto8.ToString() + "," + nextday.ToString() + "," + over24.ToString();
                }
                catch (SqlException ex)
                {


                }
                finally
                {
                    con.Close();
                }

            }
            return data;
        }
        public string getTotalReceived(string userid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConn"]);
            int count = 0;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    con.Open();

                  

                    string str = "select COUNT(*) from ReceivedEmail where userName like '" + userid + "%' ";
                    SqlCommand sqlCmd = new SqlCommand(str, con);
                    SqlDataReader rdr = sqlCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            count = Int32.Parse(rdr[0].ToString());
                        }
                    }
                }
                catch (SqlException ex)
                {


                }
                finally
                {
                    con.Close();
                }

            }
            return count.ToString();
        }
        public string getTotalReceivedCompany()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConn"]);
            int count = 0;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    con.Open();



                    string str = "select COUNT(*) from ReceivedEmail";
                    SqlCommand sqlCmd = new SqlCommand(str, con);
                    SqlDataReader rdr = sqlCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            count = Int32.Parse(rdr[0].ToString());
                        }
                    }
                }
                catch (SqlException ex)
                {


                }
                finally
                {
                    con.Close();
                }

            }
            return count.ToString();
        }
        public string getTotalSend(string userid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConn"]);
            int count = 0;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    con.Open();



                    string str = "select COUNT(*) from SendEmail where Subject like 'Re:%' and userName like '" + userid + "%' ";
                    SqlCommand sqlCmd = new SqlCommand(str, con);
                    SqlDataReader rdr = sqlCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            count = Int32.Parse(rdr[0].ToString());
                        }
                    }
                }
                catch (SqlException ex)
                {


                }
                finally
                {
                    con.Close();
                }

            }
            return count.ToString();
        }


        public string getTotalSendCompany()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConn"]);
            int count = 0;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    con.Open();



                    string str = "select COUNT(*) from SendEmail where Subject like 'Re:%'";
                    SqlCommand sqlCmd = new SqlCommand(str, con);
                    SqlDataReader rdr = sqlCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            count = Int32.Parse(rdr[0].ToString());
                        }
                    }
                }
                catch (SqlException ex)
                {


                }
                finally
                {
                    con.Close();
                }

            }
            return count.ToString();
        }
    }

   
}