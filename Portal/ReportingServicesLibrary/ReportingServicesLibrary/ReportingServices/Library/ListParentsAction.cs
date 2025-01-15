using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000C4 RID: 196
	internal sealed class ListParentsAction : RSSoapAction<ListParentsActionParameters>
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x000220C3 File Offset: 0x000202C3
		internal ListParentsAction(RSService service)
			: base("ListParentsAction", service)
		{
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000220D4 File Offset: 0x000202D4
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			RSTrace.CatalogTrace.Assert(!catalogItemContext.ItemPath.IsEditSession, "!itemContext.ItemPath.IsEditSession");
			try
			{
				ItemType itemType;
				byte[] array;
				if (!base.Service.Storage.ObjectExists(catalogItemContext.ItemPath, out itemType, out array))
				{
					throw new ItemNotFoundException(catalogItemContext.OriginalItemPath.Value);
				}
			}
			catch (AccessDeniedException)
			{
				base.ActionParameters.Parents = new CatalogItemList();
				return;
			}
			CatalogItemList catalogItemList = null;
			if (!base.ActionParameters.ItemPath.Equals("/", StringComparison.OrdinalIgnoreCase))
			{
				base.Service.Storage.FindParents(catalogItemContext.ItemPath, out catalogItemList, base.Service.SecMgr, base.Service);
			}
			else
			{
				catalogItemList = new CatalogItemList();
			}
			base.ActionParameters.Parents = catalogItemList;
		}
	}
}
