using System;
using System.Collections.Generic;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200020D RID: 525
	public class EdmMemberCollection<T> : ReadOnlyKeyedCollection<string, T> where T : EdmMember
	{
		// Token: 0x0600188E RID: 6286 RVA: 0x0004340A File Offset: 0x0004160A
		internal EdmMemberCollection(IEnumerable<T> items)
			: base(items, EdmItem.IdentityComparer)
		{
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00043418 File Offset: 0x00041618
		protected override string GetKeyForItem(T item)
		{
			return item.Name;
		}
	}
}
