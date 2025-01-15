using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000025 RID: 37
	[DataContract]
	internal sealed class SerializedOAuthSettings
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005B6E File Offset: 0x00003D6E
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00005B76 File Offset: 0x00003D76
		[DataMember]
		private SerializedOAuthSettings.SerializedSecureTokenService[] AllowedSecureTokenServices { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005B7F File Offset: 0x00003D7F
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00005B87 File Offset: 0x00003D87
		[DataMember]
		private SerializedOAuthSettings.SerializedTrustedResource[] ResourceList { get; set; }

		// Token: 0x06000116 RID: 278 RVA: 0x00005B90 File Offset: 0x00003D90
		public SerializedOAuthSettings()
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005B98 File Offset: 0x00003D98
		public SerializedOAuthSettings(ISecureTokenService[] tokenServices, TrustedResource[] resourceList)
		{
			this.AllowedSecureTokenServices = new SerializedOAuthSettings.SerializedSecureTokenService[tokenServices.Length];
			for (int i = 0; i < tokenServices.Length; i++)
			{
				SecureTokenService secureTokenService = tokenServices[i] as SecureTokenService;
				if (secureTokenService == null)
				{
					throw new NotSupportedException("requires SecureTokenService");
				}
				this.AllowedSecureTokenServices[i] = new SerializedOAuthSettings.SerializedSecureTokenService(secureTokenService);
			}
			this.ResourceList = new SerializedOAuthSettings.SerializedTrustedResource[resourceList.Length];
			for (int j = 0; j < resourceList.Length; j++)
			{
				this.ResourceList[j] = new SerializedOAuthSettings.SerializedTrustedResource(resourceList[j]);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005C18 File Offset: 0x00003E18
		public OAuthSettings ToOAuthSettings()
		{
			return new OAuthSettings(SerializedOAuthSettings.SerializedSecureTokenService.Deserialize(this.AllowedSecureTokenServices), SerializedOAuthSettings.SerializedTrustedResource.Deserialize(this.ResourceList));
		}

		// Token: 0x02000031 RID: 49
		[DataContract]
		private sealed class SerializedTrustedResource
		{
			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000171 RID: 369 RVA: 0x00007267 File Offset: 0x00005467
			// (set) Token: 0x06000172 RID: 370 RVA: 0x0000726F File Offset: 0x0000546F
			[DataMember]
			public string ResourceValue { get; set; }

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000173 RID: 371 RVA: 0x00007278 File Offset: 0x00005478
			// (set) Token: 0x06000174 RID: 372 RVA: 0x00007280 File Offset: 0x00005480
			[DataMember]
			public string[] Urls { get; set; }

			// Token: 0x06000175 RID: 373 RVA: 0x00007289 File Offset: 0x00005489
			public SerializedTrustedResource()
			{
			}

			// Token: 0x06000176 RID: 374 RVA: 0x00007291 File Offset: 0x00005491
			public SerializedTrustedResource(TrustedResource resource)
			{
				this.ResourceValue = resource.ResourceValue;
				this.Urls = resource.Urls;
			}

			// Token: 0x06000177 RID: 375 RVA: 0x000072B4 File Offset: 0x000054B4
			internal static TrustedResource[] Deserialize(SerializedOAuthSettings.SerializedTrustedResource[] resources)
			{
				TrustedResource[] array = new TrustedResource[resources.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new TrustedResource(resources[i].ResourceValue, resources[i].Urls);
				}
				return array;
			}
		}

		// Token: 0x02000032 RID: 50
		[DataContract]
		private sealed class SerializedSecureTokenService
		{
			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000178 RID: 376 RVA: 0x000072F1 File Offset: 0x000054F1
			// (set) Token: 0x06000179 RID: 377 RVA: 0x000072F9 File Offset: 0x000054F9
			[DataMember]
			public string AuthorityId { get; set; }

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x0600017A RID: 378 RVA: 0x00007302 File Offset: 0x00005502
			// (set) Token: 0x0600017B RID: 379 RVA: 0x0000730A File Offset: 0x0000550A
			[DataMember]
			public string AuthorizeUriTemplate { get; set; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x0600017C RID: 380 RVA: 0x00007313 File Offset: 0x00005513
			// (set) Token: 0x0600017D RID: 381 RVA: 0x0000731B File Offset: 0x0000551B
			[DataMember]
			public string TokenUriTemplate { get; set; }

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x0600017E RID: 382 RVA: 0x00007324 File Offset: 0x00005524
			// (set) Token: 0x0600017F RID: 383 RVA: 0x0000732C File Offset: 0x0000552C
			[DataMember]
			public string LogoutUriTemplate { get; set; }

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000180 RID: 384 RVA: 0x00007335 File Offset: 0x00005535
			// (set) Token: 0x06000181 RID: 385 RVA: 0x0000733D File Offset: 0x0000553D
			[DataMember]
			public string TenantPlaceholder { get; set; }

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x06000182 RID: 386 RVA: 0x00007346 File Offset: 0x00005546
			// (set) Token: 0x06000183 RID: 387 RVA: 0x0000734E File Offset: 0x0000554E
			[DataMember]
			public string DefaultTenant { get; set; }

			// Token: 0x06000184 RID: 388 RVA: 0x00007357 File Offset: 0x00005557
			public SerializedSecureTokenService()
			{
			}

			// Token: 0x06000185 RID: 389 RVA: 0x00007360 File Offset: 0x00005560
			public SerializedSecureTokenService(SecureTokenService tokenService)
			{
				this.AuthorityId = tokenService.AuthorityId;
				this.AuthorizeUriTemplate = tokenService.AuthorizeUriTemplate;
				this.TokenUriTemplate = tokenService.TokenUriTemplate;
				this.LogoutUriTemplate = tokenService.LogoutUriTemplate;
				this.TenantPlaceholder = tokenService.TenantPlaceholder;
				this.DefaultTenant = tokenService.DefaultTenant;
			}

			// Token: 0x06000186 RID: 390 RVA: 0x000073BC File Offset: 0x000055BC
			internal static ISecureTokenService[] Deserialize(SerializedOAuthSettings.SerializedSecureTokenService[] tokenServices)
			{
				ISecureTokenService[] array = new ISecureTokenService[tokenServices.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new SecureTokenService(tokenServices[i].AuthorityId, tokenServices[i].AuthorizeUriTemplate, tokenServices[i].TokenUriTemplate, tokenServices[i].LogoutUriTemplate, tokenServices[i].TenantPlaceholder, tokenServices[i].DefaultTenant);
				}
				return array;
			}
		}
	}
}
