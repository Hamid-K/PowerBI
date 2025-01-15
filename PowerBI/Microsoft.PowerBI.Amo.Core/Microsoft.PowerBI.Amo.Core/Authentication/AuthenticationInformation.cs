using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000F3 RID: 243
	internal sealed class AuthenticationInformation
	{
		// Token: 0x06000F52 RID: 3922 RVA: 0x0003474C File Offset: 0x0003294C
		public AuthenticationInformation(AuthenticationEndpoint endpoint, string authority, string applicationId, string resourceId = null)
			: this()
		{
			this.Endpoint = endpoint;
			this.Authority = authority;
			this.ApplicationId = applicationId;
			this.ResourceId = resourceId;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00034774 File Offset: 0x00032974
		public AuthenticationInformation(AuthenticationInformation info)
			: this()
		{
			this.Endpoint = info.Endpoint;
			this.authority = info.authority;
			this.ApplicationId = info.ApplicationId;
			this.ResourceId = info.ResourceId;
			this.IsCommonTenant = info.IsCommonTenant;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x000347C4 File Offset: 0x000329C4
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

		// Token: 0x06000F55 RID: 3925 RVA: 0x00034826 File Offset: 0x00032A26
		private AuthenticationInformation()
		{
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0003482E File Offset: 0x00032A2E
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x00034836 File Offset: 0x00032A36
		public AuthenticationEndpoint Endpoint { get; private set; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0003483F File Offset: 0x00032A3F
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x00034848 File Offset: 0x00032A48
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

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x000348A8 File Offset: 0x00032AA8
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x000348B0 File Offset: 0x00032AB0
		public string ApplicationId { get; private set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x000348B9 File Offset: 0x00032AB9
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x000348C1 File Offset: 0x00032AC1
		public string ResourceId { get; internal set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x000348CA File Offset: 0x00032ACA
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x000348D2 File Offset: 0x00032AD2
		public string PowerBIEndpoint { get; internal set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x000348DB File Offset: 0x00032ADB
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x000348E3 File Offset: 0x00032AE3
		internal bool IsCommonTenant { get; private set; }

		// Token: 0x06000F62 RID: 3938 RVA: 0x000348EC File Offset: 0x00032AEC
		public static void ValidateResourceForAuthentication(Uri resource)
		{
			AuthenticationInformation.AuthenticationInformationRecord authenticationInformationRecord;
			if (!AuthenticationInformation.TryFindAuthenticationInformation(resource, AuthenticationInformation.GetAllowedAuthenticationInformation(true), out authenticationInformationRecord) && !AuthenticationInformation.TryFindAuthenticationInformation(resource, AuthenticationInformation.GetAllowedAuthenticationInformation(false), out authenticationInformationRecord))
			{
				throw new ArgumentException(AuthenticationSR.Exception_UnknownAuthenticationInfo);
			}
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00034924 File Offset: 0x00032B24
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

		// Token: 0x06000F64 RID: 3940 RVA: 0x00034980 File Offset: 0x00032B80
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

		// Token: 0x06000F65 RID: 3941 RVA: 0x000349E0 File Offset: 0x00032BE0
		internal static bool IsDataverseIdentityProvider(string identityProvider)
		{
			return string.Compare(identityProvider, "Dataverse", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x000349F1 File Offset: 0x00032BF1
		internal string GetTokenCacheKey()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}", this.authority, this.ApplicationId, this.ResourceId);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00034A14 File Offset: 0x00032C14
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

		// Token: 0x06000F68 RID: 3944 RVA: 0x00034A74 File Offset: 0x00032C74
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

		// Token: 0x06000F69 RID: 3945 RVA: 0x00034B20 File Offset: 0x00032D20
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

		// Token: 0x06000F6A RID: 3946 RVA: 0x00034BCC File Offset: 0x00032DCC
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

		// Token: 0x06000F6B RID: 3947 RVA: 0x00034CC0 File Offset: 0x00032EC0
		private static AuthenticationInformation.AuthenticationInformationRecord[] DeserializeAuthenticationInformation(Stream info)
		{
			AuthenticationInformation.AuthenticationInformationRecord[] array;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(info, new XmlDictionaryReaderQuotas()))
			{
				array = (AuthenticationInformation.AuthenticationInformationRecord[])new DataContractSerializer(typeof(AuthenticationInformation.AuthenticationInformationRecord[]), "AuthenticationInformations", string.Empty).ReadObject(xmlDictionaryReader, true);
			}
			return array;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00034D1C File Offset: 0x00032F1C
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

		// Token: 0x0400082A RID: 2090
		private const string EmbeddedSecurityConfigResourceName = "ASAzureSecurityConfig.xml";

		// Token: 0x0400082B RID: 2091
		private const string RemoteSecurityConfigUrl = "https://global.asazure.windows.net/ASAzureSecurityConfig.xml";

		// Token: 0x0400082C RID: 2092
		private const string V1EndpointCommonTenant = "common";

		// Token: 0x0400082D RID: 2093
		private const string V2EndpointCommonTenant = "organizations";

		// Token: 0x0400082E RID: 2094
		private const string DefaultApplicationId = "cf710c6e-dfcc-4fa8-a093-d47294e44c66";

		// Token: 0x0400082F RID: 2095
		private const string DataverseIdentityProvider = "Dataverse";

		// Token: 0x04000830 RID: 2096
		private const string TokenCacheKeyTemplate = "{0}|{1}|{2}";

		// Token: 0x04000831 RID: 2097
		private static readonly string V1EndpointCommonTenantPathSegment = string.Format("/{0}", "common");

		// Token: 0x04000832 RID: 2098
		private static readonly string V2EndpointCommonTenantPathSegment = string.Format("/{0}", "organizations");

		// Token: 0x04000833 RID: 2099
		private static AuthenticationInformation.AuthenticationInformationRecord[] embeddedSecurityConfig;

		// Token: 0x04000834 RID: 2100
		private static AuthenticationInformation.AuthenticationInformationRecord[] remoteSecurityConfig;

		// Token: 0x04000835 RID: 2101
		private string authority;

		// Token: 0x020001AE RID: 430
		[DataContract(Name = "AuthenticationInformation", Namespace = "")]
		private sealed class AuthenticationInformationRecord
		{
			// Token: 0x17000635 RID: 1589
			// (get) Token: 0x06001348 RID: 4936 RVA: 0x00043776 File Offset: 0x00041976
			// (set) Token: 0x06001349 RID: 4937 RVA: 0x0004377E File Offset: 0x0004197E
			[DataMember(Name = "DomainPostfix", Order = 0)]
			public string DomainPostfix { get; private set; }

			// Token: 0x17000636 RID: 1590
			// (get) Token: 0x0600134A RID: 4938 RVA: 0x00043787 File Offset: 0x00041987
			// (set) Token: 0x0600134B RID: 4939 RVA: 0x0004378F File Offset: 0x0004198F
			[DataMember(Name = "Authority", Order = 1)]
			public string Authority { get; private set; }

			// Token: 0x17000637 RID: 1591
			// (get) Token: 0x0600134C RID: 4940 RVA: 0x00043798 File Offset: 0x00041998
			// (set) Token: 0x0600134D RID: 4941 RVA: 0x000437A0 File Offset: 0x000419A0
			[DataMember(Name = "Authority.v2", Order = 2, EmitDefaultValue = true)]
			public string Authority2 { get; private set; }

			// Token: 0x17000638 RID: 1592
			// (get) Token: 0x0600134E RID: 4942 RVA: 0x000437A9 File Offset: 0x000419A9
			// (set) Token: 0x0600134F RID: 4943 RVA: 0x000437B1 File Offset: 0x000419B1
			[DataMember(Name = "ApplicationId", Order = 3)]
			public string ApplicationId { get; private set; }

			// Token: 0x17000639 RID: 1593
			// (get) Token: 0x06001350 RID: 4944 RVA: 0x000437BA File Offset: 0x000419BA
			// (set) Token: 0x06001351 RID: 4945 RVA: 0x000437C2 File Offset: 0x000419C2
			[DataMember(Name = "ResourceId", Order = 4, EmitDefaultValue = true)]
			public string ResourceId { get; private set; }

			// Token: 0x1700063A RID: 1594
			// (get) Token: 0x06001352 RID: 4946 RVA: 0x000437CB File Offset: 0x000419CB
			// (set) Token: 0x06001353 RID: 4947 RVA: 0x000437D3 File Offset: 0x000419D3
			[DataMember(Name = "PowerBIEndpoint", Order = 5, EmitDefaultValue = true)]
			public string PowerBIEndpoint { get; private set; }
		}
	}
}
