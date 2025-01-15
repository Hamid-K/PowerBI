using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x020005BA RID: 1466
	internal abstract class CqlBlock : InternalBase
	{
		// Token: 0x06004716 RID: 18198 RVA: 0x000FB340 File Offset: 0x000F9540
		protected CqlBlock(SlotInfo[] slotInfos, List<CqlBlock> children, BoolExpression whereClause, CqlIdentifiers identifiers, int blockAliasNum)
		{
			this.m_slots = new ReadOnlyCollection<SlotInfo>(slotInfos);
			this.m_children = new ReadOnlyCollection<CqlBlock>(children);
			this.m_whereClause = whereClause;
			this.m_blockAlias = identifiers.GetBlockAlias(blockAliasNum);
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06004717 RID: 18199 RVA: 0x000FB376 File Offset: 0x000F9576
		// (set) Token: 0x06004718 RID: 18200 RVA: 0x000FB37E File Offset: 0x000F957E
		internal ReadOnlyCollection<SlotInfo> Slots
		{
			get
			{
				return this.m_slots;
			}
			set
			{
				this.m_slots = value;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06004719 RID: 18201 RVA: 0x000FB387 File Offset: 0x000F9587
		protected ReadOnlyCollection<CqlBlock> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x0600471A RID: 18202 RVA: 0x000FB38F File Offset: 0x000F958F
		protected BoolExpression WhereClause
		{
			get
			{
				return this.m_whereClause;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x0600471B RID: 18203 RVA: 0x000FB397 File Offset: 0x000F9597
		internal string CqlAlias
		{
			get
			{
				return this.m_blockAlias;
			}
		}

		// Token: 0x0600471C RID: 18204
		internal abstract StringBuilder AsEsql(StringBuilder builder, bool isTopLevel, int indentLevel);

		// Token: 0x0600471D RID: 18205
		internal abstract DbExpression AsCqt(bool isTopLevel);

		// Token: 0x0600471E RID: 18206 RVA: 0x000FB3A0 File Offset: 0x000F95A0
		internal QualifiedSlot QualifySlotWithBlockAlias(int slotNum)
		{
			SlotInfo slotInfo = this.m_slots[slotNum];
			return new QualifiedSlot(this, slotInfo.SlotValue);
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x000FB3C6 File Offset: 0x000F95C6
		internal ProjectedSlot SlotValue(int slotNum)
		{
			return this.m_slots[slotNum].SlotValue;
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x000FB3D9 File Offset: 0x000F95D9
		internal MemberPath MemberPath(int slotNum)
		{
			return this.m_slots[slotNum].OutputMember;
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x000FB3EC File Offset: 0x000F95EC
		internal bool IsProjected(int slotNum)
		{
			return this.m_slots[slotNum].IsProjected;
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x000FB400 File Offset: 0x000F9600
		protected void GenerateProjectionEsql(StringBuilder builder, string blockAlias, bool addNewLineAfterEachSlot, int indentLevel, bool isTopLevel)
		{
			bool flag = true;
			foreach (SlotInfo slotInfo in this.Slots)
			{
				if (slotInfo.IsRequiredByParent)
				{
					if (!flag)
					{
						builder.Append(", ");
					}
					if (addNewLineAfterEachSlot)
					{
						StringUtil.IndentNewLine(builder, indentLevel + 1);
					}
					slotInfo.AsEsql(builder, blockAlias, indentLevel);
					if (!isTopLevel && (!(slotInfo.SlotValue is QualifiedSlot) || slotInfo.IsEnforcedNotNull))
					{
						builder.Append(" AS ").Append(slotInfo.CqlFieldAlias);
					}
					flag = false;
				}
			}
			if (addNewLineAfterEachSlot)
			{
				StringUtil.IndentNewLine(builder, indentLevel);
			}
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x000FB4B8 File Offset: 0x000F96B8
		protected DbExpression GenerateProjectionCqt(DbExpression row, bool isTopLevel)
		{
			if (isTopLevel)
			{
				return this.Slots.Where((SlotInfo slot) => slot.IsRequiredByParent).Single<SlotInfo>().AsCqt(row);
			}
			return DbExpressionBuilder.NewRow(from slot in this.Slots
				where slot.IsRequiredByParent
				select new KeyValuePair<string, DbExpression>(slot.CqlFieldAlias, slot.AsCqt(row)));
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x000FB550 File Offset: 0x000F9750
		internal void SetJoinTreeContext(IList<string> parentQualifiers, string leafQualifier)
		{
			this.m_joinTreeContext = new CqlBlock.JoinTreeContext(parentQualifiers, leafQualifier);
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x000FB55F File Offset: 0x000F975F
		internal DbExpression GetInput(DbExpression row)
		{
			if (this.m_joinTreeContext == null)
			{
				return row;
			}
			return this.m_joinTreeContext.FindInput(row);
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x000FB578 File Offset: 0x000F9778
		internal override void ToCompactString(StringBuilder builder)
		{
			for (int i = 0; i < this.m_slots.Count; i++)
			{
				StringUtil.FormatStringBuilder(builder, "{0}: ", new object[] { i });
				this.m_slots[i].ToCompactString(builder);
				builder.Append(' ');
			}
			this.m_whereClause.ToCompactString(builder);
		}

		// Token: 0x0400193A RID: 6458
		private ReadOnlyCollection<SlotInfo> m_slots;

		// Token: 0x0400193B RID: 6459
		private readonly ReadOnlyCollection<CqlBlock> m_children;

		// Token: 0x0400193C RID: 6460
		private readonly BoolExpression m_whereClause;

		// Token: 0x0400193D RID: 6461
		private readonly string m_blockAlias;

		// Token: 0x0400193E RID: 6462
		private CqlBlock.JoinTreeContext m_joinTreeContext;

		// Token: 0x02000BEB RID: 3051
		private sealed class JoinTreeContext
		{
			// Token: 0x06006882 RID: 26754 RVA: 0x001643DD File Offset: 0x001625DD
			internal JoinTreeContext(IList<string> parentQualifiers, string leafQualifier)
			{
				this.m_parentQualifiers = parentQualifiers;
				this.m_indexInParentQualifiers = parentQualifiers.Count;
				this.m_leafQualifier = leafQualifier;
			}

			// Token: 0x06006883 RID: 26755 RVA: 0x00164400 File Offset: 0x00162600
			internal DbExpression FindInput(DbExpression row)
			{
				DbExpression dbExpression = row;
				for (int i = this.m_parentQualifiers.Count - 1; i >= this.m_indexInParentQualifiers; i--)
				{
					dbExpression = dbExpression.Property(this.m_parentQualifiers[i]);
				}
				return dbExpression.Property(this.m_leafQualifier);
			}

			// Token: 0x04002F20 RID: 12064
			private readonly IList<string> m_parentQualifiers;

			// Token: 0x04002F21 RID: 12065
			private readonly int m_indexInParentQualifiers;

			// Token: 0x04002F22 RID: 12066
			private readonly string m_leafQualifier;
		}
	}
}
