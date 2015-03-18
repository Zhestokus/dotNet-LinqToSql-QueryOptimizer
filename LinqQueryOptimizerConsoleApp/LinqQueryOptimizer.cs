using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace LinqQueryOptimizerConsoleApp
{
    public class LinqQueryOptimizer
    {
        private const String _sqlBatch = " IN (SELECT node.value('.', '{0}') FROM @{1}.nodes('/Val') {1}(node))";

        private readonly Regex _mainRegex;
        private readonly Regex _paramsRegex;
        private readonly DataContext _dataContext;

        public LinqQueryOptimizer(DataContext dataContext)
        {
            _dataContext = dataContext;

            _mainRegex = new Regex(@" IN \((?<params>.*?)\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _paramsRegex = new Regex(@"(?<name>@p\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(IQueryable<TEntity> query)
        {
            var dbCommand = _dataContext.GetCommand(query);
            Optimize(dbCommand);

            var reader = dbCommand.ExecuteReader();

            var entities = _dataContext.Translate<TEntity>(reader);
            return entities;
        }

        private void Optimize(DbCommand command)
        {
            command.CommandText = _mainRegex.Replace(command.CommandText, m => ParamsReplacer(m, command));
        }

        private String ParamsReplacer(Match match, DbCommand command)
        {
            var allParams = match.Groups["params"].Value;
            if (!_paramsRegex.IsMatch(allParams))
            {
                return allParams;
            }

            var xmlDoc = new XmlDocument();

            var itemsElem = xmlDoc.CreateElement("Items");
            xmlDoc.AppendChild(itemsElem);

            var dbTypes = new List<DbType>();

            var paramsMatches = _paramsRegex.Matches(allParams);
            foreach (Match paramsMatch in paramsMatches)
            {
                var name = paramsMatch.Groups["name"].Value;

                var dbParam = command.Parameters[name];
                var dbValue = dbParam.Value;

                var valElem = xmlDoc.CreateElement("Val");
                valElem.InnerText = Convert.ToString(dbValue);

                itemsElem.AppendChild(valElem);

                if (dbTypes.Count == 0)
                {
                    dbTypes.Add(dbParam.DbType);
                }
                else
                {
                    var index = dbTypes.BinarySearch(dbParam.DbType);
                    if (index < 0)
                    {
                        throw new Exception("More then one type parameters");
                    }
                }

                command.Parameters.Remove(dbParam);
            }

            var hash = (uint)Guid.NewGuid().GetHashCode();

            var xmlName = String.Format("xml_{0}", hash);
            var paramName = String.Format("@{0}", xmlName);

            var xmlParam = command.CreateParameter();
            xmlParam.ParameterName = paramName;
            xmlParam.DbType = DbType.Xml;
            xmlParam.Value = GetXmlText(xmlDoc);

            command.Parameters.Add(xmlParam);

            var typeName = GetSqlTypeName(dbTypes[0]);

            var replaceText = String.Format(_sqlBatch, typeName, xmlName);
            return replaceText;
        }

        private String GetXmlText(XmlDocument xmlDoc)
        {
            if (xmlDoc == null)
                return null;

            if (xmlDoc.DocumentElement == null)
                return xmlDoc.InnerXml;

            return xmlDoc.DocumentElement.InnerXml;
        }

        private String GetSqlTypeName(DbType dbType)
        {
            switch (dbType)
            {
                case DbType.AnsiString:
                    return "VARCHAR(MAX)";

                case DbType.AnsiStringFixedLength:
                    return "CHAR(MAX)";

                case DbType.Binary:
                    return "VARBINARY(MAX)";

                case DbType.Boolean:
                    return "BIT";

                case DbType.SByte:
                case DbType.Byte:
                    return "TINYINT";

                case DbType.Currency:
                    return "MONEY";

                case DbType.Date:
                    return "DATE";

                case DbType.DateTime:
                    return "DATETIME";

                case DbType.DateTime2:
                    return "DATETIME2";

                case DbType.DateTimeOffset:
                    return "DATETIMEOFFSET";

                case DbType.Decimal:
                    return "DECIMAL(18, 0)";

                case DbType.Double:
                    return "FLOAT";

                case DbType.Guid:
                    return "UNIQUEIDENTIFIER";

                case DbType.UInt16:
                case DbType.Int16:
                    return "SMALLINT";

                case DbType.UInt32:
                case DbType.Int32:
                    return "INT";

                case DbType.UInt64:
                case DbType.Int64:
                    return "BITINT";

                case DbType.Object:
                    return "SQL_VARIANT";

                case DbType.Single:
                    return "REAL";

                case DbType.String:
                    return "NVARCHAR(MAX)";

                case DbType.StringFixedLength:
                    return "NCHAR(MAX)";

                case DbType.Time:
                    return "TIME";

                case DbType.Xml:
                    return "XML";
            }

            throw new Exception();
        }
    }
}
