using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000A0 RID: 160
	internal sealed class SqlSequentialTextReader : TextReader
	{
		// Token: 0x06000CAD RID: 3245 RVA: 0x000267F0 File Offset: 0x000249F0
		internal SqlSequentialTextReader(SqlDataReader reader, int columnIndex, Encoding encoding)
		{
			this._reader = reader;
			this._columnIndex = columnIndex;
			this._encoding = encoding;
			this._decoder = encoding.GetDecoder();
			this._leftOverBytes = null;
			this._peekedChar = -1;
			this._currentTask = null;
			this._disposalTokenSource = new CancellationTokenSource();
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00026844 File Offset: 0x00024A44
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002684C File Offset: 0x00024A4C
		public override int Peek()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (!this.HasPeekedChar)
			{
				this._peekedChar = this.Read();
			}
			return this._peekedChar;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00026888 File Offset: 0x00024A88
		public override int Read()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			int num = -1;
			if (this.HasPeekedChar)
			{
				num = this._peekedChar;
				this._peekedChar = -1;
			}
			else
			{
				char[] array = new char[1];
				int num2 = this.InternalRead(array, 0, 1);
				if (num2 == 1)
				{
					num = (int)array[0];
				}
			}
			return num;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000268E8 File Offset: 0x00024AE8
		public override int Read(char[] buffer, int index, int count)
		{
			SqlSequentialTextReader.ValidateReadParameters(buffer, index, count);
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			int num = 0;
			int num2 = count;
			if (num2 > 0 && this.HasPeekedChar)
			{
				buffer[index + num] = (char)this._peekedChar;
				num++;
				num2--;
				this._peekedChar = -1;
			}
			return num + this.InternalRead(buffer, index + num, num2);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00026954 File Offset: 0x00024B54
		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			SqlSequentialTextReader.ValidateReadParameters(buffer, index, count);
			TaskCompletionSource<int> completion = new TaskCompletionSource<int>();
			if (this.IsClosed)
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
						bool flag = true;
						int charsRead = 0;
						int adjustedIndex = index;
						int charsNeeded = count;
						if (this.HasPeekedChar && charsNeeded > 0)
						{
							int peekedChar = this._peekedChar;
							if (peekedChar >= 0)
							{
								buffer[adjustedIndex] = (char)peekedChar;
								int num = adjustedIndex;
								adjustedIndex = num + 1;
								num = charsRead;
								charsRead = num + 1;
								num = charsNeeded;
								charsNeeded = num - 1;
								this._peekedChar = -1;
							}
						}
						int byteBufferUsed;
						byte[] byteBuffer = this.PrepareByteBuffer(charsNeeded, out byteBufferUsed);
						if (byteBufferUsed < byteBuffer.Length || byteBuffer.Length == 0)
						{
							SqlDataReader reader = this._reader;
							if (reader != null)
							{
								int num2;
								Task<int> bytesAsync = reader.GetBytesAsync(this._columnIndex, byteBuffer, byteBufferUsed, byteBuffer.Length - byteBufferUsed, -1, this._disposalTokenSource.Token, out num2);
								if (bytesAsync == null)
								{
									byteBufferUsed += num2;
								}
								else
								{
									flag = false;
									bytesAsync.ContinueWith(delegate(Task<int> t)
									{
										this._currentTask = null;
										if (t.Status == TaskStatus.RanToCompletion && !this.IsClosed)
										{
											try
											{
												int result = t.Result;
												byteBufferUsed += result;
												if (byteBufferUsed > 0)
												{
													charsRead += this.DecodeBytesToChars(byteBuffer, byteBufferUsed, buffer, adjustedIndex, charsNeeded);
												}
												completion.SetResult(charsRead);
												return;
											}
											catch (Exception ex2)
											{
												completion.SetException(ex2);
												return;
											}
										}
										if (this.IsClosed)
										{
											completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
											return;
										}
										if (t.Status == TaskStatus.Faulted)
										{
											if (t.Exception.InnerException is SqlException)
											{
												completion.SetException(ADP.ExceptionWithStackTrace(ADP.ErrorReadingFromStream(t.Exception.InnerException)));
												return;
											}
											completion.SetException(t.Exception.InnerException);
											return;
										}
										else
										{
											completion.SetCanceled();
										}
									}, TaskScheduler.Default);
								}
								if (flag && byteBufferUsed > 0)
								{
									charsRead += this.DecodeBytesToChars(byteBuffer, byteBufferUsed, buffer, adjustedIndex, charsNeeded);
								}
							}
							else
							{
								completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
							}
						}
						if (flag)
						{
							this._currentTask = null;
							if (this.IsClosed)
							{
								completion.SetCanceled();
							}
							else
							{
								completion.SetResult(charsRead);
							}
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

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00026BE8 File Offset: 0x00024DE8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetClosed();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00026BFC File Offset: 0x00024DFC
		internal void SetClosed()
		{
			this._disposalTokenSource.Cancel();
			this._reader = null;
			this._peekedChar = -1;
			Task currentTask = this._currentTask;
			if (currentTask != null)
			{
				((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00026C38 File Offset: 0x00024E38
		private int InternalRead(char[] buffer, int index, int count)
		{
			int num2;
			try
			{
				int num;
				byte[] array = this.PrepareByteBuffer(count, out num);
				num += this._reader.GetBytesInternalSequential(this._columnIndex, array, num, array.Length - num, null);
				if (num > 0)
				{
					num2 = this.DecodeBytesToChars(array, num, buffer, index, count);
				}
				else
				{
					num2 = 0;
				}
			}
			catch (SqlException ex)
			{
				throw ADP.ErrorReadingFromStream(ex);
			}
			return num2;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00026CA4 File Offset: 0x00024EA4
		private byte[] PrepareByteBuffer(int numberOfChars, out int byteBufferUsed)
		{
			byte[] array;
			if (numberOfChars == 0)
			{
				array = Array.Empty<byte>();
				byteBufferUsed = 0;
			}
			else
			{
				int maxByteCount = this._encoding.GetMaxByteCount(numberOfChars);
				if (this._leftOverBytes != null)
				{
					if (this._leftOverBytes.Length > maxByteCount)
					{
						array = this._leftOverBytes;
						byteBufferUsed = array.Length;
					}
					else
					{
						array = new byte[maxByteCount];
						Buffer.BlockCopy(this._leftOverBytes, 0, array, 0, this._leftOverBytes.Length);
						byteBufferUsed = this._leftOverBytes.Length;
					}
				}
				else
				{
					array = new byte[maxByteCount];
					byteBufferUsed = 0;
				}
			}
			return array;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00026D24 File Offset: 0x00024F24
		private int DecodeBytesToChars(byte[] inBuffer, int inBufferCount, char[] outBuffer, int outBufferOffset, int outBufferCount)
		{
			int num;
			int num2;
			bool flag;
			this._decoder.Convert(inBuffer, 0, inBufferCount, outBuffer, outBufferOffset, outBufferCount, false, out num, out num2, out flag);
			if (!flag && num < inBufferCount)
			{
				this._leftOverBytes = new byte[inBufferCount - num];
				Buffer.BlockCopy(inBuffer, num, this._leftOverBytes, 0, this._leftOverBytes.Length);
			}
			else
			{
				this._leftOverBytes = null;
			}
			return num2;
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00026D80 File Offset: 0x00024F80
		private bool IsClosed
		{
			get
			{
				return this._reader == null;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00026D8B File Offset: 0x00024F8B
		private bool HasPeekedChar
		{
			get
			{
				return this._peekedChar >= 0;
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00026D9C File Offset: 0x00024F9C
		internal static void ValidateReadParameters(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (index < 0)
			{
				throw ADP.ArgumentOutOfRange("index");
			}
			if (count < 0)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			try
			{
				if (checked(index + count) > buffer.Length)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}
			catch (OverflowException)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
		}

		// Token: 0x04000359 RID: 857
		private SqlDataReader _reader;

		// Token: 0x0400035A RID: 858
		private readonly int _columnIndex;

		// Token: 0x0400035B RID: 859
		private readonly Encoding _encoding;

		// Token: 0x0400035C RID: 860
		private readonly Decoder _decoder;

		// Token: 0x0400035D RID: 861
		private byte[] _leftOverBytes;

		// Token: 0x0400035E RID: 862
		private int _peekedChar;

		// Token: 0x0400035F RID: 863
		private Task _currentTask;

		// Token: 0x04000360 RID: 864
		private readonly CancellationTokenSource _disposalTokenSource;
	}
}
