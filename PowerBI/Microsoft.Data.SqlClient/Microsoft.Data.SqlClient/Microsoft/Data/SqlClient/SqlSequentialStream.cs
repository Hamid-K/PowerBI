using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200009A RID: 154
	internal sealed class SqlSequentialStream : Stream
	{
		// Token: 0x06000C5F RID: 3167 RVA: 0x000254E8 File Offset: 0x000236E8
		internal SqlSequentialStream(SqlDataReader reader, int columnIndex)
		{
			this._reader = reader;
			this._columnIndex = columnIndex;
			this._currentTask = null;
			this._disposalTokenSource = new CancellationTokenSource();
			if (reader.Command != null && reader.Command.CommandTimeout != 0)
			{
				this._readTimeout = (int)Math.Min((long)reader.Command.CommandTimeout * 1000L, 2147483647L);
				return;
			}
			this._readTimeout = -1;
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0002555D File Offset: 0x0002375D
		public override bool CanRead
		{
			get
			{
				return this._reader != null && !this._reader.IsClosed;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0001996E File Offset: 0x00017B6E
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void Flush()
		{
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x00025577 File Offset: 0x00023777
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00025577 File Offset: 0x00023777
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00025577 File Offset: 0x00023777
		public override long Position
		{
			get
			{
				throw ADP.NotSupported();
			}
			set
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0002557E File Offset: 0x0002377E
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x00025586 File Offset: 0x00023786
		public override int ReadTimeout
		{
			get
			{
				return this._readTimeout;
			}
			set
			{
				if (value > 0 || value == -1)
				{
					this._readTimeout = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("value");
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x000255A2 File Offset: 0x000237A2
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x000255AC File Offset: 0x000237AC
		public override int Read(byte[] buffer, int offset, int count)
		{
			SqlSequentialStream.ValidateReadParameters(buffer, offset, count);
			if (!this.CanRead)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			int bytesInternalSequential;
			try
			{
				bytesInternalSequential = this._reader.GetBytesInternalSequential(this._columnIndex, buffer, offset, count, new long?((long)this._readTimeout));
			}
			catch (SqlException ex)
			{
				throw ADP.ErrorReadingFromStream(ex);
			}
			return bytesInternalSequential;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002561C File Offset: 0x0002381C
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			SqlSequentialStream.ValidateReadParameters(buffer, offset, count);
			TaskCompletionSource<int> completion = new TaskCompletionSource<int>();
			if (!this.CanRead)
			{
				completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
			}
			else
			{
				try
				{
					Task task = Interlocked.CompareExchange<Task>(ref this._currentTask, completion.Task, null);
					if (task != null)
					{
						completion.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					}
					else
					{
						CancellationTokenSource combinedTokenSource;
						if (!cancellationToken.CanBeCanceled)
						{
							combinedTokenSource = this._disposalTokenSource;
						}
						else
						{
							combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this._disposalTokenSource.Token);
						}
						int num = 0;
						Task<int> task2 = null;
						SqlDataReader reader = this._reader;
						if (reader != null && !cancellationToken.IsCancellationRequested && !this._disposalTokenSource.Token.IsCancellationRequested)
						{
							task2 = reader.GetBytesAsync(this._columnIndex, buffer, offset, count, this._readTimeout, combinedTokenSource.Token, out num);
						}
						if (task2 == null)
						{
							this._currentTask = null;
							if (cancellationToken.IsCancellationRequested)
							{
								completion.SetCanceled();
							}
							else if (!this.CanRead)
							{
								completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
							}
							else
							{
								completion.SetResult(num);
							}
							if (combinedTokenSource != this._disposalTokenSource)
							{
								combinedTokenSource.Dispose();
							}
						}
						else
						{
							task2.ContinueWith(delegate(Task<int> t)
							{
								this._currentTask = null;
								if (t.Status == TaskStatus.RanToCompletion && this.CanRead)
								{
									completion.SetResult(t.Result);
								}
								else if (t.Status == TaskStatus.Faulted)
								{
									if (t.Exception.InnerException is SqlException)
									{
										completion.SetException(ADP.ExceptionWithStackTrace(ADP.ErrorReadingFromStream(t.Exception.InnerException)));
									}
									else
									{
										completion.SetException(t.Exception.InnerException);
									}
								}
								else if (!this.CanRead)
								{
									completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
								}
								else
								{
									completion.SetCanceled();
								}
								if (combinedTokenSource != this._disposalTokenSource)
								{
									combinedTokenSource.Dispose();
								}
							}, TaskScheduler.Default);
						}
					}
				}
				catch (Exception ex)
				{
					completion.TrySetException(ex);
					Interlocked.CompareExchange<Task>(ref this._currentTask, null, completion.Task);
					throw;
				}
			}
			return completion.Task;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00025577 File Offset: 0x00023777
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00025577 File Offset: 0x00023777
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00025577 File Offset: 0x00023777
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x000257F8 File Offset: 0x000239F8
		internal void SetClosed()
		{
			this._disposalTokenSource.Cancel();
			this._reader = null;
			Task currentTask = this._currentTask;
			if (currentTask != null)
			{
				((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002582D File Offset: 0x00023A2D
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetClosed();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00025840 File Offset: 0x00023A40
		internal static void ValidateReadParameters(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (offset < 0)
			{
				throw ADP.ArgumentOutOfRange("offset");
			}
			if (count < 0)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			try
			{
				if (checked(offset + count) > buffer.Length)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}
			catch (OverflowException)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x000258A4 File Offset: 0x00023AA4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanRead)
			{
				throw ADP.ObjectDisposed(this);
			}
			Task task = this.ReadAsync(buffer, offset, count, CancellationToken.None);
			if (callback != null)
			{
				task.ContinueWith(delegate(Task t)
				{
					callback(t);
				}, TaskScheduler.Default);
			}
			return task;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00025900 File Offset: 0x00023B00
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw ADP.ArgumentNull("asyncResult");
			}
			Task<int> task = (Task<int>)asyncResult;
			try
			{
				task.Wait();
			}
			catch (AggregateException ex)
			{
				throw ex.InnerException;
			}
			return task.Result;
		}

		// Token: 0x04000327 RID: 807
		private SqlDataReader _reader;

		// Token: 0x04000328 RID: 808
		private readonly int _columnIndex;

		// Token: 0x04000329 RID: 809
		private Task _currentTask;

		// Token: 0x0400032A RID: 810
		private int _readTimeout;

		// Token: 0x0400032B RID: 811
		private readonly CancellationTokenSource _disposalTokenSource;
	}
}
