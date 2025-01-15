using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x0200002A RID: 42
	internal abstract class SqlRefExpressionBase : SqlExpression
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00007EAD File Offset: 0x000060AD
		protected SqlRefExpressionBase(SqlTableSource tableSource, SqlBatch sqlBatch, bool nullable)
			: base(nullable)
		{
			if (tableSource == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("tableSource"));
			}
			if (sqlBatch == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("sqlBatch"));
			}
			this.m_tableSource = tableSource;
			this.m_sqlBatch = sqlBatch;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00007EEA File Offset: 0x000060EA
		internal SqlTableSource SqlTableSource
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_tableSource;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007EF2 File Offset: 0x000060F2
		protected SqlBatch SqlBatch
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_sqlBatch;
			}
		}

		// Token: 0x040000C0 RID: 192
		private readonly SqlTableSource m_tableSource;

		// Token: 0x040000C1 RID: 193
		private readonly SqlBatch m_sqlBatch;
	}
}
