using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004DE RID: 1246
	internal sealed class MetadataPropertyCollection : MetadataCollection<MetadataProperty>
	{
		// Token: 0x06003DE5 RID: 15845 RVA: 0x000CD82C File Offset: 0x000CBA2C
		internal MetadataPropertyCollection(MetadataItem item)
			: base(MetadataPropertyCollection.GetSystemMetadataProperties(item))
		{
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x000CD83A File Offset: 0x000CBA3A
		private static IEnumerable<MetadataProperty> GetSystemMetadataProperties(MetadataItem item)
		{
			return MetadataPropertyCollection.GetItemTypeInformation(item.GetType()).GetItemAttributes(item);
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x000CD84D File Offset: 0x000CBA4D
		private static MetadataPropertyCollection.ItemTypeInformation GetItemTypeInformation(Type clrType)
		{
			return MetadataPropertyCollection._itemTypeMemoizer.Evaluate(clrType);
		}

		// Token: 0x04001515 RID: 5397
		private static readonly Memoizer<Type, MetadataPropertyCollection.ItemTypeInformation> _itemTypeMemoizer = new Memoizer<Type, MetadataPropertyCollection.ItemTypeInformation>((Type clrType) => new MetadataPropertyCollection.ItemTypeInformation(clrType), null);

		// Token: 0x02000AFA RID: 2810
		private class ItemTypeInformation
		{
			// Token: 0x060063EE RID: 25582 RVA: 0x0015A458 File Offset: 0x00158658
			internal ItemTypeInformation(Type clrType)
			{
				this._itemProperties = MetadataPropertyCollection.ItemTypeInformation.GetItemProperties(clrType);
			}

			// Token: 0x060063EF RID: 25583 RVA: 0x0015A46C File Offset: 0x0015866C
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

			// Token: 0x060063F0 RID: 25584 RVA: 0x0015A484 File Offset: 0x00158684
			private static List<MetadataPropertyCollection.ItemPropertyInfo> GetItemProperties(Type clrType)
			{
				List<MetadataPropertyCollection.ItemPropertyInfo> list = new List<MetadataPropertyCollection.ItemPropertyInfo>();
				foreach (PropertyInfo propertyInfo in clrType.GetInstanceProperties())
				{
					foreach (MetadataPropertyAttribute metadataPropertyAttribute in propertyInfo.GetCustomAttributes(false))
					{
						list.Add(new MetadataPropertyCollection.ItemPropertyInfo(propertyInfo, metadataPropertyAttribute));
					}
				}
				return list;
			}

			// Token: 0x04002C68 RID: 11368
			private readonly List<MetadataPropertyCollection.ItemPropertyInfo> _itemProperties;
		}

		// Token: 0x02000AFB RID: 2811
		private class ItemPropertyInfo
		{
			// Token: 0x060063F1 RID: 25585 RVA: 0x0015A518 File Offset: 0x00158718
			internal ItemPropertyInfo(PropertyInfo propertyInfo, MetadataPropertyAttribute attribute)
			{
				this._propertyInfo = propertyInfo;
				this._attribute = attribute;
			}

			// Token: 0x060063F2 RID: 25586 RVA: 0x0015A52E File Offset: 0x0015872E
			internal MetadataProperty GetMetadataProperty(MetadataItem item)
			{
				return new MetadataProperty(this._propertyInfo.Name, this._attribute.Type, this._attribute.IsCollectionType, new MetadataPropertyValue(this._propertyInfo, item));
			}

			// Token: 0x04002C69 RID: 11369
			private readonly MetadataPropertyAttribute _attribute;

			// Token: 0x04002C6A RID: 11370
			private readonly PropertyInfo _propertyInfo;
		}
	}
}
