using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.Modeling.ModelGeneration.MSSQLADW;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQL;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQLADW
{
	// Token: 0x02000047 RID: 71
	internal sealed class MsSqlAdwBatch : MsSqlBatch
	{
		// Token: 0x06000346 RID: 838 RVA: 0x0000FE48 File Offset: 0x0000E048
		internal MsSqlAdwBatch(SemanticModel model, string serverVersion, bool enableMathOpCasting)
			: base(model, serverVersion, enableMathOpCasting)
		{
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00004555 File Offset: 0x00002755
		protected override bool SetDateFirst
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000FE53 File Offset: 0x0000E053
		internal override INameGenerator CreateSqlAliasGenerator(string defaultCandidate)
		{
			return new MsSqlAdwBatch.SqlAdwAliasGenerator(defaultCandidate, this);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000FE5C File Offset: 0x0000E05C
		internal override ISqlSnippet SqlBitTrueSnippet
		{
			[DebuggerStepThrough]
			get
			{
				return MsSqlAdwBatch.MsSqlAdwBitTrueSnippet;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000FE63 File Offset: 0x0000E063
		internal override ISqlSnippet SqlBitFalseSnippet
		{
			[DebuggerStepThrough]
			get
			{
				return MsSqlAdwBatch.MsSqlAdwBitFalseSnippet;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000FE6A File Offset: 0x0000E06A
		internal override DsvCompareInfo DsvCompareInfo
		{
			[DebuggerStepThrough]
			get
			{
				return MsSqlAdwDsvGenerator.GetCompareInfoStatic(null);
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000FE72 File Offset: 0x0000E072
		internal override SqlSelectQuery CreateSelectQuery(ModelEntity primaryTableSource)
		{
			return new MsSqlAdwSelectQuery(primaryTableSource, this);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000FE7B File Offset: 0x0000E07B
		internal override SqlSelectQuery CreateSelectQuery(ISelectList primaryTableSource)
		{
			return new MsSqlAdwSelectQuery(primaryTableSource, this);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000FE84 File Offset: 0x0000E084
		internal override SqlSelectQuery CreateSelectQuery()
		{
			return new MsSqlAdwSelectQuery(this);
		}

		// Token: 0x0400017E RID: 382
		private static readonly ISqlSnippet MsSqlAdwBitTrueSnippet = new SqlStringSnippet("CAST(1 AS TINYINT)");

		// Token: 0x0400017F RID: 383
		private static readonly ISqlSnippet MsSqlAdwBitFalseSnippet = new SqlStringSnippet("CAST(0 AS TINYINT)");

		// Token: 0x020000C9 RID: 201
		private sealed class SqlAdwAliasGenerator : SqlBatch.SqlAliasGenerator
		{
			// Token: 0x06000726 RID: 1830 RVA: 0x0001BD26 File Offset: 0x00019F26
			internal SqlAdwAliasGenerator(string defaultCandidate, SqlBatch sqlBatch)
				: base(defaultCandidate, sqlBatch)
			{
				this.m_sqlBatch = sqlBatch;
			}

			// Token: 0x06000727 RID: 1831 RVA: 0x0001BD37 File Offset: 0x00019F37
			public override string CreateName(string candidate)
			{
				return base.CreateName(this.PreProcessCandidate(candidate));
			}

			// Token: 0x06000728 RID: 1832 RVA: 0x0001BD46 File Offset: 0x00019F46
			public override string CreateName(string candidate, object key)
			{
				return base.CreateName(this.PreProcessCandidate(candidate), key);
			}

			// Token: 0x06000729 RID: 1833 RVA: 0x0001BD56 File Offset: 0x00019F56
			private string PreProcessCandidate(string candidate)
			{
				if (this.m_sqlBatch.IsStringInDatabaseCharset(candidate))
				{
					return candidate;
				}
				return null;
			}

			// Token: 0x04000391 RID: 913
			private readonly SqlBatch m_sqlBatch;
		}
	}
}
