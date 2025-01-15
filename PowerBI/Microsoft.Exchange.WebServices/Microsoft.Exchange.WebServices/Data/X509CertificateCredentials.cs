using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001D6 RID: 470
	public sealed class X509CertificateCredentials : WSSecurityBasedCredentials
	{
		// Token: 0x06001562 RID: 5474 RVA: 0x0003C4D8 File Offset: 0x0003B4D8
		public X509CertificateCredentials(X509Certificate2 certificate)
			: base(null, true)
		{
			EwsUtilities.ValidateParam(certificate, "certificate");
			if (!certificate.HasPrivateKey)
			{
				throw new ServiceValidationException(Strings.CertificateHasNoPrivateKey);
			}
			this.certificate = certificate;
			string uniqueId = WSSecurityUtilityIdSignedXml.GetUniqueId();
			base.SecurityToken = string.Format("<wsse:BinarySecurityToken EncodingType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\" ValueType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3\" wsu:Id=\"{0}\">{1}</wsse:BinarySecurityToken>", uniqueId, Convert.ToBase64String(this.certificate.GetRawCertData()));
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.PreserveWhitespace = true;
			safeXmlDocument.LoadXml(string.Format("<wsse:SecurityTokenReference xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\" ><wsse:Reference URI=\"#{0}\" ValueType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3\" /></wsse:SecurityTokenReference>", uniqueId));
			this.keyInfoClause = new KeyInfoNode(safeXmlDocument.DocumentElement);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0003C56D File Offset: 0x0003B56D
		internal override void PrepareWebRequest(IEwsHttpWebRequest request)
		{
			base.EwsUrl = request.RequestUri;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0003C57B File Offset: 0x0003B57B
		internal override Uri AdjustUrl(Uri url)
		{
			return new Uri(ExchangeCredentials.GetUriWithoutSuffix(url) + "/wssecurity/x509cert");
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0003C592 File Offset: 0x0003B592
		internal override bool NeedSignature
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0003C598 File Offset: 0x0003B598
		internal override void Sign(MemoryStream memoryStream)
		{
			memoryStream.Position = 0L;
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.PreserveWhitespace = true;
			safeXmlDocument.Load(memoryStream);
			WSSecurityUtilityIdSignedXml wssecurityUtilityIdSignedXml = new WSSecurityUtilityIdSignedXml(safeXmlDocument);
			wssecurityUtilityIdSignedXml.SignedInfo.CanonicalizationMethod = "http://www.w3.org/2001/10/xml-exc-c14n#";
			wssecurityUtilityIdSignedXml.SigningKey = this.certificate.PrivateKey;
			wssecurityUtilityIdSignedXml.AddReference("/soap:Envelope/soap:Header/wsa:To");
			wssecurityUtilityIdSignedXml.AddReference("/soap:Envelope/soap:Header/wsse:Security/wsu:Timestamp");
			wssecurityUtilityIdSignedXml.KeyInfo.AddClause(this.keyInfoClause);
			wssecurityUtilityIdSignedXml.ComputeSignature();
			XmlElement xml = wssecurityUtilityIdSignedXml.GetXml();
			XmlNode xmlNode = safeXmlDocument.SelectSingleNode("/soap:Envelope/soap:Header/wsse:Security", WSSecurityBasedCredentials.NamespaceManager);
			xmlNode.AppendChild(xml);
			memoryStream.Position = 0L;
			safeXmlDocument.Save(memoryStream);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0003C645 File Offset: 0x0003B645
		public override string ToString()
		{
			return string.Format("X509:<I>={0},<S>={1}", this.certificate.Issuer, this.certificate.Subject);
		}

		// Token: 0x04000CF5 RID: 3317
		private const string BinarySecurityTokenFormat = "<wsse:BinarySecurityToken EncodingType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\" ValueType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3\" wsu:Id=\"{0}\">{1}</wsse:BinarySecurityToken>";

		// Token: 0x04000CF6 RID: 3318
		private const string KeyInfoClauseFormat = "<wsse:SecurityTokenReference xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\" ><wsse:Reference URI=\"#{0}\" ValueType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3\" /></wsse:SecurityTokenReference>";

		// Token: 0x04000CF7 RID: 3319
		private const string WsSecurityX509CertPathSuffix = "/wssecurity/x509cert";

		// Token: 0x04000CF8 RID: 3320
		private readonly X509Certificate2 certificate;

		// Token: 0x04000CF9 RID: 3321
		private readonly KeyInfoClause keyInfoClause;
	}
}
