using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C3 RID: 195
	public sealed class MiningParameter
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x0002C557 File Offset: 0x0002A757
		internal MiningParameter(string name, string paramValue)
		{
			this.name = name;
			this.paramValue = paramValue;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002C56D File Offset: 0x0002A76D
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0002C575 File Offset: 0x0002A775
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0002C57D File Offset: 0x0002A77D
		public string Value
		{
			get
			{
				return this.paramValue;
			}
		}

		// Token: 0x04000756 RID: 1878
		private string name;

		// Token: 0x04000757 RID: 1879
		private string paramValue;
	}
}
