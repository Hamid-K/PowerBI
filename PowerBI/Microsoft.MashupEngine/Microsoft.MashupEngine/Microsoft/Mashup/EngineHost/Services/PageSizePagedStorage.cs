using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A39 RID: 6713
	internal sealed class PageSizePagedStorage : IPagedStorage, IDisposable
	{
		// Token: 0x0600A9CD RID: 43469 RVA: 0x002315DD File Offset: 0x0022F7DD
		public PageSizePagedStorage(IPagedStorage pagedStorage, int pageSize)
		{
			this.pagedStorage = pagedStorage;
			this.pageSize = pageSize;
		}

		// Token: 0x17002B22 RID: 11042
		// (get) Token: 0x0600A9CE RID: 43470 RVA: 0x002315F3 File Offset: 0x0022F7F3
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
		}

		// Token: 0x17002B23 RID: 11043
		// (get) Token: 0x0600A9CF RID: 43471 RVA: 0x002315FB File Offset: 0x0022F7FB
		public int MaxPageCount
		{
			get
			{
				return this.pagedStorage.MaxPageCount;
			}
		}

		// Token: 0x0600A9D0 RID: 43472 RVA: 0x00231608 File Offset: 0x0022F808
		public Stream OpenPage(int pageIndex, out bool created)
		{
			return new PageSizePagedStorage.LimitStream(this.pagedStorage.OpenPage(pageIndex, out created), (long)this.pageSize);
		}

		// Token: 0x0600A9D1 RID: 43473 RVA: 0x00231623 File Offset: 0x0022F823
		public void CommitPage(Stream stream)
		{
			this.pagedStorage.CommitPage(((PageSizePagedStorage.LimitStream)stream).InnerStream);
		}

		// Token: 0x0600A9D2 RID: 43474 RVA: 0x0023163B File Offset: 0x0022F83B
		public void Dispose()
		{
			this.pagedStorage.Dispose();
		}

		// Token: 0x04005848 RID: 22600
		private readonly IPagedStorage pagedStorage;

		// Token: 0x04005849 RID: 22601
		private readonly int pageSize;

		// Token: 0x02001A3A RID: 6714
		private sealed class LimitStream : DelegatingStream
		{
			// Token: 0x0600A9D3 RID: 43475 RVA: 0x00231648 File Offset: 0x0022F848
			public LimitStream(Stream stream, long limit)
				: base(stream.Take(limit))
			{
				this.innerStream = stream;
			}

			// Token: 0x17002B24 RID: 11044
			// (get) Token: 0x0600A9D4 RID: 43476 RVA: 0x0023165E File Offset: 0x0022F85E
			public Stream InnerStream
			{
				get
				{
					return this.innerStream;
				}
			}

			// Token: 0x0400584A RID: 22602
			private readonly Stream innerStream;
		}
	}
}
