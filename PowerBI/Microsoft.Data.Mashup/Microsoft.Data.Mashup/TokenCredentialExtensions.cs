using System;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000011 RID: 17
	public static class TokenCredentialExtensions
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00004BD9 File Offset: 0x00002DD9
		public static DataSourceSetting ToOAuth2Credential(this TokenCredential credential)
		{
			return DataSourceSetting.CreateOAuth2Credential(credential.AccessToken, credential.Properties);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004BEC File Offset: 0x00002DEC
		public static DataSourceSetting ToRefreshableOAuth2Credential(this TokenCredential credential)
		{
			return DataSourceSetting.CreateRefreshableOAuth2Credential(credential.AccessToken, credential.Expires, credential.RefreshToken, credential.Properties);
		}
	}
}
