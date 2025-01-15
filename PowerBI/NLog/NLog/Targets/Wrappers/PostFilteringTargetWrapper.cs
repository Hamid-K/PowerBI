using System;
using System.Collections.Generic;
using NLog.Common;
using NLog.Conditions;
using NLog.Config;
using NLog.Internal;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000070 RID: 112
	[Target("PostFilteringWrapper", IsWrapper = true)]
	public class PostFilteringTargetWrapper : WrapperTargetBase
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x0001736B File Offset: 0x0001556B
		public PostFilteringTargetWrapper()
			: this(null)
		{
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00017374 File Offset: 0x00015574
		public PostFilteringTargetWrapper(Target wrappedTarget)
			: this(null, wrappedTarget)
		{
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001737E File Offset: 0x0001557E
		public PostFilteringTargetWrapper(string name, Target wrappedTarget)
		{
			base.Name = name;
			base.WrappedTarget = wrappedTarget;
			this.Rules = new List<FilteringRule>();
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0001739F File Offset: 0x0001559F
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x000173A7 File Offset: 0x000155A7
		public ConditionExpression DefaultFilter { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x000173B0 File Offset: 0x000155B0
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x000173B8 File Offset: 0x000155B8
		[ArrayParameter(typeof(FilteringRule), "when")]
		public IList<FilteringRule> Rules { get; private set; }

		// Token: 0x06000907 RID: 2311 RVA: 0x000173C1 File Offset: 0x000155C1
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (!base.OptimizeBufferReuse && base.WrappedTarget != null && base.WrappedTarget.OptimizeBufferReuse)
			{
				base.OptimizeBufferReuse = base.GetType() == typeof(PostFilteringTargetWrapper);
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00017401 File Offset: 0x00015601
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			this.Write(new AsyncLogEventInfo[] { logEvent });
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00017417 File Offset: 0x00015617
		[Obsolete("Instead override Write(IList<AsyncLogEventInfo> logEvents. Marked obsolete on NLog 4.5")]
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			this.Write(logEvents);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00017420 File Offset: 0x00015620
		protected override void Write(IList<AsyncLogEventInfo> logEvents)
		{
			InternalLogger.Trace<string, int>("PostFilteringWrapper(Name={0}): Running on {1} events", base.Name, logEvents.Count);
			ConditionExpression conditionExpression = this.EvaluateAllRules(logEvents) ?? this.DefaultFilter;
			if (conditionExpression == null)
			{
				base.WrappedTarget.WriteAsyncLogEvents(logEvents);
				return;
			}
			InternalLogger.Trace<string, ConditionExpression>("PostFilteringWrapper(Name={0}): Filter to apply: {1}", base.Name, conditionExpression);
			IList<AsyncLogEventInfo> list = logEvents.Filter(conditionExpression, new Func<AsyncLogEventInfo, ConditionExpression, bool>(PostFilteringTargetWrapper.ApplyFilter));
			InternalLogger.Trace<string, int>("PostFilteringWrapper(Name={0}): After filtering: {1} events.", base.Name, list.Count);
			if (list.Count > 0)
			{
				InternalLogger.Trace<string, Target>("PostFilteringWrapper(Name={0}): Sending to {1}", base.Name, base.WrappedTarget);
				base.WrappedTarget.WriteAsyncLogEvents(list);
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000174CC File Offset: 0x000156CC
		private static bool ApplyFilter(AsyncLogEventInfo logEvent, ConditionExpression resultFilter)
		{
			object obj = resultFilter.Evaluate(logEvent.LogEvent);
			if (PostFilteringTargetWrapper.boxedTrue.Equals(obj))
			{
				return true;
			}
			logEvent.Continuation(null);
			return false;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00017504 File Offset: 0x00015704
		private ConditionExpression EvaluateAllRules(IList<AsyncLogEventInfo> logEvents)
		{
			if (this.Rules.Count == 0)
			{
				return null;
			}
			for (int i = 0; i < logEvents.Count; i++)
			{
				for (int j = 0; j < this.Rules.Count; j++)
				{
					FilteringRule filteringRule = this.Rules[j];
					object obj = filteringRule.Exists.Evaluate(logEvents[i].LogEvent);
					if (PostFilteringTargetWrapper.boxedTrue.Equals(obj))
					{
						InternalLogger.Trace<string, ConditionExpression>("PostFilteringWrapper(Name={0}): Rule matched: {1}", base.Name, filteringRule.Exists);
						return filteringRule.Filter;
					}
				}
			}
			return null;
		}

		// Token: 0x04000206 RID: 518
		private static object boxedTrue = true;
	}
}
