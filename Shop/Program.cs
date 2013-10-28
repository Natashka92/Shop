using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.IO;
using System.IO.Compression;


namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {            
            using(var db = new DataContext("DBShop"))
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                using(var reader = new DataReader(@"..\..\Data\div_tech.xml"))
                {
                    var dataMover = new DataMover(db);
                    dataMover.LoadData(reader);
                }
            }
        }
    }
}
