using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.Instance.Validation
{
	// Token: 0x02000272 RID: 626
	internal class AadAuthorityValidator : IAuthorityValidator
	{
		// Token: 0x0600189B RID: 6299 RVA: 0x00051770 File Offset: 0x0004F970
		public AadAuthorityValidator(RequestContext requestContext)
		{
			this._requestContext = requestContext;
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00051780 File Offset: 0x0004F980
		public async Task ValidateAuthorityAsync(AuthorityInfo authorityInfo)
		{
			Uri canonicalAuthority = authorityInfo.CanonicalAuthority;
			bool isKnownEnv = KnownMetadataProvider.IsKnownEnvironment(canonicalAuthority.Host);
			this._requestContext.Logger.Info(() => string.Format("Authority validation enabled? {0}. ", authorityInfo.ValidateAuthority));
			this._requestContext.Logger.Info(() => string.Format("Authority validation - is known env? {0}. ", isKnownEnv));
			if (!isKnownEnv)
			{
				this._requestContext.Logger.Info("Authority validation is being performed. ");
				await this._requestContext.ServiceBundle.InstanceDiscoveryManager.GetMetadataEntryAsync(authorityInfo, this._requestContext, true).ConfigureAwait(false);
			}
		}

		// Token: 0x04000B22 RID: 2850
		private readonly RequestContext _requestContext;
	}
}
