using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200008C RID: 140
	public sealed class ModelFieldFolderItemCollection : OwnedModelItemCollection<ModelFieldFolderItem>
	{
		// Token: 0x06000645 RID: 1605 RVA: 0x000140BA File Offset: 0x000122BA
		internal ModelFieldFolderItemCollection(ModelItem parentItem)
			: base(parentItem)
		{
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000140C3 File Offset: 0x000122C3
		internal ModelFieldFolderItemCollection CloneFor(ModelItem newParentItem)
		{
			ModelFieldFolderItemCollection modelFieldFolderItemCollection = new ModelFieldFolderItemCollection(newParentItem);
			modelFieldFolderItemCollection.CopyFromBase(this);
			return modelFieldFolderItemCollection;
		}
	}
}
