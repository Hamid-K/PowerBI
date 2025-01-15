using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002DB RID: 731
	internal class AcquireTokenWithDeviceCodeParameters : IAcquireTokenParameters
	{
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00057485 File Offset: 0x00055685
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x0005748D File Offset: 0x0005568D
		public Func<DeviceCodeResult, Task> DeviceCodeResultCallback { get; set; }

		// Token: 0x06001B33 RID: 6963 RVA: 0x00057496 File Offset: 0x00055696
		public void LogParameters(ILoggerAdapter logger)
		{
		}
	}
}
