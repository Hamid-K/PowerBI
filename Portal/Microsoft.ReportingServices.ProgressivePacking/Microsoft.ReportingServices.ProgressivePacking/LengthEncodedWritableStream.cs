using System;
using System.IO;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x0200000D RID: 13
	internal sealed class LengthEncodedWritableStream : Stream
	{
		// Token: 0x0600003B RID: 59 RVA: 0x0000272C File Offset: 0x0000092C
		internal LengthEncodedWritableStream(BinaryWriter writer, string name)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name");
			}
			this.m_writer = writer;
			this.m_name = name;
			this.m_bufferStream = new MemoryStream();
			this.m_length = 0L;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002781 File Offset: 0x00000981
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002784 File Offset: 0x00000984
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002787 File Offset: 0x00000987
		public override bool CanWrite
		{
			get
			{
				return this.m_writer.BaseStream.CanWrite;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002799 File Offset: 0x00000999
		public override void Flush()
		{
			this.m_bufferStream.Flush();
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000027A6 File Offset: 0x000009A6
		public override long Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000027AE File Offset: 0x000009AE
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000027B6 File Offset: 0x000009B6
		public override long Position
		{
			get
			{
				return this.m_length;
			}
			set
			{
				throw new NotSupportedException("LengthEncodedWritableStream.set_Position");
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027C2 File Offset: 0x000009C2
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("LengthEncodedWritableStream.Read");
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000027CE File Offset: 0x000009CE
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("LengthEncodedWritableStream.Seek");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000027DA File Offset: 0x000009DA
		public override void SetLength(long value)
		{
			throw new NotSupportedException("LengthEncodedWritableStream.SetLength");
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000027E6 File Offset: 0x000009E6
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_bufferStream.Write(buffer, offset, count);
			this.m_length += (long)count;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002808 File Offset: 0x00000A08
		public override void Close()
		{
			if (!this.m_closed)
			{
				this.m_closed = true;
				try
				{
					this.m_writer.Write(this.m_name);
					MessageUtil.WriteByteArray(this.m_writer, this.m_bufferStream.GetBuffer(), 0, checked((int)this.m_bufferStream.Length));
				}
				finally
				{
					this.m_bufferStream.Close();
					base.Close();
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000287C File Offset: 0x00000A7C
		internal bool Closed
		{
			get
			{
				return this.m_closed;
			}
		}

		// Token: 0x0400001A RID: 26
		private MemoryStream m_bufferStream;

		// Token: 0x0400001B RID: 27
		private BinaryWriter m_writer;

		// Token: 0x0400001C RID: 28
		private string m_name;

		// Token: 0x0400001D RID: 29
		private bool m_closed;

		// Token: 0x0400001E RID: 30
		private long m_length;
	}
}
