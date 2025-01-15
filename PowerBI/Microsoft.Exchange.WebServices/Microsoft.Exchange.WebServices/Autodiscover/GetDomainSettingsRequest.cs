using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000017 RID: 23
	internal class GetDomainSettingsRequest : AutodiscoverRequest
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00006121 File Offset: 0x00005121
		internal GetDomainSettingsRequest(AutodiscoverService service, Uri url)
			: base(service, url)
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000612C File Offset: 0x0000512C
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.Domains, "domains");
			EwsUtilities.ValidateParam(this.Settings, "settings");
			if (this.Settings.Count == 0)
			{
				throw new ServiceValidationException(Strings.InvalidAutodiscoverSettingsCount);
			}
			if (this.domains.Count == 0)
			{
				throw new ServiceValidationException(Strings.InvalidAutodiscoverDomainsCount);
			}
			foreach (string text in this.domains)
			{
				if (string.IsNullOrEmpty(text))
				{
					throw new ServiceValidationException(Strings.InvalidAutodiscoverDomain);
				}
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000061F0 File Offset: 0x000051F0
		internal GetDomainSettingsResponseCollection Execute()
		{
			GetDomainSettingsResponseCollection getDomainSettingsResponseCollection = (GetDomainSettingsResponseCollection)base.InternalExecute();
			if (getDomainSettingsResponseCollection.ErrorCode == AutodiscoverErrorCode.NoError)
			{
				this.PostProcessResponses(getDomainSettingsResponseCollection);
			}
			return getDomainSettingsResponseCollection;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000621C File Offset: 0x0000521C
		private void PostProcessResponses(GetDomainSettingsResponseCollection responses)
		{
			for (int i = 0; i < responses.Count; i++)
			{
				responses[i].Domain = this.Domains[i];
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006252 File Offset: 0x00005252
		internal override string GetRequestXmlElementName()
		{
			return "GetDomainSettingsRequestMessage";
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006259 File Offset: 0x00005259
		internal override string GetResponseXmlElementName()
		{
			return "GetDomainSettingsResponseMessage";
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006260 File Offset: 0x00005260
		internal override string GetWsAddressingActionName()
		{
			return "http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetDomainSettings";
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006267 File Offset: 0x00005267
		internal override AutodiscoverResponse CreateServiceResponse()
		{
			return new GetDomainSettingsResponseCollection();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000626E File Offset: 0x0000526E
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("xmlns", "a", "http://schemas.microsoft.com/exchange/2010/Autodiscover");
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006288 File Offset: 0x00005288
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Autodiscover, "Request");
			writer.WriteStartElement(XmlNamespace.Autodiscover, "Domains");
			foreach (string text in this.Domains)
			{
				if (!string.IsNullOrEmpty(text))
				{
					writer.WriteElementValue(XmlNamespace.Autodiscover, "Domain", text);
				}
			}
			writer.WriteEndElement();
			writer.WriteStartElement(XmlNamespace.Autodiscover, "RequestedSettings");
			foreach (DomainSettingName domainSettingName in this.settings)
			{
				writer.WriteElementValue(XmlNamespace.Autodiscover, "Setting", domainSettingName);
			}
			writer.WriteEndElement();
			if (this.requestedVersion != null)
			{
				writer.WriteElementValue(XmlNamespace.Autodiscover, "RequestedVersion", this.requestedVersion.Value);
			}
			writer.WriteEndElement();
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000639C File Offset: 0x0000539C
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000063A4 File Offset: 0x000053A4
		internal List<string> Domains
		{
			get
			{
				return this.domains;
			}
			set
			{
				this.domains = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000063AD File Offset: 0x000053AD
		// (set) Token: 0x06000103 RID: 259 RVA: 0x000063B5 File Offset: 0x000053B5
		internal List<DomainSettingName> Settings
		{
			get
			{
				return this.settings;
			}
			set
			{
				this.settings = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000063BE File Offset: 0x000053BE
		// (set) Token: 0x06000105 RID: 261 RVA: 0x000063C6 File Offset: 0x000053C6
		internal ExchangeVersion? RequestedVersion
		{
			get
			{
				return this.requestedVersion;
			}
			set
			{
				this.requestedVersion = value;
			}
		}

		// Token: 0x0400005E RID: 94
		private const string GetDomainSettingsActionUri = "http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetDomainSettings";

		// Token: 0x0400005F RID: 95
		private List<string> domains;

		// Token: 0x04000060 RID: 96
		private List<DomainSettingName> settings;

		// Token: 0x04000061 RID: 97
		private ExchangeVersion? requestedVersion;
	}
}
