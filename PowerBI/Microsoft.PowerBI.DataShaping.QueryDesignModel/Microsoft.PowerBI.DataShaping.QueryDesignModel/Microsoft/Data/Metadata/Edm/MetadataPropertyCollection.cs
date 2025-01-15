using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common.Utils;
using System.Reflection;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200009E RID: 158
	internal sealed class MetadataPropertyCollection : MetadataCollection<MetadataProperty>
	{
		// Token: 0x06000B10 RID: 2832 RVA: 0x0001B28A File Offset: 0x0001948A
		internal MetadataPropertyCollection(MetadataItem item)
			: base(MetadataPropertyCollection.GetSystemMetadataProperties(item))
		{
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0001B298 File Offset: 0x00019498
		private static IEnumerable<MetadataProperty> GetSystemMetadataProperties(MetadataItem item)
		{
			EntityUtil.CheckArgumentNull<MetadataItem>(item, "item");
			return MetadataPropertyCollection.GetItemTypeInformation(item.GetType()).GetItemAttributes(item);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001B2B7 File Offset: 0x000194B7
		private static MetadataPropertyCollection.ItemTypeInformation GetItemTypeInformation(Type clrType)
		{
			return MetadataPropertyCollection.s_itemTypeMemoizer.Evaluate(clrType);
		}

		// Token: 0x0400086D RID: 2157
		private static readonly Memoizer<Type, MetadataPropertyCollection.ItemTypeInformation> s_itemTypeMemoizer = new Memoizer<Type, MetadataPropertyCollection.ItemTypeInformation>((Type clrType) => new MetadataPropertyCollection.ItemTypeInformation(clrType), null);

		// Token: 0x020002C0 RID: 704
		private class ItemTypeInformation
		{
			// Token: 0x06001C70 RID: 7280 RVA: 0x0004EE84 File Offset: 0x0004D084
			internal ItemTypeInformation(Type clrType)
			{
				this._itemProperties = MetadataPropertyCollection.ItemTypeInformation.GetItemProperties(clrType);
			}

			// Token: 0x06001C71 RID: 7281 RVA: 0x0004EE98 File Offset: 0x0004D098
			internal IEnumerable<MetadataProperty> GetItemAttributes(MetadataItem item)
			{
				foreach (MetadataPropertyCollection.ItemPropertyInfo itemPropertyInfo in this._itemProperties)
				{
					yield return itemPropertyInfo.GetMetadataProperty(item);
				}
				List<MetadataPropertyCollection.ItemPropertyInfo>.Enumerator enumerator = default(List<MetadataPropertyCollection.ItemPropertyInfo>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x06001C72 RID: 7282 RVA: 0x0004EEB0 File Offset: 0x0004D0B0
			private static List<MetadataPropertyCollection.ItemPropertyInfo> GetItemProperties(Type clrType)
			{
				List<MetadataPropertyCollection.ItemPropertyInfo> list = new List<MetadataPropertyCollection.ItemPropertyInfo>();
				foreach (PropertyInfo propertyInfo in clrType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					foreach (MetadataPropertyAttribute metadataPropertyAttribute in propertyInfo.GetCustomAttributes(typeof(MetadataPropertyAttribute), false))
					{
						list.Add(new MetadataPropertyCollection.ItemPropertyInfo(propertyInfo, metadataPropertyAttribute));
					}
				}
				return list;
			}

			// Token: 0x04000FD4 RID: 4052
			private readonly List<MetadataPropertyCollection.ItemPropertyInfo> _itemProperties;
		}

		// Token: 0x020002C1 RID: 705
		private class ItemPropertyInfo
		{
			// Token: 0x06001C73 RID: 7283 RVA: 0x0004EF1F File Offset: 0x0004D11F
			internal ItemPropertyInfo(PropertyInfo propertyInfo, MetadataPropertyAttribute attribute)
			{
				this._propertyInfo = propertyInfo;
				this._attribute = attribute;
			}

			// Token: 0x06001C74 RID: 7284 RVA: 0x0004EF35 File Offset: 0x0004D135
			internal MetadataProperty GetMetadataProperty(MetadataItem item)
			{
				return new MetadataProperty(this._propertyInfo.Name, this._attribute.Type, this._attribute.IsCollectionType, new MetadataPropertyValue(this._propertyInfo, item));
			}

			// Token: 0x04000FD5 RID: 4053
			private readonly MetadataPropertyAttribute _attribute;

			// Token: 0x04000FD6 RID: 4054
			private readonly PropertyInfo _propertyInfo;
		}
	}
}
