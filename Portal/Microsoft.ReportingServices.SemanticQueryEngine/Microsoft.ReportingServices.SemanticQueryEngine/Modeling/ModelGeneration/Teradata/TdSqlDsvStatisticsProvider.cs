using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.SemanticQueryEngine;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Teradata;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration.Teradata
{
	// Token: 0x02000007 RID: 7
	[CLSCompliant(false)]
	public sealed class TdSqlDsvStatisticsProvider : SqlDsvStatisticsProvider
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00003671 File Offset: 0x00001871
		public TdSqlDsvStatisticsProvider(IDbConnection connection)
			: base(connection)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000367C File Offset: 0x0000187C
		internal static bool IsBlobStatic(DsvColumn column)
		{
			bool? isBlob = column.IsBlob;
			if (isBlob != null)
			{
				return isBlob.Value;
			}
			if (column.DbDataType.Equals("CLOB", StringComparison.OrdinalIgnoreCase) || column.DbDataType.Equals("NCLOB", StringComparison.OrdinalIgnoreCase) || column.DbDataType.Equals("BLOB", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (column.DataType == typeof(string))
			{
				return column.MaxLength > 4000 || column.MaxLength < 0;
			}
			return column.DataType == typeof(byte[]);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003726 File Offset: 0x00001926
		protected override bool IsBlob(DsvColumn column)
		{
			return TdSqlDsvStatisticsProvider.IsBlobStatic(column);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000372E File Offset: 0x0000192E
		protected override DsvCompareInfo GetCompareInfo()
		{
			return TdSqlDsvGenerator.GetCompareInfoStatic(this.Connection);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000373C File Offset: 0x0000193C
		protected override void SetNoLock()
		{
			IDbCommand dbCommand = null;
			try
			{
				dbCommand = base.CreateCommand("SET SESSION CHARACTERISTICS AS TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
				dbCommand.ExecuteNonQuery();
			}
			finally
			{
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000377C File Offset: 0x0000197C
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
			Type tdConnectionType = TdSqlDsvGenerator.GetTdConnectionType();
			if (this.Connection == null || this.Connection.GetType() != tdConnectionType)
			{
				throw SQEAssert.AssertFalseAndThrow("Connection must be TdConnection.", Array.Empty<object>());
			}
			List<DataRow> list = new List<DataRow>();
			foreach (string text in bag)
			{
				DataTable schema = this.Connection.GetSchema("Indexes", new string[] { text });
				list.AddRange(schema.Select(null, "TABLE_SCHEMA, TABLE_NAME, INDEX_SCHEMA, INDEX_NAME, ORDINAL_POSITION"));
			}
			return new TdSqlDsvStatisticsProvider.ColumnIndexesReader(list, base.DBObjectNameComparer);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000038A8 File Offset: 0x00001AA8
		protected override SqlDsvStatisticsProvider.IGetColumnDbTypeName GetColumnDbTypeNameDictionary()
		{
			SqlDsvStatisticsProvider.IGetColumnDbTypeName getColumnDbTypeName;
			try
			{
				getColumnDbTypeName = new TdSqlDsvGenerator(this.Connection).GenerateColumnsExtendedInfo(base.DataSourceView.CompareInfo);
			}
			catch
			{
				getColumnDbTypeName = null;
			}
			return getColumnDbTypeName;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000038EC File Offset: 0x00001AEC
		protected override string GetDelimitedIdentifier(string name)
		{
			return TdSqlBatch.GetDelimitedIdentifierStatic(name);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000038F4 File Offset: 0x00001AF4
		protected override string GetSqlForSelectSampledTableSource(long actualRowCount, long targetRowCount, string tableSource, bool isTable)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("(SELECT * FROM {0} SAMPLE {1})", new object[] { tableSource, targetRowCount });
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003914 File Offset: 0x00001B14
		protected override string GetSqlForStringAvgWidthExpression(DsvColumn column)
		{
			string text = base.GetSqlForColumnSelectExpression(column);
			if (column.MaxLength > 0)
			{
				text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant(" CAST({0} AS VARCHAR({1})) ", new object[] { text, column.MaxLength });
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(AVG(CHARACTER_LENGTH(TRIM(TRAILING FROM {0} ))) AS DECIMAL(28, 10))", new object[] { text });
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000396C File Offset: 0x00001B6C
		protected override string GetSqlForStringStDevWidthExpression(DsvColumn column)
		{
			string text = base.GetSqlForColumnSelectExpression(column);
			if (column.MaxLength > 0)
			{
				text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant(" CAST({0} AS VARCHAR({1})) ", new object[] { text, column.MaxLength });
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(STDDEV_SAMP(CHARACTER_LENGTH(TRIM(TRAILING FROM {0} ))) AS DECIMAL(28, 10))", new object[] { text });
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000039C4 File Offset: 0x00001BC4
		protected override string GetSqlForStringMaxWidthExpression(DsvColumn column)
		{
			string text = base.GetSqlForColumnSelectExpression(column);
			if (column.MaxLength > 0)
			{
				text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant(" CAST({0} AS VARCHAR({1})) ", new object[] { text, column.MaxLength });
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(MAX(CHARACTER_LENGTH(TRIM(TRAILING FROM {0} ))) AS DECIMAL(28, 10))", new object[] { text });
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003A1C File Offset: 0x00001C1C
		protected override string GetSqlForIntAvgWidthExpression(DsvColumn column)
		{
			string text;
			if (column.DbDataType == "UDT")
			{
				if (column.DataType == typeof(long))
				{
					text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST({0} AS BIGINT)", new object[] { base.GetSqlForColumnSelectExpression(column) });
				}
				else
				{
					text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST({0} AS INTEGER)", new object[] { base.GetSqlForColumnSelectExpression(column) });
				}
			}
			else
			{
				text = base.GetSqlForColumnSelectExpression(column);
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(AVG(CHARACTER_LENGTH(CAST({0} AS VARCHAR(50)))) AS DECIMAL(28, 10))", new object[] { text });
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003AA8 File Offset: 0x00001CA8
		protected override string GetSqlForIntStDevWidthExpression(DsvColumn column)
		{
			string text;
			if (column.DbDataType == "UDT")
			{
				if (column.DataType == typeof(long))
				{
					text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST({0} AS BIGINT)", new object[] { base.GetSqlForColumnSelectExpression(column) });
				}
				else
				{
					text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST({0} AS INTEGER)", new object[] { base.GetSqlForColumnSelectExpression(column) });
				}
			}
			else
			{
				text = base.GetSqlForColumnSelectExpression(column);
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(STDDEV_SAMP(CHARACTER_LENGTH(CAST({0} AS VARCHAR(50)))) AS DECIMAL(28, 10))", new object[] { text });
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003B34 File Offset: 0x00001D34
		protected override string GetSqlForIntMaxWidthExpression(DsvColumn column)
		{
			string text;
			if (column.DbDataType == "UDT")
			{
				if (column.DataType == typeof(long))
				{
					text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST({0} AS BIGINT)", new object[] { base.GetSqlForColumnSelectExpression(column) });
				}
				else
				{
					text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST({0} AS INTEGER)", new object[] { base.GetSqlForColumnSelectExpression(column) });
				}
			}
			else
			{
				text = base.GetSqlForColumnSelectExpression(column);
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(MAX(CHARACTER_LENGTH(CAST({0} AS VARCHAR(50)))) AS DECIMAL(28, 10))", new object[] { text });
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003BC0 File Offset: 0x00001DC0
		protected override string GetSqlForRealAvgWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(AVG(CHARACTER_LENGTH(CAST(CAST({0} AS DECIMAL(38,0)) AS VARCHAR(50)))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003BDC File Offset: 0x00001DDC
		protected override string GetSqlForRealStDevWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(STDDEV_SAMP(CHARACTER_LENGTH(CAST(CAST({0} AS DECIMAL(38,0)) AS VARCHAR(50)))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003BF8 File Offset: 0x00001DF8
		protected override string GetSqlForRealMaxWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(MAX(CHARACTER_LENGTH(CAST(CAST({0} AS DECIMAL(38,0)) AS VARCHAR(50)))) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003C14 File Offset: 0x00001E14
		protected override string GetSqlForRealAvgScaleExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(AVG(CHARACTER_LENGTH(TRIM(BOTH '0' FROM CAST(ABS({0} - CAST({0} AS DECIMAL(38,0))) AS VARCHAR(50)))) - 1) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003C30 File Offset: 0x00001E30
		protected override string GetSqlForRealStDevScaleExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(STDDEV_SAMP(CHARACTER_LENGTH(TRIM(BOTH '0' FROM CAST(ABS({0} - CAST({0} AS DECIMAL(38,0))) AS VARCHAR(50)))) - 1) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003C4C File Offset: 0x00001E4C
		protected override string GetSqlForRealMaxScaleExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST(MAX(CHARACTER_LENGTH(TRIM(BOTH '0' FROM CAST(ABS({0} - CAST({0} AS DECIMAL(38,0))) AS VARCHAR(50)))) - 1) AS DECIMAL(28, 10))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003C68 File Offset: 0x00001E68
		private new DbConnection Connection
		{
			[DebuggerStepThrough]
			get
			{
				return (DbConnection)base.Connection;
			}
		}

		// Token: 0x02000092 RID: 146
		private sealed class ColumnIndexesReader : SqlDsvStatisticsProvider.IColumnIndexesReader, IDisposable
		{
			// Token: 0x060005A8 RID: 1448 RVA: 0x0001776B File Offset: 0x0001596B
			internal ColumnIndexesReader(List<DataRow> indexes, IEqualityComparer<string> dbObjectNameComparer)
			{
				this.m_reader = indexes.GetEnumerator();
				this.m_dbObjectNameComparer = dbObjectNameComparer;
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x000177A4 File Offset: 0x000159A4
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
				this.m_tableSchemaName = this.m_reader.Current["TABLE_SCHEMA"].ToString();
				this.m_tableName = this.m_reader.Current["TABLE_NAME"].ToString();
				this.m_indexSchemaName = this.m_reader.Current["INDEX_SCHEMA"].ToString();
				this.m_indexName = this.m_reader.Current["INDEX_NAME"].ToString();
				this.m_indexColumns.Clear();
				do
				{
					this.m_indexColumns.Add(this.m_reader.Current["COLUMN_NAME"].ToString());
					this.m_EOF = !this.m_reader.MoveNext();
				}
				while (!this.m_EOF && this.m_dbObjectNameComparer.Equals(this.m_tableSchemaName, this.m_reader.Current["TABLE_SCHEMA"].ToString()) && this.m_dbObjectNameComparer.Equals(this.m_tableName, this.m_reader.Current["TABLE_NAME"].ToString()) && this.m_dbObjectNameComparer.Equals(this.m_indexSchemaName, this.m_reader.Current["INDEX_SCHEMA"].ToString()) && this.m_dbObjectNameComparer.Equals(this.m_indexName, this.m_reader.Current["INDEX_NAME"].ToString()));
				return true;
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x060005AA RID: 1450 RVA: 0x00017968 File Offset: 0x00015B68
			string SqlDsvStatisticsProvider.IColumnIndexesReader.IndexSchemaName
			{
				get
				{
					return this.m_indexSchemaName;
				}
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x060005AB RID: 1451 RVA: 0x00017970 File Offset: 0x00015B70
			string SqlDsvStatisticsProvider.IColumnIndexesReader.IndexName
			{
				get
				{
					return this.m_indexName;
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x060005AC RID: 1452 RVA: 0x00017978 File Offset: 0x00015B78
			string SqlDsvStatisticsProvider.IColumnIndexesReader.TableSchemaName
			{
				get
				{
					return this.m_tableSchemaName;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x060005AD RID: 1453 RVA: 0x00017980 File Offset: 0x00015B80
			string SqlDsvStatisticsProvider.IColumnIndexesReader.TableName
			{
				get
				{
					return this.m_tableName;
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x060005AE RID: 1454 RVA: 0x00017988 File Offset: 0x00015B88
			IList<string> SqlDsvStatisticsProvider.IColumnIndexesReader.IndexColumns
			{
				get
				{
					return this.m_indexColumns;
				}
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x00017990 File Offset: 0x00015B90
			void IDisposable.Dispose()
			{
				this.m_reader.Dispose();
			}

			// Token: 0x04000278 RID: 632
			private readonly IEnumerator<DataRow> m_reader;

			// Token: 0x04000279 RID: 633
			private readonly IEqualityComparer<string> m_dbObjectNameComparer;

			// Token: 0x0400027A RID: 634
			private bool m_firstRow = true;

			// Token: 0x0400027B RID: 635
			private bool m_EOF = true;

			// Token: 0x0400027C RID: 636
			private string m_tableSchemaName;

			// Token: 0x0400027D RID: 637
			private string m_tableName;

			// Token: 0x0400027E RID: 638
			private string m_indexSchemaName;

			// Token: 0x0400027F RID: 639
			private string m_indexName;

			// Token: 0x04000280 RID: 640
			private readonly List<string> m_indexColumns = new List<string>();
		}
	}
}
