using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000087 RID: 135
	public sealed class ModelEntityFolderItemCollection : OwnedModelItemCollection<ModelEntityFolderItem>
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x000138C6 File Offset: 0x00011AC6
		internal ModelEntityFolderItemCollection(ModelItem parentItem)
			: base(parentItem)
		{
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000138CF File Offset: 0x00011ACF
		internal ModelEntityFolderItemCollection CloneFor(ModelItem newParentItem)
		{
			ModelEntityFolderItemCollection modelEntityFolderItemCollection = new ModelEntityFolderItemCollection(newParentItem);
			modelEntityFolderItemCollection.CopyFromBase(this);
			return modelEntityFolderItemCollection;
		}
	}
}
