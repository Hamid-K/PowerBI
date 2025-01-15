using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000118 RID: 280
	internal abstract class GetItemRequestBase<TResponse> : GetRequest<Item, TResponse> where TResponse : ServiceResponse
	{
		// Token: 0x06000DD2 RID: 3538 RVA: 0x0002B46C File Offset: 0x0002A46C
		protected GetItemRequestBase(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0002B481 File Offset: 0x0002A481
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParamCollection(this.ItemIds, "ItemIds");
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0002B499 File Offset: 0x0002A499
		internal override int GetExpectedResponseMessageCount()
		{
			return this.ItemIds.Count;
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0002B4A6 File Offset: 0x0002A4A6
		internal override ServiceObjectType GetServiceObjectType()
		{
			return ServiceObjectType.Item;
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0002B4A9 File Offset: 0x0002A4A9
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			this.ItemIds.WriteToXml(writer, XmlNamespace.Messages, "ItemIds");
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0002B4C4 File Offset: 0x0002A4C4
		internal override void AddIdsToRequest(JsonObject jsonRequest, ExchangeService service)
		{
			jsonRequest.Add("ItemIds", this.ItemIds.InternalToJson(service));
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0002B4DD File Offset: 0x0002A4DD
		internal override string GetXmlElementName()
		{
			return "GetItem";
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0002B4E4 File Offset: 0x0002A4E4
		internal override string GetResponseXmlElementName()
		{
			return "GetItemResponse";
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0002B4EB File Offset: 0x0002A4EB
		internal override string GetResponseMessageXmlElementName()
		{
			return "GetItemResponseMessage";
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0002B4F2 File Offset: 0x0002A4F2
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0002B4F5 File Offset: 0x0002A4F5
		public ItemIdWrapperList ItemIds
		{
			get
			{
				return this.itemIds;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0002B4FD File Offset: 0x0002A4FD
		internal override bool EmitTimeZoneHeader
		{
			get
			{
				return base.PropertySet.Contains(ItemSchema.MimeContent);
			}
		}

		// Token: 0x0400090D RID: 2317
		private ItemIdWrapperList itemIds = new ItemIdWrapperList();
	}
}
