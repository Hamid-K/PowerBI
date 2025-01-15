using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001F9 RID: 505
	internal sealed class GetPermissionsAction : RSSoapAction<GetPermissionsActionParameters>
	{
		// Token: 0x06001117 RID: 4375 RVA: 0x0003B008 File Offset: 0x00039208
		internal GetPermissionsAction(RSService service)
			: base("GetPermissionsAction", service)
		{
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0003B018 File Offset: 0x00039218
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			RSTrace.CatalogTrace.Assert(!catalogItemContext.ItemPath.IsEditSession, "!itemContext.ItemPath.IsEditSession");
			ItemType itemType;
			byte[] array;
			if (!base.Service.Storage.ObjectExists(catalogItemContext.ItemPath, out itemType, out array))
			{
				throw new ItemNotFoundException(catalogItemContext.OriginalItemPath.Value);
			}
			base.ActionParameters.Operations = base.Service.SecMgr.GetCatalogItemPermissions(itemType, array, catalogItemContext.ItemPath);
		}
	}
}
