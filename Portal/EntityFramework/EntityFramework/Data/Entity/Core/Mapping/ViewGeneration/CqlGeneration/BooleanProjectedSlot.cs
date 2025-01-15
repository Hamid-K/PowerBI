using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x020005B8 RID: 1464
	internal sealed class BooleanProjectedSlot : ProjectedSlot
	{
		// Token: 0x0600470E RID: 18190 RVA: 0x000FB0A2 File Offset: 0x000F92A2
		internal BooleanProjectedSlot(BoolExpression expr, CqlIdentifiers identifiers, int originalCellNum)
		{
			this.m_expr = expr;
			this.m_originalCell = new CellIdBoolean(identifiers, originalCellNum);
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x000FB0BE File Offset: 0x000F92BE
		internal override string GetCqlFieldAlias(MemberPath outputMember)
		{
			return this.m_originalCell.SlotName;
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x000FB0CC File Offset: 0x000F92CC
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			if (this.m_expr.IsTrue || this.m_expr.IsFalse)
			{
				this.m_expr.AsEsql(builder, blockAlias);
			}
			else
			{
				builder.Append("CASE WHEN ");
				this.m_expr.AsEsql(builder, blockAlias);
				builder.Append(" THEN True ELSE False END");
			}
			return builder;
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x000FB12C File Offset: 0x000F932C
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			if (this.m_expr.IsTrue || this.m_expr.IsFalse)
			{
				return this.m_expr.AsCqt(row);
			}
			return DbExpressionBuilder.Case(new DbExpression[] { this.m_expr.AsCqt(row) }, new DbExpression[] { DbExpressionBuilder.True }, DbExpressionBuilder.False);
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x000FB18D File Offset: 0x000F938D
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.FormatStringBuilder(builder, "<{0}, ", new object[] { this.m_originalCell.SlotName });
			this.m_expr.ToCompactString(builder);
			builder.Append('>');
		}

		// Token: 0x04001937 RID: 6455
		private readonly BoolExpression m_expr;

		// Token: 0x04001938 RID: 6456
		private readonly CellIdBoolean m_originalCell;
	}
}
