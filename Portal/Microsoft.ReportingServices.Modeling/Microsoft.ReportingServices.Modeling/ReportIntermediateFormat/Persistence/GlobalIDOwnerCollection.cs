using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200001B RID: 27
	internal class GlobalIDOwnerCollection
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00005B97 File Offset: 0x00003D97
		internal GlobalIDOwnerCollection()
		{
			this.m_globallyReferenceableItems = new Dictionary<int, IGloballyReferenceable>(EqualityComparers.Int32ComparerInstance);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005BB6 File Offset: 0x00003DB6
		internal int LastAssignedID
		{
			get
			{
				return this.m_currentID;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005BC0 File Offset: 0x00003DC0
		internal int GetGlobalID()
		{
			int num = this.m_currentID + 1;
			this.m_currentID = num;
			return num;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005BDE File Offset: 0x00003DDE
		internal void Add(IGloballyReferenceable globallyReferenceableItem)
		{
			this.m_globallyReferenceableItems.Add(this.m_currentID, globallyReferenceableItem);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal bool TryGetValue(int refID, out IGloballyReferenceable referenceableItem)
		{
			return this.m_globallyReferenceableItems.TryGetValue(refID, out referenceableItem);
		}

		// Token: 0x040000E6 RID: 230
		private int m_currentID = -1;

		// Token: 0x040000E7 RID: 231
		private Dictionary<int, IGloballyReferenceable> m_globallyReferenceableItems;
	}
}
