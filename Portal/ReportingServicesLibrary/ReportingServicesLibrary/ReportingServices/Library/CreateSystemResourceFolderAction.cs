using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000051 RID: 81
	internal sealed class CreateSystemResourceFolderAction : CreateSystemResourceActionBase<CreateSystemResourceFolderActionParameters, FolderCatalogItem>
	{
		// Token: 0x0600039C RID: 924 RVA: 0x0001026A File Offset: 0x0000E46A
		internal CreateSystemResourceFolderAction(RSService service)
			: base("CreateSystemResourceFolderAction", service)
		{
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00010278 File Offset: 0x0000E478
		protected override void AddActionToBatch()
		{
			CreateSystemResourceFolderActionParameters actionParameters = base.ActionParameters;
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateFolder, actionParameters.ItemName, "Folder", actionParameters.ParentPath, "Parent", null, null, false, null, Property.ThisArrayToXml(actionParameters.Properties));
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000102D3 File Offset: 0x0000E4D3
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			CreateSystemResourceFolderActionParameters actionParameters = base.ActionParameters;
			actionParameters.ItemName = parameters.Item;
			actionParameters.ParentPath = parameters.Parent;
			actionParameters.Properties = Property.XmlToThisArray(parameters.Properties);
			this.PerformActionNow();
		}
	}
}
