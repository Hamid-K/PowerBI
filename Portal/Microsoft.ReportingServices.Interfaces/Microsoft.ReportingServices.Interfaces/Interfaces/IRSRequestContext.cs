using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000054 RID: 84
	public interface IRSRequestContext
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000EE RID: 238
		IDictionary<string, string> Cookies { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000EF RID: 239
		IDictionary<string, string[]> Headers { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000F0 RID: 240
		IIdentity User { get; }
	}
}
