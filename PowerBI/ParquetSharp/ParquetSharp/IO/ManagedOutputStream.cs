using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.IO
{
	// Token: 0x020000A9 RID: 169
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ManagedOutputStream : OutputStream
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x000116BC File Offset: 0x0000F8BC
		public ManagedOutputStream(Stream stream)
			: this(stream, false)
		{
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000116C8 File Offset: 0x0000F8C8
		public ManagedOutputStream(Stream stream, bool leaveOpen)
		{
			this._buffer = new byte[8192L];
			this._stream = stream;
			this._leaveOpen = leaveOpen;
			this._write = new ManagedOutputStream.WriteDelegate(this.Write);
			this._tell = new ManagedOutputStream.TellDelegate(this.Tell);
			this._flush = new ManagedOutputStream.FlushDelegate(this.Flush);
			this._close = new ManagedOutputStream.CloseDelegate(this.Close);
			this._closed = new ManagedOutputStream.ClosedDelegate(this.Closed);
			SafeDeleter safeDeleter = new SafeDeleter(new SafeDeleter.DeleteDelegate(this.Close));
			this.Handle = ManagedOutputStream.Create(this._write, this._tell, this._flush, this._close, this._closed, safeDeleter.Delete);
			safeDeleter.GiveOwnership();
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000117A0 File Offset: 0x0000F9A0
		private static ParquetHandle Create(ManagedOutputStream.WriteDelegate write, ManagedOutputStream.TellDelegate tell, ManagedOutputStream.FlushDelegate flush, ManagedOutputStream.CloseDelegate close, ManagedOutputStream.ClosedDelegate closed, SafeDeleter.DeleteDelegate delete)
		{
			IntPtr intPtr;
			ExceptionInfo.Check(ManagedOutputStream.ManagedOutputStream_Create(write, tell, flush, close, closed, delete, out intPtr));
			return new ParquetHandle(intPtr, new Action<IntPtr>(OutputStream.OutputStream_Free));
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000117D8 File Offset: 0x0000F9D8
		[NullableContext(2)]
		private byte Write(IntPtr src, long nbytes, out string exception)
		{
			byte b;
			try
			{
				while (nbytes > 0L)
				{
					int num = (int)Math.Min(nbytes, (long)this._buffer.Length);
					Marshal.Copy(src, this._buffer, 0, num);
					this._stream.Write(this._buffer, 0, num);
					nbytes -= (long)num;
					src = IntPtr.Add(src, num);
				}
				exception = (this._exceptionMessage = null);
				b = 0;
			}
			catch (Exception ex)
			{
				b = this.HandleException(ex, out exception);
			}
			return b;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00011864 File Offset: 0x0000FA64
		[NullableContext(2)]
		private byte Tell(IntPtr position, out string exception)
		{
			byte b;
			try
			{
				Marshal.WriteInt64(position, this._stream.Position);
				exception = (this._exceptionMessage = null);
				b = 0;
			}
			catch (Exception ex)
			{
				b = this.HandleException(ex, out exception);
			}
			return b;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000118B8 File Offset: 0x0000FAB8
		[NullableContext(2)]
		private byte Flush(out string exception)
		{
			byte b;
			try
			{
				this._stream.Flush();
				exception = (this._exceptionMessage = null);
				b = 0;
			}
			catch (Exception ex)
			{
				b = this.HandleException(ex, out exception);
			}
			return b;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00011904 File Offset: 0x0000FB04
		[NullableContext(2)]
		private byte Close(out string exception)
		{
			byte b;
			try
			{
				if (!this._leaveOpen)
				{
					this._stream.Close();
				}
				exception = null;
				b = 0;
			}
			catch (Exception ex)
			{
				b = this.HandleException(ex, out exception);
			}
			return b;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00011954 File Offset: 0x0000FB54
		private bool Closed()
		{
			bool flag;
			try
			{
				flag = !this._stream.CanWrite;
			}
			catch
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00011990 File Offset: 0x0000FB90
		private byte HandleException(Exception error, [Nullable(2)] out string exception)
		{
			if (error is OutOfMemoryException)
			{
				exception = (this._exceptionMessage = null);
				return 1;
			}
			if (error is IOException)
			{
				exception = (this._exceptionMessage = error.ToString());
				return 5;
			}
			exception = (this._exceptionMessage = error.ToString());
			return 9;
		}

		// Token: 0x06000520 RID: 1312
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ManagedOutputStream_Create(ManagedOutputStream.WriteDelegate write, ManagedOutputStream.TellDelegate tell, ManagedOutputStream.FlushDelegate flush, ManagedOutputStream.CloseDelegate close, ManagedOutputStream.ClosedDelegate closed, SafeDeleter.DeleteDelegate delete, out IntPtr outputStream);

		// Token: 0x04000173 RID: 371
		private const long BufferSize = 8192L;

		// Token: 0x04000174 RID: 372
		private readonly Stream _stream;

		// Token: 0x04000175 RID: 373
		private readonly bool _leaveOpen;

		// Token: 0x04000176 RID: 374
		private readonly byte[] _buffer;

		// Token: 0x04000177 RID: 375
		private readonly ManagedOutputStream.WriteDelegate _write;

		// Token: 0x04000178 RID: 376
		private readonly ManagedOutputStream.TellDelegate _tell;

		// Token: 0x04000179 RID: 377
		private readonly ManagedOutputStream.FlushDelegate _flush;

		// Token: 0x0400017A RID: 378
		private readonly ManagedOutputStream.CloseDelegate _close;

		// Token: 0x0400017B RID: 379
		private readonly ManagedOutputStream.ClosedDelegate _closed;

		// Token: 0x0400017C RID: 380
		[Nullable(2)]
		private string _exceptionMessage;

		// Token: 0x0400017D RID: 381
		private const long MaxArraySize = 2147483591L;

		// Token: 0x02000137 RID: 311
		// (Invoke) Token: 0x060009DF RID: 2527
		[NullableContext(0)]
		private delegate byte WriteDelegate(IntPtr buffer, long nbyte, [MarshalAs(UnmanagedType.LPStr)] out string exception);

		// Token: 0x02000138 RID: 312
		// (Invoke) Token: 0x060009E3 RID: 2531
		[NullableContext(0)]
		private delegate byte TellDelegate(IntPtr position, [MarshalAs(UnmanagedType.LPStr)] out string exception);

		// Token: 0x02000139 RID: 313
		// (Invoke) Token: 0x060009E7 RID: 2535
		[NullableContext(0)]
		private delegate byte FlushDelegate([MarshalAs(UnmanagedType.LPStr)] out string exception);

		// Token: 0x0200013A RID: 314
		// (Invoke) Token: 0x060009EB RID: 2539
		[NullableContext(0)]
		private delegate byte CloseDelegate([MarshalAs(UnmanagedType.LPStr)] out string exception);

		// Token: 0x0200013B RID: 315
		// (Invoke) Token: 0x060009EF RID: 2543
		[NullableContext(0)]
		private delegate bool ClosedDelegate();
	}
}
