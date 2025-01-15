using System;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000287 RID: 647
	internal sealed class SnapshotManager : IChunkFactory, ISnapshotTransactionFactory
	{
		// Token: 0x06001787 RID: 6023 RVA: 0x0005F554 File Offset: 0x0005D754
		public Stream GetChunk(string name, ReportProcessing.ReportChunkTypes type, out string mimeType)
		{
			ServerSnapshot chunkTargetSnapshot = this.ChunkTargetSnapshot;
			WriteOptions writeOptions = (this.SnapshotVersioningEnabled ? WriteOptions.Version : WriteOptions.Update);
			Stream chunk = chunkTargetSnapshot.GetChunk(name, type, true, writeOptions, out mimeType);
			BufferedReadWriteStream bufferedReadWriteStream = chunk as BufferedReadWriteStream;
			if (bufferedReadWriteStream != null)
			{
				bufferedReadWriteStream.StreamFirstWrite += this.FirstWriteHandler;
			}
			return chunk;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0005F59A File Offset: 0x0005D79A
		public string GetStreamMimeType(string name, ReportProcessing.ReportChunkTypes type)
		{
			return this.ChunkTargetSnapshot.GetStreamMimeType(name, type);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0005F5A9 File Offset: 0x0005D7A9
		public Stream CreateChunk(string name, ReportProcessing.ReportChunkTypes type, string mimeType)
		{
			if (this.SnapshotVersioningEnabled && this.UpdatedSnapshot == null)
			{
				this.CreateUpdatedSnapshot();
			}
			return this.ChunkTargetSnapshot.CreateChunk(name, type, mimeType, true);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0005F5D0 File Offset: 0x0005D7D0
		public void EnrollInPersistedEvent(SessionReportItem sessionItem)
		{
			RSTrace.CatalogTrace.Assert(sessionItem != null, "sessionItem");
			sessionItem.Persisted += this.PersistedHandler;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0005F5F8 File Offset: 0x0005D7F8
		private void CommitSnapshotTransaction()
		{
			try
			{
				if (this.m_sharedConnection != null)
				{
					this.m_sharedConnection.CommitTransaction();
				}
				else
				{
					this.m_transactionTargetSnapshot.CommitChunkTransaction();
				}
			}
			finally
			{
				this.m_sharedConnection = null;
				this.m_transactionTargetSnapshot = null;
				this.ResetSnapshotConnectionStates();
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0005F64C File Offset: 0x0005D84C
		private void RollbackSnapshotTransaction()
		{
			try
			{
				if (this.m_sharedConnection != null)
				{
					this.m_sharedConnection.AbortTransaction();
				}
				else
				{
					this.m_transactionTargetSnapshot.RollbackChunkTransaction();
				}
			}
			finally
			{
				this.m_sharedConnection = null;
				this.m_transactionTargetSnapshot = null;
				this.ResetSnapshotConnectionStates();
			}
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0005F6A0 File Offset: 0x0005D8A0
		private void ResetSnapshotConnectionStates()
		{
			try
			{
				if (this.OriginalSnapshot != null)
				{
					this.OriginalSnapshot.ResetConnectionState();
				}
			}
			finally
			{
				if (this.UpdatedSnapshot != null)
				{
					this.UpdatedSnapshot.ResetConnectionState();
				}
			}
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0005F6E8 File Offset: 0x0005D8E8
		private void CloseAllSnapshotStreams()
		{
			if (this.OriginalSnapshot != null)
			{
				this.OriginalSnapshot.EnsureAllStreamsAreClosed(false);
			}
			if (this.UpdatedSnapshot != null)
			{
				this.UpdatedSnapshot.EnsureAllStreamsAreClosed(false);
			}
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0005F712 File Offset: 0x0005D912
		public void VersionSnapshot()
		{
			if (this.SnapshotVersioningEnabled && this.UpdatedSnapshot == null)
			{
				this.CreateUpdatedSnapshot();
			}
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0005F72A File Offset: 0x0005D92A
		public void DecrementTransientRefCount(ReportSnapshot snapshot)
		{
			if (!this.m_didDecrement)
			{
				snapshot.DecreaseTransientRefcount();
				this.m_didDecrement = true;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x0005F741 File Offset: 0x0005D941
		// (set) Token: 0x06001792 RID: 6034 RVA: 0x0005F749 File Offset: 0x0005D949
		public bool SnapshotVersioningEnabled
		{
			get
			{
				return this.m_newSnapshotOnUpdate;
			}
			set
			{
				this.m_newSnapshotOnUpdate = value;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x0005F752 File Offset: 0x0005D952
		// (set) Token: 0x06001794 RID: 6036 RVA: 0x0005F75A File Offset: 0x0005D95A
		public ReportSnapshot OriginalSnapshot
		{
			get
			{
				return this.m_originalSnapshot;
			}
			set
			{
				this.m_originalSnapshot = value;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x0005F763 File Offset: 0x0005D963
		// (set) Token: 0x06001796 RID: 6038 RVA: 0x0005F76B File Offset: 0x0005D96B
		public ReportSnapshot UpdatedSnapshot
		{
			get
			{
				return this.m_updateSnapshot;
			}
			set
			{
				this.m_updateSnapshot = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x0005F774 File Offset: 0x0005D974
		public ReportSnapshot ChunkTargetSnapshot
		{
			get
			{
				object snapshotUpdateSync = this.SnapshotUpdateSync;
				ReportSnapshot reportSnapshot;
				lock (snapshotUpdateSync)
				{
					if (this.UpdatedSnapshot != null)
					{
						reportSnapshot = this.UpdatedSnapshot;
					}
					else
					{
						reportSnapshot = this.OriginalSnapshot;
					}
				}
				return reportSnapshot;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0005F7C8 File Offset: 0x0005D9C8
		public object SnapshotUpdateSync
		{
			get
			{
				return this.m_newSnapshotSync;
			}
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0005F7D0 File Offset: 0x0005D9D0
		private void CreateUpdatedSnapshot()
		{
			RSTrace.CatalogTrace.Assert(this.SnapshotVersioningEnabled);
			object snapshotUpdateSync = this.SnapshotUpdateSync;
			lock (snapshotUpdateSync)
			{
				if (this.UpdatedSnapshot == null)
				{
					this.UpdatedSnapshot = this.OriginalSnapshot.CreateNewVersion();
					if (RSTrace.CatalogTrace.TraceVerbose)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Versioned existing snapshot '{0}' to '{1}'", new object[]
						{
							this.OriginalSnapshot.SnapshotDataID,
							this.UpdatedSnapshot.SnapshotDataID
						});
					}
					this.m_sharedConnection = this.OriginalSnapshot.ShareTransactionContext(this.UpdatedSnapshot);
					SnapshotUpdatedEventArgs snapshotUpdatedEventArgs = new SnapshotUpdatedEventArgs(this.OriginalSnapshot, this.UpdatedSnapshot);
					this.OnSnapshotUpdated(snapshotUpdatedEventArgs);
				}
			}
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0005F8B0 File Offset: 0x0005DAB0
		private void FirstWriteHandler(object sender, BufferedReadWriteStream.StreamFirstWriteEventArgs e)
		{
			Stream stream = sender as Stream;
			if (this.SnapshotVersioningEnabled)
			{
				this.CreateUpdatedSnapshot();
				IUpdateSnapshot updateSnapshot = stream as IUpdateSnapshot;
				if (updateSnapshot != null)
				{
					updateSnapshot.UpdateSnapshot(this.ChunkTargetSnapshot.SnapshotDataID);
				}
			}
			this.OriginalSnapshot.UnregisterStream(stream, false, false);
			this.ChunkTargetSnapshot.RegisterStream(stream, true);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0005F908 File Offset: 0x0005DB08
		private void PersistedHandler(object sender, SessionPersistedEventArgs e)
		{
			if (this.SnapshotVersioningEnabled && this.UpdatedSnapshot != null)
			{
				if (RSTrace.CatalogTrace.TraceInfo)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Replacing references of snapshot '{0}' with snapshot '{1}'", new object[]
					{
						this.OriginalSnapshot.SnapshotDataID,
						this.UpdatedSnapshot.SnapshotDataID
					});
				}
				this.UpdatedSnapshot.ReplaceExistingSnapshot(e.ConnectionManager, this.OriginalSnapshot, false);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600179C RID: 6044 RVA: 0x0005F988 File Offset: 0x0005DB88
		// (remove) Token: 0x0600179D RID: 6045 RVA: 0x0005F9C0 File Offset: 0x0005DBC0
		internal event SnapshotManager.SnapshotUpdatedHandler SnapshotUpdated;

		// Token: 0x0600179E RID: 6046 RVA: 0x0005F9F8 File Offset: 0x0005DBF8
		private void OnSnapshotUpdated(SnapshotUpdatedEventArgs e)
		{
			SnapshotManager.SnapshotUpdatedHandler snapshotUpdated = this.SnapshotUpdated;
			if (snapshotUpdated != null)
			{
				snapshotUpdated(this, e);
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0005FA17 File Offset: 0x0005DC17
		Stream IChunkFactory.CreateChunk(string chunkName, ReportProcessing.ReportChunkTypes type, string mimeType)
		{
			return this.CreateChunk(chunkName, type, mimeType);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0005FA24 File Offset: 0x0005DC24
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

		// Token: 0x060017A1 RID: 6049 RVA: 0x0005FA54 File Offset: 0x0005DC54
		bool IChunkFactory.Erase(string chunkName, ReportProcessing.ReportChunkTypes type)
		{
			this.VersionSnapshot();
			return this.ChunkTargetSnapshot.RemoveChunkFromSnapshot(chunkName, type);
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0005FA69 File Offset: 0x0005DC69
		ReportProcessingFlags IChunkFactory.ReportProcessingFlags
		{
			get
			{
				return this.ChunkTargetSnapshot.ProcessingFlags;
			}
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0005FA78 File Offset: 0x0005DC78
		public ISnapshotTransaction EnterTransactionContext()
		{
			SnapshotManager.SnapshotManagerTransaction snapshotManagerTransaction = new SnapshotManager.SnapshotManagerTransaction(this);
			ServerSnapshot serverSnapshot = this.OriginalSnapshot;
			if (this.UpdatedSnapshot != null)
			{
				serverSnapshot = this.UpdatedSnapshot;
			}
			this.m_transactionTargetSnapshot = serverSnapshot;
			ISnapshotTransaction snapshotTransaction = serverSnapshot.EnterTransactionContext();
			((ISnapshotTransaction)snapshotManagerTransaction).IsRootTransaction = snapshotTransaction.IsRootTransaction;
			if (!((ISnapshotTransaction)snapshotManagerTransaction).IsRootTransaction)
			{
				serverSnapshot.PreTransactionEvent += this.TransactionEventHandler;
			}
			return snapshotManagerTransaction;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0005FAD5 File Offset: 0x0005DCD5
		private void TransactionEventHandler(object sender, ServerSnapshot.SnapshotTransactionEventArgs eventArgs)
		{
			this.CloseAllSnapshotStreams();
		}

		// Token: 0x04000885 RID: 2181
		private bool m_didDecrement;

		// Token: 0x04000886 RID: 2182
		private bool m_newSnapshotOnUpdate;

		// Token: 0x04000887 RID: 2183
		private ConnectionManager m_sharedConnection;

		// Token: 0x04000888 RID: 2184
		private ReportSnapshot m_originalSnapshot;

		// Token: 0x04000889 RID: 2185
		private ReportSnapshot m_updateSnapshot;

		// Token: 0x0400088A RID: 2186
		private ServerSnapshot m_transactionTargetSnapshot;

		// Token: 0x0400088B RID: 2187
		private readonly object m_newSnapshotSync = new object();

		// Token: 0x020004CD RID: 1229
		// (Invoke) Token: 0x0600245C RID: 9308
		internal delegate void SnapshotUpdatedHandler(object sender, SnapshotUpdatedEventArgs e);

		// Token: 0x020004CE RID: 1230
		private class SnapshotManagerTransaction : SnapshotTransactionBase
		{
			// Token: 0x0600245F RID: 9311 RVA: 0x000860E3 File Offset: 0x000842E3
			public SnapshotManagerTransaction(SnapshotManager snapshotManager)
			{
				this.m_snapshotManager = snapshotManager;
			}

			// Token: 0x06002460 RID: 9312 RVA: 0x000860F2 File Offset: 0x000842F2
			protected override void CloseStreams()
			{
				this.m_snapshotManager.CloseAllSnapshotStreams();
			}

			// Token: 0x06002461 RID: 9313 RVA: 0x000860FF File Offset: 0x000842FF
			protected override void CommitInternal()
			{
				this.m_snapshotManager.CommitSnapshotTransaction();
			}

			// Token: 0x06002462 RID: 9314 RVA: 0x0008610C File Offset: 0x0008430C
			protected override void RollbackInternal()
			{
				this.m_snapshotManager.RollbackSnapshotTransaction();
			}

			// Token: 0x04001114 RID: 4372
			private readonly SnapshotManager m_snapshotManager;
		}
	}
}
