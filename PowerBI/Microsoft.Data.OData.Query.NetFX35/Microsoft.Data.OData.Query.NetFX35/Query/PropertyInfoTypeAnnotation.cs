using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Data.Edm;
using Microsoft.Data.Experimental.OData.Metadata;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000036 RID: 54
	internal sealed class PropertyInfoTypeAnnotation
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00006F68 File Offset: 0x00005168
		internal static PropertyInfoTypeAnnotation GetPropertyInfoTypeAnnotation(IEdmStructuredType structuredType, IEdmModel model)
		{
			PropertyInfoTypeAnnotation propertyInfoTypeAnnotation = model.GetAnnotationValue(structuredType);
			if (propertyInfoTypeAnnotation == null)
			{
				propertyInfoTypeAnnotation = new PropertyInfoTypeAnnotation();
				model.SetAnnotationValue(structuredType, propertyInfoTypeAnnotation);
			}
			return propertyInfoTypeAnnotation;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006F90 File Offset: 0x00005190
		internal PropertyInfo GetPropertyInfo(IEdmStructuredType structuredType, IEdmProperty property, IEdmModel model)
		{
			if (this.propertyInfosDeclaredOnThisType == null)
			{
				this.propertyInfosDeclaredOnThisType = new Dictionary<IEdmProperty, PropertyInfo>(ReferenceEqualityComparer<IEdmProperty>.Instance);
			}
			PropertyInfo property2;
			if (!this.propertyInfosDeclaredOnThisType.TryGetValue(property, ref property2))
			{
				BindingFlags bindingFlags = 20;
				property2 = structuredType.GetInstanceType(model).GetProperty(property.Name, bindingFlags);
				if (property2 == null)
				{
					throw new ODataException(Strings.PropertyInfoTypeAnnotation_CannotFindProperty(structuredType.ODataFullName(), structuredType.GetInstanceType(model), property.Name));
				}
				this.propertyInfosDeclaredOnThisType.Add(property, property2);
			}
			return property2;
		}

		// Token: 0x04000168 RID: 360
		private Dictionary<IEdmProperty, PropertyInfo> propertyInfosDeclaredOnThisType;
	}
}
