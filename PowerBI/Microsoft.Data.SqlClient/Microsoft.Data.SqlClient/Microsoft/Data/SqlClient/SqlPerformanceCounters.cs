using System;
using System.Diagnostics;
using System.Security.Permissions;
using Microsoft.Data.ProviderBase;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000E9 RID: 233
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	internal sealed class SqlPerformanceCounters : DbConnectionPoolCounters
	{
		// Token: 0x0600114B RID: 4427 RVA: 0x0003FCA6 File Offset: 0x0003DEA6
		[PerformanceCounterPermission(SecurityAction.Assert, PermissionAccess = PerformanceCounterPermissionAccess.Write, MachineName = ".", CategoryName = ".NET Data Provider for SqlServer")]
		private SqlPerformanceCounters()
			: base(".NET Data Provider for SqlServer", "Counters for Microsoft.Data.SqlClient")
		{
		}

		// Token: 0x0400073B RID: 1851
		private const string CategoryName = ".NET Data Provider for SqlServer";

		// Token: 0x0400073C RID: 1852
		private const string CategoryHelp = "Counters for Microsoft.Data.SqlClient";

		// Token: 0x0400073D RID: 1853
		public static readonly SqlPerformanceCounters SingletonInstance = new SqlPerformanceCounters();
	}
}
