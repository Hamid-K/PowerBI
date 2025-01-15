using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000189 RID: 393
	public sealed class AsyncReaderWriterLock : IIdentifiable, IShuttable
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x00022D34 File Offset: 0x00020F34
		public AsyncReaderWriterLock([NotNull] string id)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(id, "id");
			this.m_id = id;
			this.m_lock = new object();
			this.m_active = true;
			this.m_workTicketManager = new WorkTicketManager(id + ".WorkTicketManager");
			this.m_pendingReaders = new LinkedList<AsyncReaderWriterLock.PendingAcquireReaderLock>();
			this.m_pendingWriters = new LinkedList<AsyncReaderWriterLock.PendingAcquireWriterLock>();
			this.m_activeReaders = 0;
			this.m_activeWriters = 0;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00022DA8 File Offset: 0x00020FA8
		public Task<AcquiredReaderLock> AcquireReaderLockAsync(string acquireId)
		{
			object @lock = this.m_lock;
			Task<AcquiredReaderLock> task;
			lock (@lock)
			{
				this.ThrowOnShutdownUnderLock(acquireId, "AcquireReaderLockAsync");
				if (this.m_activeWriters == 0 && this.m_pendingWriters.Count == 0)
				{
					task = Task.FromResult<AcquiredReaderLock>(this.AcquireReaderLock(acquireId, "AcquireReaderLockAsync"));
				}
				else
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "AsyncReaderWriterLock '{0}': AcquireReaderLockAsync({1}) invoked, pending", new object[] { this.m_id, acquireId });
					AsyncReaderWriterLock.PendingAcquireReaderLock pendingAcquireReaderLock = new AsyncReaderWriterLock.PendingAcquireReaderLock(acquireId);
					this.m_pendingReaders.AddLast(pendingAcquireReaderLock);
					task = pendingAcquireReaderLock.Task;
				}
			}
			return task;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00022E58 File Offset: 0x00021058
		public Task<AcquiredWriterLock> AcquireWriterLockAsync(string acquireId)
		{
			object @lock = this.m_lock;
			Task<AcquiredWriterLock> task;
			lock (@lock)
			{
				this.ThrowOnShutdownUnderLock(acquireId, "AcquireWriterLockAsync");
				if (this.m_activeReaders == 0 && this.m_activeWriters == 0 && this.m_pendingWriters.Count == 0)
				{
					task = Task.FromResult<AcquiredWriterLock>(this.AcquireWriterLock(acquireId, "AcquireWriterLockAsync"));
				}
				else
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "AsyncReaderWriterLock '{0}': AcquireWriterLockAsync({1}) invoked, pending", new object[] { this.m_id, acquireId });
					AsyncReaderWriterLock.PendingAcquireWriterLock pendingAcquireWriterLock = new AsyncReaderWriterLock.PendingAcquireWriterLock(acquireId);
					this.m_pendingWriters.AddLast(pendingAcquireWriterLock);
					task = pendingAcquireWriterLock.Task;
				}
			}
			return task;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00022F10 File Offset: 0x00021110
		public bool TryAcquireReaderLock(string acquireId, out IDisposable lockHandle)
		{
			lockHandle = null;
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.CheckShutdownStarted(acquireId, "TryAcquireReaderLock") && this.m_activeWriters == 0 && this.m_pendingWriters.Count == 0)
				{
					lockHandle = this.AcquireReaderLock(acquireId, "TryAcquireReaderLock");
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00022F88 File Offset: 0x00021188
		public bool TryAcquireWriterLock(string acquireId, out IDisposable lockHandle)
		{
			lockHandle = null;
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.CheckShutdownStarted(acquireId, "TryAcquireWriterLock") && this.m_activeReaders == 0 && this.m_activeWriters == 0 && this.m_pendingWriters.Count == 0)
				{
					lockHandle = this.AcquireWriterLock(acquireId, "TryAcquireWriterLock");
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00023008 File Offset: 0x00021208
		public AcquiredReaderLock DowngradeToReaderLock([NotNull] AcquiredWriterLock writerLock)
		{
			Ensure.ArgNotNull<AcquiredWriterLock>(writerLock, "writerLock");
			AsyncReaderWriterLock.AcquiredWriterLockImpl acquiredWriterLockImpl = (AsyncReaderWriterLock.AcquiredWriterLockImpl)writerLock;
			Ensure.ArgNotNull<WorkTicket>(acquiredWriterLockImpl.WorkTicket, "writerLock.WorkTicket");
			if (!acquiredWriterLockImpl.WorkTicket.IsValid())
			{
				Ensure.IsTrue(false, string.Concat(new string[] { "AsyncReaderWriterLock(", this.m_id, ").DowngradeToReaderLock(", acquiredWriterLockImpl.AcquireId, "): work ticket IsValid can't be false" }));
			}
			AsyncReaderWriterLock.AcquiredReaderLockImpl acquiredReaderLockImpl = new AsyncReaderWriterLock.AcquiredReaderLockImpl(acquiredWriterLockImpl);
			bool flag = false;
			object @lock = this.m_lock;
			lock (@lock)
			{
				ExtendedDiagnostics.EnsureArgument("m_activeReaders", this.m_activeReaders == 0, "There should not be any active readers (while '{0}' Active readers exist)".FormatWithInvariantCulture(new object[] { this.m_activeReaders }));
				ExtendedDiagnostics.EnsureArgument("m_activeWriters", this.m_activeWriters == 1, "There should be only one active writer (while '{0}' Active writers exist)".FormatWithInvariantCulture(new object[] { this.m_activeWriters }));
				this.m_activeWriters = 0;
				this.m_activeReaders = 1;
				flag = this.m_active && this.m_pendingReaders.Count > 0;
			}
			if (flag)
			{
				this.TrySatisfyPendingOperations();
			}
			return acquiredReaderLockImpl;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002314C File Offset: 0x0002134C
		private AcquiredReaderLock AcquireReaderLock(string acquireId, string methodName)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "AsyncReaderWriterLock '{0}': {2} ({1}) invoked, granted immediately", new object[] { this.m_id, acquireId, methodName });
			AcquiredReaderLock acquiredReaderLock = new AsyncReaderWriterLock.AcquiredReaderLockImpl(acquireId, this, this.m_workTicketManager);
			this.m_activeReaders++;
			return acquiredReaderLock;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002319B File Offset: 0x0002139B
		private AcquiredWriterLock AcquireWriterLock(string acquireId, string methodName)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "AsyncReaderWriterLock '{0}': {2} ({1}) invoked, granted immediately", new object[] { this.m_id, acquireId, methodName });
			AcquiredWriterLock acquiredWriterLock = new AsyncReaderWriterLock.AcquiredWriterLockImpl(acquireId, this, this.m_workTicketManager);
			this.m_activeWriters = 1;
			return acquiredWriterLock;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000231D8 File Offset: 0x000213D8
		private void ThrowOnShutdownUnderLock(string acquireId, string operation)
		{
			if (this.CheckShutdownStarted(acquireId, operation))
			{
				throw new ShutdownSequenceStartedException();
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000231EA File Offset: 0x000213EA
		private bool CheckShutdownStarted(string acquireId, string operation)
		{
			if (!this.m_active)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "AsyncReaderWriterLock '{0}': {1}({2}) invoked, denied due to shutdown", new object[] { this.m_id, operation, acquireId });
				return true;
			}
			return false;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00023220 File Offset: 0x00021420
		private void OnAcquiredReaderLockDisposed(AsyncReaderWriterLock.AcquiredReaderLockImpl readerLock)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_activeReaders--;
			}
			this.TrySatisfyPendingOperations();
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00023270 File Offset: 0x00021470
		private void TrySatisfyPendingOperations()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_active)
				{
					if (this.m_activeWriters == 0)
					{
						if (this.m_pendingWriters.Count > 0)
						{
							if (this.m_activeReaders <= 0)
							{
								TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNothing, delegate
								{
									AsyncReaderWriterLock.PendingAcquireWriterLock value = this.m_pendingWriters.First.Value;
									this.m_pendingWriters.RemoveFirst();
									AsyncReaderWriterLock.AcquiredWriterLockImpl acquiredWriterLockImpl = new AsyncReaderWriterLock.AcquiredWriterLockImpl(value.AcquireId, this, this.m_workTicketManager);
									this.m_activeWriters = 1;
									value.SetResultAsync(acquiredWriterLockImpl).DoNotWait();
								});
							}
						}
						else
						{
							while (this.m_pendingReaders.Count > 0)
							{
								TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNothing, delegate
								{
									AsyncReaderWriterLock.PendingAcquireReaderLock value2 = this.m_pendingReaders.First.Value;
									this.m_pendingReaders.RemoveFirst();
									AsyncReaderWriterLock.AcquiredReaderLockImpl acquiredReaderLockImpl = new AsyncReaderWriterLock.AcquiredReaderLockImpl(value2.AcquireId, this, this.m_workTicketManager);
									this.m_activeReaders++;
									value2.SetResultAsync(acquiredReaderLockImpl).DoNotWait();
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00023310 File Offset: 0x00021510
		private void OnAcquiredWriterLockDisposed(AsyncReaderWriterLock.AcquiredWriterLockImpl writerLock)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_activeWriters = 0;
			}
			this.TrySatisfyPendingOperations();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00023358 File Offset: 0x00021558
		internal AsyncReaderWriterLock.AsyncReaderWriterLockStatistics GetStatistics()
		{
			AsyncReaderWriterLock.AsyncReaderWriterLockStatistics asyncReaderWriterLockStatistics = default(AsyncReaderWriterLock.AsyncReaderWriterLockStatistics);
			object @lock = this.m_lock;
			lock (@lock)
			{
				asyncReaderWriterLockStatistics.ActiveReaders = this.m_activeReaders;
				asyncReaderWriterLockStatistics.ActiveWriters = this.m_activeWriters;
				if (this.m_active)
				{
					asyncReaderWriterLockStatistics.PendingReaders = this.m_pendingReaders.Count;
					asyncReaderWriterLockStatistics.PendingWriters = this.m_pendingWriters.Count;
				}
			}
			if (asyncReaderWriterLockStatistics.ActiveReaders != 0 && asyncReaderWriterLockStatistics.ActiveWriters != 0)
			{
				ExtendedDiagnostics.AlertDebugger(AlertDebuggerAction.LaunchManagedDebugger);
			}
			return asyncReaderWriterLockStatistics;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x000233FC File Offset: 0x000215FC
		public string Name
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00023404 File Offset: 0x00021604
		public void Stop()
		{
			LinkedList<AsyncReaderWriterLock.PendingAcquireReaderLock> linkedList = null;
			LinkedList<AsyncReaderWriterLock.PendingAcquireWriterLock> linkedList2 = null;
			object @lock = this.m_lock;
			lock (@lock)
			{
				Ensure.IsTrue(this.m_active, "AsyncReaderWriterLock.Stop has been invoked more than once");
				this.m_active = false;
				this.m_workTicketManager.Stop();
				linkedList = this.m_pendingReaders;
				this.m_pendingReaders = null;
				linkedList2 = this.m_pendingWriters;
				this.m_pendingWriters = null;
			}
			if (linkedList != null)
			{
				foreach (AsyncReaderWriterLock.PendingAcquireReaderLock pendingAcquireReaderLock in linkedList)
				{
					pendingAcquireReaderLock.SetCanceled();
				}
			}
			if (linkedList2 != null)
			{
				foreach (AsyncReaderWriterLock.PendingAcquireWriterLock pendingAcquireWriterLock in linkedList2)
				{
					pendingAcquireWriterLock.SetCanceled();
				}
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000234FC File Offset: 0x000216FC
		public void WaitForStopToComplete()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				Ensure.IsTrue(!this.m_active, "AsyncReaderWriterLock.WaitForStopToComplete has been called without Stop being called beforehand");
			}
			this.m_workTicketManager.WaitForStopToComplete();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00023554 File Offset: 0x00021754
		public void Shutdown()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				Ensure.IsTrue(!this.m_active, "AsyncReaderWriterLock.Shutdown has been called without Stop/WaitForStopToComplete being called beforehand");
			}
			this.m_workTicketManager.Shutdown();
		}

		// Token: 0x040003F5 RID: 1013
		private string m_id;

		// Token: 0x040003F6 RID: 1014
		private object m_lock;

		// Token: 0x040003F7 RID: 1015
		private bool m_active;

		// Token: 0x040003F8 RID: 1016
		private WorkTicketManager m_workTicketManager;

		// Token: 0x040003F9 RID: 1017
		private LinkedList<AsyncReaderWriterLock.PendingAcquireReaderLock> m_pendingReaders;

		// Token: 0x040003FA RID: 1018
		private LinkedList<AsyncReaderWriterLock.PendingAcquireWriterLock> m_pendingWriters;

		// Token: 0x040003FB RID: 1019
		private int m_activeReaders;

		// Token: 0x040003FC RID: 1020
		private int m_activeWriters;

		// Token: 0x0200065B RID: 1627
		internal struct AsyncReaderWriterLockStatistics
		{
			// Token: 0x17000719 RID: 1817
			// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000A042F File Offset: 0x0009E62F
			// (set) Token: 0x06002D2C RID: 11564 RVA: 0x000A0437 File Offset: 0x0009E637
			public int ActiveReaders { get; set; }

			// Token: 0x1700071A RID: 1818
			// (get) Token: 0x06002D2D RID: 11565 RVA: 0x000A0440 File Offset: 0x0009E640
			// (set) Token: 0x06002D2E RID: 11566 RVA: 0x000A0448 File Offset: 0x0009E648
			public int ActiveWriters { get; set; }

			// Token: 0x1700071B RID: 1819
			// (get) Token: 0x06002D2F RID: 11567 RVA: 0x000A0451 File Offset: 0x0009E651
			// (set) Token: 0x06002D30 RID: 11568 RVA: 0x000A0459 File Offset: 0x0009E659
			public int PendingReaders { get; set; }

			// Token: 0x1700071C RID: 1820
			// (get) Token: 0x06002D31 RID: 11569 RVA: 0x000A0462 File Offset: 0x0009E662
			// (set) Token: 0x06002D32 RID: 11570 RVA: 0x000A046A File Offset: 0x0009E66A
			public int PendingWriters { get; set; }
		}

		// Token: 0x0200065C RID: 1628
		private class PendingAcquireReaderLock : TaskCompletionSource<AcquiredReaderLock>
		{
			// Token: 0x1700071D RID: 1821
			// (get) Token: 0x06002D33 RID: 11571 RVA: 0x000A0473 File Offset: 0x0009E673
			// (set) Token: 0x06002D34 RID: 11572 RVA: 0x000A047B File Offset: 0x0009E67B
			public string AcquireId { get; set; }

			// Token: 0x06002D35 RID: 11573 RVA: 0x000A0484 File Offset: 0x0009E684
			public PendingAcquireReaderLock(string acquireId)
			{
				this.AcquireId = acquireId;
			}
		}

		// Token: 0x0200065D RID: 1629
		private class PendingAcquireWriterLock : TaskCompletionSource<AcquiredWriterLock>
		{
			// Token: 0x1700071E RID: 1822
			// (get) Token: 0x06002D36 RID: 11574 RVA: 0x000A0493 File Offset: 0x0009E693
			// (set) Token: 0x06002D37 RID: 11575 RVA: 0x000A049B File Offset: 0x0009E69B
			public string AcquireId { get; set; }

			// Token: 0x06002D38 RID: 11576 RVA: 0x000A04A4 File Offset: 0x0009E6A4
			public PendingAcquireWriterLock(string acquireId)
			{
				this.AcquireId = acquireId;
			}
		}

		// Token: 0x0200065E RID: 1630
		private sealed class AcquiredReaderLockImpl : AcquiredReaderLock
		{
			// Token: 0x1700071F RID: 1823
			// (get) Token: 0x06002D39 RID: 11577 RVA: 0x000A04B3 File Offset: 0x0009E6B3
			// (set) Token: 0x06002D3A RID: 11578 RVA: 0x000A04BB File Offset: 0x0009E6BB
			public string AcquireId { get; private set; }

			// Token: 0x17000720 RID: 1824
			// (get) Token: 0x06002D3B RID: 11579 RVA: 0x000A04C4 File Offset: 0x0009E6C4
			// (set) Token: 0x06002D3C RID: 11580 RVA: 0x000A04CC File Offset: 0x0009E6CC
			public WorkTicket WorkTicket { get; private set; }

			// Token: 0x17000721 RID: 1825
			// (get) Token: 0x06002D3D RID: 11581 RVA: 0x000A04D5 File Offset: 0x0009E6D5
			// (set) Token: 0x06002D3E RID: 11582 RVA: 0x000A04DD File Offset: 0x0009E6DD
			public AsyncReaderWriterLock Parent { get; private set; }

			// Token: 0x06002D3F RID: 11583 RVA: 0x000A04E6 File Offset: 0x0009E6E6
			public AcquiredReaderLockImpl(string acquireId, AsyncReaderWriterLock parent, IWorkTicketFactory ticketFactory)
			{
				this.AcquireId = acquireId;
				this.WorkTicket = ticketFactory.CreateWorkTicket(parent);
				this.Parent = parent;
			}

			// Token: 0x06002D40 RID: 11584 RVA: 0x000A0509 File Offset: 0x0009E709
			public AcquiredReaderLockImpl(AsyncReaderWriterLock.AcquiredWriterLockImpl writerLock)
			{
				this.AcquireId = writerLock.AcquireId;
				this.WorkTicket = writerLock.GiveUpWorkTicket();
				this.Parent = writerLock.Parent;
			}

			// Token: 0x06002D41 RID: 11585 RVA: 0x000A0535 File Offset: 0x0009E735
			internal override bool IsValid()
			{
				return this.WorkTicket != null && this.WorkTicket.IsValid();
			}

			// Token: 0x06002D42 RID: 11586 RVA: 0x000A054C File Offset: 0x0009E74C
			internal override string GetAcquireId()
			{
				return this.AcquireId;
			}

			// Token: 0x06002D43 RID: 11587 RVA: 0x000A0554 File Offset: 0x0009E754
			public override void Dispose()
			{
				if (this.WorkTicket != null)
				{
					this.Parent.OnAcquiredReaderLockDisposed(this);
					this.WorkTicket.Dispose();
					this.WorkTicket = null;
				}
			}
		}

		// Token: 0x0200065F RID: 1631
		private sealed class AcquiredWriterLockImpl : AcquiredWriterLock
		{
			// Token: 0x17000722 RID: 1826
			// (get) Token: 0x06002D44 RID: 11588 RVA: 0x000A057C File Offset: 0x0009E77C
			// (set) Token: 0x06002D45 RID: 11589 RVA: 0x000A0584 File Offset: 0x0009E784
			public string AcquireId { get; private set; }

			// Token: 0x17000723 RID: 1827
			// (get) Token: 0x06002D46 RID: 11590 RVA: 0x000A058D File Offset: 0x0009E78D
			// (set) Token: 0x06002D47 RID: 11591 RVA: 0x000A0595 File Offset: 0x0009E795
			public WorkTicket WorkTicket { get; private set; }

			// Token: 0x17000724 RID: 1828
			// (get) Token: 0x06002D48 RID: 11592 RVA: 0x000A059E File Offset: 0x0009E79E
			// (set) Token: 0x06002D49 RID: 11593 RVA: 0x000A05A6 File Offset: 0x0009E7A6
			public AsyncReaderWriterLock Parent { get; private set; }

			// Token: 0x06002D4A RID: 11594 RVA: 0x000A05AF File Offset: 0x0009E7AF
			public AcquiredWriterLockImpl(string acquireId, AsyncReaderWriterLock parent, IWorkTicketFactory ticketFactory)
			{
				this.AcquireId = acquireId;
				this.WorkTicket = ticketFactory.CreateWorkTicket(parent);
				this.Parent = parent;
			}

			// Token: 0x06002D4B RID: 11595 RVA: 0x000A05D2 File Offset: 0x0009E7D2
			internal override bool IsValid()
			{
				return this.WorkTicket != null && this.WorkTicket.IsValid();
			}

			// Token: 0x06002D4C RID: 11596 RVA: 0x000A05E9 File Offset: 0x0009E7E9
			internal override string GetAcquireId()
			{
				return this.AcquireId;
			}

			// Token: 0x06002D4D RID: 11597 RVA: 0x000A05F1 File Offset: 0x0009E7F1
			public WorkTicket GiveUpWorkTicket()
			{
				WorkTicket workTicket = this.WorkTicket;
				this.WorkTicket = null;
				return workTicket;
			}

			// Token: 0x06002D4E RID: 11598 RVA: 0x000A0600 File Offset: 0x0009E800
			public override void Dispose()
			{
				if (this.WorkTicket != null)
				{
					this.Parent.OnAcquiredWriterLockDisposed(this);
					this.WorkTicket.Dispose();
					this.WorkTicket = null;
				}
			}
		}
	}
}
