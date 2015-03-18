using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using LinqQueryOptimizerConsoleApp.DAL;

namespace LinqQueryOptimizerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AdventureWorksDataContext())
            {
                var customersIdList = db.Customers.Select(n => n.CustomerID);
                var customersIdSet = new HashSet<int>(customersIdList);

                //Classic LINQ TO SQL Query
                var query = from n in db.Customers
                            where Enumerable.Contains(customersIdSet, n.CustomerID)
                            select n;

                //Classic LINQ TO SQL Query OPTIMIZER
                var optimizer = new LinqQueryOptimizer(db);
                var customers = optimizer.ExecuteQuery(query).ToList();
            }
        }
    }
}
