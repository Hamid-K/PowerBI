using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x0200003C RID: 60
	internal sealed class SqlTableSource : ISqlSnippet
	{
		// Token: 0x0600028C RID: 652 RVA: 0x0000C48C File Offset: 0x0000A68C
		internal SqlTableSource(ISelectList tuplesSource, string alias, SqlBatch sqlBatch)
		{
			if (tuplesSource == null)
			{
				SQEAssert.AssertFalseAndThrow(new ArgumentNullException("tuplesSource"));
			}
			if (alias == null)
			{
				SQEAssert.AssertFalseAndThrow(new ArgumentNullException("alias"));
			}
			if (sqlBatch == null)
			{
				SQEAssert.AssertFalseAndThrow(new ArgumentNullException("sqlBatch"));
			}
			this.m_tuplesSource = tuplesSource;
			this.m_alias = alias;
			this.m_sqlBatch = sqlBatch;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		void ISqlSnippet.Compile(FormattedStringWriter fsw)
		{
			if (this.m_tuplesSource.UseParensWhenNested)
			{
				int num = fsw.IndentationLevel + 1;
				fsw.IndentationLevel = num;
				fsw.WriteLineIndent("(");
			}
			this.m_tuplesSource.Compile(fsw);
			if (this.m_tuplesSource.UseParensWhenNested)
			{
				int num = fsw.IndentationLevel - 1;
				fsw.IndentationLevel = num;
				fsw.WriteLineIndent();
				fsw.Write(")");
			}
			fsw.Write(" {0}", new object[] { this.m_sqlBatch.GetDelimitedIdentifier(this.m_alias) });
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000C584 File Offset: 0x0000A784
		internal ISelectList TuplesSource
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_tuplesSource;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000C58C File Offset: 0x0000A78C
		internal string Alias
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_alias;
			}
		}

		// Token: 0x040000F0 RID: 240
		private readonly ISelectList m_tuplesSource;

		// Token: 0x040000F1 RID: 241
		private readonly string m_alias;

		// Token: 0x040000F2 RID: 242
		private readonly SqlBatch m_sqlBatch;
	}
}
