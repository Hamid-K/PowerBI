using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x020000FE RID: 254
	internal sealed class AuthenticationInformation
	{
		// Token: 0x06000EB5 RID: 3765 RVA: 0x00031A9A File Offset: 0x0002FC9A
		public AuthenticationInformation(AuthenticationEndpoint endpoint, string authority, string applicationId, string resourceId = null)
			: this()
		{
			this.Endpoint = endpoint;
			this.Authority = authority;
			this.ApplicationId = applicationId;
			this.ResourceId = resourceId;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00031AC0 File Offset: 0x0002FCC0
		public AuthenticationInformation(AuthenticationInformation info)
			: this()
		{
			this.Endpoint = info.Endpoint;
			this.authority = info.authority;
			this.ApplicationId = info.ApplicationId;
			this.ResourceId = info.ResourceId;
			this.IsCommonTenant = info.IsCommonTenant;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00031B10 File Offset: 0x0002FD10
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

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00031B72 File Offset: 0x0002FD72
		private AuthenticationInformation()
		{
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00031B7A File Offset: 0x0002FD7A
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x00031B82 File Offset: 0x0002FD82
		public AuthenticationEndpoint Endpoint { get; private set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00031B8B File Offset: 0x0002FD8B
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x00031B94 File Offset: 0x0002FD94
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

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00031BF4 File Offset: 0x0002FDF4
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x00031BFC File Offset: 0x0002FDFC
		public string ApplicationId { get; private set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00031C05 File Offset: 0x0002FE05
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x00031C0D File Offset: 0x0002FE0D
		public string ResourceId { get; internal set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00031C16 File Offset: 0x0002FE16
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x00031C1E File Offset: 0x0002FE1E
		public string PowerBIEndpoint { get; internal set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00031C27 File Offset: 0x0002FE27
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x00031C2F File Offset: 0x0002FE2F
		internal bool IsCommonTenant { get; private set; }

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00031C38 File Offset: 0x0002FE38
		public static void ValidateResourceForAuthentication(Uri resource)
		{
			AuthenticationInformation.AuthenticationInformationRecord authenticationInformationRecord;
			if (!AuthenticationInformation.TryFindAuthenticationInformation(resource, AuthenticationInformation.GetAllowedAuthenticationInformation(true), out authenticationInformationRecord) && !AuthenticationInformation.TryFindAuthenticationInformation(resource, AuthenticationInformation.GetAllowedAuthenticationInformation(false), out authenticationInformationRecord))
			{
				throw new ArgumentException(AuthenticationSR.Exception_UnknownAuthenticationInfo);
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00031C70 File Offset: 0x0002FE70
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

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00031CCC File Offset: 0x0002FECC
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

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00031D2C File Offset: 0x0002FF2C
		internal static bool IsDataverseIdentityProvider(string identityProvider)
		{
			return string.Compare(identityProvider, "Dataverse", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00031D3D File Offset: 0x0002FF3D
		internal string GetTokenCacheKey()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}", this.authority, this.ApplicationId, this.ResourceId);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00031D60 File Offset: 0x0002FF60
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

		// Token: 0x06000ECB RID: 3787 RVA: 0x00031DC0 File Offset: 0x0002FFC0
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

		// Token: 0x06000ECC RID: 3788 RVA: 0x00031E6C File Offset: 0x0003006C
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

		// Token: 0x06000ECD RID: 3789 RVA: 0x00031F18 File Offset: 0x00030118
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

		// Token: 0x06000ECE RID: 3790 RVA: 0x0003200C File Offset: 0x0003020C
		private static AuthenticationInformation.AuthenticationInformationRecord[] DeserializeAuthenticationInformation(Stream info)
		{
			AuthenticationInformation.AuthenticationInformationRecord[] array;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(info, new XmlDictionaryReaderQuotas()))
			{
				array = (AuthenticationInformation.AuthenticationInformationRecord[])new DataContractSerializer(typeof(AuthenticationInformation.AuthenticationInformationRecord[]), "AuthenticationInformations", string.Empty).ReadObject(xmlDictionaryReader, true);
			}
			return array;
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00032068 File Offset: 0x00030268
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

		// Token: 0x04000863 RID: 2147
		private const string EmbeddedSecurityConfigResourceName = "ASAzureSecurityConfig.xml";

		// Token: 0x04000864 RID: 2148
		private const string RemoteSecurityConfigUrl = "https://global.asazure.windows.net/ASAzureSecurityConfig.xml";

		// Token: 0x04000865 RID: 2149
		private const string V1EndpointCommonTenant = "common";

		// Token: 0x04000866 RID: 2150
		private const string V2EndpointCommonTenant = "organizations";

		// Token: 0x04000867 RID: 2151
		private const string DefaultApplicationId = "cf710c6e-dfcc-4fa8-a093-d47294e44c66";

		// Token: 0x04000868 RID: 2152
		private const string DataverseIdentityProvider = "Dataverse";

		// Token: 0x04000869 RID: 2153
		private const string TokenCacheKeyTemplate = "{0}|{1}|{2}";

		// Token: 0x0400086A RID: 2154
		private static readonly string V1EndpointCommonTenantPathSegment = string.Format("/{0}", "common");

		// Token: 0x0400086B RID: 2155
		private static readonly string V2EndpointCommonTenantPathSegment = string.Format("/{0}", "organizations");

		// Token: 0x0400086C RID: 2156
		private static AuthenticationInformation.AuthenticationInformationRecord[] embeddedSecurityConfig;

		// Token: 0x0400086D RID: 2157
		private static AuthenticationInformation.AuthenticationInformationRecord[] remoteSecurityConfig;

		// Token: 0x0400086E RID: 2158
		private string authority;

		// Token: 0x020001D1 RID: 465
		[DataContract(Name = "AuthenticationInformation", Namespace = "")]
		private sealed class AuthenticationInformationRecord
		{
			// Token: 0x170006EA RID: 1770
			// (get) Token: 0x060013DF RID: 5087 RVA: 0x00044FEE File Offset: 0x000431EE
			// (set) Token: 0x060013E0 RID: 5088 RVA: 0x00044FF6 File Offset: 0x000431F6
			[DataMember(Name = "DomainPostfix", Order = 0)]
			public string DomainPostfix { get; private set; }

			// Token: 0x170006EB RID: 1771
			// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00044FFF File Offset: 0x000431FF
			// (set) Token: 0x060013E2 RID: 5090 RVA: 0x00045007 File Offset: 0x00043207
			[DataMember(Name = "Authority", Order = 1)]
			public string Authority { get; private set; }

			// Token: 0x170006EC RID: 1772
			// (get) Token: 0x060013E3 RID: 5091 RVA: 0x00045010 File Offset: 0x00043210
			// (set) Token: 0x060013E4 RID: 5092 RVA: 0x00045018 File Offset: 0x00043218
			[DataMember(Name = "Authority.v2", Order = 2, EmitDefaultValue = true)]
			public string Authority2 { get; private set; }

			// Token: 0x170006ED RID: 1773
			// (get) Token: 0x060013E5 RID: 5093 RVA: 0x00045021 File Offset: 0x00043221
			// (set) Token: 0x060013E6 RID: 5094 RVA: 0x00045029 File Offset: 0x00043229
			[DataMember(Name = "ApplicationId", Order = 3)]
			public string ApplicationId { get; private set; }

			// Token: 0x170006EE RID: 1774
			// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00045032 File Offset: 0x00043232
			// (set) Token: 0x060013E8 RID: 5096 RVA: 0x0004503A File Offset: 0x0004323A
			[DataMember(Name = "ResourceId", Order = 4, EmitDefaultValue = true)]
			public string ResourceId { get; private set; }

			// Token: 0x170006EF RID: 1775
			// (get) Token: 0x060013E9 RID: 5097 RVA: 0x00045043 File Offset: 0x00043243
			// (set) Token: 0x060013EA RID: 5098 RVA: 0x0004504B File Offset: 0x0004324B
			[DataMember(Name = "PowerBIEndpoint", Order = 5, EmitDefaultValue = true)]
			public string PowerBIEndpoint { get; private set; }
		}
	}
}
