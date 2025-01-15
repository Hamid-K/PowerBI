using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000020 RID: 32
	[ImmutableObject(true)]
	public sealed class ConceptualEntityStatistics
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000025F4 File Offset: 0x000007F4
		internal ConceptualEntityStatistics(int propertyCount, int measureCount, int kpiCount, int hierarchyCount, int hiddenPropertyCount, int orderByPropertyCount, bool isDateTable, bool isHidden, bool hasDefaultLabel, ConceptualPrimitiveType defaultLabelDataType, ReadOnlyCollection<ConceptualPrimitiveType> rowIdentifierDataTypes, int keepUniqueRowsColumnsCount, bool hasDefaultImage, ConceptualPrimitiveType defaultImageDataType, ReadOnlyCollection<ConceptualPrimitiveType> defaultFieldSetDataTypes, IDictionary<ConceptualPrimitiveType, int> propertyCountByDataType, IDictionary<ConceptualDataCategory, int> propertyCountByDataCategory, IDictionary<ConceptualDefaultAggregate, int> propertyCountByDefaultAggregate)
		{
			this.ColumnCount = propertyCount;
			this.MeasureCount = measureCount;
			this.KpiCount = kpiCount;
			this.HierarchyCount = hierarchyCount;
			this.HiddenColumnCount = hiddenPropertyCount;
			this.OrderByColumnCount = orderByPropertyCount;
			this.IsDateTable = isDateTable;
			this.IsHidden = isHidden;
			this.HasDefaultLabel = hasDefaultLabel;
			this.DefaultLabelDataType = defaultLabelDataType;
			this.HasRowIdentifier = rowIdentifierDataTypes.Count > 0;
			this.RowIdentifierDataTypes = rowIdentifierDataTypes;
			this.KeepUniqueRowsColumnsCount = keepUniqueRowsColumnsCount;
			this.HasDefaultImage = hasDefaultImage;
			this.DefaultImageDataType = defaultImageDataType;
			this.HasDefaultFieldSet = defaultFieldSetDataTypes.Count > 0;
			this.DefaultFieldSetDataTypes = defaultFieldSetDataTypes;
			this.ColumnCountByDataType = propertyCountByDataType.AsReadOnlyDictionary<ConceptualPrimitiveType, int>();
			this.ColumnCountByDataCategory = propertyCountByDataCategory.AsReadOnlyDictionary<ConceptualDataCategory, int>();
			this.ColumnCountByDefaultAggregate = propertyCountByDefaultAggregate.AsReadOnlyDictionary<ConceptualDefaultAggregate, int>();
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000026C3 File Offset: 0x000008C3
		public int ColumnCount { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000026CB File Offset: 0x000008CB
		public int MeasureCount { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000026D3 File Offset: 0x000008D3
		public int KpiCount { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000026DB File Offset: 0x000008DB
		public int HierarchyCount { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000026E3 File Offset: 0x000008E3
		public int HiddenColumnCount { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000026EB File Offset: 0x000008EB
		public int OrderByColumnCount { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000026F3 File Offset: 0x000008F3
		public bool IsDateTable { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000026FB File Offset: 0x000008FB
		public bool IsHidden { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002703 File Offset: 0x00000903
		public bool HasDefaultLabel { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000270B File Offset: 0x0000090B
		public ConceptualPrimitiveType DefaultLabelDataType { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002713 File Offset: 0x00000913
		public bool HasRowIdentifier { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000271B File Offset: 0x0000091B
		public ReadOnlyCollection<ConceptualPrimitiveType> RowIdentifierDataTypes { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002723 File Offset: 0x00000923
		public int KeepUniqueRowsColumnsCount { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000272B File Offset: 0x0000092B
		public bool HasDefaultImage { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002733 File Offset: 0x00000933
		public ConceptualPrimitiveType DefaultImageDataType { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000273B File Offset: 0x0000093B
		public bool HasDefaultFieldSet { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002743 File Offset: 0x00000943
		public ReadOnlyCollection<ConceptualPrimitiveType> DefaultFieldSetDataTypes { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000274B File Offset: 0x0000094B
		public ReadOnlyDictionary<ConceptualPrimitiveType, int> ColumnCountByDataType { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002753 File Offset: 0x00000953
		public ReadOnlyDictionary<ConceptualDataCategory, int> ColumnCountByDataCategory { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000275B File Offset: 0x0000095B
		public ReadOnlyDictionary<ConceptualDefaultAggregate, int> ColumnCountByDefaultAggregate { get; }
	}
}
