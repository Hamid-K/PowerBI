using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001D9 RID: 473
	public static class DiagnosticsAlert
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x0002B248 File Offset: 0x00029448
		public static void Initialize()
		{
			DiagnosticsAlert.s_asyncSendWTSMessage = new DiagnosticsAlert.AsyncSendWTSMessage(DiagnosticsAlert.WTSSendMessage);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002B25C File Offset: 0x0002945C
		public static DiagnosticsAlert.WTSResponse SendMessageToAllInteractiveSessions(string displayMessage, string title, int messageBoxType, int timeoutMsec)
		{
			IList<int> activeSessions = DiagnosticsAlert.GetActiveSessions();
			AutoResetEvent[] array = new AutoResetEvent[activeSessions.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new AutoResetEvent(false);
			}
			int num = 0;
			IList<DiagnosticsAlert.WTSResponse> list = new List<DiagnosticsAlert.WTSResponse> { DiagnosticsAlert.WTSResponse.IDTIMEOUT };
			foreach (int num2 in activeSessions)
			{
				DiagnosticsAlert.SendMessageState sendMessageState = new DiagnosticsAlert.SendMessageState
				{
					ResponseArrivedEvent = array[num++],
					AggregatedResponse = list
				};
				DiagnosticsAlert.s_asyncSendWTSMessage.BeginInvoke(num2, displayMessage, title, messageBoxType, timeoutMsec, delegate(IAsyncResult ar)
				{
					DiagnosticsAlert.WTSResponse wtsresponse = DiagnosticsAlert.s_asyncSendWTSMessage.EndInvoke(ar);
					DiagnosticsAlert.SendMessageState sendMessageState2 = (DiagnosticsAlert.SendMessageState)ar.AsyncState;
					if (wtsresponse == DiagnosticsAlert.WTSResponse.IDOK || wtsresponse - DiagnosticsAlert.WTSResponse.IDABORT <= 2)
					{
						IList<DiagnosticsAlert.WTSResponse> aggregatedResponse = sendMessageState2.AggregatedResponse;
						lock (aggregatedResponse)
						{
							sendMessageState2.AggregatedResponse[0] = wtsresponse;
						}
						try
						{
							sendMessageState2.ResponseArrivedEvent.Set();
						}
						catch (ObjectDisposedException)
						{
						}
					}
				}, sendMessageState);
			}
			WaitHandle[] array2 = array;
			WaitHandle.WaitAny(array2, timeoutMsec, false);
			AutoResetEvent[] array3 = array;
			for (int j = 0; j < array3.Length; j++)
			{
				array3[j].Close();
			}
			return list[0];
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002B368 File Offset: 0x00029568
		private static DiagnosticsAlert.WTSResponse WTSSendMessage(int sessionId, string message, string title, int messageBoxType, int timeoutMsec)
		{
			IntPtr zero = IntPtr.Zero;
			int length = title.Length;
			int length2 = message.Length;
			bool flag = true;
			DiagnosticsAlert.WTSResponse wtsresponse;
			if (!DiagnosticsAlert.NativeMethods.WTSSendMessage(zero, sessionId, title, length, message, length2, messageBoxType, timeoutMsec, out wtsresponse, flag))
			{
				return DiagnosticsAlert.WTSResponse.IDTIMEOUT;
			}
			return wtsresponse;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002B3A4 File Offset: 0x000295A4
		private static IList<int> GetActiveSessions()
		{
			IntPtr zero = IntPtr.Zero;
			List<int> list = new List<int>();
			IntPtr zero2 = IntPtr.Zero;
			int num = 0;
			bool flag = DiagnosticsAlert.NativeMethods.WTSEnumerateSessions(zero, 0, 1, ref zero2, ref num);
			int num2 = Marshal.SizeOf(typeof(DiagnosticsAlert.WTSSessionInfo));
			int num3 = (int)zero2;
			if (flag)
			{
				try
				{
					for (int i = 0; i < num; i++)
					{
						DiagnosticsAlert.WTSSessionInfo wtssessionInfo = (DiagnosticsAlert.WTSSessionInfo)Marshal.PtrToStructure((IntPtr)num3, typeof(DiagnosticsAlert.WTSSessionInfo));
						num3 += num2;
						if (wtssessionInfo.State == DiagnosticsAlert.WTSConnectState.Active)
						{
							list.Add(wtssessionInfo.SessionID);
						}
					}
				}
				finally
				{
					DiagnosticsAlert.NativeMethods.WTSFreeMemory(zero2);
				}
			}
			return list;
		}

		// Token: 0x040004BA RID: 1210
		private static DiagnosticsAlert.AsyncSendWTSMessage s_asyncSendWTSMessage;

		// Token: 0x040004BB RID: 1211
		public const int MessageBoxOk = 0;

		// Token: 0x040004BC RID: 1212
		public const int MessageBoxAbortRetryIgnore = 2;

		// Token: 0x0200068B RID: 1675
		// (Invoke) Token: 0x06002DDF RID: 11743
		private delegate DiagnosticsAlert.WTSResponse AsyncSendWTSMessage(int sessionId, string message, string title, int messageBoxType, int timeoutMsec);

		// Token: 0x0200068C RID: 1676
		private class SendMessageState
		{
			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x000A16A2 File Offset: 0x0009F8A2
			// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x000A16AA File Offset: 0x0009F8AA
			public AutoResetEvent ResponseArrivedEvent { get; set; }

			// Token: 0x17000732 RID: 1842
			// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x000A16B3 File Offset: 0x0009F8B3
			// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x000A16BB File Offset: 0x0009F8BB
			public IList<DiagnosticsAlert.WTSResponse> AggregatedResponse { get; set; }
		}

		// Token: 0x0200068D RID: 1677
		private struct WTSSessionInfo
		{
			// Token: 0x0400127C RID: 4732
			public int SessionID;

			// Token: 0x0400127D RID: 4733
			[MarshalAs(UnmanagedType.LPStr)]
			public string pWinStationName;

			// Token: 0x0400127E RID: 4734
			public DiagnosticsAlert.WTSConnectState State;
		}

		// Token: 0x0200068E RID: 1678
		private enum WTSConnectState
		{
			// Token: 0x04001280 RID: 4736
			Active,
			// Token: 0x04001281 RID: 4737
			Connected,
			// Token: 0x04001282 RID: 4738
			ConnectQuery,
			// Token: 0x04001283 RID: 4739
			Shadow,
			// Token: 0x04001284 RID: 4740
			Disconnected,
			// Token: 0x04001285 RID: 4741
			Idle,
			// Token: 0x04001286 RID: 4742
			Listen,
			// Token: 0x04001287 RID: 4743
			Reset,
			// Token: 0x04001288 RID: 4744
			Down,
			// Token: 0x04001289 RID: 4745
			Init
		}

		// Token: 0x0200068F RID: 1679
		public enum WTSResponse
		{
			// Token: 0x0400128B RID: 4747
			IDABORT = 3,
			// Token: 0x0400128C RID: 4748
			IDCANCEL = 2,
			// Token: 0x0400128D RID: 4749
			IDIGNORE = 5,
			// Token: 0x0400128E RID: 4750
			IDNO = 7,
			// Token: 0x0400128F RID: 4751
			IDOK = 1,
			// Token: 0x04001290 RID: 4752
			IDRETRY = 4,
			// Token: 0x04001291 RID: 4753
			IDYES = 6,
			// Token: 0x04001292 RID: 4754
			IDASYNC = 32001,
			// Token: 0x04001293 RID: 4755
			IDTIMEOUT = 32000
		}

		// Token: 0x02000690 RID: 1680
		private static class NativeMethods
		{
			// Token: 0x06002DE7 RID: 11751
			[DllImport("wtsapi32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool WTSEnumerateSessions(IntPtr hServer, [MarshalAs(UnmanagedType.U4)] int Reserved, [MarshalAs(UnmanagedType.U4)] int Version, ref IntPtr ppSessionInfo, [MarshalAs(UnmanagedType.U4)] ref int pCount);

			// Token: 0x06002DE8 RID: 11752
			[DllImport("wtsapi32.dll")]
			public static extern void WTSFreeMemory(IntPtr pMemory);

			// Token: 0x06002DE9 RID: 11753
			[DllImport("wtsapi32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool WTSSendMessage(IntPtr hServer, [MarshalAs(UnmanagedType.I4)] int SessionId, string pTitle, [MarshalAs(UnmanagedType.U4)] int TitleLength, string pMessage, [MarshalAs(UnmanagedType.U4)] int MessageLength, [MarshalAs(UnmanagedType.U4)] int Style, [MarshalAs(UnmanagedType.U4)] int Timeout, [MarshalAs(UnmanagedType.U4)] out DiagnosticsAlert.WTSResponse pResponse, bool bWait);
		}
	}
}
