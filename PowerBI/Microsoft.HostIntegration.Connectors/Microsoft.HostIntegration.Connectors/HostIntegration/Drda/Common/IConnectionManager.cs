using System;
using System.Net;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000810 RID: 2064
	public interface IConnectionManager
	{
		// Token: 0x06004133 RID: 16691
		void Initialize(IConfiguration config, IPEndPoint ipEndPoint, bool usingWCFService);

		// Token: 0x06004134 RID: 16692
		void Destroy();
	}
}
