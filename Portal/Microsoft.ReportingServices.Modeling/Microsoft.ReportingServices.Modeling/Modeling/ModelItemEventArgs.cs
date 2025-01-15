using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000098 RID: 152
	public class ModelItemEventArgs : EventArgs
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x00017ECD File Offset: 0x000160CD
		public ModelItemEventArgs(IList<ModelItem> affectedItems)
		{
			if (affectedItems == null)
			{
				throw new ArgumentNullException();
			}
			this.m_affectedItems = new ReadOnlyCollection<ModelItem>(affectedItems);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00017EEA File Offset: 0x000160EA
		public ModelItemEventArgs(params ModelItem[] affectedItems)
			: this(affectedItems)
		{
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00017EF3 File Offset: 0x000160F3
		public ReadOnlyCollection<ModelItem> AffectedItems
		{
			get
			{
				return this.m_affectedItems;
			}
		}

		// Token: 0x04000382 RID: 898
		private readonly ReadOnlyCollection<ModelItem> m_affectedItems;
	}
}
