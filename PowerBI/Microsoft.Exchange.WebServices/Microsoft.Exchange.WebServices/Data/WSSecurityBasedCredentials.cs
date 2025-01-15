using System;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001D0 RID: 464
	public abstract class WSSecurityBasedCredentials : ExchangeCredentials
	{
		// Token: 0x0600152F RID: 5423 RVA: 0x0003B86B File Offset: 0x0003A86B
		internal WSSecurityBasedCredentials()
		{
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0003B873 File Offset: 0x0003A873
		internal WSSecurityBasedCredentials(string securityToken)
		{
			this.securityToken = securityToken;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0003B882 File Offset: 0x0003A882
		internal WSSecurityBasedCredentials(string securityToken, bool addTimestamp)
		{
			this.securityToken = securityToken;
			this.addTimestamp = addTimestamp;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0003B898 File Offset: 0x0003A898
		internal override void PreAuthenticate()
		{
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0003B89A File Offset: 0x0003A89A
		internal override void EmitExtraSoapHeaderNamespaceAliases(XmlWriter writer)
		{
			writer.WriteAttributeString("xmlns", "wsse", null, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
			writer.WriteAttributeString("xmlns", "wsa", null, "http://www.w3.org/2005/08/addressing");
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0003B8C8 File Offset: 0x0003A8C8
		internal override void SerializeExtraSoapHeaders(XmlWriter writer, string webMethodName)
		{
			this.SerializeWSAddressingHeaders(writer, webMethodName);
			this.SerializeWSSecurityHeaders(writer);
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0003B8DC File Offset: 0x0003A8DC
		private void SerializeWSAddressingHeaders(XmlWriter xmlWriter, string webMethodName)
		{
			EwsUtilities.Assert(webMethodName != null, "WSSecurityBasedCredentials.SerializeWSAddressingHeaders", "Web method name cannot be null!");
			EwsUtilities.Assert(this.ewsUrl != null, "WSSecurityBasedCredentials.SerializeWSAddressingHeaders", "EWS Url cannot be null!");
			string text = string.Format("<wsa:Action soap:mustUnderstand='1'>http://schemas.microsoft.com/exchange/services/2006/messages/{0}</wsa:Action><wsa:ReplyTo><wsa:Address>http://www.w3.org/2005/08/addressing/anonymous</wsa:Address></wsa:ReplyTo><wsa:To soap:mustUnderstand='1'>{1}</wsa:To>", webMethodName, this.ewsUrl);
			xmlWriter.WriteRaw(text);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0003B934 File Offset: 0x0003A934
		internal override void SerializeWSSecurityHeaders(XmlWriter xmlWriter)
		{
			EwsUtilities.Assert(this.securityToken != null, "WSSecurityBasedCredentials.SerializeWSSecurityHeaders", "Security token cannot be null!");
			string text = null;
			if (this.addTimestamp)
			{
				DateTime utcNow = DateTime.UtcNow;
				text = string.Format("<wsu:Timestamp><wsu:Created>{0:yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'}</wsu:Created><wsu:Expires>{1:yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'}</wsu:Expires></wsu:Timestamp>", utcNow, utcNow.AddMinutes(5.0));
			}
			string text2 = string.Format("<wsse:Security soap:mustUnderstand='1'>  {0}</wsse:Security>", text + this.securityToken);
			xmlWriter.WriteRaw(text2);
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0003B9B0 File Offset: 0x0003A9B0
		internal override Uri AdjustUrl(Uri url)
		{
			return new Uri(ExchangeCredentials.GetUriWithoutSuffix(url) + "/wssecurity");
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0003B9C7 File Offset: 0x0003A9C7
		// (set) Token: 0x06001539 RID: 5433 RVA: 0x0003B9CF File Offset: 0x0003A9CF
		internal string SecurityToken
		{
			get
			{
				return this.securityToken;
			}
			set
			{
				this.securityToken = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0003B9D8 File Offset: 0x0003A9D8
		// (set) Token: 0x0600153B RID: 5435 RVA: 0x0003B9E0 File Offset: 0x0003A9E0
		internal Uri EwsUrl
		{
			get
			{
				return this.ewsUrl;
			}
			set
			{
				this.ewsUrl = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x0003B9EC File Offset: 0x0003A9EC
		internal static XmlNamespaceManager NamespaceManager
		{
			get
			{
				if (WSSecurityBasedCredentials.namespaceManager == null)
				{
					WSSecurityBasedCredentials.namespaceManager = new XmlNamespaceManager(new NameTable());
					WSSecurityBasedCredentials.namespaceManager.AddNamespace("wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
					WSSecurityBasedCredentials.namespaceManager.AddNamespace("wsa", "http://www.w3.org/2005/08/addressing");
					WSSecurityBasedCredentials.namespaceManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
					WSSecurityBasedCredentials.namespaceManager.AddNamespace("t", "http://schemas.microsoft.com/exchange/services/2006/types");
					WSSecurityBasedCredentials.namespaceManager.AddNamespace("wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
				}
				return WSSecurityBasedCredentials.namespaceManager;
			}
		}

		// Token: 0x04000CD6 RID: 3286
		internal const string WsAddressingHeadersFormat = "<wsa:Action soap:mustUnderstand='1'>http://schemas.microsoft.com/exchange/services/2006/messages/{0}</wsa:Action><wsa:ReplyTo><wsa:Address>http://www.w3.org/2005/08/addressing/anonymous</wsa:Address></wsa:ReplyTo><wsa:To soap:mustUnderstand='1'>{1}</wsa:To>";

		// Token: 0x04000CD7 RID: 3287
		internal const string WsSecurityHeaderFormat = "<wsse:Security soap:mustUnderstand='1'>  {0}</wsse:Security>";

		// Token: 0x04000CD8 RID: 3288
		internal const string WsuTimeStampFormat = "<wsu:Timestamp><wsu:Created>{0:yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'}</wsu:Created><wsu:Expires>{1:yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'}</wsu:Expires></wsu:Timestamp>";

		// Token: 0x04000CD9 RID: 3289
		internal const string WsSecurityPathSuffix = "/wssecurity";

		// Token: 0x04000CDA RID: 3290
		private readonly bool addTimestamp;

		// Token: 0x04000CDB RID: 3291
		private static XmlNamespaceManager namespaceManager;

		// Token: 0x04000CDC RID: 3292
		private string securityToken;

		// Token: 0x04000CDD RID: 3293
		private Uri ewsUrl;
	}
}
