using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200061D RID: 1565
	internal class OdbcNativeQueryTableValue : TableValue
	{
		// Token: 0x060030FA RID: 12538 RVA: 0x00094ED0 File Offset: 0x000930D0
		public OdbcNativeQueryTableValue(string dataSourceName, IEngineHost host, OdbcDataSource dataSource, string nativeQuery, string catalog, bool suppressPermissionChallenge = false, IQueryDomain queryDomain = null, IList<OdbcParameter> parameters = null)
		{
			this.dataSourceName = dataSourceName;
			this.host = host;
			this.dataSource = dataSource;
			this.nativeQuery = nativeQuery;
			this.catalog = catalog;
			if (queryDomain == null)
			{
				this.queryDomain = new OdbcNativeQueryTableValue.NativeQueryDomain();
			}
			else
			{
				IQueryDomain queryDomain2;
				if (catalog != null)
				{
					queryDomain2 = ((OdbcQueryDomain)queryDomain).WithCatalog(catalog);
				}
				else
				{
					IQueryDomain queryDomain3 = (OdbcQueryDomain)queryDomain;
					queryDomain2 = queryDomain3;
				}
				this.queryDomain = queryDomain2;
			}
			this.suppressPermissionChallenge = suppressPermissionChallenge;
			this.parameters = parameters ?? EmptyArray<OdbcParameter>.Instance;
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x060030FB RID: 12539 RVA: 0x00094F55 File Offset: 0x00093155
		public override IExpression Expression
		{
			get
			{
				this.EnsurePermission();
				return OdbcNativeQueryExpression.New(this.dataSource.Resource, this.nativeQuery);
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x00094F73 File Offset: 0x00093173
		public override TypeValue Type
		{
			get
			{
				if (this.type == null)
				{
					this.type = OdbcNativeQueryTableValue.GetTableType(TableSchema.FromDataReader(this.GetReaderCore()));
				}
				return this.type;
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x00094F99 File Offset: 0x00093199
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.queryDomain;
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x00094FA1 File Offset: 0x000931A1
		protected string Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x00094FA9 File Offset: 0x000931A9
		protected string DataSourceName
		{
			get
			{
				return this.dataSourceName;
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x00094FB1 File Offset: 0x000931B1
		protected string OdbcNativeQuery
		{
			get
			{
				return this.nativeQuery;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x00094FB9 File Offset: 0x000931B9
		protected OdbcDataSource DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x00094FC1 File Offset: 0x000931C1
		protected IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x00094FC9 File Offset: 0x000931C9
		protected IList<OdbcParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x00094FD1 File Offset: 0x000931D1
		public override void TestConnection()
		{
			this.dataSource.TestConnectionAndGetVersion(this.Catalog);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x00094FE8 File Offset: 0x000931E8
		public override IPageReader GetReader()
		{
			IPageReader pageReader = this.dataSource.ExecutePageReader(this.OdbcNativeQuery, this.Catalog, this.parameters, RowRange.All, null, null);
			if (this.type == null)
			{
				this.type = OdbcNativeQueryTableValue.GetTableType(pageReader.Schema);
			}
			return new ColumnRenamePageReader(pageReader, this.Type.AsTableType.ItemType.Fields.Keys.ToArray<string>());
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x00095058 File Offset: 0x00093258
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			IDataReader dataReader = null;
			IEnumerator<IValueReference> enumerator;
			try
			{
				dataReader = this.GetReaderCore();
				this.reader = null;
				enumerator = new DbDataReaderEnumerator(dataReader, true, this.Type.AsTableType.ItemType, this.dataSourceName, null);
			}
			catch
			{
				if (dataReader != null)
				{
					dataReader.Dispose();
				}
				throw;
			}
			return enumerator;
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000950B4 File Offset: 0x000932B4
		private IDataReader GetReaderCore()
		{
			if (this.reader == null)
			{
				this.EnsurePermission();
				try
				{
					this.reader = this.DataSource.Execute(this.DataSource.Host.GetPersistentCache(), this.OdbcNativeQuery, this.Catalog, this.parameters, RowRange.All, null, false, null);
					if (this.reader.FieldCount == 0)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.NativeQuery_NullSchema, null, null);
					}
				}
				catch
				{
					if (this.reader != null)
					{
						this.reader.Dispose();
						this.reader = null;
					}
					throw;
				}
				this.reader = this.host.RegisterForCleanup(this.reader);
			}
			return this.reader;
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x00095174 File Offset: 0x00093374
		private void EnsurePermission()
		{
			if (!this.suppressPermissionChallenge)
			{
				this.VerifyQueryPermission();
			}
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x00095184 File Offset: 0x00093384
		protected void VerifyQueryPermission()
		{
			HostResourceQueryPermissionService.VerifyQueryPermission(this.Host, this.DataSource.Resource, QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted, this.OdbcNativeQuery);
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000951A4 File Offset: 0x000933A4
		public TableSchema GetSchema()
		{
			TableSchema tableSchema;
			using (IDataReader readerCore = this.GetReaderCore())
			{
				tableSchema = TableSchema.FromDataReader(readerCore);
			}
			return tableSchema;
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x000951DC File Offset: 0x000933DC
		private static TableTypeValue GetTableType(TableSchema schema)
		{
			string[] array = new string[schema.ColumnCount];
			for (int i = 0; i < schema.ColumnCount; i++)
			{
				array[i] = schema.GetColumn(i).Name;
			}
			Keys keys = ColumnLabelGenerator.GenerateKeys(array, array.Length);
			RecordBuilder recordBuilder = new RecordBuilder(schema.ColumnCount);
			for (int j = 0; j < schema.ColumnCount; j++)
			{
				SchemaColumn column = schema.GetColumn(j);
				TypeValue typeValue = OdbcTypeValue.New((Odbc32.SQL_TYPE)column.ProviderType.Value, column.DataTypeName, null, column.NumericBase, column.ColumnSize, column.NumericScale, column.IsUnsigned, new bool?(column.Nullable), null);
				recordBuilder.Add(keys[j], RecordTypeAlgebra.NewField(typeValue, false), TypeValue.Record);
			}
			return TableTypeValue.New(RecordTypeValue.New(recordBuilder.ToRecord()));
		}

		// Token: 0x040015AB RID: 5547
		private readonly IEngineHost host;

		// Token: 0x040015AC RID: 5548
		private readonly OdbcDataSource dataSource;

		// Token: 0x040015AD RID: 5549
		private readonly string nativeQuery;

		// Token: 0x040015AE RID: 5550
		private readonly string dataSourceName;

		// Token: 0x040015AF RID: 5551
		private readonly IQueryDomain queryDomain;

		// Token: 0x040015B0 RID: 5552
		private readonly bool suppressPermissionChallenge;

		// Token: 0x040015B1 RID: 5553
		private readonly string catalog;

		// Token: 0x040015B2 RID: 5554
		private readonly IList<OdbcParameter> parameters;

		// Token: 0x040015B3 RID: 5555
		private IDataReaderWithTableSchema reader;

		// Token: 0x040015B4 RID: 5556
		private TypeValue type;

		// Token: 0x0200061E RID: 1566
		private class NativeQueryDomain : INativeQueryDomain, IQueryDomain
		{
			// Token: 0x170011FD RID: 4605
			// (get) Token: 0x0600310D RID: 12557 RVA: 0x00002105 File Offset: 0x00000305
			public bool CanIndex
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600310E RID: 12558 RVA: 0x000952C1 File Offset: 0x000934C1
			public bool IsCompatibleWith(IQueryDomain domain)
			{
				return domain == this;
			}

			// Token: 0x0600310F RID: 12559 RVA: 0x0000A6A5 File Offset: 0x000088A5
			public Query Optimize(Query query)
			{
				return query;
			}

			// Token: 0x06003110 RID: 12560 RVA: 0x000952C8 File Offset: 0x000934C8
			public bool TryGetNativeQuery(Query query, out IResource resource, out Value nativeQuery, out RecordValue options)
			{
				TableQuery tableQuery = query as TableQuery;
				if (tableQuery != null)
				{
					OdbcNativeQueryTableValue odbcNativeQueryTableValue = tableQuery.Table as OdbcNativeQueryTableValue;
					if (odbcNativeQueryTableValue != null)
					{
						odbcNativeQueryTableValue.EnsurePermission();
						resource = odbcNativeQueryTableValue.dataSource.Resource;
						nativeQuery = TextValue.New(odbcNativeQueryTableValue.nativeQuery);
						options = RecordValue.Empty;
						return true;
					}
				}
				resource = null;
				nativeQuery = null;
				options = RecordValue.Empty;
				return false;
			}
		}
	}
}
