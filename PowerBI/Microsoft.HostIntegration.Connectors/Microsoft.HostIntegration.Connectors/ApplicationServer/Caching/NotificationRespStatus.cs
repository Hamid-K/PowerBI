using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000262 RID: 610
	internal enum NotificationRespStatus : byte
	{
		// Token: 0x04000C3B RID: 3131
		Success,
		// Token: 0x04000C3C RID: 3132
		NotificationMissed,
		// Token: 0x04000C3D RID: 3133
		NotificationsNotSupported,
		// Token: 0x04000C3E RID: 3134
		DataLoss
	}
}
