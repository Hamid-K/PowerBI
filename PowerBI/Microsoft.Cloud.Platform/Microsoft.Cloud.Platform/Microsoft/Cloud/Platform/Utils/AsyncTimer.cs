using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000195 RID: 405
	public static class AsyncTimer
	{
		// Token: 0x06000A64 RID: 2660 RVA: 0x00023DA4 File Offset: 0x00021FA4
		public static IAsyncResult BeginSleep(int dueTime, AsyncCallback callback, object context)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(dueTime, "dueTime");
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "AsyncTimer.BeginSleep (dueTime={0}", new object[] { dueTime });
			return new TimerAsyncResult(dueTime, AsyncTimer.s_timerFactory, callback, context);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00023DDD File Offset: 0x00021FDD
		public static IAsyncResult BeginSleep(TimeSpan sleep, AsyncCallback callback, object context)
		{
			return AsyncTimer.BeginSleep((int)sleep.TotalMilliseconds, callback, context);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00023DEE File Offset: 0x00021FEE
		public static void EndSleep(IAsyncResult ar)
		{
			((TimerAsyncResult)ar).End();
		}

		// Token: 0x04000412 RID: 1042
		private static readonly TimerFactory s_timerFactory = new TimerFactory("AsyncTimer", TimerCreationFlags.Crash);
	}
}
