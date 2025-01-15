using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.IO
{
	// Token: 0x020000AA RID: 170
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ManagedRandomAccessFile : RandomAccessFile
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x000119EC File Offset: 0x0000FBEC
		public ManagedRandomAccessFile(Stream stream)
			: this(stream, false)
		{
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000119F8 File Offset: 0x0000FBF8
		public ManagedRandomAccessFile(Stream stream, bool leaveOpen)
		{
			this._buffer = new byte[8192L];
			this._stream = stream;
			this._leaveOpen = leaveOpen;
			this._read = new ManagedRandomAccessFile.ReadDelegate(this.Read);
			this._readBuffer = new ManagedRandomAccessFile.ReadBufferDelegate(this.ReadBuffer);
			this._close = new ManagedRandomAccessFile.CloseDelegate(this.Close);
			this._getSize = new ManagedRandomAccessFile.GetSizeDelegate(this.GetSize);
			this._tell = new ManagedRandomAccessFile.TellDelegate(this.Tell);
			this._seek = new ManagedRandomAccessFile.SeekDelegate(this.Seek);
			this._closed = new ManagedRandomAccessFile.ClosedDelegate(this.Closed);
			SafeDeleter safeDeleter = new SafeDeleter(new SafeDeleter.DeleteDelegate(this.Close));
			this.Handle = ManagedRandomAccessFile.Create(this._read, this._readBuffer, this._close, this._getSize, this._tell, this._seek, this._closed, safeDeleter.Delete);
			safeDeleter.GiveOwnership();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00011B00 File Offset: 0x0000FD00
		private static ParquetHandle Create(ManagedRandomAccessFile.ReadDelegate read, ManagedRandomAccessFile.ReadBufferDelegate readBuffer, ManagedRandomAccessFile.CloseDelegate close, ManagedRandomAccessFile.GetSizeDelegate getSize, ManagedRandomAccessFile.TellDelegate tell, ManagedRandomAccessFile.SeekDelegate seek, ManagedRandomAccessFile.ClosedDelegate closed, SafeDeleter.DeleteDelegate delete)
		{
			IntPtr intPtr;
			ExceptionInfo.Check(ManagedRandomAccessFile.ManagedRandomAccessFile_Create(read, readBuffer, close, getSize, tell, seek, closed, delete, out intPtr));
			return new ParquetHandle(intPtr, new Action<IntPtr>(RandomAccessFile.RandomAccessFile_Free));
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00011B3C File Offset: 0x0000FD3C
		[NullableContext(2)]
		private byte Read(long nbytes, IntPtr bytesRead, IntPtr dest, out string exception)
		{
			byte b;
			try
			{
				long num = 0L;
				while (num < nbytes)
				{
					int num2 = (int)Math.Min(nbytes - num, (long)this._buffer.Length);
					int num3 = this._stream.Read(this._buffer, 0, num2);
					Marshal.Copy(this._buffer, 0, dest, num3);
					if (num3 == 0)
					{
						break;
					}
					num += (long)num3;
					dest = IntPtr.Add(dest, num3);
				}
				Marshal.WriteInt64(bytesRead, num);
				exception = null;
				b = 0;
			}
			catch (Exception ex)
			{
				b = this.HandleException(ex, out exception);
			}
			return b;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00011BD4 File Offset: 0x0000FDD4
		[NullableContext(2)]
		private byte ReadBuffer(long nbytes, out IntPtr buffer_handle, out string exception)
		{
			byte b;
			try
			{
				byte[] array = new byte[nbytes];
				int num = 0;
				while (nbytes > 0L)
				{
					int num2 = (int)Math.Min(2147483647L, nbytes);
					int num3 = this._stream.Read(array, num, num2);
					if (num3 == 0)
					{
						break;
					}
					num += num3;
					nbytes -= (long)num3;
				}
				Buffer buffer = ManagedBuffer.New(array, new long?((long)num));
				buffer_handle = IntPtr.Zero;
				if (buffer != null)
				{
					ParquetHandle parquetHandle = buffer.Consume();
					buffer_handle = parquetHandle.IntPtr;
					GC.SuppressFinalize(parquetHandle);
				}
				exception = null;
				b = 0;
			}
			catch (Exception ex)
			{
				buffer_handle = IntPtr.Zero;
				b = this.HandleException(ex, out exception);
			}
			return b;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00011C90 File Offset: 0x0000FE90
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

		// Token: 0x06000527 RID: 1319 RVA: 0x00011CE0 File Offset: 0x0000FEE0
		[NullableContext(2)]
		private byte GetSize(IntPtr size, out string exception)
		{
			byte b;
			try
			{
				Marshal.WriteInt64(size, this._stream.Length);
				exception = null;
				b = 0;
			}
			catch (Exception ex)
			{
				b = this.HandleException(ex, out exception);
			}
			return b;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00011D28 File Offset: 0x0000FF28
		[NullableContext(2)]
		private byte Tell(IntPtr position, out string exception)
		{
			byte b;
			try
			{
				Marshal.WriteInt64(position, this._stream.Position);
				exception = null;
				b = 0;
			}
			catch (Exception ex)
			{
				b = this.HandleException(ex, out exception);
			}
			return b;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00011D70 File Offset: 0x0000FF70
		[NullableContext(2)]
		private byte Seek(long position, out string exception)
		{
			byte b;
			try
			{
				this._stream.Position = position;
				exception = null;
				b = 0;
			}
			catch (Exception ex)
			{
				b = this.HandleException(ex, out exception);
			}
			return b;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00011DB4 File Offset: 0x0000FFB4
		private bool Closed()
		{
			bool flag;
			try
			{
				flag = !this._stream.CanRead;
			}
			catch
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00011DF0 File Offset: 0x0000FFF0
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

		// Token: 0x0600052C RID: 1324
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ManagedRandomAccessFile_Create(ManagedRandomAccessFile.ReadDelegate read, ManagedRandomAccessFile.ReadBufferDelegate readBuffer, ManagedRandomAccessFile.CloseDelegate close, ManagedRandomAccessFile.GetSizeDelegate getSize, ManagedRandomAccessFile.TellDelegate tell, ManagedRandomAccessFile.SeekDelegate seek, ManagedRandomAccessFile.ClosedDelegate closed, SafeDeleter.DeleteDelegate delete, out IntPtr randomAccessFile);

		// Token: 0x0400017E RID: 382
		private const long BufferSize = 8192L;

		// Token: 0x0400017F RID: 383
		private readonly Stream _stream;

		// Token: 0x04000180 RID: 384
		private readonly bool _leaveOpen;

		// Token: 0x04000181 RID: 385
		private readonly byte[] _buffer;

		// Token: 0x04000182 RID: 386
		private readonly ManagedRandomAccessFile.ReadDelegate _read;

		// Token: 0x04000183 RID: 387
		private readonly ManagedRandomAccessFile.ReadBufferDelegate _readBuffer;

		// Token: 0x04000184 RID: 388
		private readonly ManagedRandomAccessFile.CloseDelegate _close;

		// Token: 0x04000185 RID: 389
		private readonly ManagedRandomAccessFile.GetSizeDelegate _getSize;

		// Token: 0x04000186 RID: 390
		private readonly ManagedRandomAccessFile.TellDelegate _tell;

		// Token: 0x04000187 RID: 391
		private readonly ManagedRandomAccessFile.SeekDelegate _seek;

		// Token: 0x04000188 RID: 392
		private readonly ManagedRandomAccessFile.ClosedDelegate _closed;

		// Token: 0x04000189 RID: 393
		[Nullable(2)]
		private string _exceptionMessage;

		// Token: 0x0400018A RID: 394
		private const long MaxArraySize = 2147483591L;

		// Token: 0x0200013C RID: 316
		// (Invoke) Token: 0x060009F3 RID: 2547
		[NullableContext(0)]
		private delegate byte ReadDelegate(long nbyte, IntPtr bytesRead, IntPtr dest, [MarshalAs(UnmanagedType.LPStr)] out string exception);

		// Token: 0x0200013D RID: 317
		// (Invoke) Token: 0x060009F7 RID: 2551
		[NullableContext(0)]
		private delegate byte ReadBufferDelegate(long nbytes, out IntPtr buffer_handle, [MarshalAs(UnmanagedType.LPStr)] out string exception);

		// Token: 0x0200013E RID: 318
		// (Invoke) Token: 0x060009FB RID: 2555
		[NullableContext(0)]
		private delegate byte CloseDelegate([MarshalAs(UnmanagedType.LPStr)] out string exception);

		// Token: 0x0200013F RID: 319
		// (Invoke) Token: 0x060009FF RID: 2559
		[NullableContext(0)]
		private delegate byte GetSizeDelegate(IntPtr size, [MarshalAs(UnmanagedType.LPStr)] out string exception);

		// Token: 0x02000140 RID: 320
		// (Invoke) Token: 0x06000A03 RID: 2563
		[NullableContext(0)]
		private delegate byte TellDelegate(IntPtr position, [MarshalAs(UnmanagedType.LPStr)] out string exception);

		// Token: 0x02000141 RID: 321
		// (Invoke) Token: 0x06000A07 RID: 2567
		[NullableContext(0)]
		private delegate byte SeekDelegate(long position, [MarshalAs(UnmanagedType.LPStr)] out string exception);

		// Token: 0x02000142 RID: 322
		// (Invoke) Token: 0x06000A0B RID: 2571
		[NullableContext(0)]
		private delegate bool ClosedDelegate();
	}
}
