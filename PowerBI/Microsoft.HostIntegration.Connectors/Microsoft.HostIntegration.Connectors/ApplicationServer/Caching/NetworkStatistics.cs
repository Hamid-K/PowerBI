using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000159 RID: 345
	internal static class NetworkStatistics
	{
		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002393B File Offset: 0x00021B3B
		internal static void OnConnectionRequestFailed()
		{
			NetworkStatistics.Stats[0] += 1L;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00023956 File Offset: 0x00021B56
		internal static void OnConnectionClosed()
		{
			NetworkStatistics.Stats[1] -= 1L;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00023971 File Offset: 0x00021B71
		internal static void OnChannelCreated()
		{
			NetworkStatistics.Stats[1] += 1L;
		}

		// Token: 0x04000759 RID: 1881
		internal static long[] Stats = new long[Enum.GetValues(typeof(NetworkStatistics.StatName)).Length];

		// Token: 0x0200015A RID: 346
		internal enum StatName
		{
			// Token: 0x0400075B RID: 1883
			ConnectionRequestFailed,
			// Token: 0x0400075C RID: 1884
			TotalActiveConnections
		}
	}
}
