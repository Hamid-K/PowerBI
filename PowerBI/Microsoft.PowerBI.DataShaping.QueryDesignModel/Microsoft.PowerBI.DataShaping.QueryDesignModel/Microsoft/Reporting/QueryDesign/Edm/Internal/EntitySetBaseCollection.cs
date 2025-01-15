using System;
using System.Collections.Generic;
using Microsoft.Data.Metadata.Edm;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200023F RID: 575
	public class EntitySetBaseCollection<T> : ReadOnlyKeyedCollection<string, T> where T : EntitySetBase
	{
		// Token: 0x06001952 RID: 6482 RVA: 0x00044F9D File Offset: 0x0004319D
		internal EntitySetBaseCollection(IEnumerable<T> items)
			: base(items, EdmItem.IdentityComparer)
		{
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00044FAB File Offset: 0x000431AB
		protected override string GetKeyForItem(T item)
		{
			return item.FullName;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00044FBD File Offset: 0x000431BD
		internal T GetItemFromEdmSet(EntitySetBase edmSet)
		{
			ArgumentValidation.CheckNotNull<EntitySetBase>(edmSet, "edmSet");
			return base[EntitySetBase.GetFullName(edmSet)];
		}
	}
}
