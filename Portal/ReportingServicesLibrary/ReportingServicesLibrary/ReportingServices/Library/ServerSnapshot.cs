using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000282 RID: 642
	internal abstract class ServerSnapshot : SnapshotBase, ISnapshotTransactionFactory
	{
		// Token: 0x0600173B RID: 5947 RVA: 0x0005DB49 File Offset: 0x0005BD49
		protected ServerSnapshot(Guid snapshotDataID, bool isPermanentSnapshot)
			: base(snapshotDataID, isPermanentSnapshot)
		{
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0005DB89 File Offset: 0x0005BD89
		protected ServerSnapshot(bool isPermanentSnapshot)
			: base(isPermanentSnapshot)
		{
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0005DBC8 File Offset: 0x0005BDC8
		protected ServerSnapshot(ServerSnapshot snapshotDataToCopy)
			: base(snapshotDataToCopy)
		{
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0005DC07 File Offset: 0x0005BE07
		public override Stream GetChunk(string name, ReportProcessing.ReportChunkTypes type, out string mimeType)
		{
			return this.GetChunk(name, type, false, WriteOptions.None, out mimeType);
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0005DC14 File Offset: 0x0005BE14
		internal Stream GetChunk(string name, ReportProcessing.ReportChunkTypes type, bool supportReadWrite, WriteOptions updateMode, out string mimeType)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### GetChunk('{0}', {1}) this={2}, #ReadChunks={3}, #WriteChunks={4}", new object[]
				{
					name,
					type,
					base.SnapshotDataID,
					this.m_readStreams.Count,
					this.m_writeStreams.Count
				});
			}
			this.InitializeChunkConnection();
			Stream stream = SnapshotChunkStreamFactory.CreateReadStream(base.SnapshotDataID, base.IsPermanentSnapshot, name, (int)type, this.ConnectionManager, supportReadWrite, updateMode, this.IsInUpgradeScope, out mimeType);
			this.RegisterStream(stream, false);
			return stream;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0005DCB8 File Offset: 0x0005BEB8
		public override string GetStreamMimeType(string name, ReportProcessing.ReportChunkTypes type)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### GetStreamMimeType('{0}', {1}) this={2}, #ReadChunks={3}, #WriteChunks={4}", new object[]
				{
					name,
					type,
					base.SnapshotDataID,
					this.m_readStreams.Count,
					this.m_writeStreams.Count
				});
			}
			ChunkStorage chunkStorage = null;
			string chunkInformation;
			try
			{
				chunkStorage = new ChunkStorage();
				chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.Explicit, IsolationLevel.ReadCommitted);
				chunkStorage.ConnectionManager.WillDisconnectStorage();
				chunkInformation = chunkStorage.GetChunkInformation(base.SnapshotDataID, base.IsPermanentSnapshot, name, (int)type);
			}
			finally
			{
				chunkStorage.DisconnectStorage();
			}
			return chunkInformation;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0005DD7C File Offset: 0x0005BF7C
		public override Stream CreateChunk(string name, ReportProcessing.ReportChunkTypes type, string mimeType)
		{
			return this.CreateChunk(name, type, mimeType, true);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0005DD88 File Offset: 0x0005BF88
		internal Stream CreateChunk(string name, ReportProcessing.ReportChunkTypes type, string mimeType, bool supportReadWrite)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### CreateChunk('{0}', {1}) this={2}, #ReadChunks={3}, #WriteChunks={4}", new object[]
				{
					name,
					type,
					base.SnapshotDataID,
					this.m_readStreams.Count,
					this.m_writeStreams.Count
				});
			}
			this.InitializeChunkConnection();
			Stream stream = SnapshotChunkStreamFactory.CreateWriteStream(base.SnapshotDataID, base.IsPermanentSnapshot, name, (int)type, mimeType, Global.SqlStreamingBufferSize, this.ConnectionManager, WriteOptions.Create, supportReadWrite);
			this.RegisterStream(stream, true);
			return stream;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0005DE2C File Offset: 0x0005C02C
		internal ConnectionManager ShareTransactionContext(ReportSnapshot target)
		{
			if (this.m_inTransactionScope)
			{
				this.InitializeChunkConnection();
			}
			RSTrace.CatalogTrace.Assert(target.ConnectionManager == null, "target.ConnectionManager == null");
			target.ConnectionManager = this.ConnectionManager;
			target.m_inTransactionScope = this.m_inTransactionScope;
			return target.ConnectionManager;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0005DE7D File Offset: 0x0005C07D
		internal void ResetConnectionState()
		{
			this.ConnectionManager = null;
			this.m_inTransactionScope = false;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0005DE90 File Offset: 0x0005C090
		internal void EnsureAllStreamsAreClosed(bool commit)
		{
			try
			{
				object obj = this.m_readStreams.SyncRoot;
				lock (obj)
				{
					foreach (object obj2 in this.m_readStreams)
					{
						Stream stream = (Stream)obj2;
						this.UpdatePerfData(stream);
						stream.Close();
					}
					this.m_readStreams.Clear();
				}
				obj = this.m_writeStreams.SyncRoot;
				lock (obj)
				{
					foreach (object obj3 in this.m_writeStreams)
					{
						Stream stream2 = (Stream)obj3;
						this.UpdatePerfData(stream2);
						this.m_totalCreatedLength += stream2.Length;
						stream2.Close();
					}
					this.m_writeStreams.Clear();
				}
				if (commit)
				{
					this.CommitChunkTransaction();
				}
			}
			catch (Exception ex)
			{
				if (RSTrace.ChunkTracer.TraceWarning)
				{
					RSTrace.ChunkTracer.TraceException(TraceLevel.Warning, ex.ToString());
				}
				if (commit)
				{
					this.RollbackChunkTransaction();
				}
			}
			this.WritePerfData();
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public virtual void EnsureAllSnapshotChunksInDB()
		{
		}

		// Token: 0x06001747 RID: 5959
		public abstract void WriteNewSnapshotToDB(ParameterInfoCollection parameters, DateTime createdDate, string description);

		// Token: 0x06001748 RID: 5960 RVA: 0x0005E050 File Offset: 0x0005C250
		protected void WriteNewSnapshotToDBImpl(ParameterInfoCollection parameters, DateTime createdDate, string description, int flags)
		{
			ChunkStorage chunkStorage = new ChunkStorage();
			chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			chunkStorage.ConnectionManager.WillDisconnectStorage();
			try
			{
				chunkStorage.WriteNewSnapshotToDB(parameters, createdDate, description, base.SnapshotDataID, base.IsPermanentSnapshot, flags);
				this.m_snapshotParameters = parameters;
			}
			finally
			{
				chunkStorage.DisconnectStorage();
			}
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0005E0B8 File Offset: 0x0005C2B8
		public virtual void IncreaseTransientRefcount()
		{
			ChunkStorage chunkStorage = new ChunkStorage();
			chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			chunkStorage.ConnectionManager.WillDisconnectStorage();
			try
			{
				chunkStorage.IncreaseTransientSnapshotRefcount(base.SnapshotDataID, base.IsPermanentSnapshot, 1440);
			}
			finally
			{
				chunkStorage.DisconnectStorage();
			}
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0005E118 File Offset: 0x0005C318
		public virtual void DecreaseTransientRefcount()
		{
			ChunkStorage chunkStorage = new ChunkStorage();
			chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			chunkStorage.ConnectionManager.WillDisconnectStorage();
			try
			{
				chunkStorage.DecreaseTransientSnapshotRefcount(base.SnapshotDataID, base.IsPermanentSnapshot);
			}
			finally
			{
				chunkStorage.DisconnectStorage();
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0005E174 File Offset: 0x0005C374
		public override void DeleteSnapshotAndChunks()
		{
			if (!this.m_deleteAttempted)
			{
				this.m_deleteAttempted = true;
				ChunkStorage chunkStorage = new ChunkStorage();
				chunkStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
				chunkStorage.ConnectionManager.WillDisconnectStorage();
				try
				{
					chunkStorage.DeleteSnapshotAndChunks(base.SnapshotDataID, base.IsPermanentSnapshot);
				}
				finally
				{
					chunkStorage.DisconnectStorage();
				}
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0005E1E0 File Offset: 0x0005C3E0
		internal override void UpdatePerfData(Stream chunk)
		{
			object perfDataSync = this.m_perfDataSync;
			lock (perfDataSync)
			{
				ServerSnapshot.IHasPerformanceData hasPerformanceData = chunk as ServerSnapshot.IHasPerformanceData;
				if (hasPerformanceData != null)
				{
					ServerSnapshot.SnapshotPerfData snapshotPerfData = hasPerformanceData.RetrievePerfData();
					this.m_perfData = this.m_perfData.Combine(snapshotPerfData);
				}
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0005E240 File Offset: 0x0005C440
		internal override void WritePerfData()
		{
			object perfDataSync = this.m_perfDataSync;
			lock (perfDataSync)
			{
				if (RSTrace.ChunkTracer.TraceVerbose)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ID={0}, Length={1}, CompressedLength={2}, TimeCompressing={3}, TimeUncompressing={4}, Ratio={5}, Buffering={6}, Permanent={7}", new object[]
					{
						base.SnapshotDataID,
						this.m_perfData.UncompressedLength,
						this.m_perfData.CompressedLength,
						this.m_perfData.TimeCompressing,
						this.m_perfData.TimeUncompressing,
						this.m_perfData.CompressionRatio,
						this.m_perfData.BufferingRatio,
						base.IsPermanentSnapshot
					});
				}
			}
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0005E330 File Offset: 0x0005C530
		internal void RegisterStream(Stream s, bool writeable)
		{
			if (s != null)
			{
				if (writeable)
				{
					this.m_writeStreams.Add(s);
					return;
				}
				this.m_readStreams.Add(s);
			}
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0005E353 File Offset: 0x0005C553
		internal void UnregisterStream(Stream s, bool writeable, bool close)
		{
			if (s != null)
			{
				if (writeable)
				{
					this.m_writeStreams.Remove(s);
				}
				else
				{
					this.m_readStreams.Remove(s);
				}
				this.UpdatePerfData(s);
				if (close)
				{
					s.Close();
				}
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void FinishChunkCopy(ReportSnapshot targetSnapshot)
		{
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0005E388 File Offset: 0x0005C588
		internal void CommitChunkTransaction()
		{
			try
			{
				if (this.ConnectionManager != null)
				{
					if (RSTrace.ChunkTracer.TraceVerbose)
					{
						RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Committing shared chunk transaction for snapshot '{0}', Permanent={1}.", new object[]
						{
							base.SnapshotDataID.ToString(),
							base.IsPermanentSnapshot.ToString()
						});
					}
					try
					{
						this.ConnectionManager.CommitTransaction();
					}
					finally
					{
						this.ConnectionManager.DisconnectStorage();
						this.ConnectionManager = null;
					}
				}
			}
			finally
			{
				this.m_inTransactionScope = false;
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0005E430 File Offset: 0x0005C630
		internal void RollbackChunkTransaction()
		{
			try
			{
				if (this.ConnectionManager != null)
				{
					if (this.m_writeStreams.Count > 0 && RSTrace.ChunkTracer.TraceWarning)
					{
						RSTrace.ChunkTracer.Trace(TraceLevel.Warning, "Rolling back shared chunk transaction for snapshot '{0}', Permanent={1}.", new object[]
						{
							base.SnapshotDataID.ToString(),
							base.IsPermanentSnapshot.ToString()
						});
					}
					try
					{
						this.ConnectionManager.AbortTransaction();
					}
					finally
					{
						this.ConnectionManager.DisconnectStorage();
						this.ConnectionManager = null;
					}
				}
			}
			finally
			{
				this.m_inTransactionScope = false;
			}
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0005E4EC File Offset: 0x0005C6EC
		private void InitializeChunkConnection()
		{
			if (!this.m_inTransactionScope)
			{
				throw new InternalCatalogException("Snapshot access outside of transaction scope");
			}
			if (this.ConnectionManager != null)
			{
				return;
			}
			object initializeConnectionLock = this.m_initializeConnectionLock;
			lock (initializeConnectionLock)
			{
				if (this.ConnectionManager == null)
				{
					this.ConnectionManager = ConnectionManager.Create(ConnectionTransactionType.Explicit, IsolationLevel.ReadCommitted);
					RSTrace.ChunkTracer.Assert(this.ConnectionManager is MultithreadedConnectionManager, "ConnectionManager is MultithreadedConnectionManager");
					this.ConnectionManager.WillDisconnectStorage();
					this.ConnectionManager.SingleCommitEnabled = true;
					this.ConnectionManager.BeginTransaction();
				}
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x0005E59C File Offset: 0x0005C79C
		// (set) Token: 0x06001755 RID: 5973 RVA: 0x0005E5A4 File Offset: 0x0005C7A4
		internal ConnectionManager ConnectionManager
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_connectionManager;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_connectionManager = value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x0005E5AD File Offset: 0x0005C7AD
		internal long TotalCreatedChunkLength
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_totalCreatedLength;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x0005E5B5 File Offset: 0x0005C7B5
		// (set) Token: 0x06001758 RID: 5976 RVA: 0x0005E5BD File Offset: 0x0005C7BD
		internal bool IsInUpgradeScope
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_inUpgradeScope;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_inUpgradeScope = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x0005E5C6 File Offset: 0x0005C7C6
		internal ParameterInfoCollection SnapshotParameters
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_snapshotParameters;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0005E5D0 File Offset: 0x0005C7D0
		internal int SnapshotQueryParametersHash
		{
			[DebuggerStepThrough]
			get
			{
				int num = 0;
				if (this.SnapshotParameters != null)
				{
					num = StackTraceUtil.GetInvariantHashCode<char>(this.SnapshotParameters.ToXml(true));
				}
				return num;
			}
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0005E5FA File Offset: 0x0005C7FA
		internal ISnapshotTransaction EnterTransactionContext()
		{
			return this.InternalEnterTransactionContext();
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0005E604 File Offset: 0x0005C804
		private ISnapshotTransaction InternalEnterTransactionContext()
		{
			ISnapshotTransaction snapshotTransaction = new ServerSnapshot.ServerSnapshotTransaction(this);
			if (!this.m_inTransactionScope)
			{
				this.m_inTransactionScope = true;
				snapshotTransaction.IsRootTransaction = true;
			}
			else
			{
				snapshotTransaction.IsRootTransaction = false;
			}
			return snapshotTransaction;
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0005E638 File Offset: 0x0005C838
		private void FireTransactionEvent(bool commit)
		{
			ServerSnapshot.SnapshotTransactionEventHandler preTransactionEvent = this.PreTransactionEvent;
			if (preTransactionEvent != null)
			{
				ServerSnapshot.SnapshotTransactionEventArgs snapshotTransactionEventArgs = (commit ? ServerSnapshot.SnapshotTransactionEventArgs.ForCommit : ServerSnapshot.SnapshotTransactionEventArgs.ForRollback);
				preTransactionEvent(this, snapshotTransactionEventArgs);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600175E RID: 5982 RVA: 0x0005E668 File Offset: 0x0005C868
		// (remove) Token: 0x0600175F RID: 5983 RVA: 0x0005E6A0 File Offset: 0x0005C8A0
		internal event ServerSnapshot.SnapshotTransactionEventHandler PreTransactionEvent;

		// Token: 0x06001760 RID: 5984 RVA: 0x0005E5FA File Offset: 0x0005C7FA
		ISnapshotTransaction ISnapshotTransactionFactory.EnterTransactionContext()
		{
			return this.InternalEnterTransactionContext();
		}

		// Token: 0x04000872 RID: 2162
		private ConnectionManager m_connectionManager;

		// Token: 0x04000873 RID: 2163
		private ServerSnapshot.SnapshotPerfData m_perfData;

		// Token: 0x04000874 RID: 2164
		private readonly object m_perfDataSync = new object();

		// Token: 0x04000875 RID: 2165
		protected long m_totalCreatedLength;

		// Token: 0x04000876 RID: 2166
		private bool m_inTransactionScope;

		// Token: 0x04000877 RID: 2167
		private bool m_inUpgradeScope;

		// Token: 0x04000878 RID: 2168
		private readonly object m_initializeConnectionLock = new object();

		// Token: 0x04000879 RID: 2169
		private ParameterInfoCollection m_snapshotParameters;

		// Token: 0x0400087A RID: 2170
		private bool m_deleteAttempted;

		// Token: 0x0400087B RID: 2171
		private readonly ArrayList m_readStreams = ArrayList.Synchronized(new ArrayList());

		// Token: 0x0400087C RID: 2172
		private readonly ArrayList m_writeStreams = ArrayList.Synchronized(new ArrayList());

		// Token: 0x020004C8 RID: 1224
		internal struct SnapshotPerfData
		{
			// Token: 0x06002448 RID: 9288 RVA: 0x00085F7C File Offset: 0x0008417C
			public SnapshotPerfData(long bytesReadTotal, long bytesReadFromBuffer, long timeCompressing, long timeUncompressing, long compressedLength, long uncompressedLength)
			{
				this.BytesReadTotal = bytesReadTotal;
				this.BytesReadFromBuffer = bytesReadFromBuffer;
				this.TimeCompressing = timeCompressing;
				this.TimeUncompressing = timeUncompressing;
				this.CompressedLength = compressedLength;
				this.UncompressedLength = uncompressedLength;
			}

			// Token: 0x17000A99 RID: 2713
			// (get) Token: 0x06002449 RID: 9289 RVA: 0x00085FAB File Offset: 0x000841AB
			internal float CompressionRatio
			{
				get
				{
					if (this.UncompressedLength != 0L)
					{
						return (float)this.CompressedLength / (float)this.UncompressedLength;
					}
					return 0f;
				}
			}

			// Token: 0x17000A9A RID: 2714
			// (get) Token: 0x0600244A RID: 9290 RVA: 0x00085FCA File Offset: 0x000841CA
			internal float BufferingRatio
			{
				get
				{
					if (this.BytesReadTotal != 0L)
					{
						return (float)this.BytesReadFromBuffer / (float)this.BytesReadTotal;
					}
					return 0f;
				}
			}

			// Token: 0x0600244B RID: 9291 RVA: 0x00085FEC File Offset: 0x000841EC
			internal ServerSnapshot.SnapshotPerfData Combine(ServerSnapshot.SnapshotPerfData data)
			{
				return new ServerSnapshot.SnapshotPerfData(this.BytesReadTotal + data.BytesReadTotal, this.BytesReadFromBuffer + data.BytesReadFromBuffer, this.TimeCompressing + data.TimeCompressing, this.TimeUncompressing + data.TimeUncompressing, this.CompressedLength + data.CompressedLength, this.UncompressedLength + data.UncompressedLength);
			}

			// Token: 0x0400110A RID: 4362
			internal readonly long BytesReadTotal;

			// Token: 0x0400110B RID: 4363
			internal readonly long BytesReadFromBuffer;

			// Token: 0x0400110C RID: 4364
			internal readonly long TimeCompressing;

			// Token: 0x0400110D RID: 4365
			internal readonly long TimeUncompressing;

			// Token: 0x0400110E RID: 4366
			internal readonly long CompressedLength;

			// Token: 0x0400110F RID: 4367
			internal readonly long UncompressedLength;
		}

		// Token: 0x020004C9 RID: 1225
		internal interface IHasPerformanceData
		{
			// Token: 0x0600244C RID: 9292
			ServerSnapshot.SnapshotPerfData RetrievePerfData();
		}

		// Token: 0x020004CA RID: 1226
		private class ServerSnapshotTransaction : SnapshotTransactionBase
		{
			// Token: 0x0600244D RID: 9293 RVA: 0x0008604C File Offset: 0x0008424C
			public ServerSnapshotTransaction(ServerSnapshot snapshot)
			{
				this.m_serverSnapshot = snapshot;
			}

			// Token: 0x0600244E RID: 9294 RVA: 0x0008605B File Offset: 0x0008425B
			protected override void CloseStreams()
			{
				this.m_serverSnapshot.EnsureAllStreamsAreClosed(false);
			}

			// Token: 0x0600244F RID: 9295 RVA: 0x00086069 File Offset: 0x00084269
			protected override void CommitInternal()
			{
				this.m_serverSnapshot.FireTransactionEvent(true);
				this.m_serverSnapshot.CommitChunkTransaction();
			}

			// Token: 0x06002450 RID: 9296 RVA: 0x00086082 File Offset: 0x00084282
			protected override void RollbackInternal()
			{
				this.m_serverSnapshot.FireTransactionEvent(false);
				this.m_serverSnapshot.RollbackChunkTransaction();
			}

			// Token: 0x04001110 RID: 4368
			private readonly ServerSnapshot m_serverSnapshot;
		}

		// Token: 0x020004CB RID: 1227
		internal sealed class SnapshotTransactionEventArgs : EventArgs
		{
			// Token: 0x17000A9B RID: 2715
			// (get) Token: 0x06002451 RID: 9297 RVA: 0x0008609B File Offset: 0x0008429B
			public static ServerSnapshot.SnapshotTransactionEventArgs ForCommit
			{
				[DebuggerStepThrough]
				get
				{
					return ServerSnapshot.SnapshotTransactionEventArgs.m_forCommit;
				}
			}

			// Token: 0x17000A9C RID: 2716
			// (get) Token: 0x06002452 RID: 9298 RVA: 0x000860A2 File Offset: 0x000842A2
			public static ServerSnapshot.SnapshotTransactionEventArgs ForRollback
			{
				[DebuggerStepThrough]
				get
				{
					return ServerSnapshot.SnapshotTransactionEventArgs.m_forRollback;
				}
			}

			// Token: 0x17000A9D RID: 2717
			// (get) Token: 0x06002453 RID: 9299 RVA: 0x000860A9 File Offset: 0x000842A9
			public bool IsRollbackEvent
			{
				[DebuggerStepThrough]
				get
				{
					return !this.m_committing;
				}
			}

			// Token: 0x17000A9E RID: 2718
			// (get) Token: 0x06002454 RID: 9300 RVA: 0x000860B4 File Offset: 0x000842B4
			public bool IsCommittEvent
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_committing;
				}
			}

			// Token: 0x06002455 RID: 9301 RVA: 0x000860BC File Offset: 0x000842BC
			private SnapshotTransactionEventArgs(bool committing)
			{
				this.m_committing = committing;
			}

			// Token: 0x04001111 RID: 4369
			private static readonly ServerSnapshot.SnapshotTransactionEventArgs m_forCommit = new ServerSnapshot.SnapshotTransactionEventArgs(true);

			// Token: 0x04001112 RID: 4370
			private static readonly ServerSnapshot.SnapshotTransactionEventArgs m_forRollback = new ServerSnapshot.SnapshotTransactionEventArgs(false);

			// Token: 0x04001113 RID: 4371
			private readonly bool m_committing;
		}

		// Token: 0x020004CC RID: 1228
		// (Invoke) Token: 0x06002458 RID: 9304
		internal delegate void SnapshotTransactionEventHandler(object sender, ServerSnapshot.SnapshotTransactionEventArgs eventArgs);
	}
}
