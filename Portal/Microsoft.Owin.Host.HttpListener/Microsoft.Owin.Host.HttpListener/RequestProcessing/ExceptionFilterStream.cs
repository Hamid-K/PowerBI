using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.HttpListener.RequestProcessing
{
	// Token: 0x0200000E RID: 14
	internal abstract class ExceptionFilterStream : Stream
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00005259 File Offset: 0x00003459
		protected ExceptionFilterStream(Stream innerStream)
		{
			this._innerStream = innerStream;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005268 File Offset: 0x00003468
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005275 File Offset: 0x00003475
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005282 File Offset: 0x00003482
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000528F File Offset: 0x0000348F
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000529C File Offset: 0x0000349C
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000052A9 File Offset: 0x000034A9
		public override long Position
		{
			get
			{
				return this._innerStream.Position;
			}
			set
			{
				this._innerStream.Position = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000052B7 File Offset: 0x000034B7
		public override bool CanTimeout
		{
			get
			{
				return this._innerStream.CanTimeout;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000052C4 File Offset: 0x000034C4
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000052D1 File Offset: 0x000034D1
		public override int ReadTimeout
		{
			get
			{
				return this._innerStream.ReadTimeout;
			}
			set
			{
				this._innerStream.ReadTimeout = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000052DF File Offset: 0x000034DF
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000052EC File Offset: 0x000034EC
		public override int WriteTimeout
		{
			get
			{
				return this._innerStream.WriteTimeout;
			}
			set
			{
				this._innerStream.WriteTimeout = value;
			}
		}

		// Token: 0x060000AE RID: 174
		protected abstract bool TryWrapException(Exception ex, out Exception wrapped);

		// Token: 0x060000AF RID: 175 RVA: 0x000052FA File Offset: 0x000034FA
		private void FirstWrite()
		{
			this._onFirstWrite.TryInvoke();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005307 File Offset: 0x00003507
		public override void SetLength(long value)
		{
			this._innerStream.SetLength(value);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005315 File Offset: 0x00003515
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(offset, origin);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005324 File Offset: 0x00003524
		public void OnFirstWrite(Action<object> callback, object state)
		{
			this._onFirstWrite = new ExceptionFilterStream.OneTimeCallback(callback, state);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005334 File Offset: 0x00003534
		public override async Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			try
			{
				await this._innerStream.CopyToAsync(destination, bufferSize, cancellationToken);
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005390 File Offset: 0x00003590
		public override int ReadByte()
		{
			int num;
			try
			{
				num = this._innerStream.ReadByte();
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000053CC File Offset: 0x000035CC
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num;
			try
			{
				num = this._innerStream.Read(buffer, offset, count);
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000540C File Offset: 0x0000360C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this._innerStream.BeginRead(buffer, offset, count, callback, state);
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
			return asyncResult;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005450 File Offset: 0x00003650
		public override int EndRead(IAsyncResult asyncResult)
		{
			int num;
			try
			{
				num = this._innerStream.EndRead(asyncResult);
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005490 File Offset: 0x00003690
		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			int num2;
			try
			{
				int num = await this._innerStream.ReadAsync(buffer, offset, count, cancellationToken);
				num2 = num;
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
			return num2;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000054F4 File Offset: 0x000036F4
		public override void Write(byte[] buffer, int offset, int count)
		{
			try
			{
				this.FirstWrite();
				this._innerStream.Write(buffer, offset, count);
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005538 File Offset: 0x00003738
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			try
			{
				this.FirstWrite();
				asyncResult = this._innerStream.BeginWrite(buffer, offset, count, callback, state);
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
			return asyncResult;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005584 File Offset: 0x00003784
		public override void EndWrite(IAsyncResult asyncResult)
		{
			try
			{
				this._innerStream.EndWrite(asyncResult);
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000055C0 File Offset: 0x000037C0
		public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			try
			{
				this.FirstWrite();
				await this._innerStream.WriteAsync(buffer, offset, count, cancellationToken);
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005624 File Offset: 0x00003824
		public override void WriteByte(byte value)
		{
			try
			{
				this.FirstWrite();
				this._innerStream.WriteByte(value);
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005668 File Offset: 0x00003868
		public override void Flush()
		{
			try
			{
				this.FirstWrite();
				this._innerStream.Flush();
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000056A8 File Offset: 0x000038A8
		public override async Task FlushAsync(CancellationToken cancellationToken)
		{
			try
			{
				this.FirstWrite();
				await this._innerStream.FlushAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000056F4 File Offset: 0x000038F4
		public override void Close()
		{
			try
			{
				this.FirstWrite();
				this._innerStream.Close();
			}
			catch (Exception ex)
			{
				Exception wrapped;
				if (this.TryWrapException(ex, out wrapped))
				{
					throw wrapped;
				}
				throw;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005734 File Offset: 0x00003934
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._innerStream.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000081 RID: 129
		private readonly Stream _innerStream;

		// Token: 0x04000082 RID: 130
		private ExceptionFilterStream.OneTimeCallback _onFirstWrite;

		// Token: 0x02000024 RID: 36
		private struct OneTimeCallback
		{
			// Token: 0x0600016B RID: 363 RVA: 0x00008ABB File Offset: 0x00006CBB
			public OneTimeCallback(Action<object> callback, object state)
			{
				if (callback == null)
				{
					throw new ArgumentNullException("callback");
				}
				this._callback = callback;
				this._state = state;
				this._pending = 1;
			}

			// Token: 0x0600016C RID: 364 RVA: 0x00008AE0 File Offset: 0x00006CE0
			public void TryInvoke()
			{
				if (this._pending == 1 && Interlocked.CompareExchange(ref this._pending, 0, 1) == 1)
				{
					this._callback(this._state);
				}
			}

			// Token: 0x040000D7 RID: 215
			private Action<object> _callback;

			// Token: 0x040000D8 RID: 216
			private object _state;

			// Token: 0x040000D9 RID: 217
			private int _pending;
		}
	}
}
