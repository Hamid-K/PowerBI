using System;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Validation.ResolvedCommandValidation
{
	// Token: 0x020000EE RID: 238
	internal interface IVisualShapeValidator
	{
		// Token: 0x06000818 RID: 2072
		void Validate(ResolvedSemanticQueryDataShapeCommand command, SemanticQueryDataShapeAnnotations annotations);
	}
}
