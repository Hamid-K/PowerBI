using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.Internal
{
	// Token: 0x02000186 RID: 390
	internal static class CollectionModelBinderUtil
	{
		// Token: 0x06000A0D RID: 2573 RVA: 0x00019F44 File Offset: 0x00018144
		internal static void CreateOrReplaceCollection<TElement>(ModelBindingContext bindingContext, IEnumerable<TElement> incomingElements, Func<ICollection<TElement>> creator)
		{
			ICollection<TElement> collection = bindingContext.Model as ICollection<TElement>;
			if (collection == null || collection.IsReadOnly)
			{
				collection = creator();
				bindingContext.Model = collection;
			}
			collection.Clear();
			foreach (TElement telement in incomingElements)
			{
				collection.Add(telement);
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00019FB8 File Offset: 0x000181B8
		internal static void CreateOrReplaceDictionary<TKey, TValue>(ModelBindingContext bindingContext, IEnumerable<KeyValuePair<TKey, TValue>> incomingElements, Func<IDictionary<TKey, TValue>> creator)
		{
			IDictionary<TKey, TValue> dictionary = bindingContext.Model as IDictionary<TKey, TValue>;
			if (dictionary == null || dictionary.IsReadOnly)
			{
				dictionary = creator();
				bindingContext.Model = dictionary;
			}
			dictionary.Clear();
			foreach (KeyValuePair<TKey, TValue> keyValuePair in incomingElements)
			{
				if (keyValuePair.Key != null)
				{
					dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001A048 File Offset: 0x00018248
		internal static IModelBinder GetGenericBinder(Type supportedInterfaceType, Type newInstanceType, Type openBinderType, Type modelType)
		{
			Type[] genericBinderTypeArgs = CollectionModelBinderUtil.GetGenericBinderTypeArgs(supportedInterfaceType, modelType);
			if (genericBinderTypeArgs == null)
			{
				return null;
			}
			Type type = newInstanceType.MakeGenericType(genericBinderTypeArgs);
			if (!modelType.IsAssignableFrom(type))
			{
				return null;
			}
			return (IModelBinder)Activator.CreateInstance(openBinderType.MakeGenericType(genericBinderTypeArgs));
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001A088 File Offset: 0x00018288
		internal static Type[] GetGenericBinderTypeArgs(Type supportedInterfaceType, Type modelType)
		{
			if (!modelType.IsGenericType || modelType.IsGenericTypeDefinition)
			{
				return null;
			}
			Type[] genericArguments = modelType.GetGenericArguments();
			if (genericArguments.Length != supportedInterfaceType.GetGenericArguments().Length)
			{
				return null;
			}
			return genericArguments;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001A0C0 File Offset: 0x000182C0
		internal static IEnumerable<string> GetIndexNamesFromValueProviderResult(ValueProviderResult valueProviderResultIndex)
		{
			IEnumerable<string> enumerable = null;
			if (valueProviderResultIndex != null)
			{
				string[] array = (string[])valueProviderResultIndex.ConvertTo(typeof(string[]));
				if (array != null && array.Length != 0)
				{
					enumerable = array;
				}
			}
			return enumerable;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001A0F2 File Offset: 0x000182F2
		internal static IEnumerable<string> GetZeroBasedIndexes()
		{
			int i = 0;
			for (;;)
			{
				yield return i.ToString(CultureInfo.InvariantCulture);
				int num = i;
				i = num + 1;
			}
			yield break;
		}
	}
}
