using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.ServiceModel;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000DD RID: 221
	[NullableContext(1)]
	[Nullable(0)]
	public static class NetworkExceptionsList
	{
		// Token: 0x060010DF RID: 4319 RVA: 0x000464BA File Offset: 0x000446BA
		public static bool IsNetworkException(Exception ex)
		{
			if (NetworkExceptionsList.exceptionDictionary == null)
			{
				NetworkExceptionsList.Initialize();
			}
			return ex != null && (NetworkExceptionsList.InternalCheck(ex) || NetworkExceptionsList.InternalCheck(ex.GetBaseException()));
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x000464E4 File Offset: 0x000446E4
		private static void Initialize()
		{
			NetworkExceptionsList.exceptionDictionary = new ConcurrentDictionary<string, byte>();
			byte b = 0;
			foreach (string text in NetworkExceptionsList.exceptionList)
			{
				NetworkExceptionsList.exceptionDictionary.TryAdd(text, b);
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00046522 File Offset: 0x00044722
		private static bool InternalCheck(Exception ex)
		{
			return ex is CommunicationException || NetworkExceptionsList.exceptionDictionary.ContainsKey(ex.GetType().ToString());
		}

		// Token: 0x04000366 RID: 870
		private static ConcurrentDictionary<string, byte> exceptionDictionary;

		// Token: 0x04000367 RID: 871
		private static string[] exceptionList = new string[] { "System.TimeoutException", "System.UnauthorizedAccessException", "System.Net.WebException", "System.Net.Sockets.SocketException", "System.Net.WebSockets.WebSocketException" };
	}
}
