using System;
using System.IO;

namespace Microsoft.MachineLearning.Data.IO.Zlib
{
	// Token: 0x020000E2 RID: 226
	public sealed class ZDeflateStream : Stream
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x00019B4C File Offset: 0x00017D4C
		public unsafe ZDeflateStream(Stream compressed, Constants.Level level = Constants.Level.BestCompression, Constants.Strategy strategy = Constants.Strategy.DefaultStrategy, int memLevel = 9, bool useZlibFormat = false, int windowBits = 15)
		{
			this._compressed = compressed;
			this._buffer = new byte[32768];
			Constants.RetCode retCode;
			fixed (ZStream* ptr = &this._zstrm)
			{
				retCode = Zlib.deflateInit2(ptr, (int)level, 8, useZlibFormat ? windowBits : (-windowBits), memLevel, strategy);
			}
			if (retCode != Constants.RetCode.OK)
			{
				throw Contracts.Except("Could not initialize zstream. Error code: {0}", new object[] { retCode });
			}
			this._zstrm.avail_out = (uint)this._buffer.Length;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00019BCC File Offset: 0x00017DCC
		protected unsafe override void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			this._disposed = true;
			Constants.RetCode retCode = Constants.RetCode.StreamEnd;
			if (disposing)
			{
				fixed (byte* buffer = this._buffer)
				{
					fixed (ZStream* ptr = &this._zstrm)
					{
						ptr->avail_in = 0U;
						ptr->next_in = null;
						ptr->next_out = buffer + this.BufferUsed;
						do
						{
							this.RefreshOutput(buffer);
							retCode = Zlib.deflate(ptr, Constants.Flush.Finish);
						}
						while (retCode == Constants.RetCode.OK);
						if (retCode == Constants.RetCode.StreamEnd)
						{
							this.Flush();
							this._compressed.Flush();
						}
					}
				}
			}
			Constants.RetCode retCode2;
			fixed (ZStream* ptr2 = &this._zstrm)
			{
				retCode2 = Zlib.deflateEnd(ptr2);
			}
			base.Dispose(disposing);
			if (disposing)
			{
				GC.SuppressFinalize(this);
				if (retCode != Constants.RetCode.StreamEnd)
				{
					throw Contracts.Except("Zlib deflate failed with {0}", new object[] { retCode });
				}
				if (retCode2 != Constants.RetCode.OK)
				{
					throw Contracts.Except("Zlib deflateEnd failed with {0}", new object[] { retCode2 });
				}
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00019CD0 File Offset: 0x00017ED0
		~ZDeflateStream()
		{
			this.Dispose(false);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00019D00 File Offset: 0x00017F00
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00019D03 File Offset: 0x00017F03
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00019D06 File Offset: 0x00017F06
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00019D09 File Offset: 0x00017F09
		private int BufferUsed
		{
			get
			{
				return this._buffer.Length - (int)this._zstrm.avail_out;
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00019D1F File Offset: 0x00017F1F
		public override void Flush()
		{
			if (this.BufferUsed <= 0)
			{
				return;
			}
			this._compressed.Write(this._buffer, 0, this.BufferUsed);
			this._zstrm.avail_out = (uint)this._buffer.Length;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00019D56 File Offset: 0x00017F56
		public override long Length
		{
			get
			{
				throw Contracts.ExceptNotSupp();
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00019D5D File Offset: 0x00017F5D
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00019D64 File Offset: 0x00017F64
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

		// Token: 0x060004A5 RID: 1189 RVA: 0x00019D6B File Offset: 0x00017F6B
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw Contracts.ExceptNotImpl();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00019D72 File Offset: 0x00017F72
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw Contracts.ExceptNotImpl();
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00019D79 File Offset: 0x00017F79
		public override void SetLength(long value)
		{
			throw Contracts.ExceptNotImpl();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00019D80 File Offset: 0x00017F80
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			Contracts.CheckValue<byte[]>(buffer, "buffer");
			Contracts.CheckParamValue<int>(offset >= 0, offset, "offset", "offset can't be negative value");
			Contracts.CheckParamValue<int>(offset < buffer.Length, offset, "offset", "offset can't be greater than buffer length");
			Contracts.CheckParamValue<int>(count >= 0, count, "count", "count can't be negative value");
			Contracts.CheckParamValue<int>(count <= buffer.Length - offset, count, "count", "count should be less or equal than difference between buffer length and offset");
			if (count == 0)
			{
				return;
			}
			fixed (byte* ptr = &this._buffer[0])
			{
				fixed (byte* ptr2 = &buffer[offset])
				{
					this.RawWrite(ptr2, ptr, count);
				}
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00019E21 File Offset: 0x00018021
		private unsafe void RefreshOutput(byte* pOutput)
		{
			if (this._zstrm.avail_out != 0U)
			{
				return;
			}
			this.Flush();
			this._zstrm.next_out = pOutput;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00019E44 File Offset: 0x00018044
		private unsafe void RawWrite(byte* buffer, byte* pOutput, int count)
		{
			this._zstrm.avail_in = (uint)count;
			this._zstrm.next_in = buffer;
			this._zstrm.next_out = pOutput + this.BufferUsed;
			Constants.RetCode retCode;
			for (;;)
			{
				this.RefreshOutput(pOutput);
				fixed (ZStream* ptr = &this._zstrm)
				{
					retCode = Zlib.deflate(ptr, Constants.Flush.NoFlush);
				}
				if (retCode != Constants.RetCode.OK)
				{
					break;
				}
				if (this._zstrm.avail_in <= 0U)
				{
					goto Block_2;
				}
			}
			throw Contracts.Except("Zlib.deflate failed with {0}", new object[] { retCode });
			Block_2:
			this._zstrm.next_in = null;
		}

		// Token: 0x0400022B RID: 555
		private readonly Stream _compressed;

		// Token: 0x0400022C RID: 556
		private readonly byte[] _buffer;

		// Token: 0x0400022D RID: 557
		private ZStream _zstrm;

		// Token: 0x0400022E RID: 558
		private bool _disposed;
	}
}
