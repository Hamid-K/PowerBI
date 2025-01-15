using System;
using System.Net;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200001A RID: 26
	internal static class WebUtil
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002A24 File Offset: 0x00000C24
		internal static bool IsWellKnownLocalServer(string server)
		{
			return string.Compare(server, "localhost", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(server, "(local)", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(server, ".", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(server, Environment.MachineName, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(server, "127.0.0.1", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(server, "::1", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(server, "[::1]", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002AA4 File Offset: 0x00000CA4
		internal static bool IsSameServer(IPAddress[] server1IPAddresses, IPAddress[] server2IPAddresses)
		{
			foreach (IPAddress ipaddress in server1IPAddresses)
			{
				foreach (IPAddress ipaddress2 in server2IPAddresses)
				{
					if (ipaddress.Equals(ipaddress2))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002AEC File Offset: 0x00000CEC
		internal static string NormalizeWellKnownLocalServerName(string server)
		{
			if (!WebUtil.IsWellKnownLocalServer(server))
			{
				return server;
			}
			return "localhost";
		}

		// Token: 0x0400005A RID: 90
		internal const string NormalizedLocalServerName = "localhost";
	}
}
