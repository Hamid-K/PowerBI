using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000026 RID: 38
	public sealed class ConceptualMParameter
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000027F7 File Offset: 0x000009F7
		public ConceptualMParameter(string name, string expressionType = null)
		{
			this.Name = name;
			this.ExpressionType = expressionType;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000280D File Offset: 0x00000A0D
		public string Name { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002815 File Offset: 0x00000A15
		public string ExpressionType { get; }
	}
}
