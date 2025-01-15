using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200002B RID: 43
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class AnalyticsAIMetadataCollection : NamedMetadataObjectCollection<AnalyticsAIMetadata, Model>
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00003B8D File Offset: 0x00001D8D
		internal AnalyticsAIMetadataCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.AnalyticsAIMetadata, parent, comparer, true)
		{
		}
	}
}
