using System;
using NLog.Internal;

namespace NLog.Common
{
	// Token: 0x020001BD RID: 445
	public class LogEventInfoBuffer
	{
		// Token: 0x060013F2 RID: 5106 RVA: 0x00036234 File Offset: 0x00034434
		public LogEventInfoBuffer(int size, bool growAsNeeded, int growLimit)
		{
			this._growAsNeeded = growAsNeeded;
			this._buffer = new AsyncLogEventInfo[size];
			this._growLimit = growLimit;
			this._getPointer = 0;
			this._putPointer = 0;
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0003626F File Offset: 0x0003446F
		public int Size
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0003627C File Offset: 0x0003447C
		internal int Count
		{
			get
			{
				object lockObject = this._lockObject;
				int count;
				lock (lockObject)
				{
					count = this._count;
				}
				return count;
			}
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x000362C0 File Offset: 0x000344C0
		public int Append(AsyncLogEventInfo eventInfo)
		{
			object lockObject = this._lockObject;
			int count;
			lock (lockObject)
			{
				if (this._count >= this._buffer.Length)
				{
					if (this._growAsNeeded && this._buffer.Length < this._growLimit)
					{
						int num = this._buffer.Length * 2;
						if (num >= this._growLimit)
						{
							num = this._growLimit;
						}
						InternalLogger.Trace<int, int>("Enlarging LogEventInfoBuffer from {0} to {1}", this._buffer.Length, num);
						AsyncLogEventInfo[] array = new AsyncLogEventInfo[num];
						Array.Copy(this._buffer, 0, array, 0, this._buffer.Length);
						this._buffer = array;
					}
					else
					{
						this._getPointer++;
					}
				}
				this._putPointer %= this._buffer.Length;
				this._buffer[this._putPointer] = eventInfo;
				this._putPointer++;
				this._count++;
				if (this._count >= this._buffer.Length)
				{
					this._count = this._buffer.Length;
				}
				count = this._count;
			}
			return count;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000363F0 File Offset: 0x000345F0
		public AsyncLogEventInfo[] GetEventsAndClear()
		{
			object lockObject = this._lockObject;
			AsyncLogEventInfo[] array;
			lock (lockObject)
			{
				int count = this._count;
				if (count == 0)
				{
					array = ArrayHelper.Empty<AsyncLogEventInfo>();
				}
				else
				{
					AsyncLogEventInfo[] array2 = new AsyncLogEventInfo[count];
					for (int i = 0; i < count; i++)
					{
						int num = (this._getPointer + i) % this._buffer.Length;
						AsyncLogEventInfo asyncLogEventInfo = this._buffer[num];
						this._buffer[num] = default(AsyncLogEventInfo);
						array2[i] = asyncLogEventInfo;
					}
					this._count = 0;
					this._getPointer = 0;
					this._putPointer = 0;
					array = array2;
				}
			}
			return array;
		}

		// Token: 0x04000558 RID: 1368
		private readonly object _lockObject = new object();

		// Token: 0x04000559 RID: 1369
		private readonly bool _growAsNeeded;

		// Token: 0x0400055A RID: 1370
		private readonly int _growLimit;

		// Token: 0x0400055B RID: 1371
		private AsyncLogEventInfo[] _buffer;

		// Token: 0x0400055C RID: 1372
		private int _getPointer;

		// Token: 0x0400055D RID: 1373
		private int _putPointer;

		// Token: 0x0400055E RID: 1374
		private int _count;
	}
}
