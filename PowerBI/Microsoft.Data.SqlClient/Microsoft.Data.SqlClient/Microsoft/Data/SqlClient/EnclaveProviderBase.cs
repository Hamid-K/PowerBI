using System;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Threading;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200002A RID: 42
	internal abstract class EnclaveProviderBase : SqlColumnEncryptionEnclaveProvider
	{
		// Token: 0x060006C9 RID: 1737 RVA: 0x0000DE40 File Offset: 0x0000C040
		protected void GetEnclaveSessionHelper(EnclaveSessionParameters enclaveSessionParameters, bool shouldGenerateNonce, bool isRetry, out SqlEnclaveSession sqlEnclaveSession, out long counter, out byte[] customData, out int customDataLength)
		{
			customData = null;
			customDataLength = 0;
			sqlEnclaveSession = EnclaveProviderBase.SessionCache.GetEnclaveSession(enclaveSessionParameters, out counter);
			if (sqlEnclaveSession == null)
			{
				bool flag = false;
				bool flag2 = false;
				string text = EnclaveProviderBase.ThreadRetryCache[Thread.CurrentThread.ManagedThreadId.ToString()] as string;
				if (!string.IsNullOrEmpty(text))
				{
					flag2 = true;
				}
				else if (!isRetry)
				{
					flag = EnclaveProviderBase.sessionLockEvent.WaitOne(EnclaveProviderBase.lockTimeoutInMilliseconds);
					if (flag)
					{
						object obj = EnclaveProviderBase.lockUpdateSessionLock;
						lock (obj)
						{
							EnclaveProviderBase.isSessionLockAcquired = true;
						}
					}
				}
				if (flag || flag2 || isRetry)
				{
					sqlEnclaveSession = EnclaveProviderBase.SessionCache.GetEnclaveSession(enclaveSessionParameters, out counter);
					if (sqlEnclaveSession == null || flag2)
					{
						goto IL_00EC;
					}
					object obj2 = EnclaveProviderBase.lockUpdateSessionLock;
					lock (obj2)
					{
						EnclaveProviderBase.isSessionLockAcquired = false;
						EnclaveProviderBase.sessionLockEvent.Set();
						goto IL_00EC;
					}
				}
				Interlocked.Exchange(ref EnclaveProviderBase.lockTimeoutInMilliseconds, 0);
				IL_00EC:
				if (sqlEnclaveSession == null)
				{
					if (shouldGenerateNonce)
					{
						using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
						{
							byte[] array = new byte[256];
							randomNumberGenerator.GetBytes(array);
							customData = array;
							customDataLength = array.Length;
						}
					}
					if (!flag2)
					{
						text = Thread.CurrentThread.ManagedThreadId.ToString();
					}
					EnclaveProviderBase.ThreadRetryCache.Set(Thread.CurrentThread.ManagedThreadId.ToString(), text, DateTime.UtcNow.AddMinutes(10.0), null);
				}
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000DFF4 File Offset: 0x0000C1F4
		protected void UpdateEnclaveSessionLockStatus(SqlEnclaveSession sqlEnclaveSession)
		{
			if (sqlEnclaveSession != null && EnclaveProviderBase.isSessionLockAcquired)
			{
				object obj = EnclaveProviderBase.lockUpdateSessionLock;
				lock (obj)
				{
					if (EnclaveProviderBase.isSessionLockAcquired)
					{
						EnclaveProviderBase.isSessionLockAcquired = false;
						Interlocked.Exchange(ref EnclaveProviderBase.lockTimeoutInMilliseconds, 15000);
						EnclaveProviderBase.sessionLockEvent.Set();
					}
				}
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000E060 File Offset: 0x0000C260
		protected void InvalidateEnclaveSessionHelper(EnclaveSessionParameters enclaveSessionParameters, SqlEnclaveSession enclaveSessionToInvalidate)
		{
			EnclaveProviderBase.SessionCache.InvalidateSession(enclaveSessionParameters, enclaveSessionToInvalidate);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0000E06E File Offset: 0x0000C26E
		protected SqlEnclaveSession GetEnclaveSessionFromCache(EnclaveSessionParameters enclaveSessionParameters, out long counter)
		{
			return EnclaveProviderBase.SessionCache.GetEnclaveSession(enclaveSessionParameters, out counter);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0000E07C File Offset: 0x0000C27C
		protected SqlEnclaveSession AddEnclaveSessionToCache(EnclaveSessionParameters enclaveSessionParameters, byte[] sharedSecret, long sessionId, out long counter)
		{
			return EnclaveProviderBase.SessionCache.CreateSession(enclaveSessionParameters, sharedSecret, sessionId, out counter);
		}

		// Token: 0x0400008F RID: 143
		private const int NonceSize = 256;

		// Token: 0x04000090 RID: 144
		private const int ThreadRetryCacheTimeoutInMinutes = 10;

		// Token: 0x04000091 RID: 145
		private const int LockTimeoutMaxInMilliseconds = 15000;

		// Token: 0x04000092 RID: 146
		private static readonly EnclaveSessionCache SessionCache = new EnclaveSessionCache();

		// Token: 0x04000093 RID: 147
		private static AutoResetEvent sessionLockEvent = new AutoResetEvent(true);

		// Token: 0x04000094 RID: 148
		private static int lockTimeoutInMilliseconds = 15000;

		// Token: 0x04000095 RID: 149
		private static bool isSessionLockAcquired = false;

		// Token: 0x04000096 RID: 150
		private static readonly object lockUpdateSessionLock = new object();

		// Token: 0x04000097 RID: 151
		protected static readonly MemoryCache ThreadRetryCache = new MemoryCache("ThreadRetryCache", null);
	}
}
