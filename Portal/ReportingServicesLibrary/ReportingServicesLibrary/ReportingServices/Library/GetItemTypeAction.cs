using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000B8 RID: 184
	internal sealed class GetItemTypeAction : RSSoapAction<GetItemTypeActionParameters>
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x0002185C File Offset: 0x0001FA5C
		public GetItemTypeAction(RSService service)
			: base("GetItemTypeAction", service)
		{
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002186C File Offset: 0x0001FA6C
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service);
			ItemPathOptions itemPathOptions = ItemPathOptions.Default;
			if (base.ActionParameters.AllowEditSessions)
			{
				itemPathOptions |= ItemPathOptions.AllowEditSessionSyntax;
			}
			if (!catalogItemContext.SetPath(base.ActionParameters.ItemPath, itemPathOptions))
			{
				throw new InvalidItemPathException(base.ActionParameters.ItemPath);
			}
			ItemType itemType;
			byte[] array;
			if (base.Service.Storage.ObjectExists(catalogItemContext.ItemPath, out itemType, out array) && !base.Service.SecMgr.CheckAccess(itemType, array, CommonOperation.ReadProperties, catalogItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			base.ActionParameters.ItemType = (int)itemType;
		}
	}
}
