using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000325 RID: 805
	internal interface ICacheUsagePublisher
	{
		// Token: 0x06001D22 RID: 7458
		void PublishDataTransfer(string cacheName, long size, DataTransferDirectionType directionType, ClientLocationType? categoryType, string remoteEndpointIPAddress, int remoteEndpointPort, DateTime startTime, DateTime endTime);

		// Token: 0x06001D23 RID: 7459
		ClientLocationType? GetClientLocationForIP(string ipAddress, int port);
	}
}
