using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000F0 RID: 240
	internal abstract class MoveCopyItemRequest<TResponse> : MoveCopyRequest<Item, TResponse> where TResponse : ServiceResponse
	{
		// Token: 0x06000C27 RID: 3111 RVA: 0x00028861 File Offset: 0x00027861
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.ItemIds, "ItemIds");
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00028879 File Offset: 0x00027879
		internal MoveCopyItemRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00028890 File Offset: 0x00027890
		internal override void WriteIdsToXml(EwsServiceXmlWriter writer)
		{
			this.ItemIds.WriteToXml(writer, XmlNamespace.Messages, "ItemIds");
			if (this.ReturnNewItemIds != null)
			{
				writer.WriteElementValue(XmlNamespace.Messages, "ReturnNewItemIds", this.ReturnNewItemIds.Value);
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000288E0 File Offset: 0x000278E0
		internal override void AddIdsToJson(JsonObject jsonObject, ExchangeService service)
		{
			jsonObject.Add("ItemIds", this.ItemIds.InternalToJson(service));
			if (this.ReturnNewItemIds != null)
			{
				jsonObject.Add("ReturnNewItemIds", this.ReturnNewItemIds.Value);
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002892D File Offset: 0x0002792D
		internal override int GetExpectedResponseMessageCount()
		{
			return this.ItemIds.Count;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0002893A File Offset: 0x0002793A
		internal ItemIdWrapperList ItemIds
		{
			get
			{
				return this.itemIds;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x00028942 File Offset: 0x00027942
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x0002894A File Offset: 0x0002794A
		internal bool? ReturnNewItemIds { get; set; }

		// Token: 0x040008C2 RID: 2242
		private ItemIdWrapperList itemIds = new ItemIdWrapperList();
	}
}
