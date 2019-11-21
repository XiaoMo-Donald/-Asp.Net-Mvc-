using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Second.Website.Model;
using System.Data.Entity;

namespace Second.Website.DAL
{
   public class DBDal
    {
        SecondWebsiteDB db = new SecondWebsiteDB();
        public bool CreateDatabase()
        {
            if (db.Database.CreateIfNotExists())
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }


    }
}
