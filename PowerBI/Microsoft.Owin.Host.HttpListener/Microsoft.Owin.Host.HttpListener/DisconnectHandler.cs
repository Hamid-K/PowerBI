using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Owin.Host.HttpListener
{
	// Token: 0x02000004 RID: 4
	internal class DisconnectHandler
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020A4 File Offset: 0x000002A4
		internal DisconnectHandler(HttpListener listener, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> logger)
		{
			this._connectionCancellationTokens = new ConcurrentDictionary<ulong, DisconnectHandler.ConnectionCancellation>();
			this._listener = listener;
			this._logger = logger;
			FieldInfo requestQueueHandleField = typeof(HttpListener).GetField("m_RequestQueueHandle", BindingFlags.Instance | BindingFlags.NonPublic);
			this._connectionIdField = typeof(HttpListenerRequest).GetField("m_ConnectionId", BindingFlags.Instance | BindingFlags.NonPublic);
			if (requestQueueHandleField != null && requestQueueHandleField.FieldType == typeof(CriticalHandle))
			{
				this._requestQueueHandle = (CriticalHandle)requestQueueHandleField.GetValue(this._listener);
			}
			if (this._connectionIdField == null || this._requestQueueHandle == null)
			{
				LogHelper.LogInfo(this._logger, "Unable to resolve handles. Disconnect notifications will be ignored.");
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002160 File Offset: 0x00000360
		internal CancellationToken GetDisconnectToken(HttpListenerContext context)
		{
			if (this._connectionIdField == null || this._requestQueueHandle == null)
			{
				return CancellationToken.None;
			}
			ulong connectionId = (ulong)this._connectionIdField.GetValue(context.Request);
			DisconnectHandler.ConnectionCancellation cancellation = this.GetConnectionCancellation(connectionId);
			return cancellation.GetCancellationToken(this, connectionId);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B0 File Offset: 0x000003B0
		private DisconnectHandler.ConnectionCancellation GetConnectionCancellation(ulong connectionId)
		{
			DisconnectHandler.ConnectionCancellation cancellation;
			if (this._connectionCancellationTokens.TryGetValue(connectionId, out cancellation))
			{
				return cancellation;
			}
			return this.GetCreatedConnectionCancellation(connectionId);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021D8 File Offset: 0x000003D8
		private DisconnectHandler.ConnectionCancellation GetCreatedConnectionCancellation(ulong connectionId)
		{
			DisconnectHandler.ConnectionCancellation cancellation = new DisconnectHandler.ConnectionCancellation();
			return this._connectionCancellationTokens.GetOrAdd(connectionId, cancellation);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021F8 File Offset: 0x000003F8
		private unsafe CancellationToken CreateToken(ulong connectionId)
		{
			Overlapped overlapped = new Overlapped();
			CancellationTokenSource cts = new CancellationTokenSource();
			CancellationToken returnToken = cts.Token;
			NativeOverlapped* nativeOverlapped = overlapped.UnsafePack(delegate(uint errorCode, uint numBytes, NativeOverlapped* overlappedPtr)
			{
				Overlapped.Free(overlappedPtr);
				if (errorCode != 0U)
				{
					LogHelper.LogException(this._logger, "IOCompletionCallback", new Win32Exception((int)errorCode));
				}
				DisconnectHandler.ConnectionCancellation cancellation3;
				this._connectionCancellationTokens.TryRemove(connectionId, out cancellation3);
				bool success = ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(this.CancelToken), cts);
			}, null);
			uint hr = NativeMethods.HttpWaitForDisconnectEx(this._requestQueueHandle, connectionId, 0U, nativeOverlapped);
			if (hr != 997U && hr != 0U)
			{
				Overlapped.Free(nativeOverlapped);
				DisconnectHandler.ConnectionCancellation cancellation;
				this._connectionCancellationTokens.TryRemove(connectionId, out cancellation);
				LogHelper.LogException(this._logger, "HttpWaitForDisconnectEx", new Win32Exception((int)hr));
				cts.Cancel();
			}
			if (hr == 0U && DisconnectHandler.SkipIOCPCallbackOnSuccess)
			{
				Overlapped.Free(nativeOverlapped);
				DisconnectHandler.ConnectionCancellation cancellation2;
				this._connectionCancellationTokens.TryRemove(connectionId, out cancellation2);
				cts.Cancel();
			}
			return returnToken;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022DC File Offset: 0x000004DC
		private void CancelToken(object state)
		{
			CancellationTokenSource cts = (CancellationTokenSource)state;
			try
			{
				cts.Cancel();
			}
			catch (AggregateException age)
			{
				LogHelper.LogException(this._logger, "App errors on disconnect notification.", age);
			}
		}

		// Token: 0x0400002D RID: 45
		private static bool SkipIOCPCallbackOnSuccess = Environment.OSVersion.Version >= new Version(6, 2);

		// Token: 0x0400002E RID: 46
		private readonly ConcurrentDictionary<ulong, DisconnectHandler.ConnectionCancellation> _connectionCancellationTokens;

		// Token: 0x0400002F RID: 47
		private readonly HttpListener _listener;

		// Token: 0x04000030 RID: 48
		private readonly CriticalHandle _requestQueueHandle;

		// Token: 0x04000031 RID: 49
		private readonly FieldInfo _connectionIdField;

		// Token: 0x04000032 RID: 50
		private readonly Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> _logger;

		// Token: 0x02000017 RID: 23
		private class ConnectionCancellation
		{
			// Token: 0x06000136 RID: 310 RVA: 0x00006B4E File Offset: 0x00004D4E
			internal CancellationToken GetCancellationToken(DisconnectHandler disconnectHandler, ulong connectionId)
			{
				if (this._initialized)
				{
					return this._cancellationToken;
				}
				return this.InitializeCancellationToken(disconnectHandler, connectionId);
			}

			// Token: 0x06000137 RID: 311 RVA: 0x00006B6C File Offset: 0x00004D6C
			private CancellationToken InitializeCancellationToken(DisconnectHandler disconnectHandler, ulong connectionId)
			{
				object syncObject = this;
				return LazyInitializer.EnsureInitialized<CancellationToken>(ref this._cancellationToken, ref this._initialized, ref syncObject, () => disconnectHandler.CreateToken(connectionId));
			}

			// Token: 0x040000A0 RID: 160
			private volatile bool _initialized;

			// Token: 0x040000A1 RID: 161
			private CancellationToken _cancellationToken;
		}
	}
}
