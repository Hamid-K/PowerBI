using System;
using System.ServiceModel.Security;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002C2 RID: 706
	internal static class SecurityUtil
	{
		// Token: 0x06001A26 RID: 6694 RVA: 0x0004F14D File Offset: 0x0004D34D
		internal static void ThrowSecurityAccessDeniedException(bool isClientToServer, string identity, Exception innerException)
		{
			if (isClientToServer)
			{
				EventLogProvider.EventWriteClientSecurityAuthorizationFailed("AppFabricCachingService.ServerChannel.Security", identity);
			}
			else
			{
				EventLogProvider.EventWriteServerSecurityAuthorizationFailed("AppFabricCachingService.ServerChannel.Security", identity);
			}
			throw new SecurityAccessDeniedException(null, innerException);
		}
	}
}
