using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000011 RID: 17
	internal sealed class MultithreadedConnectionManager : ConnectionManager
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00006058 File Offset: 0x00004258
		public MultithreadedConnectionManager(ConnectionTransactionType transactionType, IsolationLevel defaultIsolationLevel)
			: base(transactionType, defaultIsolationLevel)
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006074 File Offset: 0x00004274
		public MultithreadedConnectionManager()
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000608E File Offset: 0x0000428E
		protected override void PerformVerificationChecks()
		{
			this.ValidateThreadIsLocked();
			base.PerformVerificationChecks();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000609C File Offset: 0x0000429C
		public override IDisposable EnterThreadSafeContext()
		{
			return new MultithreadedConnectionManager.ConnectionLock(this);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000060A4 File Offset: 0x000042A4
		private void AcquireLock()
		{
			if (this._lockState == MultithreadedConnectionManager.LockState.Valid)
			{
				int lockTimeoutSeconds = MultithreadedConnectionManager.LockTimeoutSeconds;
				TimeSpan timeSpan = TimeSpan.FromSeconds((double)lockTimeoutSeconds);
				if (lockTimeoutSeconds <= 0)
				{
					timeSpan = TimeSpan.FromMilliseconds(-1.0);
				}
				else
				{
					timeSpan.Add(TimeSpan.FromSeconds(30.0));
				}
				bool flag = false;
				Stopwatch stopwatch = Stopwatch.StartNew();
				while (!flag)
				{
					MultithreadedConnectionManager.LockState lockState = this._lockState;
					int num = lockState - MultithreadedConnectionManager.LockState.TimedOut;
					if (flag = Monitor.TryEnter(this._connectionSync, timeSpan))
					{
						this._lockDepth++;
						this._lockedThreadId = Thread.CurrentThread.ManagedThreadId;
					}
					else if (stopwatch.Elapsed > timeSpan)
					{
						this._lockState = MultithreadedConnectionManager.LockState.TimedOut;
					}
				}
			}
			if (this._lockState != MultithreadedConnectionManager.LockState.Valid)
			{
				throw new ReportServerStorageException(null, "Unable to acquire connection monitor, reason=" + this._lockState.ToString());
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000617C File Offset: 0x0000437C
		private void ReleaseLock()
		{
			this.ValidateThreadIsLocked();
			this._lockDepth--;
			Monitor.Exit(this._connectionSync);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000061A0 File Offset: 0x000043A0
		private void ValidateThreadIsLocked()
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			RSTrace.CatalogTrace.Assert(this._lockDepth > 0, "_lockDepth");
			RSTrace.CatalogTrace.Assert(managedThreadId == this._lockedThreadId, "currentThreadId");
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000061E8 File Offset: 0x000043E8
		private static int LockTimeoutSeconds
		{
			get
			{
				if (MultithreadedConnectionManager._lockTimeoutSeconds < 0)
				{
					int num = Math.Max(ConnectionManager.SqlCleanupTimeoutSeconds, ConnectionManager.SqlCommandTimeoutSeconds);
					num = Math.Max(num, CachedSystemProperties.SessionAccessTimeout);
					Interlocked.Exchange(ref MultithreadedConnectionManager._lockTimeoutSeconds, num);
				}
				return MultithreadedConnectionManager._lockTimeoutSeconds;
			}
		}

		// Token: 0x04000093 RID: 147
		private static int _lockTimeoutSeconds = int.MinValue;

		// Token: 0x04000094 RID: 148
		private int _lockedThreadId = -1;

		// Token: 0x04000095 RID: 149
		private int _lockDepth;

		// Token: 0x04000096 RID: 150
		private MultithreadedConnectionManager.LockState _lockState;

		// Token: 0x04000097 RID: 151
		private readonly object _connectionSync = new object();

		// Token: 0x0200004C RID: 76
		private enum LockState
		{
			// Token: 0x040001E6 RID: 486
			Valid,
			// Token: 0x040001E7 RID: 487
			TimedOut,
			// Token: 0x040001E8 RID: 488
			FinalizationOrphan
		}

		// Token: 0x0200004D RID: 77
		private sealed class ConnectionLock : IDisposable
		{
			// Token: 0x06000288 RID: 648 RVA: 0x0000AF5F File Offset: 0x0000915F
			public ConnectionLock(MultithreadedConnectionManager connectionManager)
			{
				this._connection = connectionManager;
				this._connection.AcquireLock();
				this._hasLock = true;
			}

			// Token: 0x06000289 RID: 649 RVA: 0x0000AF80 File Offset: 0x00009180
			~ConnectionLock()
			{
				this.Dispose(false);
			}

			// Token: 0x0600028A RID: 650 RVA: 0x0000AFB0 File Offset: 0x000091B0
			public void Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x0600028B RID: 651 RVA: 0x0000AFBC File Offset: 0x000091BC
			private void Dispose(bool isDisposing)
			{
				try
				{
					if (isDisposing)
					{
						GC.SuppressFinalize(this);
					}
					else
					{
						this._connection._lockState = MultithreadedConnectionManager.LockState.FinalizationOrphan;
					}
				}
				finally
				{
					if (this._hasLock && isDisposing)
					{
						this._connection.ReleaseLock();
						this._hasLock = false;
					}
				}
			}

			// Token: 0x040001E9 RID: 489
			private readonly MultithreadedConnectionManager _connection;

			// Token: 0x040001EA RID: 490
			private bool _hasLock;
		}
	}
}
