using System;
using System.Reflection;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200009F RID: 159
	internal sealed class MetadataPropertyValue
	{
		// Token: 0x06000B14 RID: 2836 RVA: 0x0001B2E1 File Offset: 0x000194E1
		internal MetadataPropertyValue(PropertyInfo propertyInfo, MetadataItem item)
		{
			this._propertyInfo = propertyInfo;
			this._item = item;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0001B2F7 File Offset: 0x000194F7
		internal object GetValue()
		{
			return this._propertyInfo.GetValue(this._item, new object[0]);
		}

		// Token: 0x0400086E RID: 2158
		private PropertyInfo _propertyInfo;

		// Token: 0x0400086F RID: 2159
		private MetadataItem _item;
	}
}
