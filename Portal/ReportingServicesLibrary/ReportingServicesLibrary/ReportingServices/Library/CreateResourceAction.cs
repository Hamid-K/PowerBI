using System;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001CD RID: 461
	internal sealed class CreateResourceAction : CreateItemAction<CreateResourceActionParameters, ResourceCatalogItem>
	{
		// Token: 0x0600101F RID: 4127 RVA: 0x00039193 File Offset: 0x00037393
		internal CreateResourceAction(RSService service)
			: base("CreateResourceAction", service)
		{
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000391A4 File Offset: 0x000373A4
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateResource, base.ActionParameters.ItemName, "Resource", base.ActionParameters.ParentPath, "Parent", base.ActionParameters.MimeType, "MimeType", base.ActionParameters.Overwrite, base.ActionParameters.Content, Property.ThisArrayToXml(base.ActionParameters.Properties));
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0003922C File Offset: 0x0003742C
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemName = parameters.Item;
			base.ActionParameters.ParentPath = parameters.Parent;
			base.ActionParameters.Overwrite = parameters.BoolParam;
			base.ActionParameters.Content = parameters.Content;
			base.ActionParameters.MimeType = parameters.Param;
			base.ActionParameters.Properties = Property.XmlToThisArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool IsUpdateSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000392AC File Offset: 0x000374AC
		protected override void InitAndCheckParams()
		{
			base.Service.EnsureValidMimeType(base.ActionParameters.MimeType);
			base.Service.ThrowIfResctrictedMimeType(base.ActionParameters.MimeType);
			if (base.Service.IsCommentAttachment(base.ActionParameters.ParentPath, ItemType.Resource, base.ActionParameters.SubType))
			{
				CommentRestrictions.EnsureValidCommentAttachment(base.ActionParameters.MimeType, base.ActionParameters.ItemName);
			}
			base.Service.ThrowIfInvalidFileFormat(base.ActionParameters.ItemName);
			if (!base.Service.IsTrustedFileType(base.ActionParameters.ItemName))
			{
				base.ActionParameters.MimeType = "application/octet-stream";
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00039362 File Offset: 0x00037562
		protected override void UpdateExistingItem(ResourceCatalogItem item)
		{
			SetResourceContentsAction setResourceContentsAction = base.Service.SetResourceContentsAction;
			setResourceContentsAction.ActionParameters.Definition = base.ActionParameters.Content;
			setResourceContentsAction.ActionParameters.MimeType = base.ActionParameters.MimeType;
			setResourceContentsAction.Update(item);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000393A4 File Offset: 0x000375A4
		protected override void PrepareForNewItem(ResourceCatalogItem item)
		{
			item.Content = base.ActionParameters.Content;
			item.MimeType = base.ActionParameters.MimeType;
			if (base.ActionParameters.SubType != null)
			{
				item.ItemMetadata.SubType = base.ActionParameters.SubType;
			}
		}
	}
}
