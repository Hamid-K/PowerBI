using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000290 RID: 656
	internal abstract class WriteSnapshotChunkStreamBase : SnapshotChunkStreamBase, ReportProcessing.IErasable
	{
		// Token: 0x060017F9 RID: 6137 RVA: 0x00061AD8 File Offset: 0x0005FCD8
		internal WriteSnapshotChunkStreamBase(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType, string mimeType)
			: base(snapshotDataID, isPermanentSnapshot, chunkName, chunkType)
		{
			this.m_mimeType = mimeType;
		}

		// Token: 0x060017FA RID: 6138
		protected abstract bool DeleteOpenChunk();

		// Token: 0x060017FB RID: 6139
		protected abstract bool DeleteClosedChunk();

		// Token: 0x060017FC RID: 6140 RVA: 0x00061AF0 File Offset: 0x0005FCF0
		public bool Erase()
		{
			if (this.m_isOpen)
			{
				bool flag = this.DeleteOpenChunk();
				if (flag)
				{
					this.m_isOpen = false;
				}
				return flag;
			}
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ChunkWriteStream.DeleteAndClose() - deleting... , id={0}, name='{1}'", new object[] { this.m_snapshotDataID, this.m_chunkName });
			}
			return this.DeleteClosedChunk();
		}

		// Token: 0x040008A2 RID: 2210
		protected bool m_isOpen;
	}
}
