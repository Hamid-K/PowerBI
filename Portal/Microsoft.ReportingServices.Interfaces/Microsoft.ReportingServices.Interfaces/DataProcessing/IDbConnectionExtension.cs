using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000004 RID: 4
	public interface IDbConnectionExtension : IDbConnection, IDisposable, IExtension
	{
		// Token: 0x17000003 RID: 3
		// (set) Token: 0x06000008 RID: 8
		string Impersonate { set; }

		// Token: 0x17000004 RID: 4
		// (set) Token: 0x06000009 RID: 9
		string UserName { set; }

		// Token: 0x17000005 RID: 5
		// (set) Token: 0x0600000A RID: 10
		string Password { set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11
		// (set) Token: 0x0600000C RID: 12
		bool IntegratedSecurity { get; set; }
	}
}
