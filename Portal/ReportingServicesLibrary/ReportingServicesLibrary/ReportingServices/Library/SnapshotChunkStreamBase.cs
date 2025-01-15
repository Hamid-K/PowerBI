using System;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200028F RID: 655
	internal abstract class SnapshotChunkStreamBase : Stream
	{
		// Token: 0x060017EF RID: 6127 RVA: 0x00061A30 File Offset: 0x0005FC30
		internal SnapshotChunkStreamBase(Guid snapshotDataID, bool isPermanentSnapshot, string chunkName, int chunkType)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ChunkWriteStream - constructor({0}, '{1}', {2})", new object[] { snapshotDataID, chunkName, chunkType });
			}
			this.m_snapshotDataID = snapshotDataID;
			this.m_chunkName = chunkName;
			this.m_chunkType = chunkType;
			this.m_isPermanentSnapshot = isPermanentSnapshot;
			this.m_IsAvailable = false;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00061AA0 File Offset: 0x0005FCA0
		internal ChunkFlags Flags
		{
			get
			{
				return this.m_chunkFlags;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x00061AA8 File Offset: 0x0005FCA8
		public Guid SnapshotDataID
		{
			get
			{
				return this.m_snapshotDataID;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00061AB0 File Offset: 0x0005FCB0
		public bool IsPermanent
		{
			get
			{
				return this.m_isPermanentSnapshot;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0006185B File Offset: 0x0005FA5B
		public string ChunkName
		{
			get
			{
				return this.m_chunkName;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x00061AB8 File Offset: 0x0005FCB8
		public int ChunkType
		{
			get
			{
				return this.m_chunkType;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x00061AC0 File Offset: 0x0005FCC0
		public string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x00061AA0 File Offset: 0x0005FCA0
		public ChunkFlags ChunkFlags
		{
			get
			{
				return this.m_chunkFlags;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x00061AC8 File Offset: 0x0005FCC8
		internal short Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00061AD0 File Offset: 0x0005FCD0
		public bool IsAvailable
		{
			get
			{
				return this.m_IsAvailable;
			}
		}

		// Token: 0x0400089A RID: 2202
		protected Guid m_snapshotDataID;

		// Token: 0x0400089B RID: 2203
		protected bool m_isPermanentSnapshot;

		// Token: 0x0400089C RID: 2204
		protected string m_chunkName;

		// Token: 0x0400089D RID: 2205
		protected int m_chunkType;

		// Token: 0x0400089E RID: 2206
		protected string m_mimeType;

		// Token: 0x0400089F RID: 2207
		protected ChunkFlags m_chunkFlags;

		// Token: 0x040008A0 RID: 2208
		protected short m_version;

		// Token: 0x040008A1 RID: 2209
		protected bool m_IsAvailable;
	}
}
