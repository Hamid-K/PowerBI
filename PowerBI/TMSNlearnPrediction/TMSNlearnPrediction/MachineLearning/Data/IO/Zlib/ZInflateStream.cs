using System;
using System.IO;

namespace Microsoft.MachineLearning.Data.IO.Zlib
{
	// Token: 0x020000E3 RID: 227
	public sealed class ZInflateStream : Stream
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x00019ED0 File Offset: 0x000180D0
		public unsafe ZInflateStream(Stream compressed, bool useZlibFormat = false)
		{
			this._compressed = compressed;
			this._buffer = new byte[32768];
			Constants.RetCode retCode;
			fixed (ZStream* ptr = &this._zstrm)
			{
				retCode = Zlib.inflateInit2(ptr, useZlibFormat ? 15 : (-15));
			}
			if (retCode != Constants.RetCode.OK)
			{
				throw Contracts.Except("Could not initialize zstream. Error code: {0}", new object[] { retCode });
			}
			this._bufferUsed = 0;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00019F3C File Offset: 0x0001813C
		protected unsafe override void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			this._disposed = true;
			Constants.RetCode retCode;
			fixed (ZStream* ptr = &this._zstrm)
			{
				retCode = Zlib.inflateEnd(ptr);
			}
			base.Dispose(disposing);
			if (disposing)
			{
				GC.SuppressFinalize(this);
				if (retCode != Constants.RetCode.OK)
				{
					throw Contracts.Except("Zlib inflateEnd failed with {0}", new object[] { retCode });
				}
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00019F9C File Offset: 0x0001819C
		~ZInflateStream()
		{
			this.Dispose(false);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00019FCC File Offset: 0x000181CC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00019FCF File Offset: 0x000181CF
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00019FD2 File Offset: 0x000181D2
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00019FD5 File Offset: 0x000181D5
		public override void Flush()
		{
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00019FD7 File Offset: 0x000181D7
		public override long Length
		{
			get
			{
				throw Contracts.ExceptNotSupp();
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00019FDE File Offset: 0x000181DE
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00019FE5 File Offset: 0x000181E5
		public override long Position
		{
			get
			{
				throw Contracts.ExceptNotSupp();
			}
			set
			{
				throw Contracts.ExceptNotSupp();
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00019FEC File Offset: 0x000181EC
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			Contracts.CheckValue<byte[]>(buffer, "buffer");
			Contracts.CheckParamValue<int>(offset >= 0, offset, "offset", "offset can't be negative value");
			Contracts.CheckParamValue<int>(offset < buffer.Length, offset, "offset", "offset can't be greater than buffer length");
			Contracts.CheckParamValue<int>(count >= 0, count, "count", "count can't be negative value");
			Contracts.CheckParamValue<int>(count <= buffer.Length - offset, count, "count", "count should be less or equal than difference between buffer lenght and offset");
			if (count == 0)
			{
				return 0;
			}
			fixed (byte* ptr = &this._buffer[0])
			{
				fixed (byte* ptr2 = &buffer[offset])
				{
					return this.InternalRead(ptr, ptr2, count);
				}
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001A08C File Offset: 0x0001828C
		private unsafe int InternalRead(byte* pInput, byte* pOutput, int count)
		{
			this._zstrm.next_in = pInput + this._bufferUsed - this._zstrm.avail_in;
			this._zstrm.next_out = pOutput;
			this._zstrm.avail_out = (uint)count;
			Constants.RetCode retCode;
			for (;;)
			{
				if (this._compressed != null && (this._bufferUsed == 0 || this._zstrm.avail_in == 0U))
				{
					this._bufferUsed = this._compressed.Read(this._buffer, 0, this._buffer.Length);
					this._zstrm.avail_in = (uint)this._bufferUsed;
					if (this._bufferUsed == 0)
					{
						goto IL_0116;
					}
					this._zstrm.next_in = pInput;
				}
				else
				{
					this._zstrm.next_in = pInput + this._bufferUsed - this._zstrm.avail_in;
				}
				if (this._zstrm.avail_in == 0U)
				{
					break;
				}
				fixed (ZStream* ptr = &this._zstrm)
				{
					retCode = Zlib.inflate(ptr, Constants.Flush.NoFlush);
					if (retCode != Constants.RetCode.StreamEnd && retCode != Constants.RetCode.OK)
					{
						goto Block_6;
					}
				}
				if (retCode == Constants.RetCode.StreamEnd || this._zstrm.avail_out == 0U)
				{
					goto IL_0116;
				}
			}
			return 0;
			Block_6:
			throw Contracts.Except("Zlib.inflate failed with {0}", new object[] { retCode });
			IL_0116:
			return count - (int)this._zstrm.avail_out;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001A1BC File Offset: 0x000183BC
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw Contracts.ExceptNotSupp();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001A1C3 File Offset: 0x000183C3
		public override void SetLength(long value)
		{
			throw Contracts.ExceptNotSupp();
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001A1CA File Offset: 0x000183CA
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw Contracts.ExceptNotSupp();
		}

		// Token: 0x0400022F RID: 559
		private readonly Stream _compressed;

		// Token: 0x04000230 RID: 560
		private readonly byte[] _buffer;

		// Token: 0x04000231 RID: 561
		private int _bufferUsed;

		// Token: 0x04000232 RID: 562
		private ZStream _zstrm;

		// Token: 0x04000233 RID: 563
		private bool _disposed;
	}
}
