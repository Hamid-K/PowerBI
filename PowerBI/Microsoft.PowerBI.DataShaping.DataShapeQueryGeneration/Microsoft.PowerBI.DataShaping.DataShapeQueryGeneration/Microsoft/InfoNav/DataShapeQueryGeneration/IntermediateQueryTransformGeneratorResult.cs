using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000D8 RID: 216
	internal sealed class IntermediateQueryTransformGeneratorResult
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x0001CB19 File Offset: 0x0001AD19
		internal IntermediateQueryTransformGeneratorResult(Dictionary<ResolvedSemanticQueryDataShape, IntermediateQueryTransformContext> mapping)
		{
			this._mapping = mapping;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001CB28 File Offset: 0x0001AD28
		internal IntermediateQueryTransformContext GetTransformContext(ResolvedSemanticQueryDataShape rsqds)
		{
			IntermediateQueryTransformContext intermediateQueryTransformContext;
			if (this._mapping.TryGetValue(rsqds, out intermediateQueryTransformContext))
			{
				return intermediateQueryTransformContext;
			}
			return IntermediateQueryTransformContext.Empty;
		}

		// Token: 0x040003F4 RID: 1012
		internal static readonly IntermediateQueryTransformGeneratorResult Empty = new IntermediateQueryTransformGeneratorResult(new Dictionary<ResolvedSemanticQueryDataShape, IntermediateQueryTransformContext>());

		// Token: 0x040003F5 RID: 1013
		private readonly Dictionary<ResolvedSemanticQueryDataShape, IntermediateQueryTransformContext> _mapping;
	}
}
