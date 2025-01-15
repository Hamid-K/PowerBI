using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000093 RID: 147
	public sealed class PerspectiveCollection : NamedMetadataObjectCollection<Perspective, Model>
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x0004F052 File Offset: 0x0004D252
		internal PerspectiveCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Perspective, parent, comparer, true)
		{
		}
	}
}
