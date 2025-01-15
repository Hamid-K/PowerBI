using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011E7 RID: 4583
	internal abstract class OutputClause : IScriptable
	{
		// Token: 0x060078D5 RID: 30933 RVA: 0x001A2377 File Offset: 0x001A0577
		protected OutputClause(IList<SelectItem> columnList)
		{
			this.columnList = columnList;
		}

		// Token: 0x060078D6 RID: 30934 RVA: 0x000020FD File Offset: 0x000002FD
		protected OutputClause()
		{
		}

		// Token: 0x1700210E RID: 8462
		// (get) Token: 0x060078D7 RID: 30935 RVA: 0x001A2386 File Offset: 0x001A0586
		public IList<SelectItem> ColumnList
		{
			get
			{
				return this.columnList;
			}
		}

		// Token: 0x1700210F RID: 8463
		// (get) Token: 0x060078D8 RID: 30936 RVA: 0x001A238E File Offset: 0x001A058E
		public virtual IList<Alias> OutputParameters
		{
			get
			{
				return EmptyArray<Alias>.Instance;
			}
		}

		// Token: 0x060078D9 RID: 30937 RVA: 0x001A2398 File Offset: 0x001A0598
		protected void WriteColumns(ScriptWriter writer)
		{
			bool flag = false;
			foreach (SelectItem selectItem in this.columnList)
			{
				flag = writer.WriteCommaIfNeeded(flag);
				selectItem.WriteCreateScript(writer);
			}
		}

		// Token: 0x060078DA RID: 30938 RVA: 0x001A23F0 File Offset: 0x001A05F0
		protected void WriteOutputParameters(ScriptWriter writer)
		{
			bool flag = false;
			foreach (Alias alias in this.OutputParameters)
			{
				flag = writer.WriteCommaIfNeeded(flag);
				writer.WriteParameter(alias);
			}
		}

		// Token: 0x060078DB RID: 30939 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void WriteCreateScript(ScriptWriter writer)
		{
		}

		// Token: 0x060078DC RID: 30940 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void WritePrefixScript(ScriptWriter writer)
		{
		}

		// Token: 0x060078DD RID: 30941 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void WriteSuffixScript(ScriptWriter writer)
		{
		}

		// Token: 0x040041D4 RID: 16852
		private readonly IList<SelectItem> columnList;

		// Token: 0x040041D5 RID: 16853
		public static OutputClause Null = new OutputClause.NullOutputClause();

		// Token: 0x020011E8 RID: 4584
		private sealed class NullOutputClause : OutputClause
		{
		}
	}
}
