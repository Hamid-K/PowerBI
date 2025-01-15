using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.Modeling.ModelGeneration.Teradata;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Teradata
{
	// Token: 0x0200003D RID: 61
	internal sealed class TdSqlBatch : SqlBatch
	{
		// Token: 0x06000290 RID: 656 RVA: 0x0000C594 File Offset: 0x0000A794
		internal TdSqlBatch(SemanticModel model, string serverVersion, string replaceFunctionName, bool enableMathOpCasting)
			: base(model, serverVersion, enableMathOpCasting)
		{
			this.m_replaceFunctionName = replaceFunctionName;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		internal static string GetDelimitedIdentifierStatic(string name)
		{
			char[] array = new char[] { '\u001a' };
			char[] array2 = new char[] { '\t', '\n', '\v', '\f', '\r', ' ' };
			string text = new string(array);
			if (name.Contains("\0") || name.Contains(text) || name.Trim(array2).Length == 0)
			{
				throw SQEAssert.AssertFalseAndThrow("Found db object name containing invalid characters: [{0}].", new object[] { name });
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("\"{0}\"", new object[] { name.Replace("\"", "\"\"") });
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000C638 File Offset: 0x0000A838
		internal string ReplaceFunctionName
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_replaceFunctionName;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C640 File Offset: 0x0000A840
		internal override SqlSelectQuery CreateSelectQuery(ModelEntity primaryTableSource)
		{
			return new TdSqlSelectQuery(primaryTableSource, this);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000C649 File Offset: 0x0000A849
		internal override SqlSelectQuery CreateSelectQuery(ISelectList primaryTableSource)
		{
			return new TdSqlSelectQuery(primaryTableSource, this);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C652 File Offset: 0x0000A852
		internal override SqlSelectQuery CreateSelectQuery()
		{
			return new TdSqlSelectQuery(this);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C65A File Offset: 0x0000A85A
		internal override INameGenerator CreateSqlAliasGenerator(string defaultCandidate)
		{
			return new TdSqlBatch.TdSqlAliasGenerator(defaultCandidate, this);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000038EC File Offset: 0x00001AEC
		internal override string GetDelimitedIdentifier(string name)
		{
			return TdSqlBatch.GetDelimitedIdentifierStatic(name);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00003726 File Offset: 0x00001926
		internal override bool IsBlob(DsvColumn column)
		{
			return TdSqlDsvStatisticsProvider.IsBlobStatic(column);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000C663 File Offset: 0x0000A863
		internal override int IdentifierMaxLength
		{
			[DebuggerStepThrough]
			get
			{
				return 30;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000C667 File Offset: 0x0000A867
		internal override ISqlSnippet SqlBitTrueSnippet
		{
			[DebuggerStepThrough]
			get
			{
				return TdSqlBatch.TdSqlBitTrueSnippet;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000C66E File Offset: 0x0000A86E
		internal override ISqlSnippet SqlBitFalseSnippet
		{
			[DebuggerStepThrough]
			get
			{
				return TdSqlBatch.TdSqlBitFalseSnippet;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000C675 File Offset: 0x0000A875
		internal override string StatementSeparator
		{
			get
			{
				return ";";
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C67C File Offset: 0x0000A87C
		internal override ISqlSnippet CreateSqlSnippetForInteger(long integer)
		{
			if (integer >= -32768L && integer <= 32767L)
			{
				return new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CastOpenParenSnippet,
					base.CreateSqlSnippetForInteger(integer),
					TdSqlFunctionExpression.AsIntCloseParenSnippet
				});
			}
			return base.CreateSqlSnippetForInteger(integer);
		}

		// Token: 0x040000F3 RID: 243
		private static readonly ISqlSnippet TdSqlBitTrueSnippet = new SqlStringSnippet("CAST(1 AS INTEGER)");

		// Token: 0x040000F4 RID: 244
		private static readonly ISqlSnippet TdSqlBitFalseSnippet = new SqlStringSnippet("CAST(0 AS INTEGER)");

		// Token: 0x040000F5 RID: 245
		private readonly string m_replaceFunctionName;

		// Token: 0x020000C7 RID: 199
		private sealed class TdSqlAliasGenerator : SqlBatch.SqlAliasGenerator
		{
			// Token: 0x0600071E RID: 1822 RVA: 0x0001BCA0 File Offset: 0x00019EA0
			internal TdSqlAliasGenerator(string defaultCandidate, SqlBatch sqlBatch)
				: base(defaultCandidate, sqlBatch)
			{
				this.m_sqlBatch = sqlBatch;
			}

			// Token: 0x0600071F RID: 1823 RVA: 0x0001BCB1 File Offset: 0x00019EB1
			public override string CreateName(string candidate)
			{
				return base.CreateName(this.PreProcessCandidate(candidate));
			}

			// Token: 0x06000720 RID: 1824 RVA: 0x0001BCC0 File Offset: 0x00019EC0
			public override string CreateName(string candidate, object key)
			{
				return base.CreateName(this.PreProcessCandidate(candidate), key);
			}

			// Token: 0x06000721 RID: 1825 RVA: 0x0001BCD0 File Offset: 0x00019ED0
			private string PreProcessCandidate(string candidate)
			{
				if (this.m_sqlBatch.IsStringInDatabaseCharset(candidate))
				{
					return candidate;
				}
				return null;
			}

			// Token: 0x0400038F RID: 911
			private readonly SqlBatch m_sqlBatch;
		}
	}
}
