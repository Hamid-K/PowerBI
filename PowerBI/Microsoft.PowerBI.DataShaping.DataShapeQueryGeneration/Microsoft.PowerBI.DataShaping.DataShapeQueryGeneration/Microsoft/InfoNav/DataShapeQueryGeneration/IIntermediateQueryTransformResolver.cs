using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000D4 RID: 212
	internal interface IIntermediateQueryTransformResolver
	{
		// Token: 0x0600077B RID: 1915
		bool TryResolveColumn(ResolvedQueryTransformTableColumn column, out IntermediateQueryTransformTableColumn intermediateColumn);
	}
}
