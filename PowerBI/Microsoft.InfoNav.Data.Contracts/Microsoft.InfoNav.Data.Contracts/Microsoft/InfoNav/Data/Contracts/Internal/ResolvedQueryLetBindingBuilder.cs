using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000221 RID: 545
	public sealed class ResolvedQueryLetBindingBuilder : ResolvedQueryLetBindingBuilder<object>
	{
		// Token: 0x06000FD6 RID: 4054 RVA: 0x0001E13B File Offset: 0x0001C33B
		public ResolvedQueryLetBindingBuilder(string name)
			: base(null, null, new ResolvedQueryReferenceContext(), name)
		{
		}
	}
}
