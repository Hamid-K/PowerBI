using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200012E RID: 302
	internal sealed class MarkAllItemsAsReadRequest : MultiResponseServiceRequest<ServiceResponse>, IJsonSerializable
	{
		// Token: 0x06000E9D RID: 3741 RVA: 0x0002C720 File Offset: 0x0002B720
		internal MarkAllItemsAsReadRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0002C735 File Offset: 0x0002B735
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.FolderIds, "FolderIds");
			this.FolderIds.Validate(base.Service.RequestedServerVersion);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0002C763 File Offset: 0x0002B763
		internal override int GetExpectedResponseMessageCount()
		{
			return this.FolderIds.Count;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0002C770 File Offset: 0x0002B770
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ServiceResponse();
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002C777 File Offset: 0x0002B777
		internal override string GetXmlElementName()
		{
			return "MarkAllItemsAsRead";
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0002C77E File Offset: 0x0002B77E
		internal override string GetResponseXmlElementName()
		{
			return "MarkAllItemsAsReadResponse";
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0002C785 File Offset: 0x0002B785
		internal override string GetResponseMessageXmlElementName()
		{
			return "MarkAllItemsAsReadResponseMessage";
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002C78C File Offset: 0x0002B78C
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Messages, "ReadFlag", this.ReadFlag);
			writer.WriteElementValue(XmlNamespace.Messages, "SuppressReadReceipts", this.SuppressReadReceipts);
			this.FolderIds.WriteToXml(writer, XmlNamespace.Messages, "FolderIds");
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002C7DC File Offset: 0x0002B7DC
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("ReadFlag", this.ReadFlag);
			jsonObject.Add("SuppressReadReceipts", this.SuppressReadReceipts);
			jsonObject.Add("FolderIds", this.FolderIds.InternalToJson(base.Service));
			return jsonObject;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0002C82E File Offset: 0x0002B82E
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0002C831 File Offset: 0x0002B831
		internal FolderIdWrapperList FolderIds
		{
			get
			{
				return this.folderIds;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0002C839 File Offset: 0x0002B839
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x0002C841 File Offset: 0x0002B841
		internal bool ReadFlag { get; set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0002C84A File Offset: 0x0002B84A
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x0002C852 File Offset: 0x0002B852
		internal bool SuppressReadReceipts { get; set; }

		// Token: 0x0400093A RID: 2362
		private FolderIdWrapperList folderIds = new FolderIdWrapperList();
	}
}
