using System;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007C1 RID: 1985
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	public class ExternFeatureMappingAttribute : Attribute
	{
		// Token: 0x06002A48 RID: 10824 RVA: 0x00076DD4 File Offset: 0x00074FD4
		public ExternFeatureMappingAttribute(string rule, int parameter)
		{
			this.Rule = rule;
			this.Parameter = parameter;
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002A49 RID: 10825 RVA: 0x00076DEA File Offset: 0x00074FEA
		// (set) Token: 0x06002A4A RID: 10826 RVA: 0x00076DF2 File Offset: 0x00074FF2
		public string Rule { get; set; }

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x00076DFB File Offset: 0x00074FFB
		// (set) Token: 0x06002A4C RID: 10828 RVA: 0x00076E03 File Offset: 0x00075003
		public int Parameter { get; set; }
	}
}
