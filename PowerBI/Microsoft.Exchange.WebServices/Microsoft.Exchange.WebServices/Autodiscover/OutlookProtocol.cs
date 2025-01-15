using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000025 RID: 37
	[EditorBrowsable(1)]
	internal sealed class OutlookProtocol
	{
		// Token: 0x06000182 RID: 386 RVA: 0x000077C7 File Offset: 0x000067C7
		internal OutlookProtocol()
		{
			this.internalOutlookWebAccessUrls = new WebClientUrlCollection();
			this.externalOutlookWebAccessUrls = new WebClientUrlCollection();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000077E8 File Offset: 0x000067E8
		internal void LoadFromXml(EwsXmlReader reader)
		{
			do
			{
				reader.Read();
				if (reader.NodeType == 1)
				{
					string localName;
					if ((localName = reader.LocalName) != null)
					{
						if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600017d-1 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(38);
							dictionary.Add("Type", 0);
							dictionary.Add("AuthPackage", 1);
							dictionary.Add("Server", 2);
							dictionary.Add("ServerDN", 3);
							dictionary.Add("ServerVersion", 4);
							dictionary.Add("AD", 5);
							dictionary.Add("MdbDN", 6);
							dictionary.Add("EwsUrl", 7);
							dictionary.Add("EmwsUrl", 8);
							dictionary.Add("ASUrl", 9);
							dictionary.Add("OOFUrl", 10);
							dictionary.Add("UMUrl", 11);
							dictionary.Add("OABUrl", 12);
							dictionary.Add("PublicFolderServer", 13);
							dictionary.Add("Internal", 14);
							dictionary.Add("External", 15);
							dictionary.Add("SSL", 16);
							dictionary.Add("SharingUrl", 17);
							dictionary.Add("EcpUrl", 18);
							dictionary.Add("EcpUrl-um", 19);
							dictionary.Add("EcpUrl-aggr", 20);
							dictionary.Add("EcpUrl-sms", 21);
							dictionary.Add("EcpUrl-mt", 22);
							dictionary.Add("EcpUrl-ret", 23);
							dictionary.Add("EcpUrl-publish", 24);
							dictionary.Add("EcpUrl-photo", 25);
							dictionary.Add("ExchangeRpcUrl", 26);
							dictionary.Add("EwsPartnerUrl", 27);
							dictionary.Add("EcpUrl-connect", 28);
							dictionary.Add("EcpUrl-tm", 29);
							dictionary.Add("EcpUrl-tmCreating", 30);
							dictionary.Add("EcpUrl-tmEditing", 31);
							dictionary.Add("EcpUrl-tmHiding", 32);
							dictionary.Add("SiteMailboxCreationURL", 33);
							dictionary.Add("EcpUrl-extinstall", 34);
							dictionary.Add("ServerExclusiveConnect", 35);
							dictionary.Add("CertPrincipalName", 36);
							dictionary.Add("GroupingInformation", 37);
							<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600017d-1 = dictionary;
						}
						int num;
						if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600017d-1.TryGetValue(localName, ref num))
						{
							switch (num)
							{
							case 0:
								this.ProtocolType = OutlookProtocol.ProtocolNameToType(reader.ReadElementValue());
								goto IL_0572;
							case 1:
								this.authPackage = reader.ReadElementValue();
								goto IL_0572;
							case 2:
								this.server = reader.ReadElementValue();
								goto IL_0572;
							case 3:
								this.serverDN = reader.ReadElementValue();
								goto IL_0572;
							case 4:
								reader.ReadElementValue();
								goto IL_0572;
							case 5:
								this.activeDirectoryServer = reader.ReadElementValue();
								goto IL_0572;
							case 6:
								this.mailboxDN = reader.ReadElementValue();
								goto IL_0572;
							case 7:
								this.exchangeWebServicesUrl = reader.ReadElementValue();
								goto IL_0572;
							case 8:
								this.exchangeManagementWebServicesUrl = reader.ReadElementValue();
								goto IL_0572;
							case 9:
								this.availabilityServiceUrl = reader.ReadElementValue();
								goto IL_0572;
							case 10:
								reader.ReadElementValue();
								goto IL_0572;
							case 11:
								this.unifiedMessagingUrl = reader.ReadElementValue();
								goto IL_0572;
							case 12:
								this.offlineAddressBookUrl = reader.ReadElementValue();
								goto IL_0572;
							case 13:
								this.publicFolderServer = reader.ReadElementValue();
								goto IL_0572;
							case 14:
								OutlookProtocol.LoadWebClientUrlsFromXml(reader, this.internalOutlookWebAccessUrls, reader.LocalName);
								goto IL_0572;
							case 15:
								OutlookProtocol.LoadWebClientUrlsFromXml(reader, this.externalOutlookWebAccessUrls, reader.LocalName);
								goto IL_0572;
							case 16:
							{
								string text = reader.ReadElementValue();
								this.sslEnabled = text.Equals("On", 5);
								goto IL_0572;
							}
							case 17:
								this.sharingEnabled = reader.ReadElementValue().Length > 0;
								goto IL_0572;
							case 18:
								this.ecpUrl = reader.ReadElementValue();
								goto IL_0572;
							case 19:
								this.ecpUrlUm = reader.ReadElementValue();
								goto IL_0572;
							case 20:
								this.ecpUrlAggr = reader.ReadElementValue();
								goto IL_0572;
							case 21:
								this.ecpUrlSms = reader.ReadElementValue();
								goto IL_0572;
							case 22:
								this.ecpUrlMt = reader.ReadElementValue();
								goto IL_0572;
							case 23:
								this.ecpUrlRet = reader.ReadElementValue();
								goto IL_0572;
							case 24:
								this.ecpUrlPublish = reader.ReadElementValue();
								goto IL_0572;
							case 25:
								this.ecpUrlPhoto = reader.ReadElementValue();
								goto IL_0572;
							case 26:
								this.exchangeRpcUrl = reader.ReadElementValue();
								goto IL_0572;
							case 27:
								this.exchangeWebServicesPartnerUrl = reader.ReadElementValue();
								goto IL_0572;
							case 28:
								this.ecpUrlConnect = reader.ReadElementValue();
								goto IL_0572;
							case 29:
								this.ecpUrlTm = reader.ReadElementValue();
								goto IL_0572;
							case 30:
								this.ecpUrlTmCreating = reader.ReadElementValue();
								goto IL_0572;
							case 31:
								this.ecpUrlTmEditing = reader.ReadElementValue();
								goto IL_0572;
							case 32:
								this.ecpUrlTmHiding = reader.ReadElementValue();
								goto IL_0572;
							case 33:
								this.siteMailboxCreationURL = reader.ReadElementValue();
								goto IL_0572;
							case 34:
								this.ecpUrlExtInstall = reader.ReadElementValue();
								goto IL_0572;
							case 35:
							{
								string text2 = reader.ReadElementValue();
								this.serverExclusiveConnect = text2.Equals("On", 5);
								goto IL_0572;
							}
							case 36:
								this.certPrincipalName = reader.ReadElementValue();
								goto IL_0572;
							case 37:
								this.groupingInformation = reader.ReadElementValue();
								goto IL_0572;
							}
						}
					}
					reader.SkipCurrentElement();
				}
				IL_0572:;
			}
			while (!reader.IsEndElement(XmlNamespace.NotSpecified, "Protocol"));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007D78 File Offset: 0x00006D78
		private static OutlookProtocolType ProtocolNameToType(string protocolName)
		{
			OutlookProtocolType outlookProtocolType;
			if (!OutlookProtocol.protocolNameToTypeMap.Member.TryGetValue(protocolName, ref outlookProtocolType))
			{
				outlookProtocolType = OutlookProtocolType.Unknown;
			}
			return outlookProtocolType;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007D9C File Offset: 0x00006D9C
		private static void LoadWebClientUrlsFromXml(EwsXmlReader reader, WebClientUrlCollection webClientUrls, string elementName)
		{
			do
			{
				reader.Read();
				if (reader.NodeType == 1)
				{
					string localName;
					if ((localName = reader.LocalName) != null && localName == "OWAUrl")
					{
						string text = reader.ReadAttributeValue("AuthenticationMethod");
						string text2 = reader.ReadElementValue();
						WebClientUrl webClientUrl = new WebClientUrl(text, text2);
						webClientUrls.Urls.Add(webClientUrl);
					}
					else
					{
						reader.SkipCurrentElement();
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.NotSpecified, elementName));
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007E08 File Offset: 0x00006E08
		private string ConvertEcpFragmentToUrl(string fragment)
		{
			if (!string.IsNullOrEmpty(this.ecpUrl) && !string.IsNullOrEmpty(fragment))
			{
				return this.ecpUrl + fragment;
			}
			return null;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007E4C File Offset: 0x00006E4C
		internal void ConvertToUserSettings(List<UserSettingName> requestedSettings, GetUserSettingsResponse response)
		{
			if (this.ConverterDictionary != null)
			{
				IEnumerable<KeyValuePair<UserSettingName, Func<OutlookProtocol, object>>> enumerable = Enumerable.Where<KeyValuePair<UserSettingName, Func<OutlookProtocol, object>>>(this.ConverterDictionary, (KeyValuePair<UserSettingName, Func<OutlookProtocol, object>> converter) => requestedSettings.Contains(converter.Key));
				foreach (KeyValuePair<UserSettingName, Func<OutlookProtocol, object>> keyValuePair in enumerable)
				{
					object obj = keyValuePair.Value.Invoke(this);
					if (obj != null)
					{
						response.Settings[keyValuePair.Key] = obj;
					}
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00007EF0 File Offset: 0x00006EF0
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00007EF8 File Offset: 0x00006EF8
		internal OutlookProtocolType ProtocolType { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007F04 File Offset: 0x00006F04
		private Dictionary<UserSettingName, Func<OutlookProtocol, object>> ConverterDictionary
		{
			get
			{
				switch (this.ProtocolType)
				{
				case OutlookProtocolType.Rpc:
					return OutlookProtocol.internalProtocolConverterDictionary.Member;
				case OutlookProtocolType.RpcOverHttp:
					return OutlookProtocol.externalProtocolConverterDictionary.Member;
				case OutlookProtocolType.Web:
					return OutlookProtocol.webProtocolConverterDictionary.Member;
				default:
					return null;
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007F4E File Offset: 0x00006F4E
		internal static List<UserSettingName> AvailableUserSettings
		{
			get
			{
				return OutlookProtocol.availableUserSettings.Member;
			}
		}

		// Token: 0x0400008D RID: 141
		private const string EXCH = "EXCH";

		// Token: 0x0400008E RID: 142
		private const string EXPR = "EXPR";

		// Token: 0x0400008F RID: 143
		private const string WEB = "WEB";

		// Token: 0x04000090 RID: 144
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>> commonProtocolSettings = new LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>>(delegate
		{
			Dictionary<UserSettingName, Func<OutlookProtocol, object>> dictionary = new Dictionary<UserSettingName, Func<OutlookProtocol, object>>();
			dictionary.Add(UserSettingName.EcpDeliveryReportUrlFragment, (OutlookProtocol p) => p.ecpUrlMt);
			dictionary.Add(UserSettingName.EcpEmailSubscriptionsUrlFragment, (OutlookProtocol p) => p.ecpUrlAggr);
			dictionary.Add(UserSettingName.EcpPublishingUrlFragment, (OutlookProtocol p) => p.ecpUrlPublish);
			dictionary.Add(UserSettingName.EcpPhotoUrlFragment, (OutlookProtocol p) => p.ecpUrlPhoto);
			dictionary.Add(UserSettingName.EcpRetentionPolicyTagsUrlFragment, (OutlookProtocol p) => p.ecpUrlRet);
			dictionary.Add(UserSettingName.EcpTextMessagingUrlFragment, (OutlookProtocol p) => p.ecpUrlSms);
			dictionary.Add(UserSettingName.EcpVoicemailUrlFragment, (OutlookProtocol p) => p.ecpUrlUm);
			dictionary.Add(UserSettingName.EcpConnectUrlFragment, (OutlookProtocol p) => p.ecpUrlConnect);
			dictionary.Add(UserSettingName.EcpTeamMailboxUrlFragment, (OutlookProtocol p) => p.ecpUrlTm);
			dictionary.Add(UserSettingName.EcpTeamMailboxCreatingUrlFragment, (OutlookProtocol p) => p.ecpUrlTmCreating);
			dictionary.Add(UserSettingName.EcpTeamMailboxEditingUrlFragment, (OutlookProtocol p) => p.ecpUrlTmEditing);
			dictionary.Add(UserSettingName.EcpExtensionInstallationUrlFragment, (OutlookProtocol p) => p.ecpUrlExtInstall);
			dictionary.Add(UserSettingName.SiteMailboxCreationURL, (OutlookProtocol p) => p.siteMailboxCreationURL);
			return dictionary;
		});

		// Token: 0x04000091 RID: 145
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>> internalProtocolSettings = new LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>>(delegate
		{
			Dictionary<UserSettingName, Func<OutlookProtocol, object>> dictionary2 = new Dictionary<UserSettingName, Func<OutlookProtocol, object>>();
			dictionary2.Add(UserSettingName.ActiveDirectoryServer, (OutlookProtocol p) => p.activeDirectoryServer);
			dictionary2.Add(UserSettingName.CrossOrganizationSharingEnabled, (OutlookProtocol p) => p.sharingEnabled.ToString());
			dictionary2.Add(UserSettingName.InternalEcpUrl, (OutlookProtocol p) => p.ecpUrl);
			dictionary2.Add(UserSettingName.InternalEcpDeliveryReportUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlMt));
			dictionary2.Add(UserSettingName.InternalEcpEmailSubscriptionsUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlAggr));
			dictionary2.Add(UserSettingName.InternalEcpPublishingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlPublish));
			dictionary2.Add(UserSettingName.InternalEcpPhotoUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlPhoto));
			dictionary2.Add(UserSettingName.InternalEcpRetentionPolicyTagsUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlRet));
			dictionary2.Add(UserSettingName.InternalEcpTextMessagingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlSms));
			dictionary2.Add(UserSettingName.InternalEcpVoicemailUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlUm));
			dictionary2.Add(UserSettingName.InternalEcpConnectUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlConnect));
			dictionary2.Add(UserSettingName.InternalEcpTeamMailboxUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTm));
			dictionary2.Add(UserSettingName.InternalEcpTeamMailboxCreatingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTmCreating));
			dictionary2.Add(UserSettingName.InternalEcpTeamMailboxEditingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTmEditing));
			dictionary2.Add(UserSettingName.InternalEcpTeamMailboxHidingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTmHiding));
			dictionary2.Add(UserSettingName.InternalEcpExtensionInstallationUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlExtInstall));
			dictionary2.Add(UserSettingName.InternalEwsUrl, (OutlookProtocol p) => p.exchangeWebServicesUrl ?? p.availabilityServiceUrl);
			dictionary2.Add(UserSettingName.InternalEmwsUrl, (OutlookProtocol p) => p.exchangeManagementWebServicesUrl);
			dictionary2.Add(UserSettingName.InternalMailboxServerDN, (OutlookProtocol p) => p.serverDN);
			dictionary2.Add(UserSettingName.InternalRpcClientServer, (OutlookProtocol p) => p.server);
			dictionary2.Add(UserSettingName.InternalOABUrl, (OutlookProtocol p) => p.offlineAddressBookUrl);
			dictionary2.Add(UserSettingName.InternalUMUrl, (OutlookProtocol p) => p.unifiedMessagingUrl);
			dictionary2.Add(UserSettingName.MailboxDN, (OutlookProtocol p) => p.mailboxDN);
			dictionary2.Add(UserSettingName.PublicFolderServer, (OutlookProtocol p) => p.publicFolderServer);
			dictionary2.Add(UserSettingName.InternalServerExclusiveConnect, (OutlookProtocol p) => p.serverExclusiveConnect);
			return dictionary2;
		});

		// Token: 0x04000092 RID: 146
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>> externalProtocolSettings = new LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>>(delegate
		{
			Dictionary<UserSettingName, Func<OutlookProtocol, object>> dictionary3 = new Dictionary<UserSettingName, Func<OutlookProtocol, object>>();
			dictionary3.Add(UserSettingName.ExternalEcpDeliveryReportUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlRet));
			dictionary3.Add(UserSettingName.ExternalEcpEmailSubscriptionsUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlAggr));
			dictionary3.Add(UserSettingName.ExternalEcpPublishingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlPublish));
			dictionary3.Add(UserSettingName.ExternalEcpPhotoUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlPhoto));
			dictionary3.Add(UserSettingName.ExternalEcpRetentionPolicyTagsUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlRet));
			dictionary3.Add(UserSettingName.ExternalEcpTextMessagingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlSms));
			dictionary3.Add(UserSettingName.ExternalEcpUrl, (OutlookProtocol p) => p.ecpUrl);
			dictionary3.Add(UserSettingName.ExternalEcpVoicemailUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlUm));
			dictionary3.Add(UserSettingName.ExternalEcpConnectUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlConnect));
			dictionary3.Add(UserSettingName.ExternalEcpTeamMailboxUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTm));
			dictionary3.Add(UserSettingName.ExternalEcpTeamMailboxCreatingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTmCreating));
			dictionary3.Add(UserSettingName.ExternalEcpTeamMailboxEditingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTmEditing));
			dictionary3.Add(UserSettingName.ExternalEcpTeamMailboxHidingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTmHiding));
			dictionary3.Add(UserSettingName.ExternalEcpExtensionInstallationUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlExtInstall));
			dictionary3.Add(UserSettingName.ExternalEwsUrl, (OutlookProtocol p) => p.exchangeWebServicesUrl ?? p.availabilityServiceUrl);
			dictionary3.Add(UserSettingName.ExternalEmwsUrl, (OutlookProtocol p) => p.exchangeManagementWebServicesUrl);
			dictionary3.Add(UserSettingName.ExternalMailboxServer, (OutlookProtocol p) => p.server);
			dictionary3.Add(UserSettingName.ExternalMailboxServerAuthenticationMethods, (OutlookProtocol p) => p.authPackage);
			dictionary3.Add(UserSettingName.ExternalMailboxServerRequiresSSL, (OutlookProtocol p) => p.sslEnabled.ToString());
			dictionary3.Add(UserSettingName.ExternalOABUrl, (OutlookProtocol p) => p.offlineAddressBookUrl);
			dictionary3.Add(UserSettingName.ExternalUMUrl, (OutlookProtocol p) => p.unifiedMessagingUrl);
			dictionary3.Add(UserSettingName.ExchangeRpcUrl, (OutlookProtocol p) => p.exchangeRpcUrl);
			dictionary3.Add(UserSettingName.EwsPartnerUrl, (OutlookProtocol p) => p.exchangeWebServicesPartnerUrl);
			dictionary3.Add(UserSettingName.ExternalServerExclusiveConnect, (OutlookProtocol p) => p.serverExclusiveConnect.ToString());
			dictionary3.Add(UserSettingName.CertPrincipalName, (OutlookProtocol p) => p.certPrincipalName);
			dictionary3.Add(UserSettingName.GroupingInformation, (OutlookProtocol p) => p.groupingInformation);
			return dictionary3;
		});

		// Token: 0x04000093 RID: 147
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>> internalProtocolConverterDictionary = new LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>>(delegate
		{
			Dictionary<UserSettingName, Func<OutlookProtocol, object>> results = new Dictionary<UserSettingName, Func<OutlookProtocol, object>>();
			Enumerable.ToList<KeyValuePair<UserSettingName, Func<OutlookProtocol, object>>>(OutlookProtocol.commonProtocolSettings.Member).ForEach(delegate(KeyValuePair<UserSettingName, Func<OutlookProtocol, object>> kv)
			{
				results.Add(kv.Key, kv.Value);
			});
			Enumerable.ToList<KeyValuePair<UserSettingName, Func<OutlookProtocol, object>>>(OutlookProtocol.internalProtocolSettings.Member).ForEach(delegate(KeyValuePair<UserSettingName, Func<OutlookProtocol, object>> kv)
			{
				results.Add(kv.Key, kv.Value);
			});
			return results;
		});

		// Token: 0x04000094 RID: 148
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>> externalProtocolConverterDictionary = new LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>>(delegate
		{
			Dictionary<UserSettingName, Func<OutlookProtocol, object>> results = new Dictionary<UserSettingName, Func<OutlookProtocol, object>>();
			Enumerable.ToList<KeyValuePair<UserSettingName, Func<OutlookProtocol, object>>>(OutlookProtocol.commonProtocolSettings.Member).ForEach(delegate(KeyValuePair<UserSettingName, Func<OutlookProtocol, object>> kv)
			{
				results.Add(kv.Key, kv.Value);
			});
			Enumerable.ToList<KeyValuePair<UserSettingName, Func<OutlookProtocol, object>>>(OutlookProtocol.externalProtocolSettings.Member).ForEach(delegate(KeyValuePair<UserSettingName, Func<OutlookProtocol, object>> kv)
			{
				results.Add(kv.Key, kv.Value);
			});
			return results;
		});

		// Token: 0x04000095 RID: 149
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>> webProtocolConverterDictionary = new LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>>(delegate
		{
			Dictionary<UserSettingName, Func<OutlookProtocol, object>> dictionary4 = new Dictionary<UserSettingName, Func<OutlookProtocol, object>>();
			dictionary4.Add(UserSettingName.InternalWebClientUrls, (OutlookProtocol p) => p.internalOutlookWebAccessUrls);
			dictionary4.Add(UserSettingName.ExternalWebClientUrls, (OutlookProtocol p) => p.externalOutlookWebAccessUrls);
			return dictionary4;
		});

		// Token: 0x04000096 RID: 150
		private static LazyMember<List<UserSettingName>> availableUserSettings = new LazyMember<List<UserSettingName>>(delegate
		{
			List<UserSettingName> list = new List<UserSettingName>();
			list.AddRange(OutlookProtocol.commonProtocolSettings.Member.Keys);
			list.AddRange(OutlookProtocol.internalProtocolSettings.Member.Keys);
			list.AddRange(OutlookProtocol.externalProtocolSettings.Member.Keys);
			list.AddRange(OutlookProtocol.webProtocolConverterDictionary.Member.Keys);
			return list;
		});

		// Token: 0x04000097 RID: 151
		private static LazyMember<Dictionary<string, OutlookProtocolType>> protocolNameToTypeMap = new LazyMember<Dictionary<string, OutlookProtocolType>>(delegate
		{
			Dictionary<string, OutlookProtocolType> dictionary5 = new Dictionary<string, OutlookProtocolType>();
			dictionary5.Add("EXCH", OutlookProtocolType.Rpc);
			dictionary5.Add("EXPR", OutlookProtocolType.RpcOverHttp);
			dictionary5.Add("WEB", OutlookProtocolType.Web);
			return dictionary5;
		});

		// Token: 0x04000098 RID: 152
		private string activeDirectoryServer;

		// Token: 0x04000099 RID: 153
		private string authPackage;

		// Token: 0x0400009A RID: 154
		private string availabilityServiceUrl;

		// Token: 0x0400009B RID: 155
		private string ecpUrl;

		// Token: 0x0400009C RID: 156
		private string ecpUrlAggr;

		// Token: 0x0400009D RID: 157
		private string ecpUrlMt;

		// Token: 0x0400009E RID: 158
		private string ecpUrlPublish;

		// Token: 0x0400009F RID: 159
		private string ecpUrlPhoto;

		// Token: 0x040000A0 RID: 160
		private string ecpUrlConnect;

		// Token: 0x040000A1 RID: 161
		private string ecpUrlRet;

		// Token: 0x040000A2 RID: 162
		private string ecpUrlSms;

		// Token: 0x040000A3 RID: 163
		private string ecpUrlUm;

		// Token: 0x040000A4 RID: 164
		private string ecpUrlTm;

		// Token: 0x040000A5 RID: 165
		private string ecpUrlTmCreating;

		// Token: 0x040000A6 RID: 166
		private string ecpUrlTmEditing;

		// Token: 0x040000A7 RID: 167
		private string ecpUrlTmHiding;

		// Token: 0x040000A8 RID: 168
		private string siteMailboxCreationURL;

		// Token: 0x040000A9 RID: 169
		private string ecpUrlExtInstall;

		// Token: 0x040000AA RID: 170
		private string exchangeWebServicesUrl;

		// Token: 0x040000AB RID: 171
		private string exchangeManagementWebServicesUrl;

		// Token: 0x040000AC RID: 172
		private string mailboxDN;

		// Token: 0x040000AD RID: 173
		private string offlineAddressBookUrl;

		// Token: 0x040000AE RID: 174
		private string exchangeRpcUrl;

		// Token: 0x040000AF RID: 175
		private string exchangeWebServicesPartnerUrl;

		// Token: 0x040000B0 RID: 176
		private string publicFolderServer;

		// Token: 0x040000B1 RID: 177
		private string server;

		// Token: 0x040000B2 RID: 178
		private string serverDN;

		// Token: 0x040000B3 RID: 179
		private string unifiedMessagingUrl;

		// Token: 0x040000B4 RID: 180
		private bool sharingEnabled;

		// Token: 0x040000B5 RID: 181
		private bool sslEnabled;

		// Token: 0x040000B6 RID: 182
		private bool serverExclusiveConnect;

		// Token: 0x040000B7 RID: 183
		private string certPrincipalName;

		// Token: 0x040000B8 RID: 184
		private string groupingInformation;

		// Token: 0x040000B9 RID: 185
		private WebClientUrlCollection externalOutlookWebAccessUrls;

		// Token: 0x040000BA RID: 186
		private WebClientUrlCollection internalOutlookWebAccessUrls;
	}
}
