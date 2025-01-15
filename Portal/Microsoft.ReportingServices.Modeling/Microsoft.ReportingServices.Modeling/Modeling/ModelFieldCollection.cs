using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000089 RID: 137
	public sealed class ModelFieldCollection : OwnedModelItemCollection<ModelField>
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x00013CF4 File Offset: 0x00011EF4
		internal ModelFieldCollection(ModelItem parentItem)
			: base(parentItem)
		{
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00013CFD File Offset: 0x00011EFD
		internal ModelFieldCollection CloneFor(ModelItem newParentItem)
		{
			ModelFieldCollection modelFieldCollection = new ModelFieldCollection(newParentItem);
			modelFieldCollection.CopyFromBase(this);
			return modelFieldCollection;
		}
	}
}
