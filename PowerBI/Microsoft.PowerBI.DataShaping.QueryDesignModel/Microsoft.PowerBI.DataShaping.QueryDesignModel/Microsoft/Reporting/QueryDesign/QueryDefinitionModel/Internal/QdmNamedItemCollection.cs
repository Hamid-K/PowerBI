using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000E4 RID: 228
	internal sealed class QdmNamedItemCollection<T> : KeyedCollection<string, T> where T : class, INamedItem
	{
		// Token: 0x06000DDF RID: 3551 RVA: 0x000236C4 File Offset: 0x000218C4
		internal QdmNamedItemCollection(IEnumerable<T> items)
			: base(EdmItem.IdentityComparer)
		{
			foreach (T t in items)
			{
				base.Add(t);
			}
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00023718 File Offset: 0x00021918
		internal bool TryGetItem(string key, out T item)
		{
			if (base.Dictionary == null || key == null)
			{
				item = default(T);
				return false;
			}
			return base.Dictionary.TryGetValue(key, out item);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0002373B File Offset: 0x0002193B
		protected override string GetKeyForItem(T item)
		{
			return item.Name;
		}
	}
}
