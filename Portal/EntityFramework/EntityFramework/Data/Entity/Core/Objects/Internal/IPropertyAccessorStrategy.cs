using System;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000449 RID: 1097
	internal interface IPropertyAccessorStrategy
	{
		// Token: 0x06003573 RID: 13683
		object GetNavigationPropertyValue(RelatedEnd relatedEnd);

		// Token: 0x06003574 RID: 13684
		void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x06003575 RID: 13685
		void CollectionAdd(RelatedEnd relatedEnd, object value);

		// Token: 0x06003576 RID: 13686
		bool CollectionRemove(RelatedEnd relatedEnd, object value);

		// Token: 0x06003577 RID: 13687
		object CollectionCreate(RelatedEnd relatedEnd);
	}
}
