using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x020005B9 RID: 1465
	internal sealed class CaseCqlBlock : CqlBlock
	{
		// Token: 0x06004713 RID: 18195 RVA: 0x000FB1C4 File Offset: 0x000F93C4
		internal CaseCqlBlock(SlotInfo[] slots, int caseSlot, CqlBlock child, BoolExpression whereClause, CqlIdentifiers identifiers, int blockAliasNum)
			: base(slots, new List<CqlBlock>(new CqlBlock[] { child }), whereClause, identifiers, blockAliasNum)
		{
			this.m_caseSlotInfo = slots[caseSlot];
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x000FB1EC File Offset: 0x000F93EC
		internal override StringBuilder AsEsql(StringBuilder builder, bool isTopLevel, int indentLevel)
		{
			StringUtil.IndentNewLine(builder, indentLevel);
			builder.Append("SELECT ");
			if (isTopLevel)
			{
				builder.Append("VALUE ");
			}
			builder.Append("-- Constructing ").Append(this.m_caseSlotInfo.OutputMember.LeafName);
			CqlBlock cqlBlock = base.Children[0];
			base.GenerateProjectionEsql(builder, cqlBlock.CqlAlias, true, indentLevel, isTopLevel);
			builder.Append("FROM (");
			cqlBlock.AsEsql(builder, false, indentLevel + 1);
			StringUtil.IndentNewLine(builder, indentLevel);
			builder.Append(") AS ").Append(cqlBlock.CqlAlias);
			if (!BoolExpression.EqualityComparer.Equals(base.WhereClause, BoolExpression.True))
			{
				StringUtil.IndentNewLine(builder, indentLevel);
				builder.Append("WHERE ");
				base.WhereClause.AsEsql(builder, cqlBlock.CqlAlias);
			}
			return builder;
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x000FB2D0 File Offset: 0x000F94D0
		internal override DbExpression AsCqt(bool isTopLevel)
		{
			DbExpression dbExpression = base.Children[0].AsCqt(false);
			if (!BoolExpression.EqualityComparer.Equals(base.WhereClause, BoolExpression.True))
			{
				dbExpression = dbExpression.Where((DbExpression row) => this.WhereClause.AsCqt(row));
			}
			return dbExpression.Select((DbExpression row) => this.GenerateProjectionCqt(row, isTopLevel));
		}

		// Token: 0x04001939 RID: 6457
		private readonly SlotInfo m_caseSlotInfo;
	}
}
