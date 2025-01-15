using System;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Validation.ResolvedCommandValidation
{
	// Token: 0x020000EC RID: 236
	internal interface ISuppressedJoinPredicatesByNameValidator
	{
		// Token: 0x06000816 RID: 2070
		void Validate(ResolvedSemanticQueryDataShapeCommand command, SemanticQueryDataShapeAnnotations annotations);
	}
}
