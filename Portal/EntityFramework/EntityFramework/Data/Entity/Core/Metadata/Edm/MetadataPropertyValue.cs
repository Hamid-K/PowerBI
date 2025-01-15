using System;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004DF RID: 1247
	internal sealed class MetadataPropertyValue
	{
		// Token: 0x06003DE9 RID: 15849 RVA: 0x000CD877 File Offset: 0x000CBA77
		internal MetadataPropertyValue(PropertyInfo propertyInfo, MetadataItem item)
		{
			this._propertyInfo = propertyInfo;
			this._item = item;
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x000CD88D File Offset: 0x000CBA8D
		internal object GetValue()
		{
			return this._propertyInfo.GetValue(this._item, new object[0]);
		}

		// Token: 0x04001516 RID: 5398
		private readonly PropertyInfo _propertyInfo;

		// Token: 0x04001517 RID: 5399
		private readonly MetadataItem _item;
	}
}
