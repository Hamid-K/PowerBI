using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x02000062 RID: 98
	internal static class LogonMethodExtensions
	{
		// Token: 0x06000307 RID: 775 RVA: 0x000149F8 File Offset: 0x00012BF8
		public static LogonType ToLogonType(this LogonMethod logonMethod)
		{
			LogonType logonType;
			if (logonMethod != LogonMethod.Network)
			{
				if (logonMethod != LogonMethod.Cleartext)
				{
					throw new ArgumentOutOfRangeException("logonMethod", (int)logonMethod, "Valid value should be between 2 and 3.");
				}
				logonType = LogonType.NetworkCleartext;
			}
			else
			{
				logonType = LogonType.Network;
			}
			return logonType;
		}
	}
}
