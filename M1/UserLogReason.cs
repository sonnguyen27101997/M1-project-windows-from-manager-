using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M1.Model;
namespace M1
{
    public class UserLogReason
    {
        public  session1Entities db = new session1Entities();
        public DateTime timelogin { get; set; }
        public  DateTime timelogout { get; set; }
        
        public void insertUserLog(string email)
        {

            try
            {
                UserLog ulg = new UserLog();
                ulg.Email = email;
                ulg.TimeLogin = timelogin;
                ulg.TimeLogout = timelogout;

                db.UserLogs.Add(ulg);
                db.SaveChanges();
            }
            catch(Exception ex)
            {

            }
            
        }
        public  void insertUserCrash(DateTime loginTime, DateTime logoutTime, string email, bool typeCrash, string describ)
        {

            try
            {
                UserLog ulg = new UserLog();
                ulg.Email = email;
                ulg.TimeLogin = loginTime;
                ulg.TimeLogout = logoutTime;
                ulg.TypeError = typeCrash;
                ulg.Describ = describ;
                db.UserLogs.Add(ulg);
                db.SaveChanges();
               
            }
            catch (Exception ex)
            {

            }

        }

    }
}
