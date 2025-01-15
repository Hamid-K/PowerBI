using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200004B RID: 75
	internal abstract class UpdateItemContentAction<TParameter, TItem> : UpdateItemAction<TParameter, TItem> where TParameter : UpdateItemContentActionParameters, new() where TItem : CatalogItem
	{
		// Token: 0x06000382 RID: 898 RVA: 0x0000FCD4 File Offset: 0x0000DED4
		internal UpdateItemContentAction(string actionName, RSService service)
			: base(actionName, service)
		{
		}

		// Token: 0x06000383 RID: 899
		protected abstract void ValidateAccess(TItem item);

		// Token: 0x06000384 RID: 900 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void PrepareForUpdate(TItem item)
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void InitAndCheckParams()
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		internal override void Update(TItem item)
		{
			this.InitAndCheckParams();
			this.ValidateAccess(item);
			this.PrepareForUpdate(item);
			if (base.ActionParameters.CatalogItemContent.IsStreamContent)
			{
				base.Service.Storage.WriteExtendedCatalogContent(item.ItemID, ExtendedContentType.CatalogItem, base.ActionParameters.CatalogItemContent.ContentStream, null);
				return;
			}
			item.Content = base.ActionParameters.CatalogItemContent.Content;
			item.Save(false);
		}
	}
}
