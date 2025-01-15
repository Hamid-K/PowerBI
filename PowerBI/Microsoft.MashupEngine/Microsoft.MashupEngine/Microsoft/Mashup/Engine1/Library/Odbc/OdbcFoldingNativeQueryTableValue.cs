using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005F4 RID: 1524
	internal sealed class OdbcFoldingNativeQueryTableValue : OdbcNativeQueryTableValue
	{
		// Token: 0x06003012 RID: 12306 RVA: 0x000916DC File Offset: 0x0008F8DC
		public OdbcFoldingNativeQueryTableValue(string dataSourceName, IEngineHost host, OdbcDataSource dataSource, string nativeQuery, string catalog, Value options, IQueryDomain queryDomain, IList<OdbcParameter> parameters)
			: base(dataSourceName, host, dataSource, nativeQuery, catalog, false, queryDomain, parameters)
		{
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000916FC File Offset: 0x0008F8FC
		public TableValue CreateOdbcQueryTableValue()
		{
			base.VerifyQueryPermission();
			string text = this.SelectStarForSchema();
			StructuredCacheKey structuredCacheKey = base.DataSource.CurrentCacheContext.WithCatalog(base.Catalog).GetStructuredCacheKey(new string[] { "NativeQuerySchema/1", text });
			TableSchema orCommitValue = base.Host.QueryService<ICacheSets>().Metadata.PersistentObjectCache.GetOrCommitValue(structuredCacheKey, () => new OdbcNativeQueryTableValue(base.DataSourceName, base.Host, base.DataSource, this.SelectStarForSchema(), base.Catalog, true, null, base.Parameters).GetSchema(), delegate(Stream s, TableSchema v)
			{
				v.Serialize(s);
			}, new Func<Stream, TableSchema>(TableSchema.Deserialize));
			OdbcQueryColumnInfo[] array = new OdbcQueryColumnInfo[orCommitValue.ColumnCount];
			SelectItem[] array2 = new SelectItem[orCommitValue.ColumnCount];
			for (int i = 0; i < orCommitValue.ColumnCount; i++)
			{
				SchemaColumn column = orCommitValue.GetColumn(i);
				Odbc32.SQL_TYPE sql_TYPE = (Odbc32.SQL_TYPE)column.ProviderType.Value;
				OdbcQueryColumnInfo[] array3 = array;
				int num = i;
				string name = column.Name;
				TypeValue typeValue = OdbcTypeValue.New(sql_TYPE, column.DataTypeName, null, column.NumericBase, column.ColumnSize, column.NumericScale, column.IsUnsigned, new bool?(column.Nullable), null);
				OdbcTypeInfo odbcTypeInfo = OdbcFoldingNativeQueryTableValue.GetOdbcTypeInfo(base.DataSource, sql_TYPE, column.DataTypeName);
				bool nullable = column.Nullable;
				long? columnSize = column.ColumnSize;
				long num2 = 0L;
				array3[num] = new OdbcQueryColumnInfo(name, typeValue, new OdbcDerivedColumnTypeInfo(odbcTypeInfo, nullable, new int?(((columnSize.GetValueOrDefault() > num2) & (columnSize != null)) ? ((int)column.ColumnSize.Value) : 0), column.NumericScale));
				array2[i] = new SelectItem(new ColumnReference(Alias.NewNativeAlias(column.Name)));
			}
			OdbcQuerySpecification querySpecification = this.GetQuerySpecification(array2, false);
			return new QueryTableValue(new OptimizableQuery(((OdbcQueryDomain)this.QueryDomain).NewQuery(array, querySpecification, EmptyArray<TableKey>.Instance, true, TableSortOrder.Unknown, RowRange.All)));
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000918E8 File Offset: 0x0008FAE8
		private string SelectStarForSchema()
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				ScriptWriter scriptWriter = new ScriptWriter(stringWriter, base.DataSource.SqlSettings);
				this.GetQuerySpecification(new SelectItem[]
				{
					new SelectItem(SqlConstant.SelectAll)
				}, true).WriteCreateScript(scriptWriter);
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x00091958 File Offset: 0x0008FB58
		private OdbcQuerySpecification GetQuerySpecification(SelectItem[] selectItems, bool forSchema)
		{
			OdbcQuerySpecification odbcQuerySpecification = new OdbcQuerySpecification();
			odbcQuerySpecification.SelectItems = selectItems;
			QuerySpecification querySpecification = odbcQuerySpecification;
			FromItem[] array = new FromItem[1];
			int num = 0;
			FromQuery fromQuery = new FromQuery();
			string odbcNativeQuery = base.OdbcNativeQuery;
			IList<OdbcParameter> parameters = base.Parameters;
			IEnumerable<DynamicParameter> enumerable;
			if (parameters == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = parameters.Select((OdbcParameter p) => new DynamicParameter(p));
			}
			fromQuery.Query = new VerbatimSqlQueryExpression(odbcNativeQuery, enumerable);
			fromQuery.Alias = Alias.Underscore;
			array[num] = fromQuery;
			querySpecification.FromItems = array;
			OdbcQuerySpecification odbcQuerySpecification2 = odbcQuerySpecification;
			if (forSchema)
			{
				OdbcQueryDomain odbcQueryDomain = (OdbcQueryDomain)this.QueryDomain;
				NativeQuerySchemaStrategy nativeQuerySchemaStrategy = odbcQueryDomain.DataSource.Info.NativeQuerySchemaStrategy;
				if (nativeQuerySchemaStrategy > NativeQuerySchemaStrategy.TopZero)
				{
					if (nativeQuerySchemaStrategy != NativeQuerySchemaStrategy.WhereZeroEqualsOne)
					{
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					}
					if (odbcQuerySpecification2.WhereClause != null)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Odbc_InvalidSQLGetInfoOverride, null, null);
					}
					odbcQuerySpecification2.WhereClause = new BinaryLogicalOperation(SqlConstant.Zero, BinaryLogicalOperator.Equals, SqlConstant.One);
				}
				else
				{
					RowCount rowCount = ((odbcQueryDomain.DataSource.Info.NativeQuerySchemaStrategy == NativeQuerySchemaStrategy.TopZero) ? RowCount.Zero : RowCount.One);
					OdbcLimitClause odbcLimitClause;
					RowRange rowRange;
					if (!odbcQueryDomain.SqlExpressionGenerator.TryGetLimitClause(RowRange.All.Take(rowCount), out odbcLimitClause, out rowRange) || !rowRange.IsAll)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Odbc_InvalidLimitClauseKind, null, null);
					}
					odbcQuerySpecification2.LimitClause = odbcLimitClause;
				}
			}
			return odbcQuerySpecification2;
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x00091AAC File Offset: 0x0008FCAC
		private static OdbcTypeInfo GetOdbcTypeInfo(OdbcDataSource dataSource, Odbc32.SQL_TYPE sqlType, string dataTypeName)
		{
			OdbcTypeInfo odbcTypeInfo;
			if (dataSource.Types.TryGetType(sqlType, dataTypeName, out odbcTypeInfo))
			{
				return odbcTypeInfo;
			}
			return new OdbcTypeInfo(sqlType);
		}
	}
}
