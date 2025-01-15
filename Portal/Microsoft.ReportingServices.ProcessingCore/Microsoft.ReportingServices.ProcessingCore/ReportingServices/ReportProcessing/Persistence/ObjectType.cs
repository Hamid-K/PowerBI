using System;

namespace Microsoft.ReportingServices.ReportProcessing.Persistence
{
	// Token: 0x0200079D RID: 1949
	internal enum ObjectType
	{
		// Token: 0x04003698 RID: 13976
		None,
		// Token: 0x04003699 RID: 13977
		IDOwner,
		// Token: 0x0400369A RID: 13978
		ReportItem,
		// Token: 0x0400369B RID: 13979
		ReportItemList,
		// Token: 0x0400369C RID: 13980
		Report,
		// Token: 0x0400369D RID: 13981
		PageSection,
		// Token: 0x0400369E RID: 13982
		Line,
		// Token: 0x0400369F RID: 13983
		Rectangle,
		// Token: 0x040036A0 RID: 13984
		Image,
		// Token: 0x040036A1 RID: 13985
		ImageStreamNames,
		// Token: 0x040036A2 RID: 13986
		CheckBox,
		// Token: 0x040036A3 RID: 13987
		TextBox,
		// Token: 0x040036A4 RID: 13988
		SubReport,
		// Token: 0x040036A5 RID: 13989
		ActiveXControl,
		// Token: 0x040036A6 RID: 13990
		DataRegion,
		// Token: 0x040036A7 RID: 13991
		DataRegionList,
		// Token: 0x040036A8 RID: 13992
		ReportHierarchyNode,
		// Token: 0x040036A9 RID: 13993
		Grouping,
		// Token: 0x040036AA RID: 13994
		Sorting,
		// Token: 0x040036AB RID: 13995
		List,
		// Token: 0x040036AC RID: 13996
		Pivot,
		// Token: 0x040036AD RID: 13997
		Matrix,
		// Token: 0x040036AE RID: 13998
		PivotHeading,
		// Token: 0x040036AF RID: 13999
		MatrixHeading,
		// Token: 0x040036B0 RID: 14000
		MatrixColumn,
		// Token: 0x040036B1 RID: 14001
		MatrixColumnList,
		// Token: 0x040036B2 RID: 14002
		MatrixRow,
		// Token: 0x040036B3 RID: 14003
		MatrixRowList,
		// Token: 0x040036B4 RID: 14004
		Subtotal,
		// Token: 0x040036B5 RID: 14005
		Table,
		// Token: 0x040036B6 RID: 14006
		TableColumn,
		// Token: 0x040036B7 RID: 14007
		TableColumnList,
		// Token: 0x040036B8 RID: 14008
		TableGroup,
		// Token: 0x040036B9 RID: 14009
		TableRow,
		// Token: 0x040036BA RID: 14010
		TableRowList,
		// Token: 0x040036BB RID: 14011
		OWCChart,
		// Token: 0x040036BC RID: 14012
		ChartColumn,
		// Token: 0x040036BD RID: 14013
		ChartColumnList,
		// Token: 0x040036BE RID: 14014
		ReportItemCollection,
		// Token: 0x040036BF RID: 14015
		ReportItemIndexer,
		// Token: 0x040036C0 RID: 14016
		ReportItemIndexerList,
		// Token: 0x040036C1 RID: 14017
		Style,
		// Token: 0x040036C2 RID: 14018
		StyleAttributeHashtable,
		// Token: 0x040036C3 RID: 14019
		AttributeInfo,
		// Token: 0x040036C4 RID: 14020
		Visibility,
		// Token: 0x040036C5 RID: 14021
		ExpressionInfo,
		// Token: 0x040036C6 RID: 14022
		ExpressionInfoList,
		// Token: 0x040036C7 RID: 14023
		DataAggregateInfo,
		// Token: 0x040036C8 RID: 14024
		DataAggregateInfoList,
		// Token: 0x040036C9 RID: 14025
		RunningValueInfo,
		// Token: 0x040036CA RID: 14026
		RunningValueInfoList,
		// Token: 0x040036CB RID: 14027
		EmbeddedImageHashtable,
		// Token: 0x040036CC RID: 14028
		Filter,
		// Token: 0x040036CD RID: 14029
		FilterList,
		// Token: 0x040036CE RID: 14030
		DataSource,
		// Token: 0x040036CF RID: 14031
		DataSourceList,
		// Token: 0x040036D0 RID: 14032
		DataSet,
		// Token: 0x040036D1 RID: 14033
		DataSetList,
		// Token: 0x040036D2 RID: 14034
		ReportQuery,
		// Token: 0x040036D3 RID: 14035
		Field,
		// Token: 0x040036D4 RID: 14036
		DataFieldList,
		// Token: 0x040036D5 RID: 14037
		ParameterValue,
		// Token: 0x040036D6 RID: 14038
		ParameterValueList,
		// Token: 0x040036D7 RID: 14039
		StringList,
		// Token: 0x040036D8 RID: 14040
		IntList,
		// Token: 0x040036D9 RID: 14041
		BoolList,
		// Token: 0x040036DA RID: 14042
		ReportSnapshot,
		// Token: 0x040036DB RID: 14043
		SenderInformation,
		// Token: 0x040036DC RID: 14044
		SenderInformationHashtable,
		// Token: 0x040036DD RID: 14045
		ReceiverInformation,
		// Token: 0x040036DE RID: 14046
		ReceiverInformationHashtable,
		// Token: 0x040036DF RID: 14047
		DocumentMapNode,
		// Token: 0x040036E0 RID: 14048
		InfoBase,
		// Token: 0x040036E1 RID: 14049
		OffsetInfo,
		// Token: 0x040036E2 RID: 14050
		InstanceInfo,
		// Token: 0x040036E3 RID: 14051
		ReportItemInstanceInfo,
		// Token: 0x040036E4 RID: 14052
		ReportInstanceInfo,
		// Token: 0x040036E5 RID: 14053
		ReportItemColInstanceInfo,
		// Token: 0x040036E6 RID: 14054
		LineInstanceInfo,
		// Token: 0x040036E7 RID: 14055
		TextBoxInstanceInfo,
		// Token: 0x040036E8 RID: 14056
		RectangleInstanceInfo,
		// Token: 0x040036E9 RID: 14057
		CheckBoxInstanceInfo,
		// Token: 0x040036EA RID: 14058
		ImageInstanceInfo,
		// Token: 0x040036EB RID: 14059
		SubReportInstanceInfo,
		// Token: 0x040036EC RID: 14060
		ActiveXControlInstanceInfo,
		// Token: 0x040036ED RID: 14061
		ListInstanceInfo,
		// Token: 0x040036EE RID: 14062
		ListContentInstanceInfo,
		// Token: 0x040036EF RID: 14063
		MatrixInstanceInfo,
		// Token: 0x040036F0 RID: 14064
		MatrixHeadingInstanceInfo,
		// Token: 0x040036F1 RID: 14065
		MatrixCellInstanceInfo,
		// Token: 0x040036F2 RID: 14066
		TableInstanceInfo,
		// Token: 0x040036F3 RID: 14067
		TableGroupInstanceInfo,
		// Token: 0x040036F4 RID: 14068
		TableRowInstanceInfo,
		// Token: 0x040036F5 RID: 14069
		OWCChartInstanceInfo,
		// Token: 0x040036F6 RID: 14070
		ChartInstanceInfo,
		// Token: 0x040036F7 RID: 14071
		NonComputedUniqueNames,
		// Token: 0x040036F8 RID: 14072
		InstanceInfoOwner,
		// Token: 0x040036F9 RID: 14073
		ReportItemInstance,
		// Token: 0x040036FA RID: 14074
		ReportItemInstanceList,
		// Token: 0x040036FB RID: 14075
		ReportInstance,
		// Token: 0x040036FC RID: 14076
		ReportItemColInstance,
		// Token: 0x040036FD RID: 14077
		LineInstance,
		// Token: 0x040036FE RID: 14078
		TextBoxInstance,
		// Token: 0x040036FF RID: 14079
		RectangleInstance,
		// Token: 0x04003700 RID: 14080
		CheckBoxInstance,
		// Token: 0x04003701 RID: 14081
		ImageInstance,
		// Token: 0x04003702 RID: 14082
		SubReportInstance,
		// Token: 0x04003703 RID: 14083
		ActiveXControlInstance,
		// Token: 0x04003704 RID: 14084
		ListInstance,
		// Token: 0x04003705 RID: 14085
		ListContentInstance,
		// Token: 0x04003706 RID: 14086
		ListContentInstanceList,
		// Token: 0x04003707 RID: 14087
		MatrixInstance,
		// Token: 0x04003708 RID: 14088
		MatrixHeadingInstance,
		// Token: 0x04003709 RID: 14089
		MatrixHeadingInstanceList,
		// Token: 0x0400370A RID: 14090
		MatrixCellInstance,
		// Token: 0x0400370B RID: 14091
		MatrixCellInstanceList,
		// Token: 0x0400370C RID: 14092
		MatrixCellInstancesList,
		// Token: 0x0400370D RID: 14093
		TableInstance,
		// Token: 0x0400370E RID: 14094
		TableRowInstance,
		// Token: 0x0400370F RID: 14095
		TableColumnInstance,
		// Token: 0x04003710 RID: 14096
		TableGroupInstance,
		// Token: 0x04003711 RID: 14097
		TableGroupInstanceList,
		// Token: 0x04003712 RID: 14098
		OWCChartInstance,
		// Token: 0x04003713 RID: 14099
		ParameterInfo,
		// Token: 0x04003714 RID: 14100
		ParameterInfoCollection,
		// Token: 0x04003715 RID: 14101
		Variant,
		// Token: 0x04003716 RID: 14102
		VariantList,
		// Token: 0x04003717 RID: 14103
		QuickFindHashtable,
		// Token: 0x04003718 RID: 14104
		SubReportList,
		// Token: 0x04003719 RID: 14105
		RecordSetInfo,
		// Token: 0x0400371A RID: 14106
		RecordRow,
		// Token: 0x0400371B RID: 14107
		RecordField,
		// Token: 0x0400371C RID: 14108
		ValidValue,
		// Token: 0x0400371D RID: 14109
		ValidValueList,
		// Token: 0x0400371E RID: 14110
		ParameterDataSource,
		// Token: 0x0400371F RID: 14111
		ParameterDef,
		// Token: 0x04003720 RID: 14112
		ParameterDefList,
		// Token: 0x04003721 RID: 14113
		ParameterBase,
		// Token: 0x04003722 RID: 14114
		ProcessingMessageList,
		// Token: 0x04003723 RID: 14115
		ProcessingMessage,
		// Token: 0x04003724 RID: 14116
		MatrixSubtotalHeadingInstanceInfo,
		// Token: 0x04003725 RID: 14117
		MatrixSubtotalCellInstance,
		// Token: 0x04003726 RID: 14118
		CodeClass,
		// Token: 0x04003727 RID: 14119
		CodeClassList,
		// Token: 0x04003728 RID: 14120
		TableDetail,
		// Token: 0x04003729 RID: 14121
		TableDetailInstance,
		// Token: 0x0400372A RID: 14122
		TableDetailInstanceList,
		// Token: 0x0400372B RID: 14123
		TableDetailInstanceInfo,
		// Token: 0x0400372C RID: 14124
		String,
		// Token: 0x0400372D RID: 14125
		Action,
		// Token: 0x0400372E RID: 14126
		ActionInstance,
		// Token: 0x0400372F RID: 14127
		Chart,
		// Token: 0x04003730 RID: 14128
		ChartHeading,
		// Token: 0x04003731 RID: 14129
		ChartDataPoint,
		// Token: 0x04003732 RID: 14130
		ChartDataPointList,
		// Token: 0x04003733 RID: 14131
		MultiChart,
		// Token: 0x04003734 RID: 14132
		MultiChartInstance,
		// Token: 0x04003735 RID: 14133
		MultiChartInstanceList,
		// Token: 0x04003736 RID: 14134
		Axis,
		// Token: 0x04003737 RID: 14135
		AxisInstance,
		// Token: 0x04003738 RID: 14136
		ChartTitle,
		// Token: 0x04003739 RID: 14137
		ChartTitleInstance,
		// Token: 0x0400373A RID: 14138
		ThreeDProperties,
		// Token: 0x0400373B RID: 14139
		PlotArea,
		// Token: 0x0400373C RID: 14140
		Legend,
		// Token: 0x0400373D RID: 14141
		GridLines,
		// Token: 0x0400373E RID: 14142
		ChartDataLabel,
		// Token: 0x0400373F RID: 14143
		ChartInstance,
		// Token: 0x04003740 RID: 14144
		ChartHeadingInstance,
		// Token: 0x04003741 RID: 14145
		ChartHeadingInstanceInfo,
		// Token: 0x04003742 RID: 14146
		ChartHeadingInstanceList,
		// Token: 0x04003743 RID: 14147
		ChartDataPointInstance,
		// Token: 0x04003744 RID: 14148
		ChartDataPointInstanceInfo,
		// Token: 0x04003745 RID: 14149
		ChartDataPointInstanceList,
		// Token: 0x04003746 RID: 14150
		ChartDataPointInstancesList,
		// Token: 0x04003747 RID: 14151
		RenderingPagesRanges,
		// Token: 0x04003748 RID: 14152
		RenderingPagesRangesList,
		// Token: 0x04003749 RID: 14153
		IntermediateFormatVersion,
		// Token: 0x0400374A RID: 14154
		ImageInfo,
		// Token: 0x0400374B RID: 14155
		ActionItem,
		// Token: 0x0400374C RID: 14156
		ActionItemInstance,
		// Token: 0x0400374D RID: 14157
		ActionItemList,
		// Token: 0x0400374E RID: 14158
		ActionItemInstanceList,
		// Token: 0x0400374F RID: 14159
		DataValue,
		// Token: 0x04003750 RID: 14160
		DataValueInstance,
		// Token: 0x04003751 RID: 14161
		DataValueList,
		// Token: 0x04003752 RID: 14162
		DataValueInstanceList,
		// Token: 0x04003753 RID: 14163
		Tablix,
		// Token: 0x04003754 RID: 14164
		TablixHeading,
		// Token: 0x04003755 RID: 14165
		CustomReportItem,
		// Token: 0x04003756 RID: 14166
		CustomReportItemInstance,
		// Token: 0x04003757 RID: 14167
		CustomReportItemHeading,
		// Token: 0x04003758 RID: 14168
		CustomReportItemHeadingInstance,
		// Token: 0x04003759 RID: 14169
		CustomReportItemHeadingList,
		// Token: 0x0400375A RID: 14170
		CustomReportItemHeadingInstanceList,
		// Token: 0x0400375B RID: 14171
		DataCellsList,
		// Token: 0x0400375C RID: 14172
		DataCellList,
		// Token: 0x0400375D RID: 14173
		CustomReportItemCellInstance,
		// Token: 0x0400375E RID: 14174
		CustomReportItemCellInstanceList,
		// Token: 0x0400375F RID: 14175
		CustomReportItemCellInstancesList,
		// Token: 0x04003760 RID: 14176
		DataValueCRIList,
		// Token: 0x04003761 RID: 14177
		BookmarkInformation,
		// Token: 0x04003762 RID: 14178
		BookmarksHashtable,
		// Token: 0x04003763 RID: 14179
		DrillthroughInformation,
		// Token: 0x04003764 RID: 14180
		DrillthroughHashtable,
		// Token: 0x04003765 RID: 14181
		DrillthroughParameters,
		// Token: 0x04003766 RID: 14182
		CustomReportItemInstanceInfo,
		// Token: 0x04003767 RID: 14183
		ImageMapAreaInstanceList,
		// Token: 0x04003768 RID: 14184
		ImageMapAreaInstance,
		// Token: 0x04003769 RID: 14185
		Single,
		// Token: 0x0400376A RID: 14186
		SortFilterEventInfoHashtable,
		// Token: 0x0400376B RID: 14187
		SortFilterEventInfo,
		// Token: 0x0400376C RID: 14188
		EndUserSort,
		// Token: 0x0400376D RID: 14189
		ISortFilterScope,
		// Token: 0x0400376E RID: 14190
		GroupingList,
		// Token: 0x0400376F RID: 14191
		RecordSetPropertyNames,
		// Token: 0x04003770 RID: 14192
		RecordSetPropertyNamesList,
		// Token: 0x04003771 RID: 14193
		FieldPropertyHashtable,
		// Token: 0x04003772 RID: 14194
		Int64List,
		// Token: 0x04003773 RID: 14195
		PageSectionInstance,
		// Token: 0x04003774 RID: 14196
		PageSectionInstanceList,
		// Token: 0x04003775 RID: 14197
		PageSectionInstanceInfo,
		// Token: 0x04003776 RID: 14198
		SimpleTextBoxInstanceInfo,
		// Token: 0x04003777 RID: 14199
		ScopeLookupTable,
		// Token: 0x04003778 RID: 14200
		InScopeSortFilterHashtable,
		// Token: 0x04003779 RID: 14201
		ReportDrillthroughInfo,
		// Token: 0x0400377A RID: 14202
		TokensHashtable,
		// Token: 0x0400377B RID: 14203
		MaxValue
	}
}
