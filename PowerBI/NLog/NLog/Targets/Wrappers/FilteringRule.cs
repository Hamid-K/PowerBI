using System;
using NLog.Conditions;
using NLog.Config;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000069 RID: 105
	[NLogConfigurationItem]
	public class FilteringRule
	{
		// Token: 0x060008C1 RID: 2241 RVA: 0x00016CBD File Offset: 0x00014EBD
		public FilteringRule()
			: this(null, null)
		{
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00016CC7 File Offset: 0x00014EC7
		public FilteringRule(ConditionExpression whenExistsExpression, ConditionExpression filterToApply)
		{
			this.Exists = whenExistsExpression;
			this.Filter = filterToApply;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00016CDD File Offset: 0x00014EDD
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x00016CE5 File Offset: 0x00014EE5
		[RequiredParameter]
		public ConditionExpression Exists { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00016CEE File Offset: 0x00014EEE
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x00016CF6 File Offset: 0x00014EF6
		[RequiredParameter]
		public ConditionExpression Filter { get; set; }
	}
}
