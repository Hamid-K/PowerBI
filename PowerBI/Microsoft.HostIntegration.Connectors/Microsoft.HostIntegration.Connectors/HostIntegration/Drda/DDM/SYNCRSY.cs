using System;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008E7 RID: 2279
	public class SYNCRSY : SYNCRRD
	{
		// Token: 0x06004845 RID: 18501 RVA: 0x001073E2 File Offset: 0x001055E2
		public SYNCRSY(int sessionId, string resyncIpAddress, int resyncPort)
			: base(sessionId, resyncIpAddress, resyncPort)
		{
		}
	}
}
