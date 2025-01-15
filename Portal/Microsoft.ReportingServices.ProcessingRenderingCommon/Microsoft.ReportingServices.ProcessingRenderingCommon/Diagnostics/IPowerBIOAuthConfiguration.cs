using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200003B RID: 59
	internal interface IPowerBIOAuthConfiguration
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001C6 RID: 454
		string ResourceUrl { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001C7 RID: 455
		string AuthorizationUrl { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001C8 RID: 456
		List<string> RedirectUrls { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001C9 RID: 457
		string TokenUrl { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001CA RID: 458
		string LogoutUrl { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001CB RID: 459
		string ClientId { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001CC RID: 460
		string ClientSecret { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001CD RID: 461
		string AppObjectId { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001CE RID: 462
		string PowerBIEndpoint { get; }
	}
}
