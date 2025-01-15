using System;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000041 RID: 65
	internal class UpgradeMutex : IDisposable
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x0000A0C4 File Offset: 0x000082C4
		public UpgradeMutex()
		{
			string text = string.Format("{0}_Upgrade_Lock", Globals.Configuration.InstanceName);
			try
			{
				MutexAccessRule mutexAccessRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow);
				MutexSecurity mutexSecurity = new MutexSecurity();
				mutexSecurity.AddAccessRule(mutexAccessRule);
				bool flag = false;
				this._mutex = new Mutex(false, text, out flag, mutexSecurity);
				bool flag2 = false;
				try
				{
					flag2 = this._mutex.WaitOne(TimeSpan.FromMinutes(10.0));
				}
				catch (AbandonedMutexException)
				{
				}
				if (!flag2)
				{
					throw new TimeoutException("Timeout trying to acquire the catalog upgrade mutex.");
				}
			}
			catch (Exception ex)
			{
				Trace.TraceError(string.Format("Exception acquiring the catalog upgrade mutex. Name: {0}, Exception:{1}", text, ex));
				throw;
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A184 File Offset: 0x00008384
		public void Dispose()
		{
			this._mutex.ReleaseMutex();
			this._mutex = null;
		}

		// Token: 0x0400018C RID: 396
		private const string MutexNameFormat = "{0}_Upgrade_Lock";

		// Token: 0x0400018D RID: 397
		private const int MinutesBeforeTimeout = 10;

		// Token: 0x0400018E RID: 398
		private Mutex _mutex;
	}
}
