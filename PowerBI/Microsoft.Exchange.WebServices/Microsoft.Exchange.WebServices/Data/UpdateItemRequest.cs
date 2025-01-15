using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000149 RID: 329
	internal sealed class UpdateItemRequest : MultiResponseServiceRequest<UpdateItemResponse>, IJsonSerializable
	{
		// Token: 0x06000FF8 RID: 4088 RVA: 0x0002ECF8 File Offset: 0x0002DCF8
		internal UpdateItemRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0002ED10 File Offset: 0x0002DD10
		internal override bool EmitTimeZoneHeader
		{
			get
			{
				foreach (Item item in this.Items)
				{
					if (item.GetIsTimeZoneHeaderRequired(true))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0002ED6C File Offset: 0x0002DD6C
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParamCollection(this.Items, "Items");
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i] == null || this.Items[i].IsNew)
				{
					throw new ArgumentException(string.Format(Strings.ItemToUpdateCannotBeNullOrNew, i));
				}
			}
			if (this.SavedItemsDestinationFolder != null)
			{
				this.SavedItemsDestinationFolder.Validate(base.Service.RequestedServerVersion);
			}
			foreach (Item item in this.Items)
			{
				item.Validate();
			}
			if (this.SuppressReadReceipts && base.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.ParameterIncompatibleWithRequestVersion, "SuppressReadReceipts", ExchangeVersion.Exchange2013));
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0002EE7C File Offset: 0x0002DE7C
		internal override UpdateItemResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new UpdateItemResponse(this.Items[responseIndex]);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0002EE8F File Offset: 0x0002DE8F
		internal override string GetXmlElementName()
		{
			return "UpdateItem";
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0002EE96 File Offset: 0x0002DE96
		internal override string GetResponseXmlElementName()
		{
			return "UpdateItemResponse";
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0002EE9D File Offset: 0x0002DE9D
		internal override string GetResponseMessageXmlElementName()
		{
			return "UpdateItemResponseMessage";
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0002EEA4 File Offset: 0x0002DEA4
		internal override int GetExpectedResponseMessageCount()
		{
			return this.items.Count;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0002EEB4 File Offset: 0x0002DEB4
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			if (this.MessageDisposition != null)
			{
				writer.WriteAttributeValue("MessageDisposition", this.MessageDisposition);
			}
			if (this.SuppressReadReceipts)
			{
				writer.WriteAttributeValue("SuppressReadReceipts", true);
			}
			writer.WriteAttributeValue("ConflictResolution", this.ConflictResolutionMode);
			if (this.SendInvitationsOrCancellationsMode != null)
			{
				writer.WriteAttributeValue("SendMeetingInvitationsOrCancellations", this.SendInvitationsOrCancellationsMode.Value);
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0002EF4C File Offset: 0x0002DF4C
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.SavedItemsDestinationFolder != null)
			{
				writer.WriteStartElement(XmlNamespace.Messages, "SavedItemFolderId");
				this.SavedItemsDestinationFolder.WriteToXml(writer);
				writer.WriteEndElement();
			}
			writer.WriteStartElement(XmlNamespace.Messages, "ItemChanges");
			foreach (Item item in this.items)
			{
				item.WriteToXmlForUpdate(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0002EFD8 File Offset: 0x0002DFD8
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			if (this.MessageDisposition != null)
			{
				jsonObject.Add("MessageDisposition", (Enum)this.MessageDisposition);
			}
			jsonObject.Add("ConflictResolution", this.ConflictResolutionMode);
			if (this.SendInvitationsOrCancellationsMode != null)
			{
				jsonObject.Add("SendMeetingInvitationsOrCancellations", this.SendInvitationsOrCancellationsMode.Value);
			}
			if (this.SuppressReadReceipts)
			{
				jsonObject.Add("SuppressReadReceipts", true);
			}
			if (this.SavedItemsDestinationFolder != null)
			{
				jsonObject.Add("SavedItemFolderId", this.SavedItemsDestinationFolder.InternalToJson(service));
			}
			List<object> list = new List<object>();
			foreach (Item item in this.items)
			{
				list.Add(item.WriteToJsonForUpdate(service));
			}
			jsonObject.Add("ItemChanges", list.ToArray());
			return jsonObject;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0002F0F4 File Offset: 0x0002E0F4
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0002F0F7 File Offset: 0x0002E0F7
		// (set) Token: 0x06001005 RID: 4101 RVA: 0x0002F0FF File Offset: 0x0002E0FF
		public MessageDisposition? MessageDisposition
		{
			get
			{
				return this.messageDisposition;
			}
			set
			{
				this.messageDisposition = value;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x0002F108 File Offset: 0x0002E108
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x0002F110 File Offset: 0x0002E110
		public ConflictResolutionMode ConflictResolutionMode
		{
			get
			{
				return this.conflictResolutionMode;
			}
			set
			{
				this.conflictResolutionMode = value;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0002F119 File Offset: 0x0002E119
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x0002F121 File Offset: 0x0002E121
		public SendInvitationsOrCancellationsMode? SendInvitationsOrCancellationsMode
		{
			get
			{
				return this.sendInvitationsOrCancellationsMode;
			}
			set
			{
				this.sendInvitationsOrCancellationsMode = value;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0002F12A File Offset: 0x0002E12A
		// (set) Token: 0x0600100B RID: 4107 RVA: 0x0002F132 File Offset: 0x0002E132
		public bool SuppressReadReceipts { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0002F13B File Offset: 0x0002E13B
		public List<Item> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x0002F143 File Offset: 0x0002E143
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x0002F14B File Offset: 0x0002E14B
		public FolderId SavedItemsDestinationFolder
		{
			get
			{
				return this.savedItemsDestinationFolder;
			}
			set
			{
				this.savedItemsDestinationFolder = value;
			}
		}

		// Token: 0x04000982 RID: 2434
		private List<Item> items = new List<Item>();

		// Token: 0x04000983 RID: 2435
		private FolderId savedItemsDestinationFolder;

		// Token: 0x04000984 RID: 2436
		private ConflictResolutionMode conflictResolutionMode;

		// Token: 0x04000985 RID: 2437
		private MessageDisposition? messageDisposition;

		// Token: 0x04000986 RID: 2438
		private SendInvitationsOrCancellationsMode? sendInvitationsOrCancellationsMode;
	}
}
