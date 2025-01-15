using System;
using System.Collections.Generic;
using System.Threading;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000077 RID: 119
	[Target("SplitGroup", IsCompound = true)]
	public class SplitGroupTarget : CompoundTargetBase
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x00017971 File Offset: 0x00015B71
		public SplitGroupTarget()
			: this(new Target[0])
		{
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001797F File Offset: 0x00015B7F
		public SplitGroupTarget(string name, params Target[] targets)
			: this(targets)
		{
			base.Name = name;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001798F File Offset: 0x00015B8F
		public SplitGroupTarget(params Target[] targets)
			: base(targets)
		{
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00017998 File Offset: 0x00015B98
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			AsyncHelpers.ForEachItemSequentially<Target>(base.Targets, logEvent.Continuation, delegate(Target t, AsyncContinuation cont)
			{
				t.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(cont));
			});
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000179D4 File Offset: 0x00015BD4
		[Obsolete("Instead override Write(IList<AsyncLogEventInfo> logEvents. Marked obsolete on NLog 4.5")]
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			this.Write(logEvents);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x000179E0 File Offset: 0x00015BE0
		protected override void Write(IList<AsyncLogEventInfo> logEvents)
		{
			InternalLogger.Trace<string, int>("SplitGroup(Name={0}): Writing {1} events", base.Name, logEvents.Count);
			for (int i = 0; i < logEvents.Count; i++)
			{
				AsyncLogEventInfo asyncLogEventInfo = logEvents[i];
				logEvents[i] = new AsyncLogEventInfo(asyncLogEventInfo.LogEvent, SplitGroupTarget.CountedWrap(asyncLogEventInfo.Continuation, base.Targets.Count));
			}
			for (int j = 0; j < base.Targets.Count; j++)
			{
				InternalLogger.Trace<string, int, Target>("SplitGroup(Name={0}): Sending {1} events to {2}", base.Name, logEvents.Count, base.Targets[j]);
				IList<AsyncLogEventInfo> list = logEvents;
				if (j < base.Targets.Count - 1)
				{
					AsyncLogEventInfo[] array = new AsyncLogEventInfo[logEvents.Count];
					logEvents.CopyTo(array, 0);
					list = array;
				}
				base.Targets[j].WriteAsyncLogEvents(list);
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00017ABC File Offset: 0x00015CBC
		private static AsyncContinuation CountedWrap(AsyncContinuation originalContinuation, int counter)
		{
			if (counter == 1)
			{
				return originalContinuation;
			}
			List<Exception> exceptions = new List<Exception>();
			return delegate(Exception ex)
			{
				if (ex != null)
				{
					List<Exception> exceptions2 = exceptions;
					lock (exceptions2)
					{
						exceptions.Add(ex);
					}
				}
				int num = Interlocked.Decrement(ref counter);
				if (num == 0)
				{
					Exception combinedException = AsyncHelpers.GetCombinedException(exceptions);
					InternalLogger.Trace<Exception>("Combined exception: {0}", combinedException);
					originalContinuation(combinedException);
					return;
				}
				InternalLogger.Trace<int>("{0} remaining.", num);
			};
		}
	}
}
