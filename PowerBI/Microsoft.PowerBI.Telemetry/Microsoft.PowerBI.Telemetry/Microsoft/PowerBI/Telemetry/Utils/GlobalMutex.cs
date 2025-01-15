using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace Microsoft.PowerBI.Telemetry.Utils
{
	// Token: 0x0200003F RID: 63
	public class GlobalMutex : IDisposable
	{
		// Token: 0x06000196 RID: 406 RVA: 0x00005D54 File Offset: 0x00003F54
		private GlobalMutex(string name)
		{
			MutexSecurity mutexSecurity = null;
			try
			{
				MutexAccessRule mutexAccessRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow);
				mutexSecurity = new MutexSecurity();
				mutexSecurity.AddAccessRule(mutexAccessRule);
			}
			catch (Exception ex)
			{
				if (!(ex is ArgumentOutOfRangeException) && !(ex is ArgumentNullException) && !(ex is ArgumentException) && !(ex is NotSupportedException) && !(ex is IdentityNotMappedException))
				{
					throw;
				}
				this.caughtException = ex;
			}
			if (mutexSecurity != null)
			{
				try
				{
					bool flag;
					this.mutex = new Mutex(false, name, out flag, mutexSecurity);
				}
				catch (Exception ex2)
				{
					if (!(ex2 is IOException) && !(ex2 is UnauthorizedAccessException) && !(ex2 is WaitHandleCannotBeOpenedException) && !(ex2 is ArgumentException))
					{
						throw;
					}
					this.caughtException = ex2;
				}
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005E30 File Offset: 0x00004030
		private void Aquire(int timeOut)
		{
			if (this.mutex != null && this.caughtException == null)
			{
				try
				{
					if (timeOut < 0)
					{
						this.hasHandle = this.mutex.WaitOne(-1, false);
					}
					else
					{
						this.hasHandle = this.mutex.WaitOne(timeOut, false);
					}
					if (!this.hasHandle)
					{
						throw new TimeoutException("Timeout waiting for exclusive access on GlobalMutex");
					}
				}
				catch (AbandonedMutexException)
				{
					this.hasHandle = true;
				}
				catch (Exception ex)
				{
					if (!(ex is ObjectDisposedException) && !(ex is ArgumentOutOfRangeException) && !(ex is InvalidOperationException))
					{
						throw;
					}
					this.caughtException = ex;
				}
			}
			if (this.caughtException != null)
			{
				throw new GlobalMutexException("Failed to aquire the global mutex", this.caughtException);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005EF4 File Offset: 0x000040F4
		public static GlobalMutex CreateAndAquire(string name, int timeOut)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (!name.StartsWith("Global\\", StringComparison.Ordinal))
			{
				name = "Global\\" + name;
			}
			GlobalMutex globalMutex = new GlobalMutex(name);
			globalMutex.Aquire(timeOut);
			return globalMutex;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005F28 File Offset: 0x00004128
		public void Dispose()
		{
			if (this.mutex != null)
			{
				if (this.hasHandle)
				{
					this.mutex.ReleaseMutex();
				}
				this.mutex.Dispose();
			}
		}

		// Token: 0x040000EB RID: 235
		private bool hasHandle;

		// Token: 0x040000EC RID: 236
		private Mutex mutex;

		// Token: 0x040000ED RID: 237
		private Exception caughtException;
	}
}
