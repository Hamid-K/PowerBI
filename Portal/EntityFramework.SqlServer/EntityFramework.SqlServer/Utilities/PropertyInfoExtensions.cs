using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000026 RID: 38
	internal static class PropertyInfoExtensions
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0000FD44 File Offset: 0x0000DF44
		public static bool IsSameAs(this PropertyInfo propertyInfo, PropertyInfo otherPropertyInfo)
		{
			return propertyInfo == otherPropertyInfo || (propertyInfo.Name == otherPropertyInfo.Name && (propertyInfo.DeclaringType == otherPropertyInfo.DeclaringType || propertyInfo.DeclaringType.IsSubclassOf(otherPropertyInfo.DeclaringType) || otherPropertyInfo.DeclaringType.IsSubclassOf(propertyInfo.DeclaringType) || propertyInfo.DeclaringType.GetInterfaces().Contains(otherPropertyInfo.DeclaringType) || otherPropertyInfo.DeclaringType.GetInterfaces().Contains(propertyInfo.DeclaringType)));
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000FDDA File Offset: 0x0000DFDA
		public static bool ContainsSame(this IEnumerable<PropertyInfo> enumerable, PropertyInfo propertyInfo)
		{
			return enumerable.Any(new Func<PropertyInfo, bool>(propertyInfo.IsSameAs));
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000FDEE File Offset: 0x0000DFEE
		public static bool IsValidStructuralProperty(this PropertyInfo propertyInfo)
		{
			return propertyInfo.IsValidInterfaceStructuralProperty() && !propertyInfo.Getter().IsAbstract;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000FE08 File Offset: 0x0000E008
		public static bool IsValidInterfaceStructuralProperty(this PropertyInfo propertyInfo)
		{
			return propertyInfo.CanRead && (propertyInfo.CanWriteExtended() || propertyInfo.PropertyType.IsCollection()) && propertyInfo.GetIndexParameters().Length == 0 && propertyInfo.PropertyType.IsValidStructuralPropertyType();
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000FE3D File Offset: 0x0000E03D
		public static bool IsValidEdmScalarProperty(this PropertyInfo propertyInfo)
		{
			return propertyInfo.IsValidInterfaceStructuralProperty() && propertyInfo.PropertyType.IsValidEdmScalarType();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000FE54 File Offset: 0x0000E054
		public static bool IsValidEdmNavigationProperty(this PropertyInfo propertyInfo)
		{
			Type type;
			return propertyInfo.IsValidInterfaceStructuralProperty() && ((propertyInfo.PropertyType.IsCollection(out type) && type.IsValidStructuralType()) || propertyInfo.PropertyType.IsValidStructuralType());
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000FE90 File Offset: 0x0000E090
		public static EdmProperty AsEdmPrimitiveProperty(this PropertyInfo propertyInfo)
		{
			Type propertyType = propertyInfo.PropertyType;
			bool flag = propertyType.TryUnwrapNullableType(out propertyType) || !propertyType.IsValueType();
			PrimitiveType primitiveType;
			if (propertyType.IsPrimitiveType(out primitiveType))
			{
				EdmProperty edmProperty = EdmProperty.CreatePrimitive(propertyInfo.Name, primitiveType);
				edmProperty.Nullable = flag;
				return edmProperty;
			}
			return null;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000FEDC File Offset: 0x0000E0DC
		public static bool CanWriteExtended(this PropertyInfo propertyInfo)
		{
			if (propertyInfo.CanWrite)
			{
				return true;
			}
			PropertyInfo declaredProperty = PropertyInfoExtensions.GetDeclaredProperty(propertyInfo);
			return declaredProperty != null && declaredProperty.CanWrite;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000FF0B File Offset: 0x0000E10B
		public static PropertyInfo GetPropertyInfoForSet(this PropertyInfo propertyInfo)
		{
			PropertyInfo propertyInfo2;
			if (!propertyInfo.CanWrite)
			{
				if ((propertyInfo2 = PropertyInfoExtensions.GetDeclaredProperty(propertyInfo)) == null)
				{
					return propertyInfo;
				}
			}
			else
			{
				propertyInfo2 = propertyInfo;
			}
			return propertyInfo2;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000FF24 File Offset: 0x0000E124
		private static PropertyInfo GetDeclaredProperty(PropertyInfo propertyInfo)
		{
			if (!(propertyInfo.DeclaringType == propertyInfo.ReflectedType))
			{
				return propertyInfo.DeclaringType.GetInstanceProperties().SingleOrDefault((PropertyInfo p) => p.Name == propertyInfo.Name && p.DeclaringType == propertyInfo.DeclaringType && !p.GetIndexParameters().Any<ParameterInfo>() && p.PropertyType == propertyInfo.PropertyType);
			}
			return propertyInfo;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000FF84 File Offset: 0x0000E184
		public static IEnumerable<PropertyInfo> GetPropertiesInHierarchy(this PropertyInfo property)
		{
			List<PropertyInfo> list = new List<PropertyInfo> { property };
			PropertyInfoExtensions.CollectProperties(property, list);
			return list.Distinct<PropertyInfo>();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000FFAB File Offset: 0x0000E1AB
		private static void CollectProperties(PropertyInfo property, IList<PropertyInfo> collection)
		{
			PropertyInfoExtensions.FindNextProperty(property, collection, true);
			PropertyInfoExtensions.FindNextProperty(property, collection, false);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
		private static void FindNextProperty(PropertyInfo property, IList<PropertyInfo> collection, bool getter)
		{
			MethodInfo methodInfo = (getter ? property.Getter() : property.Setter());
			if (methodInfo != null)
			{
				Type type = methodInfo.DeclaringType.BaseType();
				if (type != null && type != typeof(object))
				{
					MethodInfo baseMethod = methodInfo.GetBaseDefinition();
					PropertyInfo propertyInfo = (from p in type.GetInstanceProperties()
						let candidateMethod = getter ? p.Getter() : p.Setter()
						where candidateMethod != null && candidateMethod.GetBaseDefinition() == baseMethod
						select p).FirstOrDefault<PropertyInfo>();
					if (propertyInfo != null)
					{
						collection.Add(propertyInfo);
						PropertyInfoExtensions.CollectProperties(propertyInfo, collection);
					}
				}
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000100A6 File Offset: 0x0000E2A6
		public static MethodInfo Getter(this PropertyInfo property)
		{
			return property.GetMethod;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000100AE File Offset: 0x0000E2AE
		public static MethodInfo Setter(this PropertyInfo property)
		{
			return property.SetMethod;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000100B6 File Offset: 0x0000E2B6
		public static bool IsStatic(this PropertyInfo property)
		{
			return (property.Getter() ?? property.Setter()).IsStatic;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000100D0 File Offset: 0x0000E2D0
		public static bool IsPublic(this PropertyInfo property)
		{
			MethodInfo methodInfo = property.Getter();
			MethodAttributes methodAttributes = ((methodInfo == null) ? MethodAttributes.Private : (methodInfo.Attributes & MethodAttributes.MemberAccessMask));
			MethodInfo methodInfo2 = property.Setter();
			MethodAttributes methodAttributes2 = ((methodInfo2 == null) ? MethodAttributes.Private : (methodInfo2.Attributes & MethodAttributes.MemberAccessMask));
			return ((methodAttributes > methodAttributes2) ? methodAttributes : methodAttributes2) == MethodAttributes.Public;
		}
	}
}
