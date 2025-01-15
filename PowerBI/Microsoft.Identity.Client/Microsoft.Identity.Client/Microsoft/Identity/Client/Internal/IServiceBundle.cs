using System;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.OAuth2.Throttling;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.TelemetryCore;
using Microsoft.Identity.Client.WsTrust;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x02000235 RID: 565
	internal interface IServiceBundle
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600170C RID: 5900
		ApplicationConfiguration Config { get; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600170D RID: 5901
		ILoggerAdapter ApplicationLogger { get; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600170E RID: 5902
		IHttpManager HttpManager { get; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600170F RID: 5903
		IInstanceDiscoveryManager InstanceDiscoveryManager { get; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001710 RID: 5904
		IPlatformProxy PlatformProxy { get; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001711 RID: 5905
		IWsTrustWebRequestManager WsTrustWebRequestManager { get; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001712 RID: 5906
		IDeviceAuthManager DeviceAuthManager { get; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001713 RID: 5907
		IThrottlingProvider ThrottlingManager { get; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001714 RID: 5908
		IHttpTelemetryManager HttpTelemetryManager { get; }

		// Token: 0x06001715 RID: 5909
		void SetPlatformProxyForTest(IPlatformProxy platformProxy);
	}
}
