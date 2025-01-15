using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200008E RID: 142
	internal static class PropertyInfoExtensions
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x00010A40 File Offset: 0x0000EC40
		public static bool IsSameAs(this PropertyInfo propertyInfo, PropertyInfo otherPropertyInfo)
		{
			return propertyInfo == otherPropertyInfo || (propertyInfo.Name == otherPropertyInfo.Name && (propertyInfo.DeclaringType == otherPropertyInfo.DeclaringType || propertyInfo.DeclaringType.IsSubclassOf(otherPropertyInfo.DeclaringType) || otherPropertyInfo.DeclaringType.IsSubclassOf(propertyInfo.DeclaringType) || propertyInfo.DeclaringType.GetInterfaces().Contains(otherPropertyInfo.DeclaringType) || otherPropertyInfo.DeclaringType.GetInterfaces().Contains(propertyInfo.DeclaringType)));
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00010AD6 File Offset: 0x0000ECD6
		public static bool ContainsSame(this IEnumerable<PropertyInfo> enumerable, PropertyInfo propertyInfo)
		{
			return enumerable.Any(new Func<PropertyInfo, bool>(propertyInfo.IsSameAs));
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00010AEA File Offset: 0x0000ECEA
		public static bool IsValidStructuralProperty(this PropertyInfo propertyInfo)
		{
			return propertyInfo.IsValidInterfaceStructuralProperty() && !propertyInfo.Getter().IsAbstract;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00010B04 File Offset: 0x0000ED04
		public static bool IsValidInterfaceStructuralProperty(this PropertyInfo propertyInfo)
		{
			return propertyInfo.CanRead && (propertyInfo.CanWriteExtended() || propertyInfo.PropertyType.IsCollection()) && propertyInfo.GetIndexParameters().Length == 0 && propertyInfo.PropertyType.IsValidStructuralPropertyType();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00010B39 File Offset: 0x0000ED39
		public static bool IsValidEdmScalarProperty(this PropertyInfo propertyInfo)
		{
			return propertyInfo.IsValidInterfaceStructuralProperty() && propertyInfo.PropertyType.IsValidEdmScalarType();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00010B50 File Offset: 0x0000ED50
		public static bool IsValidEdmNavigationProperty(this PropertyInfo propertyInfo)
		{
			Type type;
			return propertyInfo.IsValidInterfaceStructuralProperty() && ((propertyInfo.PropertyType.IsCollection(out type) && type.IsValidStructuralType()) || propertyInfo.PropertyType.IsValidStructuralType());
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00010B8C File Offset: 0x0000ED8C
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

		// Token: 0x06000487 RID: 1159 RVA: 0x00010BD8 File Offset: 0x0000EDD8
		public static bool CanWriteExtended(this PropertyInfo propertyInfo)
		{
			if (propertyInfo.CanWrite)
			{
				return true;
			}
			PropertyInfo declaredProperty = PropertyInfoExtensions.GetDeclaredProperty(propertyInfo);
			return declaredProperty != null && declaredProperty.CanWrite;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00010C07 File Offset: 0x0000EE07
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

		// Token: 0x06000489 RID: 1161 RVA: 0x00010C20 File Offset: 0x0000EE20
		private static PropertyInfo GetDeclaredProperty(PropertyInfo propertyInfo)
		{
			if (!(propertyInfo.DeclaringType == propertyInfo.ReflectedType))
			{
				return propertyInfo.DeclaringType.GetInstanceProperties().SingleOrDefault((PropertyInfo p) => p.Name == propertyInfo.Name && p.DeclaringType == propertyInfo.DeclaringType && !p.GetIndexParameters().Any<ParameterInfo>() && p.PropertyType == propertyInfo.PropertyType);
			}
			return propertyInfo;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00010C80 File Offset: 0x0000EE80
		public static IEnumerable<PropertyInfo> GetPropertiesInHierarchy(this PropertyInfo property)
		{
			List<PropertyInfo> list = new List<PropertyInfo> { property };
			PropertyInfoExtensions.CollectProperties(property, list);
			return list.Distinct<PropertyInfo>();
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00010CA7 File Offset: 0x0000EEA7
		private static void CollectProperties(PropertyInfo property, IList<PropertyInfo> collection)
		{
			PropertyInfoExtensions.FindNextProperty(property, collection, true);
			PropertyInfoExtensions.FindNextProperty(property, collection, false);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00010CBC File Offset: 0x0000EEBC
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

		// Token: 0x0600048D RID: 1165 RVA: 0x00010DA2 File Offset: 0x0000EFA2
		public static MethodInfo Getter(this PropertyInfo property)
		{
			return property.GetMethod;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00010DAA File Offset: 0x0000EFAA
		public static MethodInfo Setter(this PropertyInfo property)
		{
			return property.SetMethod;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00010DB2 File Offset: 0x0000EFB2
		public static bool IsStatic(this PropertyInfo property)
		{
			return (property.Getter() ?? property.Setter()).IsStatic;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00010DCC File Offset: 0x0000EFCC
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
