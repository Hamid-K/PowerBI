using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000843 RID: 2115
	internal sealed class IndexTable : IIndexStrategy
	{
		// Token: 0x0600762F RID: 30255 RVA: 0x001E9D28 File Offset: 0x001E7F28
		public IndexTable(IStreamHandler streamCreator, int pageSize, int cacheSize)
		{
			if (pageSize % 8 != 0)
			{
				Global.Tracer.Assert(false, "Page size must be divisible by value size: {0}", new object[] { 8 });
			}
			this.m_streamCreator = streamCreator;
			this.m_stream = null;
			this.m_nextTempId = -1L;
			this.m_pageSize = pageSize;
			this.m_cacheSize = cacheSize;
			this.m_pageCache = new Dictionary<int, IndexTablePage>(this.m_cacheSize);
			this.m_queueFirstPage = null;
			this.m_queueLastPage = null;
			this.m_slotsPerPage = this.m_pageSize / 8;
			this.m_idShift = (int)Math.Log((double)this.m_slotsPerPage, 2.0);
		}

		// Token: 0x06007630 RID: 30256 RVA: 0x001E9DCC File Offset: 0x001E7FCC
		public ReferenceID GenerateTempId()
		{
			long nextTempId = this.m_nextTempId;
			this.m_nextTempId = nextTempId - 1L;
			return new ReferenceID(nextTempId);
		}

		// Token: 0x06007631 RID: 30257 RVA: 0x001E9DF0 File Offset: 0x001E7FF0
		public ReferenceID GenerateId(ReferenceID tempId)
		{
			ReferenceID referenceID = tempId;
			if (tempId.IsTemporary)
			{
				if (this.m_nextIdPageSlot >= (long)this.m_slotsPerPage)
				{
					this.m_nextIdPageSlot = 0L;
					this.m_nextIdPageNum += 1L;
				}
				long num = this.m_nextIdPageSlot;
				num |= this.m_nextIdPageNum << this.m_idShift;
				referenceID = new ReferenceID(num);
				this.m_nextIdPageSlot += 1L;
			}
			return referenceID;
		}

		// Token: 0x06007632 RID: 30258 RVA: 0x001E9E60 File Offset: 0x001E8060
		public void Update(ReferenceID id, long value)
		{
			IndexTablePage page = this.GetPage(id.Value);
			this.WriteValue(id.Value, page, value);
		}

		// Token: 0x06007633 RID: 30259 RVA: 0x001E9E8C File Offset: 0x001E808C
		public long Retrieve(ReferenceID id)
		{
			IndexTablePage page = this.GetPage(id.Value);
			return this.ReadValue(id.Value, page);
		}

		// Token: 0x06007634 RID: 30260 RVA: 0x001E9EB5 File Offset: 0x001E80B5
		public void Close()
		{
			if (this.m_stream != null)
			{
				this.m_stream.Close();
				this.m_stream = null;
			}
		}

		// Token: 0x06007635 RID: 30261 RVA: 0x001E9ED4 File Offset: 0x001E80D4
		private IndexTablePage GetPage(long id)
		{
			int num = this.CalcPageNum(id);
			IndexTablePage indexTablePage = null;
			if (!this.m_pageCache.TryGetValue(num, out indexTablePage))
			{
				if (this.m_pageCache.Count == this.m_cacheSize)
				{
					if (this.m_stream == null)
					{
						this.m_stream = this.m_streamCreator.OpenStream();
						this.m_streamCreator = null;
						if (!this.m_stream.CanSeek || !this.m_stream.CanRead || !this.m_stream.CanWrite)
						{
							Global.Tracer.Assert(false, "Must be able to Seek, Read, and Write stream");
						}
					}
					indexTablePage = this.QueueExtractFirst();
					int pageNumber = indexTablePage.PageNumber;
					this.m_pageCache.Remove(pageNumber);
					if (indexTablePage.Dirty)
					{
						long num2 = this.CalcPageOffset((long)pageNumber);
						this.m_stream.Seek(num2, SeekOrigin.Begin);
						indexTablePage.Write(this.m_stream);
					}
					long num3 = this.CalcPageOffset((long)num);
					this.m_stream.Seek(num3, SeekOrigin.Begin);
					indexTablePage.Read(this.m_stream);
				}
				else
				{
					indexTablePage = new IndexTablePage(this.m_pageSize);
				}
				indexTablePage.PageNumber = num;
				this.m_pageCache[num] = indexTablePage;
				this.QueueAppendPage(indexTablePage);
			}
			return indexTablePage;
		}

		// Token: 0x06007636 RID: 30262 RVA: 0x001EA000 File Offset: 0x001E8200
		private long ReadValue(long id, IndexTablePage page)
		{
			long num = 0L;
			byte[] buffer = page.Buffer;
			int num2 = this.CalcValueOffset(id);
			int num3 = num2 + 8;
			for (int i = num2; i < num3; i++)
			{
				num <<= 8;
				num |= (long)((ulong)buffer[i]);
			}
			return num;
		}

		// Token: 0x06007637 RID: 30263 RVA: 0x001EA03C File Offset: 0x001E823C
		private void WriteValue(long id, IndexTablePage page, long value)
		{
			byte[] buffer = page.Buffer;
			int num = this.CalcValueOffset(id);
			for (int i = num + 8 - 1; i >= num; i--)
			{
				buffer[i] = (byte)value;
				value >>= 8;
			}
			page.Dirty = true;
		}

		// Token: 0x06007638 RID: 30264 RVA: 0x001EA079 File Offset: 0x001E8279
		private int CalcPageNum(long id)
		{
			return (int)(id >> this.m_idShift);
		}

		// Token: 0x06007639 RID: 30265 RVA: 0x001EA087 File Offset: 0x001E8287
		private long CalcPageOffset(long pageNum)
		{
			return pageNum * (long)this.m_pageSize;
		}

		// Token: 0x0600763A RID: 30266 RVA: 0x001EA092 File Offset: 0x001E8292
		private int CalcValueOffset(long id)
		{
			return (int)((ulong)((ulong)id << 64 - this.m_idShift) >> 64 - this.m_idShift) * 8;
		}

		// Token: 0x170027AC RID: 10156
		// (get) Token: 0x0600763B RID: 30267 RVA: 0x001EA0B2 File Offset: 0x001E82B2
		public ReferenceID MaxId
		{
			get
			{
				return new ReferenceID((this.m_nextTempId + 1L) * -1L);
			}
		}

		// Token: 0x0600763C RID: 30268 RVA: 0x001EA0C5 File Offset: 0x001E82C5
		private void QueueAppendPage(IndexTablePage page)
		{
			if (this.m_queueFirstPage == null)
			{
				this.m_queueFirstPage = page;
				this.m_queueLastPage = page;
				return;
			}
			page.PreviousPage = this.m_queueLastPage;
			this.m_queueLastPage.NextPage = page;
			this.m_queueLastPage = page;
		}

		// Token: 0x0600763D RID: 30269 RVA: 0x001EA100 File Offset: 0x001E8300
		private IndexTablePage QueueExtractFirst()
		{
			if (this.m_queueFirstPage == null)
			{
				return null;
			}
			IndexTablePage queueFirstPage = this.m_queueFirstPage;
			this.m_queueFirstPage = queueFirstPage.NextPage;
			queueFirstPage.NextPage = null;
			if (this.m_queueFirstPage == null)
			{
				this.m_queueLastPage = null;
			}
			else
			{
				this.m_queueFirstPage.PreviousPage = null;
			}
			return queueFirstPage;
		}

		// Token: 0x04003BB8 RID: 15288
		private Dictionary<int, IndexTablePage> m_pageCache;

		// Token: 0x04003BB9 RID: 15289
		private IndexTablePage m_queueFirstPage;

		// Token: 0x04003BBA RID: 15290
		private IndexTablePage m_queueLastPage;

		// Token: 0x04003BBB RID: 15291
		private long m_nextTempId;

		// Token: 0x04003BBC RID: 15292
		private int m_pageSize;

		// Token: 0x04003BBD RID: 15293
		private int m_cacheSize;

		// Token: 0x04003BBE RID: 15294
		private Stream m_stream;

		// Token: 0x04003BBF RID: 15295
		private IStreamHandler m_streamCreator;

		// Token: 0x04003BC0 RID: 15296
		private long m_nextIdPageNum;

		// Token: 0x04003BC1 RID: 15297
		private long m_nextIdPageSlot;

		// Token: 0x04003BC2 RID: 15298
		private readonly int m_slotsPerPage;

		// Token: 0x04003BC3 RID: 15299
		private readonly int m_idShift;

		// Token: 0x04003BC4 RID: 15300
		private const int m_valueSize = 8;
	}
}
