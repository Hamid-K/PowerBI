using System;
using System.Collections.Generic;
using Microsoft.Reporting.Common.Internal;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000E5 RID: 229
	internal sealed class ReadOnlyQdmNamedItemCollection<T> : ReadOnlyKeyedCollection<string, T> where T : class, INamedItem
	{
		// Token: 0x06000DE2 RID: 3554 RVA: 0x00023748 File Offset: 0x00021948
		internal ReadOnlyQdmNamedItemCollection(IEnumerable<T> items)
			: base(items, EdmItem.IdentityComparer)
		{
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00023756 File Offset: 0x00021956
		protected override string GetKeyForItem(T item)
		{
			return item.Name;
		}
	}
}
