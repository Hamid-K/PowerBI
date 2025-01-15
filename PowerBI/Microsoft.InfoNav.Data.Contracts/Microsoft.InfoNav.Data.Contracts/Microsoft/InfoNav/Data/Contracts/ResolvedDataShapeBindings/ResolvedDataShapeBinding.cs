using System;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000AB RID: 171
	public sealed class ResolvedDataShapeBinding
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x0000B721 File Offset: 0x00009921
		public ResolvedDataShapeBinding(ResolvedDataReduction dataReduction)
		{
			this.DataReduction = dataReduction;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000B730 File Offset: 0x00009930
		public ResolvedDataReduction DataReduction { get; }
	}
}
