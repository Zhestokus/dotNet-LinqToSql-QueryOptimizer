# dotNet-LinqToSql-QueryOptimizer

Simple LINQ TO SQL Query optimizer
main problem why SQL Server DBAs don't like Linq To SQL is very ugly and not optimized T-SQL queries,
especially when developer use .Contains method (IN keyword in T-SQL)

e.g for Linq To SQL query provided below

                var query = from n in db.Customers
                            where Enumerable.Contains(customersIdSet, n.CustomerID)
                            select n;

classic Linq To SQL generates following T-SQL query

                SELECT [t0].[CustomerID], 
                       [t0].[PersonID], 
                       [t0].[StoreID],
                       [t0].[TerritoryID], 
                       [t0].[AccountNumber], 
                       [t0].[rowguid], 
                       [t0].[ModifiedDate]
                FROM [Sales].[Customer] AS [t0]
                WHERE [t0].[CustomerID] IN (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)

but Linq To SQL Query Optimizer translates query above to folowing

                DECLARE @xml_850030529 XML= '<Val>0</Val>
                                             <Val>1</Val>
                                             <Val>2</Val>
                                             <Val>3</Val>
                                             <Val>4</Val>
                                             <Val>5</Val>
                                             <Val>6</Val>
                                             <Val>7</Val>
                                             <Val>8</Val>
                                             <Val>9</Val>'
                
                SELECT [t0].[CustomerID], 
                       [t0].[PersonID], 
                       [t0].[StoreID], 
                       [t0].[TerritoryID], 
                       [t0].[AccountNumber], 
                       [t0].[rowguid], 
                       [t0].[ModifiedDate]
                FROM [Sales].[Customer] AS [t0]
                WHERE [t0].[CustomerID] IN (SELECT node.value('.', 'INT') 
                                            FROM @xml_850030529.nodes('/Val') xml_850030529(node))

but Linq To SQL Query Optimizer translates query above to following
in this case count of SQL query parameters does not change if count of items will be changed in XML
and because of why you always will have pretty nice Execution Plan and without Limitation count of parameters
(2000 - max count of SQL query parameters)
