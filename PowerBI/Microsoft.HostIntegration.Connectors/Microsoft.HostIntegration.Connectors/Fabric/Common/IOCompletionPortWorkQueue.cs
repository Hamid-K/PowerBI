using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003E2 RID: 994
	internal class IOCompletionPortWorkQueue : IDisposable
	{
		// Token: 0x060022E0 RID: 8928 RVA: 0x0006B548 File Offset: 0x00069748
		internal IOCompletionPortWorkQueue(ThreadPriority priority)
			: this(priority, Environment.ProcessorCount * 25)
		{
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x0006B55C File Offset: 0x0006975C
		private IOCompletionPortWorkQueue(ThreadPriority priority, int maxNumberOfThreads)
		{
			this.m_lock = new object();
			this.m_workQueue = new IOCompletionPortWorkQueue.WorkItemQueue();
			this.m_ioCompletionPort = NativeMethods.CreateIoCompletionPort(IOCompletionPortWorkQueue.s_invalidHandle, IOCompletionPortWorkQueue.s_nullHandle, IntPtr.Zero, 0);
			if (this.m_ioCompletionPort.IsInvalid)
			{
				int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
				Marshal.ThrowExceptionForHR(hrforLastWin32Error);
			}
			this.m_priority = priority;
			this.m_maxNumberOfThreads = maxNumberOfThreads;
			this.m_currentNumberOfThreads = 0;
			this.m_numberOfIdleThreads = 0;
			this.m_freeList = new IOCompletionPortWorkQueue.WorkItemQueue();
			this.m_maxFreeListCount = Environment.ProcessorCount * 25;
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0006B5F9 File Offset: 0x000697F9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x0006B608 File Offset: 0x00069808
		private void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.m_ioCompletionPort.Close();
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x0006B618 File Offset: 0x00069818
		// (set) Token: 0x060022E5 RID: 8933 RVA: 0x0006B620 File Offset: 0x00069820
		public int MaximumNumberOfThreads
		{
			get
			{
				return this.m_maxNumberOfThreads;
			}
			set
			{
				if (this.m_maxNumberOfThreads < Environment.ProcessorCount)
				{
					return;
				}
				this.m_maxNumberOfThreads = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060022E6 RID: 8934 RVA: 0x0006B637 File Offset: 0x00069837
		public int CurrentNumberOfThreads
		{
			get
			{
				return this.m_currentNumberOfThreads;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x0006B63F File Offset: 0x0006983F
		public int CurrentNumberOfIdleThreads
		{
			get
			{
				return this.m_numberOfIdleThreads;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x0006B647 File Offset: 0x00069847
		public bool IsWorkQueueEmpty
		{
			get
			{
				return this.m_workQueue.IsEmpty;
			}
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0006B654 File Offset: 0x00069854
		public void QueueWorkItem(WaitCallback callback, object state)
		{
			IOCompletionPortWorkQueue.WorkItem workItem = null;
			if (this.m_freeList.IsEmpty)
			{
				workItem = new IOCompletionPortWorkQueue.WorkItem(callback, state);
			}
			bool isWorkQueueEmpty;
			bool flag;
			lock (this.m_lock)
			{
				isWorkQueueEmpty = this.IsWorkQueueEmpty;
				if (workItem == null)
				{
					if (!this.m_freeList.IsEmpty)
					{
						workItem = this.m_freeList.Dequeue();
						workItem.Initialize(callback, state);
					}
					else
					{
						workItem = new IOCompletionPortWorkQueue.WorkItem(callback, state);
					}
				}
				this.m_workQueue.InsertAsTail(workItem);
				flag = this.IsNewWorkerThreadNeeded();
			}
			if (isWorkQueueEmpty)
			{
				this.PostCompletion();
			}
			if (flag)
			{
				this.CreateWorkerThread();
			}
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0006B6F8 File Offset: 0x000698F8
		private void PostCompletion()
		{
			if (!NativeMethods.PostQueuedCompletionStatus(this.m_ioCompletionPort, 0, IntPtr.Zero, IOCompletionPortWorkQueue.s_lpDummyOverlapped))
			{
				int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
				Marshal.ThrowExceptionForHR(hrforLastWin32Error);
			}
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0006B72C File Offset: 0x0006992C
		private bool IsNewWorkerThreadNeeded()
		{
			bool flag = false;
			if (this.m_numberOfIdleThreads == 0 && this.m_currentNumberOfThreads < this.m_maxNumberOfThreads)
			{
				flag = true;
				this.m_currentNumberOfThreads++;
				this.m_numberOfIdleThreads = 1;
			}
			return flag;
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x0006B76C File Offset: 0x0006996C
		private void CreateWorkerThread()
		{
			new Thread(new ThreadStart(this.WorkerThreadStart))
			{
				IsBackground = true,
				Priority = this.m_priority
			}.Start();
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x0006B7A4 File Offset: 0x000699A4
		private unsafe void WorkerThreadStart()
		{
			SafeFileHandle currentThread = NativeMethods.GetCurrentThread();
			for (;;)
			{
				int num;
				IntPtr intPtr;
				NativeOverlapped* ptr;
				bool flag = NativeMethods.GetQueuedCompletionStatus(this.m_ioCompletionPort, out num, out intPtr, out ptr, this.WORKER_THREAD_IDLE_WAIT_TIME);
				if (flag)
				{
					ReleaseAssert.IsTrue(ptr == IOCompletionPortWorkQueue.s_lpDummyOverlapped);
					bool flag2 = false;
					WaitCallback callback;
					object callbackState;
					bool flag3;
					lock (this.m_lock)
					{
						IOCompletionPortWorkQueue.WorkItem workItem = this.m_workQueue.Dequeue();
						this.m_numberOfIdleThreads--;
						ReleaseAssert.IsTrue(this.m_currentNumberOfThreads > this.m_numberOfIdleThreads);
						callback = workItem.Callback;
						callbackState = workItem.CallbackState;
						if (this.m_freeList.Count < this.m_maxFreeListCount)
						{
							workItem.Initialize(null, null);
							this.m_freeList.InsertAsHead(workItem);
						}
						flag3 = !this.IsWorkQueueEmpty;
						if (flag3)
						{
							flag2 = this.IsNewWorkerThreadNeeded();
						}
					}
					if (flag2)
					{
						this.CreateWorkerThread();
					}
					if (flag3)
					{
						this.PostCompletion();
					}
					IOCompletionPortWorkQueue.Invoke(callback, callbackState);
					lock (this.m_lock)
					{
						ReleaseAssert.IsTrue(this.m_currentNumberOfThreads > this.m_numberOfIdleThreads);
						this.m_numberOfIdleThreads++;
						continue;
					}
				}
				int num2 = Marshal.GetLastWin32Error();
				if ((long)num2 == 258L)
				{
					ReleaseAssert.IsTrue(ptr == null);
					bool flag4;
					flag = NativeMethods.GetThreadIOPendingFlag(currentThread, out flag4);
					if (flag)
					{
						if (flag4)
						{
							continue;
						}
						lock (this.m_lock)
						{
							if (this.m_workQueue.Count < this.m_numberOfIdleThreads)
							{
								this.m_numberOfIdleThreads--;
								this.m_currentNumberOfThreads--;
								break;
							}
							continue;
						}
					}
					num2 = Marshal.GetLastWin32Error();
				}
				Marshal.ThrowExceptionForHR(num2);
			}
			currentThread.SetHandleAsInvalid();
			currentThread.Close();
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x0006B99C File Offset: 0x00069B9C
		private static void Invoke(WaitCallback callback, object state)
		{
			try
			{
				callback(state);
			}
			catch (Exception ex)
			{
				EventLogWriter.WriteError("IOCompletionPortWorkQueue", "wait callback {0} failed with exception {1}", new object[] { callback, ex });
				throw;
			}
		}

		// Token: 0x040015D1 RID: 5585
		private const uint WIN32_WAIT_TIMEOUT = 258U;

		// Token: 0x040015D2 RID: 5586
		private const int DEFAULT_MULTIPLICATION_FACTOR = 25;

		// Token: 0x040015D3 RID: 5587
		public int WORKER_THREAD_IDLE_WAIT_TIME = 180000;

		// Token: 0x040015D4 RID: 5588
		private static SafeFileHandle s_invalidHandle = new SafeFileHandle(new IntPtr(-1), false);

		// Token: 0x040015D5 RID: 5589
		private static SafeFileHandle s_nullHandle = new SafeFileHandle(IntPtr.Zero, false);

		// Token: 0x040015D6 RID: 5590
		private unsafe static NativeOverlapped* s_lpDummyOverlapped = (NativeOverlapped*)(void*)new IntPtr(-1);

		// Token: 0x040015D7 RID: 5591
		public static readonly IOCompletionPortWorkQueue HighPriorityWorkQueue = new IOCompletionPortWorkQueue(Priority.HighThreadPriority);

		// Token: 0x040015D8 RID: 5592
		public static readonly IOCompletionPortWorkQueue NormalPriorityWorkQueue = new IOCompletionPortWorkQueue(Priority.NormalThreadPriority);

		// Token: 0x040015D9 RID: 5593
		private object m_lock;

		// Token: 0x040015DA RID: 5594
		private IOCompletionPortWorkQueue.WorkItemQueue m_workQueue;

		// Token: 0x040015DB RID: 5595
		private SafeFileHandle m_ioCompletionPort;

		// Token: 0x040015DC RID: 5596
		private ThreadPriority m_priority;

		// Token: 0x040015DD RID: 5597
		private int m_maxNumberOfThreads;

		// Token: 0x040015DE RID: 5598
		private int m_currentNumberOfThreads;

		// Token: 0x040015DF RID: 5599
		private int m_numberOfIdleThreads;

		// Token: 0x040015E0 RID: 5600
		private IOCompletionPortWorkQueue.WorkItemQueue m_freeList;

		// Token: 0x040015E1 RID: 5601
		private int m_maxFreeListCount;

		// Token: 0x020003E3 RID: 995
		private class WorkItem
		{
			// Token: 0x060022F0 RID: 8944 RVA: 0x0006BA40 File Offset: 0x00069C40
			internal WorkItem(WaitCallback callback, object callbackState)
			{
				this.MarkAsDequeued();
				this.Initialize(callback, callbackState);
			}

			// Token: 0x060022F1 RID: 8945 RVA: 0x0006BA56 File Offset: 0x00069C56
			internal void Initialize(WaitCallback callback, object callbackState)
			{
				this.m_callback = callback;
				this.m_callbackState = callbackState;
			}

			// Token: 0x17000705 RID: 1797
			// (get) Token: 0x060022F2 RID: 8946 RVA: 0x0006BA66 File Offset: 0x00069C66
			internal WaitCallback Callback
			{
				get
				{
					return this.m_callback;
				}
			}

			// Token: 0x17000706 RID: 1798
			// (get) Token: 0x060022F3 RID: 8947 RVA: 0x0006BA6E File Offset: 0x00069C6E
			internal object CallbackState
			{
				get
				{
					return this.m_callbackState;
				}
			}

			// Token: 0x17000707 RID: 1799
			// (get) Token: 0x060022F4 RID: 8948 RVA: 0x0006BA76 File Offset: 0x00069C76
			// (set) Token: 0x060022F5 RID: 8949 RVA: 0x0006BA7E File Offset: 0x00069C7E
			internal IOCompletionPortWorkQueue.WorkItem Next
			{
				get
				{
					return this.m_next;
				}
				set
				{
					this.m_next = value;
				}
			}

			// Token: 0x17000708 RID: 1800
			// (get) Token: 0x060022F6 RID: 8950 RVA: 0x0006BA87 File Offset: 0x00069C87
			// (set) Token: 0x060022F7 RID: 8951 RVA: 0x0006BA8F File Offset: 0x00069C8F
			internal IOCompletionPortWorkQueue.WorkItem Prev
			{
				get
				{
					return this.m_prev;
				}
				set
				{
					this.m_prev = value;
				}
			}

			// Token: 0x060022F8 RID: 8952 RVA: 0x0006BA98 File Offset: 0x00069C98
			internal void MarkAsDequeued()
			{
				this.m_prev = this;
				this.m_next = this;
			}

			// Token: 0x040015E2 RID: 5602
			private IOCompletionPortWorkQueue.WorkItem m_next;

			// Token: 0x040015E3 RID: 5603
			private IOCompletionPortWorkQueue.WorkItem m_prev;

			// Token: 0x040015E4 RID: 5604
			private WaitCallback m_callback;

			// Token: 0x040015E5 RID: 5605
			private object m_callbackState;
		}

		// Token: 0x020003E4 RID: 996
		private class WorkItemQueue : IOCompletionPortWorkQueue.WorkItem
		{
			// Token: 0x060022F9 RID: 8953 RVA: 0x0006BAB5 File Offset: 0x00069CB5
			internal WorkItemQueue()
				: base(null, null)
			{
				this.m_count = 0;
			}

			// Token: 0x17000709 RID: 1801
			// (get) Token: 0x060022FA RID: 8954 RVA: 0x0006BAC6 File Offset: 0x00069CC6
			internal bool IsEmpty
			{
				get
				{
					return this.m_count == 0;
				}
			}

			// Token: 0x1700070A RID: 1802
			// (get) Token: 0x060022FB RID: 8955 RVA: 0x0006BAD1 File Offset: 0x00069CD1
			internal int Count
			{
				get
				{
					return this.m_count;
				}
			}

			// Token: 0x060022FC RID: 8956 RVA: 0x0006BADC File Offset: 0x00069CDC
			internal IOCompletionPortWorkQueue.WorkItem Dequeue()
			{
				IOCompletionPortWorkQueue.WorkItem workItem = null;
				if (this.m_count > 0)
				{
					workItem = base.Next;
					ReleaseAssert.IsTrue(workItem != this);
					base.Next = workItem.Next;
					workItem.Next.Prev = this;
					workItem.MarkAsDequeued();
					this.m_count--;
				}
				return workItem;
			}

			// Token: 0x060022FD RID: 8957 RVA: 0x0006BB34 File Offset: 0x00069D34
			internal void InsertAsHead(IOCompletionPortWorkQueue.WorkItem head)
			{
				head.Prev = this;
				head.Next = base.Next;
				base.Next.Prev = head;
				base.Next = head;
				this.m_count++;
			}

			// Token: 0x060022FE RID: 8958 RVA: 0x0006BB6A File Offset: 0x00069D6A
			internal void InsertAsTail(IOCompletionPortWorkQueue.WorkItem tail)
			{
				tail.Next = this;
				tail.Prev = base.Prev;
				base.Prev.Next = tail;
				base.Prev = tail;
				this.m_count++;
			}

			// Token: 0x040015E6 RID: 5606
			private int m_count;
		}
	}
}
