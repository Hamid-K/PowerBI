using System;
using System.Collections.Generic;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000203 RID: 515
	public class EdmHierarchyCollection : ReadOnlyKeyedCollection<string, EdmHierarchy>
	{
		// Token: 0x06001833 RID: 6195 RVA: 0x00042A7E File Offset: 0x00040C7E
		internal EdmHierarchyCollection(IEnumerable<EdmHierarchy> items)
			: base(items, EdmItem.IdentityComparer)
		{
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00042A8C File Offset: 0x00040C8C
		protected override string GetKeyForItem(EdmHierarchy item)
		{
			return item.Name;
		}
	}
}
