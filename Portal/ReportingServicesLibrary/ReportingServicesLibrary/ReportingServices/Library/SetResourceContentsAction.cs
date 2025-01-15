using System;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001D1 RID: 465
	internal sealed class SetResourceContentsAction : UpdateItemAction<SetResourceContentsActionParameters, ResourceCatalogItem>
	{
		// Token: 0x0600103A RID: 4154 RVA: 0x000395B2 File Offset: 0x000377B2
		internal SetResourceContentsAction(RSService service)
			: base("SetResourceContentsAction", service)
		{
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000395C0 File Offset: 0x000377C0
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetResourceContents, base.ActionParameters.ItemPath, "Resource", null, null, base.ActionParameters.MimeType, "MimeType", false, base.ActionParameters.Definition, null);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0003961F File Offset: 0x0003781F
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			base.ActionParameters.Definition = parameters.Content;
			base.ActionParameters.MimeType = parameters.Param;
			this.PerformActionNow();
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0003965C File Offset: 0x0003785C
		internal override void Update(ResourceCatalogItem resource)
		{
			base.Service.EnsureValidMimeType(base.ActionParameters.MimeType);
			base.Service.ThrowIfInvalidFileFormat(resource.ItemContext.ItemName);
			base.Service.ThrowIfResctrictedMimeType(base.ActionParameters.MimeType);
			if (resource.Parent != null && base.Service.IsCommentAttachment(resource.Parent.ItemContext.ItemPathAsString, ItemType.Resource, resource.Properties.SubType))
			{
				CommentRestrictions.EnsureValidCommentAttachment(base.ActionParameters.MimeType, resource.ItemContext.ItemName);
			}
			if (!base.Service.IsTrustedFileType(resource.ItemContext.ItemName))
			{
				base.ActionParameters.MimeType = "application/octet-stream";
			}
			resource.ThrowIfNoAccess(ResourceOperation.UpdateContent);
			resource.Content = base.ActionParameters.Definition;
			resource.MimeType = base.ActionParameters.MimeType;
			resource.Save(false);
		}
	}
}
