using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000AE RID: 174
	[CompatibilityRequirement("1450")]
	public sealed class RefreshPolicyAnnotationCollection : NamedMetadataObjectCollection<Annotation, RefreshPolicy>
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x00058637 File Offset: 0x00056837
		internal RefreshPolicyAnnotationCollection(RefreshPolicy parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
