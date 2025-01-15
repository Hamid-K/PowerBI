using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000054 RID: 84
	public sealed class DataSourceAnnotationCollection : NamedMetadataObjectCollection<Annotation, DataSource>
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x00020230 File Offset: 0x0001E430
		internal DataSourceAnnotationCollection(DataSource parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
