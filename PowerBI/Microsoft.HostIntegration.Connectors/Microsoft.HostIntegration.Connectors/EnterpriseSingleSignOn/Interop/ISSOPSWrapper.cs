using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004BA RID: 1210
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0CDBE0A1-24E8-421D-B983-D71CC4F74DD2")]
	[CoClass(typeof(SSOPSHelper))]
	[ComImport]
	public interface ISSOPSWrapper
	{
		// Token: 0x06002960 RID: 10592
		void InitializeAdapter(string adapterName, int adapterFlags, out long notifyEvent, out Guid trackingIdOut);

		// Token: 0x06002961 RID: 10593
		void SendNotification(int notificationType, int notificationFlags, Guid trackingIdIn, string externalAccount, string newExternalPassword, string oldExternalPassword, long timestamp, long errorCode, string errorMessage, out Guid trackingIdOut);

		// Token: 0x06002962 RID: 10594
		void ReceiveNotification(int notificationFlagsIn, Guid trackingIdIn, out int notificationType, out int notificationFlagsOut, out string externalAccount, out string newExternalPassword, out string oldExternalPassword, out long timestamp, out long errorCode, out string errorMessage, out Array adapters, out string adapterName, out Guid trackingIdOut);

		// Token: 0x06002963 RID: 10595
		void ShutdownAdapter(out Guid trackingId);
	}
}
