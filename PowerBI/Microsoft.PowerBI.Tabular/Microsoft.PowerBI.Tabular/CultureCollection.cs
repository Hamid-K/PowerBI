using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200004D RID: 77
	public sealed class CultureCollection : NamedMetadataObjectCollection<Culture, Model>
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0001C15A File Offset: 0x0001A35A
		internal CultureCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Culture, parent, comparer, true)
		{
		}
	}
}
