using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000274 RID: 628
	public sealed class FilterDefinitionBuilder : FilterDefinitionBuilder<object>
	{
		// Token: 0x0600131D RID: 4893 RVA: 0x00022514 File Offset: 0x00020714
		public FilterDefinitionBuilder(int? version = null)
			: base(null)
		{
			base.WithVersion(version);
		}
	}
}
