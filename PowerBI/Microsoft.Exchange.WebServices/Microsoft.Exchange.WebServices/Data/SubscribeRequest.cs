using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200013D RID: 317
	internal abstract class SubscribeRequest<TSubscription> : MultiResponseServiceRequest<SubscribeResponse<TSubscription>>, IJsonSerializable where TSubscription : SubscriptionBase
	{
		// Token: 0x06000F64 RID: 3940 RVA: 0x0002DD90 File Offset: 0x0002CD90
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.FolderIds, "FolderIds");
			EwsUtilities.ValidateParamCollection(this.EventTypes, "EventTypes");
			this.FolderIds.Validate(base.Service.RequestedServerVersion);
			if (Enumerable.Count<EventType>(this.EventTypes, (EventType eventType) => eventType == EventType.Status) > 0)
			{
				throw new ServiceValidationException(Strings.CannotSubscribeToStatusEvents);
			}
			if (!string.IsNullOrEmpty(this.Watermark))
			{
				EwsUtilities.ValidateNonBlankStringParam(this.Watermark, "Watermark");
			}
			this.EventTypes.ForEach(delegate(EventType eventType)
			{
				EwsUtilities.ValidateEnumVersionValue(eventType, base.Service.RequestedServerVersion);
			});
		}

		// Token: 0x06000F65 RID: 3941
		internal abstract string GetSubscriptionXmlElementName();

		// Token: 0x06000F66 RID: 3942 RVA: 0x0002DE48 File Offset: 0x0002CE48
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0002DE4B File Offset: 0x0002CE4B
		internal override string GetXmlElementName()
		{
			return "Subscribe";
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002DE52 File Offset: 0x0002CE52
		internal override string GetResponseXmlElementName()
		{
			return "SubscribeResponse";
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0002DE59 File Offset: 0x0002CE59
		internal override string GetResponseMessageXmlElementName()
		{
			return "SubscribeResponseMessage";
		}

		// Token: 0x06000F6A RID: 3946
		internal abstract void InternalWriteElementsToXml(EwsServiceXmlWriter writer);

		// Token: 0x06000F6B RID: 3947 RVA: 0x0002DE60 File Offset: 0x0002CE60
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, this.GetSubscriptionXmlElementName());
			if (this.FolderIds.Count == 0)
			{
				writer.WriteAttributeValue("SubscribeToAllFolders", true);
			}
			this.FolderIds.WriteToXml(writer, XmlNamespace.Types, "FolderIds");
			writer.WriteStartElement(XmlNamespace.Types, "EventTypes");
			foreach (EventType eventType in this.EventTypes)
			{
				writer.WriteElementValue(XmlNamespace.Types, "EventType", eventType);
			}
			writer.WriteEndElement();
			if (!string.IsNullOrEmpty(this.Watermark))
			{
				writer.WriteElementValue(XmlNamespace.Types, "Watermark", this.Watermark);
			}
			this.InternalWriteElementsToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0002DF3C File Offset: 0x0002CF3C
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			JsonObject jsonObject2 = new JsonObject();
			jsonObject2.AddTypeParameter(this.GetSubscriptionXmlElementName());
			jsonObject2.Add("EventTypes", this.EventTypes.ToArray());
			if (this.FolderIds.Count > 0)
			{
				jsonObject2.Add("FolderIds", this.FolderIds.InternalToJson(service));
			}
			else
			{
				jsonObject2.Add("SubscribeToAllFolders", true);
			}
			if (!string.IsNullOrEmpty(this.Watermark))
			{
				jsonObject2.Add("Watermark", this.Watermark);
			}
			this.AddJsonProperties(jsonObject2, service);
			jsonObject.Add("SubscriptionRequest", jsonObject2);
			return jsonObject;
		}

		// Token: 0x06000F6D RID: 3949
		internal abstract void AddJsonProperties(JsonObject jsonSubscribeRequest, ExchangeService service);

		// Token: 0x06000F6E RID: 3950 RVA: 0x0002DFDD File Offset: 0x0002CFDD
		internal SubscribeRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
			this.FolderIds = new FolderIdWrapperList();
			this.EventTypes = new List<EventType>();
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0002DFFD File Offset: 0x0002CFFD
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x0002E005 File Offset: 0x0002D005
		public FolderIdWrapperList FolderIds { get; private set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0002E00E File Offset: 0x0002D00E
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x0002E016 File Offset: 0x0002D016
		public List<EventType> EventTypes { get; private set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0002E01F File Offset: 0x0002D01F
		// (set) Token: 0x06000F74 RID: 3956 RVA: 0x0002E027 File Offset: 0x0002D027
		public string Watermark { get; set; }
	}
}
