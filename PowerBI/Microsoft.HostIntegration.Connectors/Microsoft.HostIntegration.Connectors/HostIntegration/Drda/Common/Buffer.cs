using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.PerformanceCounters;
using Microsoft.HostIntegration.PerformanceCounters.Drda;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000796 RID: 1942
	public class Buffer
	{
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06003E9E RID: 16030 RVA: 0x000D1D9B File Offset: 0x000CFF9B
		// (set) Token: 0x06003E9F RID: 16031 RVA: 0x000D1DA3 File Offset: 0x000CFFA3
		internal bool IsMemoryBuffer { get; set; }

		// Token: 0x06003EA0 RID: 16032 RVA: 0x000D1DAC File Offset: 0x000CFFAC
		public Buffer(Stream stream, CommonDrdaPerformanceCountersContainer perfContainer, object tracePoint)
		{
			this._inputStream = stream;
			this._buffer = ByteArrayPool.Get();
			this._tracePoint = tracePoint;
			this.IsMemoryBuffer = false;
			if (perfContainer != null)
			{
				this.CreatePerformanceCounters(perfContainer);
			}
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x000D1DE0 File Offset: 0x000CFFE0
		private void CreatePerformanceCounters(CommonDrdaPerformanceCountersContainer perfContainer)
		{
			try
			{
				object obj = Buffer.perfmonLockObject;
				lock (obj)
				{
					if (Buffer.CommonPerformanceContainer == null)
					{
						Buffer.CommonPerformanceContainer = perfContainer;
						Buffer.bytesReceivedPerSecond = Buffer.CommonPerformanceContainer.GetPerformanceCounter(CommonDrdaPerformanceCounter.BytesReceivedPerSecond) as PerSecondCounter;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x000D1E4C File Offset: 0x000D004C
		public void Dispose()
		{
			if (this._inputStream != null)
			{
				this._inputStream.Close();
				this._inputStream = null;
			}
			this.SetMemoryBuffer(null);
			ByteArrayPool.Put(this._buffer);
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06003EA3 RID: 16035 RVA: 0x000D1E7A File Offset: 0x000D007A
		public Stream InputStream
		{
			get
			{
				return this._inputStream;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		public byte this[int index]
		{
			get
			{
				return this._buffer[index];
			}
			set
			{
				this._buffer[index] = value;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06003EA6 RID: 16038 RVA: 0x000D1E97 File Offset: 0x000D0097
		public int Length
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x000D1EA1 File Offset: 0x000D00A1
		// (set) Token: 0x06003EA8 RID: 16040 RVA: 0x000D1EA9 File Offset: 0x000D00A9
		public int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x000D1EB2 File Offset: 0x000D00B2
		// (set) Token: 0x06003EAA RID: 16042 RVA: 0x000D1EBA File Offset: 0x000D00BA
		public int Count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06003EAB RID: 16043 RVA: 0x000D1EC3 File Offset: 0x000D00C3
		public byte[] Bytes
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x000D1ECB File Offset: 0x000D00CB
		public void CopyTo(byte[] destinationArray, int destinationIndex, int length)
		{
			this.CopyTo(destinationArray, destinationIndex, length, 0);
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x000D1ED7 File Offset: 0x000D00D7
		public void CopyTo(byte[] destinationArray, int destinationIndex, int length, int sourceIndex)
		{
			Buffer.BlockCopy(this._buffer, sourceIndex, destinationArray, destinationIndex, length);
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x000D1EE9 File Offset: 0x000D00E9
		public void CopyFrom(byte[] sourceArray, int sourceIndex, int length)
		{
			this.CopyFrom(sourceArray, sourceIndex, length, 0);
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x000D1EF5 File Offset: 0x000D00F5
		public void CopyFrom(byte[] sourceArray, int sourceIndex, int length, int destinationIndex)
		{
			Buffer.BlockCopy(sourceArray, sourceIndex, this._buffer, destinationIndex, length);
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x000D1F08 File Offset: 0x000D0108
		public void EnsureDataAvailable(int required)
		{
			this.EnsureDataAvailableAsync(required, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x000D1F30 File Offset: 0x000D0130
		public async Task EnsureDataAvailableAsync(int required, bool isAsync, CancellationToken cancellationToken)
		{
			int num = this._count - this._position;
			if (num < required)
			{
				await this.FillAsync(required - num, isAsync, cancellationToken);
			}
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x000D1F8D File Offset: 0x000D018D
		public bool IsDataAvailable(int required)
		{
			return this._count - this._position > 0;
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x000D1FA0 File Offset: 0x000D01A0
		internal void SetMemoryBuffer(byte[] memoryBuffer)
		{
			if (memoryBuffer != null)
			{
				if (!this.IsMemoryBuffer)
				{
					this._originalBuffer = this._buffer;
					this._originalCount = this._count;
					this._originalPosition = this._position;
					this._buffer = memoryBuffer;
					this._count = memoryBuffer.Length;
					this._position = 0;
					this.IsMemoryBuffer = true;
					return;
				}
				if (memoryBuffer != this._buffer)
				{
					this._buffer = memoryBuffer;
					this._count = memoryBuffer.Length;
					this._position = 0;
					Logger.Warning(this._tracePoint, 0, "Memory buffer overwrite.", Array.Empty<object>());
					return;
				}
			}
			else if (this.IsMemoryBuffer)
			{
				this._buffer = this._originalBuffer;
				this._count = this._originalCount;
				this._position = this._originalPosition;
				this._originalBuffer = null;
				this._originalCount = 0;
				this._originalPosition = 0;
				this.IsMemoryBuffer = false;
			}
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x000D2080 File Offset: 0x000D0280
		private async Task FillAsync(int required, bool isAsync, CancellationToken cancellationToken)
		{
			this.EnsureSpace(required);
			int totalRead = 0;
			int read = 0;
			while (this._inputStream != null)
			{
				try
				{
					int num = this._buffer.Length - this._count;
					if (isAsync)
					{
						int num2 = await this._inputStream.ReadAsync(this._buffer, this._count, num, cancellationToken);
						read = num2;
					}
					else
					{
						read = this._inputStream.Read(this._buffer, this._count, num);
					}
					if (Buffer.bytesReceivedPerSecond != null)
					{
						Buffer.bytesReceivedPerSecond.IncrementBy(read);
					}
				}
				catch (IOException ex)
				{
					Logger.Error(this._tracePoint, 0, "Buffer::Fill " + ex.Message + Environment.NewLine + ex.StackTrace, Array.Empty<object>());
					DrdaException.CommunicationFailure(ex);
				}
				if (read != 0)
				{
					this._count += read;
					totalRead += read;
				}
				if (totalRead >= required || read == 0)
				{
					if (read == 0 && totalRead < required)
					{
						DrdaException.CommunicationFailure("The remote connection was closed");
					}
					return;
				}
			}
			throw new NullReferenceException("InputStream is not initialized");
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x000D20E0 File Offset: 0x000D02E0
		private void EnsureSpace(int required)
		{
			int num = this._buffer.Length - this._count + this._position;
			if (num < required)
			{
				int num2 = required - num + this._buffer.Length;
				int num3 = 2 * this._buffer.Length;
				byte[] array = new byte[(num2 < num3) ? num3 : num2];
				this.Shift(array);
				return;
			}
			if (this._position != 0)
			{
				this.Shift(this._buffer);
			}
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x000D214C File Offset: 0x000D034C
		private void Shift(byte[] newBuffer)
		{
			int num = this._count - this._position;
			Buffer.BlockCopy(this._buffer, this._position, newBuffer, 0, num);
			this._buffer = newBuffer;
			this._position = 0;
			this._count = num;
		}

		// Token: 0x04002529 RID: 9513
		private byte[] _buffer;

		// Token: 0x0400252A RID: 9514
		private int _position;

		// Token: 0x0400252B RID: 9515
		private int _count;

		// Token: 0x0400252C RID: 9516
		private Stream _inputStream;

		// Token: 0x0400252D RID: 9517
		private static object perfmonLockObject = new object();

		// Token: 0x0400252E RID: 9518
		private static CommonDrdaPerformanceCountersContainer CommonPerformanceContainer;

		// Token: 0x0400252F RID: 9519
		private static PerSecondCounter bytesReceivedPerSecond;

		// Token: 0x04002530 RID: 9520
		private object _tracePoint;

		// Token: 0x04002531 RID: 9521
		private byte[] _originalBuffer;

		// Token: 0x04002532 RID: 9522
		private int _originalPosition;

		// Token: 0x04002533 RID: 9523
		private int _originalCount;
	}
}
