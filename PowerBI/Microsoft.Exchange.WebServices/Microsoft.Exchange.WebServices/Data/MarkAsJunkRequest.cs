using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200012F RID: 303
	internal sealed class MarkAsJunkRequest : MultiResponseServiceRequest<MarkAsJunkResponse>, IJsonSerializable
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x0002C85B File Offset: 0x0002B85B
		internal MarkAsJunkRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0002C870 File Offset: 0x0002B870
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.ItemIds, "ItemIds");
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0002C888 File Offset: 0x0002B888
		internal override MarkAsJunkResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new MarkAsJunkResponse();
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0002C88F File Offset: 0x0002B88F
		internal override string GetXmlElementName()
		{
			return "MarkAsJunk";
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0002C896 File Offset: 0x0002B896
		internal override string GetResponseXmlElementName()
		{
			return "MarkAsJunkResponse";
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002C89D File Offset: 0x0002B89D
		internal override string GetResponseMessageXmlElementName()
		{
			return "MarkAsJunkResponseMessage";
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0002C8A4 File Offset: 0x0002B8A4
		internal override int GetExpectedResponseMessageCount()
		{
			return this.itemIds.Count;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0002C8B1 File Offset: 0x0002B8B1
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("IsJunk", this.IsJunk);
			writer.WriteAttributeValue("MoveItem", this.MoveItem);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0002C8DF File Offset: 0x0002B8DF
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.itemIds.WriteToXml(writer, XmlNamespace.Messages, "ItemIds");
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0002C8F4 File Offset: 0x0002B8F4
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("ItemIds", this.ItemIds.InternalToJson(service));
			return jsonObject;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0002C91F File Offset: 0x0002B91F
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0002C922 File Offset: 0x0002B922
		internal ItemIdWrapperList ItemIds
		{
			get
			{
				return this.itemIds;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0002C92A File Offset: 0x0002B92A
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x0002C932 File Offset: 0x0002B932
		internal bool IsJunk { get; set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0002C93B File Offset: 0x0002B93B
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x0002C943 File Offset: 0x0002B943
		internal bool MoveItem { get; set; }

		// Token: 0x0400093D RID: 2365
		private ItemIdWrapperList itemIds = new ItemIdWrapperList();
	}
}
