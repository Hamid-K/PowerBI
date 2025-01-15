using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.Client.Metadata
{
	// Token: 0x020000F4 RID: 244
	internal static class ClientTypeUtil
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x0002650E File Offset: 0x0002470E
		internal static void SetClientTypeAnnotation(this IEdmModel model, IEdmType edmType, ClientTypeAnnotation annotation)
		{
			model.SetAnnotationValue(edmType, annotation);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00026518 File Offset: 0x00024718
		internal static ClientTypeAnnotation GetClientTypeAnnotation(this ClientEdmModel model, Type type)
		{
			IEdmType orCreateEdmType = model.GetOrCreateEdmType(type);
			return model.GetClientTypeAnnotation(orCreateEdmType);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00026534 File Offset: 0x00024734
		internal static ClientTypeAnnotation GetClientTypeAnnotation(this IEdmModel model, IEdmType edmType)
		{
			return model.GetAnnotationValue(edmType);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002653D File Offset: 0x0002473D
		internal static void SetClientPropertyAnnotation(this IEdmProperty edmProperty, ClientPropertyAnnotation annotation)
		{
			annotation.Model.SetAnnotationValue(edmProperty, annotation);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002654C File Offset: 0x0002474C
		internal static ClientPropertyAnnotation GetClientPropertyAnnotation(this IEdmModel model, IEdmProperty edmProperty)
		{
			return model.GetAnnotationValue(edmProperty);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00026558 File Offset: 0x00024758
		internal static ClientTypeAnnotation GetClientTypeAnnotation(this IEdmModel model, IEdmProperty edmProperty)
		{
			IEdmType definition = edmProperty.Type.Definition;
			return model.GetAnnotationValue(definition);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00026578 File Offset: 0x00024778
		internal static IEdmTypeReference ToEdmTypeReference(this IEdmType edmType, bool isNullable)
		{
			return edmType.ToTypeReference(isNullable);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00026584 File Offset: 0x00024784
		internal static string FullName(this IEdmType edmType)
		{
			IEdmSchemaElement edmSchemaElement = edmType as IEdmSchemaElement;
			if (edmSchemaElement != null)
			{
				return edmSchemaElement.FullName();
			}
			return null;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000265A4 File Offset: 0x000247A4
		internal static MethodInfo GetMethodForGenericType(Type propertyType, Type genericTypeDefinition, string methodName, out Type type)
		{
			type = null;
			Type implementationType = ClientTypeUtil.GetImplementationType(propertyType, genericTypeDefinition);
			if (null != implementationType)
			{
				Type[] genericArguments = implementationType.GetGenericArguments();
				MethodInfo method = implementationType.GetMethod(methodName);
				type = genericArguments[genericArguments.Length - 1];
				return method;
			}
			return null;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000265E0 File Offset: 0x000247E0
		internal static Action<object, object> GetAddToCollectionDelegate(Type listType)
		{
			Type type;
			MethodInfo addToCollectionMethod = ClientTypeUtil.GetAddToCollectionMethod(listType, out type);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "list");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "element");
			Expression expression = Expression.Call(Expression.Convert(parameterExpression, listType), addToCollectionMethod, new Expression[] { Expression.Convert(parameterExpression2, type) });
			LambdaExpression lambdaExpression = Expression.Lambda(expression, new ParameterExpression[] { parameterExpression, parameterExpression2 });
			return (Action<object, object>)lambdaExpression.Compile();
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00026662 File Offset: 0x00024862
		internal static MethodInfo GetAddToCollectionMethod(Type collectionType, out Type type)
		{
			return ClientTypeUtil.GetMethodForGenericType(collectionType, typeof(ICollection<>), "Add", out type);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002667C File Offset: 0x0002487C
		internal static Type GetImplementationType(Type type, Type genericTypeDefinition)
		{
			if (ClientTypeUtil.IsConstructedGeneric(type, genericTypeDefinition))
			{
				return type;
			}
			Type type2 = null;
			foreach (Type type3 in type.GetInterfaces())
			{
				if (ClientTypeUtil.IsConstructedGeneric(type3, genericTypeDefinition))
				{
					if (!(null == type2))
					{
						throw Error.NotSupported(Strings.ClientType_MultipleImplementationNotSupported);
					}
					type2 = type3;
				}
			}
			return type2;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000266D1 File Offset: 0x000248D1
		internal static bool TypeIsEntity(Type t, ClientEdmModel model)
		{
			return model.GetOrCreateEdmType(t).TypeKind == EdmTypeKind.Entity;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000266E4 File Offset: 0x000248E4
		internal static bool TypeIsStructured(Type t, ClientEdmModel model)
		{
			EdmTypeKind typeKind = model.GetOrCreateEdmType(t).TypeKind;
			return typeKind == EdmTypeKind.Entity || typeKind == EdmTypeKind.Complex;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00026708 File Offset: 0x00024908
		internal static bool TypeOrElementTypeIsEntity(Type type)
		{
			type = TypeSystem.GetElementType(type);
			type = Nullable.GetUnderlyingType(type) ?? type;
			return !PrimitiveType.IsKnownType(type) && ClientTypeUtil.GetKeyPropertiesOnType(type) != null;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00026732 File Offset: 0x00024932
		internal static bool TypeOrElementTypeIsStructured(Type type)
		{
			type = TypeSystem.GetElementType(type);
			type = Nullable.GetUnderlyingType(type) ?? type;
			return !PrimitiveType.IsKnownType(type) && !type.IsEnum();
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002675C File Offset: 0x0002495C
		internal static bool IsDataServiceCollection(Type type)
		{
			while (type != null)
			{
				if (type.IsGenericType() && WebUtil.IsDataServiceCollectionType(type.GetGenericTypeDefinition()))
				{
					return true;
				}
				type = type.GetBaseType();
			}
			return false;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00026789 File Offset: 0x00024989
		internal static bool CanAssignNull(Type type)
		{
			return !type.IsValueType() || (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000267B4 File Offset: 0x000249B4
		internal static IEnumerable<PropertyInfo> GetPropertiesOnType(Type type, bool declaredOnly)
		{
			if (!PrimitiveType.IsKnownType(type))
			{
				foreach (PropertyInfo propertyInfo in type.GetPublicProperties(true, declaredOnly))
				{
					Type type2 = propertyInfo.PropertyType;
					type2 = Nullable.GetUnderlyingType(type2) ?? type2;
					if (!type2.IsPointer && (!type2.IsArray || !(typeof(byte[]) != type2) || !(typeof(char[]) != type2)) && !(typeof(IntPtr) == type2) && !(typeof(UIntPtr) == type2) && (!declaredOnly || !ClientTypeUtil.IsOverride(type, propertyInfo)) && propertyInfo.CanRead && (!type2.IsValueType() || propertyInfo.CanWrite) && !type2.ContainsGenericParameters() && propertyInfo.GetIndexParameters().Length == 0)
					{
						yield return propertyInfo;
					}
				}
				IEnumerator<PropertyInfo> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000267CC File Offset: 0x000249CC
		internal static PropertyInfo[] GetKeyPropertiesOnType(Type type)
		{
			bool flag;
			return ClientTypeUtil.GetKeyPropertiesOnType(type, out flag);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000267E4 File Offset: 0x000249E4
		internal static PropertyInfo[] GetKeyPropertiesOnType(Type type, out bool hasProperties)
		{
			if (CommonUtil.IsUnsupportedType(type))
			{
				throw new InvalidOperationException(Strings.ClientType_UnsupportedType(type));
			}
			string text = type.ToString();
			IEnumerable<object> customAttributes = type.GetCustomAttributes(true);
			bool flag = customAttributes.OfType<EntityTypeAttribute>().Any<EntityTypeAttribute>();
			KeyAttribute keyAttribute = customAttributes.OfType<KeyAttribute>().FirstOrDefault<KeyAttribute>();
			List<PropertyInfo> list = new List<PropertyInfo>();
			PropertyInfo[] properties = ClientTypeUtil.GetPropertiesOnType(type, false).ToArray<PropertyInfo>();
			hasProperties = properties.Length != 0;
			ClientTypeUtil.KeyKind keyKind = ClientTypeUtil.KeyKind.NotKey;
			ClientTypeUtil.KeyKind keyKind2 = ClientTypeUtil.KeyKind.NotKey;
			foreach (PropertyInfo propertyInfo in properties)
			{
				if ((keyKind2 = ClientTypeUtil.IsKeyProperty(propertyInfo, keyAttribute)) != ClientTypeUtil.KeyKind.NotKey)
				{
					if (keyKind2 > keyKind)
					{
						list.Clear();
						keyKind = keyKind2;
						list.Add(propertyInfo);
					}
					else if (keyKind2 == keyKind)
					{
						list.Add(propertyInfo);
					}
				}
			}
			Type type2 = null;
			foreach (PropertyInfo propertyInfo2 in list)
			{
				if (null == type2)
				{
					type2 = propertyInfo2.DeclaringType;
				}
				else if (type2 != propertyInfo2.DeclaringType)
				{
					throw Error.InvalidOperation(Strings.ClientType_KeysOnDifferentDeclaredType(text));
				}
				if (!PrimitiveType.IsKnownType(propertyInfo2.PropertyType) && (!(propertyInfo2.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) || !propertyInfo2.PropertyType.GetGenericArguments().First<Type>().IsEnum()))
				{
					throw Error.InvalidOperation(Strings.ClientType_KeysMustBeSimpleTypes(propertyInfo2.Name, text, propertyInfo2.PropertyType.FullName));
				}
			}
			if (keyKind2 == ClientTypeUtil.KeyKind.AttributedKey && list.Count != keyAttribute.KeyNames.Count)
			{
				string text2 = (from string a in keyAttribute.KeyNames
					where null == properties.Where((PropertyInfo b) => b.Name == a).FirstOrDefault<PropertyInfo>()
					select a).First<string>();
				throw Error.InvalidOperation(Strings.ClientType_MissingProperty(text, text2));
			}
			if (list.Count > 0)
			{
				return list.ToArray();
			}
			if (!flag)
			{
				return null;
			}
			return ClientTypeUtil.EmptyPropertyInfoArray;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000269FC File Offset: 0x00024BFC
		internal static Type GetMemberType(MemberInfo member)
		{
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.PropertyType;
			}
			FieldInfo fieldInfo = member as FieldInfo;
			return fieldInfo.FieldType;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00026A30 File Offset: 0x00024C30
		internal static string GetServerDefinedName(PropertyInfo propertyInfo)
		{
			OriginalNameAttribute originalNameAttribute = (OriginalNameAttribute)propertyInfo.GetCustomAttributes(typeof(OriginalNameAttribute), false).SingleOrDefault<object>();
			if (originalNameAttribute != null)
			{
				return originalNameAttribute.OriginalName;
			}
			return propertyInfo.Name;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00026A6C File Offset: 0x00024C6C
		internal static string GetServerDefinedName(MemberInfo memberInfo)
		{
			OriginalNameAttribute originalNameAttribute = (OriginalNameAttribute)memberInfo.GetCustomAttributes(typeof(OriginalNameAttribute), false).SingleOrDefault<object>();
			if (originalNameAttribute != null)
			{
				return originalNameAttribute.OriginalName;
			}
			return memberInfo.Name;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00026AA8 File Offset: 0x00024CA8
		internal static string GetServerDefinedTypeName(Type type)
		{
			OriginalNameAttribute originalNameAttribute = (OriginalNameAttribute)type.GetCustomAttributes(typeof(OriginalNameAttribute), false).SingleOrDefault<object>();
			if (originalNameAttribute != null)
			{
				return originalNameAttribute.OriginalName;
			}
			return type.Name;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00026AE4 File Offset: 0x00024CE4
		internal static string GetServerDefinedTypeFullName(Type type)
		{
			OriginalNameAttribute originalNameAttribute = (OriginalNameAttribute)type.GetCustomAttributes(typeof(OriginalNameAttribute), false).SingleOrDefault<object>();
			if (originalNameAttribute != null)
			{
				return type.Namespace + "." + originalNameAttribute.OriginalName;
			}
			return type.FullName;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00026B30 File Offset: 0x00024D30
		internal static string GetClientFieldName(Type t, string serverDefinedName)
		{
			List<string> list = (from name in serverDefinedName.Split(new char[] { ',' })
				select name.Trim()).ToList<string>();
			List<string> list2 = new List<string>();
			using (List<string>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string serverSideName = enumerator.Current;
					FieldInfo fieldInfo = t.GetField(serverSideName) ?? t.GetFields().ToList<FieldInfo>().Where(delegate(FieldInfo m)
					{
						OriginalNameAttribute originalNameAttribute = (OriginalNameAttribute)m.GetCustomAttributes(typeof(OriginalNameAttribute), false).SingleOrDefault<object>();
						return originalNameAttribute != null && originalNameAttribute.OriginalName == serverSideName;
					})
						.SingleOrDefault<FieldInfo>();
					if (fieldInfo == null)
					{
						throw Error.InvalidOperation(Strings.ClientType_MissingProperty(t.ToString(), serverSideName));
					}
					list2.Add(fieldInfo.Name);
				}
			}
			return string.Join(",", list2);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00026C30 File Offset: 0x00024E30
		internal static PropertyInfo GetClientPropertyInfo(Type t, string serverDefinedName, UndeclaredPropertyBehavior undeclaredPropertyBehavior)
		{
			PropertyInfo propertyInfo = t.GetProperty(serverDefinedName);
			if (propertyInfo == null)
			{
				propertyInfo = t.GetProperties().Where(delegate(PropertyInfo m)
				{
					OriginalNameAttribute originalNameAttribute = (OriginalNameAttribute)m.GetCustomAttributes(typeof(OriginalNameAttribute), false).SingleOrDefault<object>();
					return originalNameAttribute != null && originalNameAttribute.OriginalName == serverDefinedName;
				}).SingleOrDefault<PropertyInfo>();
			}
			if (propertyInfo == null && undeclaredPropertyBehavior == UndeclaredPropertyBehavior.ThrowException)
			{
				throw Error.InvalidOperation(Strings.ClientType_MissingProperty(t.ToString(), serverDefinedName));
			}
			return propertyInfo;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00026CA4 File Offset: 0x00024EA4
		internal static string GetClientPropertyName(Type t, string serverDefinedName, UndeclaredPropertyBehavior undeclaredPropertyBehavior)
		{
			PropertyInfo clientPropertyInfo = ClientTypeUtil.GetClientPropertyInfo(t, serverDefinedName, undeclaredPropertyBehavior);
			if (!(clientPropertyInfo == null))
			{
				return clientPropertyInfo.Name;
			}
			return serverDefinedName;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00026CCC File Offset: 0x00024ECC
		internal static MethodInfo GetClientMethod(Type t, string serverDefinedName, Type[] parameters)
		{
			MethodInfo methodInfo = t.GetMethod(serverDefinedName, parameters);
			if (methodInfo == null)
			{
				MethodInfo methodInfo2 = t.GetMethods().Where(delegate(MethodInfo m)
				{
					OriginalNameAttribute originalNameAttribute = (OriginalNameAttribute)m.GetCustomAttributes(typeof(OriginalNameAttribute), false).SingleOrDefault<object>();
					return originalNameAttribute != null && originalNameAttribute.OriginalName == serverDefinedName;
				}).FirstOrDefault<MethodInfo>();
				if (methodInfo2 != null)
				{
					methodInfo = t.GetMethod(methodInfo2.Name, parameters);
				}
			}
			return methodInfo;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00026D34 File Offset: 0x00024F34
		internal static string GetEnumValuesString(string enumString, Type enumType)
		{
			string[] array = (from v in enumString.Split(new char[] { ',' })
				select v.Trim()).ToArray<string>();
			List<string> list = new List<string>();
			foreach (string text in array)
			{
				MemberInfo field = enumType.GetField(text);
				if (field == null)
				{
					throw new NotSupportedException(Strings.Serializer_InvalidEnumMemberValue(enumType.Name, text));
				}
				list.Add(ClientTypeUtil.GetServerDefinedName(field));
			}
			return string.Join(",", list);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00026DD8 File Offset: 0x00024FD8
		private static ClientTypeUtil.KeyKind IsKeyProperty(PropertyInfo propertyInfo, KeyAttribute dataServiceKeyAttribute)
		{
			string serverDefinedName = ClientTypeUtil.GetServerDefinedName(propertyInfo);
			ClientTypeUtil.KeyKind keyKind = ClientTypeUtil.KeyKind.NotKey;
			if (dataServiceKeyAttribute != null && dataServiceKeyAttribute.KeyNames.Contains(serverDefinedName))
			{
				keyKind = ClientTypeUtil.KeyKind.AttributedKey;
			}
			else if (serverDefinedName.EndsWith("ID", StringComparison.Ordinal))
			{
				string name = propertyInfo.DeclaringType.Name;
				if (serverDefinedName.Length == name.Length + 2 && serverDefinedName.StartsWith(name, StringComparison.Ordinal))
				{
					keyKind = ClientTypeUtil.KeyKind.TypeNameId;
				}
				else if (2 == serverDefinedName.Length)
				{
					keyKind = ClientTypeUtil.KeyKind.Id;
				}
			}
			return keyKind;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00026E47 File Offset: 0x00025047
		private static bool IsConstructedGeneric(Type type, Type genericTypeDefinition)
		{
			return type.IsGenericType() && type.GetGenericTypeDefinition() == genericTypeDefinition && !type.ContainsGenericParameters();
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00026E6C File Offset: 0x0002506C
		private static bool IsOverride(Type type, PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetGetMethod();
			return getMethod != null && getMethod.GetBaseDefinition().DeclaringType != type;
		}

		// Token: 0x04000603 RID: 1539
		internal static readonly PropertyInfo[] EmptyPropertyInfoArray = new PropertyInfo[0];

		// Token: 0x020001C5 RID: 453
		private enum KeyKind
		{
			// Token: 0x040007FF RID: 2047
			NotKey,
			// Token: 0x04000800 RID: 2048
			Id,
			// Token: 0x04000801 RID: 2049
			TypeNameId,
			// Token: 0x04000802 RID: 2050
			AttributedKey
		}
	}
}
