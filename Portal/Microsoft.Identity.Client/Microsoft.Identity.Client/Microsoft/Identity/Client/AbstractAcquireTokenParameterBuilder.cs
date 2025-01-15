using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.AuthScheme;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000117 RID: 279
	public abstract class AbstractAcquireTokenParameterBuilder<T> : BaseAbstractAcquireTokenParameterBuilder<T> where T : BaseAbstractAcquireTokenParameterBuilder<T>
	{
		// Token: 0x06000DEA RID: 3562 RVA: 0x00036F47 File Offset: 0x00035147
		protected AbstractAcquireTokenParameterBuilder()
		{
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00036F4F File Offset: 0x0003514F
		internal AbstractAcquireTokenParameterBuilder(IServiceBundle serviceBundle)
			: base(serviceBundle)
		{
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00036F58 File Offset: 0x00035158
		protected T WithScopes(IEnumerable<string> scopes)
		{
			base.CommonParameters.Scopes = scopes;
			return this as T;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00036F71 File Offset: 0x00035171
		public T WithExtraQueryParameters(Dictionary<string, string> extraQueryParameters)
		{
			base.CommonParameters.ExtraQueryParameters = extraQueryParameters ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			return this as T;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00036F98 File Offset: 0x00035198
		public T WithClaims(string claims)
		{
			base.CommonParameters.Claims = claims;
			return this as T;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00036FB1 File Offset: 0x000351B1
		public T WithExtraQueryParameters(string extraQueryParameters)
		{
			if (!string.IsNullOrWhiteSpace(extraQueryParameters))
			{
				return this.WithExtraQueryParameters(CoreHelpers.ParseKeyValueList(extraQueryParameters, '&', true, null));
			}
			return this as T;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00036FD7 File Offset: 0x000351D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API has been deprecated. You can override the tenant ID in the request using WithTenantId. See https://aka.ms/msal-net-authority-override ")]
		public T WithAuthority(string authorityUri, bool validateAuthority = true)
		{
			if (string.IsNullOrWhiteSpace(authorityUri))
			{
				throw new ArgumentNullException("authorityUri");
			}
			base.CommonParameters.AuthorityOverride = AuthorityInfo.FromAuthorityUri(authorityUri, validateAuthority);
			return this as T;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00037009 File Offset: 0x00035209
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API has been deprecated. You can override the tenant ID in the request using WithTenantId. See https://aka.ms/msal-net-authority-override ")]
		public T WithAuthority(string cloudInstanceUri, Guid tenantId, bool validateAuthority = true)
		{
			if (string.IsNullOrWhiteSpace(cloudInstanceUri))
			{
				throw new ArgumentNullException("cloudInstanceUri");
			}
			base.CommonParameters.AuthorityOverride = AuthorityInfo.FromAadAuthority(cloudInstanceUri, tenantId, validateAuthority);
			return this as T;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0003703C File Offset: 0x0003523C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API has been deprecated. You can override the tenant ID in the request using WithTenantId. See https://aka.ms/msal-net-authority-override ")]
		public T WithAuthority(string cloudInstanceUri, string tenant, bool validateAuthority = true)
		{
			if (string.IsNullOrWhiteSpace(cloudInstanceUri))
			{
				throw new ArgumentNullException("cloudInstanceUri");
			}
			base.CommonParameters.AuthorityOverride = AuthorityInfo.FromAadAuthority(cloudInstanceUri, tenant, validateAuthority);
			return this as T;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003706F File Offset: 0x0003526F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API has been deprecated. You can override the tenant ID in the request using WithTenantId. See https://aka.ms/msal-net-authority-override ")]
		public T WithAuthority(AzureCloudInstance azureCloudInstance, Guid tenantId, bool validateAuthority = true)
		{
			base.CommonParameters.AuthorityOverride = AuthorityInfo.FromAadAuthority(azureCloudInstance, tenantId, validateAuthority);
			return this as T;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003708F File Offset: 0x0003528F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API has been deprecated. You can override the tenant ID in the request using WithTenantId. See https://aka.ms/msal-net-authority-override ")]
		public T WithAuthority(AzureCloudInstance azureCloudInstance, string tenant, bool validateAuthority = true)
		{
			base.CommonParameters.AuthorityOverride = AuthorityInfo.FromAadAuthority(azureCloudInstance, tenant, validateAuthority);
			return this as T;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000370AF File Offset: 0x000352AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API has been deprecated. You can override the tenant ID in the request using WithTenantId. See https://aka.ms/msal-net-authority-override ")]
		public T WithAuthority(AzureCloudInstance azureCloudInstance, AadAuthorityAudience authorityAudience, bool validateAuthority = true)
		{
			base.CommonParameters.AuthorityOverride = AuthorityInfo.FromAadAuthority(azureCloudInstance, authorityAudience, validateAuthority);
			return this as T;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x000370CF File Offset: 0x000352CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API has been deprecated. You can override the tenant ID in the request using WithTenantId. See https://aka.ms/msal-net-authority-override ")]
		public T WithAuthority(AadAuthorityAudience authorityAudience, bool validateAuthority = true)
		{
			base.CommonParameters.AuthorityOverride = AuthorityInfo.FromAadAuthority(authorityAudience, validateAuthority);
			return this as T;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x000370F0 File Offset: 0x000352F0
		public T WithTenantId(string tenantId)
		{
			if (string.IsNullOrEmpty(tenantId))
			{
				throw new ArgumentNullException("tenantId");
			}
			Authority authority = AuthorityInfo.AuthorityInfoHelper.CreateAuthorityWithTenant(base.ServiceBundle.Config.Authority, tenantId, true);
			base.CommonParameters.AuthorityOverride = authority.AuthorityInfo;
			return this as T;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00037144 File Offset: 0x00035344
		public T WithTenantIdFromAuthority(Uri authorityUri)
		{
			if (authorityUri == null)
			{
				throw new ArgumentNullException("authorityUri");
			}
			Authority authority = Authority.CreateAuthority(AuthorityInfo.FromAuthorityUri(authorityUri.ToString(), false));
			return this.WithTenantId(authority.TenantId);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00037183 File Offset: 0x00035383
		public T WithAdfsAuthority(string authorityUri, bool validateAuthority = true)
		{
			if (string.IsNullOrWhiteSpace(authorityUri))
			{
				throw new ArgumentNullException("authorityUri");
			}
			base.CommonParameters.AuthorityOverride = new AuthorityInfo(AuthorityType.Adfs, authorityUri, validateAuthority);
			return this as T;
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x000371B6 File Offset: 0x000353B6
		public T WithB2CAuthority(string authorityUri)
		{
			if (string.IsNullOrWhiteSpace(authorityUri))
			{
				throw new ArgumentNullException("authorityUri");
			}
			base.CommonParameters.AuthorityOverride = new AuthorityInfo(AuthorityType.B2C, authorityUri, false);
			return this as T;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x000371E9 File Offset: 0x000353E9
		internal T WithAuthenticationScheme(IAuthenticationScheme scheme)
		{
			AcquireTokenCommonParameters commonParameters = base.CommonParameters;
			if (scheme == null)
			{
				throw new ArgumentNullException("scheme");
			}
			commonParameters.AuthenticationScheme = scheme;
			return this as T;
		}
	}
}
