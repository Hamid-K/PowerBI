using System;
using System.Reflection;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000BE RID: 190
	internal static class PropertyInfoExtensionMethods
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		internal static PropertyInfo GetPropertyInfo(this IEdmStructuredTypeReference typeReference, IEdmProperty property, IEdmModel model)
		{
			IEdmStructuredType edmStructuredType = typeReference.StructuredDefinition();
			return PropertyInfoTypeAnnotation.GetPropertyInfoTypeAnnotation(edmStructuredType, model).GetPropertyInfo(edmStructuredType, property, model);
		}
	}
}
