using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000C0 RID: 192
	internal sealed class ListDependentItemsAction : RSSoapAction<ListDependentItemsActionParameters>
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x00021CA6 File Offset: 0x0001FEA6
		internal ListDependentItemsAction(RSService service)
			: base("ListDependentItems", service)
		{
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00021CB4 File Offset: 0x0001FEB4
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			RSTrace.CatalogTrace.Assert(!catalogItemContext.ItemPath.IsEditSession, "!itemContext.ItemPath.IsEditSession");
			Guid guid;
			byte[] array;
			if (!base.Service.Storage.ObjectExists(catalogItemContext.ItemPath, out this.m_itemType, out guid, out array))
			{
				throw new ItemNotFoundException(catalogItemContext.OriginalItemPath.Value);
			}
			if (!base.Service.SecMgr.CheckAccess(this.m_itemType, array, CommonOperation.ReadProperties, catalogItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			CatalogItemList catalogItemList;
			switch (this.m_itemType)
			{
			case ItemType.Report:
				base.Service.Storage.FindObjectsByLink(guid, out catalogItemList, base.Service.SecMgr, base.Service);
				goto IL_015A;
			case ItemType.DataSource:
			case ItemType.Model:
				base.Service.Storage.FindItemsByDataSource(guid, out catalogItemList, base.Service.SecMgr, base.Service, !base.ActionParameters.DirectDependentItemsOnly);
				goto IL_015A;
			case ItemType.DataSet:
				base.Service.Storage.FindItemsByDataSet(guid, out catalogItemList, base.Service.SecMgr, base.Service);
				goto IL_015A;
			}
			catalogItemList = new CatalogItemList();
			IL_015A:
			base.ActionParameters.DependentItems = catalogItemList;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00021E27 File Offset: 0x00020027
		public void EnsureItemIsReport()
		{
			RSService.EnsureItemType(this.m_itemType, base.ActionParameters.ItemPath, new ItemType[] { ItemType.Report });
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00021E49 File Offset: 0x00020049
		public void EnsureItemIsDataSource()
		{
			RSService.EnsureItemType(this.m_itemType, base.ActionParameters.ItemPath, new ItemType[] { ItemType.DataSource });
		}

		// Token: 0x04000427 RID: 1063
		private ItemType m_itemType;
	}
}
