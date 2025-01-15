using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.SemanticQueryEngine;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Oracle;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration.Oracle
{
	// Token: 0x02000009 RID: 9
	[CLSCompliant(false)]
	public sealed class OraSqlDsvStatisticsProvider : SqlDsvStatisticsProvider
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003671 File Offset: 0x00001871
		public OraSqlDsvStatisticsProvider(IDbConnection connection)
			: base(connection)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003F34 File Offset: 0x00002134
		internal static bool IsBlobStatic(DsvColumn column)
		{
			bool? isBlob = column.IsBlob;
			if (isBlob != null)
			{
				return isBlob.Value;
			}
			if (column.DataType == typeof(string))
			{
				return column.MaxLength > 4000 || column.MaxLength < 0;
			}
			return column.DataType == typeof(byte[]);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003FA3 File Offset: 0x000021A3
		protected override bool IsBlob(DsvColumn column)
		{
			return OraSqlDsvStatisticsProvider.IsBlobStatic(column);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003FAB File Offset: 0x000021AB
		protected override DsvCompareInfo GetCompareInfo()
		{
			return OraSqlDsvGenerator.GetCompareInfoStatic(base.Connection);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected override void SetNoLock()
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003FBC File Offset: 0x000021BC
		protected override SqlDsvStatisticsProvider.IColumnIndexesReader GetColumnIndexesReader()
		{
			Microsoft.ReportingServices.Common.Bag<string> bag = new Microsoft.ReportingServices.Common.Bag<string>(base.DBObjectNameComparer);
			foreach (DsvTable dsvTable in base.DataSourceView.Tables)
			{
				if (dsvTable.DbSchemaName != null && !bag.Contains(dsvTable.DbSchemaName))
				{
					bag.Add(dsvTable.DbSchemaName);
				}
			}
			OracleConnection oracleConnection = base.Connection as OracleConnection;
			if (oracleConnection == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Connection must be OracleConnection.", Array.Empty<object>());
			}
			List<DataRow> list = new List<DataRow>();
			foreach (string text in bag)
			{
				DataTable schema = oracleConnection.GetSchema("IndexColumns", new string[] { null, null, text });
				list.AddRange(schema.Select(null, "TABLE_OWNER, TABLE_NAME, INDEX_OWNER, INDEX_NAME, COLUMN_POSITION"));
			}
			return new OraSqlDsvStatisticsProvider.ColumnIndexesReader(list, base.DBObjectNameComparer);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000040D4 File Offset: 0x000022D4
		protected override SqlDsvStatisticsProvider.IGetColumnDbTypeName GetColumnDbTypeNameDictionary()
		{
			SqlDsvStatisticsProvider.IGetColumnDbTypeName getColumnDbTypeName;
			try
			{
				getColumnDbTypeName = new OraSqlDsvGenerator(base.Connection).GenerateColumnsExtendedInfo(base.DataSourceView.CompareInfo);
			}
			catch
			{
				getColumnDbTypeName = null;
			}
			return getColumnDbTypeName;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004118 File Offset: 0x00002318
		protected override string GetDelimitedIdentifier(string name)
		{
			return OraSqlBatch.GetDelimitedIdentifierStatic(name);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004120 File Offset: 0x00002320
		protected override string GetSqlForSelectSampledTableSource(long actualRowCount, long targetRowCount, string tableSource, bool isTable)
		{
			if (isTable)
			{
				return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("(SELECT * FROM {0} SAMPLE({1}) t)", new object[]
				{
					tableSource,
					Math.Min(99f, (float)targetRowCount / (float)actualRowCount * 100f)
				});
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("(SELECT * FROM {0} t WHERE ROWNUM < {1})", new object[] { tableSource, targetRowCount });
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004180 File Offset: 0x00002380
		protected override string GetSqlForStringAvgWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(AVG(LENGTH(RTRIM({0}))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000419C File Offset: 0x0000239C
		protected override string GetSqlForStringStDevWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(STDDEV_SAMP(LENGTH(RTRIM({0}))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000041B8 File Offset: 0x000023B8
		protected override string GetSqlForStringMaxWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(MAX(LENGTH(RTRIM({0}))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000041D4 File Offset: 0x000023D4
		protected override string GetSqlForIntAvgWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(AVG(LENGTH(CAST({0} AS VARCHAR(50)))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000041F0 File Offset: 0x000023F0
		protected override string GetSqlForIntStDevWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(STDDEV_SAMP(LENGTH(CAST({0} AS VARCHAR(50)))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000420C File Offset: 0x0000240C
		protected override string GetSqlForIntMaxWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(MAX(LENGTH(CAST({0} AS VARCHAR(50)))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004228 File Offset: 0x00002428
		protected override string GetSqlForRealAvgWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(AVG(LENGTH(CAST(TRUNC({0}) AS VARCHAR(50)))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004244 File Offset: 0x00002444
		protected override string GetSqlForRealStDevWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(STDDEV_SAMP(LENGTH(CAST(TRUNC({0}) AS VARCHAR(50)))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004260 File Offset: 0x00002460
		protected override string GetSqlForRealMaxWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(MAX(LENGTH(CAST(TRUNC({0}) AS VARCHAR(50)))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000427C File Offset: 0x0000247C
		protected override string GetSqlForRealAvgScaleExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(AVG(LENGTH(REPLACE(RTRIM(CAST({0} - TRUNC({0}) AS VARCHAR(50)), '0'), '.', ''))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004298 File Offset: 0x00002498
		protected override string GetSqlForRealStDevScaleExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(STDDEV_SAMP(LENGTH(REPLACE(RTRIM(CAST({0} - TRUNC({0}) AS VARCHAR(50)), '0'), '.', ''))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000042B4 File Offset: 0x000024B4
		protected override string GetSqlForRealMaxScaleExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(MAX(LENGTH(REPLACE(RTRIM(CAST({0} - TRUNC({0}) AS VARCHAR(50)), '0'), '.', ''))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x0200009A RID: 154
		private sealed class ColumnIndexesReader : SqlDsvStatisticsProvider.IColumnIndexesReader, IDisposable
		{
			// Token: 0x060005E4 RID: 1508 RVA: 0x00018443 File Offset: 0x00016643
			internal ColumnIndexesReader(List<DataRow> indexes, IEqualityComparer<string> dbObjectNameComparer)
			{
				this.m_reader = indexes.GetEnumerator();
				this.m_dbObjectNameComparer = dbObjectNameComparer;
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x0001847C File Offset: 0x0001667C
			bool SqlDsvStatisticsProvider.IColumnIndexesReader.Read()
			{
				if (this.m_firstRow)
				{
					this.m_EOF = !this.m_reader.MoveNext();
					this.m_firstRow = false;
				}
				if (this.m_EOF)
				{
					return false;
				}
				this.m_tableSchemaName = this.GetString("TABLE_OWNER");
				this.m_tableName = this.GetString("TABLE_NAME");
				this.m_indexSchemaName = this.GetString("INDEX_OWNER");
				this.m_indexName = this.GetString("INDEX_NAME");
				this.m_indexColumns.Clear();
				do
				{
					this.m_indexColumns.Add(this.GetString("COLUMN_NAME"));
					this.m_EOF = !this.m_reader.MoveNext();
				}
				while (!this.m_EOF && this.m_dbObjectNameComparer.Equals(this.m_tableSchemaName, this.GetString("TABLE_OWNER")) && this.m_dbObjectNameComparer.Equals(this.m_tableName, this.GetString("TABLE_NAME")) && this.m_dbObjectNameComparer.Equals(this.m_indexSchemaName, this.GetString("INDEX_OWNER")) && this.m_dbObjectNameComparer.Equals(this.m_indexName, this.GetString("INDEX_NAME")));
				return true;
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000185B3 File Offset: 0x000167B3
			string SqlDsvStatisticsProvider.IColumnIndexesReader.IndexSchemaName
			{
				get
				{
					return this.m_indexSchemaName;
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000185BB File Offset: 0x000167BB
			string SqlDsvStatisticsProvider.IColumnIndexesReader.IndexName
			{
				get
				{
					return this.m_indexName;
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000185C3 File Offset: 0x000167C3
			string SqlDsvStatisticsProvider.IColumnIndexesReader.TableSchemaName
			{
				get
				{
					return this.m_tableSchemaName;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x060005E9 RID: 1513 RVA: 0x000185CB File Offset: 0x000167CB
			string SqlDsvStatisticsProvider.IColumnIndexesReader.TableName
			{
				get
				{
					return this.m_tableName;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x060005EA RID: 1514 RVA: 0x000185D3 File Offset: 0x000167D3
			IList<string> SqlDsvStatisticsProvider.IColumnIndexesReader.IndexColumns
			{
				get
				{
					return this.m_indexColumns;
				}
			}

			// Token: 0x060005EB RID: 1515 RVA: 0x000185DB File Offset: 0x000167DB
			void IDisposable.Dispose()
			{
				this.m_reader.Dispose();
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x000185E8 File Offset: 0x000167E8
			private string GetString(string columnName)
			{
				if (this.m_reader.Current.IsNull(columnName))
				{
					return null;
				}
				return (string)this.m_reader.Current[columnName];
			}

			// Token: 0x040002A3 RID: 675
			private readonly IEnumerator<DataRow> m_reader;

			// Token: 0x040002A4 RID: 676
			private readonly IEqualityComparer<string> m_dbObjectNameComparer;

			// Token: 0x040002A5 RID: 677
			private bool m_firstRow = true;

			// Token: 0x040002A6 RID: 678
			private bool m_EOF = true;

			// Token: 0x040002A7 RID: 679
			private string m_tableSchemaName;

			// Token: 0x040002A8 RID: 680
			private string m_tableName;

			// Token: 0x040002A9 RID: 681
			private string m_indexSchemaName;

			// Token: 0x040002AA RID: 682
			private string m_indexName;

			// Token: 0x040002AB RID: 683
			private readonly List<string> m_indexColumns = new List<string>();
		}
	}
}
