using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000022 RID: 34
	internal sealed class RemoveFromFavoritesAction : RSSoapAction<RemoveFromFavoritesActionParameters>
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x000054C9 File Offset: 0x000036C9
		internal RemoveFromFavoritesAction(RSService service)
			: base("RemoveFromFavoritesAction", service)
		{
			this.m_requestInspector = service.RequestInspector;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000054E4 File Offset: 0x000036E4
		internal override void PerformActionNow()
		{
			string value = base.Service.Storage.GetPathById(base.ActionParameters.ItemId).Value;
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service);
			if (!catalogItemContext.SetPath(value))
			{
				throw new InvalidItemPathException(value);
			}
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfNoAccess(CommonOperation.ReadProperties);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.MobileReport,
				ItemType.Kpi,
				ItemType.LinkedReport,
				ItemType.DataSource,
				ItemType.DataSet,
				ItemType.Resource,
				ItemType.PowerBIReport,
				ItemType.ExcelWorkbook
			});
			base.ActionParameters.Status = base.Service.Storage.RemoveFromFavorites(base.Service.UserName, base.ActionParameters.ItemId);
		}

		// Token: 0x040000B7 RID: 183
		private readonly IRSRequestInspector m_requestInspector;
	}
}
