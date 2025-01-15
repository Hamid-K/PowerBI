using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001B7 RID: 439
	internal static class CollectionDeserializationHelpers
	{
		// Token: 0x06000E7A RID: 3706 RVA: 0x0003B560 File Offset: 0x00039760
		public static void AddToCollection(this IEnumerable items, IEnumerable collection, Type elementType, Type resourceType, string propertyName, Type propertyType)
		{
			MethodInfo methodInfo = null;
			IList list = collection as IList;
			if (list == null)
			{
				methodInfo = collection.GetType().GetMethod("Add", new Type[] { elementType });
				if (methodInfo == null)
				{
					throw new SerializationException(Error.Format(SRResources.CollectionShouldHaveAddMethod, new object[] { propertyType.FullName, propertyName, resourceType.FullName }));
				}
			}
			else if (list.GetType().IsArray)
			{
				throw new SerializationException(Error.Format(SRResources.GetOnlyCollectionCannotBeArray, new object[] { propertyName, resourceType.FullName }));
			}
			items.AddToCollectionCore(collection, elementType, list, methodInfo);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003B608 File Offset: 0x00039808
		public static void AddToCollection(this IEnumerable items, IEnumerable collection, Type elementType, string paramName, Type paramType)
		{
			MethodInfo methodInfo = null;
			IList list = collection as IList;
			if (list == null)
			{
				methodInfo = collection.GetType().GetMethod("Add", new Type[] { elementType });
				if (methodInfo == null)
				{
					throw new SerializationException(Error.Format(SRResources.CollectionParameterShouldHaveAddMethod, new object[] { paramType, paramName }));
				}
			}
			items.AddToCollectionCore(collection, elementType, list, methodInfo);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003B670 File Offset: 0x00039870
		private static void AddToCollectionCore(this IEnumerable items, IEnumerable collection, Type elementType, IList list, MethodInfo addMethod)
		{
			bool flag;
			EdmLibHelpers.IsNonstandardEdmPrimitive(elementType, out flag);
			foreach (object obj in items)
			{
				if (flag && obj != null)
				{
					obj = EdmPrimitiveHelpers.ConvertPrimitiveValue(obj, elementType);
				}
				if (list != null)
				{
					list.Add(obj);
				}
				else
				{
					addMethod.Invoke(collection, new object[] { obj });
				}
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0003B6F0 File Offset: 0x000398F0
		public static void Clear(this IEnumerable collection, string propertyName, Type resourceType)
		{
			MethodInfo method = collection.GetType().GetMethod("Clear", CollectionDeserializationHelpers._emptyTypeArray);
			if (method == null)
			{
				throw new SerializationException(Error.Format(SRResources.CollectionShouldHaveClearMethod, new object[]
				{
					collection.GetType().FullName,
					propertyName,
					resourceType.FullName
				}));
			}
			method.Invoke(collection, CollectionDeserializationHelpers._emptyObjectArray);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0003B75C File Offset: 0x0003995C
		public static bool TryCreateInstance(Type collectionType, IEdmCollectionTypeReference edmCollectionType, Type elementType, out IEnumerable instance)
		{
			if (collectionType == typeof(EdmComplexObjectCollection))
			{
				instance = new EdmComplexObjectCollection(edmCollectionType);
				return true;
			}
			if (collectionType == typeof(EdmEntityObjectCollection))
			{
				instance = new EdmEntityObjectCollection(edmCollectionType);
				return true;
			}
			if (collectionType == typeof(EdmEnumObjectCollection))
			{
				instance = new EdmEnumObjectCollection(edmCollectionType);
				return true;
			}
			if (collectionType.IsGenericType())
			{
				Type genericTypeDefinition = collectionType.GetGenericTypeDefinition();
				if (genericTypeDefinition == typeof(IEnumerable<>) || genericTypeDefinition == typeof(ICollection<>) || genericTypeDefinition == typeof(IList<>))
				{
					instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { elementType })) as IEnumerable;
					return true;
				}
			}
			if (collectionType.IsArray)
			{
				instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { elementType })) as IEnumerable;
				return true;
			}
			if (collectionType.GetConstructor(Type.EmptyTypes) != null && !TypeHelper.IsAbstract(collectionType))
			{
				instance = Activator.CreateInstance(collectionType) as IEnumerable;
				return true;
			}
			instance = null;
			return false;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003B886 File Offset: 0x00039A86
		public static IEnumerable ToArray(IEnumerable value, Type elementType)
		{
			return CollectionDeserializationHelpers._toArrayMethodInfo.MakeGenericMethod(new Type[] { elementType }).Invoke(null, new object[] { value }) as IEnumerable;
		}

		// Token: 0x0400040C RID: 1036
		private static readonly Type[] _emptyTypeArray = new Type[0];

		// Token: 0x0400040D RID: 1037
		private static readonly object[] _emptyObjectArray = new object[0];

		// Token: 0x0400040E RID: 1038
		private static readonly MethodInfo _toArrayMethodInfo = typeof(Enumerable).GetMethod("ToArray");
	}
}
