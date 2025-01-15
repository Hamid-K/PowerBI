using System;
using System.IO;
using Microsoft.Mashup.Shims.Interprocess;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B35 RID: 6965
	public sealed class SharedExclusiveLock : IDisposable
	{
		// Token: 0x0600AE57 RID: 44631 RVA: 0x0023B1E7 File Offset: 0x002393E7
		public SharedExclusiveLock(string identity)
		{
			this.sharedLockCount = new SharedExclusiveLock.SharedLockCounter(identity + ".shared");
			this.exclusiveSemaphore = new NamedSemaphore(identity + ".exclusive", 1);
			this.lockState = SharedExclusiveLock.LockState.None;
		}

		// Token: 0x0600AE58 RID: 44632 RVA: 0x0023B223 File Offset: 0x00239423
		public static string CreateIdentityFromFilePath(string path)
		{
			return path.ToUpperInvariant().Replace("_", "__").Replace(Path.DirectorySeparatorChar, '_');
		}

		// Token: 0x0600AE59 RID: 44633 RVA: 0x0023B246 File Offset: 0x00239446
		public bool TryAcquireSharedLock(TimeSpan timeout)
		{
			if (this.lockState != SharedExclusiveLock.LockState.None)
			{
				throw new InvalidOperationException();
			}
			if (this.exclusiveSemaphore.Wait(timeout))
			{
				this.sharedLockCount.Increment();
				this.exclusiveSemaphore.Release();
				this.lockState = SharedExclusiveLock.LockState.Shared;
				return true;
			}
			return false;
		}

		// Token: 0x0600AE5A RID: 44634 RVA: 0x0023B284 File Offset: 0x00239484
		public bool TryAcquireExclusiveLock(TimeSpan timeout)
		{
			if (this.lockState == SharedExclusiveLock.LockState.Shared)
			{
				if (this.exclusiveSemaphore.Wait(timeout))
				{
					this.sharedLockCount.Decrement();
					bool isZero = this.sharedLockCount.IsZero;
					this.sharedLockCount.Increment();
					if (isZero)
					{
						this.lockState = SharedExclusiveLock.LockState.Exclusive;
						return true;
					}
					this.exclusiveSemaphore.Release();
				}
				return false;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600AE5B RID: 44635 RVA: 0x0023B2E6 File Offset: 0x002394E6
		public void ReleaseExclusiveLock()
		{
			if (this.lockState == SharedExclusiveLock.LockState.Exclusive)
			{
				this.exclusiveSemaphore.Release();
				this.lockState = SharedExclusiveLock.LockState.Shared;
				return;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600AE5C RID: 44636 RVA: 0x0023B30C File Offset: 0x0023950C
		public void ReleaseSharedLock()
		{
			if (this.lockState == SharedExclusiveLock.LockState.Shared)
			{
				this.exclusiveSemaphore.Wait();
				try
				{
					this.sharedLockCount.Decrement();
					this.lockState = SharedExclusiveLock.LockState.None;
					return;
				}
				finally
				{
					this.exclusiveSemaphore.Release();
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600AE5D RID: 44637 RVA: 0x0023B364 File Offset: 0x00239564
		public void Dispose()
		{
			if (this.sharedLockCount != null)
			{
				this.sharedLockCount.Dispose();
				this.exclusiveSemaphore.Dispose();
				this.sharedLockCount = null;
				this.exclusiveSemaphore = null;
				if (this.lockState != SharedExclusiveLock.LockState.None)
				{
					throw new InvalidOperationException("Lock was disposed while still acquired.");
				}
			}
		}

		// Token: 0x040059E7 RID: 23015
		private const string sharedLockSuffix = ".shared";

		// Token: 0x040059E8 RID: 23016
		private const string exclusiveLockSuffix = ".exclusive";

		// Token: 0x040059E9 RID: 23017
		private SharedExclusiveLock.SharedLockCounter sharedLockCount;

		// Token: 0x040059EA RID: 23018
		private NamedSemaphore exclusiveSemaphore;

		// Token: 0x040059EB RID: 23019
		private SharedExclusiveLock.LockState lockState;

		// Token: 0x02001B36 RID: 6966
		private enum LockState
		{
			// Token: 0x040059ED RID: 23021
			None,
			// Token: 0x040059EE RID: 23022
			Shared,
			// Token: 0x040059EF RID: 23023
			Exclusive
		}

		// Token: 0x02001B37 RID: 6967
		private class SharedLockCounter
		{
			// Token: 0x0600AE5E RID: 44638 RVA: 0x0023B3B0 File Offset: 0x002395B0
			public SharedLockCounter(string name)
			{
				this.countingSemaphore = new NamedSemaphore(name);
			}

			// Token: 0x0600AE5F RID: 44639 RVA: 0x0023B3C4 File Offset: 0x002395C4
			public void Increment()
			{
				this.countingSemaphore.Release();
			}

			// Token: 0x0600AE60 RID: 44640 RVA: 0x0023B3D1 File Offset: 0x002395D1
			public void Decrement()
			{
				if (!this.countingSemaphore.Wait(TimeSpan.Zero))
				{
					throw new InvalidOperationException("Cannot decrement a zero valued counter");
				}
			}

			// Token: 0x17002BC8 RID: 11208
			// (get) Token: 0x0600AE61 RID: 44641 RVA: 0x0023B3F0 File Offset: 0x002395F0
			public bool IsZero
			{
				get
				{
					bool flag = this.countingSemaphore.Wait(TimeSpan.Zero);
					if (flag)
					{
						this.countingSemaphore.Release();
					}
					return !flag;
				}
			}

			// Token: 0x0600AE62 RID: 44642 RVA: 0x0023B413 File Offset: 0x00239613
			public void Dispose()
			{
				this.countingSemaphore.Dispose();
			}

			// Token: 0x040059F0 RID: 23024
			private readonly NamedSemaphore countingSemaphore;
		}
	}
}
