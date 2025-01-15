using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x02000153 RID: 339
	internal static class ConventionsHelpers
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x00030218 File Offset: 0x0002E418
		public static IEnumerable<KeyValuePair<string, object>> GetEntityKey(ResourceContext resourceContext)
		{
			IEdmEntityType edmEntityType = resourceContext.StructuredType as IEdmEntityType;
			if (edmEntityType == null)
			{
				return Enumerable.Empty<KeyValuePair<string, object>>();
			}
			return from k in edmEntityType.Key()
				select new KeyValuePair<string, object>(k.Name, ConventionsHelpers.GetKeyValue(k, resourceContext));
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00030264 File Offset: 0x0002E464
		private static object GetKeyValue(IEdmProperty key, ResourceContext resourceContext)
		{
			object propertyValue = resourceContext.GetPropertyValue(key.Name);
			if (propertyValue == null)
			{
				IEdmTypeReference edmType = resourceContext.EdmObject.GetEdmType();
				throw Error.InvalidOperation(SRResources.KeyValueCannotBeNull, new object[] { key.Name, edmType.Definition });
			}
			return ConventionsHelpers.ConvertValue(propertyValue);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000302B8 File Offset: 0x0002E4B8
		public static object ConvertValue(object value)
		{
			Type type = value.GetType();
			if (TypeHelper.IsEnum(type))
			{
				value = new ODataEnumValue(value.ToString(), type.EdmFullName());
			}
			else
			{
				value = ODataPrimitiveSerializer.ConvertUnsupportedPrimitives(value);
			}
			return value;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000302F4 File Offset: 0x0002E4F4
		public static string GetEntityKeyValue(ResourceContext resourceContext)
		{
			IEdmEntityType edmEntityType = resourceContext.StructuredType as IEdmEntityType;
			if (edmEntityType == null)
			{
				return string.Empty;
			}
			IEnumerable<IEdmProperty> enumerable = edmEntityType.Key();
			if (enumerable.Count<IEdmProperty>() == 1)
			{
				return ConventionsHelpers.GetUriRepresentationForKeyValue(enumerable.First<IEdmProperty>(), resourceContext);
			}
			IEnumerable<string> enumerable2 = enumerable.Select((IEdmProperty key) => string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[]
			{
				key.Name,
				ConventionsHelpers.GetUriRepresentationForKeyValue(key, resourceContext)
			}));
			return string.Join(",", enumerable2);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00030368 File Offset: 0x0002E568
		public static IEnumerable<PropertyInfo> GetProperties(StructuralTypeConfiguration structural, bool includeReadOnly)
		{
			IEnumerable<PropertyInfo> allProperties = ConventionsHelpers.GetAllProperties(structural, includeReadOnly);
			if (structural.BaseTypeInternal != null)
			{
				IEnumerable<PropertyInfo> allProperties2 = ConventionsHelpers.GetAllProperties(structural.BaseTypeInternal, includeReadOnly);
				return allProperties.Except(allProperties2, ConventionsHelpers.PropertyEqualityComparer.Instance);
			}
			return allProperties;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000303A0 File Offset: 0x0002E5A0
		public static IEnumerable<PropertyInfo> GetAllProperties(StructuralTypeConfiguration type, bool includeReadOnly)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return from p in type.ClrType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where p.IsValidStructuralProperty() && !type.IgnoredProperties().Any((PropertyInfo p1) => p1.Name == p.Name) && (includeReadOnly || p.GetSetMethod() != null || TypeHelper.IsCollection(p.PropertyType))
				select p;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000303F8 File Offset: 0x0002E5F8
		public static bool IsValidStructuralProperty(this PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw Error.ArgumentNull("propertyInfo");
			}
			return !propertyInfo.GetIndexParameters().Any<ParameterInfo>() && (propertyInfo.CanRead && propertyInfo.GetGetMethod() != null && propertyInfo.PropertyType.IsValidStructuralPropertyType());
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00030450 File Offset: 0x0002E650
		public static IEnumerable<PropertyInfo> IgnoredProperties(this StructuralTypeConfiguration structuralType)
		{
			if (structuralType == null)
			{
				return Enumerable.Empty<PropertyInfo>();
			}
			EntityTypeConfiguration entityTypeConfiguration = structuralType as EntityTypeConfiguration;
			if (entityTypeConfiguration != null)
			{
				return entityTypeConfiguration.IgnoredProperties.Concat(entityTypeConfiguration.BaseType.IgnoredProperties());
			}
			return structuralType.IgnoredProperties;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00030490 File Offset: 0x0002E690
		public static bool IsValidStructuralPropertyType(this Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			Type type2;
			return !type.IsGenericTypeDefinition() && !type.IsPointer && !(type == typeof(object)) && (!TypeHelper.IsCollection(type, out type2) || !(type2 == typeof(object)));
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x000304F4 File Offset: 0x0002E6F4
		public static string GetUriRepresentationForValue(object value)
		{
			Type type = value.GetType();
			if (TypeHelper.IsEnum(type))
			{
				value = new ODataEnumValue(value.ToString(), type.EdmFullName());
			}
			else
			{
				value = ODataPrimitiveSerializer.ConvertUnsupportedPrimitives(value);
			}
			return ODataUriUtils.ConvertToUriLiteral(value, ODataVersion.V4);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00030534 File Offset: 0x0002E734
		private static string GetUriRepresentationForKeyValue(IEdmProperty key, ResourceContext resourceContext)
		{
			object propertyValue = resourceContext.GetPropertyValue(key.Name);
			if (propertyValue == null)
			{
				IEdmTypeReference edmType = resourceContext.EdmObject.GetEdmType();
				throw Error.InvalidOperation(SRResources.KeyValueCannotBeNull, new object[] { key.Name, edmType.Definition });
			}
			return ConventionsHelpers.GetUriRepresentationForValue(propertyValue);
		}

		// Token: 0x02000313 RID: 787
		private class PropertyEqualityComparer : IEqualityComparer<PropertyInfo>
		{
			// Token: 0x0600141E RID: 5150 RVA: 0x00044290 File Offset: 0x00042490
			public bool Equals(PropertyInfo x, PropertyInfo y)
			{
				return x.Name == y.Name;
			}

			// Token: 0x0600141F RID: 5151 RVA: 0x000442A3 File Offset: 0x000424A3
			public int GetHashCode(PropertyInfo obj)
			{
				return obj.Name.GetHashCode();
			}

			// Token: 0x04000684 RID: 1668
			public static ConventionsHelpers.PropertyEqualityComparer Instance = new ConventionsHelpers.PropertyEqualityComparer();
		}
	}
}
