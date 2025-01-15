using System;
using System.IO;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A38 RID: 6712
	internal abstract class PagedStream : Stream
	{
		// Token: 0x0600A9BC RID: 43452 RVA: 0x002311C8 File Offset: 0x0022F3C8
		protected PagedStream(int pageSize)
		{
			this.pageBuffer = new byte[pageSize];
			this.pageIndex = -1;
		}

		// Token: 0x0600A9BD RID: 43453 RVA: 0x002311E3 File Offset: 0x0022F3E3
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Flush();
				this.pageIndex = -1;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17002B1F RID: 11039
		// (get) Token: 0x0600A9BE RID: 43454
		// (set) Token: 0x0600A9BF RID: 43455
		protected abstract int PageCount { get; set; }

		// Token: 0x0600A9C0 RID: 43456
		protected abstract int ReadPage(int page, byte[] buffer);

		// Token: 0x0600A9C1 RID: 43457
		protected abstract void WritePage(int page, byte[] buffer, int length);

		// Token: 0x17002B20 RID: 11040
		// (get) Token: 0x0600A9C2 RID: 43458 RVA: 0x002311FC File Offset: 0x0022F3FC
		// (set) Token: 0x0600A9C3 RID: 43459 RVA: 0x00231217 File Offset: 0x0022F417
		public override long Position
		{
			get
			{
				return (long)this.pageIndex * (long)this.pageBuffer.Length + (long)this.pagePosition;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x17002B21 RID: 11041
		// (get) Token: 0x0600A9C4 RID: 43460 RVA: 0x00231224 File Offset: 0x0022F424
		public override long Length
		{
			get
			{
				this.Flush();
				int pageCount = this.PageCount;
				long num;
				if (this.pageIndex >= pageCount - 1)
				{
					num = (long)this.pageLength;
				}
				else
				{
					byte[] array = new byte[this.pageBuffer.Length];
					num = (long)this.ReadPage(pageCount - 1, array);
				}
				return (long)Math.Max(pageCount - 1, 0) * (long)this.pageBuffer.Length + num;
			}
		}

		// Token: 0x0600A9C5 RID: 43461 RVA: 0x00231284 File Offset: 0x0022F484
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!this.CanSeek)
			{
				throw new InvalidOperationException();
			}
			this.Flush();
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
				num = this.Length + offset;
				break;
			default:
				throw new ArgumentException("origin");
			}
			if (num > this.Length)
			{
				throw new ArgumentException("offset");
			}
			this.SeekToPage((int)(num / (long)this.pageBuffer.Length));
			this.pagePosition = (int)(num % (long)this.pageBuffer.Length);
			return num;
		}

		// Token: 0x0600A9C6 RID: 43462 RVA: 0x00231317 File Offset: 0x0022F517
		public override void Flush()
		{
			if (this.pageModified)
			{
				this.WritePage(this.pageIndex, this.pageBuffer, this.pageLength);
				this.pageModified = false;
			}
		}

		// Token: 0x0600A9C7 RID: 43463 RVA: 0x00231340 File Offset: 0x0022F540
		public override void SetLength(long length)
		{
			this.Flush();
			long position = this.Position;
			int num = (int)(length / (long)this.pageBuffer.Length);
			int num2 = (int)(length % (long)this.pageBuffer.Length);
			if (num2 != 0)
			{
				num++;
			}
			this.PageCount = num;
			this.SeekToPage(num - 1);
			this.pageLength = num2;
			this.pageModified = true;
			this.Flush();
			this.Position = Math.Min(position, length);
		}

		// Token: 0x0600A9C8 RID: 43464 RVA: 0x002313AC File Offset: 0x0022F5AC
		public override int Read(byte[] array, int offset, int count)
		{
			int num = 0;
			while (count > 0)
			{
				if (this.pagePosition == this.pageLength)
				{
					if (this.pageIndex >= this.PageCount - 1)
					{
						break;
					}
					this.SeekToPage(this.pageIndex + 1);
				}
				int num2 = Math.Min(this.pageLength - this.pagePosition, count);
				Buffer.BlockCopy(this.pageBuffer, this.pagePosition, array, offset, num2);
				this.pagePosition += num2;
				num += num2;
				offset += num2;
				count -= num2;
			}
			return num;
		}

		// Token: 0x0600A9C9 RID: 43465 RVA: 0x00231434 File Offset: 0x0022F634
		public override void Write(byte[] array, int offset, int count)
		{
			while (count > 0)
			{
				if (this.pagePosition == this.pageBuffer.Length)
				{
					this.SeekToPage(this.pageIndex + 1);
				}
				int num = Math.Min(this.pageBuffer.Length - this.pagePosition, count);
				Buffer.BlockCopy(array, offset, this.pageBuffer, this.pagePosition, num);
				this.pagePosition += num;
				if (this.pagePosition > this.pageLength)
				{
					this.pageLength = this.pagePosition;
				}
				this.pageModified = true;
				offset += num;
				count -= num;
			}
		}

		// Token: 0x0600A9CA RID: 43466 RVA: 0x002314D0 File Offset: 0x0022F6D0
		public override int ReadByte()
		{
			if (this.pagePosition == this.pageLength)
			{
				if (this.pageIndex >= this.PageCount - 1)
				{
					return -1;
				}
				this.SeekToPage(this.pageIndex + 1);
			}
			byte[] array = this.pageBuffer;
			int num = this.pagePosition;
			this.pagePosition = num + 1;
			return array[num];
		}

		// Token: 0x0600A9CB RID: 43467 RVA: 0x00231524 File Offset: 0x0022F724
		public override void WriteByte(byte value)
		{
			if (this.pagePosition == this.pageBuffer.Length)
			{
				this.SeekToPage(this.pageIndex + 1);
			}
			byte[] array = this.pageBuffer;
			int num = this.pagePosition;
			this.pagePosition = num + 1;
			array[num] = value;
			if (this.pagePosition > this.pageLength)
			{
				this.pageLength = this.pagePosition;
			}
			this.pageModified = true;
		}

		// Token: 0x0600A9CC RID: 43468 RVA: 0x0023158C File Offset: 0x0022F78C
		protected void SeekToPage(int page)
		{
			if (page != this.pageIndex)
			{
				if (page < 0)
				{
					throw new ArgumentOutOfRangeException("page");
				}
				this.Flush();
				this.pageIndex = page;
				this.pageLength = this.ReadPage(this.pageIndex, this.pageBuffer);
				this.pagePosition = 0;
			}
		}

		// Token: 0x04005843 RID: 22595
		private readonly byte[] pageBuffer;

		// Token: 0x04005844 RID: 22596
		private int pageIndex;

		// Token: 0x04005845 RID: 22597
		private int pagePosition;

		// Token: 0x04005846 RID: 22598
		private int pageLength;

		// Token: 0x04005847 RID: 22599
		private bool pageModified;
	}
}
