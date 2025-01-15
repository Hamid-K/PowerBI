using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000B6 RID: 182
	internal static class BindingUtils
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x0001B2D1 File Offset: 0x000194D1
		internal static void ValidateEntitySetName(string entitySetName, object entity)
		{
			if (string.IsNullOrEmpty(entitySetName))
			{
				throw new InvalidOperationException(Strings.DataBinding_Util_UnknownEntitySetName(entity.GetType().FullName));
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001B2F1 File Offset: 0x000194F1
		internal static Type GetCollectionEntityType(Type collectionType)
		{
			while (collectionType != null)
			{
				if (collectionType.IsGenericType() && WebUtil.IsDataServiceCollectionType(collectionType.GetGenericTypeDefinition()))
				{
					return collectionType.GetGenericArguments()[0];
				}
				collectionType = collectionType.GetBaseType();
			}
			return null;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001B328 File Offset: 0x00019528
		internal static void VerifyObserverNotPresent<T>(object oec, string sourceProperty, Type sourceType)
		{
			DataServiceCollection<T> dataServiceCollection = oec as DataServiceCollection<T>;
			if (dataServiceCollection.Observer != null)
			{
				throw new InvalidOperationException(Strings.DataBinding_CollectionPropertySetterValueHasObserver(sourceProperty, sourceType));
			}
		}
	}
}
