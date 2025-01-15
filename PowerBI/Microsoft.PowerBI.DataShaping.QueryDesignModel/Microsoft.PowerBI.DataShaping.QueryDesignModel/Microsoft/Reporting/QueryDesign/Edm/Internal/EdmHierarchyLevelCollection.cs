using System;
using System.Collections.Generic;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000205 RID: 517
	public class EdmHierarchyLevelCollection : ReadOnlyKeyedCollection<string, EdmHierarchyLevel>
	{
		// Token: 0x0600183C RID: 6204 RVA: 0x00042BA9 File Offset: 0x00040DA9
		internal EdmHierarchyLevelCollection(IEnumerable<EdmHierarchyLevel> items)
			: base(items, EdmItem.IdentityComparer)
		{
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00042BB7 File Offset: 0x00040DB7
		protected override string GetKeyForItem(EdmHierarchyLevel item)
		{
			return item.Name;
		}
	}
}
