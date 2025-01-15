using System;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000016 RID: 22
	public interface ISecureTokenService
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008A RID: 138
		string AuthorityId { get; }

		// Token: 0x0600008B RID: 139
		Uri GetAuthorizeUri(string tenant);

		// Token: 0x0600008C RID: 140
		Uri GetTokenUri(string tenant);

		// Token: 0x0600008D RID: 141
		Uri GetLogoutUri(string tenant);
	}
}
