using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000557 RID: 1367
	internal enum MemberName
	{
		// Token: 0x040022CD RID: 8909
		FieldValueSerializable = 10000,
		// Token: 0x040022CE RID: 8910
		ID = 0,
		// Token: 0x040022CF RID: 8911
		ReaderExtensionsSupported,
		// Token: 0x040022D0 RID: 8912
		FieldPropertyNames,
		// Token: 0x040022D1 RID: 8913
		CompareOptions,
		// Token: 0x040022D2 RID: 8914
		RecordFields,
		// Token: 0x040022D3 RID: 8915
		IsAggregateRow,
		// Token: 0x040022D4 RID: 8916
		AggregationFieldCount,
		// Token: 0x040022D5 RID: 8917
		PropertyNames,
		// Token: 0x040022D6 RID: 8918
		FieldStatus,
		// Token: 0x040022D7 RID: 8919
		FieldValue,
		// Token: 0x040022D8 RID: 8920
		IsAggregateField,
		// Token: 0x040022D9 RID: 8921
		FieldPropertyValues,
		// Token: 0x040022DA RID: 8922
		CellAggregates,
		// Token: 0x040022DB RID: 8923
		CellPostSortAggregates,
		// Token: 0x040022DC RID: 8924
		CellRunningValues,
		// Token: 0x040022DD RID: 8925
		DataSetIndexInCollection,
		// Token: 0x040022DE RID: 8926
		RowMembers,
		// Token: 0x040022DF RID: 8927
		ColumnMembers,
		// Token: 0x040022E0 RID: 8928
		Cells,
		// Token: 0x040022E1 RID: 8929
		MemberInstanceIndexWithinScopeLevel,
		// Token: 0x040022E2 RID: 8930
		Children,
		// Token: 0x040022E3 RID: 8931
		Variable,
		// Token: 0x040022E4 RID: 8932
		Variables,
		// Token: 0x040022E5 RID: 8933
		RecursiveLevel,
		// Token: 0x040022E6 RID: 8934
		GroupExpressionValues,
		// Token: 0x040022E7 RID: 8935
		RecordSetSize,
		// Token: 0x040022E8 RID: 8936
		RewrittenCommandText,
		// Token: 0x040022E9 RID: 8937
		Fields,
		// Token: 0x040022EA RID: 8938
		CaseSensitivity,
		// Token: 0x040022EB RID: 8939
		Collation,
		// Token: 0x040022EC RID: 8940
		AccentSensitivity,
		// Token: 0x040022ED RID: 8941
		KanatypeSensitivity,
		// Token: 0x040022EE RID: 8942
		WidthSensitivity,
		// Token: 0x040022EF RID: 8943
		LCID,
		// Token: 0x040022F0 RID: 8944
		TablixProcessingComplete,
		// Token: 0x040022F1 RID: 8945
		FieldPropertyReaderIndices,
		// Token: 0x040022F2 RID: 8946
		NoRows,
		// Token: 0x040022F3 RID: 8947
		Language,
		// Token: 0x040022F4 RID: 8948
		DataSetInstances,
		// Token: 0x040022F5 RID: 8949
		FirstRowIndex,
		// Token: 0x040022F6 RID: 8950
		DataRegionInstances,
		// Token: 0x040022F7 RID: 8951
		SubReportInstances,
		// Token: 0x040022F8 RID: 8952
		AggregateValues,
		// Token: 0x040022F9 RID: 8953
		SubReport,
		// Token: 0x040022FA RID: 8954
		ReportInstance,
		// Token: 0x040022FB RID: 8955
		DataSetUniqueName,
		// Token: 0x040022FC RID: 8956
		ThreadCulture,
		// Token: 0x040022FD RID: 8957
		Parameters,
		// Token: 0x040022FE RID: 8958
		Status,
		// Token: 0x040022FF RID: 8959
		ProcessedWithError,
		// Token: 0x04002300 RID: 8960
		LastID,
		// Token: 0x04002301 RID: 8961
		UniqueName,
		// Token: 0x04002302 RID: 8962
		ReportPath,
		// Token: 0x04002303 RID: 8963
		ParametersFromCatalog,
		// Token: 0x04002304 RID: 8964
		Name,
		// Token: 0x04002305 RID: 8965
		Value,
		// Token: 0x04002306 RID: 8966
		DataType,
		// Token: 0x04002307 RID: 8967
		AggregateType,
		// Token: 0x04002308 RID: 8968
		Expressions,
		// Token: 0x04002309 RID: 8969
		DuplicateNames,
		// Token: 0x0400230A RID: 8970
		Scope,
		// Token: 0x0400230B RID: 8971
		TotalGroupingExpressionCount,
		// Token: 0x0400230C RID: 8972
		IsScopedInEvaluationScope,
		// Token: 0x0400230D RID: 8973
		IsInOutermostStatic,
		// Token: 0x0400230E RID: 8974
		ParentInstanceIndex,
		// Token: 0x0400230F RID: 8975
		IsInstanceShared,
		// Token: 0x04002310 RID: 8976
		DataChunkNameModifier,
		// Token: 0x04002311 RID: 8977
		OdpContext,
		// Token: 0x04002312 RID: 8978
		ProcessedPreviousAggregates,
		// Token: 0x04002313 RID: 8979
		ReportObjectModel,
		// Token: 0x04002314 RID: 8980
		AggregationFieldChecked,
		// Token: 0x04002315 RID: 8981
		Properties,
		// Token: 0x04002316 RID: 8982
		FieldDef,
		// Token: 0x04002317 RID: 8983
		UsedInExpression,
		// Token: 0x04002318 RID: 8984
		HierarchyRoot,
		// Token: 0x04002319 RID: 8985
		SortHierarchyStruct,
		// Token: 0x0400231A RID: 8986
		DataHolder,
		// Token: 0x0400231B RID: 8987
		Owner,
		// Token: 0x0400231C RID: 8988
		ScopeInstances,
		// Token: 0x0400231D RID: 8989
		ScopeValuesList,
		// Token: 0x0400231E RID: 8990
		SortTree,
		// Token: 0x0400231F RID: 8991
		CurrentScopeIndex,
		// Token: 0x04002320 RID: 8992
		ScopeInstanceIndices,
		// Token: 0x04002321 RID: 8993
		Tuples,
		// Token: 0x04002322 RID: 8994
		IndexInParent,
		// Token: 0x04002323 RID: 8995
		List,
		// Token: 0x04002324 RID: 8996
		Capacity,
		// Token: 0x04002325 RID: 8997
		Child,
		// Token: 0x04002326 RID: 8998
		Key,
		// Token: 0x04002327 RID: 8999
		HierarchyNode,
		// Token: 0x04002328 RID: 9000
		OuterGroupDynamicIndex,
		// Token: 0x04002329 RID: 9001
		RowIndexes,
		// Token: 0x0400232A RID: 9002
		ColumnIndexes,
		// Token: 0x0400232B RID: 9003
		CellNonCustomAggObjs,
		// Token: 0x0400232C RID: 9004
		CellCustomAggObjs,
		// Token: 0x0400232D RID: 9005
		CellAggValueList,
		// Token: 0x0400232E RID: 9006
		RunningValueValues,
		// Token: 0x0400232F RID: 9007
		Innermost,
		// Token: 0x04002330 RID: 9008
		FirstRow,
		// Token: 0x04002331 RID: 9009
		FirstRowIsAggregate,
		// Token: 0x04002332 RID: 9010
		NextCell,
		// Token: 0x04002333 RID: 9011
		SortFilterExpressionScopeInfoIndices,
		// Token: 0x04002334 RID: 9012
		CellsWithSameScope,
		// Token: 0x04002335 RID: 9013
		ReportItemColIndex,
		// Token: 0x04002336 RID: 9014
		OuterScope,
		// Token: 0x04002337 RID: 9015
		NonCustomAggregates,
		// Token: 0x04002338 RID: 9016
		CustomAggregates,
		// Token: 0x04002339 RID: 9017
		DataAction,
		// Token: 0x0400233A RID: 9018
		OuterDataAction,
		// Token: 0x0400233B RID: 9019
		InnerDataAction,
		// Token: 0x0400233C RID: 9020
		UserSortTargetInfo,
		// Token: 0x0400233D RID: 9021
		NonAggregateMode,
		// Token: 0x0400233E RID: 9022
		Aggregator,
		// Token: 0x0400233F RID: 9023
		AggregateDef,
		// Token: 0x04002340 RID: 9024
		ReportRuntime,
		// Token: 0x04002341 RID: 9025
		AggregateResult,
		// Token: 0x04002342 RID: 9026
		Updated,
		// Token: 0x04002343 RID: 9027
		ExpressionType,
		// Token: 0x04002344 RID: 9028
		CurrentTotalType,
		// Token: 0x04002345 RID: 9029
		CurrentTotal,
		// Token: 0x04002346 RID: 9030
		CurrentCount,
		// Token: 0x04002347 RID: 9031
		CompareInfo,
		// Token: 0x04002348 RID: 9032
		CurrentMax,
		// Token: 0x04002349 RID: 9033
		DistinctValues,
		// Token: 0x0400234A RID: 9034
		SumOfXType,
		// Token: 0x0400234B RID: 9035
		SumOfX,
		// Token: 0x0400234C RID: 9036
		SumOfXSquared,
		// Token: 0x0400234D RID: 9037
		Previous,
		// Token: 0x0400234E RID: 9038
		StyleClass,
		// Token: 0x0400234F RID: 9039
		Top,
		// Token: 0x04002350 RID: 9040
		TopValue,
		// Token: 0x04002351 RID: 9041
		Left,
		// Token: 0x04002352 RID: 9042
		LeftValue,
		// Token: 0x04002353 RID: 9043
		Height,
		// Token: 0x04002354 RID: 9044
		HeightValue,
		// Token: 0x04002355 RID: 9045
		Width,
		// Token: 0x04002356 RID: 9046
		WidthValue,
		// Token: 0x04002357 RID: 9047
		ZIndex,
		// Token: 0x04002358 RID: 9048
		Visibility,
		// Token: 0x04002359 RID: 9049
		Label,
		// Token: 0x0400235A RID: 9050
		RepeatedSibling,
		// Token: 0x0400235B RID: 9051
		Author,
		// Token: 0x0400235C RID: 9052
		AutoRefresh,
		// Token: 0x0400235D RID: 9053
		EmbeddedImages,
		// Token: 0x0400235E RID: 9054
		PageHeader,
		// Token: 0x0400235F RID: 9055
		PageFooter,
		// Token: 0x04002360 RID: 9056
		ReportItems,
		// Token: 0x04002361 RID: 9057
		DataSources,
		// Token: 0x04002362 RID: 9058
		PageHeight,
		// Token: 0x04002363 RID: 9059
		PageHeightValue,
		// Token: 0x04002364 RID: 9060
		PageWidth,
		// Token: 0x04002365 RID: 9061
		PageWidthValue,
		// Token: 0x04002366 RID: 9062
		LeftMargin,
		// Token: 0x04002367 RID: 9063
		LeftMarginValue,
		// Token: 0x04002368 RID: 9064
		RightMargin,
		// Token: 0x04002369 RID: 9065
		RightMarginValue,
		// Token: 0x0400236A RID: 9066
		TopMargin,
		// Token: 0x0400236B RID: 9067
		TopMarginValue,
		// Token: 0x0400236C RID: 9068
		BottomMargin,
		// Token: 0x0400236D RID: 9069
		BottomMarginValue,
		// Token: 0x0400236E RID: 9070
		ClassName,
		// Token: 0x0400236F RID: 9071
		InstanceName,
		// Token: 0x04002370 RID: 9072
		CodeModules,
		// Token: 0x04002371 RID: 9073
		CodeClasses,
		// Token: 0x04002372 RID: 9074
		Columns,
		// Token: 0x04002373 RID: 9075
		ColumnSpacing,
		// Token: 0x04002374 RID: 9076
		ColumnSpacingValue,
		// Token: 0x04002375 RID: 9077
		PageAggregates,
		// Token: 0x04002376 RID: 9078
		CompiledCode,
		// Token: 0x04002377 RID: 9079
		MergeOnePass,
		// Token: 0x04002378 RID: 9080
		PageMergeOnePass,
		// Token: 0x04002379 RID: 9081
		SubReportMergeTransactions,
		// Token: 0x0400237A RID: 9082
		NeedPostGroupProcessing,
		// Token: 0x0400237B RID: 9083
		HasPostSortAggregates,
		// Token: 0x0400237C RID: 9084
		HasReportItemReferences,
		// Token: 0x0400237D RID: 9085
		ShowHideType,
		// Token: 0x0400237E RID: 9086
		BodyID,
		// Token: 0x0400237F RID: 9087
		PrintOnFirstPage,
		// Token: 0x04002380 RID: 9088
		PrintOnLastPage,
		// Token: 0x04002381 RID: 9089
		PostProcessEvaluate,
		// Token: 0x04002382 RID: 9090
		Slanted,
		// Token: 0x04002383 RID: 9091
		PageBreakAtEnd,
		// Token: 0x04002384 RID: 9092
		PageBreakAtStart,
		// Token: 0x04002385 RID: 9093
		HyperLinkURL,
		// Token: 0x04002386 RID: 9094
		Source,
		// Token: 0x04002387 RID: 9095
		MIMEType,
		// Token: 0x04002388 RID: 9096
		Sizing,
		// Token: 0x04002389 RID: 9097
		HideDuplicates,
		// Token: 0x0400238A RID: 9098
		CanGrow,
		// Token: 0x0400238B RID: 9099
		CanShrink,
		// Token: 0x0400238C RID: 9100
		IsToggle,
		// Token: 0x0400238D RID: 9101
		InitialToggleState,
		// Token: 0x0400238E RID: 9102
		MergeTransactions,
		// Token: 0x0400238F RID: 9103
		ReportName,
		// Token: 0x04002390 RID: 9104
		Description,
		// Token: 0x04002391 RID: 9105
		Report,
		// Token: 0x04002392 RID: 9106
		StringUri,
		// Token: 0x04002393 RID: 9107
		ClassID,
		// Token: 0x04002394 RID: 9108
		CodeBase,
		// Token: 0x04002395 RID: 9109
		DataSetName,
		// Token: 0x04002396 RID: 9110
		KeepTogether,
		// Token: 0x04002397 RID: 9111
		RepeatSiblings,
		// Token: 0x04002398 RID: 9112
		Grouping,
		// Token: 0x04002399 RID: 9113
		Sorting,
		// Token: 0x0400239A RID: 9114
		DataRegionDef,
		// Token: 0x0400239B RID: 9115
		GroupExpressions,
		// Token: 0x0400239C RID: 9116
		GroupLabel,
		// Token: 0x0400239D RID: 9117
		SortDirections,
		// Token: 0x0400239E RID: 9118
		Aggregates,
		// Token: 0x0400239F RID: 9119
		GroupAndSort,
		// Token: 0x040023A0 RID: 9120
		SortExpressions,
		// Token: 0x040023A1 RID: 9121
		ColumnCount,
		// Token: 0x040023A2 RID: 9122
		RowCount,
		// Token: 0x040023A3 RID: 9123
		InnerRowLevelWithPageBreak,
		// Token: 0x040023A4 RID: 9124
		GroupsBeforeRowHeaders,
		// Token: 0x040023A5 RID: 9125
		ProcessingInnerGrouping,
		// Token: 0x040023A6 RID: 9126
		Size,
		// Token: 0x040023A7 RID: 9127
		SizeValue,
		// Token: 0x040023A8 RID: 9128
		Subtotal,
		// Token: 0x040023A9 RID: 9129
		Level,
		// Token: 0x040023AA RID: 9130
		IsColumn,
		// Token: 0x040023AB RID: 9131
		Position,
		// Token: 0x040023AC RID: 9132
		RunningValues,
		// Token: 0x040023AD RID: 9133
		ChartSeriesCollection,
		// Token: 0x040023AE RID: 9134
		ChartAreas,
		// Token: 0x040023AF RID: 9135
		Titles,
		// Token: 0x040023B0 RID: 9136
		AxisTitle,
		// Token: 0x040023B1 RID: 9137
		LegendTitle,
		// Token: 0x040023B2 RID: 9138
		BorderSkin,
		// Token: 0x040023B3 RID: 9139
		Title,
		// Token: 0x040023B4 RID: 9140
		TitleAngle,
		// Token: 0x040023B5 RID: 9141
		StripWidth,
		// Token: 0x040023B6 RID: 9142
		StripWidthType,
		// Token: 0x040023B7 RID: 9143
		CellType,
		// Token: 0x040023B8 RID: 9144
		Text,
		// Token: 0x040023B9 RID: 9145
		CellSpan,
		// Token: 0x040023BA RID: 9146
		ImageWidth,
		// Token: 0x040023BB RID: 9147
		ImageHeight,
		// Token: 0x040023BC RID: 9148
		SymbolHeight,
		// Token: 0x040023BD RID: 9149
		SymbolWidth,
		// Token: 0x040023BE RID: 9150
		Alignment,
		// Token: 0x040023BF RID: 9151
		ColumnType,
		// Token: 0x040023C0 RID: 9152
		ToolTip,
		// Token: 0x040023C1 RID: 9153
		MinimumWidth,
		// Token: 0x040023C2 RID: 9154
		MaximumWidth,
		// Token: 0x040023C3 RID: 9155
		SeriesSymbolWidth,
		// Token: 0x040023C4 RID: 9156
		SeriesSymbolHeight,
		// Token: 0x040023C5 RID: 9157
		Header,
		// Token: 0x040023C6 RID: 9158
		Marker,
		// Token: 0x040023C7 RID: 9159
		Separator,
		// Token: 0x040023C8 RID: 9160
		SeparatorColor,
		// Token: 0x040023C9 RID: 9161
		AllowOutSidePlotArea,
		// Token: 0x040023CA RID: 9162
		CalloutBackColor,
		// Token: 0x040023CB RID: 9163
		CalloutLineAnchor,
		// Token: 0x040023CC RID: 9164
		CalloutLineColor,
		// Token: 0x040023CD RID: 9165
		CalloutLineStyle,
		// Token: 0x040023CE RID: 9166
		CalloutLineWidth,
		// Token: 0x040023CF RID: 9167
		CalloutStyle,
		// Token: 0x040023D0 RID: 9168
		HideOverlapped,
		// Token: 0x040023D1 RID: 9169
		MarkerOverlapping,
		// Token: 0x040023D2 RID: 9170
		MaxMovingDistance,
		// Token: 0x040023D3 RID: 9171
		MinMovingDistance,
		// Token: 0x040023D4 RID: 9172
		NoMoveDirections,
		// Token: 0x040023D5 RID: 9173
		Up,
		// Token: 0x040023D6 RID: 9174
		Down,
		// Token: 0x040023D7 RID: 9175
		Right,
		// Token: 0x040023D8 RID: 9176
		UpLeft,
		// Token: 0x040023D9 RID: 9177
		UpRight,
		// Token: 0x040023DA RID: 9178
		DownLeft,
		// Token: 0x040023DB RID: 9179
		DownRight,
		// Token: 0x040023DC RID: 9180
		Visible,
		// Token: 0x040023DD RID: 9181
		Margin,
		// Token: 0x040023DE RID: 9182
		Interval,
		// Token: 0x040023DF RID: 9183
		IntervalType,
		// Token: 0x040023E0 RID: 9184
		IntervalOffset,
		// Token: 0x040023E1 RID: 9185
		IntervalOffsetType,
		// Token: 0x040023E2 RID: 9186
		MajorTickMarks,
		// Token: 0x040023E3 RID: 9187
		MinorTickMarks,
		// Token: 0x040023E4 RID: 9188
		MarksNextToAxis,
		// Token: 0x040023E5 RID: 9189
		Reverse,
		// Token: 0x040023E6 RID: 9190
		Location,
		// Token: 0x040023E7 RID: 9191
		Interlaced,
		// Token: 0x040023E8 RID: 9192
		InterlacedColor,
		// Token: 0x040023E9 RID: 9193
		LogScale,
		// Token: 0x040023EA RID: 9194
		LogBase,
		// Token: 0x040023EB RID: 9195
		Angle,
		// Token: 0x040023EC RID: 9196
		Arrows,
		// Token: 0x040023ED RID: 9197
		AllowLabelRotation,
		// Token: 0x040023EE RID: 9198
		IncludeZero,
		// Token: 0x040023EF RID: 9199
		MinFontSize,
		// Token: 0x040023F0 RID: 9200
		MaxFontSize,
		// Token: 0x040023F1 RID: 9201
		OffsetLabels,
		// Token: 0x040023F2 RID: 9202
		AxisScaleBreak,
		// Token: 0x040023F3 RID: 9203
		Series,
		// Token: 0x040023F4 RID: 9204
		SourceChartSeriesName,
		// Token: 0x040023F5 RID: 9205
		DerivedSeriesFormula,
		// Token: 0x040023F6 RID: 9206
		DataLabel,
		// Token: 0x040023F7 RID: 9207
		AxisLabel,
		// Token: 0x040023F8 RID: 9208
		ChartItemInLegend,
		// Token: 0x040023F9 RID: 9209
		LegendText,
		// Token: 0x040023FA RID: 9210
		Length,
		// Token: 0x040023FB RID: 9211
		CustomPaletteColors,
		// Token: 0x040023FC RID: 9212
		CustomPaletteColor,
		// Token: 0x040023FD RID: 9213
		Color,
		// Token: 0x040023FE RID: 9214
		CodeParameters,
		// Token: 0x040023FF RID: 9215
		NoDataMessage,
		// Token: 0x04002400 RID: 9216
		LegendColumn,
		// Token: 0x04002401 RID: 9217
		LegendColumnHeader,
		// Token: 0x04002402 RID: 9218
		LegendCustomItems,
		// Token: 0x04002403 RID: 9219
		LegendCustomCells,
		// Token: 0x04002404 RID: 9220
		ChartStripLines,
		// Token: 0x04002405 RID: 9221
		ChartLegendColumns,
		// Token: 0x04002406 RID: 9222
		ChartLegendCustomItems,
		// Token: 0x04002407 RID: 9223
		ChartLegendCustomItemCells,
		// Token: 0x04002408 RID: 9224
		ChartDerivedSeriesCollection,
		// Token: 0x04002409 RID: 9225
		ChartFormulaParameters,
		// Token: 0x0400240A RID: 9226
		FormulaParamters,
		// Token: 0x0400240B RID: 9227
		EmptyPoints,
		// Token: 0x0400240C RID: 9228
		SmartLabel,
		// Token: 0x0400240D RID: 9229
		NoMoveDirection,
		// Token: 0x0400240E RID: 9230
		ChartLegends,
		// Token: 0x0400240F RID: 9231
		NonComputedReportItems,
		// Token: 0x04002410 RID: 9232
		ComputedReportItems,
		// Token: 0x04002411 RID: 9233
		SortedReportItems,
		// Token: 0x04002412 RID: 9234
		IsComputed,
		// Token: 0x04002413 RID: 9235
		Index,
		// Token: 0x04002414 RID: 9236
		StyleAttributes,
		// Token: 0x04002415 RID: 9237
		ExpressionList,
		// Token: 0x04002416 RID: 9238
		IsExpression,
		// Token: 0x04002417 RID: 9239
		StringValue,
		// Token: 0x04002418 RID: 9240
		BoolValue,
		// Token: 0x04002419 RID: 9241
		IntValue,
		// Token: 0x0400241A RID: 9242
		Hidden,
		// Token: 0x0400241B RID: 9243
		Toggle,
		// Token: 0x0400241C RID: 9244
		Type,
		// Token: 0x0400241D RID: 9245
		Transaction,
		// Token: 0x0400241E RID: 9246
		ConnectString,
		// Token: 0x0400241F RID: 9247
		DataSets,
		// Token: 0x04002420 RID: 9248
		DataField,
		// Token: 0x04002421 RID: 9249
		Query,
		// Token: 0x04002422 RID: 9250
		DataRegions,
		// Token: 0x04002423 RID: 9251
		CommandType,
		// Token: 0x04002424 RID: 9252
		CommandText,
		// Token: 0x04002425 RID: 9253
		QueryParameters,
		// Token: 0x04002426 RID: 9254
		Timeout,
		// Token: 0x04002427 RID: 9255
		StartHidden,
		// Token: 0x04002428 RID: 9256
		ReceiverUniqueNames,
		// Token: 0x04002429 RID: 9257
		ContainerUniqueNames,
		// Token: 0x0400242A RID: 9258
		SenderUniqueName,
		// Token: 0x0400242B RID: 9259
		Offset,
		// Token: 0x0400242C RID: 9260
		OffsetInfo,
		// Token: 0x0400242D RID: 9261
		ReportItemColInstance,
		// Token: 0x0400242E RID: 9262
		StyleAttributeValues,
		// Token: 0x0400242F RID: 9263
		RequestUserName,
		// Token: 0x04002430 RID: 9264
		BodyUniqueName,
		// Token: 0x04002431 RID: 9265
		ChildrenUniqueNames,
		// Token: 0x04002432 RID: 9266
		ReportItemInstances,
		// Token: 0x04002433 RID: 9267
		ChildrenNonComputedUniqueNames,
		// Token: 0x04002434 RID: 9268
		OriginalValue,
		// Token: 0x04002435 RID: 9269
		DocumentMap,
		// Token: 0x04002436 RID: 9270
		Nullable,
		// Token: 0x04002437 RID: 9271
		Prompt,
		// Token: 0x04002438 RID: 9272
		PromptUser,
		// Token: 0x04002439 RID: 9273
		IsUserSupplied,
		// Token: 0x0400243A RID: 9274
		QuickFind,
		// Token: 0x0400243B RID: 9275
		Images,
		// Token: 0x0400243C RID: 9276
		Bookmark,
		// Token: 0x0400243D RID: 9277
		IntegratedSecurity,
		// Token: 0x0400243E RID: 9278
		DataSourceReference,
		// Token: 0x0400243F RID: 9279
		LinkToChild,
		// Token: 0x04002440 RID: 9280
		DrillthroughReportName,
		// Token: 0x04002441 RID: 9281
		DrillthroughParameters,
		// Token: 0x04002442 RID: 9282
		BookmarkLink,
		// Token: 0x04002443 RID: 9283
		LayoutDirection,
		// Token: 0x04002444 RID: 9284
		Expression,
		// Token: 0x04002445 RID: 9285
		Operator,
		// Token: 0x04002446 RID: 9286
		Values,
		// Token: 0x04002447 RID: 9287
		Filters,
		// Token: 0x04002448 RID: 9288
		SubReports,
		// Token: 0x04002449 RID: 9289
		HasImageStreams,
		// Token: 0x0400244A RID: 9290
		IsFullSize,
		// Token: 0x0400244B RID: 9291
		HasBookmarks,
		// Token: 0x0400244C RID: 9292
		HasLabels,
		// Token: 0x0400244D RID: 9293
		ParametersNotUsedInQuery,
		// Token: 0x0400244E RID: 9294
		DrillthroughBookmarkLink,
		// Token: 0x0400244F RID: 9295
		UsedInQuery,
		// Token: 0x04002450 RID: 9296
		UsedOnlyInParameters,
		// Token: 0x04002451 RID: 9297
		AllowBlank,
		// Token: 0x04002452 RID: 9298
		MultiValue,
		// Token: 0x04002453 RID: 9299
		ValidValues,
		// Token: 0x04002454 RID: 9300
		DefaultValue,
		// Token: 0x04002455 RID: 9301
		ValidValuesDataSource,
		// Token: 0x04002456 RID: 9302
		ValidValuesValueExpression,
		// Token: 0x04002457 RID: 9303
		ValidValuesLabelExpression,
		// Token: 0x04002458 RID: 9304
		DefaultValueDataSource,
		// Token: 0x04002459 RID: 9305
		DataSourceIndex,
		// Token: 0x0400245A RID: 9306
		DataSetIndex,
		// Token: 0x0400245B RID: 9307
		ValueFieldIndex,
		// Token: 0x0400245C RID: 9308
		LabelFieldIndex,
		// Token: 0x0400245D RID: 9309
		DynamicValidValues,
		// Token: 0x0400245E RID: 9310
		DynamicDefaultValue,
		// Token: 0x0400245F RID: 9311
		DependencyList,
		// Token: 0x04002460 RID: 9312
		NonCalculatedFieldCount,
		// Token: 0x04002461 RID: 9313
		ExecutionTime,
		// Token: 0x04002462 RID: 9314
		ReportServerUrl,
		// Token: 0x04002463 RID: 9315
		ReportFolder,
		// Token: 0x04002464 RID: 9316
		Formula,
		// Token: 0x04002465 RID: 9317
		ProcessingMessages,
		// Token: 0x04002466 RID: 9318
		Code,
		// Token: 0x04002467 RID: 9319
		Severity,
		// Token: 0x04002468 RID: 9320
		ObjectType,
		// Token: 0x04002469 RID: 9321
		ObjectName,
		// Token: 0x0400246A RID: 9322
		PropertyName,
		// Token: 0x0400246B RID: 9323
		Message,
		// Token: 0x0400246C RID: 9324
		CommonCode,
		// Token: 0x0400246D RID: 9325
		ReportItemsWithHideDuplicates,
		// Token: 0x0400246E RID: 9326
		ExprHostID,
		// Token: 0x0400246F RID: 9327
		HasExprHost,
		// Token: 0x04002470 RID: 9328
		ValueReferenced,
		// Token: 0x04002471 RID: 9329
		Omit,
		// Token: 0x04002472 RID: 9330
		Parent,
		// Token: 0x04002473 RID: 9331
		PostSortAggregates,
		// Token: 0x04002474 RID: 9332
		RecursiveAggregates,
		// Token: 0x04002475 RID: 9333
		HasSpecialRecursiveAggregates,
		// Token: 0x04002476 RID: 9334
		RecursiveSender,
		// Token: 0x04002477 RID: 9335
		RecursiveReceiver,
		// Token: 0x04002478 RID: 9336
		Action,
		// Token: 0x04002479 RID: 9337
		SubType,
		// Token: 0x0400247A RID: 9338
		PointWidth,
		// Token: 0x0400247B RID: 9339
		ThreeDProperties,
		// Token: 0x0400247C RID: 9340
		DataTransform,
		// Token: 0x0400247D RID: 9341
		DataSchema,
		// Token: 0x0400247E RID: 9342
		DataElementName,
		// Token: 0x0400247F RID: 9343
		DataElementStyleAttribute,
		// Token: 0x04002480 RID: 9344
		DataElementOutput,
		// Token: 0x04002481 RID: 9345
		DataCollectionName,
		// Token: 0x04002482 RID: 9346
		Palette,
		// Token: 0x04002483 RID: 9347
		Caption,
		// Token: 0x04002484 RID: 9348
		PlotArea,
		// Token: 0x04002485 RID: 9349
		Layout,
		// Token: 0x04002486 RID: 9350
		MajorGridLines,
		// Token: 0x04002487 RID: 9351
		MinorGridLines,
		// Token: 0x04002488 RID: 9352
		MajorInterval,
		// Token: 0x04002489 RID: 9353
		MinorInterval,
		// Token: 0x0400248A RID: 9354
		ShowGridLines,
		// Token: 0x0400248B RID: 9355
		Minimum,
		// Token: 0x0400248C RID: 9356
		Maximum,
		// Token: 0x0400248D RID: 9357
		AutoScaleMin,
		// Token: 0x0400248E RID: 9358
		AutoScaleMax,
		// Token: 0x0400248F RID: 9359
		CrossAt,
		// Token: 0x04002490 RID: 9360
		AutoCrossAt,
		// Token: 0x04002491 RID: 9361
		DataValues,
		// Token: 0x04002492 RID: 9362
		DataPointValues,
		// Token: 0x04002493 RID: 9363
		X,
		// Token: 0x04002494 RID: 9364
		Y,
		// Token: 0x04002495 RID: 9365
		High,
		// Token: 0x04002496 RID: 9366
		Low,
		// Token: 0x04002497 RID: 9367
		Start,
		// Token: 0x04002498 RID: 9368
		End,
		// Token: 0x04002499 RID: 9369
		Mean,
		// Token: 0x0400249A RID: 9370
		Median,
		// Token: 0x0400249B RID: 9371
		MarkerStyleClass,
		// Token: 0x0400249C RID: 9372
		PerspectiveProjectionMode,
		// Token: 0x0400249D RID: 9373
		Rotation,
		// Token: 0x0400249E RID: 9374
		Inclination,
		// Token: 0x0400249F RID: 9375
		Perspective,
		// Token: 0x040024A0 RID: 9376
		HeightRatio,
		// Token: 0x040024A1 RID: 9377
		DepthRatio,
		// Token: 0x040024A2 RID: 9378
		Shading,
		// Token: 0x040024A3 RID: 9379
		GapDepth,
		// Token: 0x040024A4 RID: 9380
		WallThickness,
		// Token: 0x040024A5 RID: 9381
		Origin,
		// Token: 0x040024A6 RID: 9382
		InsidePlotArea,
		// Token: 0x040024A7 RID: 9383
		Enabled,
		// Token: 0x040024A8 RID: 9384
		DrawingStyleCube,
		// Token: 0x040024A9 RID: 9385
		Clustered,
		// Token: 0x040024AA RID: 9386
		Scalar,
		// Token: 0x040024AB RID: 9387
		PlotTypesLine,
		// Token: 0x040024AC RID: 9388
		CultureName,
		// Token: 0x040024AD RID: 9389
		StartPage,
		// Token: 0x040024AE RID: 9390
		EndPage,
		// Token: 0x040024AF RID: 9391
		NumberOfPages,
		// Token: 0x040024B0 RID: 9392
		Page,
		// Token: 0x040024B1 RID: 9393
		IntermediateFormatVersionMajor,
		// Token: 0x040024B2 RID: 9394
		IntermediateFormatVersionMinor,
		// Token: 0x040024B3 RID: 9395
		IntermediateFormatVersionBuild,
		// Token: 0x040024B4 RID: 9396
		ReportVersion,
		// Token: 0x040024B5 RID: 9397
		StreamName,
		// Token: 0x040024B6 RID: 9398
		ActionItem,
		// Token: 0x040024B7 RID: 9399
		ActionItemList,
		// Token: 0x040024B8 RID: 9400
		CustomProperties,
		// Token: 0x040024B9 RID: 9401
		HasDocumentMap,
		// Token: 0x040024BA RID: 9402
		HasShowHide,
		// Token: 0x040024BB RID: 9403
		DocMapPage,
		// Token: 0x040024BC RID: 9404
		RenderReportItemColDef,
		// Token: 0x040024BD RID: 9405
		EventSource,
		// Token: 0x040024BE RID: 9406
		EventSourceScopeInfo,
		// Token: 0x040024BF RID: 9407
		DataSetID,
		// Token: 0x040024C0 RID: 9408
		ContainingScopes,
		// Token: 0x040024C1 RID: 9409
		UserSort,
		// Token: 0x040024C2 RID: 9410
		SortExpressionScope,
		// Token: 0x040024C3 RID: 9411
		GroupsInSortTarget,
		// Token: 0x040024C4 RID: 9412
		SortTarget,
		// Token: 0x040024C5 RID: 9413
		HasDetailUserSortFilter,
		// Token: 0x040024C6 RID: 9414
		SaveGroupExprValues,
		// Token: 0x040024C7 RID: 9415
		HasUserSortFilter,
		// Token: 0x040024C8 RID: 9416
		IsTablixCellScope,
		// Token: 0x040024C9 RID: 9417
		UserSortExpressions,
		// Token: 0x040024CA RID: 9418
		SortExpressionIndex,
		// Token: 0x040024CB RID: 9419
		CommandTextValue,
		// Token: 0x040024CC RID: 9420
		SharedDataSourceReferencePath,
		// Token: 0x040024CD RID: 9421
		DynamicFieldReferences,
		// Token: 0x040024CE RID: 9422
		DynamicPropertyReferences,
		// Token: 0x040024CF RID: 9423
		ReferencedProperties,
		// Token: 0x040024D0 RID: 9424
		CompiledCodeGeneratedWithRefusedPermissions,
		// Token: 0x040024D1 RID: 9425
		InteractiveHeight,
		// Token: 0x040024D2 RID: 9426
		InteractiveHeightValue,
		// Token: 0x040024D3 RID: 9427
		InteractiveWidth,
		// Token: 0x040024D4 RID: 9428
		InteractiveWidthValue,
		// Token: 0x040024D5 RID: 9429
		PageNumber,
		// Token: 0x040024D6 RID: 9430
		DetailScopeSubReports,
		// Token: 0x040024D7 RID: 9431
		DataSetUniqueNameMap,
		// Token: 0x040024D8 RID: 9432
		LookupTable,
		// Token: 0x040024D9 RID: 9433
		LookupInt,
		// Token: 0x040024DA RID: 9434
		IsSubReportTopLevelScope,
		// Token: 0x040024DB RID: 9435
		NonDetailSortFiltersInScope,
		// Token: 0x040024DC RID: 9436
		DetailSortFiltersInScope,
		// Token: 0x040024DD RID: 9437
		DrillthroughHashtable,
		// Token: 0x040024DE RID: 9438
		RewrittenCommands,
		// Token: 0x040024DF RID: 9439
		PageBreakLocation,
		// Token: 0x040024E0 RID: 9440
		PropagatedPageBreakLocation,
		// Token: 0x040024E1 RID: 9441
		Tablix,
		// Token: 0x040024E2 RID: 9442
		TablixHeader,
		// Token: 0x040024E3 RID: 9443
		TablixRow,
		// Token: 0x040024E4 RID: 9444
		TablixRows,
		// Token: 0x040024E5 RID: 9445
		TablixCell,
		// Token: 0x040024E6 RID: 9446
		TablixCells,
		// Token: 0x040024E7 RID: 9447
		CellContents,
		// Token: 0x040024E8 RID: 9448
		TablixColumn,
		// Token: 0x040024E9 RID: 9449
		TablixColumns,
		// Token: 0x040024EA RID: 9450
		TablixMember,
		// Token: 0x040024EB RID: 9451
		TablixMembers,
		// Token: 0x040024EC RID: 9452
		TablixColumnMembers,
		// Token: 0x040024ED RID: 9453
		TablixRowMembers,
		// Token: 0x040024EE RID: 9454
		TablixCornerCells,
		// Token: 0x040024EF RID: 9455
		RepeatColumnHeaders,
		// Token: 0x040024F0 RID: 9456
		RepeatRowHeaders,
		// Token: 0x040024F1 RID: 9457
		FixedColumnHeaders,
		// Token: 0x040024F2 RID: 9458
		FixedRowHeaders,
		// Token: 0x040024F3 RID: 9459
		KeepWithGroup,
		// Token: 0x040024F4 RID: 9460
		FixedData,
		// Token: 0x040024F5 RID: 9461
		NoRowsMessage,
		// Token: 0x040024F6 RID: 9462
		ColSpan,
		// Token: 0x040024F7 RID: 9463
		RowSpan,
		// Token: 0x040024F8 RID: 9464
		AutoSubtotal,
		// Token: 0x040024F9 RID: 9465
		MemberCellIndex,
		// Token: 0x040024FA RID: 9466
		CategoryMembers,
		// Token: 0x040024FB RID: 9467
		SeriesMembers,
		// Token: 0x040024FC RID: 9468
		ChartSeries,
		// Token: 0x040024FD RID: 9469
		ChartDataPoints,
		// Token: 0x040024FE RID: 9470
		Subtype,
		// Token: 0x040024FF RID: 9471
		LegendName,
		// Token: 0x04002500 RID: 9472
		ChartAreaName,
		// Token: 0x04002501 RID: 9473
		ValueAxisName,
		// Token: 0x04002502 RID: 9474
		CategoryAxisName,
		// Token: 0x04002503 RID: 9475
		PlotAsLine,
		// Token: 0x04002504 RID: 9476
		DataColumnMembers,
		// Token: 0x04002505 RID: 9477
		DataRowMembers,
		// Token: 0x04002506 RID: 9478
		DataMembers,
		// Token: 0x04002507 RID: 9479
		DataRows,
		// Token: 0x04002508 RID: 9480
		AltReportItem,
		// Token: 0x04002509 RID: 9481
		OmitBorderOnPageBreak,
		// Token: 0x0400250A RID: 9482
		DynamicHeight,
		// Token: 0x0400250B RID: 9483
		DynamicWidth,
		// Token: 0x0400250C RID: 9484
		ReGroupExpressions,
		// Token: 0x0400250D RID: 9485
		DeferVariableEvaluation,
		// Token: 0x0400250E RID: 9486
		ExpressionInfoTypeValuePair,
		// Token: 0x0400250F RID: 9487
		HideIfNoRows,
		// Token: 0x04002510 RID: 9488
		InterpretSubtotalsAsDetails,
		// Token: 0x04002511 RID: 9489
		ValueAxes,
		// Token: 0x04002512 RID: 9490
		CategoryAxes,
		// Token: 0x04002513 RID: 9491
		ChartMembers,
		// Token: 0x04002514 RID: 9492
		AngleValue,
		// Token: 0x04002515 RID: 9493
		PlotType,
		// Token: 0x04002516 RID: 9494
		ParentRowID,
		// Token: 0x04002517 RID: 9495
		ParentColumnID,
		// Token: 0x04002518 RID: 9496
		DynamicPrompt,
		// Token: 0x04002519 RID: 9497
		HasSubReports,
		// Token: 0x0400251A RID: 9498
		HideStaticsIfNoRows,
		// Token: 0x0400251B RID: 9499
		InScopeTextBoxes,
		// Token: 0x0400251C RID: 9500
		ColumnHeaderRowCount,
		// Token: 0x0400251D RID: 9501
		RowHeaderColumnCount,
		// Token: 0x0400251E RID: 9502
		IndexInCollection,
		// Token: 0x0400251F RID: 9503
		ReportSnapshot,
		// Token: 0x04002520 RID: 9504
		FirstDataSet,
		// Token: 0x04002521 RID: 9505
		DataSetNoRows,
		// Token: 0x04002522 RID: 9506
		DataSetRecordSetSizes,
		// Token: 0x04002523 RID: 9507
		ErrorOccurred,
		// Token: 0x04002524 RID: 9508
		HasCode,
		// Token: 0x04002525 RID: 9509
		GroupTreePartitionOffsets,
		// Token: 0x04002526 RID: 9510
		TopLevelScopeInstances,
		// Token: 0x04002527 RID: 9511
		ToggleSender,
		// Token: 0x04002528 RID: 9512
		Names,
		// Token: 0x04002529 RID: 9513
		Count,
		// Token: 0x0400252A RID: 9514
		IsMultiValue,
		// Token: 0x0400252B RID: 9515
		LockAdd,
		// Token: 0x0400252C RID: 9516
		SubReportInfos,
		// Token: 0x0400252D RID: 9517
		Computed,
		// Token: 0x0400252E RID: 9518
		RepeatOnNewPage,
		// Token: 0x0400252F RID: 9519
		TopLeftDataRegion,
		// Token: 0x04002530 RID: 9520
		HasInnerGroupTreeHierarchy,
		// Token: 0x04002531 RID: 9521
		ImageChunkNames,
		// Token: 0x04002532 RID: 9522
		DataSetsNotOnlyUsedInParameters,
		// Token: 0x04002533 RID: 9523
		ChartStyleContainer,
		// Token: 0x04002534 RID: 9524
		Chart,
		// Token: 0x04002535 RID: 9525
		ChartDataPoint,
		// Token: 0x04002536 RID: 9526
		AggregateIndexes,
		// Token: 0x04002537 RID: 9527
		PostSortAggregateIndexes,
		// Token: 0x04002538 RID: 9528
		RunningValueIndexes,
		// Token: 0x04002539 RID: 9529
		AltCellContents,
		// Token: 0x0400253A RID: 9530
		ROMIndexMap,
		// Token: 0x0400253B RID: 9531
		AltReportItemIndexInParentCollectionDef,
		// Token: 0x0400253C RID: 9532
		InPrevious,
		// Token: 0x0400253D RID: 9533
		NeedToCacheDataRows,
		// Token: 0x0400253E RID: 9534
		HasPreviousAggregates,
		// Token: 0x0400253F RID: 9535
		BreakLineType,
		// Token: 0x04002540 RID: 9536
		CollapsibleSpaceThreshold,
		// Token: 0x04002541 RID: 9537
		MaxNumberOfBreaks,
		// Token: 0x04002542 RID: 9538
		Spacing,
		// Token: 0x04002543 RID: 9539
		BorderSkinType,
		// Token: 0x04002544 RID: 9540
		TopLevelDataRegionIndexes,
		// Token: 0x04002545 RID: 9541
		InScopeEventSources,
		// Token: 0x04002546 RID: 9542
		InScopeTextBoxesInPage,
		// Token: 0x04002547 RID: 9543
		InScopeTextBoxesInBody,
		// Token: 0x04002548 RID: 9544
		CachedExternalImages,
		// Token: 0x04002549 RID: 9545
		TransparentImageChunkName,
		// Token: 0x0400254A RID: 9546
		DataChunkName,
		// Token: 0x0400254B RID: 9547
		DataChunkMap,
		// Token: 0x0400254C RID: 9548
		EventSources,
		// Token: 0x0400254D RID: 9549
		DataSet,
		// Token: 0x0400254E RID: 9550
		ChunkNameModifier,
		// Token: 0x0400254F RID: 9551
		ReferenceID,
		// Token: 0x04002550 RID: 9552
		DataSource,
		// Token: 0x04002551 RID: 9553
		RetrievalFailed,
		// Token: 0x04002552 RID: 9554
		ContainingDynamicVisibility,
		// Token: 0x04002553 RID: 9555
		ContainingDynamicRowVisibility,
		// Token: 0x04002554 RID: 9556
		ContainingDynamicColumnVisibility,
		// Token: 0x04002555 RID: 9557
		ImageData,
		// Token: 0x04002556 RID: 9558
		Actions,
		// Token: 0x04002557 RID: 9559
		ActionDefinition,
		// Token: 0x04002558 RID: 9560
		ImageMapAreas,
		// Token: 0x04002559 RID: 9561
		Shape,
		// Token: 0x0400255A RID: 9562
		Coordinates,
		// Token: 0x0400255B RID: 9563
		Style,
		// Token: 0x0400255C RID: 9564
		CustomPropertyNames,
		// Token: 0x0400255D RID: 9565
		CustomPropertyValues,
		// Token: 0x0400255E RID: 9566
		GeneratedReportItemChunkNames,
		// Token: 0x0400255F RID: 9567
		ChartSmartLabel,
		// Token: 0x04002560 RID: 9568
		InDynamicRowAndColumnContext,
		// Token: 0x04002561 RID: 9569
		SortFilterEventInfos,
		// Token: 0x04002562 RID: 9570
		OuterGroupingMaximumDynamicLevel,
		// Token: 0x04002563 RID: 9571
		OuterGroupingDynamicMemberCount,
		// Token: 0x04002564 RID: 9572
		OuterGroupingDynamicPathCount,
		// Token: 0x04002565 RID: 9573
		HierarchyDynamicIndex,
		// Token: 0x04002566 RID: 9574
		HierarchyPathIndex,
		// Token: 0x04002567 RID: 9575
		AggregateFieldReferences,
		// Token: 0x04002568 RID: 9576
		TrackFieldsUsedInValueExpression,
		// Token: 0x04002569 RID: 9577
		Docking,
		// Token: 0x0400256A RID: 9578
		DockToChartArea,
		// Token: 0x0400256B RID: 9579
		DockOutsideChartArea,
		// Token: 0x0400256C RID: 9580
		DockOffset,
		// Token: 0x0400256D RID: 9581
		TitleSeparator,
		// Token: 0x0400256E RID: 9582
		AlignOrientation,
		// Token: 0x0400256F RID: 9583
		ChartAlignType,
		// Token: 0x04002570 RID: 9584
		AlignWithChartArea,
		// Token: 0x04002571 RID: 9585
		EquallySizedAxesFont,
		// Token: 0x04002572 RID: 9586
		Cursor,
		// Token: 0x04002573 RID: 9587
		AxesView,
		// Token: 0x04002574 RID: 9588
		InnerPlotPosition,
		// Token: 0x04002575 RID: 9589
		ChartLegendTitle,
		// Token: 0x04002576 RID: 9590
		AutoFitTextDisabled,
		// Token: 0x04002577 RID: 9591
		HeaderSeparator,
		// Token: 0x04002578 RID: 9592
		HeaderSeparatorColor,
		// Token: 0x04002579 RID: 9593
		ColumnSeparator,
		// Token: 0x0400257A RID: 9594
		ColumnSeparatorColor,
		// Token: 0x0400257B RID: 9595
		InterlacedRows,
		// Token: 0x0400257C RID: 9596
		InterlacedRowsColor,
		// Token: 0x0400257D RID: 9597
		EquallySpacedItems,
		// Token: 0x0400257E RID: 9598
		Reversed,
		// Token: 0x0400257F RID: 9599
		MaxAutoSize,
		// Token: 0x04002580 RID: 9600
		TextWrapThreshold,
		// Token: 0x04002581 RID: 9601
		ShowOverlapped,
		// Token: 0x04002582 RID: 9602
		HideInLegend,
		// Token: 0x04002583 RID: 9603
		UseValueAsLabel,
		// Token: 0x04002584 RID: 9604
		ProjectionMode,
		// Token: 0x04002585 RID: 9605
		MarksAlwaysAtPlotEdge,
		// Token: 0x04002586 RID: 9606
		HideLabels,
		// Token: 0x04002587 RID: 9607
		PreventFontShrink,
		// Token: 0x04002588 RID: 9608
		PreventFontGrow,
		// Token: 0x04002589 RID: 9609
		PreventLabelOffset,
		// Token: 0x0400258A RID: 9610
		PreventWordWrap,
		// Token: 0x0400258B RID: 9611
		LabelsAutoFitDisabled,
		// Token: 0x0400258C RID: 9612
		HideEndLabels,
		// Token: 0x0400258D RID: 9613
		VariableAutoInterval,
		// Token: 0x0400258E RID: 9614
		LabelInterval,
		// Token: 0x0400258F RID: 9615
		LabelIntervalType,
		// Token: 0x04002590 RID: 9616
		LabelIntervalOffset,
		// Token: 0x04002591 RID: 9617
		LabelIntervalOffsetType,
		// Token: 0x04002592 RID: 9618
		IsMajor,
		// Token: 0x04002593 RID: 9619
		GroupsWithVariables,
		// Token: 0x04002594 RID: 9620
		InstanceParameterValues,
		// Token: 0x04002595 RID: 9621
		HierarchyParentGroups,
		// Token: 0x04002596 RID: 9622
		InnerGroupingMaximumDynamicLevel,
		// Token: 0x04002597 RID: 9623
		InnerGroupingDynamicMemberCount,
		// Token: 0x04002598 RID: 9624
		InnerGroupingDynamicPathCount,
		// Token: 0x04002599 RID: 9625
		GroupTreeRootOffset,
		// Token: 0x0400259A RID: 9626
		Item,
		// Token: 0x0400259B RID: 9627
		Priority,
		// Token: 0x0400259C RID: 9628
		AggregateRows,
		// Token: 0x0400259D RID: 9629
		SortFilterInfoIndices,
		// Token: 0x0400259E RID: 9630
		TargetForNonDetailSort,
		// Token: 0x0400259F RID: 9631
		TargetForDetailSort,
		// Token: 0x040025A0 RID: 9632
		FirstCell,
		// Token: 0x040025A1 RID: 9633
		LastCell,
		// Token: 0x040025A2 RID: 9634
		CurrentMin,
		// Token: 0x040025A3 RID: 9635
		ReportItemsDef,
		// Token: 0x040025A4 RID: 9636
		CellReportItemDef,
		// Token: 0x040025A5 RID: 9637
		CellAltReportItemDef,
		// Token: 0x040025A6 RID: 9638
		ReportItemDef,
		// Token: 0x040025A7 RID: 9639
		DataRegionObjs,
		// Token: 0x040025A8 RID: 9640
		CurrentDataRegion,
		// Token: 0x040025A9 RID: 9641
		ProcessSubreports,
		// Token: 0x040025AA RID: 9642
		OldUniqueName,
		// Token: 0x040025AB RID: 9643
		SortSourceScopeInfo,
		// Token: 0x040025AC RID: 9644
		SortDirection,
		// Token: 0x040025AD RID: 9645
		EventSourceRowScope,
		// Token: 0x040025AE RID: 9646
		EventSourceColDetailIndex,
		// Token: 0x040025AF RID: 9647
		EventSourceRowDetailIndex,
		// Token: 0x040025B0 RID: 9648
		DetailRowScopes,
		// Token: 0x040025B1 RID: 9649
		DetailRowScopeIndices,
		// Token: 0x040025B2 RID: 9650
		DetailColScopeIndices,
		// Token: 0x040025B3 RID: 9651
		EventTarget,
		// Token: 0x040025B4 RID: 9652
		TargetSortFilterInfoAdded,
		// Token: 0x040025B5 RID: 9653
		GroupExpressionsInSortTarget,
		// Token: 0x040025B6 RID: 9654
		SortFilterExpressionScopeObjects,
		// Token: 0x040025B7 RID: 9655
		CurrentSortIndex,
		// Token: 0x040025B8 RID: 9656
		CurrentInstanceIndex,
		// Token: 0x040025B9 RID: 9657
		SortOrders,
		// Token: 0x040025BA RID: 9658
		Processed,
		// Token: 0x040025BB RID: 9659
		NullScopeCount,
		// Token: 0x040025BC RID: 9660
		NewUniqueName,
		// Token: 0x040025BD RID: 9661
		PeerSortFilters,
		// Token: 0x040025BE RID: 9662
		Direction,
		// Token: 0x040025BF RID: 9663
		ExpressionsHost,
		// Token: 0x040025C0 RID: 9664
		ExpressionIndex,
		// Token: 0x040025C1 RID: 9665
		HierarchyObjs,
		// Token: 0x040025C2 RID: 9666
		Hashtable,
		// Token: 0x040025C3 RID: 9667
		Tree,
		// Token: 0x040025C4 RID: 9668
		ParentInfo,
		// Token: 0x040025C5 RID: 9669
		FirstChild,
		// Token: 0x040025C6 RID: 9670
		LastChild,
		// Token: 0x040025C7 RID: 9671
		RvValueList,
		// Token: 0x040025C8 RID: 9672
		RunningValuesInGroup,
		// Token: 0x040025C9 RID: 9673
		PreviousValuesInGroup,
		// Token: 0x040025CA RID: 9674
		GlobalRunningValueCollection,
		// Token: 0x040025CB RID: 9675
		GroupCollection,
		// Token: 0x040025CC RID: 9676
		ProcessingStage,
		// Token: 0x040025CD RID: 9677
		ScopedRunningValues,
		// Token: 0x040025CE RID: 9678
		ParentInstance,
		// Token: 0x040025CF RID: 9679
		GroupingType,
		// Token: 0x040025D0 RID: 9680
		ParentExpression,
		// Token: 0x040025D1 RID: 9681
		CurrentGroupExprValue,
		// Token: 0x040025D2 RID: 9682
		BuiltinSortOverridden,
		// Token: 0x040025D3 RID: 9683
		IsDetailGroup,
		// Token: 0x040025D4 RID: 9684
		HierarchyDef,
		// Token: 0x040025D5 RID: 9685
		Collection,
		// Token: 0x040025D6 RID: 9686
		ErrorContext,
		// Token: 0x040025D7 RID: 9687
		InnerGroupings,
		// Token: 0x040025D8 RID: 9688
		OutermostStaticCellRvs,
		// Token: 0x040025D9 RID: 9689
		HeadingLevel,
		// Token: 0x040025DA RID: 9690
		OutermostStatics,
		// Token: 0x040025DB RID: 9691
		HasLeafCells,
		// Token: 0x040025DC RID: 9692
		ProcessOutermostStaticCells,
		// Token: 0x040025DD RID: 9693
		CurrentMemberIndexWithinScopeLevel,
		// Token: 0x040025DE RID: 9694
		CurrentMemberInstance,
		// Token: 0x040025DF RID: 9695
		GroupRoot,
		// Token: 0x040025E0 RID: 9696
		HasStaticMembers,
		// Token: 0x040025E1 RID: 9697
		StaticLeafCellIndexes,
		// Token: 0x040025E2 RID: 9698
		StaticHeadings,
		// Token: 0x040025E3 RID: 9699
		OuterGroupingCounters,
		// Token: 0x040025E4 RID: 9700
		OuterGroupings,
		// Token: 0x040025E5 RID: 9701
		InnerGroupsWithCellsForOuterPeerGroupProcessing,
		// Token: 0x040025E6 RID: 9702
		StaticCorner,
		// Token: 0x040025E7 RID: 9703
		StaticCornerCells,
		// Token: 0x040025E8 RID: 9704
		TablixCorner,
		// Token: 0x040025E9 RID: 9705
		DataRegionScopedItems,
		// Token: 0x040025EA RID: 9706
		NextLeaf,
		// Token: 0x040025EB RID: 9707
		PrevLeaf,
		// Token: 0x040025EC RID: 9708
		GroupExprValues,
		// Token: 0x040025ED RID: 9709
		TargetScopeMatched,
		// Token: 0x040025EE RID: 9710
		MemberObjs,
		// Token: 0x040025EF RID: 9711
		HasInnerHierarchy,
		// Token: 0x040025F0 RID: 9712
		FirstPassCellNonCustomAggs,
		// Token: 0x040025F1 RID: 9713
		FirstPassCellCustomAggs,
		// Token: 0x040025F2 RID: 9714
		CellsList,
		// Token: 0x040025F3 RID: 9715
		GroupLeafIndex,
		// Token: 0x040025F4 RID: 9716
		ProcessHeading,
		// Token: 0x040025F5 RID: 9717
		MemberInstance,
		// Token: 0x040025F6 RID: 9718
		SequentialMemberIndexWithinScopeLevel,
		// Token: 0x040025F7 RID: 9719
		OutermostColumnIndexes,
		// Token: 0x040025F8 RID: 9720
		OutermostRowIndexes,
		// Token: 0x040025F9 RID: 9721
		CellRunningValueValues,
		// Token: 0x040025FA RID: 9722
		HeadingReportItemCol,
		// Token: 0x040025FB RID: 9723
		GroupScopedItems,
		// Token: 0x040025FC RID: 9724
		SortInfo,
		// Token: 0x040025FD RID: 9725
		SortIndex,
		// Token: 0x040025FE RID: 9726
		ValidAggregateRow,
		// Token: 0x040025FF RID: 9727
		ContainedType,
		// Token: 0x04002600 RID: 9728
		IsValueReady,
		// Token: 0x04002601 RID: 9729
		IsVisited,
		// Token: 0x04002602 RID: 9730
		Array,
		// Token: 0x04002603 RID: 9731
		Keys,
		// Token: 0x04002604 RID: 9732
		BucketSize,
		// Token: 0x04002605 RID: 9733
		Buckets,
		// Token: 0x04002606 RID: 9734
		Version,
		// Token: 0x04002607 RID: 9735
		Entries,
		// Token: 0x04002608 RID: 9736
		Prime,
		// Token: 0x04002609 RID: 9737
		HashInputA,
		// Token: 0x0400260A RID: 9738
		HashInputB,
		// Token: 0x0400260B RID: 9739
		NodeCapacity,
		// Token: 0x0400260C RID: 9740
		ValuesCapacity,
		// Token: 0x0400260D RID: 9741
		Comparer,
		// Token: 0x0400260E RID: 9742
		Root,
		// Token: 0x0400260F RID: 9743
		UseFixedReferences,
		// Token: 0x04002610 RID: 9744
		Depth,
		// Token: 0x04002611 RID: 9745
		SortTreeNodes,
		// Token: 0x04002612 RID: 9746
		PreviousValues,
		// Token: 0x04002613 RID: 9747
		StartIndex,
		// Token: 0x04002614 RID: 9748
		PreviousEnabled,
		// Token: 0x04002615 RID: 9749
		HasNoExplicitScope,
		// Token: 0x04002616 RID: 9750
		StaticCellRunningValues,
		// Token: 0x04002617 RID: 9751
		CellPreviousValues,
		// Token: 0x04002618 RID: 9752
		StaticCellPreviousValues,
		// Token: 0x04002619 RID: 9753
		DetailRowIndex,
		// Token: 0x0400261A RID: 9754
		DetailUserSortTargetInfo,
		// Token: 0x0400261B RID: 9755
		InstanceIndex,
		// Token: 0x0400261C RID: 9756
		RecursiveParentIndexes,
		// Token: 0x0400261D RID: 9757
		IsOuterGrouping,
		// Token: 0x0400261E RID: 9758
		State,
		// Token: 0x0400261F RID: 9759
		Fixed,
		// Token: 0x04002620 RID: 9760
		SpanSize,
		// Token: 0x04002621 RID: 9761
		DefIndex,
		// Token: 0x04002622 RID: 9762
		MemberCell,
		// Token: 0x04002623 RID: 9763
		DeltaX,
		// Token: 0x04002624 RID: 9764
		DeltaY,
		// Token: 0x04002625 RID: 9765
		ItemPageSizes,
		// Token: 0x04002626 RID: 9766
		ItemsAbove,
		// Token: 0x04002627 RID: 9767
		ItemsLeft,
		// Token: 0x04002628 RID: 9768
		RPLElement,
		// Token: 0x04002629 RID: 9769
		RPLState,
		// Token: 0x0400262A RID: 9770
		NonSharedOffset,
		// Token: 0x0400262B RID: 9771
		TextBoxValues,
		// Token: 0x0400262C RID: 9772
		Indexes,
		// Token: 0x0400262D RID: 9773
		Padding,
		// Token: 0x0400262E RID: 9774
		DefPadding,
		// Token: 0x0400262F RID: 9775
		BodySource,
		// Token: 0x04002630 RID: 9776
		ChildBody,
		// Token: 0x04002631 RID: 9777
		InvalidImage,
		// Token: 0x04002632 RID: 9778
		ImageProps,
		// Token: 0x04002633 RID: 9779
		HorizontalPadding,
		// Token: 0x04002634 RID: 9780
		VerticalPadding,
		// Token: 0x04002635 RID: 9781
		ValueStart,
		// Token: 0x04002636 RID: 9782
		ValueEnd,
		// Token: 0x04002637 RID: 9783
		CanvasFont,
		// Token: 0x04002638 RID: 9784
		CalcSizeState,
		// Token: 0x04002639 RID: 9785
		HorizontalState,
		// Token: 0x0400263A RID: 9786
		VerticalState,
		// Token: 0x0400263B RID: 9787
		TablixState,
		// Token: 0x0400263C RID: 9788
		StartPos,
		// Token: 0x0400263D RID: 9789
		RPLTablixRow,
		// Token: 0x0400263E RID: 9790
		DetailCells,
		// Token: 0x0400263F RID: 9791
		SourceHeight,
		// Token: 0x04002640 RID: 9792
		RowHeight,
		// Token: 0x04002641 RID: 9793
		BodyRows,
		// Token: 0x04002642 RID: 9794
		BodyRowHeights,
		// Token: 0x04002643 RID: 9795
		BodyColWidths,
		// Token: 0x04002644 RID: 9796
		RowMembersDepth,
		// Token: 0x04002645 RID: 9797
		ColMembersDepth,
		// Token: 0x04002646 RID: 9798
		RowMemberDef,
		// Token: 0x04002647 RID: 9799
		RowMemberDefIndexes,
		// Token: 0x04002648 RID: 9800
		ColMemberDef,
		// Token: 0x04002649 RID: 9801
		ColMemberDefIndexes,
		// Token: 0x0400264A RID: 9802
		CellPageBreaks,
		// Token: 0x0400264B RID: 9803
		HeaderRowCols,
		// Token: 0x0400264C RID: 9804
		HeaderColumnRows,
		// Token: 0x0400264D RID: 9805
		RowMemberIndexCell,
		// Token: 0x0400264E RID: 9806
		ColMemberIndexCell,
		// Token: 0x0400264F RID: 9807
		ColsBeforeRowHeaders,
		// Token: 0x04002650 RID: 9808
		ColumnHeaders,
		// Token: 0x04002651 RID: 9809
		ColumnHeadersHeights,
		// Token: 0x04002652 RID: 9810
		RowHeaders,
		// Token: 0x04002653 RID: 9811
		RowHeadersWidths,
		// Token: 0x04002654 RID: 9812
		DetailRows,
		// Token: 0x04002655 RID: 9813
		CornerCells,
		// Token: 0x04002656 RID: 9814
		GroupPageBreaks,
		// Token: 0x04002657 RID: 9815
		ColumnInfo,
		// Token: 0x04002658 RID: 9816
		IgnoreCol,
		// Token: 0x04002659 RID: 9817
		IgnoreRow,
		// Token: 0x0400265A RID: 9818
		ContentOnPage,
		// Token: 0x0400265B RID: 9819
		CellItem,
		// Token: 0x0400265C RID: 9820
		MemberItem,
		// Token: 0x0400265D RID: 9821
		CurrRowSpan,
		// Token: 0x0400265E RID: 9822
		CurrColSpan,
		// Token: 0x0400265F RID: 9823
		MemberState,
		// Token: 0x04002660 RID: 9824
		SourceIndex,
		// Token: 0x04002661 RID: 9825
		Span,
		// Token: 0x04002662 RID: 9826
		MemberInstances,
		// Token: 0x04002663 RID: 9827
		RepeatWith,
		// Token: 0x04002664 RID: 9828
		IsSimple,
		// Token: 0x04002665 RID: 9829
		ConsumeContainerWhitespace,
		// Token: 0x04002666 RID: 9830
		FloatValue,
		// Token: 0x04002667 RID: 9831
		DateTimeValue,
		// Token: 0x04002668 RID: 9832
		ConstantType,
		// Token: 0x04002669 RID: 9833
		DataValueSequenceRendering,
		// Token: 0x0400266A RID: 9834
		CommonSubReportInfo,
		// Token: 0x0400266B RID: 9835
		CommonSubReportInfos,
		// Token: 0x0400266C RID: 9836
		DefinitionUniqueName,
		// Token: 0x0400266D RID: 9837
		FirstInstanceSet,
		// Token: 0x0400266E RID: 9838
		VariablesInScope,
		// Token: 0x0400266F RID: 9839
		FlattenedDatasetDependencyMatrix,
		// Token: 0x04002670 RID: 9840
		FirstDataSetIndexToProcess,
		// Token: 0x04002671 RID: 9841
		SnapshotParameters,
		// Token: 0x04002672 RID: 9842
		UserProfileState,
		// Token: 0x04002673 RID: 9843
		SequenceID,
		// Token: 0x04002674 RID: 9844
		TextboxesInScope,
		// Token: 0x04002675 RID: 9845
		GaugePanel,
		// Token: 0x04002676 RID: 9846
		GaugeMember,
		// Token: 0x04002677 RID: 9847
		GaugeRowMember,
		// Token: 0x04002678 RID: 9848
		GaugeRow,
		// Token: 0x04002679 RID: 9849
		GaugeCell,
		// Token: 0x0400267A RID: 9850
		TransparentColor,
		// Token: 0x0400267B RID: 9851
		HueColor,
		// Token: 0x0400267C RID: 9852
		OffsetX,
		// Token: 0x0400267D RID: 9853
		OffsetY,
		// Token: 0x0400267E RID: 9854
		Transparency,
		// Token: 0x0400267F RID: 9855
		ClipImage,
		// Token: 0x04002680 RID: 9856
		FrameStyle,
		// Token: 0x04002681 RID: 9857
		FrameShape,
		// Token: 0x04002682 RID: 9858
		FrameWidth,
		// Token: 0x04002683 RID: 9859
		GlassEffect,
		// Token: 0x04002684 RID: 9860
		FrameBackground,
		// Token: 0x04002685 RID: 9861
		FrameImage,
		// Token: 0x04002686 RID: 9862
		AllowUpsideDown,
		// Token: 0x04002687 RID: 9863
		RotateLabels,
		// Token: 0x04002688 RID: 9864
		TickMarkStyle,
		// Token: 0x04002689 RID: 9865
		BackFrame,
		// Token: 0x0400268A RID: 9866
		ClipContent,
		// Token: 0x0400268B RID: 9867
		TopImage,
		// Token: 0x0400268C RID: 9868
		MinPercent,
		// Token: 0x0400268D RID: 9869
		MaxPercent,
		// Token: 0x0400268E RID: 9870
		AddConstant,
		// Token: 0x0400268F RID: 9871
		LinearGauges,
		// Token: 0x04002690 RID: 9872
		RadialGauges,
		// Token: 0x04002691 RID: 9873
		NumericIndicators,
		// Token: 0x04002692 RID: 9874
		StateIndicators,
		// Token: 0x04002693 RID: 9875
		GaugeImages,
		// Token: 0x04002694 RID: 9876
		GaugeLabels,
		// Token: 0x04002695 RID: 9877
		AntiAliasing,
		// Token: 0x04002696 RID: 9878
		AutoLayout,
		// Token: 0x04002697 RID: 9879
		ShadowIntensity,
		// Token: 0x04002698 RID: 9880
		TextAntiAliasingQuality,
		// Token: 0x04002699 RID: 9881
		ParentItem,
		// Token: 0x0400269A RID: 9882
		GaugeInputValue,
		// Token: 0x0400269B RID: 9883
		BarStart,
		// Token: 0x0400269C RID: 9884
		DistanceFromScale,
		// Token: 0x0400269D RID: 9885
		PointerImage,
		// Token: 0x0400269E RID: 9886
		MarkerLength,
		// Token: 0x0400269F RID: 9887
		MarkerStyle,
		// Token: 0x040026A0 RID: 9888
		SnappingEnabled,
		// Token: 0x040026A1 RID: 9889
		SnappingInterval,
		// Token: 0x040026A2 RID: 9890
		ScaleRanges,
		// Token: 0x040026A3 RID: 9891
		CustomLabels,
		// Token: 0x040026A4 RID: 9892
		Logarithmic,
		// Token: 0x040026A5 RID: 9893
		LogarithmicBase,
		// Token: 0x040026A6 RID: 9894
		MaximumValue,
		// Token: 0x040026A7 RID: 9895
		MinimumValue,
		// Token: 0x040026A8 RID: 9896
		Multiplier,
		// Token: 0x040026A9 RID: 9897
		GaugeMajorTickMarks,
		// Token: 0x040026AA RID: 9898
		GaugeMinorTickMarks,
		// Token: 0x040026AB RID: 9899
		MaximumPin,
		// Token: 0x040026AC RID: 9900
		MinimumPin,
		// Token: 0x040026AD RID: 9901
		ScaleLabels,
		// Token: 0x040026AE RID: 9902
		TickMarksOnTop,
		// Token: 0x040026AF RID: 9903
		Orientation,
		// Token: 0x040026B0 RID: 9904
		Thermometer,
		// Token: 0x040026B1 RID: 9905
		StartMargin,
		// Token: 0x040026B2 RID: 9906
		EndMargin,
		// Token: 0x040026B3 RID: 9907
		Placement,
		// Token: 0x040026B4 RID: 9908
		RotateLabel,
		// Token: 0x040026B5 RID: 9909
		UseFontPercent,
		// Token: 0x040026B6 RID: 9910
		CapImage,
		// Token: 0x040026B7 RID: 9911
		OnTop,
		// Token: 0x040026B8 RID: 9912
		Reflection,
		// Token: 0x040026B9 RID: 9913
		CapStyle,
		// Token: 0x040026BA RID: 9914
		GaugeScales,
		// Token: 0x040026BB RID: 9915
		PivotX,
		// Token: 0x040026BC RID: 9916
		PivotY,
		// Token: 0x040026BD RID: 9917
		PointerCap,
		// Token: 0x040026BE RID: 9918
		NeedleStyle,
		// Token: 0x040026BF RID: 9919
		GaugePointers,
		// Token: 0x040026C0 RID: 9920
		Radius,
		// Token: 0x040026C1 RID: 9921
		StartAngle,
		// Token: 0x040026C2 RID: 9922
		SweepAngle,
		// Token: 0x040026C3 RID: 9923
		FontAngle,
		// Token: 0x040026C4 RID: 9924
		ShowEndLabels,
		// Token: 0x040026C5 RID: 9925
		Enable,
		// Token: 0x040026C6 RID: 9926
		PinLabel,
		// Token: 0x040026C7 RID: 9927
		StartValue,
		// Token: 0x040026C8 RID: 9928
		EndValue,
		// Token: 0x040026C9 RID: 9929
		StartWidth,
		// Token: 0x040026CA RID: 9930
		EndWidth,
		// Token: 0x040026CB RID: 9931
		InRangeBarPointerColor,
		// Token: 0x040026CC RID: 9932
		InRangeLabelColor,
		// Token: 0x040026CD RID: 9933
		InRangeTickMarksColor,
		// Token: 0x040026CE RID: 9934
		BackgroundGradientType,
		// Token: 0x040026CF RID: 9935
		BulbOffset,
		// Token: 0x040026D0 RID: 9936
		BulbSize,
		// Token: 0x040026D1 RID: 9937
		ThermometerStyle,
		// Token: 0x040026D2 RID: 9938
		EnableGradient,
		// Token: 0x040026D3 RID: 9939
		GradientDensity,
		// Token: 0x040026D4 RID: 9940
		TickMarkImage,
		// Token: 0x040026D5 RID: 9941
		ResizeMode,
		// Token: 0x040026D6 RID: 9942
		TextShadowOffset,
		// Token: 0x040026D7 RID: 9943
		Items,
		// Token: 0x040026D8 RID: 9944
		Flags,
		// Token: 0x040026D9 RID: 9945
		UseRPLStream,
		// Token: 0x040026DA RID: 9946
		RPLSource,
		// Token: 0x040026DB RID: 9947
		GenerationIndex,
		// Token: 0x040026DC RID: 9948
		ToggleParent,
		// Token: 0x040026DD RID: 9949
		IsDefaultLine,
		// Token: 0x040026DE RID: 9950
		RowHeaderWidth,
		// Token: 0x040026DF RID: 9951
		ColumnHeaderHeight,
		// Token: 0x040026E0 RID: 9952
		RTL,
		// Token: 0x040026E1 RID: 9953
		DateTimeOffsetValue,
		// Token: 0x040026E2 RID: 9954
		DetailRowCounter,
		// Token: 0x040026E3 RID: 9955
		DetailSortAdditionalGroupLeafs,
		// Token: 0x040026E4 RID: 9956
		ProcessStaticCellsForRVs,
		// Token: 0x040026E5 RID: 9957
		Arguments,
		// Token: 0x040026E6 RID: 9958
		ChartMember,
		// Token: 0x040026E7 RID: 9959
		SourceSeries,
		// Token: 0x040026E8 RID: 9960
		ExplicitAltReportItem,
		// Token: 0x040026E9 RID: 9961
		TextOrientation,
		// Token: 0x040026EA RID: 9962
		AspectRatio,
		// Token: 0x040026EB RID: 9963
		ChartElementPosition,
		// Token: 0x040026EC RID: 9964
		ChartInnerPlotPosition,
		// Token: 0x040026ED RID: 9965
		Disabled,
		// Token: 0x040026EE RID: 9966
		DependencyRefList,
		// Token: 0x040026EF RID: 9967
		RecursiveMember,
		// Token: 0x040026F0 RID: 9968
		HasRecursiveChildren,
		// Token: 0x040026F1 RID: 9969
		DefinitionHasDocumentMap,
		// Token: 0x040026F2 RID: 9970
		Paragraphs,
		// Token: 0x040026F3 RID: 9971
		Paragraph,
		// Token: 0x040026F4 RID: 9972
		TextBox,
		// Token: 0x040026F5 RID: 9973
		TextRuns,
		// Token: 0x040026F6 RID: 9974
		TextRun,
		// Token: 0x040026F7 RID: 9975
		LeftIndent,
		// Token: 0x040026F8 RID: 9976
		RightIndent,
		// Token: 0x040026F9 RID: 9977
		HangingIndent,
		// Token: 0x040026FA RID: 9978
		SpaceBefore,
		// Token: 0x040026FB RID: 9979
		SpaceAfter,
		// Token: 0x040026FC RID: 9980
		ListStyle,
		// Token: 0x040026FD RID: 9981
		ListLevel,
		// Token: 0x040026FE RID: 9982
		MarkupType,
		// Token: 0x040026FF RID: 9983
		HasExpressionBasedValue,
		// Token: 0x04002700 RID: 9984
		HasValue,
		// Token: 0x04002701 RID: 9985
		TextRunValueReferenced,
		// Token: 0x04002702 RID: 9986
		ParagraphNumber,
		// Token: 0x04002703 RID: 9987
		ParagraphIndex,
		// Token: 0x04002704 RID: 9988
		TextRunIndex,
		// Token: 0x04002705 RID: 9989
		CharacterIndex,
		// Token: 0x04002706 RID: 9990
		ContentOffset,
		// Token: 0x04002707 RID: 9991
		ContentBottom,
		// Token: 0x04002708 RID: 9992
		PageStartOffset,
		// Token: 0x04002709 RID: 9993
		PageEndOffset,
		// Token: 0x0400270A RID: 9994
		NextPageStartOffset,
		// Token: 0x0400270B RID: 9995
		FirstLine,
		// Token: 0x0400270C RID: 9996
		TopPadding,
		// Token: 0x0400270D RID: 9997
		InDataRowSortPhase,
		// Token: 0x0400270E RID: 9998
		SortedDataRowTree,
		// Token: 0x0400270F RID: 9999
		DataRowSortExpression,
		// Token: 0x04002710 RID: 10000
		PaletteHatchBehavior,
		// Token: 0x04002711 RID: 10001
		ContentHeight,
		// Token: 0x04002712 RID: 10002
		DependencyIndexList,
		// Token: 0x04002713 RID: 10003
		EventSourceColScope,
		// Token: 0x04002714 RID: 10004
		DetailColScopes,
		// Token: 0x04002715 RID: 10005
		HasNonRecursiveSender,
		// Token: 0x04002716 RID: 10006
		IsDataRegion,
		// Token: 0x04002717 RID: 10007
		OriginalCatalogPath,
		// Token: 0x04002718 RID: 10008
		Lookups,
		// Token: 0x04002719 RID: 10009
		LookupDestinations,
		// Token: 0x0400271A RID: 10010
		SourceExpr,
		// Token: 0x0400271B RID: 10011
		DestinationExpr,
		// Token: 0x0400271C RID: 10012
		ResultExpr,
		// Token: 0x0400271D RID: 10013
		DestinationIndexInCollection,
		// Token: 0x0400271E RID: 10014
		UsedInSameDataSetTablixProcessing,
		// Token: 0x0400271F RID: 10015
		UsedInSameDataSetFirstPass,
		// Token: 0x04002720 RID: 10016
		HasSameDataSetLookups,
		// Token: 0x04002721 RID: 10017
		FirstRowOffset,
		// Token: 0x04002722 RID: 10018
		RowOffsets,
		// Token: 0x04002723 RID: 10019
		Rows,
		// Token: 0x04002724 RID: 10020
		LookupResults,
		// Token: 0x04002725 RID: 10021
		HasLookups,
		// Token: 0x04002726 RID: 10022
		TreePartitionOffsets,
		// Token: 0x04002727 RID: 10023
		GroupTreePartitions,
		// Token: 0x04002728 RID: 10024
		LookupPartitions,
		// Token: 0x04002729 RID: 10025
		LookupTablePartitionID,
		// Token: 0x0400272A RID: 10026
		NeedsTotalPages,
		// Token: 0x0400272B RID: 10027
		NeedsReportItemsOnPage,
		// Token: 0x0400272C RID: 10028
		ReportSections,
		// Token: 0x0400272D RID: 10029
		PrintBetweenSections,
		// Token: 0x0400272E RID: 10030
		HasHeadersOrFooters,
		// Token: 0x0400272F RID: 10031
		LastAssignedGlobalID,
		// Token: 0x04002730 RID: 10032
		ContainingSection,
		// Token: 0x04002731 RID: 10033
		LookupType,
		// Token: 0x04002732 RID: 10034
		ExceptionMessage,
		// Token: 0x04002733 RID: 10035
		Map,
		// Token: 0x04002734 RID: 10036
		MapDataRegions,
		// Token: 0x04002735 RID: 10037
		MapDataRegion,
		// Token: 0x04002736 RID: 10038
		MapMember,
		// Token: 0x04002737 RID: 10039
		MapRowMember,
		// Token: 0x04002738 RID: 10040
		MapRow,
		// Token: 0x04002739 RID: 10041
		MapCell,
		// Token: 0x0400273A RID: 10042
		Unit,
		// Token: 0x0400273B RID: 10043
		ShowLabels,
		// Token: 0x0400273C RID: 10044
		LabelPosition,
		// Token: 0x0400273D RID: 10045
		MapLocation,
		// Token: 0x0400273E RID: 10046
		MapSize,
		// Token: 0x0400273F RID: 10047
		DockOutsideViewport,
		// Token: 0x04002740 RID: 10048
		FieldName,
		// Token: 0x04002741 RID: 10049
		BindingExpression,
		// Token: 0x04002742 RID: 10050
		LayerName,
		// Token: 0x04002743 RID: 10051
		MapBindingFieldPairs,
		// Token: 0x04002744 RID: 10052
		ZoomEnabled,
		// Token: 0x04002745 RID: 10053
		MapCoordinateSystem,
		// Token: 0x04002746 RID: 10054
		MapProjection,
		// Token: 0x04002747 RID: 10055
		ProjectionCenterX,
		// Token: 0x04002748 RID: 10056
		ProjectionCenterY,
		// Token: 0x04002749 RID: 10057
		MaximumZoom,
		// Token: 0x0400274A RID: 10058
		MinimumZoom,
		// Token: 0x0400274B RID: 10059
		MapLimits,
		// Token: 0x0400274C RID: 10060
		ContentMargin,
		// Token: 0x0400274D RID: 10061
		MapMeridians,
		// Token: 0x0400274E RID: 10062
		MapParallels,
		// Token: 0x0400274F RID: 10063
		GridUnderContent,
		// Token: 0x04002750 RID: 10064
		MinimumX,
		// Token: 0x04002751 RID: 10065
		MinimumY,
		// Token: 0x04002752 RID: 10066
		MaximumX,
		// Token: 0x04002753 RID: 10067
		MaximumY,
		// Token: 0x04002754 RID: 10068
		LimitToData,
		// Token: 0x04002755 RID: 10069
		MapColorScaleTitle,
		// Token: 0x04002756 RID: 10070
		TickMarkLength,
		// Token: 0x04002757 RID: 10071
		ColorBarBorderColor,
		// Token: 0x04002758 RID: 10072
		LabelFormat,
		// Token: 0x04002759 RID: 10073
		LabelPlacement,
		// Token: 0x0400275A RID: 10074
		LabelBehavior,
		// Token: 0x0400275B RID: 10075
		RangeGapColor,
		// Token: 0x0400275C RID: 10076
		NoDataText,
		// Token: 0x0400275D RID: 10077
		ScaleColor,
		// Token: 0x0400275E RID: 10078
		ScaleBorderColor,
		// Token: 0x0400275F RID: 10079
		TitleSeparatorColor,
		// Token: 0x04002760 RID: 10080
		MapLegendTitle,
		// Token: 0x04002761 RID: 10081
		DistributionType,
		// Token: 0x04002762 RID: 10082
		BucketCount,
		// Token: 0x04002763 RID: 10083
		MapBuckets,
		// Token: 0x04002764 RID: 10084
		StartColor,
		// Token: 0x04002765 RID: 10085
		MiddleColor,
		// Token: 0x04002766 RID: 10086
		EndColor,
		// Token: 0x04002767 RID: 10087
		ShowInColorScale,
		// Token: 0x04002768 RID: 10088
		MapSizeRule,
		// Token: 0x04002769 RID: 10089
		MapColorRule,
		// Token: 0x0400276A RID: 10090
		MapPointRules,
		// Token: 0x0400276B RID: 10091
		StartSize,
		// Token: 0x0400276C RID: 10092
		EndSize,
		// Token: 0x0400276D RID: 10093
		MapMarkerStyle,
		// Token: 0x0400276E RID: 10094
		MapMarkerlImage,
		// Token: 0x0400276F RID: 10095
		MapMarkers,
		// Token: 0x04002770 RID: 10096
		MapMarkerRule,
		// Token: 0x04002771 RID: 10097
		DataValue,
		// Token: 0x04002772 RID: 10098
		MapCustomColors,
		// Token: 0x04002773 RID: 10099
		ScaleFactor,
		// Token: 0x04002774 RID: 10100
		CenterPointOffsetX,
		// Token: 0x04002775 RID: 10101
		CenterPointOffsetY,
		// Token: 0x04002776 RID: 10102
		ShowLabel,
		// Token: 0x04002777 RID: 10103
		MapMarker,
		// Token: 0x04002778 RID: 10104
		ReportItem,
		// Token: 0x04002779 RID: 10105
		MapLegend,
		// Token: 0x0400277A RID: 10106
		UseCustomLineTemplate,
		// Token: 0x0400277B RID: 10107
		UseCustomPolygonTemplate,
		// Token: 0x0400277C RID: 10108
		UseCustomPointTemplate,
		// Token: 0x0400277D RID: 10109
		VectorData,
		// Token: 0x0400277E RID: 10110
		MapFields,
		// Token: 0x0400277F RID: 10111
		MapPolygonTemplate,
		// Token: 0x04002780 RID: 10112
		MapPointTemplate,
		// Token: 0x04002781 RID: 10113
		MapLineTemplate,
		// Token: 0x04002782 RID: 10114
		VisibilityMode,
		// Token: 0x04002783 RID: 10115
		MapLineRules,
		// Token: 0x04002784 RID: 10116
		MapLines,
		// Token: 0x04002785 RID: 10117
		MapFieldNames,
		// Token: 0x04002786 RID: 10118
		SimplificationResolution,
		// Token: 0x04002787 RID: 10119
		MapPolygons,
		// Token: 0x04002788 RID: 10120
		SpatialField,
		// Token: 0x04002789 RID: 10121
		TileData,
		// Token: 0x0400278A RID: 10122
		ServiceUrl,
		// Token: 0x0400278B RID: 10123
		TileStyle,
		// Token: 0x0400278C RID: 10124
		MapTiles,
		// Token: 0x0400278D RID: 10125
		MapDataRegionName,
		// Token: 0x0400278E RID: 10126
		MapFieldDefinitions,
		// Token: 0x0400278F RID: 10127
		MapSpatialData,
		// Token: 0x04002790 RID: 10128
		MapPolygonRules,
		// Token: 0x04002791 RID: 10129
		MapPoints,
		// Token: 0x04002792 RID: 10130
		MapViewport,
		// Token: 0x04002793 RID: 10131
		MapLayers,
		// Token: 0x04002794 RID: 10132
		MapLegends,
		// Token: 0x04002795 RID: 10133
		MapTitles,
		// Token: 0x04002796 RID: 10134
		MapDistanceScale,
		// Token: 0x04002797 RID: 10135
		MapColorScale,
		// Token: 0x04002798 RID: 10136
		MapBorderSkin,
		// Token: 0x04002799 RID: 10137
		MaximumSpatialElementCount,
		// Token: 0x0400279A RID: 10138
		MaximumTotalPointCount,
		// Token: 0x0400279B RID: 10139
		MapBorderSkinType,
		// Token: 0x0400279C RID: 10140
		ExprHostMapMemberID,
		// Token: 0x0400279D RID: 10141
		CenterX,
		// Token: 0x0400279E RID: 10142
		CenterY,
		// Token: 0x0400279F RID: 10143
		Zoom,
		// Token: 0x040027A0 RID: 10144
		MapView,
		// Token: 0x040027A1 RID: 10145
		MapVectorLayer,
		// Token: 0x040027A2 RID: 10146
		CachedShapefiles,
		// Token: 0x040027A3 RID: 10147
		DataElementLabel,
		// Token: 0x040027A4 RID: 10148
		TileLanguage,
		// Token: 0x040027A5 RID: 10149
		CurrentUnion,
		// Token: 0x040027A6 RID: 10150
		UseSecureConnection,
		// Token: 0x040027A7 RID: 10151
		NeedsOverallTotalPages,
		// Token: 0x040027A8 RID: 10152
		NeedsPageBreakTotalPages,
		// Token: 0x040027A9 RID: 10153
		AutoRefreshExpression,
		// Token: 0x040027AA RID: 10154
		PageBreak,
		// Token: 0x040027AB RID: 10155
		ResetPageNumber,
		// Token: 0x040027AC RID: 10156
		PageName,
		// Token: 0x040027AD RID: 10157
		InitialPageName,
		// Token: 0x040027AE RID: 10158
		PageBreakProperties,
		// Token: 0x040027AF RID: 10159
		PageBreakPropertiesAtStart,
		// Token: 0x040027B0 RID: 10160
		PageBreakPropertiesAtEnd,
		// Token: 0x040027B1 RID: 10161
		UpdatedVariableValues,
		// Token: 0x040027B2 RID: 10162
		Writable,
		// Token: 0x040027B3 RID: 10163
		SerializableVariables,
		// Token: 0x040027B4 RID: 10164
		DocumentMapRenderFormat,
		// Token: 0x040027B5 RID: 10165
		AggregatesOfAggregates,
		// Token: 0x040027B6 RID: 10166
		PostSortAggregatesOfAggregates,
		// Token: 0x040027B7 RID: 10167
		RunningValuesOfAggregates,
		// Token: 0x040027B8 RID: 10168
		HasAggregatesOfAggregates,
		// Token: 0x040027B9 RID: 10169
		DataScopeInfo,
		// Token: 0x040027BA RID: 10170
		AggregatesSpanGroupFilter,
		// Token: 0x040027BB RID: 10171
		MaxAggregateOfAggregatesLevel,
		// Token: 0x040027BC RID: 10172
		InnermostUpdateScope,
		// Token: 0x040027BD RID: 10173
		NumericIndicatorRanges,
		// Token: 0x040027BE RID: 10174
		DecimalDigitColor,
		// Token: 0x040027BF RID: 10175
		DigitColor,
		// Token: 0x040027C0 RID: 10176
		DecimalDigits,
		// Token: 0x040027C1 RID: 10177
		Digits,
		// Token: 0x040027C2 RID: 10178
		NonNumericString,
		// Token: 0x040027C3 RID: 10179
		OutOfRangeString,
		// Token: 0x040027C4 RID: 10180
		ShowDecimalPoint,
		// Token: 0x040027C5 RID: 10181
		ShowLeadingZeros,
		// Token: 0x040027C6 RID: 10182
		IndicatorStyle,
		// Token: 0x040027C7 RID: 10183
		ShowSign,
		// Token: 0x040027C8 RID: 10184
		LedDimColor,
		// Token: 0x040027C9 RID: 10185
		SeparatorWidth,
		// Token: 0x040027CA RID: 10186
		IndicatorImage,
		// Token: 0x040027CB RID: 10187
		IndicatorStates,
		// Token: 0x040027CC RID: 10188
		UpdateScopeID,
		// Token: 0x040027CD RID: 10189
		UpdateScopeDepth,
		// Token: 0x040027CE RID: 10190
		UpdatesAtRowScope,
		// Token: 0x040027CF RID: 10191
		ScopeID,
		// Token: 0x040027D0 RID: 10192
		HasAggregatesToUpdateAtRowScope,
		// Token: 0x040027D1 RID: 10193
		RunningValueOfAggregateValues,
		// Token: 0x040027D2 RID: 10194
		HasAggregatesOfAggregatesInUserSort,
		// Token: 0x040027D3 RID: 10195
		CanonicalCellScope,
		// Token: 0x040027D4 RID: 10196
		DomainScope,
		// Token: 0x040027D5 RID: 10197
		ScopeIDForDomainScope,
		// Token: 0x040027D6 RID: 10198
		InnerDomainScopeCount,
		// Token: 0x040027D7 RID: 10199
		RowDomainScopeCount,
		// Token: 0x040027D8 RID: 10200
		ColumnDomainScopeCount,
		// Token: 0x040027D9 RID: 10201
		OriginalScopeID,
		// Token: 0x040027DA RID: 10202
		TransformationType,
		// Token: 0x040027DB RID: 10203
		TransformationScope,
		// Token: 0x040027DC RID: 10204
		StateDataElementName,
		// Token: 0x040027DD RID: 10205
		StateDataElementOutput,
		// Token: 0x040027DE RID: 10206
		FieldNames,
		// Token: 0x040027DF RID: 10207
		SharedDataSetQuery,
		// Token: 0x040027E0 RID: 10208
		OriginalSharedDataSetReference,
		// Token: 0x040027E1 RID: 10209
		DataSetCore,
		// Token: 0x040027E2 RID: 10210
		CatalogID,
		// Token: 0x040027E3 RID: 10211
		ExprHostAssemblyID,
		// Token: 0x040027E4 RID: 10212
		SharedDSContainerCollectionIndex,
		// Token: 0x040027E5 RID: 10213
		IsArtificialDataSource,
		// Token: 0x040027E6 RID: 10214
		ReadOnly,
		// Token: 0x040027E7 RID: 10215
		OmitFromQuery,
		// Token: 0x040027E8 RID: 10216
		ParameterType,
		// Token: 0x040027E9 RID: 10217
		DataRowHolder,
		// Token: 0x040027EA RID: 10218
		Cells2,
		// Token: 0x040027EB RID: 10219
		MemberAtLevelIndexes,
		// Token: 0x040027EC RID: 10220
		ScopeValue,
		// Token: 0x040027ED RID: 10221
		CanScroll,
		// Token: 0x040027EE RID: 10222
		CanScrollVertically,
		// Token: 0x040027EF RID: 10223
		NaturalGroup,
		// Token: 0x040027F0 RID: 10224
		LastValue,
		// Token: 0x040027F1 RID: 10225
		Relationships,
		// Token: 0x040027F2 RID: 10226
		ParentScope,
		// Token: 0x040027F3 RID: 10227
		RelatedDataSet,
		// Token: 0x040027F4 RID: 10228
		JoinConditions,
		// Token: 0x040027F5 RID: 10229
		ForeignKeyExpression,
		// Token: 0x040027F6 RID: 10230
		PrimaryKeyExpression,
		// Token: 0x040027F7 RID: 10231
		NaturalSort,
		// Token: 0x040027F8 RID: 10232
		BandLayout,
		// Token: 0x040027F9 RID: 10233
		LabelData,
		// Token: 0x040027FA RID: 10234
		Play,
		// Token: 0x040027FB RID: 10235
		DockingOption,
		// Token: 0x040027FC RID: 10236
		ReportItemReference,
		// Token: 0x040027FD RID: 10237
		Slider,
		// Token: 0x040027FE RID: 10238
		Navigation,
		// Token: 0x040027FF RID: 10239
		BandNavigationCell,
		// Token: 0x04002800 RID: 10240
		IsDecomposable,
		// Token: 0x04002801 RID: 10241
		AllowIncrementalProcessing,
		// Token: 0x04002802 RID: 10242
		NavigationItem,
		// Token: 0x04002803 RID: 10243
		NaturalSortFlags,
		// Token: 0x04002804 RID: 10244
		MemberGroupAndSortExpressionFlag,
		// Token: 0x04002805 RID: 10245
		ScopeIDContent,
		// Token: 0x04002806 RID: 10246
		ScopeIDInfo,
		// Token: 0x04002807 RID: 10247
		DefaultRelationships,
		// Token: 0x04002808 RID: 10248
		ParentDataSet,
		// Token: 0x04002809 RID: 10249
		NeedsIDC,
		// Token: 0x0400280A RID: 10250
		RowMemberInstanceIndexes,
		// Token: 0x0400280B RID: 10251
		NaturalJoin,
		// Token: 0x0400280C RID: 10252
		ExactRowHeight,
		// Token: 0x0400280D RID: 10253
		IgnoreRowHeight,
		// Token: 0x0400280E RID: 10254
		BottomPadding,
		// Token: 0x0400280F RID: 10255
		TopBorder,
		// Token: 0x04002810 RID: 10256
		BottomBorder,
		// Token: 0x04002811 RID: 10257
		LeftBorder,
		// Token: 0x04002812 RID: 10258
		RightBorder,
		// Token: 0x04002813 RID: 10259
		Footer,
		// Token: 0x04002814 RID: 10260
		FirstPageHeader,
		// Token: 0x04002815 RID: 10261
		FirstPageFooter,
		// Token: 0x04002816 RID: 10262
		HighlightY,
		// Token: 0x04002817 RID: 10263
		ScopeInstanceNumber,
		// Token: 0x04002818 RID: 10264
		JoinInfo,
		// Token: 0x04002819 RID: 10265
		RowParentDataSet,
		// Token: 0x0400281A RID: 10266
		ColumnParentDataSet,
		// Token: 0x0400281B RID: 10267
		IsMatrixIDC,
		// Token: 0x0400281C RID: 10268
		DataPipelineID,
		// Token: 0x0400281D RID: 10269
		DataPipelineCount,
		// Token: 0x0400281E RID: 10270
		HighlightX,
		// Token: 0x0400281F RID: 10271
		HighlightSize,
		// Token: 0x04002820 RID: 10272
		AggregateIndicatorFieldIndex,
		// Token: 0x04002821 RID: 10273
		HasScopeWithCustomAggregates,
		// Token: 0x04002822 RID: 10274
		GroupingFieldIndicesForServerAggregates,
		// Token: 0x04002823 RID: 10275
		HasProcessedAggregateRow,
		// Token: 0x04002824 RID: 10276
		ColumnGroupingIsSwitched,
		// Token: 0x04002825 RID: 10277
		RdlFunctionType,
		// Token: 0x04002826 RID: 10278
		RdlFunctionInfo,
		// Token: 0x04002827 RID: 10279
		NullsAsBlanks,
		// Token: 0x04002828 RID: 10280
		CollationCulture,
		// Token: 0x04002829 RID: 10281
		Tag,
		// Token: 0x0400282A RID: 10282
		TagTypeCode,
		// Token: 0x0400282B RID: 10283
		DeferredSortFlags,
		// Token: 0x0400282C RID: 10284
		DeferredSort,
		// Token: 0x0400282D RID: 10285
		EmbeddingMode,
		// Token: 0x0400282E RID: 10286
		ValueType,
		// Token: 0x0400282F RID: 10287
		EnableRowDrilldown,
		// Token: 0x04002830 RID: 10288
		EnableColumnDrilldown,
		// Token: 0x04002831 RID: 10289
		ScopedFieldInfo,
		// Token: 0x04002832 RID: 10290
		FieldIndex,
		// Token: 0x04002833 RID: 10291
		EnableCategoryDrilldown,
		// Token: 0x04002834 RID: 10292
		BackgroundRepeat,
		// Token: 0x04002835 RID: 10293
		KeyFields,
		// Token: 0x04002836 RID: 10294
		Tags,
		// Token: 0x04002837 RID: 10295
		FormatX,
		// Token: 0x04002838 RID: 10296
		FormatY,
		// Token: 0x04002839 RID: 10297
		FormatSize,
		// Token: 0x0400283A RID: 10298
		CurrencyLanguageX,
		// Token: 0x0400283B RID: 10299
		CurrencyLanguageY,
		// Token: 0x0400283C RID: 10300
		CurrencyLanguageSize,
		// Token: 0x0400283D RID: 10301
		CurrencyLanguage,
		// Token: 0x0400283E RID: 10302
		ParametersLayout,
		// Token: 0x0400283F RID: 10303
		ParametersGridLayoutDefinition,
		// Token: 0x04002840 RID: 10304
		ParametersLayoutCellDefinitions,
		// Token: 0x04002841 RID: 10305
		ParametersLayoutNumberOfColumns,
		// Token: 0x04002842 RID: 10306
		ParametersLayoutNumberOfRows,
		// Token: 0x04002843 RID: 10307
		ParameterLayoutCellDefinition,
		// Token: 0x04002844 RID: 10308
		ParameterCellRowIndex,
		// Token: 0x04002845 RID: 10309
		ParameterCellColumnIndex,
		// Token: 0x04002846 RID: 10310
		ParameterName,
		// Token: 0x04002847 RID: 10311
		DefaultFontFamily,
		// Token: 0x04002848 RID: 10312
		DiagnosticDetails,
		// Token: 0x04002849 RID: 10313
		PreviewCommandText,
		// Token: 0x0400284A RID: 10314
		TransformedExpression,
		// Token: 0x0400284B RID: 10315
		UseAllValidValues,
		// Token: 0x0400284C RID: 10316
		MemberItemOriginalHeight,
		// Token: 0x0400284D RID: 10317
		StructureTypeOverwrite
	}
}
