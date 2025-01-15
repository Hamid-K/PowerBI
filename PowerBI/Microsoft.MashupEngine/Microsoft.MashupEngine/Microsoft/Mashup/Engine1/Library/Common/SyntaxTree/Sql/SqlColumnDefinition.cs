using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011FE RID: 4606
	internal sealed class SqlColumnDefinition : IScriptable
	{
		// Token: 0x06007976 RID: 31094 RVA: 0x001A3E72 File Offset: 0x001A2072
		public SqlColumnDefinition(ColumnReference column, SqlDataType type, List<SqlColumnConstraint> constraints = null)
		{
			this.column = column;
			this.type = type;
			this.constraints = constraints;
		}

		// Token: 0x17002126 RID: 8486
		// (get) Token: 0x06007977 RID: 31095 RVA: 0x001A3E8F File Offset: 0x001A208F
		public ColumnReference Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x17002127 RID: 8487
		// (get) Token: 0x06007978 RID: 31096 RVA: 0x001A3E97 File Offset: 0x001A2097
		public SqlDataType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17002128 RID: 8488
		// (get) Token: 0x06007979 RID: 31097 RVA: 0x001A3E9F File Offset: 0x001A209F
		public IList<SqlColumnConstraint> Constraints
		{
			get
			{
				return this.constraints;
			}
		}

		// Token: 0x0600797A RID: 31098 RVA: 0x001A3EA8 File Offset: 0x001A20A8
		public void WriteCreateScript(ScriptWriter writer)
		{
			this.column.WriteCreateScript(writer);
			writer.WriteSpace();
			this.type.WriteCreateScript(writer);
			if (this.constraints != null)
			{
				foreach (SqlColumnConstraint sqlColumnConstraint in this.constraints)
				{
					writer.WriteSpace();
					sqlColumnConstraint.WriteCreateScript(writer);
				}
			}
		}

		// Token: 0x0400422A RID: 16938
		private readonly ColumnReference column;

		// Token: 0x0400422B RID: 16939
		private readonly SqlDataType type;

		// Token: 0x0400422C RID: 16940
		private readonly List<SqlColumnConstraint> constraints;
	}
}
