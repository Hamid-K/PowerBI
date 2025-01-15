using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011FA RID: 4602
	internal sealed class SelectOutputClause : OutputClause
	{
		// Token: 0x0600796E RID: 31086 RVA: 0x001A3DAC File Offset: 0x001A1FAC
		public SelectOutputClause(List<SelectItem> columnList, TableReference table, Condition whereClause, StatementType statementType)
			: base(columnList)
		{
			this.statementType = statementType;
			this.whereClause = whereClause;
			this.selectClause = new QuerySpecification
			{
				SelectItems = columnList,
				FromItems = new List<FromItem>
				{
					new FromTable
					{
						Table = table
					}
				},
				WhereClause = whereClause
			};
		}

		// Token: 0x0600796F RID: 31087 RVA: 0x001A3E05 File Offset: 0x001A2005
		public override void WritePrefixScript(ScriptWriter writer)
		{
			if (this.statementType == StatementType.Delete)
			{
				this.selectClause.WriteCreateScript(writer);
				writer.WriteSpaceBefore(SqlLanguageSymbols.SemiColonSqlString);
			}
		}

		// Token: 0x06007970 RID: 31088 RVA: 0x001A3E27 File Offset: 0x001A2027
		public override void WriteSuffixScript(ScriptWriter writer)
		{
			if (this.statementType != StatementType.Delete && this.whereClause != null)
			{
				writer.WriteSpaceBefore(SqlLanguageSymbols.SemiColonSqlString);
				this.selectClause.WriteCreateScript(writer);
			}
		}

		// Token: 0x04004204 RID: 16900
		private readonly StatementType statementType;

		// Token: 0x04004205 RID: 16901
		private readonly Condition whereClause;

		// Token: 0x04004206 RID: 16902
		private readonly QuerySpecification selectClause;
	}
}
