using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200111A RID: 4378
	internal abstract class ResourceCredentialDispatcher
	{
		// Token: 0x06007288 RID: 29320 RVA: 0x00189C28 File Offset: 0x00187E28
		protected bool Apply(IResourceCredential credential)
		{
			SqlAuthCredential sqlAuthCredential = credential as SqlAuthCredential;
			if (sqlAuthCredential != null)
			{
				return this.Apply(sqlAuthCredential);
			}
			WindowsCredential windowsCredential = credential as WindowsCredential;
			if (windowsCredential != null)
			{
				return this.Apply(windowsCredential);
			}
			FeedKeyCredential feedKeyCredential = credential as FeedKeyCredential;
			if (feedKeyCredential != null)
			{
				return this.Apply(feedKeyCredential);
			}
			FtpLoginAuthCredential ftpLoginAuthCredential = credential as FtpLoginAuthCredential;
			if (ftpLoginAuthCredential != null)
			{
				return this.Apply(ftpLoginAuthCredential);
			}
			SharedKeyAuthCredential sharedKeyAuthCredential = credential as SharedKeyAuthCredential;
			if (sharedKeyAuthCredential != null)
			{
				return this.Apply(sharedKeyAuthCredential);
			}
			BasicAuthCredential basicAuthCredential = credential as BasicAuthCredential;
			if (basicAuthCredential != null)
			{
				return this.Apply(basicAuthCredential);
			}
			WebApiKeyCredential webApiKeyCredential = credential as WebApiKeyCredential;
			if (webApiKeyCredential != null)
			{
				return this.Apply(webApiKeyCredential);
			}
			OAuthCredential oauthCredential = credential as OAuthCredential;
			if (oauthCredential != null)
			{
				return this.Apply(oauthCredential);
			}
			ParameterizedCredential parameterizedCredential = credential as ParameterizedCredential;
			if (parameterizedCredential != null)
			{
				return this.Apply(parameterizedCredential);
			}
			EncryptedConnectionAdornment encryptedConnectionAdornment = credential as EncryptedConnectionAdornment;
			if (encryptedConnectionAdornment != null)
			{
				return this.Apply(encryptedConnectionAdornment);
			}
			ConnectionStringAdornment connectionStringAdornment = credential as ConnectionStringAdornment;
			if (connectionStringAdornment != null)
			{
				return this.Apply(connectionStringAdornment);
			}
			ConnectionStringPropertiesAdornment connectionStringPropertiesAdornment = credential as ConnectionStringPropertiesAdornment;
			return connectionStringPropertiesAdornment != null && this.Apply(connectionStringPropertiesAdornment);
		}

		// Token: 0x06007289 RID: 29321
		protected abstract bool Apply(SqlAuthCredential credential);

		// Token: 0x0600728A RID: 29322
		protected abstract bool Apply(WindowsCredential credential);

		// Token: 0x0600728B RID: 29323
		protected abstract bool Apply(FeedKeyCredential credential);

		// Token: 0x0600728C RID: 29324
		protected abstract bool Apply(FtpLoginAuthCredential credential);

		// Token: 0x0600728D RID: 29325
		protected abstract bool Apply(BasicAuthCredential credential);

		// Token: 0x0600728E RID: 29326
		protected abstract bool Apply(WebApiKeyCredential credential);

		// Token: 0x0600728F RID: 29327
		protected abstract bool Apply(OAuthCredential credential);

		// Token: 0x06007290 RID: 29328
		protected abstract bool Apply(EncryptedConnectionAdornment credential);

		// Token: 0x06007291 RID: 29329
		protected abstract bool Apply(ConnectionStringAdornment credential);

		// Token: 0x06007292 RID: 29330
		protected abstract bool Apply(SharedKeyAuthCredential credential);

		// Token: 0x06007293 RID: 29331 RVA: 0x00002105 File Offset: 0x00000305
		protected virtual bool Apply(ParameterizedCredential credential)
		{
			return false;
		}

		// Token: 0x06007294 RID: 29332 RVA: 0x00002105 File Offset: 0x00000305
		protected virtual bool Apply(ConnectionStringPropertiesAdornment credential)
		{
			return false;
		}
	}
}
