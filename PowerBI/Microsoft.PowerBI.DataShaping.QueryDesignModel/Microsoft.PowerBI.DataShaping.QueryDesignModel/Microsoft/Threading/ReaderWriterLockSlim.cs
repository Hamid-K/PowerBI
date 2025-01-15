using System;
using System.Threading;

namespace Microsoft.Threading
{
	// Token: 0x020000C0 RID: 192
	internal class ReaderWriterLockSlim : IDisposable
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x0001F96E File Offset: 0x0001DB6E
		private void InitializeThreadCounts()
		{
			this.rwc = new ReaderWriterLockSlim.ReaderWriterCount[256];
			this.upgradeLockOwnerId = -1;
			this.writeLockOwnerId = -1;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0001F98E File Offset: 0x0001DB8E
		internal ReaderWriterLockSlim()
			: this(ReaderWriterLockSlim.LockRecursionPolicy.NoRecursion)
		{
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0001F997 File Offset: 0x0001DB97
		private ReaderWriterLockSlim(ReaderWriterLockSlim.LockRecursionPolicy recursionPolicy)
		{
			if (recursionPolicy == ReaderWriterLockSlim.LockRecursionPolicy.SupportsRecursion)
			{
				this.fIsReentrant = true;
			}
			this.InitializeThreadCounts();
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0001F9B0 File Offset: 0x0001DBB0
		private static bool IsRWEntryEmpty(ReaderWriterLockSlim.ReaderWriterCount rwc)
		{
			return rwc.threadid == -1 || (rwc.readercount == 0 && rwc.rc == null) || (rwc.readercount == 0 && rwc.rc.writercount == 0 && rwc.rc.upgradecount == 0);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0001F9FF File Offset: 0x0001DBFF
		private static bool IsRwHashEntryChanged(ReaderWriterLockSlim.ReaderWriterCount lrwc, int id)
		{
			return lrwc.threadid != id;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0001FA10 File Offset: 0x0001DC10
		private ReaderWriterLockSlim.ReaderWriterCount GetThreadRWCount(int id, bool DontAllocate)
		{
			int num = id & 255;
			ReaderWriterLockSlim.ReaderWriterCount readerWriterCount = null;
			if (this.rwc[num] == null)
			{
				if (DontAllocate)
				{
					return null;
				}
				this.rwc[num] = new ReaderWriterLockSlim.ReaderWriterCount(this.fIsReentrant);
			}
			if (this.rwc[num].threadid == id)
			{
				return this.rwc[num];
			}
			if (ReaderWriterLockSlim.IsRWEntryEmpty(this.rwc[num]) && !DontAllocate)
			{
				if (this.rwc[num].next == null)
				{
					this.rwc[num].threadid = id;
					return this.rwc[num];
				}
				readerWriterCount = this.rwc[num];
			}
			for (ReaderWriterLockSlim.ReaderWriterCount readerWriterCount2 = this.rwc[num].next; readerWriterCount2 != null; readerWriterCount2 = readerWriterCount2.next)
			{
				if (readerWriterCount2.threadid == id)
				{
					return readerWriterCount2;
				}
				if (readerWriterCount == null && ReaderWriterLockSlim.IsRWEntryEmpty(readerWriterCount2))
				{
					readerWriterCount = readerWriterCount2;
				}
			}
			if (DontAllocate)
			{
				return null;
			}
			if (readerWriterCount == null)
			{
				ReaderWriterLockSlim.ReaderWriterCount readerWriterCount2 = new ReaderWriterLockSlim.ReaderWriterCount(this.fIsReentrant);
				readerWriterCount2.threadid = id;
				readerWriterCount2.next = this.rwc[num].next;
				this.rwc[num].next = readerWriterCount2;
				return readerWriterCount2;
			}
			readerWriterCount.threadid = id;
			return readerWriterCount;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0001FB1B File Offset: 0x0001DD1B
		public void EnterReadLock()
		{
			this.TryEnterReadLock(-1);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0001FB28 File Offset: 0x0001DD28
		public bool TryEnterReadLock(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			int num2 = (int)timeout.TotalMilliseconds;
			return this.TryEnterReadLock(num2);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0001FB67 File Offset: 0x0001DD67
		public bool TryEnterReadLock(int millisecondsTimeout)
		{
			return this.TryEnterReadLockCore(millisecondsTimeout);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0001FB70 File Offset: 0x0001DD70
		private bool TryEnterReadLockCore(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (this.fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			ReaderWriterLockSlim.ReaderWriterCount readerWriterCount;
			if (!this.fIsReentrant)
			{
				if (managedThreadId == this.writeLockOwnerId)
				{
					throw new Exception(ReaderWriterLockSlim.SR.GetString("A read lock may not be acquired with the write lock held in this mode."));
				}
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(managedThreadId, false);
				if (readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new Exception(ReaderWriterLockSlim.SR.GetString("Recursive read lock acquisitions not allowed in this mode."));
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					readerWriterCount.readercount++;
					this.owners += 1U;
					this.ExitMyLock();
					return true;
				}
			}
			else
			{
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(managedThreadId, false);
				if (readerWriterCount.readercount > 0)
				{
					readerWriterCount.readercount++;
					this.ExitMyLock();
					return true;
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					readerWriterCount.readercount++;
					this.owners += 1U;
					this.ExitMyLock();
					this.fUpgradeThreadHoldingRead = true;
					return true;
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					readerWriterCount.readercount++;
					this.owners += 1U;
					this.ExitMyLock();
					return true;
				}
			}
			bool flag = true;
			int num = 0;
			while (this.owners >= 268435454U)
			{
				if (num < 20)
				{
					this.ExitMyLock();
					if (millisecondsTimeout == 0)
					{
						return false;
					}
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this.EnterMyLock();
					if (ReaderWriterLockSlim.IsRwHashEntryChanged(readerWriterCount, managedThreadId))
					{
						readerWriterCount = this.GetThreadRWCount(managedThreadId, false);
					}
				}
				else if (this.readEvent == null)
				{
					this.LazyCreateEvent(ref this.readEvent, false);
					if (ReaderWriterLockSlim.IsRwHashEntryChanged(readerWriterCount, managedThreadId))
					{
						readerWriterCount = this.GetThreadRWCount(managedThreadId, false);
					}
				}
				else
				{
					flag = this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout);
					if (!flag)
					{
						return false;
					}
					if (ReaderWriterLockSlim.IsRwHashEntryChanged(readerWriterCount, managedThreadId))
					{
						readerWriterCount = this.GetThreadRWCount(managedThreadId, false);
					}
				}
			}
			this.owners += 1U;
			readerWriterCount.readercount++;
			this.ExitMyLock();
			return flag;
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0001FD82 File Offset: 0x0001DF82
		public void EnterWriteLock()
		{
			this.TryEnterWriteLock(-1);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0001FD8C File Offset: 0x0001DF8C
		public bool TryEnterWriteLock(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			int num2 = (int)timeout.TotalMilliseconds;
			return this.TryEnterWriteLock(num2);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0001FDCB File Offset: 0x0001DFCB
		public bool TryEnterWriteLock(int millisecondsTimeout)
		{
			return this.TryEnterWriteLockCore(millisecondsTimeout);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0001FDD4 File Offset: 0x0001DFD4
		private bool TryEnterWriteLockCore(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (this.fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			bool flag = false;
			ReaderWriterLockSlim.ReaderWriterCount readerWriterCount;
			if (!this.fIsReentrant)
			{
				if (managedThreadId == this.writeLockOwnerId)
				{
					throw new Exception(ReaderWriterLockSlim.SR.GetString("Recursive write lock acquisitions not allowed in this mode."));
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					flag = true;
				}
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(managedThreadId, true);
				if (readerWriterCount != null && readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new Exception(ReaderWriterLockSlim.SR.GetString("Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock."));
				}
			}
			else
			{
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(managedThreadId, false);
				if (managedThreadId == this.writeLockOwnerId)
				{
					readerWriterCount.rc.writercount++;
					this.ExitMyLock();
					return true;
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					flag = true;
				}
				else if (readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new Exception(ReaderWriterLockSlim.SR.GetString("Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock."));
				}
			}
			int num = 0;
			while (!this.IsWriterAcquired())
			{
				if (flag)
				{
					uint numReaders = this.GetNumReaders();
					if (numReaders == 1U)
					{
						this.SetWriterAcquired();
					}
					else
					{
						if (numReaders != 2U || readerWriterCount == null)
						{
							goto IL_0145;
						}
						if (ReaderWriterLockSlim.IsRwHashEntryChanged(readerWriterCount, managedThreadId))
						{
							readerWriterCount = this.GetThreadRWCount(managedThreadId, false);
						}
						if (readerWriterCount.readercount <= 0)
						{
							goto IL_0145;
						}
						this.SetWriterAcquired();
					}
					IL_01D2:
					if (this.fIsReentrant)
					{
						if (ReaderWriterLockSlim.IsRwHashEntryChanged(readerWriterCount, managedThreadId))
						{
							readerWriterCount = this.GetThreadRWCount(managedThreadId, false);
						}
						readerWriterCount.rc.writercount++;
					}
					this.ExitMyLock();
					this.writeLockOwnerId = managedThreadId;
					return true;
				}
				IL_0145:
				if (num < 20)
				{
					this.ExitMyLock();
					if (millisecondsTimeout == 0)
					{
						return false;
					}
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this.EnterMyLock();
				}
				else if (flag)
				{
					if (this.waitUpgradeEvent == null)
					{
						this.LazyCreateEvent(ref this.waitUpgradeEvent, true);
					}
					else if (!this.WaitOnEvent(this.waitUpgradeEvent, ref this.numWriteUpgradeWaiters, millisecondsTimeout))
					{
						return false;
					}
				}
				else if (this.writeEvent == null)
				{
					this.LazyCreateEvent(ref this.writeEvent, true);
				}
				else if (!this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, millisecondsTimeout))
				{
					return false;
				}
			}
			this.SetWriterAcquired();
			goto IL_01D2;
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0001FFEE File Offset: 0x0001E1EE
		public void EnterUpgradeableReadLock()
		{
			this.TryEnterUpgradeableReadLock(-1);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0001FFF8 File Offset: 0x0001E1F8
		public bool TryEnterUpgradeableReadLock(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			int num2 = (int)timeout.TotalMilliseconds;
			return this.TryEnterUpgradeableReadLock(num2);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00020037 File Offset: 0x0001E237
		public bool TryEnterUpgradeableReadLock(int millisecondsTimeout)
		{
			return this.TryEnterUpgradeableReadLockCore(millisecondsTimeout);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00020040 File Offset: 0x0001E240
		private bool TryEnterUpgradeableReadLockCore(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (this.fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			ReaderWriterLockSlim.ReaderWriterCount readerWriterCount;
			if (!this.fIsReentrant)
			{
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					throw new Exception(ReaderWriterLockSlim.SR.GetString("Recursive upgradeable lock acquisitions not allowed in this mode."));
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					throw new Exception(ReaderWriterLockSlim.SR.GetString("Upgradeable lock may not be acquired with write lock held in this mode. Acquiring Upgradeable lock gives the ability to read along with an option to upgrade to a writer."));
				}
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(managedThreadId, true);
				if (readerWriterCount != null && readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new Exception(ReaderWriterLockSlim.SR.GetString("Upgradeable lock may not be acquired with read lock held."));
				}
			}
			else
			{
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(managedThreadId, false);
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					readerWriterCount.rc.upgradecount++;
					this.ExitMyLock();
					return true;
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					this.owners += 1U;
					this.upgradeLockOwnerId = managedThreadId;
					readerWriterCount.rc.upgradecount++;
					if (readerWriterCount.readercount > 0)
					{
						this.fUpgradeThreadHoldingRead = true;
					}
					this.ExitMyLock();
					return true;
				}
				if (readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new Exception(ReaderWriterLockSlim.SR.GetString("Upgradeable lock may not be acquired with read lock held."));
				}
			}
			int num = 0;
			while (this.upgradeLockOwnerId != -1 || this.owners >= 268435454U)
			{
				if (num < 20)
				{
					this.ExitMyLock();
					if (millisecondsTimeout == 0)
					{
						return false;
					}
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this.EnterMyLock();
				}
				else if (this.upgradeEvent == null)
				{
					this.LazyCreateEvent(ref this.upgradeEvent, true);
				}
				else if (!this.WaitOnEvent(this.upgradeEvent, ref this.numUpgradeWaiters, millisecondsTimeout))
				{
					return false;
				}
			}
			this.owners += 1U;
			this.upgradeLockOwnerId = managedThreadId;
			if (this.fIsReentrant)
			{
				if (ReaderWriterLockSlim.IsRwHashEntryChanged(readerWriterCount, managedThreadId))
				{
					readerWriterCount = this.GetThreadRWCount(managedThreadId, false);
				}
				readerWriterCount.rc.upgradecount++;
			}
			this.ExitMyLock();
			return true;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00020238 File Offset: 0x0001E438
		public void ExitReadLock()
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			this.EnterMyLock();
			ReaderWriterLockSlim.ReaderWriterCount threadRWCount = this.GetThreadRWCount(managedThreadId, true);
			if (!this.fIsReentrant)
			{
				if (threadRWCount == null)
				{
					this.ExitMyLock();
					throw new Exception(ReaderWriterLockSlim.SR.GetString("The read lock is being released without being held."));
				}
			}
			else
			{
				if (threadRWCount == null || threadRWCount.readercount < 1)
				{
					this.ExitMyLock();
					throw new Exception(ReaderWriterLockSlim.SR.GetString("The read lock is being released without being held."));
				}
				if (threadRWCount.readercount > 1)
				{
					threadRWCount.readercount--;
					this.ExitMyLock();
					return;
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					this.fUpgradeThreadHoldingRead = false;
				}
			}
			this.owners -= 1U;
			threadRWCount.readercount--;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000202F4 File Offset: 0x0001E4F4
		public void ExitWriteLock()
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			if (!this.fIsReentrant)
			{
				if (managedThreadId != this.writeLockOwnerId)
				{
					throw new Exception(ReaderWriterLockSlim.SR.GetString("The write lock is being released without being held."));
				}
				this.EnterMyLock();
			}
			else
			{
				this.EnterMyLock();
				ReaderWriterLockSlim.ReaderWriterCount threadRWCount = this.GetThreadRWCount(managedThreadId, false);
				if (threadRWCount == null)
				{
					this.ExitMyLock();
					throw new Exception(ReaderWriterLockSlim.SR.GetString("The write lock is being released without being held."));
				}
				ReaderWriterLockSlim.RecursiveCounts rc = threadRWCount.rc;
				if (rc.writercount < 1)
				{
					this.ExitMyLock();
					throw new Exception(ReaderWriterLockSlim.SR.GetString("The write lock is being released without being held."));
				}
				rc.writercount--;
				if (rc.writercount > 0)
				{
					this.ExitMyLock();
					return;
				}
			}
			this.ClearWriterAcquired();
			this.writeLockOwnerId = -1;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000203B0 File Offset: 0x0001E5B0
		public void ExitUpgradeableReadLock()
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			if (!this.fIsReentrant)
			{
				if (managedThreadId != this.upgradeLockOwnerId)
				{
					throw new Exception(ReaderWriterLockSlim.SR.GetString("The upgradeable lock is being released without being held."));
				}
				this.EnterMyLock();
			}
			else
			{
				this.EnterMyLock();
				ReaderWriterLockSlim.ReaderWriterCount threadRWCount = this.GetThreadRWCount(managedThreadId, true);
				if (threadRWCount == null)
				{
					this.ExitMyLock();
					throw new Exception(ReaderWriterLockSlim.SR.GetString("The upgradeable lock is being released without being held."));
				}
				ReaderWriterLockSlim.RecursiveCounts rc = threadRWCount.rc;
				if (rc.upgradecount < 1)
				{
					this.ExitMyLock();
					throw new Exception(ReaderWriterLockSlim.SR.GetString("The upgradeable lock is being released without being held."));
				}
				rc.upgradecount--;
				if (rc.upgradecount > 0)
				{
					this.ExitMyLock();
					return;
				}
				this.fUpgradeThreadHoldingRead = false;
			}
			this.owners -= 1U;
			this.upgradeLockOwnerId = -1;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002047C File Offset: 0x0001E67C
		private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
		{
			this.ExitMyLock();
			EventWaitHandle eventWaitHandle;
			if (makeAutoResetEvent)
			{
				eventWaitHandle = new AutoResetEvent(false);
			}
			else
			{
				eventWaitHandle = new ManualResetEvent(false);
			}
			this.EnterMyLock();
			if (waitEvent == null)
			{
				waitEvent = eventWaitHandle;
				return;
			}
			eventWaitHandle.Close();
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000204B8 File Offset: 0x0001E6B8
		private bool WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, int millisecondsTimeout)
		{
			waitEvent.Reset();
			numWaiters += 1U;
			this.fNoWaiters = false;
			if (this.numWriteWaiters == 1U)
			{
				this.SetWritersWaiting();
			}
			if (this.numWriteUpgradeWaiters == 1U)
			{
				this.SetUpgraderWaiting();
			}
			bool flag = false;
			this.ExitMyLock();
			try
			{
				flag = waitEvent.WaitOne(millisecondsTimeout);
			}
			finally
			{
				this.EnterMyLock();
				numWaiters -= 1U;
				if (this.numWriteWaiters == 0U && this.numWriteUpgradeWaiters == 0U && this.numUpgradeWaiters == 0U && this.numReadWaiters == 0U)
				{
					this.fNoWaiters = true;
				}
				if (this.numWriteWaiters == 0U)
				{
					this.ClearWritersWaiting();
				}
				if (this.numWriteUpgradeWaiters == 0U)
				{
					this.ClearUpgraderWaiting();
				}
				if (!flag)
				{
					this.ExitMyLock();
				}
			}
			return flag;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00020574 File Offset: 0x0001E774
		private void ExitAndWakeUpAppropriateWaiters()
		{
			if (this.fNoWaiters)
			{
				this.ExitMyLock();
				return;
			}
			this.ExitAndWakeUpAppropriateWaitersPreferringWriters();
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002058C File Offset: 0x0001E78C
		private void ExitAndWakeUpAppropriateWaitersPreferringWriters()
		{
			bool flag = false;
			bool flag2 = false;
			uint numReaders = this.GetNumReaders();
			if (this.fIsReentrant && this.numWriteUpgradeWaiters > 0U && this.fUpgradeThreadHoldingRead && numReaders == 2U)
			{
				this.ExitMyLock();
				this.waitUpgradeEvent.Set();
				return;
			}
			if (numReaders == 1U && this.numWriteUpgradeWaiters > 0U)
			{
				this.ExitMyLock();
				this.waitUpgradeEvent.Set();
				return;
			}
			if (numReaders == 0U && this.numWriteWaiters > 0U)
			{
				this.ExitMyLock();
				this.writeEvent.Set();
				return;
			}
			if (numReaders >= 0U)
			{
				if (this.numReadWaiters == 0U && this.numUpgradeWaiters == 0U)
				{
					this.ExitMyLock();
					return;
				}
				if (this.numReadWaiters != 0U)
				{
					flag2 = true;
				}
				if (this.numUpgradeWaiters != 0U && this.upgradeLockOwnerId == -1)
				{
					flag = true;
				}
				this.ExitMyLock();
				if (flag2)
				{
					this.readEvent.Set();
				}
				if (flag)
				{
					this.upgradeEvent.Set();
					return;
				}
			}
			else
			{
				this.ExitMyLock();
			}
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00020676 File Offset: 0x0001E876
		private bool IsWriterAcquired()
		{
			return (this.owners & 3221225471U) == 0U;
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00020687 File Offset: 0x0001E887
		private void SetWriterAcquired()
		{
			this.owners |= 2147483648U;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002069B File Offset: 0x0001E89B
		private void ClearWriterAcquired()
		{
			this.owners &= 2147483647U;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000206AF File Offset: 0x0001E8AF
		private void SetWritersWaiting()
		{
			this.owners |= 1073741824U;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x000206C3 File Offset: 0x0001E8C3
		private void ClearWritersWaiting()
		{
			this.owners &= 3221225471U;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x000206D7 File Offset: 0x0001E8D7
		private void SetUpgraderWaiting()
		{
			this.owners |= 536870912U;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x000206EB File Offset: 0x0001E8EB
		private void ClearUpgraderWaiting()
		{
			this.owners &= 3758096383U;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x000206FF File Offset: 0x0001E8FF
		private uint GetNumReaders()
		{
			return this.owners & 268435455U;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002070D File Offset: 0x0001E90D
		private void EnterMyLock()
		{
			if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
			{
				this.EnterMyLockSpin();
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00020724 File Offset: 0x0001E924
		private void EnterMyLockSpin()
		{
			int processorCount = Environment.ProcessorCount;
			int num = 0;
			for (;;)
			{
				if (num < 10 && processorCount > 1)
				{
					Thread.SpinWait(20 * (num + 1));
				}
				else if (num < 15)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Sleep(1);
				}
				if (this.myLock == 0 && Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
				{
					break;
				}
				num++;
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002077F File Offset: 0x0001E97F
		private void ExitMyLock()
		{
			this.myLock = 0;
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00020788 File Offset: 0x0001E988
		private static void SpinWait(int SpinCount)
		{
			if (SpinCount < 5 && Environment.ProcessorCount > 1)
			{
				Thread.SpinWait(20 * SpinCount);
				return;
			}
			if (SpinCount < 17)
			{
				Thread.Sleep(0);
				return;
			}
			Thread.Sleep(1);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000207B2 File Offset: 0x0001E9B2
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000207BC File Offset: 0x0001E9BC
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.fDisposed)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.WaitingReadCount > 0 || this.WaitingUpgradeCount > 0 || this.WaitingWriteCount > 0)
				{
					throw new Exception(ReaderWriterLockSlim.SR.GetString("The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock."));
				}
				if (this.IsReadLockHeld || this.IsUpgradeableReadLockHeld || this.IsWriteLockHeld)
				{
					throw new Exception(ReaderWriterLockSlim.SR.GetString("The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock."));
				}
				if (this.writeEvent != null)
				{
					this.writeEvent.Close();
					this.writeEvent = null;
				}
				if (this.readEvent != null)
				{
					this.readEvent.Close();
					this.readEvent = null;
				}
				if (this.upgradeEvent != null)
				{
					this.upgradeEvent.Close();
					this.upgradeEvent = null;
				}
				if (this.waitUpgradeEvent != null)
				{
					this.waitUpgradeEvent.Close();
					this.waitUpgradeEvent = null;
				}
				this.fDisposed = true;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x000208A0 File Offset: 0x0001EAA0
		public bool IsReadLockHeld
		{
			get
			{
				return this.RecursiveReadCount > 0;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x000208AE File Offset: 0x0001EAAE
		public bool IsUpgradeableReadLockHeld
		{
			get
			{
				return this.RecursiveUpgradeCount > 0;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x000208BC File Offset: 0x0001EABC
		public bool IsWriteLockHeld
		{
			get
			{
				return this.RecursiveWriteCount > 0;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x000208CC File Offset: 0x0001EACC
		public int CurrentReadCount
		{
			get
			{
				int numReaders = (int)this.GetNumReaders();
				if (this.upgradeLockOwnerId != -1)
				{
					return numReaders - 1;
				}
				return numReaders;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x000208F0 File Offset: 0x0001EAF0
		public int RecursiveReadCount
		{
			get
			{
				int managedThreadId = Thread.CurrentThread.ManagedThreadId;
				int num = 0;
				this.EnterMyLock();
				ReaderWriterLockSlim.ReaderWriterCount threadRWCount = this.GetThreadRWCount(managedThreadId, true);
				if (threadRWCount != null)
				{
					num = threadRWCount.readercount;
				}
				this.ExitMyLock();
				return num;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0002092C File Offset: 0x0001EB2C
		public int RecursiveUpgradeCount
		{
			get
			{
				int managedThreadId = Thread.CurrentThread.ManagedThreadId;
				if (this.fIsReentrant)
				{
					int num = 0;
					this.EnterMyLock();
					ReaderWriterLockSlim.ReaderWriterCount threadRWCount = this.GetThreadRWCount(managedThreadId, true);
					if (threadRWCount != null)
					{
						num = threadRWCount.rc.upgradecount;
					}
					this.ExitMyLock();
					return num;
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00020980 File Offset: 0x0001EB80
		public int RecursiveWriteCount
		{
			get
			{
				int managedThreadId = Thread.CurrentThread.ManagedThreadId;
				int num = 0;
				if (this.fIsReentrant)
				{
					this.EnterMyLock();
					ReaderWriterLockSlim.ReaderWriterCount threadRWCount = this.GetThreadRWCount(managedThreadId, true);
					if (threadRWCount != null)
					{
						num = threadRWCount.rc.writercount;
					}
					this.ExitMyLock();
					return num;
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x000209D4 File Offset: 0x0001EBD4
		public int WaitingReadCount
		{
			get
			{
				return (int)this.numReadWaiters;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x000209DC File Offset: 0x0001EBDC
		public int WaitingUpgradeCount
		{
			get
			{
				return (int)this.numUpgradeWaiters;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x000209E4 File Offset: 0x0001EBE4
		public int WaitingWriteCount
		{
			get
			{
				return (int)this.numWriteWaiters;
			}
		}

		// Token: 0x04000950 RID: 2384
		private bool fIsReentrant;

		// Token: 0x04000951 RID: 2385
		private int myLock;

		// Token: 0x04000952 RID: 2386
		private const int LockSpinCycles = 20;

		// Token: 0x04000953 RID: 2387
		private const int LockSpinCount = 10;

		// Token: 0x04000954 RID: 2388
		private const int LockSleep0Count = 5;

		// Token: 0x04000955 RID: 2389
		private uint numWriteWaiters;

		// Token: 0x04000956 RID: 2390
		private uint numReadWaiters;

		// Token: 0x04000957 RID: 2391
		private uint numWriteUpgradeWaiters;

		// Token: 0x04000958 RID: 2392
		private uint numUpgradeWaiters;

		// Token: 0x04000959 RID: 2393
		private bool fNoWaiters;

		// Token: 0x0400095A RID: 2394
		private int upgradeLockOwnerId;

		// Token: 0x0400095B RID: 2395
		private int writeLockOwnerId;

		// Token: 0x0400095C RID: 2396
		private EventWaitHandle writeEvent;

		// Token: 0x0400095D RID: 2397
		private EventWaitHandle readEvent;

		// Token: 0x0400095E RID: 2398
		private EventWaitHandle upgradeEvent;

		// Token: 0x0400095F RID: 2399
		private EventWaitHandle waitUpgradeEvent;

		// Token: 0x04000960 RID: 2400
		private ReaderWriterLockSlim.ReaderWriterCount[] rwc;

		// Token: 0x04000961 RID: 2401
		private bool fUpgradeThreadHoldingRead;

		// Token: 0x04000962 RID: 2402
		private const int hashTableSize = 255;

		// Token: 0x04000963 RID: 2403
		private const int MaxSpinCount = 20;

		// Token: 0x04000964 RID: 2404
		private uint owners;

		// Token: 0x04000965 RID: 2405
		private const uint WRITER_HELD = 2147483648U;

		// Token: 0x04000966 RID: 2406
		private const uint WAITING_WRITERS = 1073741824U;

		// Token: 0x04000967 RID: 2407
		private const uint WAITING_UPGRADER = 536870912U;

		// Token: 0x04000968 RID: 2408
		private const uint MAX_READER = 268435454U;

		// Token: 0x04000969 RID: 2409
		private const uint READER_MASK = 268435455U;

		// Token: 0x0400096A RID: 2410
		private bool fDisposed;

		// Token: 0x020002CC RID: 716
		private static class SR
		{
			// Token: 0x06001CA8 RID: 7336 RVA: 0x0004F5A7 File Offset: 0x0004D7A7
			internal static string GetString(string value)
			{
				return value;
			}

			// Token: 0x04001001 RID: 4097
			internal const string LockRecursionException_RecursiveReadNotAllowed = "Recursive read lock acquisitions not allowed in this mode.";

			// Token: 0x04001002 RID: 4098
			internal const string LockRecursionException_RecursiveWriteNotAllowed = "Recursive write lock acquisitions not allowed in this mode.";

			// Token: 0x04001003 RID: 4099
			internal const string LockRecursionException_RecursiveUpgradeNotAllowed = "Recursive upgradeable lock acquisitions not allowed in this mode.";

			// Token: 0x04001004 RID: 4100
			internal const string LockRecursionException_ReadAfterWriteNotAllowed = "A read lock may not be acquired with the write lock held in this mode.";

			// Token: 0x04001005 RID: 4101
			internal const string LockRecursionException_WriteAfterReadNotAllowed = "Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock.";

			// Token: 0x04001006 RID: 4102
			internal const string LockRecursionException_UpgradeAfterReadNotAllowed = "Upgradeable lock may not be acquired with read lock held.";

			// Token: 0x04001007 RID: 4103
			internal const string LockRecursionException_UpgradeAfterWriteNotAllowed = "Upgradeable lock may not be acquired with write lock held in this mode. Acquiring Upgradeable lock gives the ability to read along with an option to upgrade to a writer.";

			// Token: 0x04001008 RID: 4104
			internal const string SynchronizationLockException_MisMatchedRead = "The read lock is being released without being held.";

			// Token: 0x04001009 RID: 4105
			internal const string SynchronizationLockException_MisMatchedWrite = "The write lock is being released without being held.";

			// Token: 0x0400100A RID: 4106
			internal const string SynchronizationLockException_MisMatchedUpgrade = "The upgradeable lock is being released without being held.";

			// Token: 0x0400100B RID: 4107
			internal const string SynchronizationLockException_IncorrectDispose = "The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock.";
		}

		// Token: 0x020002CD RID: 717
		private enum LockRecursionPolicy
		{
			// Token: 0x0400100D RID: 4109
			NoRecursion,
			// Token: 0x0400100E RID: 4110
			SupportsRecursion
		}

		// Token: 0x020002CE RID: 718
		private class RecursiveCounts
		{
			// Token: 0x0400100F RID: 4111
			internal int writercount;

			// Token: 0x04001010 RID: 4112
			internal int upgradecount;
		}

		// Token: 0x020002CF RID: 719
		private class ReaderWriterCount
		{
			// Token: 0x06001CAA RID: 7338 RVA: 0x0004F5B2 File Offset: 0x0004D7B2
			internal ReaderWriterCount(bool fIsReentrant)
			{
				this.threadid = -1;
				if (fIsReentrant)
				{
					this.rc = new ReaderWriterLockSlim.RecursiveCounts();
				}
			}

			// Token: 0x04001011 RID: 4113
			internal int threadid;

			// Token: 0x04001012 RID: 4114
			internal int readercount;

			// Token: 0x04001013 RID: 4115
			internal ReaderWriterLockSlim.ReaderWriterCount next;

			// Token: 0x04001014 RID: 4116
			internal ReaderWriterLockSlim.RecursiveCounts rc;
		}
	}
}
