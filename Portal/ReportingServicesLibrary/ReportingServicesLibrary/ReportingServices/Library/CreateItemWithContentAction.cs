using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000049 RID: 73
	internal abstract class CreateItemWithContentAction<TParameterType, TCreatedType> : CreateItemAction<TParameterType, TCreatedType> where TParameterType : CreateItemWithContentActionParameters, new() where TCreatedType : CatalogItem
	{
		// Token: 0x0600037B RID: 891 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		public CreateItemWithContentAction(string actionName, RSService service)
			: base(actionName, service)
		{
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		protected override void CreateItem(TCreatedType item)
		{
			if (base.ActionParameters.CatalogItemContent.IsStreamContent)
			{
				long num2;
				long num = base.Service.Storage.CreateExtendedCatalogContent(ExtendedContentType.CatalogItem, base.ActionParameters.CatalogItemContent.ContentStream, out num2);
				item.Content = null;
				base.CreateItem(item);
				base.Service.Storage.FinalizeNewExtendedCatalogContent(num, item.ItemID);
				base.Service.Storage.WriteContentSize(item.ItemID, num2);
				return;
			}
			item.Content = base.ActionParameters.CatalogItemContent.Content;
			base.CreateItem(item);
		}
	}
}
