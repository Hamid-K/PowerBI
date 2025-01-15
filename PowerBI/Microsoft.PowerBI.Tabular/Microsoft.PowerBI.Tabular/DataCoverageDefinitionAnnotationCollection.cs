using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000052 RID: 82
	[CompatibilityRequirement("1603")]
	public sealed class DataCoverageDefinitionAnnotationCollection : NamedMetadataObjectCollection<Annotation, DataCoverageDefinition>
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x0001D90D File Offset: 0x0001BB0D
		internal DataCoverageDefinitionAnnotationCollection(DataCoverageDefinition parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
