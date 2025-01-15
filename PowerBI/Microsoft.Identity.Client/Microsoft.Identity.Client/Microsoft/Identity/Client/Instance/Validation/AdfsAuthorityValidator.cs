using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.OAuth2;

namespace Microsoft.Identity.Client.Instance.Validation
{
	// Token: 0x02000273 RID: 627
	internal class AdfsAuthorityValidator : IAuthorityValidator
	{
		// Token: 0x0600189D RID: 6301 RVA: 0x000517CB File Offset: 0x0004F9CB
		public AdfsAuthorityValidator(RequestContext requestContext)
		{
			this._requestContext = requestContext;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x000517DC File Offset: 0x0004F9DC
		public async Task ValidateAuthorityAsync(AuthorityInfo authorityInfo)
		{
			if (authorityInfo.ValidateAuthority)
			{
				AdfsAuthorityValidator.<>c__DisplayClass2_0 CS$<>8__locals1 = new AdfsAuthorityValidator.<>c__DisplayClass2_0();
				CS$<>8__locals1.resource = "https://" + authorityInfo.Host;
				string text = Constants.FormatAdfsWebFingerUrl(authorityInfo.Host, CS$<>8__locals1.resource);
				HttpResponse httpResponse = await this._requestContext.ServiceBundle.HttpManager.SendGetAsync(new Uri(text), null, this._requestContext.Logger, true, this._requestContext.UserCancellationToken).ConfigureAwait(false);
				if (httpResponse.StatusCode != HttpStatusCode.OK)
				{
					this._requestContext.Logger.Error(string.Format("Authority validation failed because the configured authority is invalid. Authority: {0}", authorityInfo.CanonicalAuthority));
					throw MsalServiceExceptionFactory.FromHttpResponse("invalid_authority", "Authority validation failed. ", httpResponse, null);
				}
				if (OAuth2Client.CreateResponse<AdfsWebFingerResponse>(httpResponse, this._requestContext).Links.FirstOrDefault((LinksList a) => a.Rel.Equals("http://schemas.microsoft.com/rel/trusted-realm", StringComparison.OrdinalIgnoreCase) && a.Href.Equals(CS$<>8__locals1.resource)) == null)
				{
					this._requestContext.Logger.Error(string.Format("Authority validation failed because the configured authority is invalid. Authority: {0}", authorityInfo.CanonicalAuthority));
					throw new MsalClientException("invalid_authority", "invalid authority while getting the open id config endpoint. ");
				}
				CS$<>8__locals1 = null;
			}
		}

		// Token: 0x04000B23 RID: 2851
		private readonly RequestContext _requestContext;
	}
}
