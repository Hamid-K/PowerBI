using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001D1 RID: 465
	internal sealed class PartnerTokenCredentials : WSSecurityBasedCredentials
	{
		// Token: 0x0600153D RID: 5437 RVA: 0x0003BA78 File Offset: 0x0003AA78
		internal PartnerTokenCredentials(string securityToken, string securityTokenReference)
			: base(securityToken, true)
		{
			EwsUtilities.ValidateParam(securityToken, "securityToken");
			EwsUtilities.ValidateParam(securityTokenReference, "securityTokenReference");
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.PreserveWhitespace = true;
			safeXmlDocument.LoadXml(securityTokenReference);
			this.keyInfoNode = new KeyInfoNode(safeXmlDocument.DocumentElement);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0003BAC8 File Offset: 0x0003AAC8
		internal override void PrepareWebRequest(IEwsHttpWebRequest request)
		{
			base.EwsUrl = request.RequestUri;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0003BAD6 File Offset: 0x0003AAD6
		internal override Uri AdjustUrl(Uri url)
		{
			return new Uri(ExchangeCredentials.GetUriWithoutSuffix(url) + "/wssecurity/symmetrickey");
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x0003BAED File Offset: 0x0003AAED
		internal override bool NeedSignature
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0003BAF0 File Offset: 0x0003AAF0
		internal override void Sign(MemoryStream memoryStream)
		{
			memoryStream.Position = 0L;
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.PreserveWhitespace = true;
			safeXmlDocument.Load(memoryStream);
			WSSecurityUtilityIdSignedXml wssecurityUtilityIdSignedXml = new WSSecurityUtilityIdSignedXml(safeXmlDocument);
			wssecurityUtilityIdSignedXml.SignedInfo.CanonicalizationMethod = "http://www.w3.org/2001/10/xml-exc-c14n#";
			wssecurityUtilityIdSignedXml.AddReference("/soap:Envelope/soap:Header/wsse:Security/wsu:Timestamp");
			wssecurityUtilityIdSignedXml.KeyInfo.AddClause(this.keyInfoNode);
			using (HMACSHA1 hmacsha = new HMACSHA1(ExchangeServiceBase.SessionKey))
			{
				wssecurityUtilityIdSignedXml.ComputeSignature(hmacsha);
			}
			XmlElement xml = wssecurityUtilityIdSignedXml.GetXml();
			XmlNode xmlNode = safeXmlDocument.SelectSingleNode("/soap:Envelope/soap:Header/wsse:Security", WSSecurityBasedCredentials.NamespaceManager);
			xmlNode.AppendChild(xml);
			memoryStream.Position = 0L;
			safeXmlDocument.Save(memoryStream);
		}

		// Token: 0x04000CDE RID: 3294
		private const string WsSecuritySymmetricKeyPathSuffix = "/wssecurity/symmetrickey";

		// Token: 0x04000CDF RID: 3295
		private readonly KeyInfoNode keyInfoNode;
	}
}
