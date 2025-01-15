using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000090 RID: 144
	public interface IOwnedModelItemCollection : IList<ModelItem>, ICollection<ModelItem>, IEnumerable<ModelItem>, IEnumerable
	{
		// Token: 0x1700017F RID: 383
		ModelItem this[string name] { get; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006AB RID: 1707
		ModelItem ParentItem { get; }

		// Token: 0x060006AC RID: 1708
		bool CanContain(ModelItem item);
	}
}
