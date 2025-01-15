using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200005D RID: 93
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Internal")]
	public sealed class FunctionAnnotationCollection : NamedMetadataObjectCollection<Annotation, Function>
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00026A4B File Offset: 0x00024C4B
		internal FunctionAnnotationCollection(Function parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
