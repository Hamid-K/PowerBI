using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000090 RID: 144
	internal enum ODataQueryFilterProcessingErrorCode
	{
		// Token: 0x0400030C RID: 780
		BinaryNodeLeftMustBeConvert,
		// Token: 0x0400030D RID: 781
		BinaryNodeOperatorMustBeEquality,
		// Token: 0x0400030E RID: 782
		BinaryNodeRightMustBeLiteral,
		// Token: 0x0400030F RID: 783
		ConvertNodeSourceMustBeProperty,
		// Token: 0x04000310 RID: 784
		ConvertNodeTypeMustBeString,
		// Token: 0x04000311 RID: 785
		EntityCouldNotBeResolved,
		// Token: 0x04000312 RID: 786
		EntityIsPresentMoreThanOnceInQuerySources,
		// Token: 0x04000313 RID: 787
		FilterClauseRootMustBeBinaryNode,
		// Token: 0x04000314 RID: 788
		LiteralNodeTypeMustBeString,
		// Token: 0x04000315 RID: 789
		PropertyCouldNotBeResolved,
		// Token: 0x04000316 RID: 790
		PropertyNodeMustHaveParentSource,
		// Token: 0x04000317 RID: 791
		PropertyNodeRootMustBeIterator,
		// Token: 0x04000318 RID: 792
		PropertyNodeRootMustBeRangeVariableReference,
		// Token: 0x04000319 RID: 793
		PropertyNodeSchemaMustHaveParentSource,
		// Token: 0x0400031A RID: 794
		PropertyNodeSourceMustBeProperty,
		// Token: 0x0400031B RID: 795
		PropertyNodeSourceMustHaveParentSource,
		// Token: 0x0400031C RID: 796
		QueryFilterCouldNotBeParsed,
		// Token: 0x0400031D RID: 797
		UnsupportedNodeType
	}
}
