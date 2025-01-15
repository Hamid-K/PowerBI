using System;
using Microsoft.AnalysisServices.AdomdClient.Security;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000029 RID: 41
	internal static class IdentityResolver
	{
		// Token: 0x06000280 RID: 640 RVA: 0x0000C470 File Offset: 0x0000A670
		public static UserContext Resolve(ConnectionInfo connInfo)
		{
			if (connInfo.RevertToProcessAccountForConnection)
			{
				return new ProcessAccountUserContext();
			}
			return new TransparentUserContext();
		}
	}
}
