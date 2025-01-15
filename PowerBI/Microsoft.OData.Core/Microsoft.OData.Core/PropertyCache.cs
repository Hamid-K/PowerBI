using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200001C RID: 28
	internal class PropertyCache
	{
		// Token: 0x06000132 RID: 306 RVA: 0x000039DC File Offset: 0x00001BDC
		public PropertySerializationInfo GetProperty(IEdmModel model, string name, string uniqueName, IEdmStructuredType owningType)
		{
			PropertySerializationInfo propertySerializationInfo;
			if (!this.propertyInfoDictionary.TryGetValue(uniqueName, out propertySerializationInfo))
			{
				propertySerializationInfo = new PropertySerializationInfo(model, name, owningType);
				this.propertyInfoDictionary[uniqueName] = propertySerializationInfo;
			}
			return propertySerializationInfo;
		}

		// Token: 0x0400004D RID: 77
		private readonly Dictionary<string, PropertySerializationInfo> propertyInfoDictionary = new Dictionary<string, PropertySerializationInfo>();
	}
}
