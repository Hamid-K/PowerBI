using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000097 RID: 151
	public sealed class PerspectiveCollection : OwnedModelItemCollection<Perspective>
	{
		// Token: 0x06000759 RID: 1881 RVA: 0x00017EB5 File Offset: 0x000160B5
		internal PerspectiveCollection(ModelItem parentItem)
			: base(parentItem)
		{
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00017EBE File Offset: 0x000160BE
		internal PerspectiveCollection CloneFor(ModelItem newParentItem)
		{
			PerspectiveCollection perspectiveCollection = new PerspectiveCollection(newParentItem);
			perspectiveCollection.CopyFromBase(this);
			return perspectiveCollection;
		}
	}
}
