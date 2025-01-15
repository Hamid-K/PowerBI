using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.RelayPacketContracts;

namespace Microsoft.PowerBI.DataMovement.Pipeline.CompressionInterop
{
	// Token: 0x02000009 RID: 9
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XPress9Stream : Stream
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002434 File Offset: 0x00000634
		internal XPress9Stream(Stream stream, CompressionMode mode, bool leaveOpen, XPress9Level level)
		{
			RuntimeChecks.CheckValue(stream, "stream");
			if (level != XPress9Level.Level6 && level != XPress9Level.Level9)
			{
				throw RuntimeChecks.ArgumentOutOfRange("level", level, "Only levels 6 and 9 are supported.");
			}
			if (mode != CompressionMode.Decompress)
			{
				if (mode != CompressionMode.Compress)
				{
					throw RuntimeChecks.ArgumentOutOfRange("mode", mode, "Unsupported compression mode detected.");
				}
				RuntimeChecks.Check(stream.CanWrite, "Stream is not writeable");
				this.m_index = 0;
			}
			else
			{
				RuntimeChecks.Check(stream.CanRead, "Stream is not readable");
				this.m_index = 40960;
			}
			this.m_baseStream = stream;
			this.m_compressionMode = mode;
			this.m_leaveOpen = leaveOpen;
			this.m_level = level;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024EF File Offset: 0x000006EF
		internal XPress9Stream(Stream stream, CompressionMode mode, XPress9Level level)
			: this(stream, mode, false, level)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024FB File Offset: 0x000006FB
		internal XPress9Stream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, leaveOpen, XPress9Level.Level6)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002507 File Offset: 0x00000707
		internal XPress9Stream(Stream stream, CompressionMode mode)
			: this(stream, mode, false)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002512 File Offset: 0x00000712
		public override bool CanRead
		{
			get
			{
				return this.m_baseStream != null && this.m_compressionMode == CompressionMode.Decompress && this.m_baseStream.CanRead;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002533 File Offset: 0x00000733
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002536 File Offset: 0x00000736
		public override bool CanWrite
		{
			get
			{
				return this.m_baseStream != null && this.m_compressionMode == CompressionMode.Compress && this.m_baseStream.CanWrite;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002558 File Offset: 0x00000758
		public override long Length
		{
			get
			{
				throw RuntimeChecks.UnsupportedCodepath("Not Supported", "/src/Gateway/Pipeline/CompressionInterop/XPress9Stream.cs", 118, "Unsupported code path reached");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002570 File Offset: 0x00000770
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002588 File Offset: 0x00000788
		public override long Position
		{
			get
			{
				throw RuntimeChecks.UnsupportedCodepath("Not Supported", "/src/Gateway/Pipeline/CompressionInterop/XPress9Stream.cs", 123, "Unsupported code path reached");
			}
			set
			{
				throw RuntimeChecks.UnsupportedCodepath("Not Supported", "/src/Gateway/Pipeline/CompressionInterop/XPress9Stream.cs", 124, "Unsupported code path reached");
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025A0 File Offset: 0x000007A0
		public override void Flush()
		{
			RuntimeChecks.CheckNotDisposed(this.m_baseStream == null, this.m_baseStream);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025B6 File Offset: 0x000007B6
		public override void Write(byte[] buffer, int offset, int count)
		{
			XPress9Stream.ValidateParameters(buffer, offset, count);
			RuntimeChecks.Check(this.m_compressionMode == CompressionMode.Compress, "Cannot write to Xpress9 stream");
			RuntimeChecks.CheckNotDisposed(this.m_baseStream == null, this.m_baseStream);
			this.InternalWrite(buffer, offset, count);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025F0 File Offset: 0x000007F0
		private void InternalWrite(byte[] array, int offset, int count)
		{
			if (this.m_index == 0 && count >= 40960)
			{
				this.WriteToStream(array, offset, count);
				return;
			}
			foreach (byte[] array2 in this.GetBuffersForCompression(array, offset, count))
			{
				this.WriteToStream(array2, 0, array2.Length);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002660 File Offset: 0x00000860
		private IEnumerable<byte[]> GetBuffersForCompression(byte[] array, int offset, int count)
		{
			if (this.m_innerBuffer == null)
			{
				this.m_innerBuffer = new byte[40960];
			}
			while (count > 0)
			{
				int num = Math.Min(40960 - this.m_index, count);
				Array.Copy(array, offset, this.m_innerBuffer, this.m_index, num);
				count -= num;
				offset += num;
				this.m_index += num;
				if (this.m_index == 40960)
				{
					yield return this.m_innerBuffer;
					this.m_index = 0;
				}
			}
			yield break;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002688 File Offset: 0x00000888
		private void WriteToStream(byte[] array, int offset, int count)
		{
			byte[] array2;
			int num = XPress9Compression.Compress(array, offset, count, (uint)this.m_level, out array2);
			byte[] array3 = XPress9Compression.ConvertSizeToHeader((uint)num);
			this.m_baseStream.Write(array3, 0, array3.Length);
			this.m_baseStream.Write(array2, 0, num);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026CB File Offset: 0x000008CB
		public override int Read(byte[] buffer, int offset, int count)
		{
			XPress9Stream.ValidateParameters(buffer, offset, count);
			RuntimeChecks.Check(this.m_compressionMode == CompressionMode.Decompress, "Cannot read from Xpress9 stream");
			RuntimeChecks.CheckNotDisposed(this.m_baseStream == null, this.m_baseStream);
			return this.InternalRead(buffer, offset, count);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002708 File Offset: 0x00000908
		private int InternalRead(byte[] array, int offset, int count)
		{
			int num = 0;
			int num2 = 0;
			while (count > 0)
			{
				if (this.m_innerBuffer == null || this.m_index == this.m_innerBuffer.Length)
				{
					num2 = this.GetDecompressedBuffer(array, offset, count);
					if (num2 == 0)
					{
						return num;
					}
				}
				int num3;
				if (this.m_innerBuffer == array)
				{
					num3 = num2;
					this.m_innerBuffer = null;
				}
				else
				{
					num3 = Math.Min(this.m_innerBuffer.Length - this.m_index, count);
					Array.Copy(this.m_innerBuffer, this.m_index, array, offset, num3);
				}
				count -= num3;
				offset += num3;
				this.m_index += num3;
				num += num3;
			}
			return num;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027A8 File Offset: 0x000009A8
		private int GetDecompressedBuffer(byte[] readBuffer, int readOffset, int readCount)
		{
			byte[] array = new byte[4];
			int num = this.InternalReadStreams(array);
			if (num == 0)
			{
				return 0;
			}
			if (num < 4)
			{
				this.m_bufferStream.Write(array, 0, num);
				this.m_bufferStream.Position = 0L;
				return 0;
			}
			uint num2 = XPress9Compression.ConvertHeaderToSize(array);
			byte[] array2 = new byte[num2];
			num = this.InternalReadStreams(array2);
			if ((long)num < (long)((ulong)num2))
			{
				this.m_bufferStream.Write(array, 0, array.Length);
				this.m_bufferStream.Write(array2, 0, num);
				this.m_bufferStream.Position = 0L;
				return 0;
			}
			this.m_index = 0;
			return XPress9Compression.Decompress(array2, (uint)this.m_level, out this.m_innerBuffer, ref readBuffer, readOffset, readCount);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002850 File Offset: 0x00000A50
		private int InternalReadStreams(byte[] readBuffer)
		{
			int num = 0;
			if (this.m_bufferStream.Length > 0L)
			{
				num = XPress9Stream.InternalReadStream(readBuffer, 0, this.m_bufferStream);
				if (num > 0 && num < readBuffer.Length)
				{
					this.m_bufferStream.SetLength(0L);
				}
				else if (num == readBuffer.Length)
				{
					return num;
				}
			}
			int num2 = XPress9Stream.InternalReadStream(readBuffer, num, this.m_baseStream);
			return num + num2;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028B0 File Offset: 0x00000AB0
		private static int InternalReadStream(byte[] readBuffer, int offset, Stream stream)
		{
			int num = offset;
			int num2 = 0;
			do
			{
				int num3 = stream.Read(readBuffer, num, readBuffer.Length - num);
				if (num3 == 0)
				{
					break;
				}
				num += num3;
				num2 += num3;
			}
			while (num2 != 0 && num2 != readBuffer.Length);
			return num2;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000028E4 File Offset: 0x00000AE4
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw RuntimeChecks.UnsupportedCodepath("Not Supported", "/src/Gateway/Pipeline/CompressionInterop/XPress9Stream.cs", 341, "Unsupported code path reached");
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028FF File Offset: 0x00000AFF
		public override void SetLength(long value)
		{
			throw RuntimeChecks.UnsupportedCodepath("Not Supported", "/src/Gateway/Pipeline/CompressionInterop/XPress9Stream.cs", 346, "Unsupported code path reached");
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000291C File Offset: 0x00000B1C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.m_compressionMode == CompressionMode.Compress && this.m_baseStream != null && this.m_index > 0)
				{
					this.WriteToStream(this.m_innerBuffer, 0, this.m_index);
					this.m_index = 0;
				}
			}
			finally
			{
				try
				{
					if (disposing && !this.m_leaveOpen && this.m_baseStream != null)
					{
						this.m_baseStream.Close();
					}
					if (this.m_bufferStream != null)
					{
						this.m_bufferStream.Close();
					}
				}
				finally
				{
					this.m_baseStream = null;
					this.m_bufferStream = null;
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029C8 File Offset: 0x00000BC8
		private static void ValidateParameters(byte[] array, int offset, int count)
		{
			RuntimeChecks.CheckValue(array, "array");
			RuntimeChecks.CheckNonNegative(offset, "offset");
			RuntimeChecks.CheckNonNegative(count, "count");
			RuntimeChecks.Check(array.Length - offset >= count, "Arguments offset and count are invalid");
		}

		// Token: 0x04000015 RID: 21
		private const int c_defaultBufferSize = 40960;

		// Token: 0x04000016 RID: 22
		private const int c_headerLength = 4;

		// Token: 0x04000017 RID: 23
		private readonly CompressionMode m_compressionMode;

		// Token: 0x04000018 RID: 24
		private readonly bool m_leaveOpen;

		// Token: 0x04000019 RID: 25
		private Stream m_baseStream;

		// Token: 0x0400001A RID: 26
		private MemoryStream m_bufferStream = new MemoryStream();

		// Token: 0x0400001B RID: 27
		private byte[] m_innerBuffer;

		// Token: 0x0400001C RID: 28
		private int m_index;

		// Token: 0x0400001D RID: 29
		private XPress9Level m_level;
	}
}
