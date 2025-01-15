using System;
using System.Reflection;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000035 RID: 53
	internal static class PropertyInfoExtensionMethods
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00006F44 File Offset: 0x00005144
		internal static PropertyInfo GetPropertyInfo(this IEdmStructuredTypeReference typeReference, IEdmProperty property, IEdmModel model)
		{
			IEdmStructuredType edmStructuredType = typeReference.StructuredDefinition();
			return PropertyInfoTypeAnnotation.GetPropertyInfoTypeAnnotation(edmStructuredType, model).GetPropertyInfo(edmStructuredType, property, model);
		}
	}
}
