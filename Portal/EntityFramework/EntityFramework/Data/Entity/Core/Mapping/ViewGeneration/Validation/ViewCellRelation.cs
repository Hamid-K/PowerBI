using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000580 RID: 1408
	internal class ViewCellRelation : CellRelation
	{
		// Token: 0x0600441C RID: 17436 RVA: 0x000EF75A File Offset: 0x000ED95A
		internal ViewCellRelation(Cell cell, List<ViewCellSlot> slots, int cellNumber)
			: base(cellNumber)
		{
			this.m_cell = cell;
			this.m_slots = slots;
			this.m_cell.CQuery.CreateBasicCellRelation(this);
			this.m_cell.SQuery.CreateBasicCellRelation(this);
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x000EF793 File Offset: 0x000ED993
		internal Cell Cell
		{
			get
			{
				return this.m_cell;
			}
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x000EF79C File Offset: 0x000ED99C
		internal ViewCellSlot LookupViewSlot(MemberProjectedSlot slot)
		{
			foreach (ViewCellSlot viewCellSlot in this.m_slots)
			{
				if (ProjectedSlot.EqualityComparer.Equals(slot, viewCellSlot.CSlot) || ProjectedSlot.EqualityComparer.Equals(slot, viewCellSlot.SSlot))
				{
					return viewCellSlot;
				}
			}
			return null;
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x000EF818 File Offset: 0x000EDA18
		protected override int GetHash()
		{
			return this.m_cell.GetHashCode();
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x000EF825 File Offset: 0x000EDA25
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("ViewRel[");
			this.m_cell.ToCompactString(builder);
			builder.Append(']');
		}

		// Token: 0x0400188F RID: 6287
		private readonly Cell m_cell;

		// Token: 0x04001890 RID: 6288
		private readonly List<ViewCellSlot> m_slots;
	}
}
