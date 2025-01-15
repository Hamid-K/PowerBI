using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.Modeling.ModelGeneration.MSSQL;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQL
{
	// Token: 0x0200004A RID: 74
	internal class MsSqlBatch : SqlBatch
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0000FF34 File Offset: 0x0000E134
		internal MsSqlBatch(SemanticModel model, string serverVersion, bool enableMathOpCasting)
			: base(model, serverVersion, enableMathOpCasting)
		{
			if (this.SetDateFirst)
			{
				base.Statements.Add(new SqlStringSnippet("SET DATEFIRST {0}", new object[] { MsSqlBatch.GetFirstDayOfWeekStr(base.FirstDayOfWeek) }));
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000FF70 File Offset: 0x0000E170
		internal static string GetDelimitedIdentifierStatic(string name)
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("[{0}]", new object[] { name.Replace("]", "]]") });
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000FF95 File Offset: 0x0000E195
		internal override SqlSelectQuery CreateSelectQuery(ModelEntity primaryTableSource)
		{
			return new MsSqlSelectQuery(primaryTableSource, this);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000FF9E File Offset: 0x0000E19E
		internal override SqlSelectQuery CreateSelectQuery(ISelectList primaryTableSource)
		{
			return new MsSqlSelectQuery(primaryTableSource, this);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000FFA7 File Offset: 0x0000E1A7
		internal override SqlSelectQuery CreateSelectQuery()
		{
			return new MsSqlSelectQuery(this);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		internal override ISqlSnippet CreateSqlSnippetForInteger(long integer)
		{
			if (integer >= -2147483648L && integer <= 2147483647L)
			{
				return base.CreateSqlSnippetForInteger(integer);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				base.CreateSqlSnippetForInteger(integer),
				MsSqlFunctionExpression.AsBigIntCloseParenSnippet
			});
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override string GetDelimitedIdentifier(string name)
		{
			return MsSqlBatch.GetDelimitedIdentifierStatic(name);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00004826 File Offset: 0x00002A26
		internal override bool IsBlob(DsvColumn column)
		{
			return MsSqlDsvStatisticsProvider.IsBlobStatic(column);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		internal override int IdentifierMaxLength
		{
			[DebuggerStepThrough]
			get
			{
				return 128;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00010003 File Offset: 0x0000E203
		internal override ISqlSnippet SqlBitTrueSnippet
		{
			[DebuggerStepThrough]
			get
			{
				return MsSqlBatch.MsSqlBitTrueSnippet;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0001000A File Offset: 0x0000E20A
		internal override ISqlSnippet SqlBitFalseSnippet
		{
			[DebuggerStepThrough]
			get
			{
				return MsSqlBatch.MsSqlBitFalseSnippet;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00004B5D File Offset: 0x00002D5D
		protected virtual bool SetDateFirst
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00010014 File Offset: 0x0000E214
		private static string GetFirstDayOfWeekStr(DayOfWeek dayOfWeek)
		{
			switch (dayOfWeek)
			{
			case DayOfWeek.Sunday:
				return "7";
			case DayOfWeek.Monday:
				return "1";
			case DayOfWeek.Tuesday:
				return "2";
			case DayOfWeek.Wednesday:
				return "3";
			case DayOfWeek.Thursday:
				return "4";
			case DayOfWeek.Friday:
				return "5";
			case DayOfWeek.Saturday:
				return "6";
			default:
				throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("dayOfWeek", "Unknown value: " + dayOfWeek.ToString()));
			}
		}

		// Token: 0x04000182 RID: 386
		private static readonly ISqlSnippet MsSqlBitTrueSnippet = new SqlStringSnippet("CAST(1 AS BIT)");

		// Token: 0x04000183 RID: 387
		private static readonly ISqlSnippet MsSqlBitFalseSnippet = new SqlStringSnippet("CAST(0 AS BIT)");
	}
}
