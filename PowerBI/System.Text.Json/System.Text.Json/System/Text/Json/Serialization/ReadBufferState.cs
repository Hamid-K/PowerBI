using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000096 RID: 150
	[StructLayout(LayoutKind.Auto)]
	internal struct ReadBufferState : IDisposable
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x00027060 File Offset: 0x00025260
		public ReadBufferState(int initialBufferSize)
		{
			this._unsuccessfulReadCount = 0;
			this._buffer = ArrayPool<byte>.Shared.Rent(Math.Max(initialBufferSize, JsonConstants.Utf8Bom.Length));
			this._maxCount = (this._count = (int)(this._offset = 0));
			this._isFirstBlock = true;
			this._isFinalBlock = false;
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x000270BE File Offset: 0x000252BE
		public bool IsFinalBlock
		{
			get
			{
				return this._isFinalBlock;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x000270C6 File Offset: 0x000252C6
		public ReadOnlySpan<byte> Bytes
		{
			get
			{
				return this._buffer.AsSpan((int)this._offset, this._count);
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x000270E4 File Offset: 0x000252E4
		public readonly ValueTask<ReadBufferState> ReadFromStreamAsync(Stream utf8Json, CancellationToken cancellationToken, bool fillBuffer = true)
		{
			ReadBufferState.<ReadFromStreamAsync>d__13 <ReadFromStreamAsync>d__;
			<ReadFromStreamAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<ReadBufferState>.Create();
			<ReadFromStreamAsync>d__.<>4__this = this;
			<ReadFromStreamAsync>d__.utf8Json = utf8Json;
			<ReadFromStreamAsync>d__.cancellationToken = cancellationToken;
			<ReadFromStreamAsync>d__.fillBuffer = fillBuffer;
			<ReadFromStreamAsync>d__.<>1__state = -1;
			<ReadFromStreamAsync>d__.<>t__builder.Start<ReadBufferState.<ReadFromStreamAsync>d__13>(ref <ReadFromStreamAsync>d__);
			return <ReadFromStreamAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00027144 File Offset: 0x00025344
		public void ReadFromStream(Stream utf8Json)
		{
			for (;;)
			{
				int num = utf8Json.Read(this._buffer, this._count, this._buffer.Length - this._count);
				if (num == 0)
				{
					break;
				}
				this._count += num;
				if (this._count >= this._buffer.Length)
				{
					goto IL_004C;
				}
			}
			this._isFinalBlock = true;
			IL_004C:
			this.ProcessReadBytes();
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000271A4 File Offset: 0x000253A4
		public void AdvanceBuffer(int bytesConsumed)
		{
			this._unsuccessfulReadCount = ((bytesConsumed == 0) ? (this._unsuccessfulReadCount + 1) : 0);
			this._count -= bytesConsumed;
			if (!this._isFinalBlock)
			{
				if (this._count > this._buffer.Length / 2)
				{
					byte[] buffer = this._buffer;
					int maxCount = this._maxCount;
					byte[] array = ArrayPool<byte>.Shared.Rent((this._buffer.Length < 1073741823) ? (this._buffer.Length * 2) : int.MaxValue);
					Buffer.BlockCopy(buffer, (int)this._offset + bytesConsumed, array, 0, this._count);
					this._buffer = array;
					this._maxCount = this._count;
					new Span<byte>(buffer, 0, maxCount).Clear();
					ArrayPool<byte>.Shared.Return(buffer, false);
				}
				else if (this._count != 0)
				{
					Buffer.BlockCopy(this._buffer, (int)this._offset + bytesConsumed, this._buffer, 0, this._count);
				}
			}
			this._offset = 0;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000272A4 File Offset: 0x000254A4
		private void ProcessReadBytes()
		{
			if (this._count > this._maxCount)
			{
				this._maxCount = this._count;
			}
			if (this._isFirstBlock)
			{
				this._isFirstBlock = false;
				if (this._buffer.AsSpan(0, this._count).StartsWith(JsonConstants.Utf8Bom))
				{
					this._offset = (byte)JsonConstants.Utf8Bom.Length;
					this._count -= JsonConstants.Utf8Bom.Length;
				}
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00027328 File Offset: 0x00025528
		public void Dispose()
		{
			new Span<byte>(this._buffer, 0, this._maxCount).Clear();
			byte[] buffer = this._buffer;
			this._buffer = null;
			ArrayPool<byte>.Shared.Return(buffer, false);
		}

		// Token: 0x04000303 RID: 771
		private byte[] _buffer;

		// Token: 0x04000304 RID: 772
		private byte _offset;

		// Token: 0x04000305 RID: 773
		private int _count;

		// Token: 0x04000306 RID: 774
		private int _maxCount;

		// Token: 0x04000307 RID: 775
		private bool _isFirstBlock;

		// Token: 0x04000308 RID: 776
		private bool _isFinalBlock;

		// Token: 0x04000309 RID: 777
		private const int UnsuccessfulReadCountThreshold = 5;

		// Token: 0x0400030A RID: 778
		private int _unsuccessfulReadCount;
	}
}
