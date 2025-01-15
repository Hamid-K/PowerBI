using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008A3 RID: 2211
	internal sealed class PromoteLocalitySpaceManager : ISpaceManager
	{
		// Token: 0x06007918 RID: 31000 RVA: 0x001F3027 File Offset: 0x001F1227
		internal PromoteLocalitySpaceManager(long blockSize)
		{
			this.m_blockSize = blockSize;
			this.m_position = 0L;
			this.m_streamEnd = 0L;
			this.m_blocks = new List<FileBlock>(10);
		}

		// Token: 0x06007919 RID: 31001 RVA: 0x001F3054 File Offset: 0x001F1254
		public void Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.m_position = offset;
				return;
			case SeekOrigin.Current:
				this.m_position += offset;
				return;
			case SeekOrigin.End:
				this.m_position = this.m_streamEnd + offset;
				return;
			default:
				Global.Tracer.Assert(false);
				return;
			}
		}

		// Token: 0x0600791A RID: 31002 RVA: 0x001F30A8 File Offset: 0x001F12A8
		private FileBlock GetBlock(long offset)
		{
			FileBlock fileBlock = null;
			int num = (int)(offset / this.m_blockSize);
			if (num < this.m_blocks.Count)
			{
				fileBlock = this.m_blocks[num];
			}
			return fileBlock;
		}

		// Token: 0x0600791B RID: 31003 RVA: 0x001F30E0 File Offset: 0x001F12E0
		private FileBlock GetOrCreateBlock(long offset)
		{
			int num = (int)(offset / this.m_blockSize);
			if (num >= this.m_blocks.Count)
			{
				for (int i = this.m_blocks.Count - 1; i < num; i++)
				{
					this.m_blocks.Add(null);
				}
				this.m_blocks.Add(new FileBlock());
			}
			FileBlock fileBlock = this.m_blocks[num];
			if (fileBlock == null)
			{
				fileBlock = new FileBlock();
				this.m_blocks[num] = fileBlock;
			}
			return fileBlock;
		}

		// Token: 0x0600791C RID: 31004 RVA: 0x001F315D File Offset: 0x001F135D
		public void Free(long offset, long size)
		{
			this.GetOrCreateBlock(offset).Free(offset, size);
		}

		// Token: 0x0600791D RID: 31005 RVA: 0x001F3170 File Offset: 0x001F1370
		public long AllocateSpace(long size)
		{
			long num = this.SearchBlock(this.m_position, size);
			long num2 = this.m_position - this.m_blockSize;
			long num3 = this.m_position + this.m_blockSize;
			while (num == -1L && (num2 >= 0L || num3 < this.m_streamEnd))
			{
				if (num2 >= 0L)
				{
					num = this.SearchBlock(num2, size);
				}
				if (num3 < this.m_streamEnd && num == -1L)
				{
					num = this.SearchBlock(num3, size);
				}
				num2 -= this.m_blockSize;
				num3 += this.m_blockSize;
			}
			if (num == -1L)
			{
				num = this.m_streamEnd;
				this.m_streamEnd += size;
			}
			return num;
		}

		// Token: 0x0600791E RID: 31006 RVA: 0x001F3214 File Offset: 0x001F1414
		private long SearchBlock(long offset, long size)
		{
			long num = -1L;
			FileBlock block = this.GetBlock(offset);
			if (block != null)
			{
				num = block.AllocateSpace(size);
			}
			return num;
		}

		// Token: 0x0600791F RID: 31007 RVA: 0x001F3238 File Offset: 0x001F1438
		public long Resize(long offset, long oldSize, long newSize)
		{
			this.Free(offset, oldSize);
			return this.AllocateSpace(newSize);
		}

		// Token: 0x17002824 RID: 10276
		// (get) Token: 0x06007920 RID: 31008 RVA: 0x001F3249 File Offset: 0x001F1449
		// (set) Token: 0x06007921 RID: 31009 RVA: 0x001F3251 File Offset: 0x001F1451
		public long StreamEnd
		{
			get
			{
				return this.m_streamEnd;
			}
			set
			{
				this.m_streamEnd = value;
				this.m_position = value;
			}
		}

		// Token: 0x06007922 RID: 31010 RVA: 0x001F3264 File Offset: 0x001F1464
		public void TraceStats()
		{
			Global.Tracer.Trace(TraceLevel.Verbose, "LocalitySpaceManager Stats. StreamSize: {0} MB. FileBlocks:", new object[] { this.m_streamEnd / 1048576L });
			for (int i = 0; i < this.m_blocks.Count; i++)
			{
				FileBlock fileBlock = this.m_blocks[i];
				if (fileBlock != null)
				{
					fileBlock.TraceStats(((long)i * this.m_blockSize / 1048576L).ToString(CultureInfo.InvariantCulture));
				}
			}
		}

		// Token: 0x04003CD2 RID: 15570
		private long m_blockSize;

		// Token: 0x04003CD3 RID: 15571
		private long m_position;

		// Token: 0x04003CD4 RID: 15572
		private long m_streamEnd;

		// Token: 0x04003CD5 RID: 15573
		private List<FileBlock> m_blocks;
	}
}
