using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x020005BC RID: 1468
	internal sealed class ExtentCqlBlock : CqlBlock
	{
		// Token: 0x0600472C RID: 18220 RVA: 0x000FB694 File Offset: 0x000F9894
		internal ExtentCqlBlock(EntitySetBase extent, CellQuery.SelectDistinct selectDistinct, SlotInfo[] slots, BoolExpression whereClause, CqlIdentifiers identifiers, int blockAliasNum)
			: base(slots, ExtentCqlBlock._emptyChildren, whereClause, identifiers, blockAliasNum)
		{
			this.m_extent = extent;
			this.m_nodeTableAlias = identifiers.GetBlockAlias();
			this.m_selectDistinct = selectDistinct;
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x000FB6C4 File Offset: 0x000F98C4
		internal override StringBuilder AsEsql(StringBuilder builder, bool isTopLevel, int indentLevel)
		{
			StringUtil.IndentNewLine(builder, indentLevel);
			builder.Append("SELECT ");
			if (this.m_selectDistinct == CellQuery.SelectDistinct.Yes)
			{
				builder.Append("DISTINCT ");
			}
			base.GenerateProjectionEsql(builder, this.m_nodeTableAlias, true, indentLevel, isTopLevel);
			builder.Append("FROM ");
			CqlWriter.AppendEscapedQualifiedName(builder, this.m_extent.EntityContainer.Name, this.m_extent.Name);
			builder.Append(" AS ").Append(this.m_nodeTableAlias);
			if (!BoolExpression.EqualityComparer.Equals(base.WhereClause, BoolExpression.True))
			{
				StringUtil.IndentNewLine(builder, indentLevel);
				builder.Append("WHERE ");
				base.WhereClause.AsEsql(builder, this.m_nodeTableAlias);
			}
			return builder;
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x000FB78C File Offset: 0x000F998C
		internal override DbExpression AsCqt(bool isTopLevel)
		{
			DbExpression dbExpression = this.m_extent.Scan();
			if (!BoolExpression.EqualityComparer.Equals(base.WhereClause, BoolExpression.True))
			{
				dbExpression = dbExpression.Where((DbExpression row) => this.WhereClause.AsCqt(row));
			}
			dbExpression = dbExpression.Select((DbExpression row) => this.GenerateProjectionCqt(row, isTopLevel));
			if (this.m_selectDistinct == CellQuery.SelectDistinct.Yes)
			{
				dbExpression = dbExpression.Distinct();
			}
			return dbExpression;
		}

		// Token: 0x04001940 RID: 6464
		private readonly EntitySetBase m_extent;

		// Token: 0x04001941 RID: 6465
		private readonly string m_nodeTableAlias;

		// Token: 0x04001942 RID: 6466
		private readonly CellQuery.SelectDistinct m_selectDistinct;

		// Token: 0x04001943 RID: 6467
		private static readonly List<CqlBlock> _emptyChildren = new List<CqlBlock>();
	}
}
