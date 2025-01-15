using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000468 RID: 1128
	internal static class TypeSystem
	{
		// Token: 0x0600376D RID: 14189 RVA: 0x000B580C File Offset: 0x000B3A0C
		private static T GetDefault<T>()
		{
			return default(T);
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000B5824 File Offset: 0x000B3A24
		internal static object GetDefaultValue(Type type)
		{
			if (!type.IsValueType() || (type.IsGenericType() && typeof(Nullable<>) == type.GetGenericTypeDefinition()))
			{
				return null;
			}
			return TypeSystem.GetDefaultMethod.MakeGenericMethod(new Type[] { type }).Invoke(null, new object[0]);
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000B587A File Offset: 0x000B3A7A
		internal static bool IsSequenceType(Type seqType)
		{
			return TypeSystem.FindIEnumerable(seqType) != null;
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000B5888 File Offset: 0x000B3A88
		internal static Type GetDelegateType(IEnumerable<Type> inputTypes, Type returnType)
		{
			inputTypes = inputTypes ?? Enumerable.Empty<Type>();
			int num = inputTypes.Count<Type>();
			Type[] array = new Type[num + 1];
			int num2 = 0;
			foreach (Type type in inputTypes)
			{
				array[num2++] = type;
			}
			array[num2] = returnType;
			Type type2;
			switch (num)
			{
			case 0:
				type2 = typeof(Func<>);
				break;
			case 1:
				type2 = typeof(Func<, >);
				break;
			case 2:
				type2 = typeof(Func<, , >);
				break;
			case 3:
				type2 = typeof(Func<, , , >);
				break;
			case 4:
				type2 = typeof(Func<, , , , >);
				break;
			case 5:
				type2 = typeof(Func<, , , , , >);
				break;
			case 6:
				type2 = typeof(Func<, , , , , , >);
				break;
			case 7:
				type2 = typeof(Func<, , , , , , , >);
				break;
			case 8:
				type2 = typeof(Func<, , , , , , , , >);
				break;
			case 9:
				type2 = typeof(Func<, , , , , , , , , >);
				break;
			case 10:
				type2 = typeof(Func<, , , , , , , , , , >);
				break;
			case 11:
				type2 = typeof(Func<, , , , , , , , , , , >);
				break;
			case 12:
				type2 = typeof(Func<, , , , , , , , , , , , >);
				break;
			case 13:
				type2 = typeof(Func<, , , , , , , , , , , , , >);
				break;
			case 14:
				type2 = typeof(Func<, , , , , , , , , , , , , , >);
				break;
			case 15:
				type2 = typeof(Func<, , , , , , , , , , , , , , , >);
				break;
			default:
				type2 = null;
				break;
			}
			return type2.MakeGenericType(array);
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000B5A34 File Offset: 0x000B3C34
		internal static Expression EnsureType(Expression expression, Type requiredType)
		{
			if (expression.Type != requiredType)
			{
				expression = Expression.Convert(expression, requiredType);
			}
			return expression;
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000B5A50 File Offset: 0x000B3C50
		internal static MemberInfo PropertyOrField(MemberInfo member, out string name, out Type type)
		{
			name = null;
			type = null;
			if (member.MemberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				name = fieldInfo.Name;
				type = fieldInfo.FieldType;
				return fieldInfo;
			}
			if (member.MemberType != MemberTypes.Property)
			{
				if (member.MemberType == MemberTypes.Method)
				{
					MethodInfo methodInfo = (MethodInfo)member;
					if (methodInfo.IsSpecialName)
					{
						foreach (PropertyInfo propertyInfo in methodInfo.DeclaringType.GetRuntimeProperties())
						{
							if (propertyInfo.CanRead && propertyInfo.Getter() == methodInfo)
							{
								return TypeSystem.PropertyOrField(propertyInfo, out name, out type);
							}
						}
					}
				}
				throw new NotSupportedException(Strings.ELinq_NotPropertyOrField(member.Name));
			}
			PropertyInfo propertyInfo2 = (PropertyInfo)member;
			if (propertyInfo2.GetIndexParameters().Length != 0)
			{
				throw new NotSupportedException(Strings.ELinq_PropertyIndexNotSupported);
			}
			name = propertyInfo2.Name;
			type = propertyInfo2.PropertyType;
			return propertyInfo2;
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x000B5B4C File Offset: 0x000B3D4C
		private static Type FindIEnumerable(Type seqType)
		{
			if (seqType == null || seqType == typeof(string) || seqType == typeof(byte[]))
			{
				return null;
			}
			if (seqType.IsArray)
			{
				return typeof(IEnumerable<>).MakeGenericType(new Type[] { seqType.GetElementType() });
			}
			if (seqType.IsGenericType())
			{
				foreach (Type type in seqType.GetGenericArguments())
				{
					Type type2 = typeof(IEnumerable<>).MakeGenericType(new Type[] { type });
					if (type2.IsAssignableFrom(seqType))
					{
						return type2;
					}
				}
			}
			Type[] interfaces = seqType.GetInterfaces();
			if (interfaces != null && interfaces.Length != 0)
			{
				Type[] array = interfaces;
				for (int i = 0; i < array.Length; i++)
				{
					Type type3 = TypeSystem.FindIEnumerable(array[i]);
					if (type3 != null)
					{
						return type3;
					}
				}
			}
			if (seqType.BaseType() != null && seqType.BaseType() != typeof(object))
			{
				return TypeSystem.FindIEnumerable(seqType.BaseType());
			}
			return null;
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x000B5C60 File Offset: 0x000B3E60
		internal static Type GetElementType(Type seqType)
		{
			Type type = TypeSystem.FindIEnumerable(seqType);
			if (type == null)
			{
				return seqType;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000B5C87 File Offset: 0x000B3E87
		internal static Type GetNonNullableType(Type type)
		{
			if (type != null)
			{
				return Nullable.GetUnderlyingType(type) ?? type;
			}
			return null;
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000B5CA0 File Offset: 0x000B3EA0
		internal static bool IsImplementationOfGenericInterfaceMethod(this MethodInfo test, Type match, out Type[] genericTypeArguments)
		{
			genericTypeArguments = null;
			if (null == test || null == match || !match.IsInterface() || !match.IsGenericTypeDefinition() || null == test.DeclaringType)
			{
				return false;
			}
			if (test.DeclaringType.IsInterface() && test.DeclaringType.IsGenericType() && test.DeclaringType.GetGenericTypeDefinition() == match)
			{
				return true;
			}
			foreach (Type type in test.DeclaringType.GetInterfaces())
			{
				if (type.IsGenericType() && type.GetGenericTypeDefinition() == match && test.DeclaringType.GetInterfaceMap(type).TargetMethods.Contains(test))
				{
					genericTypeArguments = type.GetGenericArguments();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000B5D6C File Offset: 0x000B3F6C
		internal static bool IsImplementationOf(this PropertyInfo propertyInfo, Type interfaceType)
		{
			PropertyInfo declaredProperty = interfaceType.GetDeclaredProperty(propertyInfo.Name);
			if (null == declaredProperty)
			{
				return false;
			}
			if (propertyInfo.DeclaringType.IsInterface())
			{
				return declaredProperty.Equals(propertyInfo);
			}
			bool flag = false;
			MethodInfo methodInfo = declaredProperty.Getter();
			InterfaceMapping interfaceMap = propertyInfo.DeclaringType.GetInterfaceMap(interfaceType);
			int num = Array.IndexOf<MethodInfo>(interfaceMap.InterfaceMethods, methodInfo);
			MethodInfo[] targetMethods = interfaceMap.TargetMethods;
			if (num > -1 && num < targetMethods.Length)
			{
				MethodInfo methodInfo2 = propertyInfo.Getter();
				if (methodInfo2 != null)
				{
					flag = methodInfo2.Equals(targetMethods[num]);
				}
			}
			return flag;
		}

		// Token: 0x040012C5 RID: 4805
		internal static readonly MethodInfo GetDefaultMethod = typeof(TypeSystem).GetOnlyDeclaredMethod("GetDefault");
	}
}
