using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BD9 RID: 3033
	internal static class ExchangeColumnInfoExtensions
	{
		// Token: 0x060052B9 RID: 21177 RVA: 0x00117A88 File Offset: 0x00115C88
		public static PropertyDefinitionBase[] GetPropertiesToFetch(this ExchangeColumnInfo[] columnInfos, HashSet<PropertyDefinitionBase> additionalProperties = null)
		{
			additionalProperties = additionalProperties ?? new HashSet<PropertyDefinitionBase>();
			return (from x in columnInfos
				where x.ColumnCategory == ColumnCategory.PrimitiveColumn || x.IsExpandedFromList
				select x.Property).Concat(additionalProperties).Distinct<PropertyDefinitionBase>().ToArray<PropertyDefinitionBase>();
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x00117AFC File Offset: 0x00115CFC
		public static SortOption[] GetTopLevelSortCollection(this ExchangeColumnInfo[] columnInfos)
		{
			return (from x in columnInfos
				where x.ColumnCategory == ColumnCategory.PrimitiveColumn && x.SortDirection != null
				select new SortOption(x.Property, x.SortDirection.Value)).ToArray<SortOption>();
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x00117B57 File Offset: 0x00115D57
		public static Keys GetKeys(this ExchangeColumnInfo[] columnInfos)
		{
			return Keys.New(columnInfos.Select((ExchangeColumnInfo x) => x.DisplayName).ToArray<string>());
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x00117B88 File Offset: 0x00115D88
		public static Value[] GetTypesFromColumnInfos(this ExchangeColumnInfo[] columnInfos)
		{
			return columnInfos.Select((ExchangeColumnInfo x) => RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				x.Type,
				LogicalValue.False
			})).ToArray<RecordValue>();
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x00117BC1 File Offset: 0x00115DC1
		public static TypeValue CreateTableTypeValue(this ExchangeColumnInfo[] columnInfos)
		{
			return TableTypeValue.New(columnInfos.CreateRecordTypeValue(), columnInfos.CreateTableKeys());
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x00117BD4 File Offset: 0x00115DD4
		private static IList<TableKey> CreateTableKeys(this ExchangeColumnInfo[] columnInfos)
		{
			List<TableKey> list = new List<TableKey>();
			for (int i = 0; i < columnInfos.Length; i++)
			{
				if (columnInfos[i].UniqueName == "Id")
				{
					list.Add(new TableKey(new int[] { i }, true));
				}
			}
			return list.ToArray();
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x00117C25 File Offset: 0x00115E25
		public static ListTypeValue CreateListTypeValue(this ExchangeColumnInfo columnInfos)
		{
			return ListTypeValue.New(columnInfos.Type);
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x00117C32 File Offset: 0x00115E32
		public static RecordTypeValue CreateRecordTypeValue(this ExchangeColumnInfo[] columnInfos)
		{
			return RecordTypeValue.New(RecordValue.New(columnInfos.GetKeys(), columnInfos.GetTypesFromColumnInfos()));
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x00117C4C File Offset: 0x00115E4C
		public static string GetName(this PropertyDefinitionBase property)
		{
			PropertyDefinition propertyDefinition = property as PropertyDefinition;
			if (propertyDefinition != null)
			{
				return propertyDefinition.Name;
			}
			ExtendedPropertyDefinition extendedPropertyDefinition = property as ExtendedPropertyDefinition;
			if (!(extendedPropertyDefinition != null))
			{
				return ((IndexedPropertyDefinition)property).Index;
			}
			if (extendedPropertyDefinition.Name != null)
			{
				return extendedPropertyDefinition.Name;
			}
			if (extendedPropertyDefinition.Id != null)
			{
				return SupportedExchangeTypes.PidLidToPropertyName[extendedPropertyDefinition.Id.Value];
			}
			return extendedPropertyDefinition.Tag.Value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x00117CDC File Offset: 0x00115EDC
		public static string[] GetSortedPropertyNames(this PropertyDefinitionBase[] properties)
		{
			string[] array = new string[(properties == null) ? 0 : properties.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = properties[i].GetName();
			}
			Array.Sort<string>(array);
			return array;
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x00117D18 File Offset: 0x00115F18
		public static string[] GetSortedSortOptionNames(this SortOption[] sortOptions)
		{
			string[] array = new string[(sortOptions == null) ? 0 : sortOptions.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = sortOptions[i].GetSerializedString();
			}
			Array.Sort<string>(array);
			return array;
		}
	}
}
