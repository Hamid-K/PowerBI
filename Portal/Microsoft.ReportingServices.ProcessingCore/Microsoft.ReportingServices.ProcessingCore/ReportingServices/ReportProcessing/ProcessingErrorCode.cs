using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005DE RID: 1502
	public enum ProcessingErrorCode
	{
		// Token: 0x04002A3F RID: 10815
		rsNone,
		// Token: 0x04002A40 RID: 10816
		rsAggregateInFilterExpression,
		// Token: 0x04002A41 RID: 10817
		rsAggregateInGroupExpression,
		// Token: 0x04002A42 RID: 10818
		rsAggregateInQueryParameterExpression,
		// Token: 0x04002A43 RID: 10819
		rsAggregateInReportParameterExpression,
		// Token: 0x04002A44 RID: 10820
		rsAggregateInReportLanguageExpression,
		// Token: 0x04002A45 RID: 10821
		rsAggregateInCalculatedFieldExpression,
		// Token: 0x04002A46 RID: 10822
		rsAggregateofAggregate,
		// Token: 0x04002A47 RID: 10823
		rsAggregateReportItemInBody,
		// Token: 0x04002A48 RID: 10824
		rsBinaryConstant,
		// Token: 0x04002A49 RID: 10825
		rsChartSeriesPlotTypeIgnored,
		// Token: 0x04002A4A RID: 10826
		rsCompilerErrorInExpression,
		// Token: 0x04002A4B RID: 10827
		rsCompilerErrorInCode,
		// Token: 0x04002A4C RID: 10828
		rsCompilerErrorInClassInstanceDeclaration,
		// Token: 0x04002A4D RID: 10829
		rsUnexpectedCompilerError,
		// Token: 0x04002A4E RID: 10830
		rsConflictingRunningValueScopesInMatrix,
		// Token: 0x04002A4F RID: 10831
		rsConflictingRunningValueScopesInTablix,
		// Token: 0x04002A50 RID: 10832
		rsCountRowsInPageSectionExpression,
		// Token: 0x04002A51 RID: 10833
		rsCountStarNotSupported,
		// Token: 0x04002A52 RID: 10834
		rsCountStarRVNotSupported,
		// Token: 0x04002A53 RID: 10835
		rsCustomAggregateAndFilter,
		// Token: 0x04002A54 RID: 10836
		rsDataRegionInDetailList,
		// Token: 0x04002A55 RID: 10837
		rsDataRegionInPageSection,
		// Token: 0x04002A56 RID: 10838
		rsDataRegionInTableDetailRow,
		// Token: 0x04002A57 RID: 10839
		rsDataRegionWithoutDataSet,
		// Token: 0x04002A58 RID: 10840
		rsDataSourceReferenceNotPublished,
		// Token: 0x04002A59 RID: 10841
		rsDuplicateChartColumnName,
		// Token: 0x04002A5A RID: 10842
		rsDuplicateChartAxisName,
		// Token: 0x04002A5B RID: 10843
		rsInvalidChartAxisName,
		// Token: 0x04002A5C RID: 10844
		rsSpecifiedNonValueAxisName,
		// Token: 0x04002A5D RID: 10845
		rsValueAxisNameNotFound,
		// Token: 0x04002A5E RID: 10846
		rsInvalidTextEffect,
		// Token: 0x04002A5F RID: 10847
		rsInvalidBackgroundHatchType,
		// Token: 0x04002A60 RID: 10848
		rsInvalidBackgroundImagePosition,
		// Token: 0x04002A61 RID: 10849
		rsInvalidTextOrientations,
		// Token: 0x04002A62 RID: 10850
		rsInvalidRowGaugeMemberCannotBeDynamic,
		// Token: 0x04002A63 RID: 10851
		rsInvalidRowGaugeMemberCannotContainChildMember,
		// Token: 0x04002A64 RID: 10852
		rsInvalidColumnGaugeMemberCannotContainMultipleChildMember,
		// Token: 0x04002A65 RID: 10853
		rsPageBreakOnGaugeGroup,
		// Token: 0x04002A66 RID: 10854
		rsDuplicateChartAreaName,
		// Token: 0x04002A67 RID: 10855
		rsDuplicateChartTitleName,
		// Token: 0x04002A68 RID: 10856
		rsDuplicateChartLegendName,
		// Token: 0x04002A69 RID: 10857
		rsDuplicateChartLegendCustomItem,
		// Token: 0x04002A6A RID: 10858
		rsDuplicateChartLegendCustomItemCellName,
		// Token: 0x04002A6B RID: 10859
		rsDuplicateChartLegendColumnName,
		// Token: 0x04002A6C RID: 10860
		rsDuplicateChartFormulaParameter,
		// Token: 0x04002A6D RID: 10861
		rsInvalidChartBorderSkinType,
		// Token: 0x04002A6E RID: 10862
		rsInvalidChartBreakLineType,
		// Token: 0x04002A6F RID: 10863
		rsInvalidAutoBool,
		// Token: 0x04002A70 RID: 10864
		rsInvalidChartTickMarkIntervalType,
		// Token: 0x04002A71 RID: 10865
		rsInvalidChartTickMarkType,
		// Token: 0x04002A72 RID: 10866
		rsInvalidChartColumnType,
		// Token: 0x04002A73 RID: 10867
		rsInvalidChartCellType,
		// Token: 0x04002A74 RID: 10868
		rsInvalidChartCellAlignment,
		// Token: 0x04002A75 RID: 10869
		rsInvalidChartAllowOutsideChartArea,
		// Token: 0x04002A76 RID: 10870
		rsInvalidChartCalloutLineAnchor,
		// Token: 0x04002A77 RID: 10871
		rsInvalidChartCalloutLineStyle,
		// Token: 0x04002A78 RID: 10872
		rsInvalidChartCalloutStyle,
		// Token: 0x04002A79 RID: 10873
		rsInvalidChartIntervalType,
		// Token: 0x04002A7A RID: 10874
		rsInvalidChartCustomItemSeparator,
		// Token: 0x04002A7B RID: 10875
		rsInvalidChartSeriesFormula,
		// Token: 0x04002A7C RID: 10876
		rsInvalidChartSeriesType,
		// Token: 0x04002A7D RID: 10877
		rsInvalidChartSeriesSubtype,
		// Token: 0x04002A7E RID: 10878
		rsInvalidChartAxisLabelRotation,
		// Token: 0x04002A7F RID: 10879
		rsInvalidChartAxisLocation,
		// Token: 0x04002A80 RID: 10880
		rsInvalidChartAxisArrow,
		// Token: 0x04002A81 RID: 10881
		rsDuplicateClassInstanceName,
		// Token: 0x04002A82 RID: 10882
		rsDuplicateDataSourceName,
		// Token: 0x04002A83 RID: 10883
		rsInvalidDataSourceNameLength,
		// Token: 0x04002A84 RID: 10884
		rsDuplicateEmbeddedImageName,
		// Token: 0x04002A85 RID: 10885
		rsInvalidEmbeddedImageNameNotCLSCompliant,
		// Token: 0x04002A86 RID: 10886
		rsInvalidEmbeddedImageNameLength,
		// Token: 0x04002A87 RID: 10887
		rsDuplicateFieldName,
		// Token: 0x04002A88 RID: 10888
		rsDuplicateParameterName,
		// Token: 0x04002A89 RID: 10889
		rsDuplicateReportItemName,
		// Token: 0x04002A8A RID: 10890
		rsDuplicateReportParameterName,
		// Token: 0x04002A8B RID: 10891
		rsDuplicateCaseInsensitiveReportParameterName,
		// Token: 0x04002A8C RID: 10892
		rsDuplicateScopeName,
		// Token: 0x04002A8D RID: 10893
		rsExpressionMissingCloseParen,
		// Token: 0x04002A8E RID: 10894
		rsFieldInPageSectionExpression,
		// Token: 0x04002A8F RID: 10895
		rsFieldInQueryParameterExpression,
		// Token: 0x04002A90 RID: 10896
		rsFieldInReportParameterExpression,
		// Token: 0x04002A91 RID: 10897
		rsFieldInReportLanguageExpression,
		// Token: 0x04002A92 RID: 10898
		rsGlobalNotDefined,
		// Token: 0x04002A93 RID: 10899
		rsInvalidAction,
		// Token: 0x04002A94 RID: 10900
		rsInvalidActionLabel,
		// Token: 0x04002A95 RID: 10901
		rsInvalidAggregateScope,
		// Token: 0x04002A96 RID: 10902
		rsInvalidAltReportItem,
		// Token: 0x04002A97 RID: 10903
		rsInvalidBooleanConstant,
		// Token: 0x04002A98 RID: 10904
		rsInvalidCategoryGrouping,
		// Token: 0x04002A99 RID: 10905
		rsInvalidCharacterInExpression,
		// Token: 0x04002A9A RID: 10906
		rsInvalidChartColumnName,
		// Token: 0x04002A9B RID: 10907
		rsInvalidChartColumnNameNotCLSCompliant,
		// Token: 0x04002A9C RID: 10908
		rsInvalidChartColumnNameLength,
		// Token: 0x04002A9D RID: 10909
		rsInvalidChartGroupings,
		// Token: 0x04002A9E RID: 10910
		rsInvalidChartSubType,
		// Token: 0x04002A9F RID: 10911
		rsInvalidColumnGrouping,
		// Token: 0x04002AA0 RID: 10912
		rsInvalidColumnsInBody,
		// Token: 0x04002AA1 RID: 10913
		rsInvalidCustomAggregateExpression,
		// Token: 0x04002AA2 RID: 10914
		rsInvalidCustomAggregateScope,
		// Token: 0x04002AA3 RID: 10915
		rsInvalidCustomPropertyName,
		// Token: 0x04002AA4 RID: 10916
		rsInvalidDataElementNameNotCLSCompliant,
		// Token: 0x04002AA5 RID: 10917
		rsInvalidDataSetName,
		// Token: 0x04002AA6 RID: 10918
		rsInvalidDataSource,
		// Token: 0x04002AA7 RID: 10919
		rsInvalidDataSourceReference,
		// Token: 0x04002AA8 RID: 10920
		rsInvalidValidValuesDataSetReference,
		// Token: 0x04002AA9 RID: 10921
		rsInvalidDefaultValueDataSetReference,
		// Token: 0x04002AAA RID: 10922
		rsInvalidDataSetReferenceField,
		// Token: 0x04002AAB RID: 10923
		rsInvalidDefaultValue,
		// Token: 0x04002AAC RID: 10924
		rsInvalidDefaultValueValues,
		// Token: 0x04002AAD RID: 10925
		rsInvalidDetailDataGrouping,
		// Token: 0x04002AAE RID: 10926
		rsInvalidEmbeddedImage,
		// Token: 0x04002AAF RID: 10927
		rsInvalidExpressionScope,
		// Token: 0x04002AB0 RID: 10928
		rsInvalidExpressionScopeDataSet,
		// Token: 0x04002AB1 RID: 10929
		rsInvalidSortExpressionScope,
		// Token: 0x04002AB2 RID: 10930
		rsIneffectiveSortExpressionScope,
		// Token: 0x04002AB3 RID: 10931
		rsInvalidField,
		// Token: 0x04002AB4 RID: 10932
		rsInvalidFieldName,
		// Token: 0x04002AB5 RID: 10933
		rsInvalidFieldNameNotCLSCompliant,
		// Token: 0x04002AB6 RID: 10934
		rsInvalidFieldNameLength,
		// Token: 0x04002AB7 RID: 10935
		rsInvalidGroupExpressionScope,
		// Token: 0x04002AB8 RID: 10936
		rsInvalidGroupingName,
		// Token: 0x04002AB9 RID: 10937
		rsInvalidGroupingNameNotCLSCompliant,
		// Token: 0x04002ABA RID: 10938
		rsInvalidGroupingNameLength,
		// Token: 0x04002ABB RID: 10939
		rsInvalidHideDuplicateScope,
		// Token: 0x04002ABC RID: 10940
		rsInvalidURLProtocol,
		// Token: 0x04002ABD RID: 10941
		rsInvalidIntegerConstant,
		// Token: 0x04002ABE RID: 10942
		rsInvalidDateTimeConstant,
		// Token: 0x04002ABF RID: 10943
		rsInvalidFloatConstant,
		// Token: 0x04002AC0 RID: 10944
		rsLabelExpressionOnChartScalarAxisIsIgnored,
		// Token: 0x04002AC1 RID: 10945
		rsInvalidMatrixSubtotalReportItem,
		// Token: 0x04002AC2 RID: 10946
		rsInvalidName,
		// Token: 0x04002AC3 RID: 10947
		rsInvalidNameNotCLSCompliant,
		// Token: 0x04002AC4 RID: 10948
		rsInvalidNameLength,
		// Token: 0x04002AC5 RID: 10949
		rsInvalidNumberOfFilterValues,
		// Token: 0x04002AC6 RID: 10950
		rsInvalidOmittedExpressionScope,
		// Token: 0x04002AC7 RID: 10951
		rsInvalidOmittedTargetScope,
		// Token: 0x04002AC8 RID: 10952
		rsInvalidParameterName,
		// Token: 0x04002AC9 RID: 10953
		rsInvalidParameterNameNotCLSCompliant,
		// Token: 0x04002ACA RID: 10954
		rsInvalidParameterNameLength,
		// Token: 0x04002ACB RID: 10955
		rsInvalidPreviousAggregateInMatrixCell,
		// Token: 0x04002ACC RID: 10956
		rsInvalidPreviousAggregateInTablixCell,
		// Token: 0x04002ACD RID: 10957
		rsInvalidRepeatWith,
		// Token: 0x04002ACE RID: 10958
		rsInvalidReportDefinition,
		// Token: 0x04002ACF RID: 10959
		rsInvalidReportParameterDependency,
		// Token: 0x04002AD0 RID: 10960
		rsInvalidRowGrouping,
		// Token: 0x04002AD1 RID: 10961
		rsInvalidRunningValueAggregate,
		// Token: 0x04002AD2 RID: 10962
		rsInvalidScopeInMatrix,
		// Token: 0x04002AD3 RID: 10963
		rsInvalidScopeInTablix,
		// Token: 0x04002AD4 RID: 10964
		rsInvalidSeriesGrouping,
		// Token: 0x04002AD5 RID: 10965
		rsInvalidStaticDataGrouping,
		// Token: 0x04002AD6 RID: 10966
		rsInvalidReportName,
		// Token: 0x04002AD7 RID: 10967
		rsInvalidReportNameCharacters,
		// Token: 0x04002AD8 RID: 10968
		rsInvalidReportUri,
		// Token: 0x04002AD9 RID: 10969
		rsInvalidTargetScope,
		// Token: 0x04002ADA RID: 10970
		rsInvalidTextboxInPageSection,
		// Token: 0x04002ADB RID: 10971
		rsInvalidReportItemInPageSection,
		// Token: 0x04002ADC RID: 10972
		rsInvalidToggleItem,
		// Token: 0x04002ADD RID: 10973
		rsInvalidValidValues,
		// Token: 0x04002ADE RID: 10974
		rsInvalidMultiValueParameter,
		// Token: 0x04002ADF RID: 10975
		rsInvalidParameterDefaultValue,
		// Token: 0x04002AE0 RID: 10976
		rsLineChartMightScatter,
		// Token: 0x04002AE1 RID: 10977
		rsMissingAggregateScope,
		// Token: 0x04002AE2 RID: 10978
		rsMissingChartDataPoints,
		// Token: 0x04002AE3 RID: 10979
		rsMissingCustomPropertyName,
		// Token: 0x04002AE4 RID: 10980
		rsMissingDataSetName,
		// Token: 0x04002AE5 RID: 10981
		rsMissingMIMEType,
		// Token: 0x04002AE6 RID: 10982
		rsMissingParameterDefault,
		// Token: 0x04002AE7 RID: 10983
		rsMultipleGroupExpressionsOnChartScalarAxis,
		// Token: 0x04002AE8 RID: 10984
		rsMultipleGroupingsOnChartScalarAxis,
		// Token: 0x04002AE9 RID: 10985
		rsMultiReportItemsInMatrixSection,
		// Token: 0x04002AEA RID: 10986
		rsMultiReportItemsInTableCell,
		// Token: 0x04002AEB RID: 10987
		rsMultiReportItemsInPageSectionExpression,
		// Token: 0x04002AEC RID: 10988
		rsMultiReportItemsInCustomReportItem,
		// Token: 0x04002AED RID: 10989
		rsMultiStaticCategoriesOrSeries,
		// Token: 0x04002AEE RID: 10990
		rsMultiStaticColumnsOrRows,
		// Token: 0x04002AEF RID: 10991
		rsNegativeLeftWidth,
		// Token: 0x04002AF0 RID: 10992
		rsNegativeTopHeight,
		// Token: 0x04002AF1 RID: 10993
		rsNonAggregateInMatrixCell,
		// Token: 0x04002AF2 RID: 10994
		rsNonAggregateInTablixCell,
		// Token: 0x04002AF3 RID: 10995
		rsNonExistingScope,
		// Token: 0x04002AF4 RID: 10996
		rsNotAReportDefinition,
		// Token: 0x04002AF5 RID: 10997
		rsNotACurrentReportDefinition,
		// Token: 0x04002AF6 RID: 10998
		rsOverlappingReportItems,
		// Token: 0x04002AF7 RID: 10999
		rsReportItemOutsideContainer,
		// Token: 0x04002AF8 RID: 11000
		rsPageBreakOnMatrixColumnGroup,
		// Token: 0x04002AF9 RID: 11001
		rsPageBreakOnChartGroup,
		// Token: 0x04002AFA RID: 11002
		rsParameterValueDefinitionMismatch,
		// Token: 0x04002AFB RID: 11003
		rsParameterValueNullOrBlank,
		// Token: 0x04002AFC RID: 11004
		rsPreviousAggregateInFilterExpression,
		// Token: 0x04002AFD RID: 11005
		rsPreviousAggregateInGroupExpression,
		// Token: 0x04002AFE RID: 11006
		rsPreviousAggregateInPageSectionExpression,
		// Token: 0x04002AFF RID: 11007
		rsPreviousAggregateInQueryParameterExpression,
		// Token: 0x04002B00 RID: 11008
		rsPreviousAggregateInReportParameterExpression,
		// Token: 0x04002B01 RID: 11009
		rsPreviousAggregateInReportLanguageExpression,
		// Token: 0x04002B02 RID: 11010
		rsPreviousAggregateInSortExpression,
		// Token: 0x04002B03 RID: 11011
		rsRepeatWithNotPeerDataRegion,
		// Token: 0x04002B04 RID: 11012
		rsReportItemInFilterExpression,
		// Token: 0x04002B05 RID: 11013
		rsReportItemInGroupExpression,
		// Token: 0x04002B06 RID: 11014
		rsReportItemInQueryParameterExpression,
		// Token: 0x04002B07 RID: 11015
		rsReportItemInReportParameterExpression,
		// Token: 0x04002B08 RID: 11016
		rsReportItemInSortExpression,
		// Token: 0x04002B09 RID: 11017
		rsReportItemInReportLanguageExpression,
		// Token: 0x04002B0A RID: 11018
		rsReportItemInVariableExpression,
		// Token: 0x04002B0B RID: 11019
		rsParameterPropertyTypeMismatch,
		// Token: 0x04002B0C RID: 11020
		rsRowNumberInFilterExpression,
		// Token: 0x04002B0D RID: 11021
		rsRowNumberInPageSectionExpression,
		// Token: 0x04002B0E RID: 11022
		rsRowNumberInQueryParameterExpression,
		// Token: 0x04002B0F RID: 11023
		rsRowNumberInReportParameterExpression,
		// Token: 0x04002B10 RID: 11024
		rsRowNumberInReportLanguageExpression,
		// Token: 0x04002B11 RID: 11025
		rsRowNumberInSortExpression,
		// Token: 0x04002B12 RID: 11026
		rsRowNumberInVariableExpression,
		// Token: 0x04002B13 RID: 11027
		rsRunningValueInFilterExpression,
		// Token: 0x04002B14 RID: 11028
		rsRunningValueInGroupExpression,
		// Token: 0x04002B15 RID: 11029
		rsRunningValueInPageSectionExpression,
		// Token: 0x04002B16 RID: 11030
		rsRunningValueInQueryParameterExpression,
		// Token: 0x04002B17 RID: 11031
		rsRunningValueInReportParameterExpression,
		// Token: 0x04002B18 RID: 11032
		rsRunningValueInReportLanguageExpression,
		// Token: 0x04002B19 RID: 11033
		rsRunningValueInSortExpression,
		// Token: 0x04002B1A RID: 11034
		rsRunningValueInVariableExpression,
		// Token: 0x04002B1B RID: 11035
		rsScopeInPageSectionExpression,
		// Token: 0x04002B1C RID: 11036
		rsStaticGroupingOnChartScalarAxis,
		// Token: 0x04002B1D RID: 11037
		rsToggleInPageSection,
		// Token: 0x04002B1E RID: 11038
		rsUnsortedCategoryInAreaChart,
		// Token: 0x04002B1F RID: 11039
		rsWrongNumberOfMatrixCells,
		// Token: 0x04002B20 RID: 11040
		rsWrongNumberOfMatrixColumns,
		// Token: 0x04002B21 RID: 11041
		rsWrongNumberOfMatrixRows,
		// Token: 0x04002B22 RID: 11042
		rsWrongNumberOfChartDataPoints,
		// Token: 0x04002B23 RID: 11043
		rsWrongNumberOfChartSeries,
		// Token: 0x04002B24 RID: 11044
		rsWrongNumberOfChartDataPointsInSeries,
		// Token: 0x04002B25 RID: 11045
		rsWrongNumberOfDataValues,
		// Token: 0x04002B26 RID: 11046
		rsWrongNumberOfParameters,
		// Token: 0x04002B27 RID: 11047
		rsWrongNumberOfTableCells,
		// Token: 0x04002B28 RID: 11048
		rsSingleHierarchyWithDataRows,
		// Token: 0x04002B29 RID: 11049
		rsMissingDataGrouping,
		// Token: 0x04002B2A RID: 11050
		rsWrongNumberOfDataRows,
		// Token: 0x04002B2B RID: 11051
		rsWrongNumberOfDataCellsInDataRow,
		// Token: 0x04002B2C RID: 11052
		rsInvalidRecursiveAggregate,
		// Token: 0x04002B2D RID: 11053
		rsInvalidAggregateRecursiveFlag,
		// Token: 0x04002B2E RID: 11054
		rsPostSortAggregateInGroupFilterExpression,
		// Token: 0x04002B2F RID: 11055
		rsPostSortAggregateInSortExpression,
		// Token: 0x04002B30 RID: 11056
		rsPostSortAggregateInVariableExpression,
		// Token: 0x04002B31 RID: 11057
		rsAggregateInPreviousAggregate,
		// Token: 0x04002B32 RID: 11058
		rsRunningValueInPreviousAggregate,
		// Token: 0x04002B33 RID: 11059
		rsPreviousInPreviousAggregate,
		// Token: 0x04002B34 RID: 11060
		rsRowNumberInPreviousAggregate,
		// Token: 0x04002B35 RID: 11061
		rsInScopeOrLevelInPreviousAggregate,
		// Token: 0x04002B36 RID: 11062
		rsInvalidScopeInInnerAggregateOfPreviousAggregate,
		// Token: 0x04002B37 RID: 11063
		rsInvalidGroupingParent,
		// Token: 0x04002B38 RID: 11064
		rsMissingDataGroupings,
		// Token: 0x04002B39 RID: 11065
		rsMissingDataCells,
		// Token: 0x04002B3A RID: 11066
		rsCRIMultiStaticColumnsOrRows,
		// Token: 0x04002B3B RID: 11067
		rsCRIStaticWithSubgroups,
		// Token: 0x04002B3C RID: 11068
		rsCRIMultiNonStaticGroups,
		// Token: 0x04002B3D RID: 11069
		rsCRISubtotalNotSupported,
		// Token: 0x04002B3E RID: 11070
		rsInvalidGrouping,
		// Token: 0x04002B3F RID: 11071
		rsCRIInPageSection,
		// Token: 0x04002B40 RID: 11072
		rsBookmarkInPageSection,
		// Token: 0x04002B41 RID: 11073
		rsCantMakeTableGroupHeadersFixed,
		// Token: 0x04002B42 RID: 11074
		rsFixedHeadersInInnerDataRegion,
		// Token: 0x04002B43 RID: 11075
		rsInvalidFixedTableColumnHeaderSpacing,
		// Token: 0x04002B44 RID: 11076
		rsUnsupportedProtocol,
		// Token: 0x04002B45 RID: 11077
		rsCRIRenderItemNull,
		// Token: 0x04002B46 RID: 11078
		rsCRIRenderInstanceNull,
		// Token: 0x04002B47 RID: 11079
		rsCRIRenderItemInvalid,
		// Token: 0x04002B48 RID: 11080
		rsCRIRenderItemInstanceType,
		// Token: 0x04002B49 RID: 11081
		rsCRIRenderItemDefinitionName,
		// Token: 0x04002B4A RID: 11082
		rsCRIRenderItemProperties,
		// Token: 0x04002B4B RID: 11083
		rsCRIRenderItemDuplicateStyle,
		// Token: 0x04002B4C RID: 11084
		rsCRIRenderItemInvalidStyleType,
		// Token: 0x04002B4D RID: 11085
		rsCRIRenderItemInvalidStyle,
		// Token: 0x04002B4E RID: 11086
		rsCRIProcessingError,
		// Token: 0x04002B4F RID: 11087
		rsVariableInPreviousAggregate,
		// Token: 0x04002B50 RID: 11088
		rsAggregateofVariable,
		// Token: 0x04002B51 RID: 11089
		rsVariableInQueryParameterExpression,
		// Token: 0x04002B52 RID: 11090
		rsVariableInReportParameterExpression,
		// Token: 0x04002B53 RID: 11091
		rsVariableInReportLanguageExpression,
		// Token: 0x04002B54 RID: 11092
		rsVariableInGroupExpression,
		// Token: 0x04002B55 RID: 11093
		rsVariableInCalculatedFieldExpression,
		// Token: 0x04002B56 RID: 11094
		rsDataSetInPageSectionExpression,
		// Token: 0x04002B57 RID: 11095
		rsDataSetInQueryParameterExpression,
		// Token: 0x04002B58 RID: 11096
		rsDataSetInReportParameterExpression,
		// Token: 0x04002B59 RID: 11097
		rsDataSetInReportLanguageExpression,
		// Token: 0x04002B5A RID: 11098
		rsDataSourceInPageSectionExpression,
		// Token: 0x04002B5B RID: 11099
		rsDataSourceInQueryParameterExpression,
		// Token: 0x04002B5C RID: 11100
		rsDataSourceInReportParameterExpression,
		// Token: 0x04002B5D RID: 11101
		rsDataSourceInReportLanguageExpression,
		// Token: 0x04002B5E RID: 11102
		rsMissingChartDataValueName,
		// Token: 0x04002B5F RID: 11103
		rsInvalidMeDotValueInExpression,
		// Token: 0x04002B60 RID: 11104
		rsWrongNumberOfTablixCornerRows,
		// Token: 0x04002B61 RID: 11105
		rsWrongNumberOfTablixCornerCells,
		// Token: 0x04002B62 RID: 11106
		rsWrongNumberOfTablixColumns,
		// Token: 0x04002B63 RID: 11107
		rsWrongNumberOfTablixCells,
		// Token: 0x04002B64 RID: 11108
		rsWrongNumberOfTablixRows,
		// Token: 0x04002B65 RID: 11109
		rsInvalidTablixCornerCellSpan,
		// Token: 0x04002B66 RID: 11110
		rsInvalidTablixCornerRowSpans,
		// Token: 0x04002B67 RID: 11111
		rsInvalidTablixCornerColumnSpans,
		// Token: 0x04002B68 RID: 11112
		rsInvalidSortNotAllowed,
		// Token: 0x04002B69 RID: 11113
		rsInvalidFixedHeaderOnOppositeHierarchy,
		// Token: 0x04002B6A RID: 11114
		rsInvalidFixedDataColumnPosition,
		// Token: 0x04002B6B RID: 11115
		rsInvalidFixedDataRowPosition,
		// Token: 0x04002B6C RID: 11116
		rsInvalidFixedDataNotContiguous,
		// Token: 0x04002B6D RID: 11117
		rsInvalidFixedDataInHierarchy,
		// Token: 0x04002B6E RID: 11118
		rsInvalidFixedDataOnInnerMembers,
		// Token: 0x04002B6F RID: 11119
		rsHiddenTablixCornerCellContents,
		// Token: 0x04002B70 RID: 11120
		rsInvalidGroupAncestorIsDetail,
		// Token: 0x04002B71 RID: 11121
		rsInvalidKeepWithGroup,
		// Token: 0x04002B72 RID: 11122
		rsInvalidKeepWithGroupOnDynamicTablixMember,
		// Token: 0x04002B73 RID: 11123
		rsInvalidKeepWithGroupOnColumnTablixMember,
		// Token: 0x04002B74 RID: 11124
		rsInvalidRepeatOnNewPageOnColumnTablixMember,
		// Token: 0x04002B75 RID: 11125
		rsInvalidRepeatOnNewPage,
		// Token: 0x04002B76 RID: 11126
		rsInvalidTablixCellColSpans,
		// Token: 0x04002B77 RID: 11127
		rsInvalidTablixCellColSpan,
		// Token: 0x04002B78 RID: 11128
		rsInvalidTablixCellRowSpan,
		// Token: 0x04002B79 RID: 11129
		rsInvalidTablixHeaderColSpan,
		// Token: 0x04002B7A RID: 11130
		rsInvalidTablixHeaderRowSpan,
		// Token: 0x04002B7B RID: 11131
		rsCellContentsNotOmitted,
		// Token: 0x04002B7C RID: 11132
		rsCellContentsRequired,
		// Token: 0x04002B7D RID: 11133
		rsInvalidTablixCellCellSpan,
		// Token: 0x04002B7E RID: 11134
		rsInconsistentNumberofCellsInRow,
		// Token: 0x04002B7F RID: 11135
		rsInvalidTablixHeaderSize,
		// Token: 0x04002B80 RID: 11136
		rsInvalidTablixHeaders,
		// Token: 0x04002B81 RID: 11137
		rsInvalidInnerDataSetName,
		// Token: 0x04002B82 RID: 11138
		rsDuplicateGroupingVariableName,
		// Token: 0x04002B83 RID: 11139
		rsInvalidGroupingVariable,
		// Token: 0x04002B84 RID: 11140
		rsDuplicateVariableName,
		// Token: 0x04002B85 RID: 11141
		rsInvalidVariable,
		// Token: 0x04002B86 RID: 11142
		rsInvalidVariableReference,
		// Token: 0x04002B87 RID: 11143
		rsInvalidVariableNameNotCLSCompliant,
		// Token: 0x04002B88 RID: 11144
		rsInvalidVariableNameLength,
		// Token: 0x04002B89 RID: 11145
		rsInvalidGroupingVariableNameNotCLSCompliant,
		// Token: 0x04002B8A RID: 11146
		rsInvalidGroupingVariableNameLength,
		// Token: 0x04002B8B RID: 11147
		rsInvalidChartHierarchy,
		// Token: 0x04002B8C RID: 11148
		rsInvalidChartMemberMustBeDynamic,
		// Token: 0x04002B8D RID: 11149
		rsInvalidChartMemberMustContainGroupExpressions,
		// Token: 0x04002B8E RID: 11150
		rsInvlaidAxisAngle,
		// Token: 0x04002B8F RID: 11151
		rsInvalidVariableCount,
		// Token: 0x04002B90 RID: 11152
		rsMissingExpression,
		// Token: 0x04002B91 RID: 11153
		rsInvalidActionsCount,
		// Token: 0x04002B92 RID: 11154
		rsInvalidChartDataValueName,
		// Token: 0x04002B93 RID: 11155
		rsInvalidChartDataValueNameNotConstant,
		// Token: 0x04002B94 RID: 11156
		rsInvalidChartDataValueNameNotUnique,
		// Token: 0x04002B95 RID: 11157
		rsInvalidFixedDataBodyCellSpans,
		// Token: 0x04002B96 RID: 11158
		rsAggregateOfMixedDataTypes,
		// Token: 0x04002B97 RID: 11159
		rsAggregateOfNonNumericData,
		// Token: 0x04002B98 RID: 11160
		rsCyclicExpression,
		// Token: 0x04002B99 RID: 11161
		rsCyclicExpressionInReportVariable,
		// Token: 0x04002B9A RID: 11162
		rsCyclicExpressionInGroupVariable,
		// Token: 0x04002B9B RID: 11163
		rsErrorExecutingSubreport,
		// Token: 0x04002B9C RID: 11164
		rsInvalidExpressionDataType,
		// Token: 0x04002B9D RID: 11165
		rsFieldErrorInExpression,
		// Token: 0x04002B9E RID: 11166
		rsInvalidValidValueList,
		// Token: 0x04002B9F RID: 11167
		rsMinMaxOfNonSortableData,
		// Token: 0x04002BA0 RID: 11168
		rsRuntimeErrorInExpression,
		// Token: 0x04002BA1 RID: 11169
		rsRuntimeUserProfileDependency,
		// Token: 0x04002BA2 RID: 11170
		rsMissingFieldInDataSet,
		// Token: 0x04002BA3 RID: 11171
		rsDataSetFieldTypeNotSupported,
		// Token: 0x04002BA4 RID: 11172
		rsErrorReadingDataSetField,
		// Token: 0x04002BA5 RID: 11173
		rsWarningExecutingSubreport,
		// Token: 0x04002BA6 RID: 11174
		rsWarningFetchingExternalImages,
		// Token: 0x04002BA7 RID: 11175
		rsInvalidImageReference,
		// Token: 0x04002BA8 RID: 11176
		rsInvalidDatabaseImage,
		// Token: 0x04002BA9 RID: 11177
		rsComparisonError,
		// Token: 0x04002BAA RID: 11178
		rsComparisonTypeError,
		// Token: 0x04002BAB RID: 11179
		rsCollationDetectionFailed,
		// Token: 0x04002BAC RID: 11180
		rsErrorLoadingExprHostAssembly,
		// Token: 0x04002BAD RID: 11181
		rsErrorInOnInit,
		// Token: 0x04002BAE RID: 11182
		rsUntrustedCodeModule,
		// Token: 0x04002BAF RID: 11183
		rsErrorReadingFieldProperty,
		// Token: 0x04002BB0 RID: 11184
		rsExceededMaxRecursionLevel,
		// Token: 0x04002BB1 RID: 11185
		rsEngineMismatchSubReport,
		// Token: 0x04002BB2 RID: 11186
		rsEngineMismatchParentReport,
		// Token: 0x04002BB3 RID: 11187
		rsExternalImageLoadingDisabled,
		// Token: 0x04002BB4 RID: 11188
		rsInvalidEmptyImageReference,
		// Token: 0x04002BB5 RID: 11189
		rsFieldReference,
		// Token: 0x04002BB6 RID: 11190
		rsInvalidBackgroundRepeat,
		// Token: 0x04002BB7 RID: 11191
		rsInvalidBackgroundGradientType,
		// Token: 0x04002BB8 RID: 11192
		rsInvalidBorderStyle,
		// Token: 0x04002BB9 RID: 11193
		rsInvalidCalendar,
		// Token: 0x04002BBA RID: 11194
		rsInvalidCalendarForLanguage,
		// Token: 0x04002BBB RID: 11195
		rsInvalidColor,
		// Token: 0x04002BBC RID: 11196
		rsInvalidDirection,
		// Token: 0x04002BBD RID: 11197
		rsInvalidDatabaseImageProperty,
		// Token: 0x04002BBE RID: 11198
		rsInvalidEmbeddedImageProperty,
		// Token: 0x04002BBF RID: 11199
		rsInvalidExternalImageProperty,
		// Token: 0x04002BC0 RID: 11200
		rsInvalidFontStyle,
		// Token: 0x04002BC1 RID: 11201
		rsInvalidFontWeight,
		// Token: 0x04002BC2 RID: 11202
		rsInvalidFormatString,
		// Token: 0x04002BC3 RID: 11203
		rsInvalidLanguage,
		// Token: 0x04002BC4 RID: 11204
		rsInvalidMeasurementUnit,
		// Token: 0x04002BC5 RID: 11205
		rsInvalidMIMEType,
		// Token: 0x04002BC6 RID: 11206
		rsInvalidNumeralVariant,
		// Token: 0x04002BC7 RID: 11207
		rsInvalidNumeralVariantForLanguage,
		// Token: 0x04002BC8 RID: 11208
		rsInvalidSize,
		// Token: 0x04002BC9 RID: 11209
		rsInvalidTextAlign,
		// Token: 0x04002BCA RID: 11210
		rsInvalidTextDecoration,
		// Token: 0x04002BCB RID: 11211
		rsInvalidUnicodeBiDi,
		// Token: 0x04002BCC RID: 11212
		rsInvalidVerticalAlign,
		// Token: 0x04002BCD RID: 11213
		rsInvalidWritingMode,
		// Token: 0x04002BCE RID: 11214
		rsNegativeSize,
		// Token: 0x04002BCF RID: 11215
		rsOutOfRangeSize,
		// Token: 0x04002BD0 RID: 11216
		rsPageNumberInBody,
		// Token: 0x04002BD1 RID: 11217
		rsParameterReference,
		// Token: 0x04002BD2 RID: 11218
		rsReportItemReference,
		// Token: 0x04002BD3 RID: 11219
		rsDataSetReference,
		// Token: 0x04002BD4 RID: 11220
		rsDataSourceReference,
		// Token: 0x04002BD5 RID: 11221
		rsErrorLoadingCodeModule,
		// Token: 0x04002BD6 RID: 11222
		rsInvalidObjectNameNotUnique,
		// Token: 0x04002BD7 RID: 11223
		rsInvalidObjectNameNotCLSCompliant,
		// Token: 0x04002BD8 RID: 11224
		rsCRIControlFailedToLoad,
		// Token: 0x04002BD9 RID: 11225
		rsCRIControlNotInstalled,
		// Token: 0x04002BDA RID: 11226
		rsInvalidSourceSeriesName,
		// Token: 0x04002BDB RID: 11227
		rsInvalidDataSourceNameNotCLSCompliant,
		// Token: 0x04002BDC RID: 11228
		rsDuplicateItemName,
		// Token: 0x04002BDD RID: 11229
		rsDuplicateChartLegendItemName,
		// Token: 0x04002BDE RID: 11230
		rsInvalidEnumValue,
		// Token: 0x04002BDF RID: 11231
		rsAggregateOfInvalidExpressionDataType,
		// Token: 0x04002BE0 RID: 11232
		rsInvalidListStyle,
		// Token: 0x04002BE1 RID: 11233
		rsInvalidMarkupType,
		// Token: 0x04002BE2 RID: 11234
		rsInvalidRichTextParseFailed,
		// Token: 0x04002BE3 RID: 11235
		rsParseErrorOutOfRangeSize,
		// Token: 0x04002BE4 RID: 11236
		rsParseErrorInvalidSize,
		// Token: 0x04002BE5 RID: 11237
		rsParseErrorInvalidValue,
		// Token: 0x04002BE6 RID: 11238
		rsParseErrorInvalidColor,
		// Token: 0x04002BE7 RID: 11239
		rsMissingAggregateScopeInPageSection,
		// Token: 0x04002BE8 RID: 11240
		rsReportItemInScopedAggregate,
		// Token: 0x04002BE9 RID: 11241
		rsPageNumberInScopedAggregate,
		// Token: 0x04002BEA RID: 11242
		rsVariableInDataRowSortExpression,
		// Token: 0x04002BEB RID: 11243
		rsAggregateInDataRowSortExpression,
		// Token: 0x04002BEC RID: 11244
		rsInvalidOperation,
		// Token: 0x04002BED RID: 11245
		rsInvalidParameterRange,
		// Token: 0x04002BEE RID: 11246
		rsInvalidParameterValue,
		// Token: 0x04002BEF RID: 11247
		rsNotInCollection,
		// Token: 0x04002BF0 RID: 11248
		rsVariableInDataRegionOrDataSetFilterExpression,
		// Token: 0x04002BF1 RID: 11249
		rsInvalidCollationCultureName,
		// Token: 0x04002BF2 RID: 11250
		rsNestedLookups,
		// Token: 0x04002BF3 RID: 11251
		rsLookupInFilterExpression,
		// Token: 0x04002BF4 RID: 11252
		rsInvalidLookupScope,
		// Token: 0x04002BF5 RID: 11253
		rsLookupOfVariable,
		// Token: 0x04002BF6 RID: 11254
		rsReportItemInLookupDestinationOrResult,
		// Token: 0x04002BF7 RID: 11255
		rsAggregateInLookupDestinationOrResult,
		// Token: 0x04002BF8 RID: 11256
		rsLookupOfInvalidExpressionDataType,
		// Token: 0x04002BF9 RID: 11257
		rsNonExistingLookupReference,
		// Token: 0x04002BFA RID: 11258
		rsPagePropertyInSubsequentReportSection,
		// Token: 0x04002BFB RID: 11259
		rsInvalidPageSectionState,
		// Token: 0x04002BFC RID: 11260
		rsReportItemReferenceInPageSection,
		// Token: 0x04002BFD RID: 11261
		rsInvalidColumnsInReportSection,
		// Token: 0x04002BFE RID: 11262
		rsRowNumberInLookupDestinationOrResult,
		// Token: 0x04002BFF RID: 11263
		rsPreviousInLookupDestinationOrResult,
		// Token: 0x04002C00 RID: 11264
		rsLevelCallRecursiveHierarchyBothDimensions,
		// Token: 0x04002C01 RID: 11265
		rsSandboxingCustomCodeNotAllowed,
		// Token: 0x04002C02 RID: 11266
		rsSandboxingExpressionExceedsMaximumLength,
		// Token: 0x04002C03 RID: 11267
		rsSandboxingStringResultExceedsMaximumLength,
		// Token: 0x04002C04 RID: 11268
		rsSandboxingArrayResultExceedsMaximumLength,
		// Token: 0x04002C05 RID: 11269
		rsSandboxingInvalidExpression,
		// Token: 0x04002C06 RID: 11270
		rsSandboxingInvalidTypeOrMemberName,
		// Token: 0x04002C07 RID: 11271
		rsSandboxingInvalidNewType,
		// Token: 0x04002C08 RID: 11272
		rsSandboxingInvalidClassName,
		// Token: 0x04002C09 RID: 11273
		rsSandboxingInvalidCodeModule,
		// Token: 0x04002C0A RID: 11274
		rsSandboxingCodeModuleUnavailableMode,
		// Token: 0x04002C0B RID: 11275
		rsSandboxingExternalResourceExceedsMaximumSize,
		// Token: 0x04002C0C RID: 11276
		rsInvalidRowMapMemberCannotBeDynamic,
		// Token: 0x04002C0D RID: 11277
		rsInvalidRowMapMemberCannotContainChildMember,
		// Token: 0x04002C0E RID: 11278
		rsInvalidColumnMapMemberCannotContainMultipleChildMember,
		// Token: 0x04002C0F RID: 11279
		rsMapPropertyAlreadyDefined,
		// Token: 0x04002C10 RID: 11280
		rsPageBreakOnMapGroup,
		// Token: 0x04002C11 RID: 11281
		rsMapLayerMissingProperty,
		// Token: 0x04002C12 RID: 11282
		rsMapInvalidShapefileReference,
		// Token: 0x04002C13 RID: 11283
		rsCannotCompareSpatialType,
		// Token: 0x04002C14 RID: 11284
		rsUnionOfNonSpatialData,
		// Token: 0x04002C15 RID: 11285
		rsUnionOfMixedSpatialTypes,
		// Token: 0x04002C16 RID: 11286
		rsInvalidMapDataRegionName,
		// Token: 0x04002C17 RID: 11287
		rsOverallPageNumberInBody,
		// Token: 0x04002C18 RID: 11288
		rsOverallPageNumberInScopedAggregate,
		// Token: 0x04002C19 RID: 11289
		rsInvalidRenderFormatUsage,
		// Token: 0x04002C1A RID: 11290
		rsInvalidWritableVariable,
		// Token: 0x04002C1B RID: 11291
		rsVariableTypeNotSerializable,
		// Token: 0x04002C1C RID: 11292
		rsUnexpectedSerializationError,
		// Token: 0x04002C1D RID: 11293
		rsInvalidNestedAggregateScope,
		// Token: 0x04002C1E RID: 11294
		rsNestedCustomAggregate,
		// Token: 0x04002C1F RID: 11295
		rsInvalidNestedRecursiveAggregate,
		// Token: 0x04002C20 RID: 11296
		rsInvalidNestedDataSetAggregate,
		// Token: 0x04002C21 RID: 11297
		rsDataSetAggregateOfAggregates,
		// Token: 0x04002C22 RID: 11298
		rsPostSortAggregateInAggregateExpression,
		// Token: 0x04002C23 RID: 11299
		rsRunningValueInAggregateExpression,
		// Token: 0x04002C24 RID: 11300
		rsPreviousInAggregateExpression,
		// Token: 0x04002C25 RID: 11301
		rsRecursiveAggregateOfAggregate,
		// Token: 0x04002C26 RID: 11302
		rsNestedAggregateViaLookup,
		// Token: 0x04002C27 RID: 11303
		rsNestedAggregateInPageSection,
		// Token: 0x04002C28 RID: 11304
		rsNestedAggregateInFilterExpression,
		// Token: 0x04002C29 RID: 11305
		rsNestedAggregateInGroupVariable,
		// Token: 0x04002C2A RID: 11306
		rsNestedAggregateScopesFromDifferentAxes,
		// Token: 0x04002C2B RID: 11307
		rsIncompatibleNestedAggregateScopes,
		// Token: 0x04002C2C RID: 11308
		rsNestedAggregateScopeRequired,
		// Token: 0x04002C2D RID: 11309
		rsInvalidGroupingDomainScopeWithParent,
		// Token: 0x04002C2E RID: 11310
		rsInvalidGroupingDomainScopeTargetWithParent,
		// Token: 0x04002C2F RID: 11311
		rsInvalidGroupingDomainScopeWithDetailGroup,
		// Token: 0x04002C30 RID: 11312
		rsInvalidGroupingDomainScope,
		// Token: 0x04002C31 RID: 11313
		rsInvalidGroupingDomainScopeDataSet,
		// Token: 0x04002C32 RID: 11314
		rsInvalidGroupingDomainScopeNotAncestor,
		// Token: 0x04002C33 RID: 11315
		rsInvalidGroupingDomainScopeNotLeaf,
		// Token: 0x04002C34 RID: 11316
		rsInvalidGroupingDomainScopeMap,
		// Token: 0x04002C35 RID: 11317
		rsInvalidSortExpressionScopeDomainScope,
		// Token: 0x04002C36 RID: 11318
		rsStateIndicatorInvalidAutoGenerateMinMaxExpression,
		// Token: 0x04002C37 RID: 11319
		rsStateIndicatorInvalidTransformationScope,
		// Token: 0x04002C38 RID: 11320
		rsInvalidDataSetQuery,
		// Token: 0x04002C39 RID: 11321
		rsDataSetReferenceNotPublished,
		// Token: 0x04002C3A RID: 11322
		rsNotASharedDataSetDefinition,
		// Token: 0x04002C3B RID: 11323
		rsInvalidSharedDataSetDefinition,
		// Token: 0x04002C3C RID: 11324
		rsMissingDataSetParameterDefault,
		// Token: 0x04002C3D RID: 11325
		rsInvalidCollationName,
		// Token: 0x04002C3E RID: 11326
		rsDataSetWithoutFields,
		// Token: 0x04002C3F RID: 11327
		rsMapUnsupportedValueFieldType,
		// Token: 0x04002C40 RID: 11328
		rsPreviousAggregateInVariableExpression,
		// Token: 0x04002C41 RID: 11329
		rsFieldReferenceAmbiguous,
		// Token: 0x04002C42 RID: 11330
		rsInvalidFilterValueDataType,
		// Token: 0x04002C43 RID: 11331
		rsParameterValueCastFailure,
		// Token: 0x04002C44 RID: 11332
		rsInvalidFeatureRdlElement,
		// Token: 0x04002C45 RID: 11333
		rsInvalidFeatureRdlExpression,
		// Token: 0x04002C46 RID: 11334
		rsInvalidFeatureRdlExpressionAggregatesOfAggregates,
		// Token: 0x04002C47 RID: 11335
		rsRenderingChunksUnavailable,
		// Token: 0x04002C48 RID: 11336
		rsInvalidGroupingNaturalGroupFeature,
		// Token: 0x04002C49 RID: 11337
		rsInvalidGroupingContainerNotNaturalGroup,
		// Token: 0x04002C4A RID: 11338
		rsConflictingNaturalGroupRequirements,
		// Token: 0x04002C4B RID: 11339
		rsVariableInJoinExpression,
		// Token: 0x04002C4C RID: 11340
		rsReportItemInJoinExpression,
		// Token: 0x04002C4D RID: 11341
		rsRunningValueInJoinExpression,
		// Token: 0x04002C4E RID: 11342
		rsPreviousAggregateInJoinExpression,
		// Token: 0x04002C4F RID: 11343
		rsAggregateInJoinExpression,
		// Token: 0x04002C50 RID: 11344
		rsElementMustContainChildren,
		// Token: 0x04002C51 RID: 11345
		rsElementMustContainChild,
		// Token: 0x04002C52 RID: 11346
		rsInvalidFeatureRdlPropertyValue,
		// Token: 0x04002C53 RID: 11347
		rsInvalidNaturalSortContainer,
		// Token: 0x04002C54 RID: 11348
		rsInvalidSortingContainerNotNaturalSort,
		// Token: 0x04002C55 RID: 11349
		rsConflictingNaturalSortRequirements,
		// Token: 0x04002C56 RID: 11350
		rsIncompatibleNaturalSortAndNaturalGroup,
		// Token: 0x04002C57 RID: 11351
		rsInvalidSortFlagCombination,
		// Token: 0x04002C58 RID: 11352
		rsInvalidBandInvalidLayoutDirection,
		// Token: 0x04002C59 RID: 11353
		rsInvalidBandPageBreakIsSet,
		// Token: 0x04002C5A RID: 11354
		rsInvalidBandShouldNotBeTogglable,
		// Token: 0x04002C5B RID: 11355
		rsBandKeepTogetherIgnored,
		// Token: 0x04002C5C RID: 11356
		rsBandIgnoredProperties,
		// Token: 0x04002C5D RID: 11357
		rsInvalidBandNavigationReference,
		// Token: 0x04002C5E RID: 11358
		rsInvalidBandNavigationItem,
		// Token: 0x04002C5F RID: 11359
		rsInvalidBandNavigations,
		// Token: 0x04002C60 RID: 11360
		rsInvalidSliderDataSetReferenceField,
		// Token: 0x04002C61 RID: 11361
		rsInvalidSliderDataSetReference,
		// Token: 0x04002C62 RID: 11362
		rsNotSupportedInStreamingMode,
		// Token: 0x04002C63 RID: 11363
		rsInvalidScopeID,
		// Token: 0x04002C64 RID: 11364
		rsInvalidScopeIDOrder,
		// Token: 0x04002C65 RID: 11365
		rsInvalidScopeIDNotSet,
		// Token: 0x04002C66 RID: 11366
		rsDetailGroupsNotSupportedInStreamingMode,
		// Token: 0x04002C67 RID: 11367
		rsRombasedRestartFailed,
		// Token: 0x04002C68 RID: 11368
		rsRombasedRestartFailedTypeMismatch,
		// Token: 0x04002C69 RID: 11369
		rsMissingDefaultRelationshipJoinCondition,
		// Token: 0x04002C6A RID: 11370
		rsNonExistingRelationshipRelatedScope,
		// Token: 0x04002C6B RID: 11371
		rsInvalidSelfJoinRelationship,
		// Token: 0x04002C6C RID: 11372
		rsInvalidDefaultRelationshipNotNaturalJoin,
		// Token: 0x04002C6D RID: 11373
		rsInvalidRelationshipGroupingContainerNotNaturalGroup,
		// Token: 0x04002C6E RID: 11374
		rsInvalidRelationshipContainerNotNaturalJoin,
		// Token: 0x04002C6F RID: 11375
		rsInvalidDefaultRelationshipDuplicateRelatedDataset,
		// Token: 0x04002C70 RID: 11376
		rsInvalidRelationshipDataSetUsedMoreThanOnce,
		// Token: 0x04002C71 RID: 11377
		rsInvalidRelationshipDataSet,
		// Token: 0x04002C72 RID: 11378
		rsInvalidDefaultRelationshipCircularReference,
		// Token: 0x04002C73 RID: 11379
		rsMissingIntersectionDataSetName,
		// Token: 0x04002C74 RID: 11380
		rsInvalidRelationshipTopLevelDataRegion,
		// Token: 0x04002C75 RID: 11381
		rsDefaultRelationshipIgnored,
		// Token: 0x04002C76 RID: 11382
		rsInvalidDataSetScopedAggregate,
		// Token: 0x04002C77 RID: 11383
		rsMissingIntersectionRelationshipParentScope,
		// Token: 0x04002C78 RID: 11384
		rsUnexpectedCellDataSetName,
		// Token: 0x04002C79 RID: 11385
		rsInvalidRelationshipDuplicateParentScope,
		// Token: 0x04002C7A RID: 11386
		rsInvalidCellDataSetName,
		// Token: 0x04002C7B RID: 11387
		rsInvalidIntersectionNaturalJoin,
		// Token: 0x04002C7C RID: 11388
		rsInvalidNaturalCrossJoin,
		// Token: 0x04002C7D RID: 11389
		rsInvalidIntersectionNaturalCrossJoin,
		// Token: 0x04002C7E RID: 11390
		rsInvalidSortDirectionMustNotBeSpecified,
		// Token: 0x04002C7F RID: 11391
		rsInvalidNaturalSortGroupExpressionNotSimpleFieldReference,
		// Token: 0x04002C80 RID: 11392
		rsInvalidPeerGroupsNotSupported,
		// Token: 0x04002C81 RID: 11393
		rsInvalidAggregateIndicatorField,
		// Token: 0x04002C82 RID: 11394
		rsAggregateIndicatorFieldOnCalculatedField,
		// Token: 0x04002C83 RID: 11395
		rsMissingOrInvalidAggregateIndicatorFieldValue,
		// Token: 0x04002C84 RID: 11396
		rsErrorCancelingCommand,
		// Token: 0x04002C85 RID: 11397
		rsCollationAndCollationCultureSpecified,
		// Token: 0x04002C86 RID: 11398
		rsErrorDisposingDataReader,
		// Token: 0x04002C87 RID: 11399
		rsSerializableTypeNotSupported,
		// Token: 0x04002C88 RID: 11400
		rsInvalidFeatureRdlAttribute,
		// Token: 0x04002C89 RID: 11401
		rsDuplicateReportSectionName,
		// Token: 0x04002C8A RID: 11402
		rsConflictingSortFlags,
		// Token: 0x04002C8B RID: 11403
		rsInvalidDeferredSortContainer,
		// Token: 0x04002C8C RID: 11404
		rsInvalidEmbeddingModeImageProperty,
		// Token: 0x04002C8D RID: 11405
		rsInvalidScopeCollectionReference,
		// Token: 0x04002C8E RID: 11406
		rsInvalidScopeReference,
		// Token: 0x04002C8F RID: 11407
		rsScopeReferenceInComplexExpression,
		// Token: 0x04002C90 RID: 11408
		rsScopeReferenceUsesDataSetMoreThanOnce,
		// Token: 0x04002C91 RID: 11409
		rsInvalidComplexExpression,
		// Token: 0x04002C92 RID: 11410
		rsInvalidComplexExpressionInReport,
		// Token: 0x04002C93 RID: 11411
		rsDataShapeDefinitionError,
		// Token: 0x04002C94 RID: 11412
		rsInvalidParameterLayoutNumberOfRowsOrColumnsExceedingLimit,
		// Token: 0x04002C95 RID: 11413
		rsInvalidParameterLayoutNumberOfConsecutiveEmptyRowsExceedingLimit,
		// Token: 0x04002C96 RID: 11414
		rsInvalidParameterLayoutZeroOrLessRowOrCol,
		// Token: 0x04002C97 RID: 11415
		rsInvalidParameterLayoutParametersMissingFromPanel,
		// Token: 0x04002C98 RID: 11416
		rsInvalidParameterLayoutCellDefNotEqualsParameterCount,
		// Token: 0x04002C99 RID: 11417
		rsInvalidParameterLayoutParameterAppearsTwice,
		// Token: 0x04002C9A RID: 11418
		rsInvalidParameterLayoutParameterNameMissing,
		// Token: 0x04002C9B RID: 11419
		rsInvalidParameterLayoutRowColOutOfBounds,
		// Token: 0x04002C9C RID: 11420
		rsInvalidParameterLayoutCellCollition,
		// Token: 0x04002C9D RID: 11421
		rsInvalidParameterLayoutParameterNotVisible,
		// Token: 0x04002C9E RID: 11422
		rsInvalidMustUnderstandNamespaces
	}
}
