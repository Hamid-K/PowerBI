using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.OAuth;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost.Services.OAuth
{
	// Token: 0x02001B58 RID: 7000
	public static class OAuthCredentialExtensions
	{
		// Token: 0x0600AF49 RID: 44873 RVA: 0x0023E5CF File Offset: 0x0023C7CF
		public static TokenCredential ToTokenCredential(this OAuthCredential credential)
		{
			return new TokenCredential(credential.AccessToken, credential.Expires, credential.RefreshToken, new Dictionary<string, string>(credential.Properties));
		}

		// Token: 0x0600AF4A RID: 44874 RVA: 0x0023E5F4 File Offset: 0x0023C7F4
		public static CredentialDataCollection Merge(this OAuthCredentialData credentialData, TokenCredential tokenCredential, CredentialData[] adornments)
		{
			credentialData.AccessToken = tokenCredential.AccessToken;
			credentialData.RefreshToken = tokenCredential.RefreshToken;
			credentialData.Expires = tokenCredential.Expires;
			foreach (KeyValuePair<string, string> keyValuePair in tokenCredential.Properties)
			{
				if (!string.IsNullOrEmpty(keyValuePair.Value))
				{
					credentialData.Properties[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			CredentialDataCollection credentialDataCollection = new CredentialDataCollection();
			credentialDataCollection.Credentials.Add(credentialData);
			credentialDataCollection.Credentials.AddRange(adornments);
			return credentialDataCollection;
		}
	}
}
