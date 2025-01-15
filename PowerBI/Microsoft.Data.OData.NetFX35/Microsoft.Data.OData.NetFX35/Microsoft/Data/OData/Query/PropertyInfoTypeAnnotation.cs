using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000BF RID: 191
	internal sealed class PropertyInfoTypeAnnotation
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x0000FD04 File Offset: 0x0000DF04
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

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000FD2C File Offset: 0x0000DF2C
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

		// Token: 0x04000193 RID: 403
		private Dictionary<IEdmProperty, PropertyInfo> propertyInfosDeclaredOnThisType;
	}
}
