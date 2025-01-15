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
		// Token: 0x06000EC2 RID: 3778 RVA: 0x00031DCA File Offset: 0x0002FFCA
		public AuthenticationInformation(AuthenticationEndpoint endpoint, string authority, string applicationId, string resourceId = null)
			: this()
		{
			this.Endpoint = endpoint;
			this.Authority = authority;
			this.ApplicationId = applicationId;
			this.ResourceId = resourceId;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00031DF0 File Offset: 0x0002FFF0
		public AuthenticationInformation(AuthenticationInformation info)
			: this()
		{
			this.Endpoint = info.Endpoint;
			this.authority = info.authority;
			this.ApplicationId = info.ApplicationId;
			this.ResourceId = info.ResourceId;
			this.IsCommonTenant = info.IsCommonTenant;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00031E40 File Offset: 0x00030040
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

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00031EA2 File Offset: 0x000300A2
		private AuthenticationInformation()
		{
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00031EAA File Offset: 0x000300AA
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x00031EB2 File Offset: 0x000300B2
		public AuthenticationEndpoint Endpoint { get; private set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00031EBB File Offset: 0x000300BB
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x00031EC4 File Offset: 0x000300C4
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

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00031F24 File Offset: 0x00030124
		// (set) Token: 0x06000ECB RID: 3787 RVA: 0x00031F2C File Offset: 0x0003012C
		public string ApplicationId { get; private set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00031F35 File Offset: 0x00030135
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x00031F3D File Offset: 0x0003013D
		public string ResourceId { get; internal set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00031F46 File Offset: 0x00030146
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x00031F4E File Offset: 0x0003014E
		public string PowerBIEndpoint { get; internal set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00031F57 File Offset: 0x00030157
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x00031F5F File Offset: 0x0003015F
		internal bool IsCommonTenant { get; private set; }

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00031F68 File Offset: 0x00030168
		public static void ValidateResourceForAuthentication(Uri resource)
		{
			AuthenticationInformation.AuthenticationInformationRecord authenticationInformationRecord;
			if (!AuthenticationInformation.TryFindAuthenticationInformation(resource, AuthenticationInformation.GetAllowedAuthenticationInformation(true), out authenticationInformationRecord) && !AuthenticationInformation.TryFindAuthenticationInformation(resource, AuthenticationInformation.GetAllowedAuthenticationInformation(false), out authenticationInformationRecord))
			{
				throw new ArgumentException(AuthenticationSR.Exception_UnknownAuthenticationInfo);
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00031FA0 File Offset: 0x000301A0
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

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00031FFC File Offset: 0x000301FC
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

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003205C File Offset: 0x0003025C
		internal static bool IsDataverseIdentityProvider(string identityProvider)
		{
			return string.Compare(identityProvider, "Dataverse", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003206D File Offset: 0x0003026D
		internal string GetTokenCacheKey()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}", this.authority, this.ApplicationId, this.ResourceId);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00032090 File Offset: 0x00030290
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

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000320F0 File Offset: 0x000302F0
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

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003219C File Offset: 0x0003039C
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

		// Token: 0x06000EDA RID: 3802 RVA: 0x00032248 File Offset: 0x00030448
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

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003233C File Offset: 0x0003053C
		private static AuthenticationInformation.AuthenticationInformationRecord[] DeserializeAuthenticationInformation(Stream info)
		{
			AuthenticationInformation.AuthenticationInformationRecord[] array;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(info, new XmlDictionaryReaderQuotas()))
			{
				array = (AuthenticationInformation.AuthenticationInformationRecord[])new DataContractSerializer(typeof(AuthenticationInformation.AuthenticationInformationRecord[]), "AuthenticationInformations", string.Empty).ReadObject(xmlDictionaryReader, true);
			}
			return array;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00032398 File Offset: 0x00030598
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

		// Token: 0x04000870 RID: 2160
		private const string EmbeddedSecurityConfigResourceName = "ASAzureSecurityConfig.xml";

		// Token: 0x04000871 RID: 2161
		private const string RemoteSecurityConfigUrl = "https://global.asazure.windows.net/ASAzureSecurityConfig.xml";

		// Token: 0x04000872 RID: 2162
		private const string V1EndpointCommonTenant = "common";

		// Token: 0x04000873 RID: 2163
		private const string V2EndpointCommonTenant = "organizations";

		// Token: 0x04000874 RID: 2164
		private const string DefaultApplicationId = "cf710c6e-dfcc-4fa8-a093-d47294e44c66";

		// Token: 0x04000875 RID: 2165
		private const string DataverseIdentityProvider = "Dataverse";

		// Token: 0x04000876 RID: 2166
		private const string TokenCacheKeyTemplate = "{0}|{1}|{2}";

		// Token: 0x04000877 RID: 2167
		private static readonly string V1EndpointCommonTenantPathSegment = string.Format("/{0}", "common");

		// Token: 0x04000878 RID: 2168
		private static readonly string V2EndpointCommonTenantPathSegment = string.Format("/{0}", "organizations");

		// Token: 0x04000879 RID: 2169
		private static AuthenticationInformation.AuthenticationInformationRecord[] embeddedSecurityConfig;

		// Token: 0x0400087A RID: 2170
		private static AuthenticationInformation.AuthenticationInformationRecord[] remoteSecurityConfig;

		// Token: 0x0400087B RID: 2171
		private string authority;

		// Token: 0x020001D1 RID: 465
		[DataContract(Name = "AuthenticationInformation", Namespace = "")]
		private sealed class AuthenticationInformationRecord
		{
			// Token: 0x170006F0 RID: 1776
			// (get) Token: 0x060013EC RID: 5100 RVA: 0x0004552A File Offset: 0x0004372A
			// (set) Token: 0x060013ED RID: 5101 RVA: 0x00045532 File Offset: 0x00043732
			[DataMember(Name = "DomainPostfix", Order = 0)]
			public string DomainPostfix { get; private set; }

			// Token: 0x170006F1 RID: 1777
			// (get) Token: 0x060013EE RID: 5102 RVA: 0x0004553B File Offset: 0x0004373B
			// (set) Token: 0x060013EF RID: 5103 RVA: 0x00045543 File Offset: 0x00043743
			[DataMember(Name = "Authority", Order = 1)]
			public string Authority { get; private set; }

			// Token: 0x170006F2 RID: 1778
			// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0004554C File Offset: 0x0004374C
			// (set) Token: 0x060013F1 RID: 5105 RVA: 0x00045554 File Offset: 0x00043754
			[DataMember(Name = "Authority.v2", Order = 2, EmitDefaultValue = true)]
			public string Authority2 { get; private set; }

			// Token: 0x170006F3 RID: 1779
			// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0004555D File Offset: 0x0004375D
			// (set) Token: 0x060013F3 RID: 5107 RVA: 0x00045565 File Offset: 0x00043765
			[DataMember(Name = "ApplicationId", Order = 3)]
			public string ApplicationId { get; private set; }

			// Token: 0x170006F4 RID: 1780
			// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0004556E File Offset: 0x0004376E
			// (set) Token: 0x060013F5 RID: 5109 RVA: 0x00045576 File Offset: 0x00043776
			[DataMember(Name = "ResourceId", Order = 4, EmitDefaultValue = true)]
			public string ResourceId { get; private set; }

			// Token: 0x170006F5 RID: 1781
			// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0004557F File Offset: 0x0004377F
			// (set) Token: 0x060013F7 RID: 5111 RVA: 0x00045587 File Offset: 0x00043787
			[DataMember(Name = "PowerBIEndpoint", Order = 5, EmitDefaultValue = true)]
			public string PowerBIEndpoint { get; private set; }
		}
	}
}
