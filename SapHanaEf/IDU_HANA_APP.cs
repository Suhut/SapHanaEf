using Sap.Data.Hana;
using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SapHanaEf

{
    //implementasi
    //https://stackoverflow.com/questions/11602409/execute-as-when-using-a-dbcontext-with-linq 

    public class IDU_HANA_APP
    {
        private static ConcurrentDictionary<Tuple<string, string>, DbCompiledModel> modelCache = new ConcurrentDictionary<Tuple<string, string>, DbCompiledModel>();

        public static Entities1 Create()
        {
            string schemaName = "SUHUT_DATAACCESS";
            DbCompiledModel compiledModel; 

            string connString = ConfigurationManager.ConnectionStrings["Model1"].ToString(); 

            using (var connection = new HanaConnection(connString))
            {
                //connection.Open();
                compiledModel = modelCache.GetOrAdd(
             Tuple.Create(connection.ConnectionString, schemaName),
             t =>
             {
                 var builder = new DbModelBuilder();
                 //builder.Conventions.Remove<PluralizingTableNameConvention>();
                 //builder.Conventions.Remove<ForeignKeyIndexConvention>();
                 builder = IduEfConfigurations.SetConfigurations(builder, schemaName);

                 var model = builder.Build(connection);
                 return model.Compile();
             });
                return new Entities1(connString, compiledModel);
            }

        }



    }


}
