using System;
using System.IO;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x0200000C RID: 12
	internal class LengthEncodedReadableStream : Stream
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000263F File Offset: 0x0000083F
		internal LengthEncodedReadableStream(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.m_reader = reader;
			this.m_length = this.m_reader.ReadInt32();
			this.m_position = 0;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002674 File Offset: 0x00000874
		public override bool CanRead
		{
			get
			{
				return this.m_reader.BaseStream.CanRead;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002686 File Offset: 0x00000886
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002689 File Offset: 0x00000889
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000268C File Offset: 0x0000088C
		public override void Flush()
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000268E File Offset: 0x0000088E
		public override long Length
		{
			get
			{
				return (long)this.m_length;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002697 File Offset: 0x00000897
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000026A0 File Offset: 0x000008A0
		public override long Position
		{
			get
			{
				return (long)this.m_position;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026A8 File Offset: 0x000008A8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.m_length == this.m_position)
			{
				return 0;
			}
			int num = Math.Min(count, this.m_length - this.m_position);
			int num2 = this.m_reader.Read(buffer, offset, num);
			this.m_position += num2;
			return num2;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026F7 File Offset: 0x000008F7
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026FE File Offset: 0x000008FE
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002705 File Offset: 0x00000905
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000270C File Offset: 0x0000090C
		public override void Close()
		{
			if (!this.m_closed)
			{
				this.m_closed = true;
				base.Close();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002723 File Offset: 0x00000923
		internal bool Closed
		{
			get
			{
				return this.m_closed;
			}
		}

		// Token: 0x04000016 RID: 22
		private BinaryReader m_reader;

		// Token: 0x04000017 RID: 23
		private int m_length;

		// Token: 0x04000018 RID: 24
		private int m_position;

		// Token: 0x04000019 RID: 25
		private bool m_closed;
	}
}
