using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000120 RID: 288
	public class DynamicPropertyDictionaryAnnotation
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x000289B4 File Offset: 0x00026BB4
		public DynamicPropertyDictionaryAnnotation(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw Error.ArgumentNull("propertyInfo");
			}
			if (!typeof(IDictionary<string, object>).IsAssignableFrom(propertyInfo.PropertyType))
			{
				throw Error.Argument("propertyInfo", SRResources.InvalidPropertyInfoForDynamicPropertyAnnotation, new object[]
				{
					propertyInfo.PropertyType.Name,
					"IDictionary<string, object>"
				});
			}
			this.PropertyInfo = propertyInfo;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00028A25 File Offset: 0x00026C25
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x00028A2D File Offset: 0x00026C2D
		public PropertyInfo PropertyInfo { get; private set; }
	}
}
