using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Instance.Validation;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000131 RID: 305
	internal class AuthorityInfo
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x0003932A File Offset: 0x0003752A
		public AuthorityInfo(AuthorityType authorityType, string authority, bool validateAuthority)
			: this(authorityType, AuthorityInfo.ValidateAndCreateAuthorityUri(authority, new AuthorityType?(authorityType)), validateAuthority)
		{
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00039340 File Offset: 0x00037540
		public AuthorityInfo(AuthorityType authorityType, Uri authorityUri, bool validateAuthority)
		{
			this.AuthorityType = authorityType;
			this.ValidateAuthority = validateAuthority;
			switch (this.AuthorityType)
			{
			case AuthorityType.B2C:
			{
				string[] array = AuthorityInfo.GetPathSegments(authorityUri.AbsolutePath);
				if (array.Length < 3)
				{
					throw new ArgumentException("The B2C authority URI should have at least 3 segments in the path (i.e. https://<host>/tfp/<tenant>/<policy>/...). ");
				}
				this.CanonicalAuthority = new Uri(string.Concat(new string[]
				{
					"https://",
					authorityUri.Authority,
					"/",
					array[0],
					"/",
					array[1],
					"/",
					array[2],
					"/"
				}));
				return;
			}
			case AuthorityType.Dsts:
			{
				string[] array = AuthorityInfo.GetPathSegments(authorityUri.AbsolutePath);
				if (array.Length < 2)
				{
					throw new ArgumentException("The DSTS authority URI should have at least 2 segments in the path (i.e. https://<host>/dstsv2/<tenant>/...). ");
				}
				this.CanonicalAuthority = new Uri(string.Concat(new string[]
				{
					"https://",
					authorityUri.Authority,
					"/",
					array[0],
					"/",
					array[1],
					"/"
				}));
				this.UserRealmUriPrefix = UriBuilderExtensions.GetHttpsUriWithOptionalPort(string.Concat(new string[]
				{
					"https://",
					authorityUri.Authority,
					"/",
					array[0],
					"/common/userrealm/"
				}), authorityUri.Port);
				return;
			}
			case AuthorityType.Generic:
				this.CanonicalAuthority = authorityUri;
				return;
			default:
				this.CanonicalAuthority = new Uri(UriBuilderExtensions.GetHttpsUriWithOptionalPort(string.Concat(new string[]
				{
					"https://",
					authorityUri.Authority,
					"/",
					AuthorityInfo.GetFirstPathSegment(authorityUri),
					"/"
				}), authorityUri.Port));
				this.UserRealmUriPrefix = UriBuilderExtensions.GetHttpsUriWithOptionalPort("https://" + this.Host + "/common/userrealm/", authorityUri.Port);
				return;
			}
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003951E File Offset: 0x0003771E
		public AuthorityInfo(AuthorityInfo other)
			: this(other.CanonicalAuthority, other.AuthorityType, other.UserRealmUriPrefix, other.ValidateAuthority)
		{
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003953E File Offset: 0x0003773E
		private AuthorityInfo(Uri canonicalAuthority, AuthorityType authorityType, string userRealmUriPrefix, bool validateAuthority)
		{
			this.CanonicalAuthority = canonicalAuthority;
			this.AuthorityType = authorityType;
			this.UserRealmUriPrefix = userRealmUriPrefix;
			this.ValidateAuthority = validateAuthority;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x00039563 File Offset: 0x00037763
		public string Host
		{
			get
			{
				return this.CanonicalAuthority.Host;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00039570 File Offset: 0x00037770
		public Uri CanonicalAuthority { get; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x00039578 File Offset: 0x00037778
		internal AuthorityType AuthorityType { get; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00039580 File Offset: 0x00037780
		public string UserRealmUriPrefix { get; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x00039588 File Offset: 0x00037788
		public bool ValidateAuthority { get; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00039590 File Offset: 0x00037790
		internal bool IsInstanceDiscoverySupported
		{
			get
			{
				return this.AuthorityType == AuthorityType.Aad;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0003959B File Offset: 0x0003779B
		internal bool IsWsTrustFlowSupported
		{
			get
			{
				return this.AuthorityType == AuthorityType.Aad || this.AuthorityType == AuthorityType.Dsts;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x000395B0 File Offset: 0x000377B0
		internal bool CanBeTenanted
		{
			get
			{
				return this.AuthorityType == AuthorityType.Aad || this.AuthorityType == AuthorityType.Dsts || this.AuthorityType == AuthorityType.B2C || this.AuthorityType == AuthorityType.Ciam;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x000395D7 File Offset: 0x000377D7
		internal bool IsClientInfoSupported
		{
			get
			{
				return this.AuthorityType == AuthorityType.Aad || this.AuthorityType == AuthorityType.Dsts || this.AuthorityType == AuthorityType.B2C || this.AuthorityType == AuthorityType.Ciam;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x000395FE File Offset: 0x000377FE
		internal bool IsSha2CredentialSupported
		{
			get
			{
				return this.AuthorityType != AuthorityType.Dsts && this.AuthorityType != AuthorityType.Generic && this.AuthorityType != AuthorityType.Adfs;
			}
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00039620 File Offset: 0x00037820
		internal static AuthorityInfo FromAuthorityUri(string authorityUri, bool validateAuthority)
		{
			Uri uri = AuthorityInfo.ValidateAndCreateAuthorityUri(AuthorityInfo.CanonicalizeAuthorityUri(authorityUri), null);
			uri = AuthorityInfo.TransformIfCiamAuthority(uri);
			AuthorityType authorityType = AuthorityInfo.GetAuthorityType(uri);
			if (authorityType == AuthorityType.B2C || authorityType == AuthorityType.Generic)
			{
				validateAuthority = false;
			}
			return new AuthorityInfo(authorityType, uri, validateAuthority);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00039663 File Offset: 0x00037863
		private static Uri TransformIfCiamAuthority(Uri authorityUri)
		{
			if (AuthorityInfo.IsCiamAuthority(authorityUri))
			{
				return CiamAuthority.TransformAuthority(authorityUri);
			}
			return authorityUri;
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00039675 File Offset: 0x00037875
		internal static AuthorityInfo FromAadAuthority(string cloudInstanceUri, Guid tenantId, bool validateAuthority)
		{
			return AuthorityInfo.FromAuthorityUri(cloudInstanceUri.Trim().TrimEnd(new char[] { '/' }) + "/" + tenantId.ToString("D"), validateAuthority);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x000396AC File Offset: 0x000378AC
		internal static AuthorityInfo FromAadAuthority(string cloudInstanceUri, string tenant, bool validateAuthority)
		{
			Guid guid;
			if (Guid.TryParse(tenant, out guid))
			{
				return AuthorityInfo.FromAadAuthority(cloudInstanceUri, guid, validateAuthority);
			}
			return AuthorityInfo.FromAuthorityUri(cloudInstanceUri.Trim().TrimEnd(new char[] { '/' }) + "/" + tenant, validateAuthority);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x000396F4 File Offset: 0x000378F4
		internal static AuthorityInfo FromAadAuthority(AzureCloudInstance azureCloudInstance, Guid tenantId, bool validateAuthority)
		{
			string authorityUri = AuthorityInfo.GetAuthorityUri(azureCloudInstance, AadAuthorityAudience.AzureAdMyOrg, tenantId.ToString("D"));
			return new AuthorityInfo(AuthorityType.Aad, authorityUri, validateAuthority);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00039720 File Offset: 0x00037920
		internal static AuthorityInfo FromAadAuthority(AzureCloudInstance azureCloudInstance, string tenant, bool validateAuthority)
		{
			Guid guid;
			if (Guid.TryParse(tenant, out guid))
			{
				return AuthorityInfo.FromAadAuthority(azureCloudInstance, guid, validateAuthority);
			}
			string authorityUri = AuthorityInfo.GetAuthorityUri(azureCloudInstance, AadAuthorityAudience.AzureAdMyOrg, tenant);
			return new AuthorityInfo(AuthorityType.Aad, authorityUri, validateAuthority);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00039754 File Offset: 0x00037954
		internal static AuthorityInfo FromAadAuthority(AzureCloudInstance azureCloudInstance, AadAuthorityAudience authorityAudience, bool validateAuthority)
		{
			string authorityUri = AuthorityInfo.GetAuthorityUri(azureCloudInstance, authorityAudience, null);
			return new AuthorityInfo(AuthorityType.Aad, authorityUri, validateAuthority);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00039774 File Offset: 0x00037974
		internal static AuthorityInfo FromAadAuthority(AadAuthorityAudience authorityAudience, bool validateAuthority)
		{
			string authorityUri = AuthorityInfo.GetAuthorityUri(AzureCloudInstance.AzurePublic, authorityAudience, null);
			return new AuthorityInfo(AuthorityType.Aad, authorityUri, validateAuthority);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00039792 File Offset: 0x00037992
		internal static AuthorityInfo FromAdfsAuthority(string authorityUri, bool validateAuthority)
		{
			return new AuthorityInfo(AuthorityType.Adfs, authorityUri, validateAuthority);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0003979C File Offset: 0x0003799C
		internal static AuthorityInfo FromB2CAuthority(string authorityUri)
		{
			return new AuthorityInfo(AuthorityType.B2C, authorityUri, false);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x000397A6 File Offset: 0x000379A6
		internal static AuthorityInfo FromGenericAuthority(string authorityUri)
		{
			return new AuthorityInfo(AuthorityType.Generic, authorityUri, false);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000397B0 File Offset: 0x000379B0
		internal static string GetCloudUrl(AzureCloudInstance azureCloudInstance)
		{
			switch (azureCloudInstance)
			{
			case AzureCloudInstance.AzurePublic:
				return "https://login.microsoftonline.com";
			case AzureCloudInstance.AzureChina:
				return "https://login.chinacloudapi.cn";
			case AzureCloudInstance.AzureGermany:
				return "https://login.microsoftonline.de";
			case AzureCloudInstance.AzureUsGovernment:
				return "https://login.microsoftonline.us";
			default:
				throw new ArgumentException("azureCloudInstance");
			}
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x000397F0 File Offset: 0x000379F0
		internal static string GetAadAuthorityAudienceValue(AadAuthorityAudience authorityAudience, string tenantId)
		{
			switch (authorityAudience)
			{
			case AadAuthorityAudience.AzureAdMyOrg:
				if (string.IsNullOrWhiteSpace(tenantId))
				{
					throw new InvalidOperationException("When specifying AadAuthorityAudience.AzureAdMyOrg, you must also specify a tenant domain or tenant GUID. ");
				}
				return tenantId;
			case AadAuthorityAudience.AzureAdAndPersonalMicrosoftAccount:
				return "common";
			case AadAuthorityAudience.AzureAdMultipleOrgs:
				return "organizations";
			case AadAuthorityAudience.PersonalMicrosoftAccount:
				return "consumers";
			default:
				throw new ArgumentException("authorityAudience");
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00039848 File Offset: 0x00037A48
		internal static string CanonicalizeAuthorityUri(string uri)
		{
			if (!string.IsNullOrWhiteSpace(uri) && !uri.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				uri += "/";
			}
			return ((uri != null) ? uri.ToLowerInvariant() : null) ?? string.Empty;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00039882 File Offset: 0x00037A82
		internal bool IsDefaultAuthority
		{
			get
			{
				return string.Equals(this.CanonicalAuthority.ToString(), "https://login.microsoftonline.com/common/", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0003989C File Offset: 0x00037A9C
		internal Authority CreateAuthority()
		{
			switch (this.AuthorityType)
			{
			case AuthorityType.Aad:
				return new AadAuthority(this);
			case AuthorityType.Adfs:
				return new AdfsAuthority(this);
			case AuthorityType.B2C:
				return new B2CAuthority(this);
			case AuthorityType.Dsts:
				return new DstsAuthority(this);
			case AuthorityType.Generic:
				return new GenericAuthority(this);
			case AuthorityType.Ciam:
				return new CiamAuthority(this);
			default:
				throw new MsalClientException("invalid_authority_type", string.Format("Unsupported authority type {0}", this.AuthorityType));
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0003991C File Offset: 0x00037B1C
		private static Uri ValidateAndCreateAuthorityUri(string authority, AuthorityType? authorityType = null)
		{
			if (string.IsNullOrWhiteSpace(authority))
			{
				throw new ArgumentNullException("authority");
			}
			if (!Uri.IsWellFormedUriString(authority, UriKind.Absolute))
			{
				throw new ArgumentException("The authority (including the tenant ID) must be in a well-formed URI format. ", "authority");
			}
			Uri uri = new Uri(authority);
			if (uri.Scheme != "https")
			{
				throw new ArgumentException("The authority must use HTTPS scheme. ", "authority");
			}
			if (!(authorityType == AuthorityType.Generic))
			{
				string text = uri.AbsolutePath.Substring(1);
				if (string.IsNullOrWhiteSpace(text) && !AuthorityInfo.IsCiamAuthority(uri))
				{
					throw new ArgumentException("The authority URI should have at least one segment in the path (i.e. https://<host>/<path>/...). ", "authority");
				}
				string[] array = text.Split(new char[] { '/' });
				if (array == null || array.Length == 0)
				{
					throw new ArgumentException("The authority URI should have at least one segment in the path (i.e. https://<host>/<path>/...). ");
				}
			}
			return uri;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000399E4 File Offset: 0x00037BE4
		private static string GetAuthorityUri(AzureCloudInstance azureCloudInstance, AadAuthorityAudience authorityAudience, string tenantId = null)
		{
			string cloudUrl = AuthorityInfo.GetCloudUrl(azureCloudInstance);
			string aadAuthorityAudienceValue = AuthorityInfo.GetAadAuthorityAudienceValue(authorityAudience, tenantId);
			return cloudUrl + "/" + aadAuthorityAudienceValue;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00039A0A File Offset: 0x00037C0A
		internal static string GetFirstPathSegment(Uri authority)
		{
			if (authority.Segments.Length >= 2)
			{
				return authority.Segments[1].TrimEnd(new char[] { '/' });
			}
			throw new InvalidOperationException("Authority should be in the form <host>/<audience>, for example https://login.microsoftonline.com/common. ");
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00039A3A File Offset: 0x00037C3A
		internal static string GetSecondPathSegment(Uri authority)
		{
			if (authority.Segments.Length >= 3)
			{
				return authority.Segments[2].TrimEnd(new char[] { '/' });
			}
			throw new InvalidOperationException("Authority should be in the form <host>/<audience>/<tenantID>, for example https://login.microsoftonline.com/dsts/<tenantid>. ");
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00039A6C File Offset: 0x00037C6C
		private static AuthorityType GetAuthorityType(Uri authorityUri)
		{
			if (AuthorityInfo.IsCiamAuthority(authorityUri))
			{
				return AuthorityType.Ciam;
			}
			string firstPathSegment = AuthorityInfo.GetFirstPathSegment(authorityUri);
			if (string.Equals(firstPathSegment, "adfs", StringComparison.OrdinalIgnoreCase))
			{
				return AuthorityType.Adfs;
			}
			if (string.Equals(firstPathSegment, "dstsv2", StringComparison.OrdinalIgnoreCase))
			{
				return AuthorityType.Dsts;
			}
			if (string.Equals(firstPathSegment, "tfp", StringComparison.OrdinalIgnoreCase))
			{
				return AuthorityType.B2C;
			}
			return AuthorityType.Aad;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00039ABB File Offset: 0x00037CBB
		private static bool IsCiamAuthority(Uri authorityUri)
		{
			return authorityUri.Host.EndsWith(".ciamlogin.com");
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00039ACD File Offset: 0x00037CCD
		private static string[] GetPathSegments(string absolutePath)
		{
			return absolutePath.Substring(1).Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x020003C0 RID: 960
		internal class AuthorityInfoHelper
		{
			// Token: 0x06001DDA RID: 7642 RVA: 0x00068754 File Offset: 0x00066954
			public static IAuthorityValidator CreateAuthorityValidator(AuthorityInfo authorityInfo, RequestContext requestContext)
			{
				switch (authorityInfo.AuthorityType)
				{
				case AuthorityType.Aad:
					return new AadAuthorityValidator(requestContext);
				case AuthorityType.Adfs:
					return new AdfsAuthorityValidator(requestContext);
				case AuthorityType.B2C:
				case AuthorityType.Dsts:
				case AuthorityType.Generic:
				case AuthorityType.Ciam:
					return new NullAuthorityValidator();
				default:
					throw new InvalidOperationException("Invalid AuthorityType");
				}
			}

			// Token: 0x06001DDB RID: 7643 RVA: 0x000687A8 File Offset: 0x000669A8
			public static async Task<Authority> CreateAuthorityForRequestAsync(RequestContext requestContext, AuthorityInfo requestAuthorityInfo, IAccount account = null)
			{
				Authority configAuthority = requestContext.ServiceBundle.Config.Authority;
				AuthorityInfo configAuthorityInfo = configAuthority.AuthorityInfo;
				if (configAuthorityInfo == null)
				{
					throw new ArgumentNullException("AuthorityInfo");
				}
				AuthorityInfo.AuthorityInfoHelper.ValidateTypeMismatch(configAuthorityInfo, requestAuthorityInfo);
				await AuthorityInfo.AuthorityInfoHelper.ValidateSameHostAsync(requestAuthorityInfo, requestContext).ConfigureAwait(false);
				AuthorityInfo authorityInfo = requestAuthorityInfo ?? configAuthorityInfo;
				Authority authority4;
				switch (configAuthorityInfo.AuthorityType)
				{
				case AuthorityType.Aad:
				{
					bool flag = requestContext.ServiceBundle.Config.MultiCloudSupportEnabled && account != null && !PublicClientApplication.IsOperatingSystemAccount(account);
					if (requestAuthorityInfo == null)
					{
						Authority authority2;
						if (!flag)
						{
							Authority authority = configAuthority;
							string text;
							if (account == null)
							{
								text = null;
							}
							else
							{
								AccountId homeAccountId = account.HomeAccountId;
								text = ((homeAccountId != null) ? homeAccountId.TenantId : null);
							}
							authority2 = AuthorityInfo.AuthorityInfoHelper.CreateAuthorityWithTenant(authority, text, false);
						}
						else
						{
							Authority authority3 = AuthorityInfo.AuthorityInfoHelper.CreateAuthorityWithEnvironment(configAuthorityInfo, account.Environment);
							string text2;
							if (account == null)
							{
								text2 = null;
							}
							else
							{
								AccountId homeAccountId2 = account.HomeAccountId;
								text2 = ((homeAccountId2 != null) ? homeAccountId2.TenantId : null);
							}
							authority2 = AuthorityInfo.AuthorityInfoHelper.CreateAuthorityWithTenant(authority3, text2, false);
						}
						authority4 = authority2;
					}
					else if (configAuthorityInfo.IsDefaultAuthority && requestAuthorityInfo.AuthorityType != AuthorityType.Aad)
					{
						authority4 = requestAuthorityInfo.CreateAuthority();
					}
					else
					{
						AadAuthority aadAuthority = (flag ? new AadAuthority(AuthorityInfo.AuthorityInfoHelper.CreateAuthorityWithEnvironment(requestAuthorityInfo, (account != null) ? account.Environment : null).AuthorityInfo) : new AadAuthority(requestAuthorityInfo));
						if (aadAuthority.IsCommonOrganizationsOrConsumersTenant())
						{
							bool flag2 = requestContext.ServiceBundle.Config.IsBrokerEnabled && requestContext.ServiceBundle.Config.BrokerOptions != null && requestContext.ServiceBundle.Config.BrokerOptions.MsaPassthrough;
							string text3;
							if (account == null)
							{
								text3 = null;
							}
							else
							{
								AccountId homeAccountId3 = account.HomeAccountId;
								text3 = ((homeAccountId3 != null) ? homeAccountId3.TenantId : null);
							}
							if (!aadAuthority.IsOrganizationsTenantWithMsaPassthroughEnabled(flag2, text3))
							{
								Authority authority6;
								if (!flag)
								{
									Authority authority5 = configAuthority;
									string text4;
									if (account == null)
									{
										text4 = null;
									}
									else
									{
										AccountId homeAccountId4 = account.HomeAccountId;
										text4 = ((homeAccountId4 != null) ? homeAccountId4.TenantId : null);
									}
									authority6 = AuthorityInfo.AuthorityInfoHelper.CreateAuthorityWithTenant(authority5, text4, false);
								}
								else
								{
									Authority authority7 = AuthorityInfo.AuthorityInfoHelper.CreateAuthorityWithEnvironment(configAuthorityInfo, account.Environment);
									string text5;
									if (account == null)
									{
										text5 = null;
									}
									else
									{
										AccountId homeAccountId5 = account.HomeAccountId;
										text5 = ((homeAccountId5 != null) ? homeAccountId5.TenantId : null);
									}
									authority6 = AuthorityInfo.AuthorityInfoHelper.CreateAuthorityWithTenant(authority7, text5, false);
								}
								authority4 = authority6;
								break;
							}
						}
						authority4 = aadAuthority;
					}
					break;
				}
				case AuthorityType.Adfs:
					authority4 = new AdfsAuthority(authorityInfo);
					break;
				case AuthorityType.B2C:
					authority4 = new B2CAuthority(authorityInfo);
					break;
				case AuthorityType.Dsts:
					authority4 = new DstsAuthority(authorityInfo);
					break;
				case AuthorityType.Generic:
					authority4 = new GenericAuthority(authorityInfo);
					break;
				case AuthorityType.Ciam:
					authority4 = new CiamAuthority(authorityInfo);
					break;
				default:
					throw new MsalClientException("invalid_authority_type", "Unsupported authority type");
				}
				return authority4;
			}

			// Token: 0x06001DDC RID: 7644 RVA: 0x000687FB File Offset: 0x000669FB
			internal static Authority CreateAuthorityWithTenant(Authority authority, string tenantId, bool forceSpecifiedTenant)
			{
				return Authority.CreateAuthority(authority.GetTenantedAuthority(tenantId, forceSpecifiedTenant), authority.AuthorityInfo.ValidateAuthority);
			}

			// Token: 0x06001DDD RID: 7645 RVA: 0x00068815 File Offset: 0x00066A15
			internal static Authority CreateAuthorityWithEnvironment(AuthorityInfo authorityInfo, string environment)
			{
				return Authority.CreateAuthority(new UriBuilder(authorityInfo.CanonicalAuthority)
				{
					Host = environment
				}.Uri.AbsoluteUri, authorityInfo.ValidateAuthority);
			}

			// Token: 0x06001DDE RID: 7646 RVA: 0x0006883E File Offset: 0x00066A3E
			private static void ValidateTypeMismatch(AuthorityInfo configAuthorityInfo, AuthorityInfo requestAuthorityInfo)
			{
				if (!configAuthorityInfo.IsDefaultAuthority && requestAuthorityInfo != null && configAuthorityInfo.AuthorityType != requestAuthorityInfo.AuthorityType)
				{
					throw new MsalClientException("authority_type_mismatch", MsalErrorMessage.AuthorityTypeMismatch(configAuthorityInfo.AuthorityType, requestAuthorityInfo.AuthorityType));
				}
			}

			// Token: 0x06001DDF RID: 7647 RVA: 0x00068878 File Offset: 0x00066A78
			private static async Task ValidateSameHostAsync(AuthorityInfo requestAuthorityInfo, RequestContext requestContext)
			{
				AuthorityInfo configAuthorityInfo = requestContext.ServiceBundle.Config.Authority.AuthorityInfo;
				if (!requestContext.ServiceBundle.Config.MultiCloudSupportEnabled && requestAuthorityInfo != null && !string.Equals(requestAuthorityInfo.Host, configAuthorityInfo.Host, StringComparison.OrdinalIgnoreCase))
				{
					if (requestAuthorityInfo.AuthorityType == AuthorityType.B2C)
					{
						throw new MsalClientException("B2C_authority_host_mismatch", "The B2C authority host that was used when creating the client application is not the same authority host used in the AcquireToken call. See https://aka.ms/msal-net-b2c for details. ");
					}
					if (requestAuthorityInfo.AuthorityType != AuthorityType.Ciam && requestAuthorityInfo.AuthorityType != AuthorityType.Generic)
					{
						if (!string.IsNullOrEmpty(requestContext.ServiceBundle.Config.AzureRegion))
						{
							throw new MsalClientException("authority_override_regional", "You configured WithAuthority at the request level, and also WithAzureRegion. This is not supported when the environment changes from application to request. Use WithTenantId at the request level instead.");
						}
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = AuthorityInfo.AuthorityInfoHelper.IsAuthorityAliasedAsync(requestContext, requestAuthorityInfo).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						}
						if (!configuredTaskAwaiter.GetResult())
						{
							if (configAuthorityInfo.IsDefaultAuthority)
							{
								throw new MsalClientException("authority_host_mismatch", string.Concat(new string[] { "You did not define an authority at the application level, so it defaults to the https://login.microsoftonline.com/common. \n\rHowever, the request is for a different cloud ", requestAuthorityInfo.Host, ". This is not supported - the app and the request must target the same cloud. \n\r\n\r Add .WithAuthority(\"https://", requestAuthorityInfo.Host, "/common\") in the app builder. \n\rSee https://aka.ms/msal-net-authority-override for details" }));
							}
							throw new MsalClientException("authority_host_mismatch", string.Concat(new string[] { "\n\r The application is configured for cloud ", configAuthorityInfo.Host, " and the request for a different cloud - ", requestAuthorityInfo.Host, ". This is not supported - the app and the request must target the same cloud. \n\rSee https://aka.ms/msal-net-authority-override for details" }));
						}
					}
				}
			}

			// Token: 0x06001DE0 RID: 7648 RVA: 0x000688C4 File Offset: 0x00066AC4
			private static async Task<bool> IsAuthorityAliasedAsync(RequestContext requestContext, AuthorityInfo requestAuthorityInfo)
			{
				return (await requestContext.ServiceBundle.InstanceDiscoveryManager.GetMetadataEntryAsync(requestContext.ServiceBundle.Config.Authority.AuthorityInfo, requestContext, false).ConfigureAwait(false)).Aliases.Any((string alias) => alias.Equals(requestAuthorityInfo.Host));
			}
		}
	}
}
