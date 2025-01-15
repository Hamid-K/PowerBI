using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000018 RID: 24
	internal class GetUserSettingsRequest : AutodiscoverRequest
	{
		// Token: 0x06000106 RID: 262 RVA: 0x000063CF File Offset: 0x000053CF
		internal GetUserSettingsRequest(AutodiscoverService service, Uri url)
			: this(service, url, false)
		{
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000063DA File Offset: 0x000053DA
		internal GetUserSettingsRequest(AutodiscoverService service, Uri url, bool expectPartnerToken)
			: base(service, url)
		{
			this.expectPartnerToken = expectPartnerToken;
			if (expectPartnerToken && !url.Scheme.Equals("https", 5))
			{
				throw new ServiceValidationException(Strings.HttpsIsRequired);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006414 File Offset: 0x00005414
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.SmtpAddresses, "smtpAddresses");
			EwsUtilities.ValidateParam(this.Settings, "settings");
			if (this.Settings.Count == 0)
			{
				throw new ServiceValidationException(Strings.InvalidAutodiscoverSettingsCount);
			}
			if (this.SmtpAddresses.Count == 0)
			{
				throw new ServiceValidationException(Strings.InvalidAutodiscoverSmtpAddressesCount);
			}
			foreach (string text in this.SmtpAddresses)
			{
				if (string.IsNullOrEmpty(text))
				{
					throw new ServiceValidationException(Strings.InvalidAutodiscoverSmtpAddress);
				}
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000064D8 File Offset: 0x000054D8
		internal GetUserSettingsResponseCollection Execute()
		{
			GetUserSettingsResponseCollection getUserSettingsResponseCollection = (GetUserSettingsResponseCollection)base.InternalExecute();
			if (getUserSettingsResponseCollection.ErrorCode == AutodiscoverErrorCode.NoError)
			{
				this.PostProcessResponses(getUserSettingsResponseCollection);
			}
			return getUserSettingsResponseCollection;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006504 File Offset: 0x00005504
		private void PostProcessResponses(GetUserSettingsResponseCollection responses)
		{
			for (int i = 0; i < responses.Count; i++)
			{
				responses[i].SmtpAddress = this.SmtpAddresses[i];
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000653A File Offset: 0x0000553A
		internal override string GetRequestXmlElementName()
		{
			return "GetUserSettingsRequestMessage";
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006541 File Offset: 0x00005541
		internal override string GetResponseXmlElementName()
		{
			return "GetUserSettingsResponseMessage";
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006548 File Offset: 0x00005548
		internal override string GetWsAddressingActionName()
		{
			return "http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetUserSettings";
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000654F File Offset: 0x0000554F
		internal override AutodiscoverResponse CreateServiceResponse()
		{
			return new GetUserSettingsResponseCollection();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006556 File Offset: 0x00005556
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("xmlns", "a", "http://schemas.microsoft.com/exchange/2010/Autodiscover");
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000656D File Offset: 0x0000556D
		internal override void WriteExtraCustomSoapHeadersToXml(EwsServiceXmlWriter writer)
		{
			if (this.expectPartnerToken)
			{
				writer.WriteElementValue(XmlNamespace.Autodiscover, "BinarySecret", Convert.ToBase64String(ExchangeServiceBase.SessionKey));
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006590 File Offset: 0x00005590
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Autodiscover, "Request");
			writer.WriteStartElement(XmlNamespace.Autodiscover, "Users");
			foreach (string text in this.SmtpAddresses)
			{
				writer.WriteStartElement(XmlNamespace.Autodiscover, "User");
				if (!string.IsNullOrEmpty(text))
				{
					writer.WriteElementValue(XmlNamespace.Autodiscover, "Mailbox", text);
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.WriteStartElement(XmlNamespace.Autodiscover, "RequestedSettings");
			foreach (UserSettingName userSettingName in this.Settings)
			{
				writer.WriteElementValue(XmlNamespace.Autodiscover, "Setting", userSettingName);
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000668C File Offset: 0x0000568C
		internal override void ReadSoapHeader(EwsXmlReader reader)
		{
			base.ReadSoapHeader(reader);
			if (this.expectPartnerToken)
			{
				if (reader.IsStartElement(XmlNamespace.Autodiscover, "PartnerToken"))
				{
					this.PartnerToken = reader.ReadInnerXml();
				}
				if (reader.IsStartElement(XmlNamespace.Autodiscover, "PartnerTokenReference"))
				{
					this.PartnerTokenReference = reader.ReadInnerXml();
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000066DE File Offset: 0x000056DE
		// (set) Token: 0x06000114 RID: 276 RVA: 0x000066E6 File Offset: 0x000056E6
		internal List<string> SmtpAddresses { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000066EF File Offset: 0x000056EF
		// (set) Token: 0x06000116 RID: 278 RVA: 0x000066F7 File Offset: 0x000056F7
		internal List<UserSettingName> Settings { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00006700 File Offset: 0x00005700
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00006708 File Offset: 0x00005708
		internal string PartnerToken { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006711 File Offset: 0x00005711
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00006719 File Offset: 0x00005719
		internal string PartnerTokenReference { get; private set; }

		// Token: 0x04000062 RID: 98
		private const string GetUserSettingsActionUri = "http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetUserSettings";

		// Token: 0x04000063 RID: 99
		private readonly bool expectPartnerToken;
	}
}
