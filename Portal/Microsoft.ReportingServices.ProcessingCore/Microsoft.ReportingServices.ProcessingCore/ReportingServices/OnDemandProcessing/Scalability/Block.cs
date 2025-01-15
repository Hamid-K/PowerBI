using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200089E RID: 2206
	internal sealed class Block
	{
		// Token: 0x060078F1 RID: 30961 RVA: 0x001F2A5F File Offset: 0x001F0C5F
		public Block(long offset, long size)
		{
			this.m_offset = offset;
			this.m_size = size;
		}

		// Token: 0x17002819 RID: 10265
		// (get) Token: 0x060078F2 RID: 30962 RVA: 0x001F2A75 File Offset: 0x001F0C75
		// (set) Token: 0x060078F3 RID: 30963 RVA: 0x001F2A7D File Offset: 0x001F0C7D
		public long Offset
		{
			get
			{
				return this.m_offset;
			}
			set
			{
				this.m_offset = value;
			}
		}

		// Token: 0x1700281A RID: 10266
		// (get) Token: 0x060078F4 RID: 30964 RVA: 0x001F2A86 File Offset: 0x001F0C86
		// (set) Token: 0x060078F5 RID: 30965 RVA: 0x001F2A8E File Offset: 0x001F0C8E
		public long EndOffset
		{
			get
			{
				return this.m_endOffset;
			}
			set
			{
				this.m_endOffset = value;
			}
		}

		// Token: 0x1700281B RID: 10267
		// (get) Token: 0x060078F6 RID: 30966 RVA: 0x001F2A97 File Offset: 0x001F0C97
		// (set) Token: 0x060078F7 RID: 30967 RVA: 0x001F2A9F File Offset: 0x001F0C9F
		public long Size
		{
			get
			{
				return this.m_size;
			}
			set
			{
				this.m_size = value;
			}
		}

		// Token: 0x04003CBB RID: 15547
		private long m_offset;

		// Token: 0x04003CBC RID: 15548
		private long m_endOffset;

		// Token: 0x04003CBD RID: 15549
		private long m_size;
	}
}
