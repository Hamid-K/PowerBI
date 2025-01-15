using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000134 RID: 308
	internal sealed class PlayOnPhoneRequest : SimpleServiceRequestBase, IJsonSerializable
	{
		// Token: 0x06000EE6 RID: 3814 RVA: 0x0002CD1D File Offset: 0x0002BD1D
		internal PlayOnPhoneRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0002CD26 File Offset: 0x0002BD26
		internal override string GetXmlElementName()
		{
			return "PlayOnPhone";
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0002CD2D File Offset: 0x0002BD2D
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.itemId.WriteToXml(writer, XmlNamespace.Messages, "ItemId");
			writer.WriteElementValue(XmlNamespace.Messages, "DialString", this.dialString);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002CD54 File Offset: 0x0002BD54
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("ItemId", this.ItemId.InternalToJson(service));
			jsonObject.Add("DialString", this.dialString);
			return jsonObject;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0002CD90 File Offset: 0x0002BD90
		internal override string GetResponseXmlElementName()
		{
			return "PlayOnPhoneResponse";
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0002CD98 File Offset: 0x0002BD98
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			PlayOnPhoneResponse playOnPhoneResponse = new PlayOnPhoneResponse(base.Service);
			playOnPhoneResponse.LoadFromXml(reader, "PlayOnPhoneResponse");
			return playOnPhoneResponse;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0002CDC0 File Offset: 0x0002BDC0
		internal override object ParseResponse(JsonObject jsonBody)
		{
			PlayOnPhoneResponse playOnPhoneResponse = new PlayOnPhoneResponse(base.Service);
			playOnPhoneResponse.LoadFromJson(jsonBody, base.Service);
			return playOnPhoneResponse;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0002CDE7 File Offset: 0x0002BDE7
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002CDEC File Offset: 0x0002BDEC
		internal PlayOnPhoneResponse Execute()
		{
			PlayOnPhoneResponse playOnPhoneResponse = (PlayOnPhoneResponse)base.InternalExecute();
			playOnPhoneResponse.ThrowIfNecessary();
			return playOnPhoneResponse;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0002CE0C File Offset: 0x0002BE0C
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x0002CE14 File Offset: 0x0002BE14
		internal ItemId ItemId
		{
			get
			{
				return this.itemId;
			}
			set
			{
				this.itemId = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0002CE1D File Offset: 0x0002BE1D
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x0002CE25 File Offset: 0x0002BE25
		internal string DialString
		{
			get
			{
				return this.dialString;
			}
			set
			{
				this.dialString = value;
			}
		}

		// Token: 0x04000948 RID: 2376
		private ItemId itemId;

		// Token: 0x04000949 RID: 2377
		private string dialString;
	}
}
