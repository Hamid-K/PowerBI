using System;
using System.Xml.Linq;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200021C RID: 540
	internal sealed class DAXFunctions
	{
		// Token: 0x060018F6 RID: 6390 RVA: 0x00044194 File Offset: 0x00042394
		internal DAXFunctions(XElement daxFunctionsElem)
			: this(daxFunctionsElem.GetEnumElementOrDefault(Extensions.LeftOuterJoinElem, LeftOuterJoinType.NotSupported), daxFunctionsElem.GetEnumElementOrDefault(Extensions.SubstituteWithIndexElem, SubstituteWithIndexType.NotSupported), daxFunctionsElem.GetEnumElementOrDefault(Extensions.SummarizeColumnsElem, SummarizeColumnsType.NotSupported), daxFunctionsElem.GetEnumElementOrDefault(Extensions.BinaryMinMaxElem, BinaryMinMaxType.NotSupported), daxFunctionsElem.GetEnumElementOrDefault(Extensions.StringMinMaxElem, StringMinMaxType.NotSupported), daxFunctionsElem.GetEnumElementOrDefault(Extensions.TreatAsElem, TreatAsType.NotSupported), daxFunctionsElem.GetEnumElementOrDefault(Extensions.SampleAxisWithLocalMinMaxElem, SampleAxisWithLocalMinMaxType.NotSupported), daxFunctionsElem.GetEnumElementOrDefault(Extensions.SampleCartesianPointsByCoverElem, SampleCartesianPointsByCoverType.NotSupported), daxFunctionsElem.GetEnumElementOrDefault(Extensions.OptimizedNotInOperatorElem, OptimizedNotInOperatorType.NotSupported), daxFunctionsElem.GetEnumElementOrDefault(Extensions.NonVisualElem, NonVisualType.NotSupported), daxFunctionsElem.GetEnumElementOrDefault(Extensions.TopNPerLevelElem, TopNPerLevelType.NotSupported), new DaxExtensionFunctions(daxFunctionsElem.GetElementOrNull(Extensions.DaxExtensionFunctionsElem)), daxFunctionsElem.GetEnumElementOrDefault(Extensions.IsAfterElem, IsAfterType.NotSupported), daxFunctionsElem.GetEnumElementOrDefault(Extensions.FormatByLocaleElem, FormatByLocale.NotSupported))
		{
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00044254 File Offset: 0x00042454
		internal DAXFunctions(LeftOuterJoinType leftOuterJoin, SubstituteWithIndexType substituteWithIndex, SummarizeColumnsType summarizeColumns, BinaryMinMaxType binaryMinMax, StringMinMaxType stringMinMax, TreatAsType treatAs, SampleAxisWithLocalMinMaxType sampleAxisWithLocalMinMax, SampleCartesianPointsByCoverType sampleCartesianPointsByCover, OptimizedNotInOperatorType optimizedNotInOperator, NonVisualType nonVisual, TopNPerLevelType topNPerLevel, DaxExtensionFunctions daxExtensionFunctions, IsAfterType isAfter, FormatByLocale formatByLocale)
		{
			this.LeftOuterJoin = leftOuterJoin;
			this.SubstituteWithIndex = substituteWithIndex;
			this.SummarizeColumns = summarizeColumns;
			this.BinaryMinMax = binaryMinMax;
			this.StringMinMax = stringMinMax;
			this.TreatAs = treatAs;
			this.SampleAxisWithLocalMinMax = sampleAxisWithLocalMinMax;
			this.SampleCartesianPointsByCover = sampleCartesianPointsByCover;
			this.OptimizedNotInOperator = optimizedNotInOperator;
			this.NonVisual = nonVisual;
			this.TopNPerLevel = topNPerLevel;
			this.DaxExtensionFunctions = daxExtensionFunctions;
			this.IsAfter = isAfter;
			this.FormatByLocale = formatByLocale;
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x000442D4 File Offset: 0x000424D4
		internal LeftOuterJoinType LeftOuterJoin { get; }

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x000442DC File Offset: 0x000424DC
		internal SubstituteWithIndexType SubstituteWithIndex { get; }

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x000442E4 File Offset: 0x000424E4
		internal SummarizeColumnsType SummarizeColumns { get; }

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x000442EC File Offset: 0x000424EC
		internal BinaryMinMaxType BinaryMinMax { get; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x000442F4 File Offset: 0x000424F4
		internal StringMinMaxType StringMinMax { get; }

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x000442FC File Offset: 0x000424FC
		internal TreatAsType TreatAs { get; }

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x00044304 File Offset: 0x00042504
		internal SampleAxisWithLocalMinMaxType SampleAxisWithLocalMinMax { get; }

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0004430C File Offset: 0x0004250C
		internal SampleCartesianPointsByCoverType SampleCartesianPointsByCover { get; }

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x00044314 File Offset: 0x00042514
		internal OptimizedNotInOperatorType OptimizedNotInOperator { get; }

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0004431C File Offset: 0x0004251C
		internal NonVisualType NonVisual { get; }

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x00044324 File Offset: 0x00042524
		internal TopNPerLevelType TopNPerLevel { get; }

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0004432C File Offset: 0x0004252C
		internal DaxExtensionFunctions DaxExtensionFunctions { get; }

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x00044334 File Offset: 0x00042534
		internal IsAfterType IsAfter { get; }

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0004433C File Offset: 0x0004253C
		internal FormatByLocale FormatByLocale { get; }

		// Token: 0x06001906 RID: 6406 RVA: 0x00044344 File Offset: 0x00042544
		internal DAXFunctions CreateCopy(LeftOuterJoinType? leftOuterJoin = null, SubstituteWithIndexType? substituteWithIndex = null, SummarizeColumnsType? summarizeColumns = null, BinaryMinMaxType? binaryMinMax = null, StringMinMaxType? stringMinMax = null, TreatAsType? treatAs = null, SampleAxisWithLocalMinMaxType? sampleAxisWithLocalMinMax = null, SampleCartesianPointsByCoverType? sampleCartesianPointsByCover = null, OptimizedNotInOperatorType? optimizedNotInOperator = null, NonVisualType? nonVisual = null, TopNPerLevelType? topNPerLevel = null, DaxExtensionFunctions daxExtensionFunctions = null, IsAfterType? isAfter = null, FormatByLocale? formatByLocale = null)
		{
			return new DAXFunctions(leftOuterJoin ?? this.LeftOuterJoin, substituteWithIndex ?? this.SubstituteWithIndex, summarizeColumns ?? this.SummarizeColumns, binaryMinMax ?? this.BinaryMinMax, stringMinMax ?? this.StringMinMax, treatAs ?? this.TreatAs, sampleAxisWithLocalMinMax ?? this.SampleAxisWithLocalMinMax, sampleCartesianPointsByCover ?? this.SampleCartesianPointsByCover, optimizedNotInOperator ?? this.OptimizedNotInOperator, nonVisual ?? this.NonVisual, topNPerLevel ?? this.TopNPerLevel, daxExtensionFunctions ?? this.DaxExtensionFunctions, isAfter ?? this.IsAfter, formatByLocale ?? this.FormatByLocale);
		}
	}
}
