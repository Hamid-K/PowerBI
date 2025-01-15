using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200001A RID: 26
	public interface IDbConnectionExtension : IDbConnection, IDisposable, IExtension
	{
		// Token: 0x17000008 RID: 8
		// (set) Token: 0x06000085 RID: 133
		string Impersonate { set; }

		// Token: 0x17000009 RID: 9
		// (set) Token: 0x06000086 RID: 134
		string UserName { set; }

		// Token: 0x1700000A RID: 10
		// (set) Token: 0x06000087 RID: 135
		string Password { set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000088 RID: 136
		// (set) Token: 0x06000089 RID: 137
		bool IntegratedSecurity { get; set; }
	}
}
