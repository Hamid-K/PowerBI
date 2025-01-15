using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000DB RID: 219
	internal sealed class GeneratedGroupKey : GroupKey
	{
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x000233DA File Offset: 0x000215DA
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x000233E2 File Offset: 0x000215E2
		public QueryExpression DataTableExpression { get; private set; }

		// Token: 0x06000DC3 RID: 3523 RVA: 0x000233EB File Offset: 0x000215EB
		internal GeneratedGroupKey(string name, QueryExpression groupKeyExpression, QueryExpression dataTableExpression)
			: base(name, groupKeyExpression)
		{
			this.DataTableExpression = dataTableExpression;
		}
	}
}
