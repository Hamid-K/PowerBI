using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2010;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000078 RID: 120
	internal sealed class ItemSearchConditions : Dictionary<string, ItemSearchCondition>
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x0001425C File Offset: 0x0001245C
		internal ItemSearchConditions(Microsoft.ReportingServices.Library.Soap2010.SearchCondition[] conditions, ItemSearchOptions options)
		{
			if (conditions == null || conditions.Length == 0)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < conditions.Length; i++)
			{
				Microsoft.ReportingServices.Library.Soap2010.SearchCondition searchCondition = conditions[i];
				if (searchCondition == null)
				{
					throw new MissingElementException("SearchCondition");
				}
				string name = searchCondition.Name;
				List<string> values = searchCondition.Values;
				string text = null;
				if (name == null)
				{
					throw new MissingElementException("Name");
				}
				text = (string)ItemSearchConditions.SearchableProperties[name];
				if (StringComparer.OrdinalIgnoreCase.Compare(name, "All") == 0)
				{
					text = "All";
				}
				if (text == null)
				{
					throw new InvalidElementException(name);
				}
				if (base.ContainsKey(text))
				{
					throw new InvalidElementCombinationException(name, name);
				}
				if (text == "All")
				{
					if (options.CompatLevel < ServerCompatLevel.Soap2010)
					{
						throw new InvalidElementException(name);
					}
					flag = true;
					foreach (string text2 in ItemSearchConditions.m_textPropertyNames)
					{
						if (base.ContainsKey(text2))
						{
							throw new InvalidElementCombinationException(text2, "All");
						}
					}
				}
				else if (flag)
				{
					foreach (string text3 in ItemSearchConditions.m_textPropertyNames)
					{
						if (text == text3)
						{
							throw new InvalidElementCombinationException("All", name);
						}
					}
				}
				if (values == null)
				{
					throw new InvalidElementException(name);
				}
				using (List<string>.Enumerator enumerator = values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							throw new InvalidElementException(name);
						}
					}
				}
				ItemSearchCondition itemSearchCondition = new ItemSearchCondition();
				itemSearchCondition.Condition = (searchCondition.ConditionSpecified ? searchCondition.Condition : ((text != "Type") ? Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.Contains : Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.Equals));
				if (options.ComponentLookup && !ItemSearchConditions.IsFieldValidForComponentLookup(text))
				{
					throw new InvalidElementCombinationException("ComponentLookup", text);
				}
				if (!ItemSearchConditions.IsConditionSupported(itemSearchCondition.Condition, text, options))
				{
					throw new InvalidElementCombinationException(name, itemSearchCondition.Condition.ToString());
				}
				itemSearchCondition.Values = values;
				if (itemSearchCondition.Condition == Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.In || searchCondition.Condition == Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.Between)
				{
					RSTrace.CatalogTrace.Assert(options.CompatLevel >= ServerCompatLevel.Soap2010, "condition compatLevel");
					if (itemSearchCondition.Condition == Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.Between)
					{
						if (itemSearchCondition.Values.Count != 2)
						{
							throw new InvalidElementException(name);
						}
					}
					else if (itemSearchCondition.Values.Count == 0)
					{
						throw new InvalidElementException(name);
					}
				}
				else if (itemSearchCondition.Values.Count != 1)
				{
					throw new InvalidElementException(name);
				}
				using (IEnumerator<string> enumerator2 = itemSearchCondition.Values.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.Length > 128)
						{
							throw new InvalidElementException(name);
						}
					}
				}
				if (text == "Type")
				{
					for (int k = 0; k < itemSearchCondition.Values.Count; k++)
					{
						ItemType itemType = ItemSearchConditions.ParseItemType(itemSearchCondition.Values[k]);
						if (itemType == ItemType.Unknown)
						{
							throw new InvalidElementException(name);
						}
						if (itemType == ItemType.RdlxReport)
						{
							throw new OperationNotSupportedNativeModeException();
						}
						if (options.ComponentLookup && itemType != ItemType.Component)
						{
							throw new InvalidElementCombinationException(name, "ComponentLookup");
						}
						IList<string> values2 = itemSearchCondition.Values;
						int num = k;
						int j = (int)itemType;
						values2[num] = j.ToString(CultureInfo.InvariantCulture);
					}
				}
				if (text == "ComponentID")
				{
					itemSearchCondition.Domain = ComparisonDomain.UniqueIdentifier;
				}
				else
				{
					itemSearchCondition.Domain = ComparisonDomain.String;
				}
				if (options.CompatLevel >= ServerCompatLevel.Soap2010 && ItemSearchConditions.DateTimeProperties.Contains(text))
				{
					itemSearchCondition.Domain = ComparisonDomain.DateTime;
					List<DateTimeOffset> list = new List<DateTimeOffset>(itemSearchCondition.Values.Count);
					list.AddRange(Microsoft.ReportingServices.Common.EnumeratorMapping.Map<string, DateTimeOffset>(itemSearchCondition.Values, delegate(string unparsedDateTime)
					{
						DateTimeOffset dateTimeOffset;
						bool flag2;
						if (!Microsoft.ReportingServices.Common.DateTimeUtil.TryParseDateTime(unparsedDateTime, options.Language, out dateTimeOffset, out flag2))
						{
							throw new InvalidElementException(name);
						}
						if (dateTimeOffset.LocalDateTime < SqlDateTime.MinValue.Value)
						{
							dateTimeOffset = new DateTimeOffset(SqlDateTime.MinValue.Value);
						}
						else if (dateTimeOffset.LocalDateTime > SqlDateTime.MaxValue.Value)
						{
							dateTimeOffset = new DateTimeOffset(SqlDateTime.MaxValue.Value);
						}
						return dateTimeOffset;
					}));
					itemSearchCondition.DateTimeValues = list;
				}
				base[text] = itemSearchCondition;
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001471C File Offset: 0x0001291C
		public void ThrowIfNotValidComponentLookup(string folderPath, BooleanOperatorEnum operation)
		{
			if (string.CompareOrdinal(folderPath, "/") != 0)
			{
				throw new InvalidElementCombinationException("ComponentLookup", "Folder");
			}
			if (operation != BooleanOperatorEnum.And)
			{
				throw new InvalidElementCombinationException("ComponentLookup", "BooleanOperator");
			}
			foreach (string text in this.m_requiredComponentLookupProperties)
			{
				if (!base.ContainsKey(text))
				{
					throw new MissingElementException(text);
				}
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00014782 File Offset: 0x00012982
		private static bool IsFieldValidForComponentLookup(string normalizedName)
		{
			return normalizedName == "ComponentID" || normalizedName == "Type";
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000147A0 File Offset: 0x000129A0
		private static bool IsConditionSupported(Microsoft.ReportingServices.Library.Soap2010.ConditionEnum condition, string normalizedName, ItemSearchOptions searchOptions)
		{
			switch (condition)
			{
			case Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.Contains:
				return (searchOptions.CompatLevel < ServerCompatLevel.Soap2010 || !ItemSearchConditions.DateTimeProperties.Contains(normalizedName)) && (!(normalizedName == "Type") && !(normalizedName == "Subtype")) && !(normalizedName == "ComponentID");
			case Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.In:
				return ItemSearchConditions.SearchableProperties.Contains(normalizedName);
			case Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.Between:
				return normalizedName == "CreationDate" || normalizedName == "ModifiedDate";
			}
			return true;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00014830 File Offset: 0x00012A30
		private static ItemType ParseItemType(string value)
		{
			if (ItemSearchConditions.ItemTypeNames == null)
			{
				string[] names = Enum.GetNames(typeof(ItemType));
				ItemType[] array = (ItemType[])Enum.GetValues(typeof(ItemType));
				ItemSearchConditions.ItemTypeNames = new Dictionary<string, ItemType>(array.Length, StringComparer.OrdinalIgnoreCase);
				for (int i = 0; i < array.Length; i++)
				{
					ItemSearchConditions.ItemTypeNames[names[i]] = array[i];
				}
			}
			ItemType itemType;
			if (ItemSearchConditions.ItemTypeNames.TryGetValue(value, out itemType))
			{
				return itemType;
			}
			return ItemType.Unknown;
		}

		// Token: 0x0400024F RID: 591
		private readonly string[] m_requiredComponentLookupProperties = new string[] { "ComponentID", "Type" };

		// Token: 0x04000250 RID: 592
		private static readonly string[] m_SearchablePropertyNames = new string[] { "Name", "Type", "Description", "CreatedBy", "CreationDate", "ModifiedBy", "ModifiedDate", "Subtype", "ComponentID" };

		// Token: 0x04000251 RID: 593
		private static readonly Hashtable SearchableProperties = PropertyHashTable.Create(ItemSearchConditions.m_SearchablePropertyNames);

		// Token: 0x04000252 RID: 594
		private static readonly string[] m_textPropertyNames = new string[] { "Name", "Description" };

		// Token: 0x04000253 RID: 595
		private static Dictionary<string, ItemType> ItemTypeNames = null;

		// Token: 0x04000254 RID: 596
		private static readonly string[] m_dateTimePropertyNames = new string[] { "CreationDate", "ModifiedDate" };

		// Token: 0x04000255 RID: 597
		private static readonly Hashtable DateTimeProperties = PropertyHashTable.Create(ItemSearchConditions.m_dateTimePropertyNames);

		// Token: 0x04000256 RID: 598
		internal const string AllTextSearch = "All";
	}
}
