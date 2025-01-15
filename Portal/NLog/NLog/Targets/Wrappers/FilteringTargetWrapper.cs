using System;
using System.Collections.Generic;
using NLog.Common;
using NLog.Conditions;
using NLog.Config;
using NLog.Filters;
using NLog.Internal;

namespace NLog.Targets.Wrappers
{
	// Token: 0x0200006A RID: 106
	[Target("FilteringWrapper", IsWrapper = true)]
	public class FilteringTargetWrapper : WrapperTargetBase
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x00016CFF File Offset: 0x00014EFF
		public FilteringTargetWrapper()
			: this(null, null)
		{
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00016D09 File Offset: 0x00014F09
		public FilteringTargetWrapper(string name, Target wrappedTarget, ConditionExpression condition)
			: this(wrappedTarget, condition)
		{
			base.Name = name;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00016D1A File Offset: 0x00014F1A
		public FilteringTargetWrapper(Target wrappedTarget, ConditionExpression condition)
		{
			base.WrappedTarget = wrappedTarget;
			this.Condition = condition;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00016D30 File Offset: 0x00014F30
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x00016D48 File Offset: 0x00014F48
		public ConditionExpression Condition
		{
			get
			{
				ConditionBasedFilter conditionBasedFilter = this.Filter as ConditionBasedFilter;
				if (conditionBasedFilter == null)
				{
					return null;
				}
				return conditionBasedFilter.Condition;
			}
			set
			{
				this.Filter = FilteringTargetWrapper.CreateFilter(value);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00016D56 File Offset: 0x00014F56
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x00016D5E File Offset: 0x00014F5E
		[RequiredParameter]
		public Filter Filter { get; set; }

		// Token: 0x060008CE RID: 2254 RVA: 0x00016D67 File Offset: 0x00014F67
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (!base.OptimizeBufferReuse && base.WrappedTarget != null && base.WrappedTarget.OptimizeBufferReuse)
			{
				base.OptimizeBufferReuse = base.GetType() == typeof(FilteringTargetWrapper);
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00016DA7 File Offset: 0x00014FA7
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			if (FilteringTargetWrapper.ShouldLogEvent(logEvent, this.Filter))
			{
				base.WrappedTarget.WriteAsyncLogEvent(logEvent);
				return;
			}
			logEvent.Continuation(null);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00016DD4 File Offset: 0x00014FD4
		protected override void Write(IList<AsyncLogEventInfo> logEvents)
		{
			IList<AsyncLogEventInfo> list = logEvents.Filter(this.Filter, new Func<AsyncLogEventInfo, Filter, bool>(FilteringTargetWrapper.ShouldLogEvent));
			base.WrappedTarget.WriteAsyncLogEvents(list);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00016E08 File Offset: 0x00015008
		private static bool ShouldLogEvent(AsyncLogEventInfo logEvent, Filter filter)
		{
			FilterResult filterResult = filter.GetFilterResult(logEvent.LogEvent);
			if (filterResult != FilterResult.Ignore && filterResult != FilterResult.IgnoreFinal)
			{
				return true;
			}
			logEvent.Continuation(null);
			return false;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00016E3B File Offset: 0x0001503B
		private static ConditionBasedFilter CreateFilter(ConditionExpression value)
		{
			if (value == null)
			{
				return null;
			}
			return new ConditionBasedFilter
			{
				Condition = value,
				DefaultFilterResult = FilterResult.Ignore
			};
		}
	}
}
