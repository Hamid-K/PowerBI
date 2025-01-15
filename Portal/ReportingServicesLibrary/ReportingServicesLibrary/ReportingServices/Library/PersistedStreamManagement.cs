using System;
using System.Collections;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000086 RID: 134
	internal abstract class PersistedStreamManagement
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000172C0 File Offset: 0x000154C0
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x000172C8 File Offset: 0x000154C8
		public bool PerformLocking
		{
			get
			{
				return this.m_performLocking;
			}
			set
			{
				this.m_performLocking = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000172D1 File Offset: 0x000154D1
		protected int LockIndex
		{
			get
			{
				return this.m_lastLockedStreamIndex;
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000172DC File Offset: 0x000154DC
		protected void EnsureAllLocksReleased()
		{
			if (!this.m_performLocking)
			{
				return;
			}
			try
			{
				this.ReleaseStream(this.m_lastLockedStreamIndex + 1);
			}
			finally
			{
				this.ReleaseStream(this.m_lastLockedStreamIndex);
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00017320 File Offset: 0x00015520
		public void AddNextStream(Stream stream)
		{
			IRenderStreamFinishNotification renderStreamFinishNotification = stream as IRenderStreamFinishNotification;
			if (renderStreamFinishNotification == null)
			{
				throw new InternalCatalogException("Persisted streams must implement IRenderStreamFinishNotification");
			}
			StreamState streamState = new StreamState(stream);
			this.m_streams.Add(streamState);
			renderStreamFinishNotification.StreamFinished += this.HandleStreamFinished;
			this.StreamAdded(this.m_streams.Count - 1, stream);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001737C File Offset: 0x0001557C
		public void AllStreamsFinished()
		{
			try
			{
				for (int i = 0; i < this.m_streams.Count; i++)
				{
					StreamState streamState = (StreamState)this.m_streams[i];
					if (!streamState.Persisted)
					{
						this.PersistStream(i, streamState.Stream);
						streamState.Persisted = true;
					}
				}
			}
			finally
			{
				this.EnsureAllLocksReleased();
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000173E8 File Offset: 0x000155E8
		public int GetTotalStreamCount()
		{
			return this.m_streams.Count - 1;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000173F8 File Offset: 0x000155F8
		private void HandleStreamFinished(object source, EventArgs args)
		{
			Stream stream = source as Stream;
			if (stream == null)
			{
				throw new InternalCatalogException("Persisting a stream which is not a stream!!!");
			}
			if (this.m_streams.Count == 0)
			{
				throw new InternalCatalogException("Stream should be added to list before hooking up event");
			}
			StreamState streamState = null;
			int i;
			for (i = this.m_streams.Count - 1; i >= 0; i--)
			{
				streamState = (StreamState)this.m_streams[i];
				if (stream == streamState.Stream)
				{
					break;
				}
			}
			if (i < 0 || streamState == null)
			{
				throw new InternalCatalogException("Stream fired finshed event that we never hooked up to.");
			}
			this.PersistStream(i, stream);
			streamState.Persisted = true;
			if (i == this.m_lastLockedStreamIndex && this.m_performLocking)
			{
				this.LockStream(this.m_lastLockedStreamIndex + 1);
				this.ReleaseStream(this.m_lastLockedStreamIndex);
				this.m_lastLockedStreamIndex++;
			}
			if (i == 0)
			{
				this.OnInitialStreamFinished(EventArgs.Empty);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060005A8 RID: 1448 RVA: 0x000174D0 File Offset: 0x000156D0
		// (remove) Token: 0x060005A9 RID: 1449 RVA: 0x00017508 File Offset: 0x00015708
		public event EventHandler InitialStreamFinished;

		// Token: 0x060005AA RID: 1450 RVA: 0x0001753D File Offset: 0x0001573D
		internal void OnInitialStreamFinished(EventArgs args)
		{
			if (this.InitialStreamFinished != null)
			{
				this.InitialStreamFinished(this, args);
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00017554 File Offset: 0x00015754
		public void ClearStreams()
		{
			this.m_streams.Clear();
			this.ResetPersistedStreams();
		}

		// Token: 0x060005AC RID: 1452
		protected abstract void StreamAdded(int streamIndex, Stream stream);

		// Token: 0x060005AD RID: 1453
		protected abstract void LockStream(int streamIndex);

		// Token: 0x060005AE RID: 1454
		protected abstract void ReleaseStream(int streamIndex);

		// Token: 0x060005AF RID: 1455
		protected abstract void PersistStream(int streamIndex, Stream stream);

		// Token: 0x060005B0 RID: 1456
		public abstract RSStream GetNextStream();

		// Token: 0x060005B1 RID: 1457
		public abstract void ResetPersistedStreams();

		// Token: 0x060005B2 RID: 1458
		public abstract void SetError(Exception e);

		// Token: 0x040002F9 RID: 761
		private ArrayList m_streams = new ArrayList();

		// Token: 0x040002FA RID: 762
		private int m_lastLockedStreamIndex;

		// Token: 0x040002FB RID: 763
		private bool m_performLocking = true;
	}
}
