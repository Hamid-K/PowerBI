using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000536 RID: 1334
	internal class GlobalIDOwnerCollection
	{
		// Token: 0x060048F7 RID: 18679 RVA: 0x0013476B File Offset: 0x0013296B
		internal GlobalIDOwnerCollection()
		{
			this.m_globallyReferenceableItems = new Dictionary<int, IGloballyReferenceable>(EqualityComparers.Int32ComparerInstance);
		}

		// Token: 0x17001DCB RID: 7627
		// (get) Token: 0x060048F8 RID: 18680 RVA: 0x0013478A File Offset: 0x0013298A
		internal int LastAssignedID
		{
			get
			{
				return this.m_currentID;
			}
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x00134794 File Offset: 0x00132994
		internal int GetGlobalID()
		{
			int num = this.m_currentID + 1;
			this.m_currentID = num;
			return num;
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x001347B2 File Offset: 0x001329B2
		internal void Add(IGloballyReferenceable globallyReferenceableItem)
		{
			this.m_globallyReferenceableItems.Add(this.m_currentID, globallyReferenceableItem);
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x001347C6 File Offset: 0x001329C6
		internal bool TryGetValue(int refID, out IGloballyReferenceable referenceableItem)
		{
			return this.m_globallyReferenceableItems.TryGetValue(refID, out referenceableItem);
		}

		// Token: 0x04002060 RID: 8288
		private int m_currentID = -1;

		// Token: 0x04002061 RID: 8289
		private Dictionary<int, IGloballyReferenceable> m_globallyReferenceableItems;
	}
}
