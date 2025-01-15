using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000EA RID: 234
	public sealed class ModelGenResult
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x0002766B File Offset: 0x0002586B
		internal ModelGenResult()
		{
			this.m_ruleResults = new List<ModelGenResult.RuleResult>();
			this.__ruleResultsWrapper = new ReadOnlyCollection<ModelGenResult.RuleResult>(this.m_ruleResults);
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0002768F File Offset: 0x0002588F
		public ReadOnlyCollection<ModelGenResult.RuleResult> RuleResults
		{
			get
			{
				return this.__ruleResultsWrapper;
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00027697 File Offset: 0x00025897
		internal void AddResult(string ruleName, RuleProcessResult result)
		{
			this.m_ruleResults.Add(new ModelGenResult.RuleResult(ruleName, result));
		}

		// Token: 0x040004FA RID: 1274
		private readonly List<ModelGenResult.RuleResult> m_ruleResults;

		// Token: 0x040004FB RID: 1275
		private readonly ReadOnlyCollection<ModelGenResult.RuleResult> __ruleResultsWrapper;

		// Token: 0x020001C9 RID: 457
		public struct RuleResult
		{
			// Token: 0x06001162 RID: 4450 RVA: 0x0003670E File Offset: 0x0003490E
			public RuleResult(string ruleName, RuleProcessResult result)
			{
				this.m_ruleName = ruleName;
				this.m_result = result;
			}

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x06001163 RID: 4451 RVA: 0x0003671E File Offset: 0x0003491E
			public string RuleName
			{
				get
				{
					return this.m_ruleName;
				}
			}

			// Token: 0x17000401 RID: 1025
			// (get) Token: 0x06001164 RID: 4452 RVA: 0x00036726 File Offset: 0x00034926
			public RuleProcessResult Result
			{
				get
				{
					return this.m_result;
				}
			}

			// Token: 0x040007DE RID: 2014
			private readonly string m_ruleName;

			// Token: 0x040007DF RID: 2015
			private readonly RuleProcessResult m_result;
		}
	}
}
