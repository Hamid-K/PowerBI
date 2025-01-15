using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200021D RID: 541
	public sealed class ResolvedQueryDefinitionBuilder : ResolvedQueryDefinitionBuilder<object>
	{
		// Token: 0x06000FA9 RID: 4009 RVA: 0x0001DBF1 File Offset: 0x0001BDF1
		public ResolvedQueryDefinitionBuilder(string name = null)
			: base(null, null, new ResolvedQueryReferenceContext(), name)
		{
		}
	}
}
