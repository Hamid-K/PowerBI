using System;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.WsTrust
{
	// Token: 0x020001BC RID: 444
	internal class WsTrustEndpoint
	{
		// Token: 0x060013D7 RID: 5079 RVA: 0x000430F3 File Offset: 0x000412F3
		public WsTrustEndpoint(Uri uri, WsTrustVersion version, ITimeService timeService = null, IGuidFactory guidFactory = null)
		{
			this.Uri = uri;
			this.Version = version;
			this._timeService = timeService ?? new TimeService();
			this._guidFactory = guidFactory ?? new GuidFactory();
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0004312A File Offset: 0x0004132A
		public Uri Uri { get; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00043132 File Offset: 0x00041332
		public WsTrustVersion Version { get; }

		// Token: 0x060013DA RID: 5082 RVA: 0x0004313A File Offset: 0x0004133A
		public string BuildTokenRequestMessageWindowsIntegratedAuth(string cloudAudienceUri)
		{
			return this.BuildTokenRequestMessage(UserAuthType.IntegratedAuth, cloudAudienceUri, string.Empty, string.Empty);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0004314E File Offset: 0x0004134E
		public string BuildTokenRequestMessageUsernamePassword(string cloudAudienceUri, string username, string password)
		{
			return this.BuildTokenRequestMessage(UserAuthType.UsernamePassword, cloudAudienceUri, username, password);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0004315C File Offset: 0x0004135C
		private string BuildTokenRequestMessage(UserAuthType authType, string cloudAudienceUri, string username, string password)
		{
			string text;
			string text2;
			string text3;
			string text4;
			if (this.Version == WsTrustVersion.WsTrust2005)
			{
				text = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue";
				text2 = "http://schemas.xmlsoap.org/ws/2005/02/trust";
				text3 = "http://schemas.xmlsoap.org/ws/2005/05/identity/NoProofKey";
				text4 = "http://schemas.xmlsoap.org/ws/2005/02/trust/Issue";
			}
			else
			{
				text = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue";
				text2 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512";
				text3 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer";
				text4 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Issue";
			}
			string text5;
			using (StringWriterWithEncoding stringWriterWithEncoding = new StringWriterWithEncoding(Encoding.UTF8))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriterWithEncoding, new XmlWriterSettings
				{
					Async = false,
					Encoding = Encoding.UTF8,
					CloseOutput = false
				}))
				{
					xmlWriter.WriteStartElement("s", "Envelope", "http://www.w3.org/2003/05/soap-envelope");
					xmlWriter.WriteAttributeString("wsa", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2005/08/addressing");
					xmlWriter.WriteAttributeString("wsu", "http://www.w3.org/2000/xmlns/", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
					xmlWriter.WriteStartElement("Header", "http://www.w3.org/2003/05/soap-envelope");
					xmlWriter.WriteStartElement("Action", "http://www.w3.org/2005/08/addressing");
					xmlWriter.WriteAttributeString("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope", "1");
					xmlWriter.WriteString(text);
					xmlWriter.WriteEndElement();
					xmlWriter.WriteStartElement("MessageID", "http://www.w3.org/2005/08/addressing");
					xmlWriter.WriteString("urn:uuid:" + this._guidFactory.NewGuid().ToString("D"));
					xmlWriter.WriteEndElement();
					xmlWriter.WriteStartElement("ReplyTo", "http://www.w3.org/2005/08/addressing");
					xmlWriter.WriteStartElement("Address", "http://www.w3.org/2005/08/addressing");
					xmlWriter.WriteString("http://www.w3.org/2005/08/addressing/anonymous");
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndElement();
					xmlWriter.WriteStartElement("To", "http://www.w3.org/2005/08/addressing");
					xmlWriter.WriteAttributeString("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope", "1");
					xmlWriter.WriteString(this.Uri.ToString());
					xmlWriter.WriteEndElement();
					if (authType == UserAuthType.UsernamePassword)
					{
						this.AppendSecurityHeader(xmlWriter, username, password);
					}
					xmlWriter.WriteEndElement();
					xmlWriter.WriteStartElement("Body", "http://www.w3.org/2003/05/soap-envelope");
					xmlWriter.WriteStartElement("wst", "RequestSecurityToken", text2);
					xmlWriter.WriteStartElement("wsp", "AppliesTo", "http://schemas.xmlsoap.org/ws/2004/09/policy");
					xmlWriter.WriteStartElement("EndpointReference", "http://www.w3.org/2005/08/addressing");
					xmlWriter.WriteStartElement("Address", "http://www.w3.org/2005/08/addressing");
					xmlWriter.WriteString(cloudAudienceUri);
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndElement();
					xmlWriter.WriteStartElement("KeyType", text2);
					xmlWriter.WriteString(text3);
					xmlWriter.WriteEndElement();
					xmlWriter.WriteStartElement("RequestType", text2);
					xmlWriter.WriteString(text4);
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndElement();
				}
				text5 = stringWriterWithEncoding.ToString();
			}
			return text5;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00043454 File Offset: 0x00041654
		private void AppendSecurityHeader(XmlWriter writer, string username, string password)
		{
			DateTime utcNow = this._timeService.GetUtcNow();
			string text = WsTrustEndpoint.BuildTimeString(utcNow);
			string text2 = WsTrustEndpoint.BuildTimeString(utcNow.AddMinutes(10.0));
			string text3 = ((this.Version == WsTrustVersion.WsTrust2005) ? "UnPwSecTok2005-" : "UnPwSecTok13-") + this._guidFactory.NewGuid().ToString("D");
			writer.WriteStartElement("wsse", "Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
			writer.WriteAttributeString("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope", "1");
			writer.WriteStartElement("Timestamp", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
			writer.WriteAttributeString("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", "MSATimeStamp");
			writer.WriteElementString("Created", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", text);
			writer.WriteElementString("Expires", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", text2);
			writer.WriteEndElement();
			writer.WriteStartElement("UsernameToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
			writer.WriteAttributeString("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", text3);
			writer.WriteElementString("Username", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", username);
			writer.WriteElementString("Password", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", password);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00043584 File Offset: 0x00041784
		private static string BuildTimeString(DateTime utcTime)
		{
			return utcTime.ToString("yyyy-MM-ddTHH:mm:ss.068Z", CultureInfo.InvariantCulture);
		}

		// Token: 0x0400082B RID: 2091
		private const string EnvelopeNamespaceValue = "http://www.w3.org/2003/05/soap-envelope";

		// Token: 0x0400082C RID: 2092
		private const string WsuNamespaceValue = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

		// Token: 0x0400082D RID: 2093
		private readonly ITimeService _timeService;

		// Token: 0x0400082E RID: 2094
		private readonly IGuidFactory _guidFactory;
	}
}
