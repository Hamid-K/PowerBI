using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005B3 RID: 1459
	internal class LimitClauseKind
	{
		// Token: 0x06002DFB RID: 11771 RVA: 0x0008BD6B File Offset: 0x00089F6B
		private LimitClauseKind(NumberValue value, OdbcLimitClauseLocation location, string format)
		{
			this.value = value;
			this.takeLocation = new OdbcLimitClauseLocation?(location);
			this.takeFormat = format;
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x0008BD90 File Offset: 0x00089F90
		private LimitClauseKind(NumberValue value, string skipTakeFormat, string takeFormat, string skipFormat)
		{
			this.value = value;
			this.skipTakeFormat = skipTakeFormat;
			this.takeFormat = takeFormat;
			this.skipFormat = skipFormat;
			this.skipTakeLocation = new OdbcLimitClauseLocation?(OdbcLimitClauseLocation.AfterQuerySpecification);
			this.takeLocation = new OdbcLimitClauseLocation?(OdbcLimitClauseLocation.AfterQuerySpecification);
			this.skipLocation = new OdbcLimitClauseLocation?(OdbcLimitClauseLocation.AfterQuerySpecification);
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06002DFD RID: 11773 RVA: 0x0008BDE4 File Offset: 0x00089FE4
		private bool CanSkip
		{
			get
			{
				return this.skipLocation != null;
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x0008BDF1 File Offset: 0x00089FF1
		private bool CanTake
		{
			get
			{
				return this.takeLocation != null;
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x06002DFF RID: 11775 RVA: 0x0008BDFE File Offset: 0x00089FFE
		private bool CanSkipTake
		{
			get
			{
				return this.skipTakeLocation != null;
			}
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x0008BE0C File Offset: 0x0008A00C
		public static bool TryGetLimitClause(LimitClauseKind.LimitClauseKindType kindType, RowRange rowRange, out OdbcLimitClause limitClause)
		{
			limitClause = null;
			if (rowRange.IsAll)
			{
				return false;
			}
			if (kindType < LimitClauseKind.LimitClauseKindType.None || kindType >= LimitClauseKind.LimitClauseKindType.Count)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Odbc_InvalidLimitClauseKind, NumberValue.New((int)kindType), null);
			}
			LimitClauseKind limitClauseKind = LimitClauseKind.patterns[(int)kindType];
			if (limitClauseKind != null)
			{
				if (!rowRange.TakeCount.IsInfinite && !rowRange.SkipCount.IsZero)
				{
					if (limitClauseKind.CanSkipTake)
					{
						limitClause = new LimitClauseKind.LimitClause(limitClauseKind.skipTakeLocation.Value, string.Format(CultureInfo.InvariantCulture, limitClauseKind.skipTakeFormat, rowRange.TakeCount.Value, rowRange.SkipCount.Value));
						return true;
					}
					return false;
				}
				else if (!rowRange.TakeCount.IsInfinite)
				{
					if (limitClauseKind.CanTake)
					{
						limitClause = new LimitClauseKind.LimitClause(limitClauseKind.takeLocation.Value, string.Format(CultureInfo.InvariantCulture, limitClauseKind.takeFormat, rowRange.TakeCount.Value));
						return true;
					}
					return false;
				}
				else if (!rowRange.SkipCount.IsZero && limitClauseKind.CanSkip)
				{
					limitClause = new LimitClauseKind.LimitClause(limitClauseKind.skipLocation.Value, string.Format(CultureInfo.InvariantCulture, limitClauseKind.skipFormat, rowRange.SkipCount.Value));
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x0008BF70 File Offset: 0x0008A170
		public static Value InferLimitClauseKind(OdbcDataSource dataSource)
		{
			string tableName = string.Empty;
			dataSource.ConnectForMetadata(delegate(IOdbcConnection connection)
			{
				using (IDataReader tables = connection.GetTables(null, null, null, dataSource.TableTypes.FilterString))
				{
					if (tables.Read())
					{
						string stringOrNull = tables.GetStringOrNull(0);
						string stringOrNull2 = tables.GetStringOrNull(1);
						StringBuilder stringBuilder = new StringBuilder();
						if (!string.IsNullOrEmpty(stringOrNull))
						{
							stringBuilder.Append(dataSource.SqlSettings.QuoteIdentifier(stringOrNull));
							stringBuilder.Append(dataSource.SqlSettings.CatalogSeparator);
						}
						if (!string.IsNullOrEmpty(stringOrNull2))
						{
							stringBuilder.Append(dataSource.SqlSettings.QuoteIdentifier(stringOrNull2));
							stringBuilder.Append(dataSource.SqlSettings.SchemaSeparator);
						}
						stringBuilder.Append(dataSource.SqlSettings.QuoteIdentifier(tables.GetStringOrNull(2)));
						tableName = stringBuilder.ToString();
					}
				}
			});
			if (string.IsNullOrEmpty(tableName))
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Odbc_NoTableAvailable, null, null);
			}
			IDictionary<string, bool> testResultsCache = new Dictionary<string, bool>();
			LimitClauseKind[] array = LimitClauseKind.patterns;
			for (int i = 0; i < array.Length; i++)
			{
				LimitClauseKind pattern = array[i];
				if (pattern != null && pattern.TestQuery((OdbcLimitClauseLocation location, string limitClause) => pattern.RunQuery(dataSource, testResultsCache, tableName, location, limitClause)))
				{
					return pattern.value;
				}
			}
			return OdbcModule.LimitClauseKindValue.None;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x0008C030 File Offset: 0x0008A230
		private bool RunQuery(OdbcDataSource dataSource, IDictionary<string, bool> testResultsCache, string tableName, OdbcLimitClauseLocation location, string limitClause)
		{
			string text = this.GenerateQuery(tableName, location, limitClause);
			bool flag;
			if (testResultsCache.TryGetValue(text, out flag))
			{
				return flag;
			}
			try
			{
				using (dataSource.ExecutePageReader(text, null, EmptyArray<OdbcParameter>.Instance, RowRange.All, null, null))
				{
				}
				flag = true;
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				flag = false;
			}
			testResultsCache.Add(text, flag);
			return flag;
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x0008C0B0 File Offset: 0x0008A2B0
		private bool TestQuery(Func<OdbcLimitClauseLocation, string, bool> runQuery)
		{
			return (!this.CanTake || runQuery(this.takeLocation.Value, string.Format(CultureInfo.InvariantCulture, this.takeFormat, 1))) && (!this.CanSkip || runQuery(this.skipLocation.Value, string.Format(CultureInfo.InvariantCulture, this.skipFormat, 1))) && (!this.CanSkipTake || runQuery(this.skipTakeLocation.Value, string.Format(CultureInfo.InvariantCulture, this.skipTakeFormat, 1, 1)));
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x0008C160 File Offset: 0x0008A360
		private string GenerateQuery(string tableName, OdbcLimitClauseLocation location, string limitClause)
		{
			switch (location)
			{
			case OdbcLimitClauseLocation.BeforeQuerySpecification:
				return limitClause + string.Format(CultureInfo.InvariantCulture, " select * from {0}", tableName);
			case OdbcLimitClauseLocation.AfterQuerySpecification:
				return string.Format(CultureInfo.InvariantCulture, "select * from {0} {1}", tableName, limitClause);
			case OdbcLimitClauseLocation.AfterSelect:
				return string.Format(CultureInfo.InvariantCulture, "select all {0} * from {1}", limitClause, tableName);
			case OdbcLimitClauseLocation.AfterSelectBeforeModifiers:
				return string.Format(CultureInfo.InvariantCulture, "select {0} all * from {1}", limitClause, tableName);
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x0400140C RID: 5132
		private const int tableCatalogOrdinal = 0;

		// Token: 0x0400140D RID: 5133
		private const int tableSchemaOrdinal = 1;

		// Token: 0x0400140E RID: 5134
		private const int tableNameOrdinal = 2;

		// Token: 0x0400140F RID: 5135
		private static readonly LimitClauseKind[] patterns = new LimitClauseKind[]
		{
			null,
			new LimitClauseKind(OdbcModule.LimitClauseKindValue.Top, OdbcLimitClauseLocation.AfterSelect, "top {0}"),
			new LimitClauseKind(OdbcModule.LimitClauseKindValue.LimitOffset, "limit {0} offset {1}", "limit {0}", "offset {0}"),
			new LimitClauseKind(OdbcModule.LimitClauseKindValue.Limit, OdbcLimitClauseLocation.AfterQuerySpecification, "limit {0}"),
			new LimitClauseKind(OdbcModule.LimitClauseKindValue.AnsiSql2008, "offset {1} rows fetch first {0} rows only", "fetch first {0} rows only", "offset {0} rows")
		};

		// Token: 0x04001410 RID: 5136
		private readonly NumberValue value;

		// Token: 0x04001411 RID: 5137
		private readonly OdbcLimitClauseLocation? skipTakeLocation;

		// Token: 0x04001412 RID: 5138
		private readonly string skipTakeFormat;

		// Token: 0x04001413 RID: 5139
		private readonly OdbcLimitClauseLocation? takeLocation;

		// Token: 0x04001414 RID: 5140
		private readonly string takeFormat;

		// Token: 0x04001415 RID: 5141
		private readonly OdbcLimitClauseLocation? skipLocation;

		// Token: 0x04001416 RID: 5142
		private readonly string skipFormat;

		// Token: 0x020005B4 RID: 1460
		public enum LimitClauseKindType
		{
			// Token: 0x04001418 RID: 5144
			None,
			// Token: 0x04001419 RID: 5145
			Top,
			// Token: 0x0400141A RID: 5146
			LimitOffset,
			// Token: 0x0400141B RID: 5147
			Limit,
			// Token: 0x0400141C RID: 5148
			AnsiSql2008,
			// Token: 0x0400141D RID: 5149
			Count
		}

		// Token: 0x020005B5 RID: 1461
		private class LimitClause : OdbcLimitClause
		{
			// Token: 0x06002E06 RID: 11782 RVA: 0x0008C24E File Offset: 0x0008A44E
			public LimitClause(OdbcLimitClauseLocation location, string script)
			{
				this.location = location;
				this.script = script;
			}

			// Token: 0x170010F0 RID: 4336
			// (get) Token: 0x06002E07 RID: 11783 RVA: 0x0008C264 File Offset: 0x0008A464
			public override OdbcLimitClauseLocation Location
			{
				get
				{
					return this.location;
				}
			}

			// Token: 0x06002E08 RID: 11784 RVA: 0x0008C26C File Offset: 0x0008A46C
			public override void WriteCreateScript(ScriptWriter scriptWriter)
			{
				scriptWriter.Write(new ConstantSqlString(this.script));
			}

			// Token: 0x0400141E RID: 5150
			private readonly OdbcLimitClauseLocation location;

			// Token: 0x0400141F RID: 5151
			private readonly string script;
		}
	}
}
