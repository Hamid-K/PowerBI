using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200057E RID: 1406
	internal class KeyConstraint<TCellRelation, TSlot> : InternalBase where TCellRelation : CellRelation
	{
		// Token: 0x06004413 RID: 17427 RVA: 0x000EF65A File Offset: 0x000ED85A
		internal KeyConstraint(TCellRelation relation, IEnumerable<TSlot> keySlots, IEqualityComparer<TSlot> comparer)
		{
			this.m_relation = relation;
			this.m_keySlots = new Set<TSlot>(keySlots, comparer).MakeReadOnly();
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x000EF67B File Offset: 0x000ED87B
		protected TCellRelation CellRelation
		{
			get
			{
				return this.m_relation;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x000EF683 File Offset: 0x000ED883
		protected Set<TSlot> KeySlots
		{
			get
			{
				return this.m_keySlots;
			}
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x000EF68B File Offset: 0x000ED88B
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.FormatStringBuilder(builder, "Key (V{0}) - ", new object[] { this.m_relation.CellNumber });
			StringUtil.ToSeparatedStringSorted(builder, this.KeySlots, ", ");
		}

		// Token: 0x0400188C RID: 6284
		private readonly TCellRelation m_relation;

		// Token: 0x0400188D RID: 6285
		private readonly Set<TSlot> m_keySlots;
	}
}
