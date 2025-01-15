using System;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007CF RID: 1999
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	public sealed class FeatureCalculatorAttribute : Attribute
	{
		// Token: 0x06002A97 RID: 10903 RVA: 0x000777DB File Offset: 0x000759DB
		public FeatureCalculatorAttribute(string ruleName)
		{
			this.RuleName = ruleName;
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x000777EA File Offset: 0x000759EA
		// (set) Token: 0x06002A99 RID: 10905 RVA: 0x000777F2 File Offset: 0x000759F2
		internal string RuleName { get; set; }

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x000777FB File Offset: 0x000759FB
		// (set) Token: 0x06002A9B RID: 10907 RVA: 0x00077803 File Offset: 0x00075A03
		public CalculationMethod Method { get; set; }

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x0007780C File Offset: 0x00075A0C
		// (set) Token: 0x06002A9D RID: 10909 RVA: 0x00077814 File Offset: 0x00075A14
		public bool SupportsLearningInfo { get; set; }
	}
}
