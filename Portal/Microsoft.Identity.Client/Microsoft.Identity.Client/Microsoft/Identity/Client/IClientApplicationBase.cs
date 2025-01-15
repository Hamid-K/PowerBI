using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200014F RID: 335
	public interface IClientApplicationBase : IApplicationBase
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060010A4 RID: 4260
		IAppConfig AppConfig { get; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060010A5 RID: 4261
		ITokenCache UserTokenCache { get; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060010A6 RID: 4262
		string Authority { get; }

		// Token: 0x060010A7 RID: 4263
		Task<IEnumerable<IAccount>> GetAccountsAsync();

		// Token: 0x060010A8 RID: 4264
		Task<IAccount> GetAccountAsync(string identifier);

		// Token: 0x060010A9 RID: 4265
		Task<IEnumerable<IAccount>> GetAccountsAsync(string userFlow);

		// Token: 0x060010AA RID: 4266
		AcquireTokenSilentParameterBuilder AcquireTokenSilent(IEnumerable<string> scopes, IAccount account);

		// Token: 0x060010AB RID: 4267
		AcquireTokenSilentParameterBuilder AcquireTokenSilent(IEnumerable<string> scopes, string loginHint);

		// Token: 0x060010AC RID: 4268
		Task RemoveAsync(IAccount account);

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060010AD RID: 4269
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[Obsolete("Use GetAccountsAsync instead (See https://aka.ms/msal-net-2-released)", true)]
		IEnumerable<IUser> Users { get; }

		// Token: 0x060010AE RID: 4270
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use GetAccountAsync instead and pass IAccount.HomeAccountId.Identifier (See https://aka.ms/msal-net-2-released)", true)]
		IUser GetUser(string identifier);

		// Token: 0x060010AF RID: 4271
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use RemoveAccountAsync instead (See https://aka.ms/msal-net-2-released)", true)]
		void Remove(IUser user);

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060010B0 RID: 4272
		// (set) Token: 0x060010B1 RID: 4273
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WithComponent on AbstractApplicationBuilder<T> to configure this instead.  See https://aka.ms/msal-net-3-breaking-changes or https://aka.ms/msal-net-application-configuration", true)]
		string Component { get; set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060010B2 RID: 4274
		// (set) Token: 0x060010B3 RID: 4275
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ExtraQueryParameters on each call instead.  See https://aka.ms/msal-net-3-breaking-changes or https://aka.ms/msal-net-application-configuration", true)]
		string SliceParameters { get; set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060010B4 RID: 4276
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Can be set on AbstractApplicationBuilder<T>.WithAuthority as needed.  See https://aka.ms/msal-net-3-breaking-changes or https://aka.ms/msal-net-application-configuration", true)]
		bool ValidateAuthority { get; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060010B5 RID: 4277
		// (set) Token: 0x060010B6 RID: 4278
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Should be set using AbstractApplicationBuilder<T>.WithRedirectUri and can be viewed with ClientApplicationBase.AppConfig.RedirectUri. See https://aka.ms/msal-net-3-breaking-changes or https://aka.ms/msal-net-application-configuration", true)]
		string RedirectUri { get; set; }

		// Token: 0x060010B7 RID: 4279
		[Obsolete("Use AcquireTokenSilent instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<AuthenticationResult> AcquireTokenSilentAsync(IEnumerable<string> scopes, IAccount account);

		// Token: 0x060010B8 RID: 4280
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenSilent instead.See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenSilentAsync(IEnumerable<string> scopes, IAccount account, string authority, bool forceRefresh);

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060010B9 RID: 4281
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AppConfig.ClientId instead.See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		string ClientId { get; }
	}
}
