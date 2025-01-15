using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200059A RID: 1434
	internal class CellIdBoolean : TrueFalseLiteral
	{
		// Token: 0x0600455E RID: 17758 RVA: 0x000F4DD7 File Offset: 0x000F2FD7
		internal CellIdBoolean(CqlIdentifiers identifiers, int index)
		{
			this.m_index = index;
			this.m_slotName = identifiers.GetFromVariable(index);
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x000F4DF3 File Offset: 0x000F2FF3
		internal string SlotName
		{
			get
			{
				return this.m_slotName;
			}
		}

		// Token: 0x06004560 RID: 17760 RVA: 0x000F4DFC File Offset: 0x000F2FFC
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			string qualifiedName = CqlWriter.GetQualifiedName(blockAlias, this.SlotName);
			builder.Append(qualifiedName);
			return builder;
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x000F4E1F File Offset: 0x000F301F
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			return row.Property(this.SlotName);
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x000F4E2D File Offset: 0x000F302D
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return this.AsEsql(builder, blockAlias, skipIsNotNull);
		}

		// Token: 0x06004563 RID: 17763 RVA: 0x000F4E38 File Offset: 0x000F3038
		internal override StringBuilder AsNegatedUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			builder.Append("NOT(");
			builder = this.AsUserString(builder, blockAlias, skipIsNotNull);
			builder.Append(")");
			return builder;
		}

		// Token: 0x06004564 RID: 17764 RVA: 0x000F4E60 File Offset: 0x000F3060
		internal override void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
		{
			int num = requiredSlots.Length - projectedSlotMap.Count;
			int num2 = projectedSlotMap.BoolIndexToSlot(this.m_index, num);
			requiredSlots[num2] = true;
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x000F4E8C File Offset: 0x000F308C
		protected override bool IsEqualTo(BoolLiteral right)
		{
			CellIdBoolean cellIdBoolean = right as CellIdBoolean;
			return cellIdBoolean != null && this.m_index == cellIdBoolean.m_index;
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x000F4EB4 File Offset: 0x000F30B4
		public override int GetHashCode()
		{
			return this.m_index.GetHashCode();
		}

		// Token: 0x06004567 RID: 17767 RVA: 0x000F4ECF File Offset: 0x000F30CF
		internal override BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			return this;
		}

		// Token: 0x06004568 RID: 17768 RVA: 0x000F4ED2 File Offset: 0x000F30D2
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.SlotName);
		}

		// Token: 0x040018E5 RID: 6373
		private readonly int m_index;

		// Token: 0x040018E6 RID: 6374
		private readonly string m_slotName;
	}
}
