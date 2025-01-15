using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200018F RID: 399
	internal abstract class SocketTransportChannel
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0002C1CC File Offset: 0x0002A3CC
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x0002C1D3 File Offset: 0x0002A3D3
		internal static IBufferManager BufferManager { get; set; }

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002C1DC File Offset: 0x0002A3DC
		protected SocketTransportChannel(long bufferSize, int maxMessageSize)
		{
			lock (SocketTransportChannel._staticLock)
			{
				if (SocketTransportChannel.BufferManager == null)
				{
					long num = bufferSize;
					if (num == -1L)
					{
						num = 268435456L;
					}
					int num2 = maxMessageSize;
					if (num2 == -1)
					{
						num2 = 8388608;
					}
					SocketTransportChannel.BufferManager = new CacheBufferManager(num, num2);
				}
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00003CAB File Offset: 0x00001EAB
		public virtual void RegisterReceiveCallback(string action, OnMessageReceived callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002C248 File Offset: 0x0002A448
		public virtual void RegisterReceiveCallback(MessageType messageType, OnMessageReceived callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this._receiveCallback = callback;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void UnregisterReceiveCallback(string action, OnMessageReceived callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void UnregisterReceiveCallback(MessageType messageType, OnMessageReceived callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void UnregisterReceiveCallback(string action)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000916 RID: 2326
		protected string _logSource;

		// Token: 0x04000917 RID: 2327
		protected OnMessageReceived _receiveCallback;

		// Token: 0x04000918 RID: 2328
		private static object _staticLock = new object();
	}
}
