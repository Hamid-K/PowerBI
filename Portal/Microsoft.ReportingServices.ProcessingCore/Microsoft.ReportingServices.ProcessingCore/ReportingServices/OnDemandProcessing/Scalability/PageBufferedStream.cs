using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200084C RID: 2124
	internal sealed class PageBufferedStream : Stream
	{
		// Token: 0x0600767D RID: 30333 RVA: 0x001EB1A8 File Offset: 0x001E93A8
		public PageBufferedStream(Stream stream, int bytesPerPage, int cachePageCount)
		{
			if (!stream.CanSeek || !stream.CanRead || !stream.CanWrite)
			{
				Global.Tracer.Assert(false, "PageBufferedStream: Must be able to Seek, Read, and Write stream");
			}
			this.m_bytesPerPage = bytesPerPage;
			this.m_pageCacheCapacity = cachePageCount;
			this.m_stream = stream;
			this.m_length = stream.Length;
			this.m_pageCache = new Dictionary<long, PageBufferedStream.CachePage>(this.m_pageCacheCapacity);
		}

		// Token: 0x170027B2 RID: 10162
		// (get) Token: 0x0600767E RID: 30334 RVA: 0x001EB215 File Offset: 0x001E9415
		// (set) Token: 0x0600767F RID: 30335 RVA: 0x001EB21D File Offset: 0x001E941D
		public bool FreezePageAllocations
		{
			get
			{
				return this.m_freezePageAllocations;
			}
			set
			{
				this.m_freezePageAllocations = value;
			}
		}

		// Token: 0x170027B3 RID: 10163
		// (get) Token: 0x06007680 RID: 30336 RVA: 0x001EB226 File Offset: 0x001E9426
		internal int PageCount
		{
			get
			{
				return this.m_pageCache.Count;
			}
		}

		// Token: 0x170027B4 RID: 10164
		// (get) Token: 0x06007681 RID: 30337 RVA: 0x001EB233 File Offset: 0x001E9433
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170027B5 RID: 10165
		// (get) Token: 0x06007682 RID: 30338 RVA: 0x001EB236 File Offset: 0x001E9436
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170027B6 RID: 10166
		// (get) Token: 0x06007683 RID: 30339 RVA: 0x001EB239 File Offset: 0x001E9439
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007684 RID: 30340 RVA: 0x001EB23C File Offset: 0x001E943C
		public override void Flush()
		{
			foreach (long num in this.m_pageCache.Keys)
			{
				this.FlushPage(this.m_pageCache[num], num);
			}
		}

		// Token: 0x170027B7 RID: 10167
		// (get) Token: 0x06007685 RID: 30341 RVA: 0x001EB2A0 File Offset: 0x001E94A0
		public override long Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x170027B8 RID: 10168
		// (get) Token: 0x06007686 RID: 30342 RVA: 0x001EB2A8 File Offset: 0x001E94A8
		// (set) Token: 0x06007687 RID: 30343 RVA: 0x001EB2B0 File Offset: 0x001E94B0
		public override long Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.m_position = value;
			}
		}

		// Token: 0x06007688 RID: 30344 RVA: 0x001EB2BC File Offset: 0x001E94BC
		public override int ReadByte()
		{
			PageBufferedStream.CachePage page = this.GetPage(this.m_position);
			int num = this.CalcOffsetWithinPage(this.m_position);
			this.UpdatePosition(1L);
			return (int)page.Buffer[num];
		}

		// Token: 0x06007689 RID: 30345 RVA: 0x001EB2F4 File Offset: 0x001E94F4
		public override void WriteByte(byte value)
		{
			PageBufferedStream.CachePage page = this.GetPage(this.m_position);
			int num = this.CalcOffsetWithinPage(this.m_position);
			this.UpdatePosition(1L);
			page.Buffer[num] = value;
			page.Dirty = true;
		}

		// Token: 0x0600768A RID: 30346 RVA: 0x001EB334 File Offset: 0x001E9534
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = count;
			long num2 = this.m_position + (long)count;
			while (this.m_position < num2)
			{
				PageBufferedStream.CachePage page = this.GetPage(this.m_position);
				int num3 = this.CalcOffsetWithinPage(this.m_position);
				byte[] buffer2 = page.Buffer;
				int num4 = Math.Min(buffer2.Length - num3, count);
				Array.Copy(buffer2, num3, buffer, offset, num4);
				this.UpdatePosition((long)num4);
				count -= num4;
				offset += num4;
			}
			return num;
		}

		// Token: 0x0600768B RID: 30347 RVA: 0x001EB3A0 File Offset: 0x001E95A0
		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.m_position = offset;
				break;
			case SeekOrigin.Current:
				this.m_position += offset;
				break;
			case SeekOrigin.End:
				this.m_position = this.m_length + offset;
				break;
			default:
				Global.Tracer.Assert(false);
				break;
			}
			return this.m_position;
		}

		// Token: 0x0600768C RID: 30348 RVA: 0x001EB3FB File Offset: 0x001E95FB
		public override void SetLength(long value)
		{
			this.m_length = value;
		}

		// Token: 0x0600768D RID: 30349 RVA: 0x001EB404 File Offset: 0x001E9604
		public override void Write(byte[] buffer, int offset, int count)
		{
			long num = this.m_position + (long)count;
			while (this.m_position < num)
			{
				PageBufferedStream.CachePage page = this.GetPage(this.m_position);
				int num2 = this.CalcOffsetWithinPage(this.m_position);
				byte[] buffer2 = page.Buffer;
				int num3 = Math.Min(buffer2.Length - num2, count);
				Array.Copy(buffer, offset, buffer2, num2, num3);
				this.UpdatePosition((long)num3);
				count -= num3;
				offset += num3;
				page.Dirty = true;
			}
		}

		// Token: 0x0600768E RID: 30350 RVA: 0x001EB474 File Offset: 0x001E9674
		public override void Close()
		{
			this.m_stream.Close();
		}

		// Token: 0x0600768F RID: 30351 RVA: 0x001EB481 File Offset: 0x001E9681
		private void UpdatePosition(long moveBy)
		{
			this.m_position += moveBy;
			if (this.m_position > this.m_length)
			{
				this.m_length = this.m_position;
			}
		}

		// Token: 0x06007690 RID: 30352 RVA: 0x001EB4AC File Offset: 0x001E96AC
		private PageBufferedStream.CachePage GetPage(long fileOffset)
		{
			long num = this.CalcPageNum(fileOffset);
			PageBufferedStream.CachePage cachePage = null;
			if (!this.m_pageCache.TryGetValue(num, out cachePage))
			{
				bool flag = false;
				if (this.m_pageCache.Count == this.m_pageCacheCapacity || (this.m_freezePageAllocations && this.m_pageCache.Count > 0))
				{
					cachePage = this.m_firstPageToEvict;
					long pageNumber = cachePage.PageNumber;
					this.m_firstPageToEvict = cachePage.NextPage;
					this.m_pageCache.Remove(pageNumber);
					this.FlushPage(cachePage, pageNumber);
					cachePage.PageNumber = num;
				}
				else
				{
					cachePage = new PageBufferedStream.CachePage(this.m_bytesPerPage, num);
					flag = true;
				}
				long num2 = this.CalcPageOffset(num);
				if (num2 < this.m_length)
				{
					this.m_stream.Seek(num2, SeekOrigin.Begin);
					cachePage.Read(this.m_stream);
				}
				else if (!flag)
				{
					cachePage.InitBuffer();
				}
				this.m_pageCache[num] = cachePage;
				if (this.m_firstPageToEvict == null)
				{
					this.m_firstPageToEvict = cachePage;
				}
				if (this.m_lastPageToEvict != null)
				{
					this.m_lastPageToEvict.NextPage = cachePage;
				}
				this.m_lastPageToEvict = cachePage;
			}
			return cachePage;
		}

		// Token: 0x06007691 RID: 30353 RVA: 0x001EB5BC File Offset: 0x001E97BC
		private void FlushPage(PageBufferedStream.CachePage page, long pageNum)
		{
			if (page.Dirty)
			{
				long num = this.CalcPageOffset(pageNum);
				this.m_stream.Seek(num, SeekOrigin.Begin);
				page.Write(this.m_stream);
			}
		}

		// Token: 0x06007692 RID: 30354 RVA: 0x001EB5F3 File Offset: 0x001E97F3
		private long CalcPageNum(long fileOffset)
		{
			return fileOffset / (long)this.m_bytesPerPage;
		}

		// Token: 0x06007693 RID: 30355 RVA: 0x001EB5FE File Offset: 0x001E97FE
		private long CalcPageOffset(long pageNum)
		{
			return pageNum * (long)this.m_bytesPerPage;
		}

		// Token: 0x06007694 RID: 30356 RVA: 0x001EB609 File Offset: 0x001E9809
		private int CalcOffsetWithinPage(long fileOffset)
		{
			return (int)(fileOffset % (long)this.m_bytesPerPage);
		}

		// Token: 0x04003C04 RID: 15364
		private readonly int m_bytesPerPage;

		// Token: 0x04003C05 RID: 15365
		private readonly int m_pageCacheCapacity;

		// Token: 0x04003C06 RID: 15366
		private Stream m_stream;

		// Token: 0x04003C07 RID: 15367
		private Dictionary<long, PageBufferedStream.CachePage> m_pageCache;

		// Token: 0x04003C08 RID: 15368
		private PageBufferedStream.CachePage m_firstPageToEvict;

		// Token: 0x04003C09 RID: 15369
		private PageBufferedStream.CachePage m_lastPageToEvict;

		// Token: 0x04003C0A RID: 15370
		private long m_position;

		// Token: 0x04003C0B RID: 15371
		private long m_length;

		// Token: 0x04003C0C RID: 15372
		private bool m_freezePageAllocations;

		// Token: 0x02000D09 RID: 3337
		private sealed class CachePage
		{
			// Token: 0x06008E8D RID: 36493 RVA: 0x0024547C File Offset: 0x0024367C
			public CachePage(int size, long pageNum)
			{
				this.Buffer = new byte[size];
				this.Dirty = false;
				this.NextPage = null;
				this.PageNumber = pageNum;
			}

			// Token: 0x06008E8E RID: 36494 RVA: 0x002454A8 File Offset: 0x002436A8
			public void Read(Stream stream)
			{
				int num = 0;
				int num2 = this.Buffer.Length;
				int num3;
				do
				{
					num3 = stream.Read(this.Buffer, num, num2);
					num += num3;
					num2 -= num3;
				}
				while (num3 > 0 && num2 > 0);
				Global.Tracer.Assert(num == this.Buffer.Length, "Error filling buffer page");
				this.Dirty = false;
			}

			// Token: 0x06008E8F RID: 36495 RVA: 0x00245502 File Offset: 0x00243702
			internal void InitBuffer()
			{
				this.Dirty = false;
			}

			// Token: 0x06008E90 RID: 36496 RVA: 0x0024550B File Offset: 0x0024370B
			public void Write(Stream stream)
			{
				stream.Write(this.Buffer, 0, this.Buffer.Length);
				this.Dirty = false;
			}

			// Token: 0x0400502A RID: 20522
			internal byte[] Buffer;

			// Token: 0x0400502B RID: 20523
			internal bool Dirty;

			// Token: 0x0400502C RID: 20524
			internal PageBufferedStream.CachePage NextPage;

			// Token: 0x0400502D RID: 20525
			internal long PageNumber;
		}
	}
}
