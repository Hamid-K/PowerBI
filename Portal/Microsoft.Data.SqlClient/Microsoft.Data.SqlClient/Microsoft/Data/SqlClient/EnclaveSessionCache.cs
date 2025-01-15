using System;
using System.Runtime.Caching;
using System.Threading;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200002B RID: 43
	internal class EnclaveSessionCache
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		internal SqlEnclaveSession GetEnclaveSession(EnclaveSessionParameters enclaveSessionParameters, out long counter)
		{
			string text = this.GenerateCacheKey(enclaveSessionParameters);
			SqlEnclaveSession sqlEnclaveSession = this.enclaveMemoryCache[text] as SqlEnclaveSession;
			counter = Interlocked.Increment(ref this._counter);
			return sqlEnclaveSession;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0000E11C File Offset: 0x0000C31C
		internal void InvalidateSession(EnclaveSessionParameters enclaveSessionParameters, SqlEnclaveSession enclaveSessionToInvalidate)
		{
			string text = this.GenerateCacheKey(enclaveSessionParameters);
			object obj = this.enclaveCacheLock;
			lock (obj)
			{
				long num;
				SqlEnclaveSession enclaveSession = this.GetEnclaveSession(enclaveSessionParameters, out num);
				if (enclaveSession != null && enclaveSession.SessionId == enclaveSessionToInvalidate.SessionId && !(this.enclaveMemoryCache.Remove(text, null) is SqlEnclaveSession))
				{
					throw new InvalidOperationException(Strings.EnclaveSessionInvalidationFailed);
				}
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		internal SqlEnclaveSession CreateSession(EnclaveSessionParameters enclaveSessionParameters, byte[] sharedSecret, long sessionId, out long counter)
		{
			string text = this.GenerateCacheKey(enclaveSessionParameters);
			SqlEnclaveSession sqlEnclaveSession = null;
			object obj = this.enclaveCacheLock;
			lock (obj)
			{
				sqlEnclaveSession = new SqlEnclaveSession(sharedSecret, sessionId);
				this.enclaveMemoryCache.Add(text, sqlEnclaveSession, DateTime.UtcNow.AddHours((double)EnclaveSessionCache.enclaveCacheTimeOutInHours), null);
				counter = Interlocked.Increment(ref this._counter);
			}
			return sqlEnclaveSession;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0000E224 File Offset: 0x0000C424
		private string GenerateCacheKey(EnclaveSessionParameters enclaveSessionParameters)
		{
			return (enclaveSessionParameters.ServerName + "+" + enclaveSessionParameters.Database + enclaveSessionParameters.AttestationUrl).ToLowerInvariant();
		}

		// Token: 0x04000098 RID: 152
		private readonly MemoryCache enclaveMemoryCache = new MemoryCache("EnclaveMemoryCache", null);

		// Token: 0x04000099 RID: 153
		private readonly object enclaveCacheLock = new object();

		// Token: 0x0400009A RID: 154
		private long _counter;

		// Token: 0x0400009B RID: 155
		private static int enclaveCacheTimeOutInHours = 8;
	}
}
