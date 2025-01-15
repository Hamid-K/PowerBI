using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000137 RID: 311
	internal sealed class SendItemRequest : MultiResponseServiceRequest<ServiceResponse>, IJsonSerializable
	{
		// Token: 0x06000F17 RID: 3863 RVA: 0x0002D59F File Offset: 0x0002C59F
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.Items, "Items");
			if (this.SavedCopyDestinationFolderId != null)
			{
				this.SavedCopyDestinationFolderId.Validate(base.Service.RequestedServerVersion);
			}
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0002D5D5 File Offset: 0x0002C5D5
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ServiceResponse();
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0002D5DC File Offset: 0x0002C5DC
		internal override int GetExpectedResponseMessageCount()
		{
			return EwsUtilities.GetEnumeratedObjectCount(this.Items);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0002D5E9 File Offset: 0x0002C5E9
		internal override string GetXmlElementName()
		{
			return "SendItem";
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0002D5F0 File Offset: 0x0002C5F0
		internal override string GetResponseXmlElementName()
		{
			return "SendItemResponse";
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0002D5F7 File Offset: 0x0002C5F7
		internal override string GetResponseMessageXmlElementName()
		{
			return "SendItemResponseMessage";
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0002D5FE File Offset: 0x0002C5FE
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			writer.WriteAttributeValue("SaveItemToFolder", this.SavedCopyDestinationFolderId != null);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0002D624 File Offset: 0x0002C624
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "ItemIds");
			foreach (Item item in this.Items)
			{
				item.Id.WriteToXml(writer, "ItemId");
			}
			writer.WriteEndElement();
			if (this.SavedCopyDestinationFolderId != null)
			{
				writer.WriteStartElement(XmlNamespace.Messages, "SavedItemFolderId");
				this.SavedCopyDestinationFolderId.WriteToXml(writer);
				writer.WriteEndElement();
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0002D6B4 File Offset: 0x0002C6B4
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("SaveItemToFolder", this.SavedCopyDestinationFolderId != null);
			if (this.SavedCopyDestinationFolderId != null)
			{
				JsonObject jsonObject2 = new JsonObject();
				jsonObject2.Add("BaseFolderId", this.SavedCopyDestinationFolderId.InternalToJson(service));
				jsonObject.Add("SavedItemFolderId", jsonObject2);
			}
			List<object> list = new List<object>();
			foreach (Item item in this.Items)
			{
				list.Add(item.Id.InternalToJson(service));
			}
			jsonObject.Add("ItemIds", list.ToArray());
			return jsonObject;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0002D778 File Offset: 0x0002C778
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0002D77B File Offset: 0x0002C77B
		internal SendItemRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0002D785 File Offset: 0x0002C785
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x0002D78D File Offset: 0x0002C78D
		public IEnumerable<Item> Items
		{
			get
			{
				return this.items;
			}
			set
			{
				this.items = value;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0002D796 File Offset: 0x0002C796
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x0002D79E File Offset: 0x0002C79E
		public FolderId SavedCopyDestinationFolderId
		{
			get
			{
				return this.savedCopyDestinationFolderId;
			}
			set
			{
				this.savedCopyDestinationFolderId = value;
			}
		}

		// Token: 0x04000955 RID: 2389
		private IEnumerable<Item> items;

		// Token: 0x04000956 RID: 2390
		private FolderId savedCopyDestinationFolderId;
	}
}
