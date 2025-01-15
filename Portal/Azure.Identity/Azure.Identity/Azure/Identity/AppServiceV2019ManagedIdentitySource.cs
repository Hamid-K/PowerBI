using System;

namespace Azure.Identity
{
	// Token: 0x02000022 RID: 34
	internal class AppServiceV2019ManagedIdentitySource : AppServiceManagedIdentitySource
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003F3F File Offset: 0x0000213F
		protected override string AppServiceMsiApiVersion
		{
			get
			{
				return "2019-08-01";
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003F46 File Offset: 0x00002146
		protected override string SecretHeaderName
		{
			get
			{
				return "X-IDENTITY-HEADER";
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003F4D File Offset: 0x0000214D
		protected override string ClientIdHeaderName
		{
			get
			{
				return "client_id";
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003F54 File Offset: 0x00002154
		public static ManagedIdentitySource TryCreate(ManagedIdentityClientOptions options)
		{
			string identityHeader = EnvironmentVariables.IdentityHeader;
			Uri uri;
			if (!AppServiceManagedIdentitySource.TryValidateEnvVars(EnvironmentVariables.IdentityEndpoint, identityHeader, out uri))
			{
				return null;
			}
			return new AppServiceV2019ManagedIdentitySource(options.Pipeline, uri, identityHeader, options);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003F86 File Offset: 0x00002186
		public AppServiceV2019ManagedIdentitySource(CredentialPipeline pipeline, Uri endpoint, string secret, ManagedIdentityClientOptions options)
			: base(pipeline, endpoint, secret, options)
		{
		}
	}
}
