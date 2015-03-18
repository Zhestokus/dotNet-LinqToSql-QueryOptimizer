﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LinqQueryOptimizerConsoleApp.DAL
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="AdventureWorks2008R2")]
	public partial class AdventureWorksDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public AdventureWorksDataContext() : 
				base(global::LinqQueryOptimizerConsoleApp.Properties.Settings.Default.AdventureWorks2008R2ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public AdventureWorksDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AdventureWorksDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AdventureWorksDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AdventureWorksDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Customer> Customers
		{
			get
			{
				return this.GetTable<Customer>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="Sales.Customer")]
	public partial class Customer
	{
		
		private int _CustomerID;
		
		private System.Nullable<int> _PersonID;
		
		private System.Nullable<int> _StoreID;
		
		private System.Nullable<int> _TerritoryID;
		
		private string _AccountNumber;
		
		private System.Guid _rowguid;
		
		private System.DateTime _ModifiedDate;
		
		public Customer()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerID", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int CustomerID
		{
			get
			{
				return this._CustomerID;
			}
			set
			{
				if ((this._CustomerID != value))
				{
					this._CustomerID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PersonID", DbType="Int")]
		public System.Nullable<int> PersonID
		{
			get
			{
				return this._PersonID;
			}
			set
			{
				if ((this._PersonID != value))
				{
					this._PersonID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StoreID", DbType="Int")]
		public System.Nullable<int> StoreID
		{
			get
			{
				return this._StoreID;
			}
			set
			{
				if ((this._StoreID != value))
				{
					this._StoreID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TerritoryID", DbType="Int")]
		public System.Nullable<int> TerritoryID
		{
			get
			{
				return this._TerritoryID;
			}
			set
			{
				if ((this._TerritoryID != value))
				{
					this._TerritoryID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccountNumber", AutoSync=AutoSync.Always, DbType="VarChar(10) NOT NULL", CanBeNull=false, IsDbGenerated=true, UpdateCheck=UpdateCheck.Never)]
		public string AccountNumber
		{
			get
			{
				return this._AccountNumber;
			}
			set
			{
				if ((this._AccountNumber != value))
				{
					this._AccountNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_rowguid", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid rowguid
		{
			get
			{
				return this._rowguid;
			}
			set
			{
				if ((this._rowguid != value))
				{
					this._rowguid = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ModifiedDate", DbType="DateTime NOT NULL")]
		public System.DateTime ModifiedDate
		{
			get
			{
				return this._ModifiedDate;
			}
			set
			{
				if ((this._ModifiedDate != value))
				{
					this._ModifiedDate = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
