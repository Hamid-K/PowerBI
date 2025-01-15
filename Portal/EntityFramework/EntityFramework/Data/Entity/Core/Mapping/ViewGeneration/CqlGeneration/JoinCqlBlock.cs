using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x020005BD RID: 1469
	internal sealed class JoinCqlBlock : CqlBlock
	{
		// Token: 0x06004730 RID: 18224 RVA: 0x000FB812 File Offset: 0x000F9A12
		internal JoinCqlBlock(CellTreeOpType opType, SlotInfo[] slotInfos, List<CqlBlock> children, List<JoinCqlBlock.OnClause> onClauses, CqlIdentifiers identifiers, int blockAliasNum)
			: base(slotInfos, children, BoolExpression.True, identifiers, blockAliasNum)
		{
			this.m_opType = opType;
			this.m_onClauses = onClauses;
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x000FB834 File Offset: 0x000F9A34
		internal override StringBuilder AsEsql(StringBuilder builder, bool isTopLevel, int indentLevel)
		{
			StringUtil.IndentNewLine(builder, indentLevel);
			builder.Append("SELECT ");
			base.GenerateProjectionEsql(builder, null, false, indentLevel, isTopLevel);
			StringUtil.IndentNewLine(builder, indentLevel);
			builder.Append("FROM ");
			int num = 0;
			foreach (CqlBlock cqlBlock in base.Children)
			{
				if (num > 0)
				{
					StringUtil.IndentNewLine(builder, indentLevel + 1);
					builder.Append(OpCellTreeNode.OpToEsql(this.m_opType));
				}
				builder.Append(" (");
				cqlBlock.AsEsql(builder, false, indentLevel + 1);
				builder.Append(") AS ").Append(cqlBlock.CqlAlias);
				if (num > 0)
				{
					StringUtil.IndentNewLine(builder, indentLevel + 1);
					builder.Append("ON ");
					this.m_onClauses[num - 1].AsEsql(builder);
				}
				num++;
			}
			return builder;
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x000FB938 File Offset: 0x000F9B38
		internal override DbExpression AsCqt(bool isTopLevel)
		{
			CqlBlock cqlBlock = base.Children[0];
			DbExpression dbExpression = cqlBlock.AsCqt(false);
			List<string> list = new List<string>();
			for (int i = 1; i < base.Children.Count; i++)
			{
				CqlBlock cqlBlock2 = base.Children[i];
				DbExpression dbExpression2 = cqlBlock2.AsCqt(false);
				Func<DbExpression, DbExpression, DbExpression> func = new Func<DbExpression, DbExpression, DbExpression>(this.m_onClauses[i - 1].AsCqt);
				DbJoinExpression dbJoinExpression;
				switch (this.m_opType)
				{
				case CellTreeOpType.FOJ:
					dbJoinExpression = dbExpression.FullOuterJoin(dbExpression2, func);
					break;
				case CellTreeOpType.LOJ:
					dbJoinExpression = dbExpression.LeftOuterJoin(dbExpression2, func);
					break;
				case CellTreeOpType.IJ:
					dbJoinExpression = dbExpression.InnerJoin(dbExpression2, func);
					break;
				default:
					return null;
				}
				if (i == 1)
				{
					cqlBlock.SetJoinTreeContext(list, dbJoinExpression.Left.VariableName);
				}
				else
				{
					list.Add(dbJoinExpression.Left.VariableName);
				}
				cqlBlock2.SetJoinTreeContext(list, dbJoinExpression.Right.VariableName);
				dbExpression = dbJoinExpression;
			}
			return dbExpression.Select((DbExpression row) => base.GenerateProjectionCqt(row, false));
		}

		// Token: 0x04001944 RID: 6468
		private readonly CellTreeOpType m_opType;

		// Token: 0x04001945 RID: 6469
		private readonly List<JoinCqlBlock.OnClause> m_onClauses;

		// Token: 0x02000BEF RID: 3055
		internal sealed class OnClause : InternalBase
		{
			// Token: 0x0600688D RID: 26765 RVA: 0x001644BF File Offset: 0x001626BF
			internal OnClause()
			{
				this.m_singleClauses = new List<JoinCqlBlock.OnClause.SingleClause>();
			}

			// Token: 0x0600688E RID: 26766 RVA: 0x001644D4 File Offset: 0x001626D4
			internal void Add(QualifiedSlot leftSlot, MemberPath leftSlotOutputMember, QualifiedSlot rightSlot, MemberPath rightSlotOutputMember)
			{
				JoinCqlBlock.OnClause.SingleClause singleClause = new JoinCqlBlock.OnClause.SingleClause(leftSlot, leftSlotOutputMember, rightSlot, rightSlotOutputMember);
				this.m_singleClauses.Add(singleClause);
			}

			// Token: 0x0600688F RID: 26767 RVA: 0x001644F8 File Offset: 0x001626F8
			internal StringBuilder AsEsql(StringBuilder builder)
			{
				bool flag = true;
				foreach (JoinCqlBlock.OnClause.SingleClause singleClause in this.m_singleClauses)
				{
					if (!flag)
					{
						builder.Append(" AND ");
					}
					singleClause.AsEsql(builder);
					flag = false;
				}
				return builder;
			}

			// Token: 0x06006890 RID: 26768 RVA: 0x00164560 File Offset: 0x00162760
			internal DbExpression AsCqt(DbExpression leftRow, DbExpression rightRow)
			{
				DbExpression dbExpression = this.m_singleClauses[0].AsCqt(leftRow, rightRow);
				for (int i = 1; i < this.m_singleClauses.Count; i++)
				{
					dbExpression = dbExpression.And(this.m_singleClauses[i].AsCqt(leftRow, rightRow));
				}
				return dbExpression;
			}

			// Token: 0x06006891 RID: 26769 RVA: 0x001645B2 File Offset: 0x001627B2
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("ON ");
				StringUtil.ToSeparatedString(builder, this.m_singleClauses, " AND ");
			}

			// Token: 0x04002F29 RID: 12073
			private readonly List<JoinCqlBlock.OnClause.SingleClause> m_singleClauses;

			// Token: 0x02000D85 RID: 3461
			private sealed class SingleClause : InternalBase
			{
				// Token: 0x06006F55 RID: 28501 RVA: 0x0017D4F4 File Offset: 0x0017B6F4
				internal SingleClause(QualifiedSlot leftSlot, MemberPath leftSlotOutputMember, QualifiedSlot rightSlot, MemberPath rightSlotOutputMember)
				{
					this.m_leftSlot = leftSlot;
					this.m_leftSlotOutputMember = leftSlotOutputMember;
					this.m_rightSlot = rightSlot;
					this.m_rightSlotOutputMember = rightSlotOutputMember;
				}

				// Token: 0x06006F56 RID: 28502 RVA: 0x0017D519 File Offset: 0x0017B719
				internal StringBuilder AsEsql(StringBuilder builder)
				{
					builder.Append(this.m_leftSlot.GetQualifiedCqlName(this.m_leftSlotOutputMember)).Append(" = ").Append(this.m_rightSlot.GetQualifiedCqlName(this.m_rightSlotOutputMember));
					return builder;
				}

				// Token: 0x06006F57 RID: 28503 RVA: 0x0017D554 File Offset: 0x0017B754
				internal DbExpression AsCqt(DbExpression leftRow, DbExpression rightRow)
				{
					return this.m_leftSlot.AsCqt(leftRow, this.m_leftSlotOutputMember).Equal(this.m_rightSlot.AsCqt(rightRow, this.m_rightSlotOutputMember));
				}

				// Token: 0x06006F58 RID: 28504 RVA: 0x0017D57F File Offset: 0x0017B77F
				internal override void ToCompactString(StringBuilder builder)
				{
					this.m_leftSlot.ToCompactString(builder);
					builder.Append(" = ");
					this.m_rightSlot.ToCompactString(builder);
				}

				// Token: 0x04003351 RID: 13137
				private readonly QualifiedSlot m_leftSlot;

				// Token: 0x04003352 RID: 13138
				private readonly MemberPath m_leftSlotOutputMember;

				// Token: 0x04003353 RID: 13139
				private readonly QualifiedSlot m_rightSlot;

				// Token: 0x04003354 RID: 13140
				private readonly MemberPath m_rightSlotOutputMember;
			}
		}
	}
}
