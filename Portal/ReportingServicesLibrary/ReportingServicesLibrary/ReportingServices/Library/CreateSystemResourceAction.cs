using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000064 RID: 100
	internal sealed class CreateSystemResourceAction : CreateSystemResourceActionBase<CreateSystemResourceActionParameters, ResourceCatalogItem>
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x00011665 File Offset: 0x0000F865
		internal CreateSystemResourceAction(RSService service)
			: base("CreateSystemResourceAction", service)
		{
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00011674 File Offset: 0x0000F874
		protected override void AddActionToBatch()
		{
			CreateSystemResourceActionParameters actionParameters = base.ActionParameters;
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateResource, actionParameters.ItemName, "Resource", actionParameters.ParentPath, "Parent", actionParameters.MimeType, "MimeType", actionParameters.Overwrite, actionParameters.Content, Property.ThisArrayToXml(actionParameters.Properties));
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000116E4 File Offset: 0x0000F8E4
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			CreateSystemResourceActionParameters actionParameters = base.ActionParameters;
			actionParameters.ItemName = parameters.Item;
			actionParameters.ParentPath = parameters.Parent;
			actionParameters.Overwrite = parameters.BoolParam;
			actionParameters.Content = parameters.Content;
			actionParameters.MimeType = parameters.Param;
			actionParameters.Properties = Property.XmlToThisArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001174C File Offset: 0x0000F94C
		internal override void PerformActionNow()
		{
			string parentPath = base.ActionParameters.ParentPath;
			if (new CatalogItemContext(base.Service, parentPath, "parent").ItemPath.Value.Equals("/68f0607b-9378-4bbb-9e70-4da3d7d66838"))
			{
				throw new InvalidItemPathException(parentPath);
			}
			base.PerformActionNow();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00011799 File Offset: 0x0000F999
		protected override void InitAndCheckParams()
		{
			base.Service.EnsureValidMimeType(base.ActionParameters.MimeType);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000117B4 File Offset: 0x0000F9B4
		protected override void PrepareForNewItem(ResourceCatalogItem item)
		{
			CreateSystemResourceActionParameters actionParameters = base.ActionParameters;
			item.Content = actionParameters.Content;
			item.MimeType = actionParameters.MimeType;
		}
	}
}
