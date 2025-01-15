using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000C7 RID: 199
	internal class PropertyCache
	{
		// Token: 0x0600079A RID: 1946 RVA: 0x00015818 File Offset: 0x00013A18
		public PropertySerializationInfo GetProperty(string name, string uniqueName, IEdmStructuredType owningType)
		{
			PropertySerializationInfo propertySerializationInfo;
			if (!this.propertyInfoDictionary.TryGetValue(uniqueName, ref propertySerializationInfo))
			{
				WriterValidationUtils.ValidatePropertyName(name);
				propertySerializationInfo = new PropertySerializationInfo(name, owningType);
				this.propertyInfoDictionary[uniqueName] = propertySerializationInfo;
			}
			return propertySerializationInfo;
		}

		// Token: 0x04000314 RID: 788
		private readonly Dictionary<string, PropertySerializationInfo> propertyInfoDictionary = new Dictionary<string, PropertySerializationInfo>();
	}
}
