using System;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000AD RID: 173
	public sealed class ResolvedScopedDataReduction
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x0000B75E File Offset: 0x0000995E
		public ResolvedScopedDataReduction(ResolvedDataReductionScope scope, ResolvedDataReductionLimit algorithm)
		{
			this.Scope = scope;
			this.Algorithm = algorithm;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000B774 File Offset: 0x00009974
		public ResolvedDataReductionScope Scope { get; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000B77C File Offset: 0x0000997C
		public ResolvedDataReductionLimit Algorithm { get; }
	}
}
