using System;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000014 RID: 20
	public interface IOAuthProvider
	{
		// Token: 0x06000085 RID: 133
		OAuthBrowserNavigation StartLogin(string state, string display);

		// Token: 0x06000086 RID: 134
		TokenCredential FinishLogin(byte[] serializedContext, Uri callbackUri, string state);

		// Token: 0x06000087 RID: 135
		TokenCredential Refresh(TokenCredential credential);

		// Token: 0x06000088 RID: 136
		Uri Logout(string accessToken);
	}
}
