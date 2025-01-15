using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.InfoNav;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200004C RID: 76
	public sealed class ContractConceptualEntitySetStatistics
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00003FD4 File Offset: 0x000021D4
		public ContractConceptualEntitySetStatistics(int? rowCount, int columnCount, int measureCount, int kpiCount, int hierarchyCount, int hiddenColumnCount, int orderByColumnCount, bool isDateTable, ConceptualPrimitiveType? defaultLabelDataType, ReadOnlyCollection<ConceptualPrimitiveType> rowIdentifierDataTypes, ConceptualPrimitiveType? defaultImageDataType, ReadOnlyCollection<ConceptualPrimitiveType> defaultFieldSetDataTypes, int keepUniqueRowsColumnsCount, Dictionary<ConceptualDataCategory, int> columnCountByDataCategory, Dictionary<ConceptualPrimitiveType, int> columnCountByDataType, Dictionary<ConceptualDefaultAggregate, int> columnCountByDefaultAggregate)
		{
			this.RowCount = rowCount;
			this.ColumnCount = columnCount;
			this.MeasureCount = measureCount;
			this.KpiCount = kpiCount;
			this.HierarchyCount = hierarchyCount;
			this.HiddenColumnCount = hiddenColumnCount;
			this.OrderByColumnCount = orderByColumnCount;
			this.IsDateTable = isDateTable;
			this.DefaultLabelDataType = defaultLabelDataType;
			this.RowIdentifierDataTypes = rowIdentifierDataTypes;
			this.DefaultImageDataType = defaultImageDataType;
			this.DefaultFieldSetDataTypes = defaultFieldSetDataTypes;
			this.KeepUniqueRowsColumnsCount = keepUniqueRowsColumnsCount;
			this.ColumnCountByDataCategory = columnCountByDataCategory;
			this.ColumnCountByDataType = columnCountByDataType;
			this.ColumnCountByDefaultAggregate = columnCountByDefaultAggregate;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004064 File Offset: 0x00002264
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000406C File Offset: 0x0000226C
		public int? RowCount { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00004075 File Offset: 0x00002275
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000407D File Offset: 0x0000227D
		public int ColumnCount { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004086 File Offset: 0x00002286
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000408E File Offset: 0x0000228E
		public int MeasureCount { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004097 File Offset: 0x00002297
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000409F File Offset: 0x0000229F
		public int KpiCount { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000040A8 File Offset: 0x000022A8
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000040B0 File Offset: 0x000022B0
		public int HierarchyCount { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000040B9 File Offset: 0x000022B9
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000040C1 File Offset: 0x000022C1
		public int HiddenColumnCount { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000040CA File Offset: 0x000022CA
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000040D2 File Offset: 0x000022D2
		public int OrderByColumnCount { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000040DB File Offset: 0x000022DB
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000040E3 File Offset: 0x000022E3
		public bool IsDateTable { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000040EC File Offset: 0x000022EC
		// (set) Token: 0x06000137 RID: 311 RVA: 0x000040F4 File Offset: 0x000022F4
		public bool IsHidden { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000040FD File Offset: 0x000022FD
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00004105 File Offset: 0x00002305
		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ConceptualPrimitiveType? DefaultLabelDataType { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000410E File Offset: 0x0000230E
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00004116 File Offset: 0x00002316
		[JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
		public ReadOnlyCollection<ConceptualPrimitiveType> RowIdentifierDataTypes { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000411F File Offset: 0x0000231F
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00004127 File Offset: 0x00002327
		public int KeepUniqueRowsColumnsCount { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004130 File Offset: 0x00002330
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00004138 File Offset: 0x00002338
		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ConceptualPrimitiveType? DefaultImageDataType { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004141 File Offset: 0x00002341
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00004149 File Offset: 0x00002349
		[JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
		public ReadOnlyCollection<ConceptualPrimitiveType> DefaultFieldSetDataTypes { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004152 File Offset: 0x00002352
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000415A File Offset: 0x0000235A
		public Dictionary<ConceptualPrimitiveType, int> ColumnCountByDataType { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004163 File Offset: 0x00002363
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000416B File Offset: 0x0000236B
		public Dictionary<ConceptualDataCategory, int> ColumnCountByDataCategory { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004174 File Offset: 0x00002374
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000417C File Offset: 0x0000237C
		public Dictionary<ConceptualDefaultAggregate, int> ColumnCountByDefaultAggregate { get; private set; }
	}
}
