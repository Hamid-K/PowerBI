using System;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners
{
	// Token: 0x02000748 RID: 1864
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	public sealed class RuleLearnerAttribute : Attribute
	{
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x000717FC File Offset: 0x0006F9FC
		public string RuleName { get; }

		// Token: 0x060027F6 RID: 10230 RVA: 0x00071804 File Offset: 0x0006FA04
		public RuleLearnerAttribute(string ruleName)
		{
			this.RuleName = ruleName;
		}
	}
}
