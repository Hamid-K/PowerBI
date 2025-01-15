using System;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000019 RID: 25
	public sealed class OAuthClientApplication
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x0000481C File Offset: 0x00002A1C
		public OAuthClientApplication(string id, string secret, string callbackUrl, ClientApplicationSecretType secretType = ClientApplicationSecretType.Default)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			if (secret == null)
			{
				throw new ArgumentNullException("secret");
			}
			if (string.IsNullOrEmpty(callbackUrl))
			{
				throw new ArgumentNullException("redirectUrl");
			}
			this.id = id;
			this.secret = secret;
			this.callbackUrl = callbackUrl;
			this.secretType = secretType;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004880 File Offset: 0x00002A80
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004888 File Offset: 0x00002A88
		public string CallbackUrl
		{
			get
			{
				return this.callbackUrl;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004890 File Offset: 0x00002A90
		public string Secret
		{
			get
			{
				return this.secret;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004898 File Offset: 0x00002A98
		public ClientApplicationSecretType SecretType
		{
			get
			{
				return this.secretType;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000048A0 File Offset: 0x00002AA0
		public SubjectNameIssuer SubjectNameIssuer
		{
			get
			{
				if (this.SecretType != ClientApplicationSecretType.SubjectNameIssuer)
				{
					throw new InvalidOperationException("Secret lookup isn't a subject name + issuer type");
				}
				int num = this.secret.IndexOf(';');
				if (num <= 0 || num == this.secret.Length - 1)
				{
					throw new FormatException("Secret string is in an improper format. The format is 'subjectName;IssuerName'");
				}
				return new SubjectNameIssuer(this.secret.Substring(0, num), this.secret.Substring(num + 1));
			}
		}

		// Token: 0x040000A9 RID: 169
		private readonly string id;

		// Token: 0x040000AA RID: 170
		private readonly string callbackUrl;

		// Token: 0x040000AB RID: 171
		private readonly string secret;

		// Token: 0x040000AC RID: 172
		private readonly ClientApplicationSecretType secretType;
	}
}
