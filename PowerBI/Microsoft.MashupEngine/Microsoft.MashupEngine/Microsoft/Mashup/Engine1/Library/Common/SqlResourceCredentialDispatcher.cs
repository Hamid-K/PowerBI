using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001132 RID: 4402
	internal abstract class SqlResourceCredentialDispatcher : ResourceCredentialDispatcher
	{
		// Token: 0x0600732C RID: 29484 RVA: 0x0018CE20 File Offset: 0x0018B020
		protected SqlResourceCredentialDispatcher(IEngineHost host, IResource resource)
		{
			this.host = host;
			this.resource = resource;
			this.impersonate = () => null;
			this.alternateIdentity = null;
		}

		// Token: 0x0600732D RID: 29485 RVA: 0x0018CE70 File Offset: 0x0018B070
		public virtual bool Apply(ResourceCredentialCollection credentials)
		{
			IResourceCredential resourceCredential;
			EncryptedConnectionAdornment encryptedConnectionAdornment;
			ApplicationCredentialPropertiesAdornment applicationCredentialPropertiesAdornment;
			ConnectionStringPropertiesAdornment connectionStringPropertiesAdornment;
			return credentials.TryGetDatabasePattern(out resourceCredential, out encryptedConnectionAdornment, out applicationCredentialPropertiesAdornment, out connectionStringPropertiesAdornment) && (base.Apply(resourceCredential) && this.Apply(encryptedConnectionAdornment)) && (connectionStringPropertiesAdornment == null || this.Apply(connectionStringPropertiesAdornment));
		}

		// Token: 0x0600732E RID: 29486 RVA: 0x00002105 File Offset: 0x00000305
		protected sealed override bool Apply(FeedKeyCredential credential)
		{
			return false;
		}

		// Token: 0x0600732F RID: 29487 RVA: 0x00002105 File Offset: 0x00000305
		protected sealed override bool Apply(FtpLoginAuthCredential credential)
		{
			return false;
		}

		// Token: 0x06007330 RID: 29488 RVA: 0x00002105 File Offset: 0x00000305
		protected override bool Apply(BasicAuthCredential credential)
		{
			return false;
		}

		// Token: 0x06007331 RID: 29489 RVA: 0x00002105 File Offset: 0x00000305
		protected sealed override bool Apply(WebApiKeyCredential credential)
		{
			return false;
		}

		// Token: 0x06007332 RID: 29490 RVA: 0x00002105 File Offset: 0x00000305
		protected override bool Apply(OAuthCredential credential)
		{
			return false;
		}

		// Token: 0x06007333 RID: 29491 RVA: 0x00002105 File Offset: 0x00000305
		protected override bool Apply(ConnectionStringAdornment credential)
		{
			return false;
		}

		// Token: 0x06007334 RID: 29492 RVA: 0x00002105 File Offset: 0x00000305
		protected override bool Apply(SharedKeyAuthCredential credential)
		{
			return false;
		}

		// Token: 0x06007335 RID: 29493 RVA: 0x0018CEAF File Offset: 0x0018B0AF
		protected sealed override bool Apply(EncryptedConnectionAdornment credential)
		{
			this.requireEncryption = credential.RequireEncryption;
			return this.ApplyEncryptedCredentialAdornment(credential);
		}

		// Token: 0x06007336 RID: 29494
		protected abstract bool ApplyEncryptedCredentialAdornment(EncryptedConnectionAdornment credential);

		// Token: 0x06007337 RID: 29495 RVA: 0x0018CEC4 File Offset: 0x0018B0C4
		protected sealed override bool Apply(WindowsCredential credential)
		{
			this.impersonate = this.GetImpersonationWrapper(credential);
			this.alternateIdentity = credential.Username;
			return this.ApplyWindowsCredential(credential);
		}

		// Token: 0x06007338 RID: 29496 RVA: 0x0018CEE6 File Offset: 0x0018B0E6
		protected virtual Func<IDisposable> GetImpersonationWrapper(WindowsCredential credential)
		{
			return credential.GetImpersonationWrapper(this.host, this.resource);
		}

		// Token: 0x06007339 RID: 29497
		protected abstract bool ApplyWindowsCredential(WindowsCredential credential);

		// Token: 0x17002024 RID: 8228
		// (get) Token: 0x0600733A RID: 29498 RVA: 0x0018CEFA File Offset: 0x0018B0FA
		protected IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17002025 RID: 8229
		// (get) Token: 0x0600733B RID: 29499 RVA: 0x0018CF02 File Offset: 0x0018B102
		protected IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17002026 RID: 8230
		// (get) Token: 0x0600733C RID: 29500 RVA: 0x0018CF0A File Offset: 0x0018B10A
		protected string AlternateIdentity
		{
			get
			{
				return this.alternateIdentity;
			}
		}

		// Token: 0x17002027 RID: 8231
		// (get) Token: 0x0600733D RID: 29501 RVA: 0x0018CF12 File Offset: 0x0018B112
		protected Func<IDisposable> Impersonate
		{
			get
			{
				return this.impersonate;
			}
		}

		// Token: 0x17002028 RID: 8232
		// (get) Token: 0x0600733E RID: 29502 RVA: 0x0018CF1A File Offset: 0x0018B11A
		protected bool RequireEncryption
		{
			get
			{
				return this.requireEncryption;
			}
		}

		// Token: 0x0600733F RID: 29503 RVA: 0x0018CF24 File Offset: 0x0018B124
		protected void SetOAuthIdentity(OAuthCredential credential)
		{
			string text;
			if (JwtParser.TryParseSubject(credential.AccessToken, out text))
			{
				this.alternateIdentity = text;
				return;
			}
			if (string.IsNullOrEmpty(credential.RefreshToken))
			{
				this.alternateIdentity = credential.AccessToken;
				return;
			}
			this.alternateIdentity = credential.RefreshToken;
		}

		// Token: 0x04003F83 RID: 16259
		private readonly IResource resource;

		// Token: 0x04003F84 RID: 16260
		private readonly IEngineHost host;

		// Token: 0x04003F85 RID: 16261
		private Func<IDisposable> impersonate;

		// Token: 0x04003F86 RID: 16262
		private string alternateIdentity;

		// Token: 0x04003F87 RID: 16263
		private bool requireEncryption;
	}
}
