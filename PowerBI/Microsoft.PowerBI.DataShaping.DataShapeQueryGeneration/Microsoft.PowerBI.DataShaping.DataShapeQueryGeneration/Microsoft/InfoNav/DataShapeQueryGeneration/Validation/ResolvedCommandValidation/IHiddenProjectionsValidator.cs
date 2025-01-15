using System;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Validation.ResolvedCommandValidation
{
	// Token: 0x020000EA RID: 234
	internal interface IHiddenProjectionsValidator
	{
		// Token: 0x06000814 RID: 2068
		void Validate(ResolvedSemanticQueryDataShapeCommand command, SemanticQueryDataShapeAnnotations annotations);
	}
}
