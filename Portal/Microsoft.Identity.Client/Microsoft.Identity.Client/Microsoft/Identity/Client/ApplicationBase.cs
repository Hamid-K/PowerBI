using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Requests;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000142 RID: 322
	public abstract class ApplicationBase : IApplicationBase
	{
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0003A7E3 File Offset: 0x000389E3
		internal IServiceBundle ServiceBundle { get; }

		// Token: 0x06001015 RID: 4117 RVA: 0x0003A7EB File Offset: 0x000389EB
		internal ApplicationBase(ApplicationConfiguration config)
		{
			this.ServiceBundle = Microsoft.Identity.Client.Internal.ServiceBundle.Create(config);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0003A800 File Offset: 0x00038A00
		internal virtual async Task<AuthenticationRequestParameters> CreateRequestParametersAsync(AcquireTokenCommonParameters commonParameters, RequestContext requestContext, ITokenCacheInternal cache)
		{
			Authority authority = await Authority.CreateAuthorityForRequestAsync(requestContext, commonParameters.AuthorityOverride, null).ConfigureAwait(false);
			return new AuthenticationRequestParameters(this.ServiceBundle, cache, commonParameters, requestContext, authority, null);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0003A85B File Offset: 0x00038A5B
		internal static void GuardMobileFrameworks()
		{
		}

		// Token: 0x040004D9 RID: 1241
		internal const string DefaultAuthority = "https://login.microsoftonline.com/common/";
	}
}
