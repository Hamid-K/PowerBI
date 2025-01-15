using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200009A RID: 154
	[NullableContext(1)]
	[Nullable(0)]
	internal class ReadTimeoutStream : ReadOnlyStream
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
		public ReadTimeoutStream(Stream stream, TimeSpan readTimeout)
		{
			this._stream = stream;
			this._readTimeout = readTimeout;
			this.UpdateReadTimeout();
			this.InitializeTokenSource();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000EE18 File Offset: 0x0000D018
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool flag;
			CancellationTokenSource cancellationTokenSource = this.StartTimeout(default(CancellationToken), out flag);
			int num;
			try
			{
				num = this._stream.Read(buffer, offset, count);
			}
			catch (IOException ex)
			{
				ResponseBodyPolicy.ThrowIfCancellationRequestedOrTimeout(default(CancellationToken), cancellationTokenSource.Token, ex, this._readTimeout);
				throw;
			}
			catch (ObjectDisposedException ex2)
			{
				ResponseBodyPolicy.ThrowIfCancellationRequestedOrTimeout(default(CancellationToken), cancellationTokenSource.Token, ex2, this._readTimeout);
				throw;
			}
			catch (OperationCanceledException ex3)
			{
				ResponseBodyPolicy.ThrowIfCancellationRequestedOrTimeout(default(CancellationToken), cancellationTokenSource.Token, ex3, this._readTimeout);
				throw;
			}
			finally
			{
				this.StopTimeout(cancellationTokenSource, flag);
			}
			return num;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			bool dispose;
			CancellationTokenSource source = this.StartTimeout(cancellationToken, out dispose);
			int num;
			try
			{
				num = await this._stream.ReadAsync(buffer, offset, count, source.Token).ConfigureAwait(false);
			}
			catch (IOException ex)
			{
				ResponseBodyPolicy.ThrowIfCancellationRequestedOrTimeout(cancellationToken, source.Token, ex, this._readTimeout);
				throw;
			}
			catch (ObjectDisposedException ex2)
			{
				ResponseBodyPolicy.ThrowIfCancellationRequestedOrTimeout(cancellationToken, source.Token, ex2, this._readTimeout);
				throw;
			}
			catch (OperationCanceledException ex3)
			{
				ResponseBodyPolicy.ThrowIfCancellationRequestedOrTimeout(cancellationToken, source.Token, ex3, this._readTimeout);
				throw;
			}
			finally
			{
				this.StopTimeout(source, dispose);
			}
			return num;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000EF4C File Offset: 0x0000D14C
		private CancellationTokenSource StartTimeout(CancellationToken additionalToken, out bool dispose)
		{
			if (this._cancellationTokenSource.IsCancellationRequested)
			{
				this.InitializeTokenSource();
			}
			CancellationTokenSource cancellationTokenSource;
			if (additionalToken.CanBeCanceled)
			{
				cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(additionalToken, this._cancellationTokenSource.Token);
				dispose = true;
			}
			else
			{
				cancellationTokenSource = this._cancellationTokenSource;
				dispose = false;
			}
			this._cancellationTokenSource.CancelAfter(this._readTimeout);
			return cancellationTokenSource;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		private void InitializeTokenSource()
		{
			this._cancellationTokenSource = new CancellationTokenSource();
			this._cancellationTokenSource.Token.Register(delegate(object state)
			{
				((ReadTimeoutStream)state).DisposeStream();
			}, this);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
		private void DisposeStream()
		{
			this._stream.Dispose();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000F001 File Offset: 0x0000D201
		private void StopTimeout(CancellationTokenSource source, bool dispose)
		{
			this._cancellationTokenSource.CancelAfter(Timeout.InfiniteTimeSpan);
			if (dispose)
			{
				source.Dispose();
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000F01C File Offset: 0x0000D21C
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._stream.Seek(offset, origin);
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000F02B File Offset: 0x0000D22B
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000F038 File Offset: 0x0000D238
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000F045 File Offset: 0x0000D245
		public override long Length
		{
			get
			{
				return this._stream.Length;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0000F052 File Offset: 0x0000D252
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x0000F05F File Offset: 0x0000D25F
		public override long Position
		{
			get
			{
				return this._stream.Position;
			}
			set
			{
				this._stream.Position = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000F06D File Offset: 0x0000D26D
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x0000F07B File Offset: 0x0000D27B
		public override int ReadTimeout
		{
			get
			{
				return (int)this._readTimeout.TotalMilliseconds;
			}
			set
			{
				this._readTimeout = TimeSpan.FromMilliseconds((double)value);
				this.UpdateReadTimeout();
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000F090 File Offset: 0x0000D290
		private void UpdateReadTimeout()
		{
			try
			{
				if (this._stream.CanTimeout)
				{
					this._stream.ReadTimeout = (int)this._readTimeout.TotalMilliseconds;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000F0D8 File Offset: 0x0000D2D8
		public override void Close()
		{
			this._stream.Close();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000F0E5 File Offset: 0x0000D2E5
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this._stream.Dispose();
			this._cancellationTokenSource.Dispose();
		}

		// Token: 0x04000202 RID: 514
		private readonly Stream _stream;

		// Token: 0x04000203 RID: 515
		private TimeSpan _readTimeout;

		// Token: 0x04000204 RID: 516
		private CancellationTokenSource _cancellationTokenSource;
	}
}
