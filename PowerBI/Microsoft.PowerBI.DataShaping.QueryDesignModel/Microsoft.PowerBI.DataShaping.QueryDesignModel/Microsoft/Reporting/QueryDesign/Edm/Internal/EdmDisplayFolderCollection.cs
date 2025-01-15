using System;
using System.Collections.Generic;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001F0 RID: 496
	public class EdmDisplayFolderCollection : ReadOnlyKeyedCollection<string, EdmDisplayFolder>
	{
		// Token: 0x060017A7 RID: 6055 RVA: 0x000413A9 File Offset: 0x0003F5A9
		internal EdmDisplayFolderCollection(IEnumerable<EdmDisplayFolder> items)
			: base(items, EdmItem.IdentityComparer)
		{
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x000413B7 File Offset: 0x0003F5B7
		protected override string GetKeyForItem(EdmDisplayFolder item)
		{
			return item.FullPath;
		}
	}
}
