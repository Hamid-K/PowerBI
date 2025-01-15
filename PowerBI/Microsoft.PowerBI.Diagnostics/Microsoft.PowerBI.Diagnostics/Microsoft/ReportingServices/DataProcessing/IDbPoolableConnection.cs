using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200001C RID: 28
	internal interface IDbPoolableConnection : IDbConnection, IDisposable, IExtension
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600008B RID: 139
		bool IsAlive { get; }

		// Token: 0x0600008C RID: 140
		string GetConnectionStringForPooling();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008D RID: 141
		// (set) Token: 0x0600008E RID: 142
		bool IsFromPool { get; set; }
	}
}
