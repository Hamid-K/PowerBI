using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000581 RID: 1409
	internal class ViewCellSlot : ProjectedSlot
	{
		// Token: 0x06004421 RID: 17441 RVA: 0x000EF848 File Offset: 0x000EDA48
		internal ViewCellSlot(int slotNum, MemberProjectedSlot cSlot, MemberProjectedSlot sSlot)
		{
			this.m_slotNum = slotNum;
			this.m_cSlot = cSlot;
			this.m_sSlot = sSlot;
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06004422 RID: 17442 RVA: 0x000EF865 File Offset: 0x000EDA65
		internal MemberProjectedSlot CSlot
		{
			get
			{
				return this.m_cSlot;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06004423 RID: 17443 RVA: 0x000EF86D File Offset: 0x000EDA6D
		internal MemberProjectedSlot SSlot
		{
			get
			{
				return this.m_sSlot;
			}
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x000EF878 File Offset: 0x000EDA78
		protected override bool IsEqualTo(ProjectedSlot right)
		{
			ViewCellSlot viewCellSlot = right as ViewCellSlot;
			return viewCellSlot != null && (this.m_slotNum == viewCellSlot.m_slotNum && ProjectedSlot.EqualityComparer.Equals(this.m_cSlot, viewCellSlot.m_cSlot)) && ProjectedSlot.EqualityComparer.Equals(this.m_sSlot, viewCellSlot.m_sSlot);
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x000EF8CF File Offset: 0x000EDACF
		protected override int GetHash()
		{
			return ProjectedSlot.EqualityComparer.GetHashCode(this.m_cSlot) ^ ProjectedSlot.EqualityComparer.GetHashCode(this.m_sSlot) ^ this.m_slotNum;
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x000EF8FC File Offset: 0x000EDAFC
		internal static string SlotsToUserString(IEnumerable<ViewCellSlot> slots, bool isFromCside)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (ViewCellSlot viewCellSlot in slots)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(ViewCellSlot.SlotToUserString(viewCellSlot, isFromCside));
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x000EF96C File Offset: 0x000EDB6C
		internal static string SlotToUserString(ViewCellSlot slot, bool isFromCside)
		{
			MemberProjectedSlot memberProjectedSlot = (isFromCside ? slot.CSlot : slot.SSlot);
			return StringUtil.FormatInvariant("{0}", new object[] { memberProjectedSlot });
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x000EF99F File Offset: 0x000EDB9F
		internal override string GetCqlFieldAlias(MemberPath outputMember)
		{
			return null;
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x000EF9A2 File Offset: 0x000EDBA2
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			return null;
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x000EF9A5 File Offset: 0x000EDBA5
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return null;
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x000EF9A8 File Offset: 0x000EDBA8
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append('<');
			StringUtil.FormatStringBuilder(builder, "{0}", new object[] { this.m_slotNum });
			builder.Append(':');
			this.m_cSlot.ToCompactString(builder);
			builder.Append('-');
			this.m_sSlot.ToCompactString(builder);
			builder.Append('>');
		}

		// Token: 0x04001891 RID: 6289
		private readonly int m_slotNum;

		// Token: 0x04001892 RID: 6290
		private readonly MemberProjectedSlot m_cSlot;

		// Token: 0x04001893 RID: 6291
		private readonly MemberProjectedSlot m_sSlot;
	}
}
