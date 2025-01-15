using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000DE RID: 222
	internal class GetComponentDefinitionAction : RSSoapAction<GetComponentDefinitionActionParameters>
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x00025D36 File Offset: 0x00023F36
		public GetComponentDefinitionAction(RSService service)
			: base("GetComponentDefinition", service)
		{
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00025D44 File Offset: 0x00023F44
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Component");
			ComponentCatalogItem componentCatalogItem = (ComponentCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Component, true);
			componentCatalogItem.LoadDefinition();
			base.ActionParameters.ComponentDefinition = componentCatalogItem.Content;
		}
	}
}
