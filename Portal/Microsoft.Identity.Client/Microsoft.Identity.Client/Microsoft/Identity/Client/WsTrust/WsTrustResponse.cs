using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.WsTrust
{
	// Token: 0x020001BD RID: 445
	internal class WsTrustResponse
	{
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00043597 File Offset: 0x00041797
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x0004359F File Offset: 0x0004179F
		public string Token { get; private set; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x000435A8 File Offset: 0x000417A8
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x000435B0 File Offset: 0x000417B0
		public string TokenType { get; private set; }

		// Token: 0x060013E3 RID: 5091 RVA: 0x000435B9 File Offset: 0x000417B9
		public static WsTrustResponse CreateFromResponse(string response, WsTrustVersion version)
		{
			return WsTrustResponse.CreateFromResponseDocument(XDocument.Parse(response, LoadOptions.PreserveWhitespace), version);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x000435C8 File Offset: 0x000417C8
		public static string ReadErrorResponse(XDocument responseDocument)
		{
			string text = null;
			XElement xelement = responseDocument.Descendants(XmlNamespace.SoapEnvelope + "Body").FirstOrDefault<XElement>();
			if (xelement != null)
			{
				XElement xelement2 = xelement.Elements(XmlNamespace.SoapEnvelope + "Fault").FirstOrDefault<XElement>();
				if (xelement2 != null)
				{
					text = WsTrustResponse.GetFaultMessage(xelement2);
				}
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				text = responseDocument.ToString();
			}
			return text;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0004362C File Offset: 0x0004182C
		private static string GetFaultMessage(XElement fault)
		{
			XElement xelement = fault.Elements(XmlNamespace.SoapEnvelope + "Reason").FirstOrDefault<XElement>();
			if (xelement != null)
			{
				XElement xelement2 = xelement.Elements(XmlNamespace.SoapEnvelope + "Text").FirstOrDefault<XElement>();
				if (xelement2 != null)
				{
					using (XmlReader xmlReader = xelement2.CreateReader())
					{
						xmlReader.MoveToContent();
						return xmlReader.ReadInnerXml();
					}
				}
			}
			return null;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x000436AC File Offset: 0x000418AC
		internal static WsTrustResponse CreateFromResponseDocument(XDocument responseDocument, WsTrustVersion version)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			XNamespace xnamespace = XmlNamespace.Trust;
			if (version == WsTrustVersion.WsTrust2005)
			{
				xnamespace = XmlNamespace.Trust2005;
			}
			bool flag = true;
			if (version == WsTrustVersion.WsTrust13 && responseDocument.Descendants(xnamespace + "RequestSecurityTokenResponseCollection").FirstOrDefault<XElement>() == null)
			{
				flag = false;
			}
			if (!flag)
			{
				return null;
			}
			foreach (XElement xelement in responseDocument.Descendants(xnamespace + "RequestSecurityTokenResponse"))
			{
				XElement xelement2 = xelement.Elements(xnamespace + "TokenType").FirstOrDefault<XElement>();
				if (xelement2 != null)
				{
					XElement xelement3 = xelement.Elements(xnamespace + "RequestedSecurityToken").FirstOrDefault<XElement>();
					if (xelement3 != null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (XNode xnode in xelement3.Nodes())
						{
							stringBuilder.Append(xnode.ToString(SaveOptions.DisableFormatting));
						}
						dictionary.Add(xelement2.Value, stringBuilder.ToString());
					}
				}
			}
			if (dictionary.Count == 0)
			{
				return null;
			}
			string text = (dictionary.ContainsKey("urn:oasis:names:tc:SAML:1.0:assertion") ? "urn:oasis:names:tc:SAML:1.0:assertion" : dictionary.Keys.First<string>());
			return new WsTrustResponse
			{
				TokenType = text,
				Token = dictionary[text]
			};
		}

		// Token: 0x04000831 RID: 2097
		public const string Saml1Assertion = "urn:oasis:names:tc:SAML:1.0:assertion";
	}
}
