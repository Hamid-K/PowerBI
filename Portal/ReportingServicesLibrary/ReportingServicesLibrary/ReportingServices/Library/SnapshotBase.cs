using System;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000299 RID: 665
	[Serializable]
	internal abstract class SnapshotBase
	{
		// Token: 0x0600182B RID: 6187 RVA: 0x00062482 File Offset: 0x00060682
		protected SnapshotBase(Guid snapshotDataID, bool isPermanentSnapshot)
		{
			this.m_snapshotDataID = snapshotDataID;
			this.m_isPermanentSnapshot = isPermanentSnapshot;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00062498 File Offset: 0x00060698
		protected SnapshotBase(bool isPermanentSnapshot)
		{
			this.m_snapshotDataID = Guid.NewGuid();
			this.m_isPermanentSnapshot = isPermanentSnapshot;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x000624B2 File Offset: 0x000606B2
		protected SnapshotBase(SnapshotBase snapshotDataToCopy)
		{
			this.m_snapshotDataID = snapshotDataToCopy.SnapshotDataID;
			this.m_isPermanentSnapshot = snapshotDataToCopy.m_isPermanentSnapshot;
		}

		// Token: 0x0600182E RID: 6190
		public abstract SnapshotBase Duplicate();

		// Token: 0x0600182F RID: 6191
		[Obsolete("Use PrepareExecutionSnapshot instead")]
		public abstract void CopyImageChunksTo(SnapshotBase target);

		// Token: 0x06001830 RID: 6192
		public abstract void PrepareExecutionSnapshot(SnapshotBase target, string compiledDefinitionChunkName);

		// Token: 0x06001831 RID: 6193
		public abstract Stream GetChunk(string name, ReportProcessing.ReportChunkTypes type, out string mimeType);

		// Token: 0x06001832 RID: 6194
		public abstract string GetStreamMimeType(string name, ReportProcessing.ReportChunkTypes type);

		// Token: 0x06001833 RID: 6195
		public abstract Stream CreateChunk(string name, ReportProcessing.ReportChunkTypes type, string mimeType);

		// Token: 0x06001834 RID: 6196
		public abstract void DeleteSnapshotAndChunks();

		// Token: 0x06001835 RID: 6197 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void UpdatePerfData(Stream chunk)
		{
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void WritePerfData()
		{
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x000624D2 File Offset: 0x000606D2
		internal Guid SnapshotDataID
		{
			get
			{
				return this.m_snapshotDataID;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x000624DA File Offset: 0x000606DA
		internal bool IsPermanentSnapshot
		{
			get
			{
				return this.m_isPermanentSnapshot;
			}
		}

		// Token: 0x040008BD RID: 2237
		private Guid m_snapshotDataID;

		// Token: 0x040008BE RID: 2238
		private bool m_isPermanentSnapshot;
	}
}
