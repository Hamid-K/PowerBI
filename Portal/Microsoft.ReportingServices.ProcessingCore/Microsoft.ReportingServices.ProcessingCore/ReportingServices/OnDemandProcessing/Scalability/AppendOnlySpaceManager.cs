using System;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200089F RID: 2207
	internal sealed class AppendOnlySpaceManager : ISpaceManager
	{
		// Token: 0x060078F8 RID: 30968 RVA: 0x001F2AA8 File Offset: 0x001F0CA8
		internal AppendOnlySpaceManager()
		{
			this.m_streamEnd = 0L;
		}

		// Token: 0x060078F9 RID: 30969 RVA: 0x001F2AB8 File Offset: 0x001F0CB8
		public void Seek(long offset, SeekOrigin origin)
		{
		}

		// Token: 0x060078FA RID: 30970 RVA: 0x001F2ABA File Offset: 0x001F0CBA
		public void Free(long offset, long size)
		{
			this.m_unuseableBytes += size;
		}

		// Token: 0x060078FB RID: 30971 RVA: 0x001F2ACA File Offset: 0x001F0CCA
		public long AllocateSpace(long size)
		{
			long streamEnd = this.m_streamEnd;
			this.m_streamEnd += size;
			return streamEnd;
		}

		// Token: 0x060078FC RID: 30972 RVA: 0x001F2AE0 File Offset: 0x001F0CE0
		public long Resize(long offset, long oldSize, long newSize)
		{
			this.Free(offset, oldSize);
			return this.AllocateSpace(newSize);
		}

		// Token: 0x1700281C RID: 10268
		// (get) Token: 0x060078FD RID: 30973 RVA: 0x001F2AF1 File Offset: 0x001F0CF1
		// (set) Token: 0x060078FE RID: 30974 RVA: 0x001F2AF9 File Offset: 0x001F0CF9
		public long StreamEnd
		{
			get
			{
				return this.m_streamEnd;
			}
			set
			{
				this.m_streamEnd = value;
			}
		}

		// Token: 0x060078FF RID: 30975 RVA: 0x001F2B04 File Offset: 0x001F0D04
		public void TraceStats()
		{
			Global.Tracer.Trace(TraceLevel.Verbose, "AppendOnlySpaceManager Stats. StreamSize: {0} MB. UnusuableSpace: {1} KB.", new object[]
			{
				this.m_streamEnd / 1048576L,
				this.m_unuseableBytes / 1024L
			});
		}

		// Token: 0x04003CBE RID: 15550
		private long m_streamEnd;

		// Token: 0x04003CBF RID: 15551
		private long m_unuseableBytes;
	}
}
