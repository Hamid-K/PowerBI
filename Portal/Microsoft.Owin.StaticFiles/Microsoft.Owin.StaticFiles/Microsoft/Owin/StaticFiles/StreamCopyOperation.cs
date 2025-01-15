using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x02000015 RID: 21
	internal class StreamCopyOperation
	{
		// Token: 0x06000066 RID: 102 RVA: 0x0000349B File Offset: 0x0000169B
		internal StreamCopyOperation(Stream source, Stream destination, long? bytesRemaining, CancellationToken cancel)
			: this(source, destination, bytesRemaining, 65536, cancel)
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000034AD File Offset: 0x000016AD
		internal StreamCopyOperation(Stream source, Stream destination, long? bytesRemaining, int bufferSize, CancellationToken cancel)
			: this(source, destination, bytesRemaining, new byte[bufferSize], cancel)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000034C4 File Offset: 0x000016C4
		internal StreamCopyOperation(Stream source, Stream destination, long? bytesRemaining, byte[] buffer, CancellationToken cancel)
		{
			this._source = source;
			this._destination = destination;
			this._bytesRemaining = bytesRemaining;
			this._cancel = cancel;
			this._buffer = buffer;
			this._tcs = new TaskCompletionSource<object>();
			this._readCallback = new AsyncCallback(this.ReadCallback);
			this._writeCallback = new AsyncCallback(this.WriteCallback);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000352B File Offset: 0x0000172B
		internal Task Start()
		{
			this.ReadNextSegment();
			return this._tcs.Task;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000353E File Offset: 0x0000173E
		private void Complete()
		{
			this._tcs.TrySetResult(null);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000354D File Offset: 0x0000174D
		private bool CheckCancelled()
		{
			if (this._cancel.IsCancellationRequested)
			{
				this._tcs.TrySetCanceled();
				return true;
			}
			return false;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000356B File Offset: 0x0000176B
		private void Fail(Exception ex)
		{
			this._tcs.TrySetException(ex);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000357C File Offset: 0x0000177C
		private void ReadNextSegment()
		{
			if (this._bytesRemaining != null && this._bytesRemaining.Value <= 0L)
			{
				this.Complete();
				return;
			}
			if (this.CheckCancelled())
			{
				return;
			}
			try
			{
				int readLength = this._buffer.Length;
				if (this._bytesRemaining != null)
				{
					readLength = (int)Math.Min(this._bytesRemaining.Value, (long)readLength);
				}
				IAsyncResult async = this._source.BeginRead(this._buffer, 0, readLength, this._readCallback, null);
				if (async.CompletedSynchronously)
				{
					int read = this._source.EndRead(async);
					this.WriteToOutputStream(read);
				}
			}
			catch (Exception ex)
			{
				this.Fail(ex);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003634 File Offset: 0x00001834
		private void ReadCallback(IAsyncResult async)
		{
			if (async.CompletedSynchronously)
			{
				return;
			}
			try
			{
				int read = this._source.EndRead(async);
				this.WriteToOutputStream(read);
			}
			catch (Exception ex)
			{
				this.Fail(ex);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000367C File Offset: 0x0000187C
		private void WriteToOutputStream(int count)
		{
			if (this._bytesRemaining != null)
			{
				this._bytesRemaining -= (long)count;
			}
			if (count == 0)
			{
				this.Complete();
				return;
			}
			if (this.CheckCancelled())
			{
				return;
			}
			try
			{
				IAsyncResult async = this._destination.BeginWrite(this._buffer, 0, count, this._writeCallback, null);
				if (async.CompletedSynchronously)
				{
					this._destination.EndWrite(async);
					this.ReadNextSegment();
				}
			}
			catch (Exception ex)
			{
				this.Fail(ex);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003730 File Offset: 0x00001930
		private void WriteCallback(IAsyncResult async)
		{
			if (async.CompletedSynchronously)
			{
				return;
			}
			try
			{
				this._destination.EndWrite(async);
				this.ReadNextSegment();
			}
			catch (Exception ex)
			{
				this.Fail(ex);
			}
		}

		// Token: 0x04000044 RID: 68
		private const int DefaultBufferSize = 65536;

		// Token: 0x04000045 RID: 69
		private readonly TaskCompletionSource<object> _tcs;

		// Token: 0x04000046 RID: 70
		private readonly Stream _source;

		// Token: 0x04000047 RID: 71
		private readonly Stream _destination;

		// Token: 0x04000048 RID: 72
		private readonly byte[] _buffer;

		// Token: 0x04000049 RID: 73
		private readonly AsyncCallback _readCallback;

		// Token: 0x0400004A RID: 74
		private readonly AsyncCallback _writeCallback;

		// Token: 0x0400004B RID: 75
		private long? _bytesRemaining;

		// Token: 0x0400004C RID: 76
		private CancellationToken _cancel;
	}
}
