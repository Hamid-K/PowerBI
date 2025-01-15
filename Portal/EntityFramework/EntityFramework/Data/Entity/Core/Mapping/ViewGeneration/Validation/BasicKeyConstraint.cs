using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000577 RID: 1399
	internal class BasicKeyConstraint : KeyConstraint<BasicCellRelation, MemberProjectedSlot>
	{
		// Token: 0x060043D8 RID: 17368 RVA: 0x000ED048 File Offset: 0x000EB248
		internal BasicKeyConstraint(BasicCellRelation relation, IEnumerable<MemberProjectedSlot> keySlots)
			: base(relation, keySlots, ProjectedSlot.EqualityComparer)
		{
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x000ED058 File Offset: 0x000EB258
		internal ViewKeyConstraint Propagate()
		{
			ViewCellRelation viewCellRelation = base.CellRelation.ViewCellRelation;
			List<ViewCellSlot> list = new List<ViewCellSlot>();
			foreach (MemberProjectedSlot memberProjectedSlot in base.KeySlots)
			{
				ViewCellSlot viewCellSlot = viewCellRelation.LookupViewSlot(memberProjectedSlot);
				if (viewCellSlot == null)
				{
					return null;
				}
				list.Add(viewCellSlot);
			}
			return new ViewKeyConstraint(viewCellRelation, list);
		}
	}
}
