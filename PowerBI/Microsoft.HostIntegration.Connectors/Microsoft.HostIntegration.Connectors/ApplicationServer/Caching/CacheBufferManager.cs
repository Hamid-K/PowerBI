using System;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020003A9 RID: 937
	internal class CacheBufferManager : IBufferManager
	{
		// Token: 0x0600214D RID: 8525 RVA: 0x00066DA0 File Offset: 0x00064FA0
		public CacheBufferManager(long size, int maxMsgSize)
		{
			this._maxMessageSize = maxMsgSize;
			this._maxBufferPoolSize = size;
			this._buffers = BufferManager.CreateBufferManager(this._maxBufferPoolSize, this._maxMessageSize);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00066DD0 File Offset: 0x00064FD0
		public byte[] TakeBuffer(int size)
		{
			return this._buffers.TakeBuffer(size);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x00066DEB File Offset: 0x00064FEB
		public void ReleaseBuffer(byte[] buffer)
		{
			this._buffers.ReturnBuffer(buffer);
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x00066DF9 File Offset: 0x00064FF9
		public int MaxMessageSize
		{
			get
			{
				return this._maxMessageSize;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x00066E01 File Offset: 0x00065001
		public long MaxBufferPoolSize
		{
			get
			{
				return this._maxBufferPoolSize;
			}
		}

		// Token: 0x04001538 RID: 5432
		private readonly BufferManager _buffers;

		// Token: 0x04001539 RID: 5433
		private long _bufferAllocated;

		// Token: 0x0400153A RID: 5434
		private readonly int _maxMessageSize;

		// Token: 0x0400153B RID: 5435
		private readonly long _maxBufferPoolSize;
	}
}
