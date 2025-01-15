using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Instance
{
	// Token: 0x0200026D RID: 621
	internal class AuthorityManager
	{
		// Token: 0x06001881 RID: 6273 RVA: 0x0005139D File Offset: 0x0004F59D
		public AuthorityManager(RequestContext requestContext, Authority initialAuthority)
		{
			this._requestContext = requestContext;
			this._initialAuthority = initialAuthority;
			this._currentAuthority = initialAuthority;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x000513BA File Offset: 0x0004F5BA
		public Authority OriginalAuthority
		{
			get
			{
				return this._initialAuthority;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x000513C2 File Offset: 0x0004F5C2
		public Authority Authority
		{
			get
			{
				return this._currentAuthority;
			}
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x000513CC File Offset: 0x0004F5CC
		public async Task<InstanceDiscoveryMetadataEntry> GetInstanceDiscoveryEntryAsync()
		{
			await this.RunInstanceDiscoveryAndValidationAsync().ConfigureAwait(false);
			return this._metadata;
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00051410 File Offset: 0x0004F610
		public async Task RunInstanceDiscoveryAndValidationAsync()
		{
			if (!this._instanceDiscoveryAndValidationExecuted)
			{
				InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry = await this._requestContext.ServiceBundle.InstanceDiscoveryManager.GetMetadataEntryAsync(this._initialAuthority.AuthorityInfo, this._requestContext, false).ConfigureAwait(false);
				this._metadata = instanceDiscoveryMetadataEntry;
				this._currentAuthority = Authority.CreateAuthorityWithEnvironment(this._initialAuthority.AuthorityInfo, this._metadata.PreferredNetwork);
				if (this._initialAuthority.AuthorityInfo.ValidateAuthority && this._requestContext.ServiceBundle.Config.IsInstanceDiscoveryEnabled)
				{
					await this.ValidateAuthorityAsync(this._initialAuthority).ConfigureAwait(false);
				}
				this._instanceDiscoveryAndValidationExecuted = true;
			}
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00051453 File Offset: 0x0004F653
		public static void ClearValidationCache()
		{
			AuthorityManager.s_validatedEnvironments.Clear();
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00051460 File Offset: 0x0004F660
		private async Task ValidateAuthorityAsync(Authority authority)
		{
			if (!AuthorityManager.s_validatedEnvironments.Contains(authority.AuthorityInfo.Host))
			{
				await AuthorityInfo.AuthorityInfoHelper.CreateAuthorityValidator(authority.AuthorityInfo, this._requestContext).ValidateAuthorityAsync(authority.AuthorityInfo).ConfigureAwait(false);
				AuthorityManager.s_validatedEnvironments.Add(authority.AuthorityInfo.Host);
			}
		}

		// Token: 0x04000B14 RID: 2836
		private static readonly ConcurrentHashSet<string> s_validatedEnvironments = new ConcurrentHashSet<string>();

		// Token: 0x04000B15 RID: 2837
		private readonly RequestContext _requestContext;

		// Token: 0x04000B16 RID: 2838
		private readonly Authority _initialAuthority;

		// Token: 0x04000B17 RID: 2839
		private Authority _currentAuthority;

		// Token: 0x04000B18 RID: 2840
		private bool _instanceDiscoveryAndValidationExecuted;

		// Token: 0x04000B19 RID: 2841
		private InstanceDiscoveryMetadataEntry _metadata;
	}
}
