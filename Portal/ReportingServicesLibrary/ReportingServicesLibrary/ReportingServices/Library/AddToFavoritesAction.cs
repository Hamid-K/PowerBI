using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000020 RID: 32
	internal sealed class AddToFavoritesAction : RSSoapAction<AddToFavoritesActionParameters>
	{
		// Token: 0x060000BB RID: 187 RVA: 0x000053C2 File Offset: 0x000035C2
		internal AddToFavoritesAction(RSService service)
			: base("AddToFavoritesAction", service)
		{
			this.m_requestInspector = service.RequestInspector;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000053E0 File Offset: 0x000035E0
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
			base.ActionParameters.Status = base.Service.Storage.AddToFavorites(base.Service.UserName, base.ActionParameters.ItemId);
		}

		// Token: 0x040000B4 RID: 180
		private readonly IRSRequestInspector m_requestInspector;
	}
}
