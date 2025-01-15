using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x02000280 RID: 640
	internal class ForwardSeekableReadOnlyStream : Stream
	{
		// Token: 0x06001A56 RID: 6742 RVA: 0x00034EF3 File Offset: 0x000330F3
		public ForwardSeekableReadOnlyStream(Stream stream)
		{
			this.stream = stream;
			this.position = 0L;
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override long Length
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x00034F0A File Offset: 0x0003310A
		// (set) Token: 0x06001A5C RID: 6748 RVA: 0x00034F14 File Offset: 0x00033114
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				if (value < this.position)
				{
					throw new InvalidOperationException();
				}
				long num = value - this.position;
				while (num > 0L && this.ReadByte() != -1)
				{
					num -= 1L;
				}
			}
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00034F4E File Offset: 0x0003314E
		public override void Close()
		{
			if (this.stream != null)
			{
				this.stream.Close();
				this.stream = null;
			}
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Flush()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00034F6A File Offset: 0x0003316A
		public override int ReadByte()
		{
			int num = this.stream.ReadByte();
			if (num != -1)
			{
				this.position += 1L;
			}
			return num;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00034F8C File Offset: 0x0003318C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.stream.Read(buffer, offset, count);
			this.position += (long)num;
			return num;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00034FB8 File Offset: 0x000331B8
		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.Position = offset;
				break;
			case SeekOrigin.Current:
				this.Position += offset;
				break;
			case SeekOrigin.End:
				this.Position = this.Length - offset;
				break;
			default:
				throw new InvalidOperationException();
			}
			return this.Position;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void SetLength(long value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x040007CB RID: 1995
		private Stream stream;

		// Token: 0x040007CC RID: 1996
		private long position;
	}
}
