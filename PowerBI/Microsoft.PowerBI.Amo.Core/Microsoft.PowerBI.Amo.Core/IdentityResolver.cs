using System;
using Microsoft.AnalysisServices.Security;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000041 RID: 65
	internal static class IdentityResolver
	{
		// Token: 0x06000319 RID: 793 RVA: 0x0000F634 File Offset: 0x0000D834
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
