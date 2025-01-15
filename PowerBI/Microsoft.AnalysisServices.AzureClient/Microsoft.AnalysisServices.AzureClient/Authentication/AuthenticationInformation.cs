using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x02000018 RID: 24
	internal sealed class AuthenticationInformation
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00002DCA File Offset: 0x00000FCA
		public AuthenticationInformation(AuthenticationEndpoint endpoint, string authority, string applicationId, string resourceId = null)
			: this()
		{
			this.Endpoint = endpoint;
			this.Authority = authority;
			this.ApplicationId = applicationId;
			this.ResourceId = resourceId;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public AuthenticationInformation(AuthenticationInformation info)
			: this()
		{
			this.Endpoint = info.Endpoint;
			this.authority = info.authority;
			this.ApplicationId = info.ApplicationId;
			this.ResourceId = info.ResourceId;
			this.IsCommonTenant = info.IsCommonTenant;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002E40 File Offset: 0x00001040
		private AuthenticationInformation(AuthenticationEndpoint endpoint, AuthenticationInformation.AuthenticationInformationRecord record)
			: this()
		{
			this.Endpoint = endpoint;
			if (endpoint != AuthenticationEndpoint.AadV1)
			{
				if (endpoint == AuthenticationEndpoint.AadV2)
				{
					this.Authority = record.Authority2;
				}
			}
			else
			{
				this.Authority = record.Authority;
			}
			this.ApplicationId = record.ApplicationId;
			this.ResourceId = record.ResourceId;
			this.PowerBIEndpoint = record.PowerBIEndpoint;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002EA2 File Offset: 0x000010A2
		private AuthenticationInformation()
		{
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002EAA File Offset: 0x000010AA
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002EB2 File Offset: 0x000010B2
		public AuthenticationEndpoint Endpoint { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002EBB File Offset: 0x000010BB
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002EC4 File Offset: 0x000010C4
		public string Authority
		{
			get
			{
				return this.authority;
			}
			private set
			{
				string text;
				AuthenticationInformation.ValidateAuthorityAndExtractTenant(value, out text);
				this.authority = value;
				AuthenticationEndpoint endpoint = this.Endpoint;
				if (endpoint == AuthenticationEndpoint.AadV1)
				{
					this.IsCommonTenant = string.Compare(text, "common", StringComparison.InvariantCultureIgnoreCase) == 0;
					return;
				}
				if (endpoint != AuthenticationEndpoint.AadV2)
				{
					this.IsCommonTenant = false;
					return;
				}
				this.IsCommonTenant = string.Compare(text, "organizations", StringComparison.InvariantCultureIgnoreCase) == 0;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002F24 File Offset: 0x00001124
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002F2C File Offset: 0x0000112C
		public string ApplicationId { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002F35 File Offset: 0x00001135
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002F3D File Offset: 0x0000113D
		public string ResourceId { get; internal set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002F46 File Offset: 0x00001146
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00002F4E File Offset: 0x0000114E
		public string PowerBIEndpoint { get; internal set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002F57 File Offset: 0x00001157
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00002F5F File Offset: 0x0000115F
		internal bool IsCommonTenant { get; private set; }

		// Token: 0x06000097 RID: 151 RVA: 0x00002F68 File Offset: 0x00001168
		public static void ValidateResourceForAuthentication(Uri resource)
		{
			AuthenticationInformation.AuthenticationInformationRecord authenticationInformationRecord;
			if (!AuthenticationInformation.TryFindAuthenticationInformation(resource, AuthenticationInformation.GetAllowedAuthenticationInformation(true), out authenticationInformationRecord) && !AuthenticationInformation.TryFindAuthenticationInformation(resource, AuthenticationInformation.GetAllowedAuthenticationInformation(false), out authenticationInformationRecord))
			{
				throw new ArgumentException(AuthenticationSR.Exception_UnknownAuthenticationInfo);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002FA0 File Offset: 0x000011A0
		public static AuthenticationInformation FindMatchingAuthenticationInformation(AuthenticationEndpoint endpoint, string identityProvider, Uri resource, bool bypassAuthInfoValidation)
		{
			AuthenticationInformation authenticationInformation = AuthenticationInformation.ExtractAuthenticationInformationFromIdentityProvider(identityProvider);
			if (authenticationInformation != null && bypassAuthInfoValidation)
			{
				return authenticationInformation;
			}
			AuthenticationInformation.AuthenticationInformationRecord authenticationInformationRecord;
			if ((AuthenticationInformation.TryFindAuthenticationInformation(resource, AuthenticationInformation.GetAllowedAuthenticationInformation(true), out authenticationInformationRecord) || AuthenticationInformation.TryFindAuthenticationInformation(resource, AuthenticationInformation.GetAllowedAuthenticationInformation(false), out authenticationInformationRecord)) && (authenticationInformation == null || authenticationInformation.CompareAuthenticationInformation(authenticationInformationRecord)))
			{
				return new AuthenticationInformation(endpoint, authenticationInformationRecord);
			}
			throw new ArgumentException(AuthenticationSR.Exception_UnknownAuthenticationInfo);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002FFC File Offset: 0x000011FC
		public void ReplaceCommonTenant(string tenantId)
		{
			AuthenticationEndpoint endpoint = this.Endpoint;
			if (endpoint == AuthenticationEndpoint.AadV1)
			{
				this.Authority = this.authority.Replace(AuthenticationInformation.V1EndpointCommonTenantPathSegment, string.Format("/{0}", tenantId));
				return;
			}
			if (endpoint != AuthenticationEndpoint.AadV2)
			{
				return;
			}
			this.Authority = this.authority.Replace(AuthenticationInformation.V2EndpointCommonTenantPathSegment, string.Format("/{0}", tenantId));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000305C File Offset: 0x0000125C
		internal static bool IsDataverseIdentityProvider(string identityProvider)
		{
			return string.Compare(identityProvider, "Dataverse", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000306D File Offset: 0x0000126D
		internal string GetTokenCacheKey()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}", new object[] { this.authority, this.ApplicationId, this.ResourceId });
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000030A0 File Offset: 0x000012A0
		private static bool TryFindAuthenticationInformation(Uri dataSource, AuthenticationInformation.AuthenticationInformationRecord[] knownRecords, out AuthenticationInformation.AuthenticationInformationRecord record)
		{
			record = null;
			for (int i = 0; i < knownRecords.Length; i++)
			{
				if (dataSource.Host.EndsWith(knownRecords[i].DomainPostfix, StringComparison.InvariantCultureIgnoreCase) && (record == null || knownRecords[i].DomainPostfix.Length > record.DomainPostfix.Length))
				{
					record = knownRecords[i];
				}
			}
			return record != null;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003100 File Offset: 0x00001300
		private static void ValidateAuthorityAndExtractTenant(string authority, out string tenant)
		{
			Uri uri;
			if (!Uri.TryCreate(authority, UriKind.Absolute, out uri) || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps) || string.IsNullOrEmpty(uri.AbsolutePath) || !uri.AbsolutePath.StartsWith("/") || uri.AbsolutePath.Length == 1)
			{
				throw new ArgumentException(AuthenticationSR.Exception_InvalidAuthorityFormat(authority), "authority");
			}
			int num = uri.AbsolutePath.IndexOf('/', 1);
			tenant = ((num == -1) ? uri.AbsolutePath.Substring(1) : uri.AbsolutePath.Substring(1, num - 1));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000031AC File Offset: 0x000013AC
		private static AuthenticationInformation ExtractAuthenticationInformationFromIdentityProvider(string identityProvider)
		{
			if (string.IsNullOrEmpty(identityProvider) || string.Compare(identityProvider, "Dataverse", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return null;
			}
			string[] array = identityProvider.Split(new char[] { ',' });
			if (array.Length > 3)
			{
				throw new ArgumentException(AuthenticationSR.Exception_InvalidIdentityProvider(identityProvider), "identityProvider");
			}
			AuthenticationInformation authenticationInformation = new AuthenticationInformation
			{
				Authority = array[0].Trim(),
				ApplicationId = ((array.Length >= 2) ? array[1].Trim() : null),
				ResourceId = ((array.Length == 3) ? array[2].Trim() : null)
			};
			if (string.IsNullOrEmpty(authenticationInformation.ApplicationId))
			{
				authenticationInformation.ApplicationId = "cf710c6e-dfcc-4fa8-a093-d47294e44c66";
			}
			return authenticationInformation;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003258 File Offset: 0x00001458
		private static AuthenticationInformation.AuthenticationInformationRecord[] GetAllowedAuthenticationInformation(bool isEmbeddedInfo)
		{
			if (isEmbeddedInfo)
			{
				if (AuthenticationInformation.embeddedSecurityConfig == null)
				{
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					string text = executingAssembly.GetManifestResourceNames().FirstOrDefault((string name) => name.EndsWith("ASAzureSecurityConfig.xml"));
					using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text))
					{
						AuthenticationInformation.embeddedSecurityConfig = AuthenticationInformation.DeserializeAuthenticationInformation(manifestResourceStream);
					}
				}
				return AuthenticationInformation.embeddedSecurityConfig;
			}
			if (AuthenticationInformation.remoteSecurityConfig == null)
			{
				using (WebClient webClient = new WebClient())
				{
					try
					{
						using (Stream stream = new MemoryStream(webClient.DownloadData("https://global.asazure.windows.net/ASAzureSecurityConfig.xml")))
						{
							AuthenticationInformation.remoteSecurityConfig = AuthenticationInformation.DeserializeAuthenticationInformation(stream);
						}
					}
					catch (WebException)
					{
						AuthenticationInformation.remoteSecurityConfig = new AuthenticationInformation.AuthenticationInformationRecord[0];
					}
				}
			}
			return AuthenticationInformation.remoteSecurityConfig;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000334C File Offset: 0x0000154C
		private static AuthenticationInformation.AuthenticationInformationRecord[] DeserializeAuthenticationInformation(Stream info)
		{
			AuthenticationInformation.AuthenticationInformationRecord[] array;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(info, new XmlDictionaryReaderQuotas()))
			{
				array = (AuthenticationInformation.AuthenticationInformationRecord[])new DataContractSerializer(typeof(AuthenticationInformation.AuthenticationInformationRecord[]), "AuthenticationInformations", string.Empty).ReadObject(xmlDictionaryReader, true);
			}
			return array;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000033A8 File Offset: 0x000015A8
		private bool CompareAuthenticationInformation(AuthenticationInformation.AuthenticationInformationRecord record)
		{
			switch (this.Endpoint)
			{
			case AuthenticationEndpoint.Unknown:
				if (string.Compare(this.authority, record.Authority, StringComparison.InvariantCultureIgnoreCase) != 0 && string.Compare(this.authority, record.Authority2, StringComparison.InvariantCultureIgnoreCase) != 0)
				{
					return false;
				}
				break;
			case AuthenticationEndpoint.AadV1:
				if (string.Compare(this.authority, record.Authority, StringComparison.InvariantCultureIgnoreCase) != 0)
				{
					return false;
				}
				break;
			case AuthenticationEndpoint.AadV2:
				if (string.Compare(this.authority, record.Authority2, StringComparison.InvariantCultureIgnoreCase) != 0)
				{
					return false;
				}
				break;
			}
			return string.Compare(this.ApplicationId, record.ApplicationId, StringComparison.InvariantCultureIgnoreCase) == 0 && string.Compare(this.ResourceId, record.ResourceId, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x04000042 RID: 66
		private const string EmbeddedSecurityConfigResourceName = "ASAzureSecurityConfig.xml";

		// Token: 0x04000043 RID: 67
		private const string RemoteSecurityConfigUrl = "https://global.asazure.windows.net/ASAzureSecurityConfig.xml";

		// Token: 0x04000044 RID: 68
		private const string V1EndpointCommonTenant = "common";

		// Token: 0x04000045 RID: 69
		private const string V2EndpointCommonTenant = "organizations";

		// Token: 0x04000046 RID: 70
		private const string DefaultApplicationId = "cf710c6e-dfcc-4fa8-a093-d47294e44c66";

		// Token: 0x04000047 RID: 71
		private const string DataverseIdentityProvider = "Dataverse";

		// Token: 0x04000048 RID: 72
		private const string TokenCacheKeyTemplate = "{0}|{1}|{2}";

		// Token: 0x04000049 RID: 73
		private static readonly string V1EndpointCommonTenantPathSegment = string.Format("/{0}", "common");

		// Token: 0x0400004A RID: 74
		private static readonly string V2EndpointCommonTenantPathSegment = string.Format("/{0}", "organizations");

		// Token: 0x0400004B RID: 75
		private static AuthenticationInformation.AuthenticationInformationRecord[] embeddedSecurityConfig;

		// Token: 0x0400004C RID: 76
		private static AuthenticationInformation.AuthenticationInformationRecord[] remoteSecurityConfig;

		// Token: 0x0400004D RID: 77
		private string authority;

		// Token: 0x0200004A RID: 74
		[DataContract(Name = "AuthenticationInformation", Namespace = "")]
		private sealed class AuthenticationInformationRecord
		{
			// Token: 0x1700005A RID: 90
			// (get) Token: 0x0600022A RID: 554 RVA: 0x0000AD62 File Offset: 0x00008F62
			// (set) Token: 0x0600022B RID: 555 RVA: 0x0000AD6A File Offset: 0x00008F6A
			[DataMember(Name = "DomainPostfix", Order = 0)]
			public string DomainPostfix { get; private set; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x0600022C RID: 556 RVA: 0x0000AD73 File Offset: 0x00008F73
			// (set) Token: 0x0600022D RID: 557 RVA: 0x0000AD7B File Offset: 0x00008F7B
			[DataMember(Name = "Authority", Order = 1)]
			public string Authority { get; private set; }

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x0600022E RID: 558 RVA: 0x0000AD84 File Offset: 0x00008F84
			// (set) Token: 0x0600022F RID: 559 RVA: 0x0000AD8C File Offset: 0x00008F8C
			[DataMember(Name = "Authority.v2", Order = 2, EmitDefaultValue = true)]
			public string Authority2 { get; private set; }

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000230 RID: 560 RVA: 0x0000AD95 File Offset: 0x00008F95
			// (set) Token: 0x06000231 RID: 561 RVA: 0x0000AD9D File Offset: 0x00008F9D
			[DataMember(Name = "ApplicationId", Order = 3)]
			public string ApplicationId { get; private set; }

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x06000232 RID: 562 RVA: 0x0000ADA6 File Offset: 0x00008FA6
			// (set) Token: 0x06000233 RID: 563 RVA: 0x0000ADAE File Offset: 0x00008FAE
			[DataMember(Name = "ResourceId", Order = 4, EmitDefaultValue = true)]
			public string ResourceId { get; private set; }

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x06000234 RID: 564 RVA: 0x0000ADB7 File Offset: 0x00008FB7
			// (set) Token: 0x06000235 RID: 565 RVA: 0x0000ADBF File Offset: 0x00008FBF
			[DataMember(Name = "PowerBIEndpoint", Order = 5, EmitDefaultValue = true)]
			public string PowerBIEndpoint { get; private set; }
		}
	}
}
