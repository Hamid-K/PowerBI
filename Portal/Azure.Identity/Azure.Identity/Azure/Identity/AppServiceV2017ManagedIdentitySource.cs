using System;

namespace Azure.Identity
{
	// Token: 0x02000021 RID: 33
	internal class AppServiceV2017ManagedIdentitySource : AppServiceManagedIdentitySource
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003EEA File Offset: 0x000020EA
		protected override string AppServiceMsiApiVersion
		{
			get
			{
				return "2017-09-01";
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003EF1 File Offset: 0x000020F1
		protected override string SecretHeaderName
		{
			get
			{
				return "secret";
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003EF8 File Offset: 0x000020F8
		protected override string ClientIdHeaderName
		{
			get
			{
				return "clientid";
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003F00 File Offset: 0x00002100
		public static ManagedIdentitySource TryCreate(ManagedIdentityClientOptions options)
		{
			string msiSecret = EnvironmentVariables.MsiSecret;
			Uri uri;
			if (!AppServiceManagedIdentitySource.TryValidateEnvVars(EnvironmentVariables.MsiEndpoint, msiSecret, out uri))
			{
				return null;
			}
			return new AppServiceV2017ManagedIdentitySource(options.Pipeline, uri, msiSecret, options);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003F32 File Offset: 0x00002132
		internal AppServiceV2017ManagedIdentitySource(CredentialPipeline pipeline, Uri endpoint, string secret, ManagedIdentityClientOptions options)
			: base(pipeline, endpoint, secret, options)
		{
		}
	}
}
