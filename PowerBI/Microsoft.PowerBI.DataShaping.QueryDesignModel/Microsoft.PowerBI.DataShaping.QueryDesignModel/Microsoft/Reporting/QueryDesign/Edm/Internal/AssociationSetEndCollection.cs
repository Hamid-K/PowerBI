using System;
using System.Collections.Generic;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001DC RID: 476
	public sealed class AssociationSetEndCollection : ReadOnlyKeyedCollection<string, AssociationSetEnd>
	{
		// Token: 0x060016BC RID: 5820 RVA: 0x0003EAFA File Offset: 0x0003CCFA
		internal AssociationSetEndCollection(IEnumerable<AssociationSetEnd> items)
			: base(items)
		{
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0003EB03 File Offset: 0x0003CD03
		protected override string GetKeyForItem(AssociationSetEnd item)
		{
			return item.Name;
		}
	}
}
