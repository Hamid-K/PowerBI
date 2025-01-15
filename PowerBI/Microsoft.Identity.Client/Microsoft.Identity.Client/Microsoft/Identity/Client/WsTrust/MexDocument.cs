using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.WsTrust
{
	// Token: 0x020001B9 RID: 441
	internal class MexDocument
	{
		// Token: 0x060013BB RID: 5051 RVA: 0x000428D0 File Offset: 0x00040AD0
		public MexDocument(string responseBody)
		{
			XDocument xdocument = XDocument.Parse(responseBody, LoadOptions.None);
			this.ReadPolicies(xdocument);
			this.ReadPolicyBindings(xdocument);
			this.SetPolicyEndpointAddresses(xdocument);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00042916 File Offset: 0x00040B16
		public WsTrustEndpoint GetWsTrustUsernamePasswordEndpoint()
		{
			return this.GetWsTrustEndpoint(UserAuthType.UsernamePassword);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0004291F File Offset: 0x00040B1F
		public WsTrustEndpoint GetWsTrustWindowsTransportEndpoint()
		{
			return this.GetWsTrustEndpoint(UserAuthType.IntegratedAuth);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00042928 File Offset: 0x00040B28
		private WsTrustEndpoint GetWsTrustEndpoint(UserAuthType userAuthType)
		{
			MexDocument.MexPolicy mexPolicy = this.SelectPolicy(userAuthType);
			if (mexPolicy == null)
			{
				return null;
			}
			return new WsTrustEndpoint(mexPolicy.Url, mexPolicy.Version, null, null);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00042958 File Offset: 0x00040B58
		private MexDocument.MexPolicy SelectPolicy(UserAuthType userAuthType)
		{
			return this._policies.Values.FirstOrDefault((MexDocument.MexPolicy p) => p.Url != null && p.AuthType == userAuthType && p.Version == WsTrustVersion.WsTrust13) ?? this._policies.Values.FirstOrDefault((MexDocument.MexPolicy p) => p.Url != null && p.AuthType == userAuthType);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000429B0 File Offset: 0x00040BB0
		private void ReadPolicies(XContainer mexDocument)
		{
			foreach (XElement xelement in MexDocument.FindElements(mexDocument, XmlNamespace.Wsp, "Policy"))
			{
				XElement xelement2 = xelement.Elements(XmlNamespace.Wsp + "ExactlyOne").FirstOrDefault<XElement>();
				if (xelement2 != null)
				{
					foreach (XElement xelement3 in xelement2.Descendants(XmlNamespace.Wsp + "All"))
					{
						XNamespace xnamespace = XmlNamespace.Sp;
						XElement xelement4 = xelement3.Elements(XmlNamespace.Http + "NegotiateAuthentication").FirstOrDefault<XElement>();
						if (xelement4 != null)
						{
							this.AddPolicy(xelement, UserAuthType.IntegratedAuth);
						}
						xelement4 = xelement3.Elements(xnamespace + "SignedEncryptedSupportingTokens").FirstOrDefault<XElement>();
						if (xelement4 != null || (xelement4 = xelement3.Elements(XmlNamespace.Sp2005 + "SignedSupportingTokens").FirstOrDefault<XElement>()) != null)
						{
							xnamespace = XmlNamespace.Sp2005;
							XElement xelement5 = xelement4.Elements(XmlNamespace.Wsp + "Policy").FirstOrDefault<XElement>();
							if (xelement5 != null)
							{
								XElement xelement6 = xelement5.Elements(xnamespace + "UsernameToken").FirstOrDefault<XElement>();
								if (xelement6 != null)
								{
									XElement xelement7 = xelement6.Elements(XmlNamespace.Wsp + "Policy").FirstOrDefault<XElement>();
									if (xelement7 != null && xelement7.Elements(xnamespace + "WssUsernameToken10").FirstOrDefault<XElement>() != null)
									{
										this.AddPolicy(xelement, UserAuthType.UsernamePassword);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00042B84 File Offset: 0x00040D84
		private void ReadPolicyBindings(XContainer mexDocument)
		{
			foreach (XElement xelement in MexDocument.FindElements(mexDocument, XmlNamespace.Wsdl, "binding"))
			{
				foreach (XElement xelement2 in xelement.Elements(XmlNamespace.Wsp + "PolicyReference"))
				{
					XAttribute xattribute = xelement2.Attribute("URI");
					if (xattribute != null && this._policies.ContainsKey(xattribute.Value))
					{
						XAttribute xattribute2 = xelement.Attribute("name");
						if (xattribute2 != null)
						{
							XElement xelement3 = xelement.Elements(XmlNamespace.Wsdl + "operation").FirstOrDefault<XElement>();
							if (xelement3 != null)
							{
								XElement xelement4 = xelement3.Elements(XmlNamespace.Soap12 + "operation").FirstOrDefault<XElement>();
								if (xelement4 != null)
								{
									XAttribute xattribute3 = xelement4.Attribute("soapAction");
									if (xattribute3 != null && (string.Compare(XmlNamespace.Issue.ToString(), xattribute3.Value, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(XmlNamespace.Issue2005.ToString(), xattribute3.Value, StringComparison.OrdinalIgnoreCase) == 0))
									{
										bool flag = string.Compare(XmlNamespace.Issue2005.ToString(), xattribute3.Value, StringComparison.OrdinalIgnoreCase) == 0;
										this._policies[xattribute.Value].Version = (flag ? WsTrustVersion.WsTrust2005 : WsTrustVersion.WsTrust13);
										XElement xelement5 = xelement.Elements(XmlNamespace.Soap12 + "binding").FirstOrDefault<XElement>();
										if (xelement5 != null)
										{
											XAttribute xattribute4 = xelement5.Attribute("transport");
											if (xattribute4 != null && string.Compare("http://schemas.xmlsoap.org/soap/http", xattribute4.Value, StringComparison.OrdinalIgnoreCase) == 0)
											{
												this._bindings.Add(xattribute2.Value, this._policies[xattribute.Value]);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00042DC0 File Offset: 0x00040FC0
		private void SetPolicyEndpointAddresses(XContainer mexDocument)
		{
			foreach (XElement xelement in MexDocument.FindElements(mexDocument, XmlNamespace.Wsdl, "service").First<XElement>().Elements(XmlNamespace.Wsdl + "port"))
			{
				XAttribute xattribute = xelement.Attribute("binding");
				if (xattribute != null)
				{
					string[] array = xattribute.Value.Split(new char[] { ':' }, 2);
					if (array.Length >= 2 && this._bindings.ContainsKey(array[1]))
					{
						XElement xelement2 = xelement.Elements(XmlNamespace.Wsa10 + "EndpointReference").FirstOrDefault<XElement>();
						if (xelement2 != null)
						{
							XElement xelement3 = xelement2.Elements(XmlNamespace.Wsa10 + "Address").FirstOrDefault<XElement>();
							if (xelement3 != null && Uri.IsWellFormedUriString(xelement3.Value, UriKind.Absolute))
							{
								this._bindings[array[1]].Url = new Uri(xelement3.Value);
							}
						}
					}
				}
			}
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00042EE4 File Offset: 0x000410E4
		private static IEnumerable<XElement> FindElements(XContainer mexDocument, XNamespace xNamespace, string element)
		{
			IEnumerable<XElement> enumerable = mexDocument.Elements();
			IEnumerable<XElement> enumerable2;
			if (enumerable == null)
			{
				enumerable2 = null;
			}
			else
			{
				XElement xelement = enumerable.First<XElement>();
				enumerable2 = ((xelement != null) ? xelement.Elements(xNamespace + element) : null);
			}
			IEnumerable<XElement> enumerable3 = enumerable2;
			if (enumerable3 == null)
			{
				throw new MsalClientException("parsing_ws_metadata_exchange_failed", "Parsing WS metadata exchange failed.  Could not find XML data.");
			}
			if (!enumerable3.Any<XElement>())
			{
				enumerable3 = mexDocument.Elements().DescendantsAndSelf().Elements(xNamespace + element);
				if (!enumerable3.Any<XElement>())
				{
					throw new MsalClientException("parsing_ws_metadata_exchange_failed", "Parsing WS metadata exchange failed.  Could not find element " + element + ".");
				}
			}
			return enumerable3;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00042F70 File Offset: 0x00041170
		private void AddPolicy(XElement policy, UserAuthType policyAuthType)
		{
			if ((policy.Descendants(XmlNamespace.Sp + "TransportBinding").FirstOrDefault<XElement>() ?? policy.Descendants(XmlNamespace.Sp2005 + "TransportBinding").FirstOrDefault<XElement>()) != null)
			{
				XAttribute xattribute = policy.Attribute(XmlNamespace.Wsu + "Id");
				if (xattribute != null)
				{
					this._policies.Add("#" + xattribute.Value, new MexDocument.MexPolicy
					{
						Id = xattribute.Value,
						AuthType = policyAuthType
					});
				}
			}
		}

		// Token: 0x04000821 RID: 2081
		private const string WsTrustSoapTransport = "http://schemas.xmlsoap.org/soap/http";

		// Token: 0x04000822 RID: 2082
		private readonly Dictionary<string, MexDocument.MexPolicy> _policies = new Dictionary<string, MexDocument.MexPolicy>();

		// Token: 0x04000823 RID: 2083
		private readonly Dictionary<string, MexDocument.MexPolicy> _bindings = new Dictionary<string, MexDocument.MexPolicy>();

		// Token: 0x0200044F RID: 1103
		private class MexPolicy
		{
			// Token: 0x1700061B RID: 1563
			// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x0006FC7E File Offset: 0x0006DE7E
			// (set) Token: 0x06001FA4 RID: 8100 RVA: 0x0006FC86 File Offset: 0x0006DE86
			public WsTrustVersion Version { get; set; }

			// Token: 0x1700061C RID: 1564
			// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x0006FC8F File Offset: 0x0006DE8F
			// (set) Token: 0x06001FA6 RID: 8102 RVA: 0x0006FC97 File Offset: 0x0006DE97
			public string Id { get; set; }

			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x0006FCA0 File Offset: 0x0006DEA0
			// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x0006FCA8 File Offset: 0x0006DEA8
			public UserAuthType AuthType { get; set; }

			// Token: 0x1700061E RID: 1566
			// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x0006FCB1 File Offset: 0x0006DEB1
			// (set) Token: 0x06001FAA RID: 8106 RVA: 0x0006FCB9 File Offset: 0x0006DEB9
			public Uri Url { get; set; }
		}
	}
}
