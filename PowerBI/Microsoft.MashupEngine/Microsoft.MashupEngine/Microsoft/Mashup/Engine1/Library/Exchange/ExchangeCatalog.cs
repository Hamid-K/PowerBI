using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BD0 RID: 3024
	internal abstract class ExchangeCatalog
	{
		// Token: 0x06005275 RID: 21109 RVA: 0x00116C3A File Offset: 0x00114E3A
		protected ExchangeCatalog(ExchangeVersion exchangeVersion, Type schemaType, string itemClass, string folderClass)
		{
			this.exchangeVersion = exchangeVersion;
			this.itemClass = itemClass;
			this.schemaType = schemaType;
			this.folderClass = folderClass;
		}

		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x06005276 RID: 21110
		protected abstract PropertyDefinitionBase[] TopLevelPropertiesAllowedList { get; }

		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x06005277 RID: 21111 RVA: 0x00116C5F File Offset: 0x00114E5F
		public virtual SearchFilter BaseFolderSearchFilter
		{
			get
			{
				if (this.folderClass != null)
				{
					return new SearchFilter.IsEqualTo(FolderSchema.FolderClass, this.folderClass);
				}
				return null;
			}
		}

		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x06005278 RID: 21112 RVA: 0x00116C7B File Offset: 0x00114E7B
		public virtual SearchFilter BaseItemSearchFilter
		{
			get
			{
				return new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter[]
				{
					new SearchFilter.IsEqualTo(ItemSchema.ItemClass, this.itemClass),
					new SearchFilter.ContainsSubstring(ItemSchema.ItemClass, this.itemClass + ".", ContainmentMode.Prefixed, ComparisonMode.IgnoreCase)
				});
			}
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x00116CBB File Offset: 0x00114EBB
		public static ExchangeColumnInfo GetLoadedExchangeColumnInfo(PropertyDefinitionBase property)
		{
			return ExchangeCatalog.LoadedExchangeColumnInfos[property];
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x00116CC8 File Offset: 0x00114EC8
		private static ExchangeColumnInfo[] GetExtendedProperties(ExchangeVersion version)
		{
			ExchangeColumnInfo[] array;
			if (!ExchangeCatalog.extendedProperties.TryGetValue(version, out array))
			{
				List<ExchangeColumnInfo> list = new List<ExchangeColumnInfo>();
				foreach (ExtendedPropertyDefinition extendedPropertyDefinition in SupportedExchangeTypes.ExtendedPropertiesWithNumericKeys)
				{
					if (extendedPropertyDefinition.Version <= version)
					{
						ExchangeCatalog.AddToExtendedProperties(extendedPropertyDefinition, list, SupportedExchangeTypes.GetPropertyNameFromPid(extendedPropertyDefinition.Id.Value));
					}
				}
				foreach (ExtendedPropertyDefinition extendedPropertyDefinition2 in SupportedExchangeTypes.ExtendedPropertiesWithStringKeys)
				{
					if (extendedPropertyDefinition2.Version <= version)
					{
						ExchangeCatalog.AddToExtendedProperties(extendedPropertyDefinition2, list, extendedPropertyDefinition2.Name);
					}
				}
				array = list.ToArray();
				ExchangeCatalog.extendedProperties[version] = array;
			}
			return array;
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x00116D74 File Offset: 0x00114F74
		private static void AddToExtendedProperties(ExtendedPropertyDefinition propertyDefinition, List<ExchangeColumnInfo> extendedPropertiesList, string name)
		{
			TypeValue typeValue = SupportedExchangeTypes.MapiToTypeValue[propertyDefinition.MapiType];
			ExchangeColumnInfo exchangeColumnInfo;
			if (typeValue.TypeKind == ValueKind.List)
			{
				Type type = ExchangeHelper.GetEnumerableType(propertyDefinition.Type);
				if (type == null || type == typeof(byte) || type == typeof(char))
				{
					type = propertyDefinition.Type;
				}
				if (!ExchangeCatalog.TryGetCollectionColumnInfo(propertyDefinition, type, out exchangeColumnInfo))
				{
					throw new InvalidOperationException("Collection column info is not defined for property: " + propertyDefinition.ToString() + ".");
				}
				extendedPropertiesList.Add(exchangeColumnInfo);
			}
			else
			{
				exchangeColumnInfo = ExchangeColumnInfo.NewPrimitiveColumnInfo(name, propertyDefinition, true, false, typeValue, null, new Func<object, Value>(ValueMarshaller.MarshalFromClr));
				extendedPropertiesList.Add(exchangeColumnInfo);
			}
			if (!ExchangeCatalog.LoadedExchangeColumnInfos.Keys.Contains(propertyDefinition))
			{
				ExchangeCatalog.LoadedExchangeColumnInfos.Add(propertyDefinition, exchangeColumnInfo);
			}
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x00116E48 File Offset: 0x00115048
		private PropertyDefinitionBase[] GetTopLevelPropertiesAllowedListWithProperVersion()
		{
			if (this.topLevelPropertiesAllowedListWithProperVersion == null)
			{
				this.topLevelPropertiesAllowedListWithProperVersion = this.TopLevelPropertiesAllowedList.Where((PropertyDefinitionBase x) => x.Version <= this.exchangeVersion).ToArray<PropertyDefinitionBase>();
			}
			return this.topLevelPropertiesAllowedListWithProperVersion;
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x00116E7C File Offset: 0x0011507C
		private void FillDefaultColumnInfos(Type schemaType, bool enableFolding, ExchangeCatalog.SchemaColumnInfos schemaColumnInfos)
		{
			FieldInfo[] fields = schemaType.GetFields(BindingFlags.Static | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			{
				PropertyDefinition propertyDefinition = fields[i].GetValue(null) as PropertyDefinition;
				if (propertyDefinition != null && propertyDefinition.Version <= this.exchangeVersion)
				{
					ExchangeColumnInfo exchangeColumnInfo;
					if (propertyDefinition == ItemSchema.Attachments)
					{
						exchangeColumnInfo = ExchangeColumnInfo.NewTableOrListColumnInfo(ItemSchema.Attachments.Name, ExchangeAttachmentTableValue.AttachmentTableType, ItemSchema.Attachments, ColumnCategory.AttachmentTableColumn, null);
					}
					else if (!ExchangeCatalog.TryGetRecordColumnInfo(propertyDefinition.Type, propertyDefinition, enableFolding, false, out exchangeColumnInfo) && !ExchangeCatalog.TryGetPrimitiveColumnInfo(propertyDefinition.GetName(), propertyDefinition.Type, enableFolding, false, propertyDefinition, null, out exchangeColumnInfo) && !ExchangeCatalog.TryGetCollectionColumnInfo(propertyDefinition, ExchangeHelper.GetEnumerableType(propertyDefinition.Type), out exchangeColumnInfo))
					{
						goto IL_00BE;
					}
					if (!ExchangeCatalog.LoadedExchangeColumnInfos.ContainsKey(propertyDefinition))
					{
						ExchangeCatalog.LoadedExchangeColumnInfos.Add(propertyDefinition, exchangeColumnInfo);
					}
					this.FillSchemaColumnInfos(schemaColumnInfos, propertyDefinition, exchangeColumnInfo);
				}
				IL_00BE:;
			}
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x00116F54 File Offset: 0x00115154
		private void FillSchemaColumnInfos(ExchangeCatalog.SchemaColumnInfos schemaColumnInfos, PropertyDefinitionBase property, ExchangeColumnInfo columnInfo)
		{
			int num = Array.IndexOf<PropertyDefinitionBase>(this.GetTopLevelPropertiesAllowedListWithProperVersion(), property);
			if (num != -1)
			{
				schemaColumnInfos.ToplevelColumnInfo[num] = columnInfo;
				return;
			}
			schemaColumnInfos.NestedColumnInfo.Add(columnInfo);
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x00116F88 File Offset: 0x00115188
		protected static bool TryGetPrimitiveColumnInfo(string name, Type type, bool enableFolding, bool isExpandedFromList, PropertyDefinitionBase property, Func<object, object> fieldSelector, out ExchangeColumnInfo columnInfo)
		{
			MarshalInfo marshalInfo;
			if (SupportedExchangeTypes.TryGetPrimitiveType(type, out marshalInfo))
			{
				bool flag = enableFolding && !ExchangeFoldingAllowedLists.NonFoldableTypes.Contains(type) && ExchangeFoldingAllowedLists.FoldableProperties.Contains(property);
				columnInfo = ExchangeColumnInfo.NewPrimitiveColumnInfo(name, property, flag, isExpandedFromList, marshalInfo.TypeValue, fieldSelector, marshalInfo.Marshal);
				return true;
			}
			columnInfo = null;
			return false;
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x00116FE4 File Offset: 0x001151E4
		private static bool TryGetRecordColumnInfo(Type type, PropertyDefinitionBase property, bool allowFolding, bool isExpandedFromList, out ExchangeColumnInfo columnInfo)
		{
			FieldSelectorInfo[] array;
			if (SupportedExchangeTypes.TryGetComplexType(type, out array))
			{
				ExchangeColumnInfo[] array2 = new ExchangeColumnInfo[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					ExchangeColumnInfo exchangeColumnInfo;
					if (ExchangeCatalog.TryGetPrimitiveColumnInfo(array[i].FieldName, array[i].FieldType, allowFolding && array[i].Foldable, isExpandedFromList, property, array[i].FieldSelector, out exchangeColumnInfo))
					{
						array2[i] = exchangeColumnInfo;
					}
				}
				columnInfo = ExchangeColumnInfo.NewRecordColumnInfo(property.GetName(), isExpandedFromList, array2.CreateRecordTypeValue(), property, array2);
				return true;
			}
			columnInfo = null;
			return false;
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x00117068 File Offset: 0x00115268
		protected static bool TryGetCollectionColumnInfo(PropertyDefinitionBase property, Type itemType, out ExchangeColumnInfo columnInfo)
		{
			if (itemType != null)
			{
				ExchangeColumnInfo exchangeColumnInfo;
				if (ExchangeCatalog.TryGetPrimitiveColumnInfo(property.GetName(), itemType, false, true, property, null, out exchangeColumnInfo))
				{
					ExchangeColumnInfo[] array = new ExchangeColumnInfo[] { exchangeColumnInfo };
					columnInfo = ExchangeColumnInfo.NewTableOrListColumnInfo(property.GetName(), exchangeColumnInfo.CreateListTypeValue(), property, ColumnCategory.ListColumn, array);
					return true;
				}
				if (ExchangeCatalog.TryGetRecordColumnInfo(itemType, property, false, true, out exchangeColumnInfo))
				{
					ExchangeColumnInfo[] array2 = new ExchangeColumnInfo[] { exchangeColumnInfo };
					columnInfo = ExchangeColumnInfo.NewTableOrListColumnInfo(property.GetName(), exchangeColumnInfo.SubColumns.CreateTableTypeValue(), property, ColumnCategory.TableColumn, array2);
					return true;
				}
			}
			columnInfo = null;
			return false;
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x001170EE File Offset: 0x001152EE
		protected static ExchangeColumnInfo GetFolderPathColumnInfo(bool enableFolding)
		{
			return ExchangeColumnInfo.NewCustomColumnInfo("Folder Path", enableFolding, ColumnCategory.FolderPath, TypeValue.Text.Nullable, null, (object o) => TextValue.NewOrNull((string)o));
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x00117128 File Offset: 0x00115328
		protected static ExchangeColumnInfo GetBodyColumnInfo()
		{
			ExchangeColumnInfo exchangeColumnInfo = ExchangeColumnInfo.NewPrimitiveColumnInfo("TextBody", ItemSchema.Body, true, false, TypeValue.Text.Nullable, (object x) => ((MessageBody)x).Text, (object x) => TextValue.NewOrNull((string)x));
			ExchangeColumnInfo exchangeColumnInfo2 = ExchangeColumnInfo.NewPrimitiveColumnInfo("HtmlBody", ExchangeHelper.PR_Html_Body, false, false, TypeValue.Text.Nullable, (object x) => Encoding.UTF8.GetString((byte[])x), (object x) => TextValue.NewOrNull((string)x));
			if (!ExchangeCatalog.LoadedExchangeColumnInfos.ContainsKey(ItemSchema.Body))
			{
				ExchangeCatalog.LoadedExchangeColumnInfos.Add(ItemSchema.Body, exchangeColumnInfo);
			}
			if (!ExchangeCatalog.LoadedExchangeColumnInfos.ContainsKey(ExchangeHelper.PR_Html_Body))
			{
				ExchangeCatalog.LoadedExchangeColumnInfos.Add(ExchangeHelper.PR_Html_Body, exchangeColumnInfo2);
			}
			ExchangeColumnInfo[] array = new ExchangeColumnInfo[] { exchangeColumnInfo, exchangeColumnInfo2 };
			return ExchangeColumnInfo.NewRecordColumnInfo("Body", false, array.CreateRecordTypeValue(), null, array);
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x0011724D File Offset: 0x0011544D
		protected static ExchangeColumnInfo GetIdColumnInfos()
		{
			return ExchangeColumnInfo.NewPrimitiveColumnInfo("Id", ItemSchema.Id, false, false, TypeValue.Text, null, (object o) => TextValue.New((string)o));
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x00117288 File Offset: 0x00115488
		public ExchangeColumnInfo[] GetAllColumnInfos(bool enableFolding)
		{
			ExchangeColumnInfo folderPathColumnInfo = ExchangeCatalog.GetFolderPathColumnInfo(enableFolding);
			ExchangeCatalog.SchemaColumnInfos schemaColumnInfos = new ExchangeCatalog.SchemaColumnInfos(this.GetTopLevelPropertiesAllowedListWithProperVersion().Length);
			this.FillDefaultColumnInfos(this.schemaType, enableFolding, schemaColumnInfos);
			this.FillDefaultColumnInfos(typeof(ItemSchema), enableFolding, schemaColumnInfos);
			ExchangeColumnInfo[] array = ExchangeCatalog.GetExtendedProperties(this.exchangeVersion);
			schemaColumnInfos.NestedColumnInfo.Add(ExchangeColumnInfo.NewRecordColumnInfo("ExtendedProperties", false, array.CreateRecordTypeValue(), null, array));
			ExchangeColumnInfo[] schemaStartupColumnInfos = schemaColumnInfos.GetSchemaStartupColumnInfos();
			List<ExchangeColumnInfo> list = new List<ExchangeColumnInfo>(schemaStartupColumnInfos.Length + 2);
			list.Add(folderPathColumnInfo);
			list.AddRange(schemaStartupColumnInfos);
			list.Add(ExchangeCatalog.GetBodyColumnInfo());
			list.Add(ExchangeCatalog.GetIdColumnInfos());
			return list.ToArray();
		}

		// Token: 0x04002D70 RID: 11632
		private static readonly Dictionary<PropertyDefinitionBase, ExchangeColumnInfo> LoadedExchangeColumnInfos = new Dictionary<PropertyDefinitionBase, ExchangeColumnInfo>();

		// Token: 0x04002D71 RID: 11633
		public const string EmailSubjectKey = "Subject";

		// Token: 0x04002D72 RID: 11634
		public const string EmailNameKey = "Name";

		// Token: 0x04002D73 RID: 11635
		public const string EmailAddressKey = "Address";

		// Token: 0x04002D74 RID: 11636
		public const string ExtendedPropertiesKey = "ExtendedProperties";

		// Token: 0x04002D75 RID: 11637
		public const string IdKey = "Id";

		// Token: 0x04002D76 RID: 11638
		public const string FolderPathKey = "Folder Path";

		// Token: 0x04002D77 RID: 11639
		public const string AttributesKey = "Attributes";

		// Token: 0x04002D78 RID: 11640
		public const string BodyKey = "Body";

		// Token: 0x04002D79 RID: 11641
		protected PropertyDefinitionBase[] topLevelPropertiesAllowedList;

		// Token: 0x04002D7A RID: 11642
		protected PropertyDefinitionBase[] topLevelPropertiesAllowedListWithProperVersion;

		// Token: 0x04002D7B RID: 11643
		private static Dictionary<ExchangeVersion, ExchangeColumnInfo[]> extendedProperties = new Dictionary<ExchangeVersion, ExchangeColumnInfo[]>();

		// Token: 0x04002D7C RID: 11644
		private readonly ExchangeVersion exchangeVersion;

		// Token: 0x04002D7D RID: 11645
		private readonly string itemClass;

		// Token: 0x04002D7E RID: 11646
		private readonly string folderClass;

		// Token: 0x04002D7F RID: 11647
		private readonly Type schemaType;

		// Token: 0x02000BD1 RID: 3025
		private class SchemaColumnInfos
		{
			// Token: 0x06005288 RID: 21128 RVA: 0x00117357 File Offset: 0x00115557
			public SchemaColumnInfos(int topLevelColumnInfoCount)
			{
				this.toplevelColumnInfo = new ExchangeColumnInfo[topLevelColumnInfoCount + 1];
				this.nestedColumnInfo = new List<ExchangeColumnInfo>();
			}

			// Token: 0x17001974 RID: 6516
			// (get) Token: 0x06005289 RID: 21129 RVA: 0x00117378 File Offset: 0x00115578
			public ExchangeColumnInfo[] ToplevelColumnInfo
			{
				get
				{
					return this.toplevelColumnInfo;
				}
			}

			// Token: 0x17001975 RID: 6517
			// (get) Token: 0x0600528A RID: 21130 RVA: 0x00117380 File Offset: 0x00115580
			public List<ExchangeColumnInfo> NestedColumnInfo
			{
				get
				{
					return this.nestedColumnInfo;
				}
			}

			// Token: 0x0600528B RID: 21131 RVA: 0x00117388 File Offset: 0x00115588
			public ExchangeColumnInfo[] GetSchemaStartupColumnInfos()
			{
				this.ToplevelColumnInfo[this.ToplevelColumnInfo.Length - 1] = ExchangeColumnInfo.NewRecordColumnInfo("Attributes", false, this.NestedColumnInfo.ToArray().CreateRecordTypeValue(), null, this.NestedColumnInfo.OrderBy((ExchangeColumnInfo x) => x.DisplayName).ToArray<ExchangeColumnInfo>());
				return this.ToplevelColumnInfo;
			}

			// Token: 0x04002D80 RID: 11648
			private ExchangeColumnInfo[] toplevelColumnInfo;

			// Token: 0x04002D81 RID: 11649
			private List<ExchangeColumnInfo> nestedColumnInfo;
		}
	}
}
