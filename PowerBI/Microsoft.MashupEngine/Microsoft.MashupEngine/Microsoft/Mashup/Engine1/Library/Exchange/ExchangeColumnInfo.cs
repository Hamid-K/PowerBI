using System;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BD7 RID: 3031
	internal class ExchangeColumnInfo
	{
		// Token: 0x060052A1 RID: 21153 RVA: 0x001177CD File Offset: 0x001159CD
		private ExchangeColumnInfo(string name, TypeValue type)
		{
			this.displayName = name;
			this.name = name;
			this.type = type;
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x001177EC File Offset: 0x001159EC
		private ExchangeColumnInfo(string displayName, string name, bool isFoldable, bool isExpanded, PropertyDefinitionBase property, ExchangeColumnInfo[] subColumns, Func<object, object> fieldSelector, Func<object, Value> marshaller, TypeValue type, ColumnCategory columnCategory, SortDirection? sortDirection)
		{
			this.displayName = displayName;
			this.name = name;
			this.isFoldable = isFoldable;
			this.isExpandedFromList = isExpanded;
			this.property = property;
			this.subColumns = subColumns;
			this.fieldSelector = fieldSelector;
			this.marshaller = marshaller;
			this.type = type;
			this.columnCategory = columnCategory;
			this.sortDirection = sortDirection;
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x00117854 File Offset: 0x00115A54
		private ExchangeColumnInfo(string name, bool isFoldable, ColumnCategory category, TypeValue type, Func<object, object> fieldSelector, Func<object, Value> marshaller)
			: this(name, type)
		{
			this.columnCategory = category;
			this.isFoldable = isFoldable;
			this.fieldSelector = fieldSelector;
			this.marshaller = marshaller;
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x00117880 File Offset: 0x00115A80
		private ExchangeColumnInfo(string name, PropertyDefinitionBase property, bool isFoldable, bool isExpanded, TypeValue type, Func<object, object> fieldSelector, Func<object, Value> marshaller)
			: this(name, type)
		{
			this.columnCategory = ColumnCategory.PrimitiveColumn;
			this.isFoldable = isFoldable;
			this.isExpandedFromList = isExpanded;
			this.property = property;
			this.subColumns = null;
			this.fieldSelector = fieldSelector;
			this.marshaller = marshaller;
			this.sortDirection = null;
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x001178D6 File Offset: 0x00115AD6
		private ExchangeColumnInfo(string name, TypeValue type, PropertyDefinitionBase property, ColumnCategory category, ExchangeColumnInfo[] subColumns)
			: this(name, type)
		{
			this.columnCategory = category;
			this.property = property;
			this.subColumns = subColumns;
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x001178F7 File Offset: 0x00115AF7
		private ExchangeColumnInfo(string name, bool isExpanded, TypeValue type, PropertyDefinitionBase property, ExchangeColumnInfo[] subColumns)
			: this(name, type)
		{
			this.isExpandedFromList = isExpanded;
			this.subColumns = subColumns;
			this.property = property;
			this.columnCategory = ColumnCategory.RecordColumn;
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x0011791F File Offset: 0x00115B1F
		public static ExchangeColumnInfo NewCustomColumnInfo(string name, bool isFoldable, ColumnCategory category, TypeValue type, Func<object, object> fieldSelector, Func<object, Value> marshaller)
		{
			return new ExchangeColumnInfo(name, isFoldable, category, type, fieldSelector, marshaller);
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x0011792E File Offset: 0x00115B2E
		public static ExchangeColumnInfo NewTableOrListColumnInfo(string name, TypeValue type, PropertyDefinitionBase property, ColumnCategory category, ExchangeColumnInfo[] subColumns)
		{
			return new ExchangeColumnInfo(name, type, property, category, subColumns);
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x0011793B File Offset: 0x00115B3B
		public static ExchangeColumnInfo NewPrimitiveColumnInfo(string name, PropertyDefinitionBase property, bool isFoldable, bool isExpanded, TypeValue type, Func<object, object> fieldSelector, Func<object, Value> marshaller)
		{
			return new ExchangeColumnInfo(name, property, isFoldable, isExpanded, type, fieldSelector, marshaller);
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x0011794C File Offset: 0x00115B4C
		public static ExchangeColumnInfo NewRecordColumnInfo(string name, bool isExpandedFromList, TypeValue type, PropertyDefinitionBase property, ExchangeColumnInfo[] subColumns)
		{
			return new ExchangeColumnInfo(name, isExpandedFromList, type, property, subColumns);
		}

		// Token: 0x17001979 RID: 6521
		// (get) Token: 0x060052AB RID: 21163 RVA: 0x00117959 File Offset: 0x00115B59
		public bool IsFoldable
		{
			get
			{
				return this.isFoldable;
			}
		}

		// Token: 0x1700197A RID: 6522
		// (get) Token: 0x060052AC RID: 21164 RVA: 0x00117961 File Offset: 0x00115B61
		public bool IsExpandedFromList
		{
			get
			{
				return this.isExpandedFromList;
			}
		}

		// Token: 0x1700197B RID: 6523
		// (get) Token: 0x060052AD RID: 21165 RVA: 0x00117969 File Offset: 0x00115B69
		public ColumnCategory ColumnCategory
		{
			get
			{
				return this.columnCategory;
			}
		}

		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x060052AE RID: 21166 RVA: 0x00117971 File Offset: 0x00115B71
		public TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x060052AF RID: 21167 RVA: 0x00117979 File Offset: 0x00115B79
		public PropertyDefinitionBase Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x060052B0 RID: 21168 RVA: 0x00117981 File Offset: 0x00115B81
		public SortDirection? SortDirection
		{
			get
			{
				return this.sortDirection;
			}
		}

		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x060052B1 RID: 21169 RVA: 0x00117989 File Offset: 0x00115B89
		public string UniqueName
		{
			get
			{
				if (this.columnCategory == ColumnCategory.PrimitiveColumn && this.fieldSelector != null)
				{
					return ExchangeColumnInfo.GetUniqueName(this.property.GetName(), this.name);
				}
				return this.name;
			}
		}

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x060052B2 RID: 21170 RVA: 0x001179B8 File Offset: 0x00115BB8
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x060052B3 RID: 21171 RVA: 0x001179C0 File Offset: 0x00115BC0
		public ExchangeColumnInfo[] SubColumns
		{
			get
			{
				return this.subColumns;
			}
		}

		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x060052B4 RID: 21172 RVA: 0x001179C8 File Offset: 0x00115BC8
		public Func<object, object> FieldSelector
		{
			get
			{
				return this.fieldSelector;
			}
		}

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x060052B5 RID: 21173 RVA: 0x001179D0 File Offset: 0x00115BD0
		public Func<object, Value> Marshal
		{
			get
			{
				return this.marshaller;
			}
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x001179D8 File Offset: 0x00115BD8
		public static string GetUniqueName(string propertyName, string fieldName)
		{
			return propertyName + "-" + fieldName;
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x001179E8 File Offset: 0x00115BE8
		public static ExchangeColumnInfo New(ExchangeColumnInfo columnInfo, string newDisplayName)
		{
			return new ExchangeColumnInfo(newDisplayName, columnInfo.name, columnInfo.isFoldable, columnInfo.isExpandedFromList, columnInfo.property, columnInfo.subColumns, columnInfo.fieldSelector, columnInfo.marshaller, columnInfo.type, columnInfo.columnCategory, columnInfo.sortDirection);
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x00117A38 File Offset: 0x00115C38
		public static ExchangeColumnInfo New(ExchangeColumnInfo columnInfo, SortDirection? newSortDirection)
		{
			return new ExchangeColumnInfo(columnInfo.displayName, columnInfo.name, columnInfo.isFoldable, columnInfo.isExpandedFromList, columnInfo.property, columnInfo.subColumns, columnInfo.fieldSelector, columnInfo.marshaller, columnInfo.type, columnInfo.columnCategory, newSortDirection);
		}

		// Token: 0x04002D96 RID: 11670
		private string displayName;

		// Token: 0x04002D97 RID: 11671
		private string name;

		// Token: 0x04002D98 RID: 11672
		private bool isFoldable;

		// Token: 0x04002D99 RID: 11673
		private bool isExpandedFromList;

		// Token: 0x04002D9A RID: 11674
		private PropertyDefinitionBase property;

		// Token: 0x04002D9B RID: 11675
		private ExchangeColumnInfo[] subColumns;

		// Token: 0x04002D9C RID: 11676
		private Func<object, object> fieldSelector;

		// Token: 0x04002D9D RID: 11677
		private Func<object, Value> marshaller;

		// Token: 0x04002D9E RID: 11678
		private TypeValue type;

		// Token: 0x04002D9F RID: 11679
		private ColumnCategory columnCategory;

		// Token: 0x04002DA0 RID: 11680
		private SortDirection? sortDirection;
	}
}
