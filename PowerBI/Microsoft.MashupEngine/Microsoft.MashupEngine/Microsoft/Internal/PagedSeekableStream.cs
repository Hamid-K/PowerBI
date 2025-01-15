using System;
using System.IO;

namespace Microsoft.Internal
{
	// Token: 0x020001B7 RID: 439
	internal class PagedSeekableStream : Stream
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x0000FF60 File Offset: 0x0000E160
		public PagedSeekableStream(long length, int pageSize, Func<int, Stream> getBufferedPage)
		{
			this.length = length;
			this.pageSize = pageSize;
			this.getBufferedPage = getBufferedPage;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00002105 File Offset: 0x00000305
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0000FF7D File Offset: 0x0000E17D
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0000FF85 File Offset: 0x0000E185
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x0000FF99 File Offset: 0x0000E199
		public override long Position
		{
			get
			{
				return this.GetPosition(this.PageIndex, this.PagePosition);
			}
			set
			{
				this.Seek(value);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0000FFA2 File Offset: 0x0000E1A2
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0000FFAA File Offset: 0x0000E1AA
		private int PageIndex
		{
			get
			{
				return this.pageIndex;
			}
			set
			{
				this.SeekPage(value);
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0000FFB3 File Offset: 0x0000E1B3
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0000FFBB File Offset: 0x0000E1BB
		private int PagePosition
		{
			get
			{
				return this.pagePosition;
			}
			set
			{
				if (value >= this.pageSize)
				{
					this.Seek(this.GetPosition(this.PageIndex, value));
					return;
				}
				this.SeekPagePosition(value);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0000FFE1 File Offset: 0x0000E1E1
		private Stream PageStream
		{
			get
			{
				if (this.pageStream == null)
				{
					this.pageStream = this.getBufferedPage(this.PageIndex);
					this.pageStream.Position = (long)this.PagePosition;
				}
				return this.pageStream;
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Flush()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001001C File Offset: 0x0000E21C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			while (count > 0 && this.Position < this.Length)
			{
				int i = this.CountInPage(count);
				while (i > 0)
				{
					int num2 = this.PageStream.Read(buffer, offset, i);
					offset += num2;
					count -= num2;
					i -= num2;
					num += num2;
					if (num2 == 0)
					{
						count = 0;
						break;
					}
				}
				this.PagePosition = (int)this.PageStream.Position;
			}
			return num;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001008C File Offset: 0x0000E28C
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Length - offset;
				break;
			default:
				throw new InvalidOperationException();
			}
			this.Seek(num);
			return num;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void SetLength(long value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000100D5 File Offset: 0x0000E2D5
		public override void Close()
		{
			if (this.pageStream != null)
			{
				this.pageStream.Dispose();
				this.pageStream = null;
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000100F4 File Offset: 0x0000E2F4
		private void Seek(long position)
		{
			long num2;
			long num = Math.DivRem(position, (long)this.pageSize, out num2);
			this.SeekPage((int)num);
			this.SeekPagePosition((int)num2);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00010121 File Offset: 0x0000E321
		private void SeekPage(int pageIndex)
		{
			if (this.pageIndex != pageIndex && this.pageStream != null)
			{
				this.pageStream.Dispose();
				this.pageStream = null;
			}
			this.pageIndex = pageIndex;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001014D File Offset: 0x0000E34D
		private void SeekPagePosition(int pagePosition)
		{
			this.pagePosition = pagePosition;
			if (this.pageStream != null)
			{
				this.pageStream.Position = (long)this.pagePosition;
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00010170 File Offset: 0x0000E370
		private long GetPosition(int pageIndex, int pagePosition)
		{
			return (long)pageIndex * (long)this.pageSize + (long)pagePosition;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001017F File Offset: 0x0000E37F
		private int CountInPage(int count)
		{
			return Math.Min(this.pageSize - this.PagePosition, count);
		}

		// Token: 0x040004BF RID: 1215
		private readonly long length;

		// Token: 0x040004C0 RID: 1216
		private readonly int pageSize;

		// Token: 0x040004C1 RID: 1217
		private readonly Func<int, Stream> getBufferedPage;

		// Token: 0x040004C2 RID: 1218
		private int pageIndex;

		// Token: 0x040004C3 RID: 1219
		private int pagePosition;

		// Token: 0x040004C4 RID: 1220
		private Stream pageStream;
	}
}
