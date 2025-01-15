using System;
using NLog.Conditions;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x02000170 RID: 368
	[Filter("when")]
	public class ConditionBasedFilter : Filter
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0002CC0C File Offset: 0x0002AE0C
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x0002CC14 File Offset: 0x0002AE14
		[RequiredParameter]
		public ConditionExpression Condition { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0002CC1D File Offset: 0x0002AE1D
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x0002CC25 File Offset: 0x0002AE25
		internal FilterResult DefaultFilterResult { get; set; }

		// Token: 0x0600113D RID: 4413 RVA: 0x0002CC30 File Offset: 0x0002AE30
		protected override FilterResult Check(LogEventInfo logEvent)
		{
			object obj = this.Condition.Evaluate(logEvent);
			if (ConditionBasedFilter.boxedTrue.Equals(obj))
			{
				return base.Action;
			}
			return this.DefaultFilterResult;
		}

		// Token: 0x0400049D RID: 1181
		private static readonly object boxedTrue = true;
	}
}
