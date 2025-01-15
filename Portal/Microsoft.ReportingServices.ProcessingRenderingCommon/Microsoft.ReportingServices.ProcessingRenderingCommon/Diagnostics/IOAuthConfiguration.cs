using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000039 RID: 57
	public interface IOAuthConfiguration
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001B7 RID: 439
		string AuthorizationUrl { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001B8 RID: 440
		string FederationMetadataUrl { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001B9 RID: 441
		string ClientId { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001BA RID: 442
		string ClientSecret { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001BB RID: 443
		string TenantId { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001BC RID: 444
		string TokenUrl { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001BD RID: 445
		string ResourceUrl { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001BE RID: 446
		string NativeClientId { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001BF RID: 447
		string GraphUrl { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001C0 RID: 448
		string SessionCookieName { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001C1 RID: 449
		string LogoutUrl { get; }

		// Token: 0x060001C2 RID: 450
		IDictionary<string, string> GetProperties(IEnumerable<string> requestedProperties);
	}
}
