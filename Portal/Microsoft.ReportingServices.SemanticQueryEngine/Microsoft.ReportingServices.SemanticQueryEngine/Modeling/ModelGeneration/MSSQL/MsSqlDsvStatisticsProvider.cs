using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.SemanticQueryEngine;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQL;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration.MSSQL
{
	// Token: 0x0200000D RID: 13
	[CLSCompliant(false)]
	public class MsSqlDsvStatisticsProvider : SqlDsvStatisticsProvider
	{
		// Token: 0x0600009D RID: 157 RVA: 0x0000476A File Offset: 0x0000296A
		public MsSqlDsvStatisticsProvider(IDbConnection connection)
			: base(connection)
		{
			if (this.IsSql && !(connection is SqlConnection))
			{
				throw SQEAssert.AssertFalseAndThrow("Connection must be SqlConnection.", Array.Empty<object>());
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004794 File Offset: 0x00002994
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
			if (column.DataType == typeof(byte[]))
			{
				int num;
				if (!int.TryParse(column.GetPropertyValue("DataSize"), out num))
				{
					num = -1;
				}
				return num > 8000 || num < 0;
			}
			return false;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004826 File Offset: 0x00002A26
		protected override bool IsBlob(DsvColumn column)
		{
			return MsSqlDsvStatisticsProvider.IsBlobStatic(column);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000482E File Offset: 0x00002A2E
		protected override DsvCompareInfo GetCompareInfo()
		{
			return MsSqlDsvGenerator.GetCompareInfoStatic(this.Connection);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000483C File Offset: 0x00002A3C
		protected override void SetNoLock()
		{
			IDbCommand dbCommand = null;
			try
			{
				dbCommand = base.CreateCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
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

		// Token: 0x060000A2 RID: 162 RVA: 0x0000487C File Offset: 0x00002A7C
		protected override SqlDsvStatisticsProvider.IColumnIndexesReader GetColumnIndexesReader()
		{
			IDbCommand dbCommand = null;
			IDataReader dataReader = null;
			SqlDsvStatisticsProvider.IColumnIndexesReader columnIndexesReader = null;
			string text = (this.IsServerMajorVersionAtLeast(9) ? "SELECT s.name AS SchemaName,\r\n       o.name AS TableName,\r\n       x.name AS IndexName,\r\n       c.name AS ColumnName\r\nFROM sys.objects o\r\n\tINNER JOIN sys.schemas s ON s.schema_id = o.schema_id\r\n\tINNER JOIN sys.indexes x ON x.object_id = o.object_id\r\n\tINNER JOIN sys.index_columns k ON k.object_id = o.object_id AND k.index_id = x.index_id\r\n\tINNER JOIN sys.columns c ON c.object_id = o.object_id AND c.column_id = k.column_id\r\nORDER BY SchemaName, TableName, IndexName" : "SELECT u.name AS SchemaName,\r\n       o.name AS TableName,\r\n       x.name AS IndexName,\r\n       c.name AS ColumnName\r\nFROM sysobjects o\r\n   INNER JOIN sysusers u ON u.uid = o.uid\r\n   INNER JOIN sysindexes x ON x.id = o.id\r\n   INNER JOIN sysindexkeys k ON k.id = o.id AND k.indid = x.indid\r\n   INNER JOIN syscolumns c ON c.id = o.id AND c.colid = k.colid\r\nORDER BY SchemaName, TableName, IndexName");
			SqlDsvStatisticsProvider.IColumnIndexesReader columnIndexesReader2;
			try
			{
				dbCommand = base.CreateCommand(text);
				dataReader = dbCommand.ExecuteReader();
				columnIndexesReader = new MsSqlDsvStatisticsProvider.ColumnIndexesReader(dbCommand, dataReader, base.DBObjectNameComparer);
				columnIndexesReader2 = columnIndexesReader;
			}
			catch
			{
				if (dataReader != null)
				{
					dataReader.Dispose();
				}
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
				if (columnIndexesReader != null)
				{
					columnIndexesReader.Dispose();
				}
				throw;
			}
			return columnIndexesReader2;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000048F8 File Offset: 0x00002AF8
		protected override SqlDsvStatisticsProvider.IGetColumnDbTypeName GetColumnDbTypeNameDictionary()
		{
			SqlDsvStatisticsProvider.IGetColumnDbTypeName getColumnDbTypeName;
			try
			{
				getColumnDbTypeName = new MsSqlDsvGenerator(this.Connection).GenerateColumnsExtendedInfo(base.DataSourceView.CompareInfo);
			}
			catch
			{
				getColumnDbTypeName = null;
			}
			return getColumnDbTypeName;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000493C File Offset: 0x00002B3C
		protected override string GetDelimitedIdentifier(string name)
		{
			return MsSqlBatch.GetDelimitedIdentifierStatic(name);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004944 File Offset: 0x00002B44
		protected override string GetSqlForSelectSampledTableSource(long actualRowCount, long targetRowCount, string tableSource, bool isTable)
		{
			if (isTable && this.IsServerMajorVersionAtLeast(9))
			{
				return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("(SELECT * FROM {0} t TABLESAMPLE({1} ROWS) REPEATABLE(1))", new object[] { tableSource, targetRowCount });
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("(SELECT TOP {0} * FROM {1} t)", new object[] { targetRowCount, tableSource });
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000499C File Offset: 0x00002B9C
		protected override string WrapForCountExpression(DsvColumn column)
		{
			string text = base.WrapForCountExpression(column);
			if (text != null && column.DataType == typeof(Guid))
			{
				text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("CAST({0} AS BINARY(16))", new object[] { text });
			}
			return text;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000049E1 File Offset: 0x00002BE1
		protected override string GetSqlForCountExpression(string expression)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("COUNT_BIG({0})", new object[] { expression });
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000049F7 File Offset: 0x00002BF7
		protected override string GetSqlForCountDistinctExpression(string expression)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("COUNT_BIG(DISTINCT {0})", new object[] { expression });
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004A0D File Offset: 0x00002C0D
		protected override string GetSqlForStringAvgWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("AVG(CAST(LEN(RTRIM({0})) AS REAL))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004A29 File Offset: 0x00002C29
		protected override string GetSqlForStringStDevWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("STDEV(LEN(RTRIM({0})))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004A45 File Offset: 0x00002C45
		protected override string GetSqlForStringMaxWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("MAX(LEN(RTRIM({0})))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004A61 File Offset: 0x00002C61
		protected override string GetSqlForIntAvgWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("AVG(CAST(LEN(CAST({0} AS VARCHAR(50))) AS REAL))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004A7D File Offset: 0x00002C7D
		protected override string GetSqlForIntStDevWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("STDEV(LEN(CAST({0} AS VARCHAR(50))))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004A99 File Offset: 0x00002C99
		protected override string GetSqlForIntMaxWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("MAX(LEN(CAST({0} AS VARCHAR(50))))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004AB5 File Offset: 0x00002CB5
		protected override string GetSqlForRealAvgWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("AVG(CAST(LEN(CAST(ROUND({0},0,1) AS VARCHAR(50))) AS REAL))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004AD1 File Offset: 0x00002CD1
		protected override string GetSqlForRealStDevWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("STDEV(LEN(CAST(ROUND({0},0,1) AS VARCHAR(50))))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004AED File Offset: 0x00002CED
		protected override string GetSqlForRealMaxWidthExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("MAX(LEN(CAST(ROUND({0},0,1) AS VARCHAR(50))))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004B09 File Offset: 0x00002D09
		protected override string GetSqlForRealAvgScaleExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("AVG(CAST(LEN(RTRIM(REPLACE(REPLACE(CAST({0} - ROUND({0},0,1) AS VARCHAR(50)), '0.', ''), '0', ' '))) AS REAL))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004B25 File Offset: 0x00002D25
		protected override string GetSqlForRealStDevScaleExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("STDEV(LEN(RTRIM(REPLACE(REPLACE(CAST({0} - ROUND({0},0,1) AS VARCHAR(50)), '0.', ''), '0', ' '))))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004B41 File Offset: 0x00002D41
		protected override string GetSqlForRealMaxScaleExpression(DsvColumn column)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("MAX(LEN(RTRIM(REPLACE(REPLACE(CAST({0} - ROUND({0},0,1) AS VARCHAR(50)), '0.', ''), '0', ' '))))", new object[] { base.GetSqlForColumnSelectExpression(column) });
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004B5D File Offset: 0x00002D5D
		protected virtual bool IsSql
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004B60 File Offset: 0x00002D60
		private bool IsServerMajorVersionAtLeast(int minimumMajorVersion)
		{
			int? num = SqlBatch.ParseServerMajorVersion(this.Connection.ServerVersion);
			return num != null && num >= minimumMajorVersion;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003C68 File Offset: 0x00001E68
		private new DbConnection Connection
		{
			[DebuggerStepThrough]
			get
			{
				return (DbConnection)base.Connection;
			}
		}

		// Token: 0x020000A9 RID: 169
		private sealed class ColumnIndexesReader : SqlDsvStatisticsProvider.IColumnIndexesReader, IDisposable
		{
			// Token: 0x06000669 RID: 1641 RVA: 0x0001A577 File Offset: 0x00018777
			internal ColumnIndexesReader(IDbCommand cmd, IDataReader reader, IEqualityComparer<string> dbObjectNameComparer)
			{
				this.m_cmd = cmd;
				this.m_reader = reader;
				this.m_dbObjectNameComparer = dbObjectNameComparer;
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x0001A5B0 File Offset: 0x000187B0
			bool SqlDsvStatisticsProvider.IColumnIndexesReader.Read()
			{
				if (this.m_firstRow)
				{
					this.m_EOF = !this.m_reader.Read();
					this.m_firstRow = false;
				}
				if (this.m_EOF)
				{
					return false;
				}
				this.m_schemaName = this.GetString(0);
				this.m_tableName = this.GetString(1);
				this.m_indexName = this.GetString(2);
				this.m_indexColumns.Clear();
				do
				{
					this.m_indexColumns.Add(this.GetString(3));
					this.m_EOF = !this.m_reader.Read();
				}
				while (!this.m_EOF && this.m_dbObjectNameComparer.Equals(this.m_schemaName, this.GetString(0)) && this.m_dbObjectNameComparer.Equals(this.m_tableName, this.GetString(1)) && this.m_dbObjectNameComparer.Equals(this.m_indexName, this.GetString(2)));
				return true;
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x0600066B RID: 1643 RVA: 0x0000334E File Offset: 0x0000154E
			string SqlDsvStatisticsProvider.IColumnIndexesReader.IndexSchemaName
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001A699 File Offset: 0x00018899
			string SqlDsvStatisticsProvider.IColumnIndexesReader.IndexName
			{
				get
				{
					return this.m_indexName;
				}
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001A6A1 File Offset: 0x000188A1
			string SqlDsvStatisticsProvider.IColumnIndexesReader.TableSchemaName
			{
				get
				{
					return this.m_schemaName;
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001A6A9 File Offset: 0x000188A9
			string SqlDsvStatisticsProvider.IColumnIndexesReader.TableName
			{
				get
				{
					return this.m_tableName;
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001A6B1 File Offset: 0x000188B1
			IList<string> SqlDsvStatisticsProvider.IColumnIndexesReader.IndexColumns
			{
				get
				{
					return this.m_indexColumns;
				}
			}

			// Token: 0x06000670 RID: 1648 RVA: 0x0001A6B9 File Offset: 0x000188B9
			void IDisposable.Dispose()
			{
				this.m_reader.Dispose();
				this.m_cmd.Dispose();
			}

			// Token: 0x06000671 RID: 1649 RVA: 0x0001A6D1 File Offset: 0x000188D1
			private string GetString(int i)
			{
				if (this.m_reader.IsDBNull(i))
				{
					return null;
				}
				return this.m_reader.GetString(i);
			}

			// Token: 0x0400030D RID: 781
			private readonly IDbCommand m_cmd;

			// Token: 0x0400030E RID: 782
			private readonly IDataReader m_reader;

			// Token: 0x0400030F RID: 783
			private readonly IEqualityComparer<string> m_dbObjectNameComparer;

			// Token: 0x04000310 RID: 784
			private bool m_firstRow = true;

			// Token: 0x04000311 RID: 785
			private bool m_EOF = true;

			// Token: 0x04000312 RID: 786
			private string m_schemaName;

			// Token: 0x04000313 RID: 787
			private string m_tableName;

			// Token: 0x04000314 RID: 788
			private string m_indexName;

			// Token: 0x04000315 RID: 789
			private readonly List<string> m_indexColumns = new List<string>();
		}
	}
}
