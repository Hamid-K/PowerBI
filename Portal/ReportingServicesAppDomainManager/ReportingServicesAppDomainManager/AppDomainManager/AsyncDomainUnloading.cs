using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.HostingInterfaces;

namespace Microsoft.ReportingServices.AppDomainManager
{
	// Token: 0x0200000B RID: 11
	internal class AsyncDomainUnloading
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002050 File Offset: 0x00000250
		internal AsyncDomainUnloading(AppDomain appDomain, IRsService rsService)
		{
			this.m_appDomain = appDomain;
			this.m_rsService = rsService;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002068 File Offset: 0x00000268
		internal void Start()
		{
			Thread thread = new Thread(new ThreadStart(this.DomainUnloadingFunction));
			thread.Start();
			int num = 1000 * Globals.Configuration.DBQueryTimeout;
			bool flag = false;
			while (!flag && num > 0)
			{
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, "AsyncDomainUnloading::Start - waiting for the unloading thread to finish unloading AppDomain '{0}'; Mark the WindowsService (worker) AppDomain as active.", new object[] { this.m_appDomain.Id });
				}
				this.m_rsService.MarkProcessAsActive();
				int num2 = Math.Min(num, 5000);
				num -= num2;
				RSTrace.AppDomainManagerTracer.Assert(num2 >= 0, "AsyncDomainUnloading.Start: Negative wait time");
				flag = thread.Join(num2);
			}
			if (!flag)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "AppDomain:{0} unloading timed out.", this.m_appDomain.Id);
				Console.WriteLine(text);
				if (RSTrace.AppDomainManagerTracer.TraceWarning)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, text);
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002160 File Offset: 0x00000360
		private static void Add(int key)
		{
			if (12L < (long)AsyncDomainUnloading.m_pendingCalls.Count + AsyncDomainUnloading.m_failedUnloads)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "Generating a dump and terminating the process because the number of pending ({0}) and failed ({1}) appdomain unloads exceeded the limit.", AsyncDomainUnloading.m_pendingCalls.Count, AsyncDomainUnloading.m_failedUnloads);
				AsyncDomainUnloading.DumpAndTerminate(null, text);
			}
			DateTime now = DateTime.Now;
			if (DateTime.MinValue != AsyncDomainUnloading.m_firstUnload)
			{
				DateTime dateTime = AsyncDomainUnloading.m_firstUnload.Add(new TimeSpan(0, 12, 0));
				if (DateTime.Now > dateTime)
				{
					AsyncDomainUnloading.DumpAndTerminate(null, "Generating a dump and terminating the process because the pending appdomain unload timed out.");
				}
			}
			Dictionary<int, DateTime> pendingCalls = AsyncDomainUnloading.m_pendingCalls;
			lock (pendingCalls)
			{
				try
				{
					AsyncDomainUnloading.m_pendingCalls.Add(key, now);
					if (DateTime.MinValue == AsyncDomainUnloading.m_firstUnload)
					{
						AsyncDomainUnloading.m_firstUnload = now;
					}
				}
				catch (ArgumentException)
				{
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002258 File Offset: 0x00000458
		private static void Remove(int key)
		{
			Dictionary<int, DateTime> pendingCalls = AsyncDomainUnloading.m_pendingCalls;
			lock (pendingCalls)
			{
				DateTime dateTime = AsyncDomainUnloading.m_pendingCalls[key];
				AsyncDomainUnloading.m_pendingCalls.Remove(key);
				if (AsyncDomainUnloading.m_pendingCalls.Count == 0)
				{
					AsyncDomainUnloading.m_firstUnload = DateTime.MinValue;
				}
				else
				{
					AsyncDomainUnloading.m_firstUnload = dateTime;
					foreach (DateTime dateTime2 in AsyncDomainUnloading.m_pendingCalls.Values)
					{
						if (AsyncDomainUnloading.m_firstUnload > dateTime2)
						{
							AsyncDomainUnloading.m_firstUnload = dateTime2;
						}
					}
				}
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000231C File Offset: 0x0000051C
		private static void DumpAndTerminate(Exception optinalException, string traceMessage)
		{
			object dumperLock = AsyncDomainUnloading.m_dumperLock;
			lock (dumperLock)
			{
				if (RSTrace.AppDomainManagerTracer.TraceError)
				{
					Console.WriteLine(traceMessage);
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, traceMessage);
				}
				Dumper.Current.DumpHere(optinalException);
				Process.GetCurrentProcess().Kill();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002388 File Offset: 0x00000588
		private void DomainUnloadingFunction()
		{
			if (this.m_appDomain == null)
			{
				return;
			}
			string text = string.Empty;
			int num = 0;
			try
			{
				if (RSTrace.AppDomainManagerTracer.TraceInfo)
				{
					text = string.Format(CultureInfo.InvariantCulture, "AppDomain:{0} {1} pending unload(s)", this.m_appDomain.Id, AsyncDomainUnloading.m_pendingCalls.Count);
					Console.WriteLine(text);
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, text);
					text = string.Format(CultureInfo.InvariantCulture, "AppDomain: calling AppDomain.Unload(AppDomain {0})", this.m_appDomain.Id);
				}
				num = this.m_appDomain.Id;
				AsyncDomainUnloading.Add(num);
			}
			catch (AppDomainUnloadedException)
			{
			}
			finally
			{
				try
				{
					if (RSTrace.AppDomainManagerTracer.TraceInfo && text != null && text.Trim().Length > 0)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Info, text);
					}
					AppDomain.Unload(this.m_appDomain);
				}
				catch (AppDomainUnloadedException)
				{
				}
				catch (Exception ex)
				{
					bool flag = false;
					try
					{
						text = string.Format(CultureInfo.InvariantCulture, "AppDomain:{0} {1} failed to unload. Error: {2}.", this.m_appDomain.Id, this.m_appDomain.FriendlyName, ex.ToString());
						if (RSTrace.AppDomainManagerTracer.TraceError)
						{
							RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Error, text);
						}
					}
					catch (AppDomainUnloadedException)
					{
						flag = true;
					}
					if (!flag)
					{
						Interlocked.Increment(ref AsyncDomainUnloading.m_failedUnloads);
					}
				}
				finally
				{
					if (num != 0)
					{
						AsyncDomainUnloading.Remove(num);
					}
				}
			}
		}

		// Token: 0x0400003C RID: 60
		private AppDomain m_appDomain;

		// Token: 0x0400003D RID: 61
		private IRsService m_rsService;

		// Token: 0x0400003E RID: 62
		private static Dictionary<int, DateTime> m_pendingCalls = new Dictionary<int, DateTime>();

		// Token: 0x0400003F RID: 63
		private static long m_failedUnloads = 0L;

		// Token: 0x04000040 RID: 64
		private static DateTime m_firstUnload = DateTime.MinValue;

		// Token: 0x04000041 RID: 65
		private static object m_dumperLock = new object();

		// Token: 0x04000042 RID: 66
		private const int MAX_PENDING_UNLOADS = 12;

		// Token: 0x04000043 RID: 67
		private const int APPDOMAIN_UNLOAD_TIMEOUT_MIN = 12;
	}
}
