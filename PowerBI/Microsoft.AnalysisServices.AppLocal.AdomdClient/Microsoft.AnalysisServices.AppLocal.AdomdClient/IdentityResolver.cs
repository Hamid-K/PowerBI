using System;
using Microsoft.AnalysisServices.AdomdClient.Security;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000029 RID: 41
	internal static class IdentityResolver
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
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
