using System;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Validation.ResolvedCommandValidation
{
	// Token: 0x020000EB RID: 235
	internal interface IResolvedSemanticQueryDataShapeCommandValidator
	{
		// Token: 0x06000815 RID: 2069
		void Validate(ResolvedSemanticQueryDataShapeCommand command, SemanticQueryDataShapeAnnotations annotations);
	}
}
