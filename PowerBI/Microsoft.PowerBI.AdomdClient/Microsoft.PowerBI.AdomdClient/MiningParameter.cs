using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C3 RID: 195
	public sealed class MiningParameter
	{
		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002C227 File Offset: 0x0002A427
		internal MiningParameter(string name, string paramValue)
		{
			this.name = name;
			this.paramValue = paramValue;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002C23D File Offset: 0x0002A43D
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0002C245 File Offset: 0x0002A445
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0002C24D File Offset: 0x0002A44D
		public string Value
		{
			get
			{
				return this.paramValue;
			}
		}

		// Token: 0x04000749 RID: 1865
		private string name;

		// Token: 0x0400074A RID: 1866
		private string paramValue;
	}
}
