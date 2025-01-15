using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000132 RID: 306
	internal sealed class CreateFolderAction : CreateItemAction<CreateFolderActionParameters, FolderCatalogItem>
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x0002E169 File Offset: 0x0002C369
		internal CreateFolderAction(RSService service)
			: base("CreateFolderAction", service)
		{
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002E178 File Offset: 0x0002C378
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateFolder, base.ActionParameters.ItemName, "Folder", base.ActionParameters.ParentPath, "Parent", null, null, false, null, Property.ThisArrayToXml(base.ActionParameters.Properties));
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002E1DB File Offset: 0x0002C3DB
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemName = parameters.Item;
			base.ActionParameters.ParentPath = parameters.Parent;
			base.ActionParameters.Properties = Property.XmlToThisArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002E21B File Offset: 0x0002C41B
		protected override void PrepareForNewItem(FolderCatalogItem item)
		{
			if (base.ActionParameters.SubType != null)
			{
				item.ItemMetadata.SubType = base.ActionParameters.SubType;
			}
		}
	}
}
