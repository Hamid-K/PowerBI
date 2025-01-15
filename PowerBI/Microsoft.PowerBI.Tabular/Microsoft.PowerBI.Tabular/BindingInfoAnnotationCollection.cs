using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000032 RID: 50
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class BindingInfoAnnotationCollection : NamedMetadataObjectCollection<Annotation, BindingInfo>
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00007B12 File Offset: 0x00005D12
		internal BindingInfoAnnotationCollection(BindingInfo parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
