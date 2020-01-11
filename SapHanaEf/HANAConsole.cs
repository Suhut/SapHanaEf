using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sap.Data.Hana;

namespace SapHanaEf
{
    public class HANAConsole
    {
        static void Main(string[] args)
        {
            //testInsertEf(); 
            testInsertEfOnTheFlyConn();
        }

        static void testInsertEfOnTheFlyConn()
        {
            using (var context = IDU_HANA_APP.Create())
            {
                var tm_Master01 = new Tm_Master01();
                tm_Master01.Description = "ini di insert dari EF on the flay conn";
                context.Tm_Master01.Add(tm_Master01);
                context.SaveChanges();
            } 
        }

        //static void testInsertEf()
        //{
        //    using (var context = new Entities1())
        //    {
        //        var tm_Master01 = new Tm_Master01();
        //        tm_Master01.Description = "ini di insert dari EF";
        //        context.Tm_Master01.Add(tm_Master01);
        //        context.SaveChanges();
        //    }
        //}

        static void testKoneksiBiasa()
        {
            HanaConnection conn = new HanaConnection("Server=$userHost$:$userPort$;UserName=$userName$;Password=$userPassword$");
            conn.Open();
            HanaCommand cmd = new HanaCommand("select 'Hello, World' from DUMMY", conn); // Exception
            try
            {
                HanaDataReader reader = cmd.ExecuteReader();
                reader.Read();
                Console.WriteLine(reader.GetString(0));
            }
            catch (Exception e)
            {
                String errorMsg = e.ToString();
                Console.WriteLine(errorMsg);
            }
        }
    }
}
