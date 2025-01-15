using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000087 RID: 135
	[CompatibilityRequirement("1400")]
	public sealed class NamedExpressionAnnotationCollection : NamedMetadataObjectCollection<Annotation, NamedExpression>
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x00046E1A File Offset: 0x0004501A
		internal NamedExpressionAnnotationCollection(NamedExpression parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
