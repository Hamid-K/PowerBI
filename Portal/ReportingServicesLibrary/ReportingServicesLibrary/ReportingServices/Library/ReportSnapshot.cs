using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000283 RID: 643
	internal class ReportSnapshot : ServerSnapshot, IChunkFactory
	{
		// Token: 0x06001761 RID: 5985 RVA: 0x0005E6D8 File Offset: 0x0005C8D8
		protected ReportSnapshot(Guid snapshotDataID, bool isPermanentSnapshot, bool dependsOnUser, ReportProcessingFlags processingFlags)
			: base(snapshotDataID, isPermanentSnapshot)
		{
			this.m_dependsOnUser = dependsOnUser;
			this.m_processingFlags = processingFlags;
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ReportSnapshot({0}) constructor of existing snapshot.", new object[] { snapshotDataID });
			}
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0005E727 File Offset: 0x0005C927
		protected ReportSnapshot(bool isPermanentSnapshot)
			: base(isPermanentSnapshot)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ReportSnapshot() New snapshot, id={0}.", new object[] { base.SnapshotDataID });
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0005E760 File Offset: 0x0005C960
		protected ReportSnapshot(ReportSnapshot snapshotDataToCopy)
			: base(snapshotDataToCopy)
		{
			this.m_processingFlags = snapshotDataToCopy.m_processingFlags;
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ReportSnapshot(copy {0})", new object[] { base.SnapshotDataID });
			}
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0005E7B0 File Offset: 0x0005C9B0
		public static ReportSnapshot Create(bool isPermanentSnapshot, ReportProcessingFlags processingFlags)
		{
			return ReportSnapshot.Create(Guid.NewGuid(), isPermanentSnapshot, false, processingFlags);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0005E7BF File Offset: 0x0005C9BF
		public static ReportSnapshot Create(Guid snapshotDataID, bool isPermanentSnapshot, bool dependsOnUser, ReportProcessingFlags processingFlags)
		{
			return new ReportSnapshot(snapshotDataID, isPermanentSnapshot, dependsOnUser, processingFlags);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0005E7CC File Offset: 0x0005C9CC
		[Obsolete("Use PrepareExecutionSnasphot", true)]
		public override void CopyImageChunksTo(SnapshotBase target)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### CopyImageChunksTo({0}) this={1}", new object[] { target.SnapshotDataID, base.SnapshotDataID });
			}
			ChunkStorage chunkStorage = new ChunkStorage();
			chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			chunkStorage.ConnectionManager.WillDisconnectStorage();
			try
			{
				int imageChunkTypeToCopy = (int)ReportProcessing.GetImageChunkTypeToCopy(this.ProcessingFlags);
				chunkStorage.CopyImages(base.SnapshotDataID, base.IsPermanentSnapshot, target.SnapshotDataID, target.IsPermanentSnapshot, imageChunkTypeToCopy);
			}
			finally
			{
				chunkStorage.DisconnectStorage();
			}
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0005E880 File Offset: 0x0005CA80
		public void CopyDataChunksTo(SnapshotBase target, ConnectionManager connectionManager)
		{
			this.CopyDataChunksTo(target, connectionManager, null, null);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0005E88C File Offset: 0x0005CA8C
		public void CopyDataChunksTo(SnapshotBase target, ConnectionManager connectionManager, string sourceChunkName, string targetChunkName)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### CopyDataChunksTo({0}) this={1}", new object[] { target.SnapshotDataID, base.SnapshotDataID });
			}
			ChunkStorage chunkStorage = new ChunkStorage();
			bool flag = connectionManager == null;
			if (flag)
			{
				connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
				connectionManager.WillDisconnectStorage();
			}
			chunkStorage.ConnectionManager = connectionManager;
			try
			{
				chunkStorage.CopyChunks(base.SnapshotDataID, base.IsPermanentSnapshot, target.SnapshotDataID, target.IsPermanentSnapshot, new int?(5), sourceChunkName, targetChunkName);
			}
			finally
			{
				if (flag)
				{
					chunkStorage.DisconnectStorage();
				}
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0005E944 File Offset: 0x0005CB44
		public override void PrepareExecutionSnapshot(SnapshotBase target, string compiledDefinitionChunkName)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### PrepareExecutionSnapshot({0}) this={1}", new object[] { target.SnapshotDataID, base.SnapshotDataID });
			}
			SegmentChunkDbInterface segmentChunkDbInterface = new SegmentChunkDbInterface();
			segmentChunkDbInterface.ConnectionManager = new ConnectionManager(ConnectionTransactionType.Explicit, IsolationLevel.ReadCommitted);
			segmentChunkDbInterface.ConnectionManager.WillDisconnectStorage();
			try
			{
				int imageChunkTypeToCopy = (int)ReportProcessing.GetImageChunkTypeToCopy(this.ProcessingFlags);
				segmentChunkDbInterface.CopyImages(base.SnapshotDataID, base.IsPermanentSnapshot, target.SnapshotDataID, target.IsPermanentSnapshot, imageChunkTypeToCopy);
				if (string.IsNullOrEmpty(compiledDefinitionChunkName))
				{
					segmentChunkDbInterface.CopyChunks(base.SnapshotDataID, base.IsPermanentSnapshot, target.SnapshotDataID, target.IsPermanentSnapshot, new int?(9), null, null);
				}
				else
				{
					segmentChunkDbInterface.CopyChunks(base.SnapshotDataID, base.IsPermanentSnapshot, target.SnapshotDataID, target.IsPermanentSnapshot, new int?(9), null, compiledDefinitionChunkName);
				}
				segmentChunkDbInterface.CopyChunks(base.SnapshotDataID, base.IsPermanentSnapshot, target.SnapshotDataID, target.IsPermanentSnapshot, new int?(5), null, null);
				segmentChunkDbInterface.Commit();
			}
			finally
			{
				segmentChunkDbInterface.DisconnectStorage();
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0005EA7C File Offset: 0x0005CC7C
		public override SnapshotBase Duplicate()
		{
			return new ReportSnapshot(this);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0005EA84 File Offset: 0x0005CC84
		public override void WriteNewSnapshotToDB(ParameterInfoCollection parameters, DateTime createdDate, string description)
		{
			base.WriteNewSnapshotToDBImpl(parameters, createdDate, description, (int)this.m_processingFlags);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0005EA98 File Offset: 0x0005CC98
		public virtual void MarkAsDependentOnUser()
		{
			if (this.m_dependsOnUser)
			{
				return;
			}
			ChunkStorage chunkStorage = new ChunkStorage();
			chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			chunkStorage.ConnectionManager.WillDisconnectStorage();
			try
			{
				chunkStorage.MarkSnapshotAsDependentOnUser(base.SnapshotDataID, base.IsPermanentSnapshot);
				this.m_dependsOnUser = true;
			}
			finally
			{
				chunkStorage.DisconnectStorage();
			}
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0005EB04 File Offset: 0x0005CD04
		internal virtual void SetSnapshotProcessingFlags(ReportProcessingFlags processingFlags, ConnectionManager connection)
		{
			ChunkStorage chunkStorage = new ChunkStorage();
			chunkStorage.ConnectionManager = connection;
			if (chunkStorage.ConnectionManager == null)
			{
				chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
				chunkStorage.ConnectionManager.WillDisconnectStorage();
			}
			try
			{
				chunkStorage.SetSnapshotProcessingFlags(base.SnapshotDataID, base.IsPermanentSnapshot, processingFlags);
				this.m_processingFlags = processingFlags;
			}
			finally
			{
				if (connection == null)
				{
					chunkStorage.DisconnectStorage();
				}
			}
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0005EB7C File Offset: 0x0005CD7C
		internal virtual ReportSnapshot CreateNewVersion()
		{
			ReportSnapshot reportSnapshot = ReportSnapshot.Create(Guid.NewGuid(), base.IsPermanentSnapshot, this.DependsOnUser, this.ProcessingFlags);
			ChunkStorage chunkStorage = new ChunkStorage();
			chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			chunkStorage.ConnectionManager.WillDisconnectStorage();
			try
			{
				chunkStorage.CreateNewSnapshotVersion(this, reportSnapshot);
			}
			finally
			{
				chunkStorage.DisconnectStorage();
			}
			try
			{
				this.FinishChunkCopy(reportSnapshot);
			}
			catch (Exception ex)
			{
				if (RSTrace.CatalogTrace.TraceWarning)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Warning, "Exception from FinishChunkCopy(): {0}", new object[] { ex.Message });
				}
				throw;
			}
			return reportSnapshot;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0005EC30 File Offset: 0x0005CE30
		internal virtual int ReplaceExistingSnapshot(ConnectionManager connectionManager, ReportSnapshot oldVersion, bool decrementRefCount)
		{
			ChunkStorage chunkStorage = new ChunkStorage();
			bool flag = connectionManager == null;
			if (flag)
			{
				connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
				connectionManager.WillDisconnectStorage();
			}
			chunkStorage.ConnectionManager = connectionManager;
			int num;
			try
			{
				chunkStorage.UpdateSnapshotReferences(oldVersion, this, decrementRefCount ? (-1) : 0, out num);
			}
			finally
			{
				if (flag)
				{
					chunkStorage.DisconnectStorage();
				}
			}
			return num;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0005EC94 File Offset: 0x0005CE94
		internal virtual bool RemoveChunkFromSnapshot(string name, ReportProcessing.ReportChunkTypes type)
		{
			ChunkStorage chunkStorage = new ChunkStorage();
			bool flag = false;
			if (base.ConnectionManager != null)
			{
				chunkStorage.ConnectionManager = base.ConnectionManager;
			}
			else
			{
				chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
				chunkStorage.ConnectionManager.WillDisconnectStorage();
				flag = true;
			}
			RSTrace.CatalogTrace.Assert(chunkStorage.ConnectionManager != null, "storage.ConnectionManager");
			bool flag2;
			try
			{
				flag2 = chunkStorage.DeleteOneChunk(base.SnapshotDataID, base.IsPermanentSnapshot, name, (int)type);
			}
			finally
			{
				if (flag)
				{
					chunkStorage.DisconnectStorage();
				}
			}
			return flag2;
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x0005ED28 File Offset: 0x0005CF28
		internal bool DependsOnUser
		{
			get
			{
				return this.m_dependsOnUser;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0005ED30 File Offset: 0x0005CF30
		internal ReportProcessingFlags ProcessingFlags
		{
			get
			{
				return this.m_processingFlags;
			}
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0005ED38 File Offset: 0x0005CF38
		Stream IChunkFactory.CreateChunk(string chunkName, ReportProcessing.ReportChunkTypes type, string mimeType)
		{
			return this.CreateChunk(chunkName, type, mimeType);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0005ED44 File Offset: 0x0005CF44
		Stream IChunkFactory.GetChunk(string chunkName, ReportProcessing.ReportChunkTypes type, ChunkMode mode, out string mimeType)
		{
			Stream stream = this.GetChunk(chunkName, type, out mimeType);
			if (mode == ChunkMode.OpenOrCreate && stream == null)
			{
				mimeType = null;
				stream = ((IChunkFactory)this).CreateChunk(chunkName, type, mimeType);
			}
			return stream;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0005ED74 File Offset: 0x0005CF74
		bool IChunkFactory.Erase(string chunkName, ReportProcessing.ReportChunkTypes type)
		{
			return this.RemoveChunkFromSnapshot(chunkName, type);
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x0005ED7E File Offset: 0x0005CF7E
		ReportProcessingFlags IChunkFactory.ReportProcessingFlags
		{
			get
			{
				return this.ProcessingFlags;
			}
		}

		// Token: 0x0400087E RID: 2174
		protected bool m_dependsOnUser;

		// Token: 0x0400087F RID: 2175
		protected ReportProcessingFlags m_processingFlags;

		// Token: 0x04000880 RID: 2176
		private const string AllChunksOfThisType = null;
	}
}
