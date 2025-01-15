using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000233 RID: 563
	internal sealed class SQLPersistedStreams : PersistedStreamManagement
	{
		// Token: 0x0600147E RID: 5246 RVA: 0x0004F4E5 File Offset: 0x0004D6E5
		public SQLPersistedStreams(string sessionID)
		{
			this.m_sessionID = sessionID;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0004F500 File Offset: 0x0004D700
		public override void ResetPersistedStreams()
		{
			base.EnsureAllLocksReleased();
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			SQLPersistedStreamDB sqlpersistedStreamDB = new SQLPersistedStreamDB(connectionManager, this.m_sessionID);
			try
			{
				int num;
				do
				{
					num = sqlpersistedStreamDB.DeleteSessionStreams();
					sqlpersistedStreamDB.Commit();
				}
				while (num != 0);
				this.m_streams.Clear();
			}
			catch
			{
				sqlpersistedStreamDB.AbortTransaction();
				throw;
			}
			finally
			{
				sqlpersistedStreamDB.Disconnect();
			}
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0004F578 File Offset: 0x0004D778
		protected override void StreamAdded(int streamIndex, Stream stream)
		{
			if (this.m_streams.Count == streamIndex)
			{
				SQLPersistedStreams.StreamState streamState = new SQLPersistedStreams.StreamState();
				this.m_streams.Add(streamState);
				return;
			}
			if (streamIndex > this.m_streams.Count)
			{
				throw new InternalCatalogException("Added a stream whose index is too large.");
			}
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0004F5C0 File Offset: 0x0004D7C0
		protected override void LockStream(int streamIndex)
		{
			if (RSTrace.SessionTrace.TraceVerbose)
			{
				RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "SQLPersistedStreams.LockStream({0} of {1}), session {2}", new object[]
				{
					streamIndex,
					this.m_streams.Count,
					this.m_sessionID
				});
			}
			if (this.m_streams.Count == streamIndex)
			{
				this.StreamAdded(streamIndex, null);
			}
			SQLPersistedStreams.StreamState streamState = (SQLPersistedStreams.StreamState)this.m_streams[streamIndex];
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			SQLPersistedStreamDB sqlpersistedStreamDB = new SQLPersistedStreamDB(connectionManager, this.m_sessionID);
			try
			{
				if (!streamState.RowCreatedInDB)
				{
					sqlpersistedStreamDB.AddNewSessionStream(streamIndex);
					streamState.RowCreatedInDB = true;
				}
				else
				{
					sqlpersistedStreamDB.LockSessionStream(streamIndex);
				}
				streamState.LockConnManager = connectionManager;
			}
			catch
			{
				connectionManager.AbortTransaction();
				streamState.LockConnManager = null;
				throw;
			}
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0004F6A0 File Offset: 0x0004D8A0
		protected override void ReleaseStream(int streamIndex)
		{
			if (RSTrace.SessionTrace.TraceVerbose)
			{
				RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "ReleaseStream({0} of {1}), {2}", new object[]
				{
					streamIndex,
					this.m_streams.Count,
					this.m_sessionID
				});
			}
			if (this.m_streams.Count == 0)
			{
				return;
			}
			if (streamIndex >= this.m_streams.Count)
			{
				return;
			}
			SQLPersistedStreams.StreamState streamState = (SQLPersistedStreams.StreamState)this.m_streams[streamIndex];
			if (streamState.IsLocked)
			{
				try
				{
					streamState.LockConnManager.CommitTransaction();
				}
				catch
				{
					streamState.LockConnManager.AbortTransaction();
					throw;
				}
				finally
				{
					streamState.LockConnManager.DisconnectStorage();
					streamState.LockConnManager = null;
				}
			}
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0004F778 File Offset: 0x0004D978
		protected override void PersistStream(int streamIndex, Stream stream)
		{
			if (streamIndex >= this.m_streams.Count)
			{
				throw new InternalCatalogException("Can't persist a stream that hasn't been added");
			}
			if (streamIndex == 0)
			{
				return;
			}
			SQLPersistedStreams.StreamState streamState = (SQLPersistedStreams.StreamState)this.m_streams[streamIndex];
			ConnectionManager connectionManager;
			if (streamState.IsLocked)
			{
				connectionManager = streamState.LockConnManager;
			}
			else
			{
				connectionManager = new ConnectionManager();
				connectionManager.WillDisconnectStorage();
			}
			SQLPersistedStreamDB sqlpersistedStreamDB = new SQLPersistedStreamDB(connectionManager, this.m_sessionID);
			try
			{
				if (!streamState.RowCreatedInDB)
				{
					sqlpersistedStreamDB.AddNewSessionStream(streamIndex);
					streamState.RowCreatedInDB = true;
				}
				sqlpersistedStreamDB.PersistStream(streamIndex, stream);
				if (!streamState.IsLocked)
				{
					sqlpersistedStreamDB.Commit();
				}
			}
			catch
			{
				if (!streamState.IsLocked)
				{
					sqlpersistedStreamDB.AbortTransaction();
				}
				throw;
			}
			finally
			{
				if (!streamState.IsLocked)
				{
					sqlpersistedStreamDB.Disconnect();
				}
			}
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0004F84C File Offset: 0x0004DA4C
		public override RSStream GetNextStream()
		{
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			SQLPersistedStreamDB sqlpersistedStreamDB = new SQLPersistedStreamDB(connectionManager, this.m_sessionID);
			RSStream rsstream;
			try
			{
				new Timer().StartTimer();
				RSStream nextStream = sqlpersistedStreamDB.GetNextStream();
				sqlpersistedStreamDB.Commit();
				rsstream = nextStream;
			}
			catch
			{
				sqlpersistedStreamDB.AbortTransaction();
				throw;
			}
			finally
			{
				sqlpersistedStreamDB.Disconnect();
			}
			return rsstream;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0004F8BC File Offset: 0x0004DABC
		public override void SetError(Exception e)
		{
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			bool flag = false;
			bool flag2 = true;
			if (base.LockIndex < this.m_streams.Count)
			{
				SQLPersistedStreams.StreamState streamState = (SQLPersistedStreams.StreamState)this.m_streams[base.LockIndex];
				if (streamState.IsLocked)
				{
					connectionManager = streamState.LockConnManager;
					flag2 = false;
				}
			}
			else
			{
				flag = true;
			}
			SQLPersistedStreamDB sqlpersistedStreamDB = new SQLPersistedStreamDB(connectionManager, this.m_sessionID);
			try
			{
				sqlpersistedStreamDB.SetError(e, base.LockIndex, flag);
			}
			finally
			{
				if (flag2)
				{
					sqlpersistedStreamDB.Disconnect();
				}
			}
		}

		// Token: 0x04000748 RID: 1864
		private string m_sessionID;

		// Token: 0x04000749 RID: 1865
		private ArrayList m_streams = new ArrayList();

		// Token: 0x020004AF RID: 1199
		private class StreamState
		{
			// Token: 0x17000A90 RID: 2704
			// (get) Token: 0x0600240D RID: 9229 RVA: 0x0008599C File Offset: 0x00083B9C
			public bool IsLocked
			{
				get
				{
					return this.LockConnManager != null;
				}
			}

			// Token: 0x0400109B RID: 4251
			public bool RowCreatedInDB;

			// Token: 0x0400109C RID: 4252
			public ConnectionManager LockConnManager;
		}
	}
}
