using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x020005BF RID: 1471
	internal sealed class UnionCqlBlock : CqlBlock
	{
		// Token: 0x06004740 RID: 18240 RVA: 0x000FBBAE File Offset: 0x000F9DAE
		internal UnionCqlBlock(SlotInfo[] slotInfos, List<CqlBlock> children, CqlIdentifiers identifiers, int blockAliasNum)
			: base(slotInfos, children, BoolExpression.True, identifiers, blockAliasNum)
		{
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x000FBBC0 File Offset: 0x000F9DC0
		internal override StringBuilder AsEsql(StringBuilder builder, bool isTopLevel, int indentLevel)
		{
			bool flag = true;
			foreach (CqlBlock cqlBlock in base.Children)
			{
				if (!flag)
				{
					StringUtil.IndentNewLine(builder, indentLevel + 1);
					builder.Append(OpCellTreeNode.OpToEsql(CellTreeOpType.Union));
				}
				flag = false;
				builder.Append(" (");
				cqlBlock.AsEsql(builder, isTopLevel, indentLevel + 1);
				builder.Append(')');
			}
			return builder;
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x000FBC44 File Offset: 0x000F9E44
		internal override DbExpression AsCqt(bool isTopLevel)
		{
			DbExpression dbExpression = base.Children[0].AsCqt(isTopLevel);
			for (int i = 1; i < base.Children.Count; i++)
			{
				dbExpression = dbExpression.UnionAll(base.Children[i].AsCqt(isTopLevel));
			}
			return dbExpression;
		}
	}
}
