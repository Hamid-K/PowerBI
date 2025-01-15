using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000038 RID: 56
	[CompatibilityRequirement("1470")]
	public sealed class CalculationGroupAnnotationCollection : NamedMetadataObjectCollection<Annotation, CalculationGroup>
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00009D37 File Offset: 0x00007F37
		internal CalculationGroupAnnotationCollection(CalculationGroup parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
