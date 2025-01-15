using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000655 RID: 1621
	internal class RPResWrapper
	{
		// Token: 0x060057CF RID: 22479 RVA: 0x0016FFEC File Offset: 0x0016E1EC
		public static string rsAggregateInPreviousAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateInPreviousAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x0016FFFB File Offset: 0x0016E1FB
		public static string rsRunningValueInPreviousAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRunningValueInPreviousAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x0017000A File Offset: 0x0016E20A
		public static string rsPreviousInPreviousAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPreviousInPreviousAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x00170019 File Offset: 0x0016E219
		public static string rsRowNumberInPreviousAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRowNumberInPreviousAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x00170028 File Offset: 0x0016E228
		public static string rsInScopeOrLevelInPreviousAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInScopeOrLevelInPreviousAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x00170037 File Offset: 0x0016E237
		public static string rsInvalidScopeInInnerAggregateOfPreviousAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidScopeInInnerAggregateOfPreviousAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x00170046 File Offset: 0x0016E246
		public static string rsVariableInPreviousAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsVariableInPreviousAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x00170055 File Offset: 0x0016E255
		public static string rsVariableInCalculatedFieldExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsVariableInCalculatedFieldExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x00170064 File Offset: 0x0016E264
		public static string rsVariableInGroupExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsVariableInGroupExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x00170073 File Offset: 0x0016E273
		public static string rsVariableInQueryParameterExpression(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsVariableInQueryParameterExpression", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x00170083 File Offset: 0x0016E283
		public static string rsVariableInReportParameterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsVariableInReportParameterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x00170092 File Offset: 0x0016E292
		public static string rsVariableInReportLanguageExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsVariableInReportLanguageExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x001700A1 File Offset: 0x0016E2A1
		public static string rsVariableInDataRowSortExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsVariableInDataRowSortExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057DC RID: 22492 RVA: 0x001700B0 File Offset: 0x0016E2B0
		public static string rsVariableInJoinExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsVariableInJoinExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057DD RID: 22493 RVA: 0x001700BF File Offset: 0x0016E2BF
		public static string rsAggregateofVariable(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateofVariable", objectType, objectName, propertyName);
		}

		// Token: 0x060057DE RID: 22494 RVA: 0x001700CE File Offset: 0x0016E2CE
		public static string rsLookupOfVariable(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsLookupOfVariable", objectType, objectName, propertyName);
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x001700DD File Offset: 0x0016E2DD
		public static string rsReportItemInLookupDestinationOrResult(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsReportItemInLookupDestinationOrResult", objectType, objectName, propertyName);
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x001700EC File Offset: 0x0016E2EC
		public static string rsAggregateInLookupDestinationOrResult(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateInLookupDestinationOrResult", objectType, objectName, propertyName);
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x001700FB File Offset: 0x0016E2FB
		public static string rsRowNumberInLookupDestinationOrResult(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRowNumberInLookupDestinationOrResult", objectType, objectName, propertyName);
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x0017010A File Offset: 0x0016E30A
		public static string rsPreviousInLookupDestinationOrResult(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPreviousInLookupDestinationOrResult", objectType, objectName, propertyName);
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x00170119 File Offset: 0x0016E319
		public static string rsVariableInDataRegionOrDataSetFilterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsVariableInDataRegionOrDataSetFilterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x00170128 File Offset: 0x0016E328
		public static string rsAggregateInFilterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateInFilterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x00170137 File Offset: 0x0016E337
		public static string rsAggregateInGroupExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateInGroupExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x00170146 File Offset: 0x0016E346
		public static string rsAggregateInDataRowSortExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateInDataRowSortExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x00170155 File Offset: 0x0016E355
		public static string rsAggregateInQueryParameterExpression(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateInQueryParameterExpression", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x00170165 File Offset: 0x0016E365
		public static string rsAggregateInReportParameterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateInReportParameterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x00170174 File Offset: 0x0016E374
		public static string rsAggregateInReportLanguageExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateInReportLanguageExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x00170183 File Offset: 0x0016E383
		public static string rsAggregateInCalculatedFieldExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateInCalculatedFieldExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x00170192 File Offset: 0x0016E392
		public static string rsAggregateInJoinExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateInJoinExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x001701A1 File Offset: 0x0016E3A1
		public static string rsAggregateofAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateofAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060057ED RID: 22509 RVA: 0x001701B0 File Offset: 0x0016E3B0
		public static string rsNestedCustomAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNestedCustomAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x001701BF File Offset: 0x0016E3BF
		public static string rsAggregateReportItemInBody(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateReportItemInBody", objectType, objectName, propertyName);
		}

		// Token: 0x060057EF RID: 22511 RVA: 0x001701CE File Offset: 0x0016E3CE
		public static string rsBinaryConstant(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsBinaryConstant", objectType, objectName, propertyName);
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x001701DD File Offset: 0x0016E3DD
		public static string rsChartSeriesPlotTypeIgnored(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsChartSeriesPlotTypeIgnored", objectType, objectName, propertyName);
		}

		// Token: 0x060057F1 RID: 22513 RVA: 0x001701EC File Offset: 0x0016E3EC
		public static string rsCompilerErrorInExpression(string objectType, string objectName, string propertyName, string error)
		{
			return RPResWrapper.Keys.GetString("rsCompilerErrorInExpression", objectType, objectName, propertyName, error);
		}

		// Token: 0x060057F2 RID: 22514 RVA: 0x001701FC File Offset: 0x0016E3FC
		public static string rsCompilerErrorInCode(string objectType, string objectName, string propertyName, string error, string lineNumber)
		{
			return RPResWrapper.Keys.GetString("rsCompilerErrorInCode", objectType, objectName, propertyName, error, lineNumber);
		}

		// Token: 0x060057F3 RID: 22515 RVA: 0x0017020E File Offset: 0x0016E40E
		public static string rsCompilerErrorInClassInstanceDeclaration(string objectType, string objectName, string propertyName, string error)
		{
			return RPResWrapper.Keys.GetString("rsCompilerErrorInClassInstanceDeclaration", objectType, objectName, propertyName, error);
		}

		// Token: 0x060057F4 RID: 22516 RVA: 0x0017021E File Offset: 0x0016E41E
		public static string rsUnexpectedCompilerError(string objectType, string objectName, string propertyName, string nativeErrorCode)
		{
			return RPResWrapper.Keys.GetString("rsUnexpectedCompilerError", objectType, objectName, propertyName, nativeErrorCode);
		}

		// Token: 0x060057F5 RID: 22517 RVA: 0x0017022E File Offset: 0x0016E42E
		public static string rsConflictingRunningValueScopesInMatrix(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsConflictingRunningValueScopesInMatrix", objectType, objectName, propertyName);
		}

		// Token: 0x060057F6 RID: 22518 RVA: 0x0017023D File Offset: 0x0016E43D
		public static string rsConflictingRunningValueScopesInTablix(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsConflictingRunningValueScopesInTablix", objectType, objectName, propertyName);
		}

		// Token: 0x060057F7 RID: 22519 RVA: 0x0017024C File Offset: 0x0016E44C
		public static string rsCountRowsInPageSectionExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCountRowsInPageSectionExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060057F8 RID: 22520 RVA: 0x0017025B File Offset: 0x0016E45B
		public static string rsCountStarNotSupported(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCountStarNotSupported", objectType, objectName, propertyName);
		}

		// Token: 0x060057F9 RID: 22521 RVA: 0x0017026A File Offset: 0x0016E46A
		public static string rsCountStarRVNotSupported(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCountStarRVNotSupported", objectType, objectName, propertyName);
		}

		// Token: 0x060057FA RID: 22522 RVA: 0x00170279 File Offset: 0x0016E479
		public static string rsCustomAggregateAndFilter(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCustomAggregateAndFilter", objectType, objectName, propertyName);
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x00170288 File Offset: 0x0016E488
		public static string rsDataRegionInDetailList(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDataRegionInDetailList", objectType, objectName, propertyName);
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x00170297 File Offset: 0x0016E497
		public static string rsDataRegionInPageSection(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDataRegionInPageSection", objectType, objectName, propertyName);
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x001702A6 File Offset: 0x0016E4A6
		public static string rsDataRegionInTableDetailRow(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDataRegionInTableDetailRow", objectType, objectName, propertyName);
		}

		// Token: 0x060057FE RID: 22526 RVA: 0x001702B5 File Offset: 0x0016E4B5
		public static string rsDataRegionWithoutDataSet(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDataRegionWithoutDataSet", objectType, objectName, propertyName);
		}

		// Token: 0x060057FF RID: 22527 RVA: 0x001702C4 File Offset: 0x0016E4C4
		public static string rsDataSourceReferenceNotPublished(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsDataSourceReferenceNotPublished", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005800 RID: 22528 RVA: 0x001702D4 File Offset: 0x0016E4D4
		public static string rsDataSourceInPageSectionExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDataSourceInPageSectionExpression", objectType, objectName, propertyName);
		}

		// Token: 0x06005801 RID: 22529 RVA: 0x001702E3 File Offset: 0x0016E4E3
		public static string rsDataSourceInQueryParameterExpression(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsDataSourceInQueryParameterExpression", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x06005802 RID: 22530 RVA: 0x001702F3 File Offset: 0x0016E4F3
		public static string rsDataSourceInReportLanguageExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDataSourceInReportLanguageExpression", objectType, objectName, propertyName);
		}

		// Token: 0x06005803 RID: 22531 RVA: 0x00170302 File Offset: 0x0016E502
		public static string rsDataSourceInReportParameterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDataSourceInReportParameterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x06005804 RID: 22532 RVA: 0x00170311 File Offset: 0x0016E511
		public static string rsDuplicateChartColumnName(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateChartColumnName", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005805 RID: 22533 RVA: 0x00170321 File Offset: 0x0016E521
		public static string rsDuplicateClassInstanceName(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateClassInstanceName", objectType, objectName, propertyName);
		}

		// Token: 0x06005806 RID: 22534 RVA: 0x00170330 File Offset: 0x0016E530
		public static string rsDataSetReferenceNotPublished(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsDataSetReferenceNotPublished", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005807 RID: 22535 RVA: 0x00170340 File Offset: 0x0016E540
		public static string rsPagePropertyInSubsequentReportSection(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPagePropertyInSubsequentReportSection", objectType, objectName, propertyName);
		}

		// Token: 0x06005808 RID: 22536 RVA: 0x0017034F File Offset: 0x0016E54F
		public static string rsInvalidPageSectionState(int sectionIndex)
		{
			return RPResWrapper.Keys.GetString("rsInvalidPageSectionState", sectionIndex);
		}

		// Token: 0x06005809 RID: 22537 RVA: 0x00170361 File Offset: 0x0016E561
		public static string rsNestedLookups(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNestedLookups", objectType, objectName, propertyName);
		}

		// Token: 0x0600580A RID: 22538 RVA: 0x00170370 File Offset: 0x0016E570
		public static string rsLookupInFilterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsLookupInFilterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x0600580B RID: 22539 RVA: 0x0017037F File Offset: 0x0016E57F
		public static string rsInvalidLookupScope(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidLookupScope", objectType, objectName, propertyName);
		}

		// Token: 0x0600580C RID: 22540 RVA: 0x0017038E File Offset: 0x0016E58E
		public static string rsDuplicateDataSourceName(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateDataSourceName", objectType, objectName, propertyName);
		}

		// Token: 0x0600580D RID: 22541 RVA: 0x0017039D File Offset: 0x0016E59D
		public static string rsInvalidDataSourceNameLength(string objectType, string objectName, string propertyName, string maxLength)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDataSourceNameLength", objectType, objectName, propertyName, maxLength);
		}

		// Token: 0x0600580E RID: 22542 RVA: 0x001703AD File Offset: 0x0016E5AD
		public static string rsInvalidDataSourceNameNotCLSCompliant(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDataSourceNameNotCLSCompliant", objectType, objectName, propertyName);
		}

		// Token: 0x0600580F RID: 22543 RVA: 0x001703BC File Offset: 0x0016E5BC
		public static string rsInvalidEmbeddedImageNameNotCLSCompliant(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidEmbeddedImageNameNotCLSCompliant", objectType, objectName, propertyName);
		}

		// Token: 0x06005810 RID: 22544 RVA: 0x001703CB File Offset: 0x0016E5CB
		public static string rsInvalidEmbeddedImageNameLength(string objectType, string objectName, string propertyName, string maxLength)
		{
			return RPResWrapper.Keys.GetString("rsInvalidEmbeddedImageNameLength", objectType, objectName, propertyName, maxLength);
		}

		// Token: 0x06005811 RID: 22545 RVA: 0x001703DB File Offset: 0x0016E5DB
		public static string rsDuplicateEmbeddedImageName(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateEmbeddedImageName", objectType, objectName, propertyName);
		}

		// Token: 0x06005812 RID: 22546 RVA: 0x001703EA File Offset: 0x0016E5EA
		public static string rsDuplicateReportSectionName(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateReportSectionName", objectType, objectName, propertyName);
		}

		// Token: 0x06005813 RID: 22547 RVA: 0x001703F9 File Offset: 0x0016E5F9
		public static string rsDuplicateFieldName(string objectType, string objectName, string propertyName, string offendingValue, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateFieldName", objectType, objectName, propertyName, offendingValue, dataSetName);
		}

		// Token: 0x06005814 RID: 22548 RVA: 0x0017040B File Offset: 0x0016E60B
		public static string rsDuplicateParameterName(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateParameterName", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005815 RID: 22549 RVA: 0x0017041B File Offset: 0x0016E61B
		public static string rsDuplicateReportItemName(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateReportItemName", objectType, objectName, propertyName);
		}

		// Token: 0x06005816 RID: 22550 RVA: 0x0017042A File Offset: 0x0016E62A
		public static string rsDuplicateReportParameterName(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateReportParameterName", objectType, objectName, propertyName);
		}

		// Token: 0x06005817 RID: 22551 RVA: 0x00170439 File Offset: 0x0016E639
		public static string rsDuplicateCaseInsensitiveReportParameterName(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateCaseInsensitiveReportParameterName", objectType, objectName, propertyName);
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x00170448 File Offset: 0x0016E648
		public static string rsDuplicateScopeName(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateScopeName", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005819 RID: 22553 RVA: 0x00170458 File Offset: 0x0016E658
		public static string rsExpressionMissingCloseParen(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsExpressionMissingCloseParen", objectType, objectName, propertyName);
		}

		// Token: 0x0600581A RID: 22554 RVA: 0x00170467 File Offset: 0x0016E667
		public static string rsFieldInPageSectionExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsFieldInPageSectionExpression", objectType, objectName, propertyName);
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x00170476 File Offset: 0x0016E676
		public static string rsFieldInQueryParameterExpression(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsFieldInQueryParameterExpression", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x00170486 File Offset: 0x0016E686
		public static string rsFieldInReportParameterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsFieldInReportParameterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x00170495 File Offset: 0x0016E695
		public static string rsFieldInReportLanguageExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsFieldInReportLanguageExpression", objectType, objectName, propertyName);
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x001704A4 File Offset: 0x0016E6A4
		public static string rsGlobalNotDefined(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsGlobalNotDefined", objectType, objectName, propertyName);
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x001704B3 File Offset: 0x0016E6B3
		public static string rsDataSetInPageSectionExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDataSetInPageSectionExpression", objectType, objectName, propertyName);
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x001704C2 File Offset: 0x0016E6C2
		public static string rsDataSetInQueryParameterExpression(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsDataSetInQueryParameterExpression", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x06005821 RID: 22561 RVA: 0x001704D2 File Offset: 0x0016E6D2
		public static string rsDataSetInReportLanguageExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDataSetInReportLanguageExpression", objectType, objectName, propertyName);
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x001704E1 File Offset: 0x0016E6E1
		public static string rsDataSetInReportParameterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDataSetInReportParameterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x06005823 RID: 22563 RVA: 0x001704F0 File Offset: 0x0016E6F0
		public static string rsInvalidActionLabel(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidActionLabel", objectType, objectName, propertyName);
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x001704FF File Offset: 0x0016E6FF
		public static string rsInvalidAction(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidAction", objectType, objectName, propertyName);
		}

		// Token: 0x06005825 RID: 22565 RVA: 0x0017050E File Offset: 0x0016E70E
		public static string rsInvalidAggregateScope(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidAggregateScope", objectType, objectName, propertyName);
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x0017051D File Offset: 0x0016E71D
		public static string rsInvalidNestedAggregateScope(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidNestedAggregateScope", objectType, objectName, propertyName);
		}

		// Token: 0x06005827 RID: 22567 RVA: 0x0017052C File Offset: 0x0016E72C
		public static string rsNestedAggregateScopesFromDifferentAxes(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNestedAggregateScopesFromDifferentAxes", objectType, objectName, propertyName);
		}

		// Token: 0x06005828 RID: 22568 RVA: 0x0017053B File Offset: 0x0016E73B
		public static string rsIncompatibleNestedAggregateScopes(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsIncompatibleNestedAggregateScopes", objectType, objectName, propertyName);
		}

		// Token: 0x06005829 RID: 22569 RVA: 0x0017054A File Offset: 0x0016E74A
		public static string rsNestedAggregateScopeRequired(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNestedAggregateScopeRequired", objectType, objectName, propertyName);
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x00170559 File Offset: 0x0016E759
		public static string rsInvalidNestedDataSetAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidNestedDataSetAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x00170568 File Offset: 0x0016E768
		public static string rsDataSetAggregateOfAggregates(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDataSetAggregateOfAggregates", objectType, objectName, propertyName);
		}

		// Token: 0x0600582C RID: 22572 RVA: 0x00170577 File Offset: 0x0016E777
		public static string rsInvalidAltReportItem(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidAltReportItem", objectType, objectName, propertyName);
		}

		// Token: 0x0600582D RID: 22573 RVA: 0x00170586 File Offset: 0x0016E786
		public static string rsInvalidBooleanConstant(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidBooleanConstant", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600582E RID: 22574 RVA: 0x00170596 File Offset: 0x0016E796
		public static string rsInvalidDateTimeConstant(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDateTimeConstant", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x001705A6 File Offset: 0x0016E7A6
		public static string rsInvalidFloatConstant(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFloatConstant", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x001705B6 File Offset: 0x0016E7B6
		public static string rsInvalidCategoryGrouping(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidCategoryGrouping", objectType, objectName, propertyName);
		}

		// Token: 0x06005831 RID: 22577 RVA: 0x001705C5 File Offset: 0x0016E7C5
		public static string rsInvalidChartHierarchy(string objectType, string objectName, string propertyName, string level)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChartHierarchy", objectType, objectName, propertyName, level);
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x001705D5 File Offset: 0x0016E7D5
		public static string rsInvalidChartMemberMustContainGroupExpressions(string objectType, string objectName, string propertyName, string subPropertyName, string groupName, string subsubPropertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChartMemberMustContainGroupExpressions", objectType, objectName, propertyName, subPropertyName, groupName, subsubPropertyName);
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x001705E9 File Offset: 0x0016E7E9
		public static string rsInvalidChartMemberMustBeDynamic(string objectType, string objectName, string propertyName, string subPropertyName, string groupName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChartMemberMustBeDynamic", objectType, objectName, propertyName, subPropertyName, groupName);
		}

		// Token: 0x06005834 RID: 22580 RVA: 0x001705FB File Offset: 0x0016E7FB
		public static string rsInvlaidAxisAngle(string objectType, string objectName, string propertyName, string subPropertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvlaidAxisAngle", objectType, objectName, propertyName, subPropertyName);
		}

		// Token: 0x06005835 RID: 22581 RVA: 0x0017060B File Offset: 0x0016E80B
		public static string rsInvalidCharacterInExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidCharacterInExpression", objectType, objectName, propertyName);
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x0017061A File Offset: 0x0016E81A
		public static string rsInvalidChartColumnNameNotCLSCompliant(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChartColumnNameNotCLSCompliant", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005837 RID: 22583 RVA: 0x0017062A File Offset: 0x0016E82A
		public static string rsInvalidChartColumnNameLength(string objectType, string objectName, string propertyName, string offendingValue, string maxLength)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChartColumnNameLength", objectType, objectName, propertyName, offendingValue, maxLength);
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x0017063C File Offset: 0x0016E83C
		public static string rsInvalidChartGroupings(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChartGroupings", objectType, objectName, propertyName);
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x0017064B File Offset: 0x0016E84B
		public static string rsInvalidChartSubType(string objectType, string objectName, string propertyName, string chartType, string chartSubType)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChartSubType", objectType, objectName, propertyName, chartType, chartSubType);
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x0017065D File Offset: 0x0016E85D
		public static string rsInvalidColumnGrouping(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidColumnGrouping", objectType, objectName, propertyName);
		}

		// Token: 0x0600583B RID: 22587 RVA: 0x0017066C File Offset: 0x0016E86C
		public static string rsInvalidColumnsInBody(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidColumnsInBody", objectType, objectName, propertyName);
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x0017067B File Offset: 0x0016E87B
		public static string rsInvalidColumnsInReportSection(string objectType, string objectName, string propertyName, string sectionIndex)
		{
			return RPResWrapper.Keys.GetString("rsInvalidColumnsInReportSection", objectType, objectName, propertyName, sectionIndex);
		}

		// Token: 0x0600583D RID: 22589 RVA: 0x0017068B File Offset: 0x0016E88B
		public static string rsInvalidCustomAggregateExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidCustomAggregateExpression", objectType, objectName, propertyName);
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x0017069A File Offset: 0x0016E89A
		public static string rsInvalidCustomAggregateScope(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidCustomAggregateScope", objectType, objectName, propertyName);
		}

		// Token: 0x0600583F RID: 22591 RVA: 0x001706A9 File Offset: 0x0016E8A9
		public static string rsInvalidCustomPropertyName(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidCustomPropertyName", objectType, objectName, propertyName);
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x001706B8 File Offset: 0x0016E8B8
		public static string rsInvalidChartDataValueNameNotUnique(string objectType, string objectName, string propertyName, string propertyNameValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChartDataValueNameNotUnique", objectType, objectName, propertyName, propertyNameValue);
		}

		// Token: 0x06005841 RID: 22593 RVA: 0x001706C8 File Offset: 0x0016E8C8
		public static string rsInvalidObjectNameNotUnique(string objectType, string objectName, string propertyName, string propertyNameValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidObjectNameNotUnique", objectType, objectName, propertyName, propertyNameValue);
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x001706D8 File Offset: 0x0016E8D8
		public static string rsInvalidObjectNameNotCLSCompliant(string objectType, string objectName, string propertyName, string propertyNameValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidObjectNameNotCLSCompliant", objectType, objectName, propertyName, propertyNameValue);
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x001706E8 File Offset: 0x0016E8E8
		public static string rsInvalidChartDataValueNameNotConstant(string objectType, string objectName, string propertyName, string subPropertyName, string dataPointIndex, string dataValueIndex)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChartDataValueNameNotConstant", objectType, objectName, propertyName, subPropertyName, dataPointIndex, dataValueIndex);
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x001706FC File Offset: 0x0016E8FC
		public static string rsInvalidChartDataValueName(string objectType, string objectName, string propertyName, string validNames)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChartDataValueName", objectType, objectName, propertyName, validNames);
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x0017070C File Offset: 0x0016E90C
		public static string rsInvalidSourceSeriesName(string objectType, string objectName, string stringPropertyName, string propertyNameValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSourceSeriesName", objectType, objectName, stringPropertyName, propertyNameValue);
		}

		// Token: 0x06005846 RID: 22598 RVA: 0x0017071C File Offset: 0x0016E91C
		public static string rsInvalidDataElementNameNotCLSCompliant(string objectType, string objectName, string propertyName, string dataRendererElement, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDataElementNameNotCLSCompliant", objectType, objectName, propertyName, dataRendererElement, offendingValue);
		}

		// Token: 0x06005847 RID: 22599 RVA: 0x0017072E File Offset: 0x0016E92E
		public static string rsInvalidDataElementNameLength(string objectType, string objectName, string propertyName, string dataRendererElement, string offendingValue, string maxLength)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDataElementNameLength", objectType, objectName, propertyName, dataRendererElement, offendingValue, maxLength);
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x00170742 File Offset: 0x0016E942
		public static string rsInvalidDataSource(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDataSource", objectType, objectName, propertyName);
		}

		// Token: 0x06005849 RID: 22601 RVA: 0x00170751 File Offset: 0x0016E951
		public static string rsInvalidDataSourceReference(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDataSourceReference", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600584A RID: 22602 RVA: 0x00170761 File Offset: 0x0016E961
		public static string rsInvalidDefaultValue(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDefaultValue", objectType, objectName, propertyName);
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x00170770 File Offset: 0x0016E970
		public static string rsInvalidDefaultValueValues(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDefaultValueValues", objectType, objectName, propertyName);
		}

		// Token: 0x0600584C RID: 22604 RVA: 0x0017077F File Offset: 0x0016E97F
		public static string rsInvalidDataSetReferenceField(string objectType, string objectName, string propertyName, string fieldName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDataSetReferenceField", objectType, objectName, propertyName, fieldName, dataSetName);
		}

		// Token: 0x0600584D RID: 22605 RVA: 0x00170791 File Offset: 0x0016E991
		public static string rsInvalidDetailDataGrouping(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDetailDataGrouping", objectType, objectName, propertyName);
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x001707A0 File Offset: 0x0016E9A0
		public static string rsInvalidEmbeddedImage(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidEmbeddedImage", objectType, objectName, propertyName);
		}

		// Token: 0x0600584F RID: 22607 RVA: 0x001707AF File Offset: 0x0016E9AF
		public static string rsInvalidField(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidField", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x06005850 RID: 22608 RVA: 0x001707BF File Offset: 0x0016E9BF
		public static string rsInvalidFieldNameNotCLSCompliant(string objectType, string objectName, string propertyName, string offendingValue, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFieldNameNotCLSCompliant", objectType, objectName, propertyName, offendingValue, dataSetName);
		}

		// Token: 0x06005851 RID: 22609 RVA: 0x001707D1 File Offset: 0x0016E9D1
		public static string rsInvalidFieldNameLength(string objectType, string objectName, string propertyName, string offendingValue, string datasetName, string maxLength)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFieldNameLength", objectType, objectName, propertyName, offendingValue, datasetName, maxLength);
		}

		// Token: 0x06005852 RID: 22610 RVA: 0x001707E5 File Offset: 0x0016E9E5
		public static string rsInvalidGroupExpressionScope(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupExpressionScope", objectType, objectName, propertyName);
		}

		// Token: 0x06005853 RID: 22611 RVA: 0x001707F4 File Offset: 0x0016E9F4
		public static string rsInvalidGroupingNameNotCLSCompliant(string objectType, string objectName, string propertyName, string groupName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingNameNotCLSCompliant", objectType, objectName, propertyName, groupName);
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x00170804 File Offset: 0x0016EA04
		public static string rsInvalidGroupingNameLength(string objectType, string objectName, string propertyName, string maxLength)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingNameLength", objectType, objectName, propertyName, maxLength);
		}

		// Token: 0x06005855 RID: 22613 RVA: 0x00170814 File Offset: 0x0016EA14
		public static string rsInvalidHideDuplicateScope(string objectType, string objectName, string propertyName, string groupName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidHideDuplicateScope", objectType, objectName, propertyName, groupName);
		}

		// Token: 0x06005856 RID: 22614 RVA: 0x00170824 File Offset: 0x0016EA24
		public static string rsInvalidURLProtocol(string objectType, string objectName, string propertyName, string url, string supportedSchemas)
		{
			return RPResWrapper.Keys.GetString("rsInvalidURLProtocol", objectType, objectName, propertyName, url, supportedSchemas);
		}

		// Token: 0x06005857 RID: 22615 RVA: 0x00170836 File Offset: 0x0016EA36
		public static string rsInvalidIntegerConstant(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidIntegerConstant", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005858 RID: 22616 RVA: 0x00170846 File Offset: 0x0016EA46
		public static string rsInvalidNameNotCLSCompliant(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidNameNotCLSCompliant", objectType, objectName, propertyName);
		}

		// Token: 0x06005859 RID: 22617 RVA: 0x00170855 File Offset: 0x0016EA55
		public static string rsInvalidNameLength(string objectType, string objectName, string propertyName, string maxLength)
		{
			return RPResWrapper.Keys.GetString("rsInvalidNameLength", objectType, objectName, propertyName, maxLength);
		}

		// Token: 0x0600585A RID: 22618 RVA: 0x00170865 File Offset: 0x0016EA65
		public static string rsInvalidNumberOfFilterValues(string objectType, string objectName, string propertyName, string op, string count)
		{
			return RPResWrapper.Keys.GetString("rsInvalidNumberOfFilterValues", objectType, objectName, propertyName, op, count);
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x00170877 File Offset: 0x0016EA77
		public static string rsInvalidFilterValueDataType(string objectType, string objectName, string propertyName, string op, string dataType)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFilterValueDataType", objectType, objectName, propertyName, op, dataType);
		}

		// Token: 0x0600585C RID: 22620 RVA: 0x00170889 File Offset: 0x0016EA89
		public static string rsInvalidParameterNameNotCLSCompliant(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidParameterNameNotCLSCompliant", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600585D RID: 22621 RVA: 0x00170899 File Offset: 0x0016EA99
		public static string rsInvalidParameterNameLength(string objectType, string objectName, string propertyName, string maxLength)
		{
			return RPResWrapper.Keys.GetString("rsInvalidParameterNameLength", objectType, objectName, propertyName, maxLength);
		}

		// Token: 0x0600585E RID: 22622 RVA: 0x001708A9 File Offset: 0x0016EAA9
		public static string rsInvalidPreviousAggregateInMatrixCell(string objectType, string objectName, string propertyName, string matrixName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidPreviousAggregateInMatrixCell", objectType, objectName, propertyName, matrixName);
		}

		// Token: 0x0600585F RID: 22623 RVA: 0x001708B9 File Offset: 0x0016EAB9
		public static string rsInvalidRepeatWith(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRepeatWith", objectType, objectName, propertyName);
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x001708C8 File Offset: 0x0016EAC8
		public static string rsInvalidReportDefinition(string objectType, string objectName, string propertyName, string message)
		{
			return RPResWrapper.Keys.GetString("rsInvalidReportDefinition", objectType, objectName, propertyName, message);
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x001708D8 File Offset: 0x0016EAD8
		public static string rsInvalidReportParameterDependency(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidReportParameterDependency", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x001708E8 File Offset: 0x0016EAE8
		public static string rsInvalidValidValuesDataSetReference(string objectType, string objectName, string propertyName, string dataSet)
		{
			return RPResWrapper.Keys.GetString("rsInvalidValidValuesDataSetReference", objectType, objectName, propertyName, dataSet);
		}

		// Token: 0x06005863 RID: 22627 RVA: 0x001708F8 File Offset: 0x0016EAF8
		public static string rsInvalidDefaultValueDataSetReference(string objectType, string objectName, string propertyName, string dataSet)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDefaultValueDataSetReference", objectType, objectName, propertyName, dataSet);
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x00170908 File Offset: 0x0016EB08
		public static string rsInvalidRowGrouping(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRowGrouping", objectType, objectName, propertyName);
		}

		// Token: 0x06005865 RID: 22629 RVA: 0x00170917 File Offset: 0x0016EB17
		public static string rsInvalidRunningValueAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRunningValueAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x06005866 RID: 22630 RVA: 0x00170926 File Offset: 0x0016EB26
		public static string rsInvalidScopeInMatrix(string objectType, string objectName, string propertyName, string matrixName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidScopeInMatrix", objectType, objectName, propertyName, matrixName);
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x00170936 File Offset: 0x0016EB36
		public static string rsInvalidSeriesGrouping(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSeriesGrouping", objectType, objectName, propertyName);
		}

		// Token: 0x06005868 RID: 22632 RVA: 0x00170945 File Offset: 0x0016EB45
		public static string rsInvalidStaticDataGrouping(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidStaticDataGrouping", objectType, objectName, propertyName);
		}

		// Token: 0x06005869 RID: 22633 RVA: 0x00170954 File Offset: 0x0016EB54
		public static string rsInvalidReportName(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidReportName", objectType, objectName, propertyName);
		}

		// Token: 0x0600586A RID: 22634 RVA: 0x00170963 File Offset: 0x0016EB63
		public static string rsInvalidReportNameCharacters(string objectType, string objectName, string propertyName, string reservedCharacters)
		{
			return RPResWrapper.Keys.GetString("rsInvalidReportNameCharacters", objectType, objectName, propertyName, reservedCharacters);
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x00170973 File Offset: 0x0016EB73
		public static string rsInvalidReportUri(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidReportUri", objectType, objectName, propertyName);
		}

		// Token: 0x0600586C RID: 22636 RVA: 0x00170982 File Offset: 0x0016EB82
		public static string rsInvalidToggleItem(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidToggleItem", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600586D RID: 22637 RVA: 0x00170992 File Offset: 0x0016EB92
		public static string rsInvalidValidValues(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidValidValues", objectType, objectName, propertyName);
		}

		// Token: 0x0600586E RID: 22638 RVA: 0x001709A1 File Offset: 0x0016EBA1
		public static string rsInvalidMatrixSubtotalReportItem(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidMatrixSubtotalReportItem", objectType, objectName, propertyName);
		}

		// Token: 0x0600586F RID: 22639 RVA: 0x001709B0 File Offset: 0x0016EBB0
		public static string rsInvalidGroupingParent(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingParent", objectType, objectName, propertyName);
		}

		// Token: 0x06005870 RID: 22640 RVA: 0x001709BF File Offset: 0x0016EBBF
		public static string rsInvalidGroupingNaturalGroupFeature(string objectType, string objectName, string propertyName, string otherFeature)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingNaturalGroupFeature", objectType, objectName, propertyName, otherFeature);
		}

		// Token: 0x06005871 RID: 22641 RVA: 0x001709CF File Offset: 0x0016EBCF
		public static string rsInvalidGroupingContainerNotNaturalGroup(string objectType, string objectName, string propertyName, string innerGroupName, string outerGroupName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingContainerNotNaturalGroup", objectType, objectName, propertyName, innerGroupName, outerGroupName);
		}

		// Token: 0x06005872 RID: 22642 RVA: 0x001709E1 File Offset: 0x0016EBE1
		public static string rsConflictingNaturalGroupRequirements(string objectType, string objectName, string propertyName, string innerGroupName, string outerGroupName)
		{
			return RPResWrapper.Keys.GetString("rsConflictingNaturalGroupRequirements", objectType, objectName, propertyName, innerGroupName, outerGroupName);
		}

		// Token: 0x06005873 RID: 22643 RVA: 0x001709F3 File Offset: 0x0016EBF3
		public static string rsInvalidDefaultRelationshipNotNaturalJoin(string objectType, string objectName, string propertyName, string innerPropertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDefaultRelationshipNotNaturalJoin", objectType, objectName, propertyName, innerPropertyName);
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x00170A03 File Offset: 0x0016EC03
		public static string rsInvalidRelationshipGroupingContainerNotNaturalGroup(string objectType, string objectName, string propertyName, string innerGroupName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRelationshipGroupingContainerNotNaturalGroup", objectType, objectName, propertyName, innerGroupName);
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x00170A13 File Offset: 0x0016EC13
		public static string rsInvalidRelationshipContainerNotNaturalJoin(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRelationshipContainerNotNaturalJoin", objectType, objectName, propertyName);
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x00170A22 File Offset: 0x0016EC22
		public static string rsInvalidDefaultRelationshipDuplicateRelatedDataset(string objectType, string objectName, string propertyName, string innerPropertyName, string repeatedDataSet)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDefaultRelationshipDuplicateRelatedDataset", objectType, objectName, propertyName, innerPropertyName, repeatedDataSet);
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x00170A34 File Offset: 0x0016EC34
		public static string rsInvalidDefaultRelationshipCircularReference(string objectType, string objectName, string propertyName, string innerPropertyName, string repeatedDataSet)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDefaultRelationshipCircularReference", objectType, objectName, propertyName, innerPropertyName, repeatedDataSet);
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x00170A46 File Offset: 0x0016EC46
		public static string rsInvalidRelationshipDataSetUsedMoreThanOnce(string objectType, string objectName, string referencerOneObjectType, string referencerOneObjectName, string referencerTwoObjectType, string referencerTwoObjectName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRelationshipDataSetUsedMoreThanOnce", objectType, objectName, referencerOneObjectType, referencerOneObjectName, referencerTwoObjectType, referencerTwoObjectName);
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x00170A5A File Offset: 0x0016EC5A
		public static string rsInvalidRelationshipDataSet(string objectType, string objectName, string referencerOneObjectType, string referencerOneObjectName, string referencerTwoObjectType, string referencerTwoObjectName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRelationshipDataSet", objectType, objectName, referencerOneObjectType, referencerOneObjectName, referencerTwoObjectType, referencerTwoObjectName);
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x00170A6E File Offset: 0x0016EC6E
		public static string rsInvalidRelationshipTopLevelDataRegion(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRelationshipTopLevelDataRegion", objectType, objectName, propertyName);
		}

		// Token: 0x0600587B RID: 22651 RVA: 0x00170A7D File Offset: 0x0016EC7D
		public static string rsDefaultRelationshipIgnored(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDefaultRelationshipIgnored", objectType, objectName, propertyName);
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x00170A8C File Offset: 0x0016EC8C
		public static string rsInvalidDataSetScopedAggregate(string objectType, string objectName, string aggregateType)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDataSetScopedAggregate", objectType, objectName, aggregateType);
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x00170A9B File Offset: 0x0016EC9B
		public static string rsInvalidInnerDataSetName(string objectType, string objectName, string propertyName, string expectedValue, string actualValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidInnerDataSetName", objectType, objectName, propertyName, expectedValue, actualValue);
		}

		// Token: 0x0600587E RID: 22654 RVA: 0x00170AAD File Offset: 0x0016ECAD
		public static string rsInvalidCellDataSetName(string objectType, string objectName, string propertyName, string expectedValue, string actualValue, string dataRegionObjectType)
		{
			return RPResWrapper.Keys.GetString("rsInvalidCellDataSetName", objectType, objectName, propertyName, expectedValue, actualValue, dataRegionObjectType);
		}

		// Token: 0x0600587F RID: 22655 RVA: 0x00170AC1 File Offset: 0x0016ECC1
		public static string rsMissingIntersectionDataSetName(string objectType, string dataRegionName, string propertyName, string dataRegionObjectType, string groupName1, string groupName2)
		{
			return RPResWrapper.Keys.GetString("rsMissingIntersectionDataSetName", objectType, dataRegionName, propertyName, dataRegionObjectType, groupName1, groupName2);
		}

		// Token: 0x06005880 RID: 22656 RVA: 0x00170AD5 File Offset: 0x0016ECD5
		public static string rsMissingIntersectionRelationshipParentScope(string objectType, string dataRegionName, string propertyName, string dataRegionObjectType, string groupName1, string groupName2)
		{
			return RPResWrapper.Keys.GetString("rsMissingIntersectionRelationshipParentScope", objectType, dataRegionName, propertyName, dataRegionObjectType, groupName1, groupName2);
		}

		// Token: 0x06005881 RID: 22657 RVA: 0x00170AE9 File Offset: 0x0016ECE9
		public static string rsUnexpectedCellDataSetName(string objectType, string dataRegionName, string propertyName, string dataRegionObjectType)
		{
			return RPResWrapper.Keys.GetString("rsUnexpectedCellDataSetName", objectType, dataRegionName, propertyName, dataRegionObjectType);
		}

		// Token: 0x06005882 RID: 22658 RVA: 0x00170AF9 File Offset: 0x0016ECF9
		public static string rsInvalidRelationshipDuplicateParentScope(string objectType, string dataRegionName, string propertyName, string dataRegionObjectType, string groupName1, string groupName2)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRelationshipDuplicateParentScope", objectType, dataRegionName, propertyName, dataRegionObjectType, groupName1, groupName2);
		}

		// Token: 0x06005883 RID: 22659 RVA: 0x00170B0D File Offset: 0x0016ED0D
		public static string rsInvalidIntersectionNaturalJoin(string objectType, string dataRegionName, string propertyName, string dataRegionObjectType, string rowGroupName, string columnGroupName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidIntersectionNaturalJoin", objectType, dataRegionName, propertyName, dataRegionObjectType, rowGroupName, columnGroupName);
		}

		// Token: 0x06005884 RID: 22660 RVA: 0x00170B21 File Offset: 0x0016ED21
		public static string rsInvalidNaturalCrossJoin(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidNaturalCrossJoin", objectType, objectName, propertyName);
		}

		// Token: 0x06005885 RID: 22661 RVA: 0x00170B30 File Offset: 0x0016ED30
		public static string rsInvalidIntersectionNaturalCrossJoin(string objectType, string dataRegionName, string propertyName, string dataRegionObjectType)
		{
			return RPResWrapper.Keys.GetString("rsInvalidIntersectionNaturalCrossJoin", objectType, dataRegionName, propertyName, dataRegionObjectType);
		}

		// Token: 0x06005886 RID: 22662 RVA: 0x00170B40 File Offset: 0x0016ED40
		public static string rsInvalidAggregateIndicatorField(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidAggregateIndicatorField", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x06005887 RID: 22663 RVA: 0x00170B50 File Offset: 0x0016ED50
		public static string rsAggregateIndicatorFieldOnCalculatedField(string objectType, string objectName, string propertyName, string valuePropertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateIndicatorFieldOnCalculatedField", objectType, objectName, propertyName, valuePropertyName, dataSetName);
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x00170B62 File Offset: 0x0016ED62
		public static string rsMissingOrInvalidAggregateIndicatorFieldValue(string objectType, string objectName, string propertyName, string dataSetName, string referringFieldName)
		{
			return RPResWrapper.Keys.GetString("rsMissingOrInvalidAggregateIndicatorFieldValue", objectType, objectName, propertyName, dataSetName, referringFieldName);
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x00170B74 File Offset: 0x0016ED74
		public static string rsInvalidNaturalSortContainer(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidNaturalSortContainer", objectType, objectName, propertyName);
		}

		// Token: 0x0600588A RID: 22666 RVA: 0x00170B83 File Offset: 0x0016ED83
		public static string rsInvalidDeferredSortContainer(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDeferredSortContainer", objectType, objectName, propertyName);
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x00170B92 File Offset: 0x0016ED92
		public static string rsInvalidSortingContainerNotNaturalSort(string objectType, string objectName, string propertyName, string innerGroupName, string outerScopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSortingContainerNotNaturalSort", objectType, objectName, propertyName, innerGroupName, outerScopeName);
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x00170BA4 File Offset: 0x0016EDA4
		public static string rsConflictingNaturalSortRequirements(string objectType, string objectName, string propertyName, string innerGroupName, string outerGroupName)
		{
			return RPResWrapper.Keys.GetString("rsConflictingNaturalSortRequirements", objectType, objectName, propertyName, innerGroupName, outerGroupName);
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x00170BB6 File Offset: 0x0016EDB6
		public static string rsIncompatibleNaturalSortAndNaturalGroup(string objectType, string objectName, string propertyName, string groupName)
		{
			return RPResWrapper.Keys.GetString("rsIncompatibleNaturalSortAndNaturalGroup", objectType, objectName, propertyName, groupName);
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x00170BC6 File Offset: 0x0016EDC6
		public static string rsInvalidSortFlagCombination(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSortFlagCombination", objectType, objectName, propertyName);
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x00170BD5 File Offset: 0x0016EDD5
		public static string rsConflictingSortFlags(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsConflictingSortFlags", objectType, objectName, propertyName);
		}

		// Token: 0x06005890 RID: 22672 RVA: 0x00170BE4 File Offset: 0x0016EDE4
		public static string rsInvalidSortDirectionMustNotBeSpecified(string objectType, string objectName, string propertyName, string subPropertyName, string relatedObjectType, string relatedObjectName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSortDirectionMustNotBeSpecified", objectType, objectName, propertyName, subPropertyName, relatedObjectType, relatedObjectName);
		}

		// Token: 0x06005891 RID: 22673 RVA: 0x00170BF8 File Offset: 0x0016EDF8
		public static string rsCantMakeTableGroupHeadersFixed(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCantMakeTableGroupHeadersFixed", objectType, objectName, propertyName);
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x00170C07 File Offset: 0x0016EE07
		public static string rsFixedHeadersInInnerDataRegion(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsFixedHeadersInInnerDataRegion", objectType, objectName, propertyName);
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x00170C16 File Offset: 0x0016EE16
		public static string rsInvalidFixedTableColumnHeaderSpacing(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFixedTableColumnHeaderSpacing", objectType, objectName, propertyName);
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x00170C25 File Offset: 0x0016EE25
		public static string rsQueryCommandTextProcessingError(string datasetName)
		{
			return RPResWrapper.Keys.GetString("rsQueryCommandTextProcessingError", datasetName);
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x00170C32 File Offset: 0x0016EE32
		public static string rsDataSourceConnectStringProcessingError(string datasourceName)
		{
			return RPResWrapper.Keys.GetString("rsDataSourceConnectStringProcessingError", datasourceName);
		}

		// Token: 0x06005896 RID: 22678 RVA: 0x00170C3F File Offset: 0x0016EE3F
		public static string rsReportParameterProcessingError(string parameterName)
		{
			return RPResWrapper.Keys.GetString("rsReportParameterProcessingError", parameterName);
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x00170C4C File Offset: 0x0016EE4C
		public static string rsReportParameterQueryProcessingError(string parameterName, string propertyName, string fieldName, string datasetName, string error)
		{
			return RPResWrapper.Keys.GetString("rsReportParameterQueryProcessingError", parameterName, propertyName, fieldName, datasetName, error);
		}

		// Token: 0x06005898 RID: 22680 RVA: 0x00170C5E File Offset: 0x0016EE5E
		public static string rsInvalidMultiValueParameter(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidMultiValueParameter", objectType, objectName, propertyName);
		}

		// Token: 0x06005899 RID: 22681 RVA: 0x00170C6D File Offset: 0x0016EE6D
		public static string rsInvalidParameterDefaultValue(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidParameterDefaultValue", objectType, objectName, propertyName);
		}

		// Token: 0x0600589A RID: 22682 RVA: 0x00170C7C File Offset: 0x0016EE7C
		public static string rsParameterPropertyTypeMismatch(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsParameterPropertyTypeMismatch", objectType, objectName, propertyName);
		}

		// Token: 0x0600589B RID: 22683 RVA: 0x00170C8B File Offset: 0x0016EE8B
		public static string rsParameterValueDefinitionMismatch(string objectType, string objectName, string propertyName, string valuePropertyName)
		{
			return RPResWrapper.Keys.GetString("rsParameterValueDefinitionMismatch", objectType, objectName, propertyName, valuePropertyName);
		}

		// Token: 0x0600589C RID: 22684 RVA: 0x00170C9B File Offset: 0x0016EE9B
		public static string rsParameterValueNullOrBlank(string objectType, string objectName, string propertyName, string valuePropertyName)
		{
			return RPResWrapper.Keys.GetString("rsParameterValueNullOrBlank", objectType, objectName, propertyName, valuePropertyName);
		}

		// Token: 0x0600589D RID: 22685 RVA: 0x00170CAB File Offset: 0x0016EEAB
		public static string rsLabelExpressionOnChartScalarAxisIsIgnored(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsLabelExpressionOnChartScalarAxisIsIgnored", objectType, objectName, propertyName);
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x00170CBA File Offset: 0x0016EEBA
		public static string rsLineChartMightScatter(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsLineChartMightScatter", objectType, objectName, propertyName);
		}

		// Token: 0x0600589F RID: 22687 RVA: 0x00170CC9 File Offset: 0x0016EEC9
		public static string rsMissingAggregateScope(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMissingAggregateScope", objectType, objectName, propertyName);
		}

		// Token: 0x060058A0 RID: 22688 RVA: 0x00170CD8 File Offset: 0x0016EED8
		public static string rsMissingAggregateScopeInPageSection(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMissingAggregateScopeInPageSection", objectType, objectName, propertyName);
		}

		// Token: 0x060058A1 RID: 22689 RVA: 0x00170CE7 File Offset: 0x0016EEE7
		public static string rsReportItemInScopedAggregate(string objectType, string objectName, string propertyName, string reportItemName)
		{
			return RPResWrapper.Keys.GetString("rsReportItemInScopedAggregate", objectType, objectName, propertyName, reportItemName);
		}

		// Token: 0x060058A2 RID: 22690 RVA: 0x00170CF7 File Offset: 0x0016EEF7
		public static string rsMissingChartDataPoints(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMissingChartDataPoints", objectType, objectName, propertyName);
		}

		// Token: 0x060058A3 RID: 22691 RVA: 0x00170D06 File Offset: 0x0016EF06
		public static string rsMissingChartDataValueName(string objectType, string objectName, string propertyName, string index, string subPropertyName)
		{
			return RPResWrapper.Keys.GetString("rsMissingChartDataValueName", objectType, objectName, propertyName, index, subPropertyName);
		}

		// Token: 0x060058A4 RID: 22692 RVA: 0x00170D18 File Offset: 0x0016EF18
		public static string rsMissingCustomPropertyName(string objectType, string objectName, string propertyName, string index)
		{
			return RPResWrapper.Keys.GetString("rsMissingCustomPropertyName", objectType, objectName, propertyName, index);
		}

		// Token: 0x060058A5 RID: 22693 RVA: 0x00170D28 File Offset: 0x0016EF28
		public static string rsInvalidDataSetName(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDataSetName", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060058A6 RID: 22694 RVA: 0x00170D38 File Offset: 0x0016EF38
		public static string rsMissingDataSetName(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMissingDataSetName", objectType, objectName, propertyName);
		}

		// Token: 0x060058A7 RID: 22695 RVA: 0x00170D47 File Offset: 0x0016EF47
		public static string rsMissingMIMEType(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMissingMIMEType", objectType, objectName, propertyName);
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x00170D56 File Offset: 0x0016EF56
		public static string rsMissingParameterDefault(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMissingParameterDefault", objectType, objectName, propertyName);
		}

		// Token: 0x060058A9 RID: 22697 RVA: 0x00170D65 File Offset: 0x0016EF65
		public static string rsMultipleGroupExpressionsOnChartScalarAxis(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMultipleGroupExpressionsOnChartScalarAxis", objectType, objectName, propertyName);
		}

		// Token: 0x060058AA RID: 22698 RVA: 0x00170D74 File Offset: 0x0016EF74
		public static string rsMultipleGroupingsOnChartScalarAxis(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMultipleGroupingsOnChartScalarAxis", objectType, objectName, propertyName);
		}

		// Token: 0x060058AB RID: 22699 RVA: 0x00170D83 File Offset: 0x0016EF83
		public static string rsMultiReportItemsInMatrixSection(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMultiReportItemsInMatrixSection", objectType, objectName, propertyName);
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x00170D92 File Offset: 0x0016EF92
		public static string rsMultiReportItemsInTableCell(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMultiReportItemsInTableCell", objectType, objectName, propertyName);
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x00170DA1 File Offset: 0x0016EFA1
		public static string rsMultiReportItemsInPageSectionExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMultiReportItemsInPageSectionExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058AE RID: 22702 RVA: 0x00170DB0 File Offset: 0x0016EFB0
		public static string rsMultiReportItemsInCustomReportItem(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMultiReportItemsInCustomReportItem", objectType, objectName, propertyName);
		}

		// Token: 0x060058AF RID: 22703 RVA: 0x00170DBF File Offset: 0x0016EFBF
		public static string rsMultiStaticColumnsOrRows(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMultiStaticColumnsOrRows", objectType, objectName, propertyName);
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x00170DCE File Offset: 0x0016EFCE
		public static string rsMultiStaticCategoriesOrSeries(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMultiStaticCategoriesOrSeries", objectType, objectName, propertyName);
		}

		// Token: 0x060058B1 RID: 22705 RVA: 0x00170DDD File Offset: 0x0016EFDD
		public static string rsNegativeLeftWidth(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNegativeLeftWidth", objectType, objectName, propertyName);
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x00170DEC File Offset: 0x0016EFEC
		public static string rsNegativeTopHeight(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNegativeTopHeight", objectType, objectName, propertyName);
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x00170DFB File Offset: 0x0016EFFB
		public static string rsNonAggregateInMatrixCell(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNonAggregateInMatrixCell", objectType, objectName, propertyName);
		}

		// Token: 0x060058B4 RID: 22708 RVA: 0x00170E0A File Offset: 0x0016F00A
		public static string rsNotAReportDefinition(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNotAReportDefinition", objectType, objectName, propertyName);
		}

		// Token: 0x060058B5 RID: 22709 RVA: 0x00170E19 File Offset: 0x0016F019
		public static string rsNotACurrentReportDefinition(string objectType, string objectName, string propertyName, string namespaceValue)
		{
			return RPResWrapper.Keys.GetString("rsNotACurrentReportDefinition", objectType, objectName, propertyName, namespaceValue);
		}

		// Token: 0x060058B6 RID: 22710 RVA: 0x00170E29 File Offset: 0x0016F029
		public static string rsOverlappingReportItems(string objectType, string objectName, string propertyName, string objectType2, string objectName2)
		{
			return RPResWrapper.Keys.GetString("rsOverlappingReportItems", objectType, objectName, propertyName, objectType2, objectName2);
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x00170E3B File Offset: 0x0016F03B
		public static string rsReportItemOutsideContainer(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsReportItemOutsideContainer", objectType, objectName, propertyName);
		}

		// Token: 0x060058B8 RID: 22712 RVA: 0x00170E4A File Offset: 0x0016F04A
		public static string rsParameterInReportParameterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsParameterInReportParameterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058B9 RID: 22713 RVA: 0x00170E59 File Offset: 0x0016F059
		public static string rsPageBreakOnMatrixColumnGroup(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsPageBreakOnMatrixColumnGroup", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060058BA RID: 22714 RVA: 0x00170E69 File Offset: 0x0016F069
		public static string rsPageBreakOnChartGroup(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsPageBreakOnChartGroup", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060058BB RID: 22715 RVA: 0x00170E79 File Offset: 0x0016F079
		public static string rsPageBreakOnGaugeGroup(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsPageBreakOnGaugeGroup", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x00170E89 File Offset: 0x0016F089
		public static string rsPageBreakOnMapGroup(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsPageBreakOnMapGroup", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x00170E99 File Offset: 0x0016F099
		public static string rsPreviousAggregateInFilterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPreviousAggregateInFilterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x00170EA8 File Offset: 0x0016F0A8
		public static string rsPreviousAggregateInGroupExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPreviousAggregateInGroupExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058BF RID: 22719 RVA: 0x00170EB7 File Offset: 0x0016F0B7
		public static string rsPreviousAggregateInPageSectionExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPreviousAggregateInPageSectionExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058C0 RID: 22720 RVA: 0x00170EC6 File Offset: 0x0016F0C6
		public static string rsPreviousAggregateInQueryParameterExpression(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsPreviousAggregateInQueryParameterExpression", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x060058C1 RID: 22721 RVA: 0x00170ED6 File Offset: 0x0016F0D6
		public static string rsPreviousAggregateInReportParameterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPreviousAggregateInReportParameterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058C2 RID: 22722 RVA: 0x00170EE5 File Offset: 0x0016F0E5
		public static string rsPreviousAggregateInSortExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPreviousAggregateInSortExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058C3 RID: 22723 RVA: 0x00170EF4 File Offset: 0x0016F0F4
		public static string rsPreviousAggregateInReportLanguageExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPreviousAggregateInReportLanguageExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058C4 RID: 22724 RVA: 0x00170F03 File Offset: 0x0016F103
		public static string rsPreviousAggregateInVariableExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPreviousAggregateInVariableExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x00170F12 File Offset: 0x0016F112
		public static string rsPreviousAggregateInJoinExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPreviousAggregateInJoinExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x00170F21 File Offset: 0x0016F121
		public static string rsRepeatWithNotPeerDataRegion(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsRepeatWithNotPeerDataRegion", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x00170F31 File Offset: 0x0016F131
		public static string rsReportItemInFilterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsReportItemInFilterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058C8 RID: 22728 RVA: 0x00170F40 File Offset: 0x0016F140
		public static string rsReportItemInGroupExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsReportItemInGroupExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058C9 RID: 22729 RVA: 0x00170F4F File Offset: 0x0016F14F
		public static string rsReportItemInQueryParameterExpression(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsReportItemInQueryParameterExpression", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x060058CA RID: 22730 RVA: 0x00170F5F File Offset: 0x0016F15F
		public static string rsReportItemInReportParameterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsReportItemInReportParameterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x00170F6E File Offset: 0x0016F16E
		public static string rsReportItemInSortExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsReportItemInSortExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x00170F7D File Offset: 0x0016F17D
		public static string rsReportItemInReportLanguageExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsReportItemInReportLanguageExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x00170F8C File Offset: 0x0016F18C
		public static string rsReportItemInVariableExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsReportItemInVariableExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x00170F9B File Offset: 0x0016F19B
		public static string rsReportItemInJoinExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsReportItemInJoinExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x00170FAA File Offset: 0x0016F1AA
		public static string rsRowNumberInFilterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRowNumberInFilterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058D0 RID: 22736 RVA: 0x00170FB9 File Offset: 0x0016F1B9
		public static string rsRowNumberInPageSectionExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRowNumberInPageSectionExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x00170FC8 File Offset: 0x0016F1C8
		public static string rsRowNumberInQueryParameterExpression(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsRowNumberInQueryParameterExpression", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x00170FD8 File Offset: 0x0016F1D8
		public static string rsRowNumberInReportParameterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRowNumberInReportParameterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x00170FE7 File Offset: 0x0016F1E7
		public static string rsRowNumberInSortExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRowNumberInSortExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x00170FF6 File Offset: 0x0016F1F6
		public static string rsRowNumberInReportLanguageExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRowNumberInReportLanguageExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x00171005 File Offset: 0x0016F205
		public static string rsRowNumberInVariableExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRowNumberInVariableExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x00171014 File Offset: 0x0016F214
		public static string rsRunningValueInFilterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRunningValueInFilterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x00171023 File Offset: 0x0016F223
		public static string rsRunningValueInGroupExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRunningValueInGroupExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x00171032 File Offset: 0x0016F232
		public static string rsRunningValueInPageSectionExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRunningValueInPageSectionExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x00171041 File Offset: 0x0016F241
		public static string rsRunningValueInQueryParameterExpression(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsRunningValueInQueryParameterExpression", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x00171051 File Offset: 0x0016F251
		public static string rsRunningValueInReportParameterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRunningValueInReportParameterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x00171060 File Offset: 0x0016F260
		public static string rsRunningValueInSortExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRunningValueInSortExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x0017106F File Offset: 0x0016F26F
		public static string rsRunningValueInReportLanguageExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRunningValueInReportLanguageExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058DD RID: 22749 RVA: 0x0017107E File Offset: 0x0016F27E
		public static string rsRunningValueInVariableExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRunningValueInVariableExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058DE RID: 22750 RVA: 0x0017108D File Offset: 0x0016F28D
		public static string rsRunningValueInJoinExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRunningValueInJoinExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058DF RID: 22751 RVA: 0x0017109C File Offset: 0x0016F29C
		public static string rsScopeInPageSectionExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsScopeInPageSectionExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058E0 RID: 22752 RVA: 0x001710AB File Offset: 0x0016F2AB
		public static string rsStaticGroupingOnChartScalarAxis(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsStaticGroupingOnChartScalarAxis", objectType, objectName, propertyName);
		}

		// Token: 0x060058E1 RID: 22753 RVA: 0x001710BA File Offset: 0x0016F2BA
		public static string rsToggleInPageSection(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsToggleInPageSection", objectType, objectName, propertyName);
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x001710C9 File Offset: 0x0016F2C9
		public static string rsUnsortedCategoryInAreaChart(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsUnsortedCategoryInAreaChart", objectType, objectName, propertyName);
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x001710D8 File Offset: 0x0016F2D8
		public static string rsWrongNumberOfMatrixCells(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfMatrixCells", objectType, objectName, propertyName);
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x001710E7 File Offset: 0x0016F2E7
		public static string rsWrongNumberOfMatrixColumns(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfMatrixColumns", objectType, objectName, propertyName);
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x001710F6 File Offset: 0x0016F2F6
		public static string rsWrongNumberOfMatrixRows(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfMatrixRows", objectType, objectName, propertyName);
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x00171105 File Offset: 0x0016F305
		public static string rsWrongNumberOfChartDataPoints(string objectType, string objectName, string propertyName, string present, string expected)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfChartDataPoints", objectType, objectName, propertyName, present, expected);
		}

		// Token: 0x060058E7 RID: 22759 RVA: 0x00171117 File Offset: 0x0016F317
		public static string rsWrongNumberOfChartSeries(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfChartSeries", objectType, objectName, propertyName);
		}

		// Token: 0x060058E8 RID: 22760 RVA: 0x00171126 File Offset: 0x0016F326
		public static string rsWrongNumberOfChartDataPointsInSeries(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfChartDataPointsInSeries", objectType, objectName, propertyName);
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x00171135 File Offset: 0x0016F335
		public static string rsWrongNumberOfDataValues(string objectType, string objectName, string propertyName, string present, string expected)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfDataValues", objectType, objectName, propertyName, present, expected);
		}

		// Token: 0x060058EA RID: 22762 RVA: 0x00171147 File Offset: 0x0016F347
		public static string rsWrongNumberOfParameters(string objectType, string objectName, string propertyName, string functionName)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfParameters", objectType, objectName, propertyName, functionName);
		}

		// Token: 0x060058EB RID: 22763 RVA: 0x00171157 File Offset: 0x0016F357
		public static string rsWrongNumberOfTableCells(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfTableCells", objectType, objectName, propertyName);
		}

		// Token: 0x060058EC RID: 22764 RVA: 0x00171166 File Offset: 0x0016F366
		public static string rsSingleHierarchyWithDataRows(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsSingleHierarchyWithDataRows", objectType, objectName, propertyName);
		}

		// Token: 0x060058ED RID: 22765 RVA: 0x00171175 File Offset: 0x0016F375
		public static string rsInvalidRecursiveAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRecursiveAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x00171184 File Offset: 0x0016F384
		public static string rsInvalidNestedRecursiveAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidNestedRecursiveAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x00171193 File Offset: 0x0016F393
		public static string rsRecursiveAggregateOfAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRecursiveAggregateOfAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x001711A2 File Offset: 0x0016F3A2
		public static string rsInvalidAggregateRecursiveFlag(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidAggregateRecursiveFlag", objectType, objectName, propertyName);
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x001711B1 File Offset: 0x0016F3B1
		public static string rsPostSortAggregateInGroupFilterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPostSortAggregateInGroupFilterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058F2 RID: 22770 RVA: 0x001711C0 File Offset: 0x0016F3C0
		public static string rsPostSortAggregateInSortExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPostSortAggregateInSortExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x001711CF File Offset: 0x0016F3CF
		public static string rsPostSortAggregateInVariableExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPostSortAggregateInVariableExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058F4 RID: 22772 RVA: 0x001711DE File Offset: 0x0016F3DE
		public static string rsBookmarkInPageSection(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsBookmarkInPageSection", objectType, objectName, propertyName);
		}

		// Token: 0x060058F5 RID: 22773 RVA: 0x001711ED File Offset: 0x0016F3ED
		public static string rsUnsupportedProtocol(string objectType, string objectName, string propertyName, string propertyValue, string protocolList)
		{
			return RPResWrapper.Keys.GetString("rsUnsupportedProtocol", objectType, objectName, propertyName, propertyValue, protocolList);
		}

		// Token: 0x060058F6 RID: 22774 RVA: 0x001711FF File Offset: 0x0016F3FF
		public static string rsInvalidVariableCount(string objectType, string objectName, string propertyName, string subPropertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidVariableCount", objectType, objectName, propertyName, subPropertyName);
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x0017120F File Offset: 0x0016F40F
		public static string rsElementMustContainChildren(string objectType, string objectName, string propertyName, string childElementName)
		{
			return RPResWrapper.Keys.GetString("rsElementMustContainChildren", objectType, objectName, propertyName, childElementName);
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x0017121F File Offset: 0x0016F41F
		public static string rsElementMustContainChild(string objectType, string objectName, string propertyName, string childElementName)
		{
			return RPResWrapper.Keys.GetString("rsElementMustContainChild", objectType, objectName, propertyName, childElementName);
		}

		// Token: 0x060058F9 RID: 22777 RVA: 0x0017122F File Offset: 0x0016F42F
		public static string rsInvalidWritableVariable(string objectType, string objectName, string name, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidWritableVariable", objectType, objectName, name, propertyName);
		}

		// Token: 0x060058FA RID: 22778 RVA: 0x0017123F File Offset: 0x0016F43F
		public static string rsMissingExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMissingExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x0017124E File Offset: 0x0016F44E
		public static string rsInvalidActionsCount(string objectType, string objectName, string propertyName, string SubPropertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidActionsCount", objectType, objectName, propertyName, SubPropertyName);
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x0017125E File Offset: 0x0016F45E
		public static string rsInvalidMeDotValueInExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidMeDotValueInExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x0017126D File Offset: 0x0016F46D
		public static string rsPostSortAggregateInAggregateExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPostSortAggregateInAggregateExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058FE RID: 22782 RVA: 0x0017127C File Offset: 0x0016F47C
		public static string rsRunningValueInAggregateExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRunningValueInAggregateExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x0017128B File Offset: 0x0016F48B
		public static string rsPreviousInAggregateExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPreviousInAggregateExpression", objectType, objectName, propertyName);
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x0017129A File Offset: 0x0016F49A
		public static string rsNestedAggregateViaLookup(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNestedAggregateViaLookup", objectType, objectName, propertyName);
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x001712A9 File Offset: 0x0016F4A9
		public static string rsNestedAggregateInPageSection(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNestedAggregateInPageSection", objectType, objectName, propertyName);
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x001712B8 File Offset: 0x0016F4B8
		public static string rsNestedAggregateInFilterExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNestedAggregateInFilterExpression", objectType, objectName, propertyName);
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x001712C7 File Offset: 0x0016F4C7
		public static string rsNestedAggregateInGroupVariable(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNestedAggregateInGroupVariable", objectType, objectName, propertyName);
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x001712D6 File Offset: 0x0016F4D6
		public static string rsWrongNumberOfTablixCornerRows(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfTablixCornerRows", objectType, objectName, propertyName);
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x001712E5 File Offset: 0x0016F4E5
		public static string rsWrongNumberOfTablixCornerCells(string objectType, string objectName, string propertyName, string columnIndex)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfTablixCornerCells", objectType, objectName, propertyName, columnIndex);
		}

		// Token: 0x06005906 RID: 22790 RVA: 0x001712F5 File Offset: 0x0016F4F5
		public static string rsWrongNumberOfTablixColumns(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfTablixColumns", objectType, objectName, propertyName);
		}

		// Token: 0x06005907 RID: 22791 RVA: 0x00171304 File Offset: 0x0016F504
		public static string rsWrongNumberOfTablixCells(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfTablixCells", objectType, objectName, propertyName);
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x00171313 File Offset: 0x0016F513
		public static string rsWrongNumberOfTablixRows(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfTablixRows", objectType, objectName, propertyName);
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x00171322 File Offset: 0x0016F522
		public static string rsInvalidTablixCornerCellSpan(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTablixCornerCellSpan", objectType, objectName, propertyName);
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x00171331 File Offset: 0x0016F531
		public static string rsInvalidTablixCellCellSpan(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTablixCellCellSpan", objectType, objectName, propertyName);
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x00171340 File Offset: 0x0016F540
		public static string rsInvalidTablixCornerColumnSpans(string objectType, string objectName, string propertyName, string columnIndex)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTablixCornerColumnSpans", objectType, objectName, propertyName, columnIndex);
		}

		// Token: 0x0600590C RID: 22796 RVA: 0x00171350 File Offset: 0x0016F550
		public static string rsInvalidTablixCornerRowSpans(string objectType, string objectName, string propertyName, string columnIndex)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTablixCornerRowSpans", objectType, objectName, propertyName, columnIndex);
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x00171360 File Offset: 0x0016F560
		public static string rsHiddenTablixCornerCellContents(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsHiddenTablixCornerCellContents", objectType, objectName, propertyName);
		}

		// Token: 0x0600590E RID: 22798 RVA: 0x0017136F File Offset: 0x0016F56F
		public static string rsInvalidSortNotAllowed(string objectType, string objectName, string propertyName, string otherProperty, string parentName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSortNotAllowed", objectType, objectName, propertyName, otherProperty, parentName);
		}

		// Token: 0x0600590F RID: 22799 RVA: 0x00171381 File Offset: 0x0016F581
		public static string rsInvalidFixedHeaderOnOppositeHierarchy(string objectType, string objectName, string propertyName, string otherPropertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFixedHeaderOnOppositeHierarchy", objectType, objectName, propertyName, otherPropertyName);
		}

		// Token: 0x06005910 RID: 22800 RVA: 0x00171391 File Offset: 0x0016F591
		public static string rsInvalidFixedDataColumnPosition(string objectType, string objectName, string propertyName, string otherPropertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFixedDataColumnPosition", objectType, objectName, propertyName, otherPropertyName);
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x001713A1 File Offset: 0x0016F5A1
		public static string rsInvalidFixedDataRowPosition(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFixedDataRowPosition", objectType, objectName, propertyName);
		}

		// Token: 0x06005912 RID: 22802 RVA: 0x001713B0 File Offset: 0x0016F5B0
		public static string rsInvalidFixedDataNotContiguous(string objectType, string objectName, string propertyName, string rowOrColumnHierarchy)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFixedDataNotContiguous", objectType, objectName, propertyName, rowOrColumnHierarchy);
		}

		// Token: 0x06005913 RID: 22803 RVA: 0x001713C0 File Offset: 0x0016F5C0
		public static string rsInvalidFixedDataInHierarchy(string objectType, string objectName, string propertyName, string rowOrColumnHierarchy)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFixedDataInHierarchy", objectType, objectName, propertyName, rowOrColumnHierarchy);
		}

		// Token: 0x06005914 RID: 22804 RVA: 0x001713D0 File Offset: 0x0016F5D0
		public static string rsInvalidFixedDataBodyCellSpans(string objectType, string objectName, string rowIndex)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFixedDataBodyCellSpans", objectType, objectName, rowIndex);
		}

		// Token: 0x06005915 RID: 22805 RVA: 0x001713DF File Offset: 0x0016F5DF
		public static string rsInvalidGroupAncestorIsDetail(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupAncestorIsDetail", objectType, objectName, propertyName);
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x001713EE File Offset: 0x0016F5EE
		public static string rsInvalidKeepWithGroupOnDynamicTablixMember(string objectType, string objectName, string propertyName, string subPropertyName, string neverValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidKeepWithGroupOnDynamicTablixMember", objectType, objectName, propertyName, subPropertyName, neverValue);
		}

		// Token: 0x06005917 RID: 22807 RVA: 0x00171400 File Offset: 0x0016F600
		public static string rsInvalidKeepWithGroup(string objectType, string objectName, string propertyName, string subPropertyName, string expectedValue, string actualValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidKeepWithGroup", objectType, objectName, propertyName, subPropertyName, expectedValue, actualValue);
		}

		// Token: 0x06005918 RID: 22808 RVA: 0x00171414 File Offset: 0x0016F614
		public static string rsInvalidKeepWithGroupOnColumnTablixMember(string objectType, string objectName, string propertyName, string subPropertyName, string neverValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidKeepWithGroupOnColumnTablixMember", objectType, objectName, propertyName, subPropertyName, neverValue);
		}

		// Token: 0x06005919 RID: 22809 RVA: 0x00171426 File Offset: 0x0016F626
		public static string rsInvalidRepeatOnNewPageOnColumnTablixMember(string objectType, string objectName, string propertyName, string subPropertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRepeatOnNewPageOnColumnTablixMember", objectType, objectName, propertyName, subPropertyName);
		}

		// Token: 0x0600591A RID: 22810 RVA: 0x00171436 File Offset: 0x0016F636
		public static string rsInvalidRepeatOnNewPage(string objectType, string objectName, string propertyName, string subPropertyName, string expectedValue, string actualValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRepeatOnNewPage", objectType, objectName, propertyName, subPropertyName, expectedValue, actualValue);
		}

		// Token: 0x0600591B RID: 22811 RVA: 0x0017144A File Offset: 0x0016F64A
		public static string rsInvalidTablixCellColSpans(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTablixCellColSpans", objectType, objectName, propertyName);
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x00171459 File Offset: 0x0016F659
		public static string rsInvalidTablixCellColSpan(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTablixCellColSpan", objectType, objectName, propertyName);
		}

		// Token: 0x0600591D RID: 22813 RVA: 0x00171468 File Offset: 0x0016F668
		public static string rsInvalidTablixCellRowSpan(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTablixCellRowSpan", objectType, objectName, propertyName);
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x00171477 File Offset: 0x0016F677
		public static string rsInvalidTablixHeaderColSpan(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTablixHeaderColSpan", objectType, objectName, propertyName);
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x00171486 File Offset: 0x0016F686
		public static string rsInvalidTablixHeaderRowSpan(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTablixHeaderRowSpan", objectType, objectName, propertyName);
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x00171495 File Offset: 0x0016F695
		public static string rsCellContentsNotOmitted(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCellContentsNotOmitted", objectType, objectName, propertyName);
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x001714A4 File Offset: 0x0016F6A4
		public static string rsCellContentsRequired(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCellContentsRequired", objectType, objectName, propertyName);
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x001714B3 File Offset: 0x0016F6B3
		public static string rsInconsistentNumberofCellsInRow(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInconsistentNumberofCellsInRow", objectType, objectName, propertyName);
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x001714C4 File Offset: 0x0016F6C4
		public static string rsInvalidTablixHeaderSize(string objectType, string objectName, string propertyName, string subPropertyName, string number, string expectedSize, string actualSize, string rowOrColumn)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTablixHeaderSize", objectType, objectName, propertyName, subPropertyName, number, expectedSize, actualSize, rowOrColumn);
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x001714E7 File Offset: 0x0016F6E7
		public static string rsInvalidPreviousAggregateInTablixCell(string objectType, string objectName, string propertyName, string tablixName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidPreviousAggregateInTablixCell", objectType, objectName, propertyName, tablixName);
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x001714F7 File Offset: 0x0016F6F7
		public static string rsInvalidScopeInTablix(string objectType, string objectName, string propertyName, string tablixName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidScopeInTablix", objectType, objectName, propertyName, tablixName);
		}

		// Token: 0x06005926 RID: 22822 RVA: 0x00171507 File Offset: 0x0016F707
		public static string rsInvalidTablixHeaders(string objectType, string objectName, string propertyName, string subPropertyName, string subSubPropertyName, string level)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTablixHeaders", objectType, objectName, propertyName, subPropertyName, subSubPropertyName, level);
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x0017151B File Offset: 0x0016F71B
		public static string rsNonAggregateInTablixCell(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNonAggregateInTablixCell", objectType, objectName, propertyName);
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x0017152A File Offset: 0x0016F72A
		public static string rsInvalidChartAxisNameNotCLSCompliant(string objectType, string objectName, string propertyName, string name)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChartAxisNameNotCLSCompliant", objectType, objectName, propertyName, name);
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x0017153A File Offset: 0x0016F73A
		public static string rsInvalidChartAxisNameLength(string objectType, string objectName, string propertyName, string name, string maxLength)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChartAxisNameLength", objectType, objectName, propertyName, name, maxLength);
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x0017154C File Offset: 0x0016F74C
		public static string rsSpecifiedNonValueAxisName(string objectType, string objectName, string propertyName, string name)
		{
			return RPResWrapper.Keys.GetString("rsSpecifiedNonValueAxisName", objectType, objectName, propertyName, name);
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x0017155C File Offset: 0x0016F75C
		public static string rsValueAxisNameNotFound(string objectType, string objectName, string propertyName, string name)
		{
			return RPResWrapper.Keys.GetString("rsValueAxisNameNotFound", objectType, objectName, propertyName, name);
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x0017156C File Offset: 0x0016F76C
		public static string rsDuplicateVariableName(string objectType, string objectName, string propertyName, string name)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateVariableName", objectType, objectName, propertyName, name);
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x0017157C File Offset: 0x0016F77C
		public static string rsInvalidVariableNameNotCLSCompliant(string objectType, string objectName, string propertyName, string variableName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidVariableNameNotCLSCompliant", objectType, objectName, propertyName, variableName);
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x0017158C File Offset: 0x0016F78C
		public static string rsInvalidVariableNameLength(string objectType, string objectName, string propertyName, string variableName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidVariableNameLength", objectType, objectName, propertyName, variableName);
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x0017159C File Offset: 0x0016F79C
		public static string rsDuplicateGroupingVariableName(string objectType, string objectName, string propertyName, string name, string groupingName)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateGroupingVariableName", objectType, objectName, propertyName, name, groupingName);
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x001715AE File Offset: 0x0016F7AE
		public static string rsInvalidGroupingVariableNameNotCLSCompliant(string objectType, string objectName, string propertyName, string variableName, string groupingName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingVariableNameNotCLSCompliant", objectType, objectName, propertyName, variableName, groupingName);
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x001715C0 File Offset: 0x0016F7C0
		public static string rsInvalidGroupingVariableNameLength(string objectType, string objectName, string propertyName, string variableName, string groupingName, string maxLength)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingVariableNameLength", objectType, objectName, propertyName, variableName, groupingName, maxLength);
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x001715D4 File Offset: 0x0016F7D4
		public static string rsInvalidVariableReference(string objectType, string objectName, string propertyName, string variablePropertyName, string variableName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidVariableReference", objectType, objectName, propertyName, variablePropertyName, variableName);
		}

		// Token: 0x06005933 RID: 22835 RVA: 0x001715E6 File Offset: 0x0016F7E6
		public static string rsInvalidReportItemInPageSection(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidReportItemInPageSection", objectType, objectName, propertyName);
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x001715F5 File Offset: 0x0016F7F5
		public static string rsInvalidTargetScope(string objectType, string objectName, string propertyName, string scopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTargetScope", objectType, objectName, propertyName, scopeName);
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x00171605 File Offset: 0x0016F805
		public static string rsInvalidOmittedTargetScope(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidOmittedTargetScope", objectType, objectName, propertyName);
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x00171614 File Offset: 0x0016F814
		public static string rsInvalidOmittedExpressionScope(string objectType, string objectName, string propertyName, string elementName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidOmittedExpressionScope", objectType, objectName, propertyName, elementName);
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x00171624 File Offset: 0x0016F824
		public static string rsInvalidExpressionScope(string objectType, string objectName, string propertyName, string scopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidExpressionScope", objectType, objectName, propertyName, scopeName);
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x00171634 File Offset: 0x0016F834
		public static string rsInvalidTextboxInPageSection(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTextboxInPageSection", objectType, objectName, propertyName);
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x00171643 File Offset: 0x0016F843
		public static string rsNonExistingScope(string objectType, string objectName, string propertyName, string scopeName)
		{
			return RPResWrapper.Keys.GetString("rsNonExistingScope", objectType, objectName, propertyName, scopeName);
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x00171653 File Offset: 0x0016F853
		public static string rsInvalidExpressionScopeDataSet(string objectType, string objectName, string propertyName, string scopeName, string targetProperty)
		{
			return RPResWrapper.Keys.GetString("rsInvalidExpressionScopeDataSet", objectType, objectName, propertyName, scopeName, targetProperty);
		}

		// Token: 0x0600593B RID: 22843 RVA: 0x00171665 File Offset: 0x0016F865
		public static string rsInvalidSortExpressionScope(string objectType, string objectName, string propertyName, string scopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSortExpressionScope", objectType, objectName, propertyName, scopeName);
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x00171675 File Offset: 0x0016F875
		public static string rsIneffectiveSortExpressionScope(string objectType, string objectName, string propertyName, string scopeName)
		{
			return RPResWrapper.Keys.GetString("rsIneffectiveSortExpressionScope", objectType, objectName, propertyName, scopeName);
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x00171685 File Offset: 0x0016F885
		public static string rsMissingDataGrouping(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMissingDataGrouping", objectType, objectName, propertyName);
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x00171694 File Offset: 0x0016F894
		public static string rsWrongNumberOfDataRows(string objectType, string objectName, string expectedNumber)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfDataRows", objectType, objectName, expectedNumber);
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x001716A3 File Offset: 0x0016F8A3
		public static string rsWrongNumberOfDataCellsInDataRow(string objectType, string objectName, string expectedNumber)
		{
			return RPResWrapper.Keys.GetString("rsWrongNumberOfDataCellsInDataRow", objectType, objectName, expectedNumber);
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x001716B2 File Offset: 0x0016F8B2
		public static string rsMissingDataGroupings(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMissingDataGroupings", objectType, objectName, propertyName);
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x001716C1 File Offset: 0x0016F8C1
		public static string rsMissingDataCells(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMissingDataCells", objectType, objectName, propertyName);
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x001716D0 File Offset: 0x0016F8D0
		public static string rsCRIMultiStaticColumnsOrRows(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCRIMultiStaticColumnsOrRows", objectType, objectName, propertyName);
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x001716DF File Offset: 0x0016F8DF
		public static string rsCRIStaticWithSubgroups(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCRIStaticWithSubgroups", objectType, objectName, propertyName);
		}

		// Token: 0x06005944 RID: 22852 RVA: 0x001716EE File Offset: 0x0016F8EE
		public static string rsCRIMultiNonStaticGroups(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCRIMultiNonStaticGroups", objectType, objectName, propertyName);
		}

		// Token: 0x06005945 RID: 22853 RVA: 0x001716FD File Offset: 0x0016F8FD
		public static string rsCRISubtotalNotSupported(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCRISubtotalNotSupported", objectType, objectName, propertyName);
		}

		// Token: 0x06005946 RID: 22854 RVA: 0x0017170C File Offset: 0x0016F90C
		public static string rsInvalidGrouping(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGrouping", objectType, objectName, propertyName);
		}

		// Token: 0x06005947 RID: 22855 RVA: 0x0017171B File Offset: 0x0016F91B
		public static string rsCRIInPageSection(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCRIInPageSection", objectType, objectName, propertyName);
		}

		// Token: 0x06005948 RID: 22856 RVA: 0x0017172A File Offset: 0x0016F92A
		public static string rsMapPropertyAlreadyDefined(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMapPropertyAlreadyDefined", objectType, objectName, propertyName);
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x00171739 File Offset: 0x0016F939
		public static string rsInvalidRowMapMemberCannotBeDynamic(string objectType, string objectName, string propertyName, string subPropertyName, string groupName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRowMapMemberCannotBeDynamic", objectType, objectName, propertyName, subPropertyName, groupName);
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x0017174B File Offset: 0x0016F94B
		public static string rsInvalidRowMapMemberCannotContainChildMember(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRowMapMemberCannotContainChildMember", objectType, objectName, propertyName);
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x0017175A File Offset: 0x0016F95A
		public static string rsInvalidColumnMapMemberCannotContainMultipleChildMember(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidColumnMapMemberCannotContainMultipleChildMember", objectType, objectName, propertyName);
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x00171769 File Offset: 0x0016F969
		public static string rsCRIRenderItemNull(string objectType, string objectName, string customControlType)
		{
			return RPResWrapper.Keys.GetString("rsCRIRenderItemNull", objectType, objectName, customControlType);
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x00171778 File Offset: 0x0016F978
		public static string rsCRIRenderInstanceNull(string objectType, string objectName, string customControlType)
		{
			return RPResWrapper.Keys.GetString("rsCRIRenderInstanceNull", objectType, objectName, customControlType);
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x00171787 File Offset: 0x0016F987
		public static string rsCRIRenderItemInstanceType(string objectType, string objectName, string customControlType, string customReportItemName, string expectedObjectType)
		{
			return RPResWrapper.Keys.GetString("rsCRIRenderItemInstanceType", objectType, objectName, customControlType, customReportItemName, expectedObjectType);
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x00171799 File Offset: 0x0016F999
		public static string rsCRIRenderItemDefinitionName(string objectType, string objectName, string customControlType, string customReportItemName, string expectedDefinitionName)
		{
			return RPResWrapper.Keys.GetString("rsCRIRenderItemDefinitionName", objectType, objectName, customControlType, customReportItemName, expectedDefinitionName);
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x001717AB File Offset: 0x0016F9AB
		public static string rsCRIRenderItemProperties(string objectType, string objectName, string customControlType, string customReportItemName, string propertyName, string expectedCount, string actualCount)
		{
			return RPResWrapper.Keys.GetString("rsCRIRenderItemProperties", objectType, objectName, customControlType, customReportItemName, propertyName, expectedCount, actualCount);
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x001717C1 File Offset: 0x0016F9C1
		public static string rsCRIRenderItemDuplicateStyle(string objectType, string objectName, string customControlType, string customReportItemName, string styleName)
		{
			return RPResWrapper.Keys.GetString("rsCRIRenderItemDuplicateStyle", objectType, objectName, customControlType, customReportItemName, styleName);
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x001717D3 File Offset: 0x0016F9D3
		public static string rsCRIRenderItemInvalidStyleType(string objectType, string objectName, string customControlType, string customReportItemName, string styleName)
		{
			return RPResWrapper.Keys.GetString("rsCRIRenderItemInvalidStyleType", objectType, objectName, customControlType, customReportItemName, styleName);
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x001717E5 File Offset: 0x0016F9E5
		public static string rsCRIRenderItemInvalidStyle(string objectType, string objectName, string customControlType, string customReportItemName, string styleName)
		{
			return RPResWrapper.Keys.GetString("rsCRIRenderItemInvalidStyle", objectType, objectName, customControlType, customReportItemName, styleName);
		}

		// Token: 0x06005954 RID: 22868 RVA: 0x001717F7 File Offset: 0x0016F9F7
		public static string rsCRIControlFailedToLoad(string objectType, string objectName, string customControlType)
		{
			return RPResWrapper.Keys.GetString("rsCRIControlFailedToLoad", objectType, objectName, customControlType);
		}

		// Token: 0x06005955 RID: 22869 RVA: 0x00171806 File Offset: 0x0016FA06
		public static string rsCRIControlNotInstalled(string objectType, string objectName, string customControlType)
		{
			return RPResWrapper.Keys.GetString("rsCRIControlNotInstalled", objectType, objectName, customControlType);
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x00171815 File Offset: 0x0016FA15
		public static string rsSandboxingExpressionExceedsMaximumLength(string objectType, string objectName, string propertyName, string maxLength)
		{
			return RPResWrapper.Keys.GetString("rsSandboxingExpressionExceedsMaximumLength", objectType, objectName, propertyName, maxLength);
		}

		// Token: 0x06005957 RID: 22871 RVA: 0x00171825 File Offset: 0x0016FA25
		public static string rsSandboxingStringResultExceedsMaximumLength(string objectType, string objectName, string propertyName, string maxLength)
		{
			return RPResWrapper.Keys.GetString("rsSandboxingStringResultExceedsMaximumLength", objectType, objectName, propertyName, maxLength);
		}

		// Token: 0x06005958 RID: 22872 RVA: 0x00171835 File Offset: 0x0016FA35
		public static string rsSandboxingArrayResultExceedsMaximumLength(string objectType, string objectName, string propertyName, string maxItems)
		{
			return RPResWrapper.Keys.GetString("rsSandboxingArrayResultExceedsMaximumLength", objectType, objectName, propertyName, maxItems);
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x00171845 File Offset: 0x0016FA45
		public static string rsSandboxingInvalidExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsSandboxingInvalidExpression", objectType, objectName, propertyName);
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x00171854 File Offset: 0x0016FA54
		public static string rsSandboxingInvalidTypeOrMemberName(string objectType, string objectName, string propertyName, string itemName)
		{
			return RPResWrapper.Keys.GetString("rsSandboxingInvalidTypeOrMemberName", objectType, objectName, propertyName, itemName);
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x00171864 File Offset: 0x0016FA64
		public static string rsSandboxingInvalidNewType(string objectType, string objectName, string propertyName, string itemName)
		{
			return RPResWrapper.Keys.GetString("rsSandboxingInvalidNewType", objectType, objectName, propertyName, itemName);
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x00171874 File Offset: 0x0016FA74
		public static string rsSandboxingInvalidClassName(string objectType, string instanceName, string className)
		{
			return RPResWrapper.Keys.GetString("rsSandboxingInvalidClassName", objectType, instanceName, className);
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x00171883 File Offset: 0x0016FA83
		public static string rsSandboxingInvalidCodeModule(string objectType, string objectName, string propertyName, string assemblyName)
		{
			return RPResWrapper.Keys.GetString("rsSandboxingInvalidCodeModule", objectType, objectName, propertyName, assemblyName);
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x00171893 File Offset: 0x0016FA93
		public static string rsSandboxingCodeModuleUnavailableMode(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsSandboxingCodeModuleUnavailableMode", objectType, objectName, propertyName);
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x001718A2 File Offset: 0x0016FAA2
		public static string rsSandboxingExternalResourceExceedsMaximumSize(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsSandboxingExternalResourceExceedsMaximumSize", objectType, objectName, propertyName);
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x001718B1 File Offset: 0x0016FAB1
		public static string rsAggregateOfMixedDataTypes(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateOfMixedDataTypes", objectType, objectName, propertyName);
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x001718C0 File Offset: 0x0016FAC0
		public static string rsAggregateOfNonNumericData(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateOfNonNumericData", objectType, objectName, propertyName);
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x001718CF File Offset: 0x0016FACF
		public static string rsAggregateOfInvalidExpressionDataType(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsAggregateOfInvalidExpressionDataType", objectType, objectName, propertyName);
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x001718DE File Offset: 0x0016FADE
		public static string rsLookupOfInvalidExpressionDataType(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsLookupOfInvalidExpressionDataType", objectType, objectName, propertyName);
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x001718ED File Offset: 0x0016FAED
		public static string rsCyclicExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCyclicExpression", objectType, objectName, propertyName);
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x001718FC File Offset: 0x0016FAFC
		public static string rsCyclicExpressionInReportVariable(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCyclicExpressionInReportVariable", objectType, objectName, propertyName);
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x0017190B File Offset: 0x0016FB0B
		public static string rsCyclicExpressionInGroupVariable(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCyclicExpressionInGroupVariable", objectType, objectName, propertyName);
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x0017191A File Offset: 0x0016FB1A
		public static string rsErrorExecutingSubreport(string objectType, string objectName, string instanceID, string message)
		{
			return RPResWrapper.Keys.GetString("rsErrorExecutingSubreport", objectType, objectName, instanceID, message);
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x0017192A File Offset: 0x0016FB2A
		public static string rsInvalidExpressionDataType(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidExpressionDataType", objectType, objectName, propertyName);
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x00171939 File Offset: 0x0016FB39
		public static string rsFieldErrorInExpression(string objectType, string objectName, string propertyName, string errorName)
		{
			return RPResWrapper.Keys.GetString("rsFieldErrorInExpression", objectType, objectName, propertyName, errorName);
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x00171949 File Offset: 0x0016FB49
		public static string rsInvalidValidValueList(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidValidValueList", objectType, objectName, propertyName);
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x00171958 File Offset: 0x0016FB58
		public static string rsMinMaxOfNonSortableData(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMinMaxOfNonSortableData", objectType, objectName, propertyName);
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x00171967 File Offset: 0x0016FB67
		public static string rsRuntimeErrorInExpression(string objectType, string objectName, string propertyName, string message)
		{
			return RPResWrapper.Keys.GetString("rsRuntimeErrorInExpression", objectType, objectName, propertyName, message);
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x00171977 File Offset: 0x0016FB77
		public static string rsRuntimeUserProfileDependency(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRuntimeUserProfileDependency", objectType, objectName, propertyName);
		}

		// Token: 0x0600596E RID: 22894 RVA: 0x00171986 File Offset: 0x0016FB86
		public static string rsMissingFieldInDataSet(string objectType, string objectName, string propertyName, string fieldName)
		{
			return RPResWrapper.Keys.GetString("rsMissingFieldInDataSet", objectType, objectName, propertyName, fieldName);
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x00171996 File Offset: 0x0016FB96
		public static string rsDataSetFieldTypeNotSupported(string objectType, string objectName, string propertyName, string fieldName)
		{
			return RPResWrapper.Keys.GetString("rsDataSetFieldTypeNotSupported", objectType, objectName, propertyName, fieldName);
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x001719A6 File Offset: 0x0016FBA6
		public static string rsErrorReadingDataSetField(string objectType, string objectName, string propertyName, string fieldName, string optionalInnerExceptionMessage)
		{
			return RPResWrapper.Keys.GetString("rsErrorReadingDataSetField", objectType, objectName, propertyName, fieldName, optionalInnerExceptionMessage);
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x001719B8 File Offset: 0x0016FBB8
		public static string rsErrorReadingFieldProperty(string objectType, string objectName, string propertyName, string fieldName, string extendedPropertyName, string optionalInnerExceptionMessage)
		{
			return RPResWrapper.Keys.GetString("rsErrorReadingFieldProperty", objectType, objectName, propertyName, fieldName, extendedPropertyName, optionalInnerExceptionMessage);
		}

		// Token: 0x06005972 RID: 22898 RVA: 0x001719CC File Offset: 0x0016FBCC
		public static string rsUnexpectedErrorInExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsUnexpectedErrorInExpression", objectType, objectName, propertyName);
		}

		// Token: 0x06005973 RID: 22899 RVA: 0x001719DB File Offset: 0x0016FBDB
		public static string rsWarningExecutingSubreport(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsWarningExecutingSubreport", objectType, objectName, propertyName);
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x001719EA File Offset: 0x0016FBEA
		public static string rsInvalidImageReference(string objectType, string objectName, string propertyName, string additionalMessage)
		{
			return RPResWrapper.Keys.GetString("rsInvalidImageReference", objectType, objectName, propertyName, additionalMessage);
		}

		// Token: 0x06005975 RID: 22901 RVA: 0x001719FA File Offset: 0x0016FBFA
		public static string rsExternalImageLoadingDisabled()
		{
			return RPResWrapper.Keys.GetString("rsExternalImageLoadingDisabled");
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x00171A06 File Offset: 0x0016FC06
		public static string rsInvalidDatabaseImage(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDatabaseImage", objectType, objectName, propertyName);
		}

		// Token: 0x06005977 RID: 22903 RVA: 0x00171A15 File Offset: 0x0016FC15
		public static string rsComparisonError(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsComparisonError", objectType, objectName, propertyName);
		}

		// Token: 0x06005978 RID: 22904 RVA: 0x00171A24 File Offset: 0x0016FC24
		public static string rsComparisonTypeError(string objectType, string objectName, string propertyName, string typeX, string typeY)
		{
			return RPResWrapper.Keys.GetString("rsComparisonTypeError", objectType, objectName, propertyName, typeX, typeY);
		}

		// Token: 0x06005979 RID: 22905 RVA: 0x00171A36 File Offset: 0x0016FC36
		public static string rsCollationDetectionFailed(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsCollationDetectionFailed", objectType, objectName, propertyName);
		}

		// Token: 0x0600597A RID: 22906 RVA: 0x00171A45 File Offset: 0x0016FC45
		public static string rsErrorLoadingExprHostAssembly(string objectType, string objectName, string propertyName, string details)
		{
			return RPResWrapper.Keys.GetString("rsErrorLoadingExprHostAssembly", objectType, objectName, propertyName, details);
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x00171A55 File Offset: 0x0016FC55
		public static string rsErrorInOnInit(string objectType, string objectName, string propertyName, string details)
		{
			return RPResWrapper.Keys.GetString("rsErrorInOnInit", objectType, objectName, propertyName, details);
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x00171A65 File Offset: 0x0016FC65
		public static string rsUntrustedCodeModule(string objectType, string objectName, string propertyName, string assemblyName)
		{
			return RPResWrapper.Keys.GetString("rsUntrustedCodeModule", objectType, objectName, propertyName, assemblyName);
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x00171A75 File Offset: 0x0016FC75
		public static string rsExceededMaxRecursionLevel(string subreportName)
		{
			return RPResWrapper.Keys.GetString("rsExceededMaxRecursionLevel", subreportName);
		}

		// Token: 0x0600597E RID: 22910 RVA: 0x00171A82 File Offset: 0x0016FC82
		public static string rsEngineMismatchSubReport(string objectType, string objectName, string propertyName, string subreportName, string parentReportPath)
		{
			return RPResWrapper.Keys.GetString("rsEngineMismatchSubReport", objectType, objectName, propertyName, subreportName, parentReportPath);
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x00171A94 File Offset: 0x0016FC94
		public static string rsEngineMismatchParentReport(string objectType, string objectName, string propertyName, string subreportName, string parentReportPath)
		{
			return RPResWrapper.Keys.GetString("rsEngineMismatchParentReport", objectType, objectName, propertyName, subreportName, parentReportPath);
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x00171AA6 File Offset: 0x0016FCA6
		public static string rsMissingSubReport(string subreportName, string subreportPath)
		{
			return RPResWrapper.Keys.GetString("rsMissingSubReport", subreportName, subreportPath);
		}

		// Token: 0x06005981 RID: 22913 RVA: 0x00171AB4 File Offset: 0x0016FCB4
		public static string rsSubReportDataRetrievalFailed(string subreportName, string subreportPath)
		{
			return RPResWrapper.Keys.GetString("rsSubReportDataRetrievalFailed", subreportName, subreportPath);
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x00171AC2 File Offset: 0x0016FCC2
		public static string rsSubReportDataNotRetrieved(string subreportName, string subreportPath)
		{
			return RPResWrapper.Keys.GetString("rsSubReportDataNotRetrieved", subreportName, subreportPath);
		}

		// Token: 0x06005983 RID: 22915 RVA: 0x00171AD0 File Offset: 0x0016FCD0
		public static string rsSubReportParametersNotSpecified(string subreportName, string subreportPath)
		{
			return RPResWrapper.Keys.GetString("rsSubReportParametersNotSpecified", subreportName, subreportPath);
		}

		// Token: 0x06005984 RID: 22916 RVA: 0x00171ADE File Offset: 0x0016FCDE
		public static string rsInvalidRichTextParseFailed(string objectType, string objectName, string markupType, string innerMessage)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRichTextParseFailed", objectType, objectName, markupType, innerMessage);
		}

		// Token: 0x06005985 RID: 22917 RVA: 0x00171AEE File Offset: 0x0016FCEE
		public static string rsInvalidCollationCultureName(string objectType, string objectName, string dataSourceType, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidCollationCultureName", objectType, objectName, dataSourceType, offendingValue);
		}

		// Token: 0x06005986 RID: 22918 RVA: 0x00171AFE File Offset: 0x0016FCFE
		public static string rsParseErrorInvalidSize(string objectType, string objectName, string propertyName, string offendingValue, string charPosition)
		{
			return RPResWrapper.Keys.GetString("rsParseErrorInvalidSize", objectType, objectName, propertyName, offendingValue, charPosition);
		}

		// Token: 0x06005987 RID: 22919 RVA: 0x00171B10 File Offset: 0x0016FD10
		public static string rsParseErrorInvalidValue(string objectType, string objectName, string propertyName, string offendingValue, string charPosition)
		{
			return RPResWrapper.Keys.GetString("rsParseErrorInvalidValue", objectType, objectName, propertyName, offendingValue, charPosition);
		}

		// Token: 0x06005988 RID: 22920 RVA: 0x00171B22 File Offset: 0x0016FD22
		public static string rsParseErrorInvalidColor(string objectType, string objectName, string propertyName, string offendingValue, string charPosition)
		{
			return RPResWrapper.Keys.GetString("rsParseErrorInvalidColor", objectType, objectName, propertyName, offendingValue, charPosition);
		}

		// Token: 0x06005989 RID: 22921 RVA: 0x00171B34 File Offset: 0x0016FD34
		public static string rsParseErrorOutOfRangeSize(string objectType, string objectName, string propertyName, string offendingValue, string minValue, string maxValue)
		{
			return RPResWrapper.Keys.GetString("rsParseErrorOutOfRangeSize", objectType, objectName, propertyName, offendingValue, minValue, maxValue);
		}

		// Token: 0x0600598A RID: 22922 RVA: 0x00171B48 File Offset: 0x0016FD48
		public static string rsInvalidEmptyImageReference(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidEmptyImageReference", objectType, objectName, propertyName);
		}

		// Token: 0x0600598B RID: 22923 RVA: 0x00171B57 File Offset: 0x0016FD57
		public static string rsFieldReference(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsFieldReference", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600598C RID: 22924 RVA: 0x00171B67 File Offset: 0x0016FD67
		public static string rsFieldReferenceAmbiguous(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsFieldReferenceAmbiguous", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x00171B77 File Offset: 0x0016FD77
		public static string rsInvalidBackgroundRepeat(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidBackgroundRepeat", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600598E RID: 22926 RVA: 0x00171B87 File Offset: 0x0016FD87
		public static string rsInvalidBackgroundGradientType(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidBackgroundGradientType", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600598F RID: 22927 RVA: 0x00171B97 File Offset: 0x0016FD97
		public static string rsInvalidBorderStyle(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidBorderStyle", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005990 RID: 22928 RVA: 0x00171BA7 File Offset: 0x0016FDA7
		public static string rsInvalidCalendar(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidCalendar", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005991 RID: 22929 RVA: 0x00171BB7 File Offset: 0x0016FDB7
		public static string rsInvalidCalendarForLanguage(string objectType, string objectName, string propertyName, string offendingValue, string language)
		{
			return RPResWrapper.Keys.GetString("rsInvalidCalendarForLanguage", objectType, objectName, propertyName, offendingValue, language);
		}

		// Token: 0x06005992 RID: 22930 RVA: 0x00171BC9 File Offset: 0x0016FDC9
		public static string rsInvalidColor(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidColor", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005993 RID: 22931 RVA: 0x00171BD9 File Offset: 0x0016FDD9
		public static string rsInvalidDirection(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDirection", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005994 RID: 22932 RVA: 0x00171BE9 File Offset: 0x0016FDE9
		public static string rsInvalidDatabaseImageProperty(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDatabaseImageProperty", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005995 RID: 22933 RVA: 0x00171BF9 File Offset: 0x0016FDF9
		public static string rsInvalidEmbeddedImageProperty(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidEmbeddedImageProperty", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005996 RID: 22934 RVA: 0x00171C09 File Offset: 0x0016FE09
		public static string rsInvalidExternalImageProperty(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidExternalImageProperty", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005997 RID: 22935 RVA: 0x00171C19 File Offset: 0x0016FE19
		public static string rsInvalidEmbeddingModeImageProperty(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidEmbeddingModeImageProperty", objectType, objectName, propertyName);
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x00171C28 File Offset: 0x0016FE28
		public static string rsInvalidFontStyle(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFontStyle", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005999 RID: 22937 RVA: 0x00171C38 File Offset: 0x0016FE38
		public static string rsInvalidFontWeight(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFontWeight", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600599A RID: 22938 RVA: 0x00171C48 File Offset: 0x0016FE48
		public static string rsInvalidFormatString(string objectType, string objectName, string propertyName, string message)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFormatString", objectType, objectName, propertyName, message);
		}

		// Token: 0x0600599B RID: 22939 RVA: 0x00171C58 File Offset: 0x0016FE58
		public static string rsInvalidLanguage(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidLanguage", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600599C RID: 22940 RVA: 0x00171C68 File Offset: 0x0016FE68
		public static string rsInvalidMeasurementUnit(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidMeasurementUnit", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600599D RID: 22941 RVA: 0x00171C78 File Offset: 0x0016FE78
		public static string rsInvalidMIMEType(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidMIMEType", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600599E RID: 22942 RVA: 0x00171C88 File Offset: 0x0016FE88
		public static string rsInvalidNumeralVariant(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidNumeralVariant", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x0600599F RID: 22943 RVA: 0x00171C98 File Offset: 0x0016FE98
		public static string rsInvalidNumeralVariantForLanguage(string objectType, string objectName, string propertyName, string offendingValue, string language)
		{
			return RPResWrapper.Keys.GetString("rsInvalidNumeralVariantForLanguage", objectType, objectName, propertyName, offendingValue, language);
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x00171CAA File Offset: 0x0016FEAA
		public static string rsInvalidSize(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSize", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x00171CBA File Offset: 0x0016FEBA
		public static string rsInvalidTextAlign(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTextAlign", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x00171CCA File Offset: 0x0016FECA
		public static string rsInvalidTextDecoration(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTextDecoration", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059A3 RID: 22947 RVA: 0x00171CDA File Offset: 0x0016FEDA
		public static string rsInvalidUnicodeBiDi(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidUnicodeBiDi", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x00171CEA File Offset: 0x0016FEEA
		public static string rsInvalidVerticalAlign(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidVerticalAlign", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059A5 RID: 22949 RVA: 0x00171CFA File Offset: 0x0016FEFA
		public static string rsInvalidWritingMode(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidWritingMode", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059A6 RID: 22950 RVA: 0x00171D0A File Offset: 0x0016FF0A
		public static string rsNegativeSize(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNegativeSize", objectType, objectName, propertyName);
		}

		// Token: 0x060059A7 RID: 22951 RVA: 0x00171D19 File Offset: 0x0016FF19
		public static string rsOutOfRangeSize(string objectType, string objectName, string propertyName, string offendingValue, string minValue, string maxValue)
		{
			return RPResWrapper.Keys.GetString("rsOutOfRangeSize", objectType, objectName, propertyName, offendingValue, minValue, maxValue);
		}

		// Token: 0x060059A8 RID: 22952 RVA: 0x00171D2D File Offset: 0x0016FF2D
		public static string rsOverallPageNumberInBody(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsOverallPageNumberInBody", objectType, objectName, propertyName);
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x00171D3C File Offset: 0x0016FF3C
		public static string rsPageNumberInBody(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPageNumberInBody", objectType, objectName, propertyName);
		}

		// Token: 0x060059AA RID: 22954 RVA: 0x00171D4B File Offset: 0x0016FF4B
		public static string rsOverallPageNumberInScopedAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsOverallPageNumberInScopedAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060059AB RID: 22955 RVA: 0x00171D5A File Offset: 0x0016FF5A
		public static string rsPageNumberInScopedAggregate(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsPageNumberInScopedAggregate", objectType, objectName, propertyName);
		}

		// Token: 0x060059AC RID: 22956 RVA: 0x00171D69 File Offset: 0x0016FF69
		public static string rsParameterReference(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsParameterReference", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x00171D79 File Offset: 0x0016FF79
		public static string rsReportItemReference(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsReportItemReference", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x00171D89 File Offset: 0x0016FF89
		public static string rsReportItemReferenceInPageSection(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsReportItemReferenceInPageSection", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x00171D99 File Offset: 0x0016FF99
		public static string rsDataSetReference(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsDataSetReference", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x00171DA9 File Offset: 0x0016FFA9
		public static string rsDataSourceReference(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsDataSourceReference", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x00171DB9 File Offset: 0x0016FFB9
		public static string rsInvalidDataSetQuery(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidDataSetQuery", objectType, objectName, propertyName);
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x00171DC8 File Offset: 0x0016FFC8
		public static string rsErrorLoadingCodeModule(string objectType, string objectName, string propertyName, string module, string details)
		{
			return RPResWrapper.Keys.GetString("rsErrorLoadingCodeModule", objectType, objectName, propertyName, module, details);
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x00171DDA File Offset: 0x0016FFDA
		public static string rsInvalidTextEffect(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTextEffect", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x00171DEA File Offset: 0x0016FFEA
		public static string rsInvalidBackgroundHatchType(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidBackgroundHatchType", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x00171DFA File Offset: 0x0016FFFA
		public static string rsInvalidBackgroundImagePosition(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidBackgroundImagePosition", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x00171E0A File Offset: 0x0017000A
		public static string rsInvalidTextOrientations(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidTextOrientations", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x00171E1A File Offset: 0x0017001A
		public static string rsInvalidListStyle(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidListStyle", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059B8 RID: 22968 RVA: 0x00171E2A File Offset: 0x0017002A
		public static string rsInvalidMarkupType(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidMarkupType", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059B9 RID: 22969 RVA: 0x00171E3A File Offset: 0x0017003A
		public static string rsInvalidRenderFormatUsage(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRenderFormatUsage", objectType, objectName, propertyName);
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x00171E49 File Offset: 0x00170049
		public static string rsNotASharedDataSetDefinition(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsNotASharedDataSetDefinition", objectType, objectName, propertyName);
		}

		// Token: 0x060059BB RID: 22971 RVA: 0x00171E58 File Offset: 0x00170058
		public static string rsInvalidSharedDataSetDefinition(string objectType, string objectName, string propertyName, string message)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSharedDataSetDefinition", objectType, objectName, propertyName, message);
		}

		// Token: 0x060059BC RID: 22972 RVA: 0x00171E68 File Offset: 0x00170068
		public static string rsMissingDataSetParameterDefault(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMissingDataSetParameterDefault", objectType, objectName, propertyName);
		}

		// Token: 0x060059BD RID: 22973 RVA: 0x00171E77 File Offset: 0x00170077
		public static string rsInvalidCollationName(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidCollationName", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x00171E87 File Offset: 0x00170087
		public static string rsCollationAndCollationCultureSpecified(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsCollationAndCollationCultureSpecified", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x00171E97 File Offset: 0x00170097
		public static string rsDataSetWithoutFields(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsDataSetWithoutFields", objectType, objectName, propertyName);
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x00171EA6 File Offset: 0x001700A6
		public static string rsInvalidFeatureRdlElement(string parentObjectType, string parentObjectName, string elementName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFeatureRdlElement", parentObjectType, parentObjectName, elementName);
		}

		// Token: 0x060059C1 RID: 22977 RVA: 0x00171EB5 File Offset: 0x001700B5
		public static string rsInvalidFeatureRdlAttribute(string parentObjectType, string parentObjectName, string attributeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFeatureRdlAttribute", parentObjectType, parentObjectName, attributeName);
		}

		// Token: 0x060059C2 RID: 22978 RVA: 0x00171EC4 File Offset: 0x001700C4
		public static string rsInvalidFeatureRdlPropertyValue(string objectType, string objectName, string propertyName, string propertyValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFeatureRdlPropertyValue", objectType, objectName, propertyName, propertyValue);
		}

		// Token: 0x060059C3 RID: 22979 RVA: 0x00171ED4 File Offset: 0x001700D4
		public static string rsInvalidFeatureRdlExpression(string objectType, string objectName, string propertyName, string expressionFeature)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFeatureRdlExpression", objectType, objectName, propertyName, expressionFeature);
		}

		// Token: 0x060059C4 RID: 22980 RVA: 0x00171EE4 File Offset: 0x001700E4
		public static string rsInvalidFeatureRdlExpressionAggregatesOfAggregates(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidFeatureRdlExpressionAggregatesOfAggregates", objectType, objectName, propertyName);
		}

		// Token: 0x060059C5 RID: 22981 RVA: 0x00171EF3 File Offset: 0x001700F3
		public static string rsInvalidPeerGroupsNotSupported(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidPeerGroupsNotSupported", objectType, objectName, propertyName);
		}

		// Token: 0x060059C6 RID: 22982 RVA: 0x00171F02 File Offset: 0x00170102
		public static string rsInvalidComplexExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidComplexExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060059C7 RID: 22983 RVA: 0x00171F11 File Offset: 0x00170111
		public static string rsRenderingChunksUnavailable(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsRenderingChunksUnavailable", objectType, objectName, propertyName);
		}

		// Token: 0x060059C8 RID: 22984 RVA: 0x00171F20 File Offset: 0x00170120
		public static string rsInvalidScopeReference(string objectType, string objectName, string propertyName, string scopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidScopeReference", objectType, objectName, propertyName, scopeName);
		}

		// Token: 0x060059C9 RID: 22985 RVA: 0x00171F30 File Offset: 0x00170130
		public static string rsInvalidScopeCollectionReference(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidScopeCollectionReference", objectType, objectName, propertyName);
		}

		// Token: 0x060059CA RID: 22986 RVA: 0x00171F3F File Offset: 0x0017013F
		public static string rsScopeReferenceInComplexExpression(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsScopeReferenceInComplexExpression", objectType, objectName, propertyName);
		}

		// Token: 0x060059CB RID: 22987 RVA: 0x00171F4E File Offset: 0x0017014E
		public static string rsScopeReferenceUsesDataSetMoreThanOnce(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsScopeReferenceUsesDataSetMoreThanOnce", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x060059CC RID: 22988 RVA: 0x00171F5E File Offset: 0x0017015E
		public static string rsInvalidRuntimeScopeReference(string scopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRuntimeScopeReference", scopeName);
		}

		// Token: 0x060059CD RID: 22989 RVA: 0x00171F6B File Offset: 0x0017016B
		public static string rsVariableTypeNotSerializable(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsVariableTypeNotSerializable", objectType, objectName, propertyName);
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x00171F7A File Offset: 0x0017017A
		public static string rsUnexpectedSerializationError(string objectType, string objectName, string message)
		{
			return RPResWrapper.Keys.GetString("rsUnexpectedSerializationError", objectType, objectName, message);
		}

		// Token: 0x060059CF RID: 22991 RVA: 0x00171F89 File Offset: 0x00170189
		public static string rsNonExistingFieldReferenceByName(string fieldName)
		{
			return RPResWrapper.Keys.GetString("rsNonExistingFieldReferenceByName", fieldName);
		}

		// Token: 0x060059D0 RID: 22992 RVA: 0x00171F96 File Offset: 0x00170196
		public static string rsNonExistingParameterReference(string paramName)
		{
			return RPResWrapper.Keys.GetString("rsNonExistingParameterReference", paramName);
		}

		// Token: 0x060059D1 RID: 22993 RVA: 0x00171FA3 File Offset: 0x001701A3
		public static string rsNonExistingReportItemReference(string itemName)
		{
			return RPResWrapper.Keys.GetString("rsNonExistingReportItemReference", itemName);
		}

		// Token: 0x060059D2 RID: 22994 RVA: 0x00171FB0 File Offset: 0x001701B0
		public static string rsNonExistingVariableReference(string varName)
		{
			return RPResWrapper.Keys.GetString("rsNonExistingVariableReference", varName);
		}

		// Token: 0x060059D3 RID: 22995 RVA: 0x00171FBD File Offset: 0x001701BD
		public static string rsNonExistingScopeReference(string scopeName)
		{
			return RPResWrapper.Keys.GetString("rsNonExistingScopeReference", scopeName);
		}

		// Token: 0x060059D4 RID: 22996 RVA: 0x00171FCA File Offset: 0x001701CA
		public static string rsNonExistingGlobalReference(string globalName)
		{
			return RPResWrapper.Keys.GetString("rsNonExistingGlobalReference", globalName);
		}

		// Token: 0x060059D5 RID: 22997 RVA: 0x00171FD7 File Offset: 0x001701D7
		public static string rsNonExistingUserReference(string propName)
		{
			return RPResWrapper.Keys.GetString("rsNonExistingUserReference", propName);
		}

		// Token: 0x060059D6 RID: 22998 RVA: 0x00171FE4 File Offset: 0x001701E4
		public static string rsNonExistingDataSetReference(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsNonExistingDataSetReference", dataSetName);
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x00171FF1 File Offset: 0x001701F1
		public static string rsNonExistingDataSourceReference(string dataSourceName)
		{
			return RPResWrapper.Keys.GetString("rsNonExistingDataSourceReference", dataSourceName);
		}

		// Token: 0x060059D8 RID: 23000 RVA: 0x00171FFE File Offset: 0x001701FE
		public static string rsFilterEvaluationError(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsFilterEvaluationError", objectType, objectName, propertyName);
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x0017200D File Offset: 0x0017020D
		public static string rsFilterFieldError(string objectType, string objectName, string propertyName, string errorName)
		{
			return RPResWrapper.Keys.GetString("rsFilterFieldError", objectType, objectName, propertyName, errorName);
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x0017201D File Offset: 0x0017021D
		public static string rsNoFieldDataAtIndex(int fieldIndex)
		{
			return RPResWrapper.Keys.GetString("rsNoFieldDataAtIndex", fieldIndex);
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x0017202F File Offset: 0x0017022F
		public static string rsErrorOpeningConnection(string dataSourceName)
		{
			return RPResWrapper.Keys.GetString("rsErrorOpeningConnection", dataSourceName);
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x0017203C File Offset: 0x0017023C
		public static string rsErrorImpersonatingUser(string dataSourceName)
		{
			return RPResWrapper.Keys.GetString("rsErrorImpersonatingUser", dataSourceName);
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x00172049 File Offset: 0x00170249
		public static string rsErrorImpersonatingServiceAccount(string dataSourceName)
		{
			return RPResWrapper.Keys.GetString("rsErrorImpersonatingServiceAccount", dataSourceName);
		}

		// Token: 0x060059DE RID: 23006 RVA: 0x00172056 File Offset: 0x00170256
		public static string rsErrorImpersonateServiceAccountNotAllowed(string dataSourceName)
		{
			return RPResWrapper.Keys.GetString("rsErrorImpersonateServiceAccountNotAllowed", dataSourceName);
		}

		// Token: 0x060059DF RID: 23007 RVA: 0x00172063 File Offset: 0x00170263
		public static string rsDataExtensionWithoutConnectionExtension(string dataSourceName)
		{
			return RPResWrapper.Keys.GetString("rsDataExtensionWithoutConnectionExtension", dataSourceName);
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x00172070 File Offset: 0x00170270
		public static string rsInvalidSharedDataSetReference(string dataSetName, string referencedDataSetName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSharedDataSetReference", dataSetName, referencedDataSetName);
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x0017207E File Offset: 0x0017027E
		public static string rsManagedDataProviderWithoutConnectionExtension(string dataSourceName)
		{
			return RPResWrapper.Keys.GetString("rsManagedDataProviderWithoutConnectionExtension", dataSourceName);
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x0017208B File Offset: 0x0017028B
		public static string rsErrorClosingConnection(string dataSourceName)
		{
			return RPResWrapper.Keys.GetString("rsErrorClosingConnection", dataSourceName);
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x00172098 File Offset: 0x00170298
		public static string rsErrorRollbackTransaction(string dataSourceName)
		{
			return RPResWrapper.Keys.GetString("rsErrorRollbackTransaction", dataSourceName);
		}

		// Token: 0x060059E4 RID: 23012 RVA: 0x001720A5 File Offset: 0x001702A5
		public static string rsErrorCommitTransaction(string dataSourceName)
		{
			return RPResWrapper.Keys.GetString("rsErrorCommitTransaction", dataSourceName);
		}

		// Token: 0x060059E5 RID: 23013 RVA: 0x001720B2 File Offset: 0x001702B2
		public static string rsErrorCreatingCommand(string dataSourceName)
		{
			return RPResWrapper.Keys.GetString("rsErrorCreatingCommand", dataSourceName);
		}

		// Token: 0x060059E6 RID: 23014 RVA: 0x001720BF File Offset: 0x001702BF
		public static string rsErrorCreatingQueryParameter(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorCreatingQueryParameter", dataSetName);
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x001720CC File Offset: 0x001702CC
		public static string rsErrorAddingMultiValueQueryParameter(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorAddingMultiValueQueryParameter", dataSetName);
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x001720D9 File Offset: 0x001702D9
		public static string rsErrorAddingQueryParameter(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorAddingQueryParameter", dataSetName);
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x001720E6 File Offset: 0x001702E6
		public static string rsErrorSettingCommandText(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorSettingCommandText", dataSetName);
		}

		// Token: 0x060059EA RID: 23018 RVA: 0x001720F3 File Offset: 0x001702F3
		public static string rsErrorSettingCommandType(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorSettingCommandType", dataSetName);
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x00172100 File Offset: 0x00170300
		public static string rsErrorSettingTransaction(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorSettingTransaction", dataSetName);
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x0017210D File Offset: 0x0017030D
		public static string rsErrorSettingQueryTimeout(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorSettingQueryTimeout", dataSetName);
		}

		// Token: 0x060059ED RID: 23021 RVA: 0x0017211A File Offset: 0x0017031A
		public static string rsErrorExecutingCommand(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorExecutingCommand", dataSetName);
		}

		// Token: 0x060059EE RID: 23022 RVA: 0x00172127 File Offset: 0x00170327
		public static string rsQueryMemoryLimitExceeded(string detail)
		{
			return RPResWrapper.Keys.GetString("rsQueryMemoryLimitExceeded", detail);
		}

		// Token: 0x060059EF RID: 23023 RVA: 0x00172134 File Offset: 0x00170334
		public static string rsQueryTimeoutExceeded(string detail)
		{
			return RPResWrapper.Keys.GetString("rsQueryTimeoutExceeded", detail);
		}

		// Token: 0x060059F0 RID: 23024 RVA: 0x00172141 File Offset: 0x00170341
		public static string rsErrorCancelingCommand(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorCancelingCommand", dataSetName);
		}

		// Token: 0x060059F1 RID: 23025 RVA: 0x0017214E File Offset: 0x0017034E
		public static string rsErrorCreatingDataReader(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorCreatingDataReader", dataSetName);
		}

		// Token: 0x060059F2 RID: 23026 RVA: 0x0017215B File Offset: 0x0017035B
		public static string rsErrorDisposingDataReader(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorDisposingDataReader", dataSetName);
		}

		// Token: 0x060059F3 RID: 23027 RVA: 0x00172168 File Offset: 0x00170368
		public static string rsErrorReadingNextDataRow(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorReadingNextDataRow", dataSetName);
		}

		// Token: 0x060059F4 RID: 23028 RVA: 0x00172175 File Offset: 0x00170375
		public static string rsErrorReadingDataField(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorReadingDataField", dataSetName);
		}

		// Token: 0x060059F5 RID: 23029 RVA: 0x00172182 File Offset: 0x00170382
		public static string rsErrorReadingDataAggregationField(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorReadingDataAggregationField", dataSetName);
		}

		// Token: 0x060059F6 RID: 23030 RVA: 0x0017218F File Offset: 0x0017038F
		public static string rsInvalidChart(string chartName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidChart", chartName);
		}

		// Token: 0x060059F7 RID: 23031 RVA: 0x0017219C File Offset: 0x0017039C
		public static string rsErrorDuringChartRendering(string chartName, string message)
		{
			return RPResWrapper.Keys.GetString("rsErrorDuringChartRendering", chartName, message);
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x001721AA File Offset: 0x001703AA
		public static string rsOWCNotInstalled(string chartName)
		{
			return RPResWrapper.Keys.GetString("rsOWCNotInstalled", chartName);
		}

		// Token: 0x060059F9 RID: 23033 RVA: 0x001721B7 File Offset: 0x001703B7
		public static string rsUnsupportedURLProtocol(string url)
		{
			return RPResWrapper.Keys.GetString("rsUnsupportedURLProtocol", url);
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x001721C4 File Offset: 0x001703C4
		public static string rsCRIProcessingError(string criName, string criType)
		{
			return RPResWrapper.Keys.GetString("rsCRIProcessingError", criName, criType);
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x001721D2 File Offset: 0x001703D2
		public static string rsSerializableTypeNotSupported(string objectType, string objectName)
		{
			return RPResWrapper.Keys.GetString("rsSerializableTypeNotSupported", objectType, objectName);
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x001721E0 File Offset: 0x001703E0
		public static string rsGaugePanelInvalidData(string gaugePanelName)
		{
			return RPResWrapper.Keys.GetString("rsGaugePanelInvalidData", gaugePanelName);
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x001721ED File Offset: 0x001703ED
		public static string rsGaugePanelInvalidMinMaxScale(string gaugePanelName)
		{
			return RPResWrapper.Keys.GetString("rsGaugePanelInvalidMinMaxScale", gaugePanelName);
		}

		// Token: 0x060059FE RID: 23038 RVA: 0x001721FA File Offset: 0x001703FA
		public static string rsGaugePanelInvalidStartEndRange(string gaugePanelName)
		{
			return RPResWrapper.Keys.GetString("rsGaugePanelInvalidStartEndRange", gaugePanelName);
		}

		// Token: 0x060059FF RID: 23039 RVA: 0x00172207 File Offset: 0x00170407
		public static string rsInvalidRowGaugeMemberCannotBeDynamic(string objectType, string objectName, string propertyName, string subPropertyName, string groupName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRowGaugeMemberCannotBeDynamic", objectType, objectName, propertyName, subPropertyName, groupName);
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x00172219 File Offset: 0x00170419
		public static string rsInvalidRowGaugeMemberCannotContainChildMember(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidRowGaugeMemberCannotContainChildMember", objectType, objectName, propertyName);
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x00172228 File Offset: 0x00170428
		public static string rsInvalidColumnGaugeMemberCannotContainMultipleChildMember(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidColumnGaugeMemberCannotContainMultipleChildMember", objectType, objectName, propertyName);
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x00172237 File Offset: 0x00170437
		public static string rsDuplicateItemName(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateItemName", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x00172247 File Offset: 0x00170447
		public static string rsDuplicateChartAxisName(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateChartAxisName", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x00172257 File Offset: 0x00170457
		public static string rsDuplicateChartLegendItemName(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateChartLegendItemName", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x00172267 File Offset: 0x00170467
		public static string rsDuplicateChartLegendCustomItemCellName(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateChartLegendCustomItemCellName", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005A06 RID: 23046 RVA: 0x00172277 File Offset: 0x00170477
		public static string rsDuplicateChartFormulaParameter(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsDuplicateChartFormulaParameter", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005A07 RID: 23047 RVA: 0x00172287 File Offset: 0x00170487
		public static string rsInvalidEnumValue(string objectType, string objectName, string propertyName, string offendingValue)
		{
			return RPResWrapper.Keys.GetString("rsInvalidEnumValue", objectType, objectName, propertyName, offendingValue);
		}

		// Token: 0x06005A08 RID: 23048 RVA: 0x00172297 File Offset: 0x00170497
		public static string rsInvalidParameterValue(string value)
		{
			return RPResWrapper.Keys.GetString("rsInvalidParameterValue", value);
		}

		// Token: 0x06005A09 RID: 23049 RVA: 0x001722A4 File Offset: 0x001704A4
		public static string rsInvalidParameterRange(int value, int downLimit, int upperLimit)
		{
			return RPResWrapper.Keys.GetString("rsInvalidParameterRange", value, downLimit, upperLimit);
		}

		// Token: 0x06005A0A RID: 23050 RVA: 0x001722C2 File Offset: 0x001704C2
		public static string rsNotInCollection(string elementName)
		{
			return RPResWrapper.Keys.GetString("rsNotInCollection", elementName);
		}

		// Token: 0x06005A0B RID: 23051 RVA: 0x001722CF File Offset: 0x001704CF
		public static string rsMapLayerMissingProperty(string objectType, string objectName, string layerName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsMapLayerMissingProperty", objectType, objectName, layerName, propertyName);
		}

		// Token: 0x06005A0C RID: 23052 RVA: 0x001722DF File Offset: 0x001704DF
		public static string rsMapMaximumSpatialElementCountReached(string objectType, string objectName)
		{
			return RPResWrapper.Keys.GetString("rsMapMaximumSpatialElementCountReached", objectType, objectName);
		}

		// Token: 0x06005A0D RID: 23053 RVA: 0x001722ED File Offset: 0x001704ED
		public static string rsMapMaximumTotalPointCountReached(string objectType, string objectName)
		{
			return RPResWrapper.Keys.GetString("rsMapMaximumTotalPointCountReached", objectType, objectName);
		}

		// Token: 0x06005A0E RID: 23054 RVA: 0x001722FB File Offset: 0x001704FB
		public static string rsMapInvalidSpatialFieldType(string objectType, string objectName, string layerName)
		{
			return RPResWrapper.Keys.GetString("rsMapInvalidSpatialFieldType", objectType, objectName, layerName);
		}

		// Token: 0x06005A0F RID: 23055 RVA: 0x0017230A File Offset: 0x0017050A
		public static string rsMapInvalidFieldName(string objectType, string objectName, string layerName, string fieldName)
		{
			return RPResWrapper.Keys.GetString("rsMapInvalidFieldName", objectType, objectName, layerName, fieldName);
		}

		// Token: 0x06005A10 RID: 23056 RVA: 0x0017231A File Offset: 0x0017051A
		public static string rsMapFieldBindingExpressionTypeMismatch(string objectType, string objectName, string layerName, string bindingFieldName)
		{
			return RPResWrapper.Keys.GetString("rsMapFieldBindingExpressionTypeMismatch", objectType, objectName, layerName, bindingFieldName);
		}

		// Token: 0x06005A11 RID: 23057 RVA: 0x0017232A File Offset: 0x0017052A
		public static string rsMapSpatialElementHasMoreThanOnMatchingGroupInstance(string objectType, string objectName, string layerName)
		{
			return RPResWrapper.Keys.GetString("rsMapSpatialElementHasMoreThanOnMatchingGroupInstance", objectType, objectName, layerName);
		}

		// Token: 0x06005A12 RID: 23058 RVA: 0x00172339 File Offset: 0x00170539
		public static string rsMapInvalidShapefileReference(string objectType, string objectName, string url, string additionalMessage)
		{
			return RPResWrapper.Keys.GetString("rsMapInvalidShapefileReference", objectType, objectName, url, additionalMessage);
		}

		// Token: 0x06005A13 RID: 23059 RVA: 0x00172349 File Offset: 0x00170549
		public static string rsMapCannotLoadShapefile(string objectType, string objectName, string url)
		{
			return RPResWrapper.Keys.GetString("rsMapCannotLoadShapefile", objectType, objectName, url);
		}

		// Token: 0x06005A14 RID: 23060 RVA: 0x00172358 File Offset: 0x00170558
		public static string rsMapShapefileTypeMismatch(string objectType, string objectName, string layerName, string url)
		{
			return RPResWrapper.Keys.GetString("rsMapShapefileTypeMismatch", objectType, objectName, layerName, url);
		}

		// Token: 0x06005A15 RID: 23061 RVA: 0x00172368 File Offset: 0x00170568
		public static string rsCannotCompareSpatialType(string objectType, string objectName, string typeName)
		{
			return RPResWrapper.Keys.GetString("rsCannotCompareSpatialType", objectType, objectName, typeName);
		}

		// Token: 0x06005A16 RID: 23062 RVA: 0x00172377 File Offset: 0x00170577
		public static string rsMapUnsupportedValueFieldType(string objectType, string objectName, string layerName, string fieldName)
		{
			return RPResWrapper.Keys.GetString("rsMapUnsupportedValueFieldType", objectType, objectName, layerName, fieldName);
		}

		// Token: 0x06005A17 RID: 23063 RVA: 0x00172387 File Offset: 0x00170587
		public static string rsUnionOfNonSpatialData(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsUnionOfNonSpatialData", objectType, objectName, propertyName);
		}

		// Token: 0x06005A18 RID: 23064 RVA: 0x00172396 File Offset: 0x00170596
		public static string rsUnionOfMixedSpatialTypes(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsUnionOfMixedSpatialTypes", objectType, objectName, propertyName);
		}

		// Token: 0x06005A19 RID: 23065 RVA: 0x001723A5 File Offset: 0x001705A5
		public static string rsInvalidMapDataRegionName(string objectType, string objectName, string propertyName, string dataRegionName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidMapDataRegionName", objectType, objectName, propertyName, dataRegionName);
		}

		// Token: 0x06005A1A RID: 23066 RVA: 0x001723B5 File Offset: 0x001705B5
		public static string rsInvalidGroupingDomainScopeWithParent(string objectType, string objectName, string propertyName, string groupName, string domainScopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingDomainScopeWithParent", objectType, objectName, propertyName, groupName, domainScopeName);
		}

		// Token: 0x06005A1B RID: 23067 RVA: 0x001723C7 File Offset: 0x001705C7
		public static string rsInvalidGroupingDomainScopeTargetWithParent(string objectType, string objectName, string propertyName, string groupName, string domainScopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingDomainScopeTargetWithParent", objectType, objectName, propertyName, groupName, domainScopeName);
		}

		// Token: 0x06005A1C RID: 23068 RVA: 0x001723D9 File Offset: 0x001705D9
		public static string rsInvalidGroupingDomainScopeWithDetailGroup(string objectType, string objectName, string propertyName, string groupName, string domainScopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingDomainScopeWithDetailGroup", objectType, objectName, propertyName, groupName, domainScopeName);
		}

		// Token: 0x06005A1D RID: 23069 RVA: 0x001723EB File Offset: 0x001705EB
		public static string rsInvalidGroupingDomainScope(string objectType, string objectName, string propertyName, string groupName, string domainScopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingDomainScope", objectType, objectName, propertyName, groupName, domainScopeName);
		}

		// Token: 0x06005A1E RID: 23070 RVA: 0x001723FD File Offset: 0x001705FD
		public static string rsInvalidGroupingDomainScopeDataSet(string objectType, string objectName, string propertyName, string groupName, string domainScopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingDomainScopeDataSet", objectType, objectName, propertyName, groupName, domainScopeName);
		}

		// Token: 0x06005A1F RID: 23071 RVA: 0x0017240F File Offset: 0x0017060F
		public static string rsInvalidGroupingDomainScopeNotAncestor(string objectType, string objectName, string propertyName, string groupName, string domainScopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingDomainScopeNotAncestor", objectType, objectName, propertyName, groupName, domainScopeName);
		}

		// Token: 0x06005A20 RID: 23072 RVA: 0x00172421 File Offset: 0x00170621
		public static string rsInvalidGroupingDomainScopeNotLeaf(string objectType, string objectName, string propertyName, string groupName, string domainScopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingDomainScopeNotLeaf", objectType, objectName, propertyName, groupName, domainScopeName);
		}

		// Token: 0x06005A21 RID: 23073 RVA: 0x00172433 File Offset: 0x00170633
		public static string rsInvalidGroupingDomainScopeMap(string objectType, string objectName, string propertyName, string groupName, string domainScopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidGroupingDomainScopeMap", objectType, objectName, propertyName, groupName, domainScopeName);
		}

		// Token: 0x06005A22 RID: 23074 RVA: 0x00172445 File Offset: 0x00170645
		public static string rsInvalidSortExpressionScopeDomainScope(string objectType, string objectName, string propertyName, string scopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSortExpressionScopeDomainScope", objectType, objectName, propertyName, scopeName);
		}

		// Token: 0x06005A23 RID: 23075 RVA: 0x00172455 File Offset: 0x00170655
		public static string rsStateIndicatorInvalidAutoGenerateMinMaxExpression(string objectType, string objectName, string propertyName, string stateIndicatorName)
		{
			return RPResWrapper.Keys.GetString("rsStateIndicatorInvalidAutoGenerateMinMaxExpression", objectType, objectName, propertyName, stateIndicatorName);
		}

		// Token: 0x06005A24 RID: 23076 RVA: 0x00172465 File Offset: 0x00170665
		public static string rsStateIndicatorInvalidTransformationScope(string objectType, string objectName, string propertyName, string stateIndicatorName)
		{
			return RPResWrapper.Keys.GetString("rsStateIndicatorInvalidTransformationScope", objectType, objectName, propertyName, stateIndicatorName);
		}

		// Token: 0x06005A25 RID: 23077 RVA: 0x00172475 File Offset: 0x00170675
		public static string rsStateIndicatorInvalidMinMax(string gaugePanelName, string stateIndicatorName)
		{
			return RPResWrapper.Keys.GetString("rsStateIndicatorInvalidMinMax", gaugePanelName, stateIndicatorName);
		}

		// Token: 0x06005A26 RID: 23078 RVA: 0x00172483 File Offset: 0x00170683
		public static string rsUnrecognizedNonIgnorableNamespaces(string namespaces)
		{
			return RPResWrapper.Keys.GetString("rsUnrecognizedNonIgnorableNamespaces", namespaces);
		}

		// Token: 0x06005A27 RID: 23079 RVA: 0x00172490 File Offset: 0x00170690
		public static string rsUndefinedMustUnderstandNamespaces(string namespaces)
		{
			return RPResWrapper.Keys.GetString("rsUndefinedMustUnderstandNamespaces", namespaces);
		}

		// Token: 0x06005A28 RID: 23080 RVA: 0x0017249D File Offset: 0x0017069D
		public static string rsParameterValueCastFailure(string objectType, string objectName, string propertyName, string paramName)
		{
			return RPResWrapper.Keys.GetString("rsParameterValueCastFailure", objectType, objectName, propertyName, paramName);
		}

		// Token: 0x06005A29 RID: 23081 RVA: 0x001724AD File Offset: 0x001706AD
		public static string rsInvalidBandInvalidLayoutDirection(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidBandInvalidLayoutDirection", objectType, objectName, propertyName);
		}

		// Token: 0x06005A2A RID: 23082 RVA: 0x001724BC File Offset: 0x001706BC
		public static string rsInvalidBandPageBreakIsSet(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidBandPageBreakIsSet", objectType, objectName, propertyName);
		}

		// Token: 0x06005A2B RID: 23083 RVA: 0x001724CB File Offset: 0x001706CB
		public static string rsInvalidBandShouldNotBeTogglable(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidBandShouldNotBeTogglable", objectType, objectName, propertyName);
		}

		// Token: 0x06005A2C RID: 23084 RVA: 0x001724DA File Offset: 0x001706DA
		public static string rsBandKeepTogetherIgnored(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsBandKeepTogetherIgnored", objectType, objectName, propertyName);
		}

		// Token: 0x06005A2D RID: 23085 RVA: 0x001724E9 File Offset: 0x001706E9
		public static string rsBandIgnoredProperties(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsBandIgnoredProperties", objectType, objectName, propertyName);
		}

		// Token: 0x06005A2E RID: 23086 RVA: 0x001724F8 File Offset: 0x001706F8
		public static string rsInvalidBandNavigationReference(string objectType, string objectName, string navigationType, string invalidReportItemReference)
		{
			return RPResWrapper.Keys.GetString("rsInvalidBandNavigationReference", objectType, objectName, navigationType, invalidReportItemReference);
		}

		// Token: 0x06005A2F RID: 23087 RVA: 0x00172508 File Offset: 0x00170708
		public static string rsInvalidBandNavigationItem(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidBandNavigationItem", objectType, objectName, propertyName);
		}

		// Token: 0x06005A30 RID: 23088 RVA: 0x00172517 File Offset: 0x00170717
		public static string rsInvalidBandNavigations(string objectType, string objectName, string propertyName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidBandNavigations", objectType, objectName, propertyName);
		}

		// Token: 0x06005A31 RID: 23089 RVA: 0x00172526 File Offset: 0x00170726
		public static string rsInvalidSliderDataSetReferenceField(string objectType, string objectName, string propertyName, string fieldName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSliderDataSetReferenceField", objectType, objectName, propertyName, fieldName, dataSetName);
		}

		// Token: 0x06005A32 RID: 23090 RVA: 0x00172538 File Offset: 0x00170738
		public static string rsInvalidSliderDataSetReference(string objectType, string objectName, string propertyName, string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSliderDataSetReference", objectType, objectName, propertyName, dataSetName);
		}

		// Token: 0x06005A33 RID: 23091 RVA: 0x00172548 File Offset: 0x00170748
		public static string rsNotSupportedInStreamingMode(string methodName)
		{
			return RPResWrapper.Keys.GetString("rsNotSupportedInStreamingMode", methodName);
		}

		// Token: 0x06005A34 RID: 23092 RVA: 0x00172555 File Offset: 0x00170755
		public static string rsInvalidScopeID(string methodName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidScopeID", methodName);
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x00172562 File Offset: 0x00170762
		public static string rsInvalidScopeIDOrder(string groupName, string methodName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidScopeIDOrder", groupName, methodName);
		}

		// Token: 0x06005A36 RID: 23094 RVA: 0x00172570 File Offset: 0x00170770
		public static string rsInvalidScopeIDNotSet(string dataregionName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidScopeIDNotSet", dataregionName);
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x0017257D File Offset: 0x0017077D
		public static string rsRombasedRestartFailed(string groupName)
		{
			return RPResWrapper.Keys.GetString("rsRombasedRestartFailed", groupName);
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x0017258A File Offset: 0x0017078A
		public static string rsRombasedRestartFailedTypeMismatch(string groupName)
		{
			return RPResWrapper.Keys.GetString("rsRombasedRestartFailedTypeMismatch", groupName);
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x00172597 File Offset: 0x00170797
		public static string rsErrorSettingStartAt(string dataSetName)
		{
			return RPResWrapper.Keys.GetString("rsErrorSettingStartAt", dataSetName);
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x001725A4 File Offset: 0x001707A4
		public static string rsMissingDefaultRelationshipJoinCondition(string objectType, string objectName, string propertyName, string relatedDataSet)
		{
			return RPResWrapper.Keys.GetString("rsMissingDefaultRelationshipJoinCondition", objectType, objectName, propertyName, relatedDataSet);
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x001725B4 File Offset: 0x001707B4
		public static string rsNonExistingRelationshipRelatedScope(string objectType, string objectName, string propertyName, string relatedScopeProperty, string relatedScopeName)
		{
			return RPResWrapper.Keys.GetString("rsNonExistingRelationshipRelatedScope", objectType, objectName, propertyName, relatedScopeProperty, relatedScopeName);
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x001725C6 File Offset: 0x001707C6
		public static string rsInvalidSelfJoinRelationship(string objectType, string objectName, string propertyName, string relatedScopeProperty, string relatedScopeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidSelfJoinRelationship", objectType, objectName, propertyName, relatedScopeProperty, relatedScopeName);
		}

		// Token: 0x06005A3D RID: 23101 RVA: 0x001725D8 File Offset: 0x001707D8
		public static string rsInvalidNaturalSortGroupExpressionNotSimpleFieldReference(string objectType, string objectName, string propertyName, string attributeName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidNaturalSortGroupExpressionNotSimpleFieldReference", objectType, objectName, propertyName, attributeName);
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x001725E8 File Offset: 0x001707E8
		public static string rsInvalidParameterLayoutNumberOfRowsOrColumnsExceedingLimit(string objectType, string objectName, string propertyName, int maxNumberOfRowsOrColumns)
		{
			return RPResWrapper.Keys.GetString("rsInvalidParameterLayoutNumberOfRowsOrColumnsExceedingLimit", objectType, objectName, propertyName, maxNumberOfRowsOrColumns);
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x001725FD File Offset: 0x001707FD
		public static string rsInvalidParameterLayoutNumberOfConsecutiveEmptyRowsExceedingLimit(string objectType, string objectName, string propertyName, int maxNumberOfConsecEmptyRows)
		{
			return RPResWrapper.Keys.GetString("rsInvalidParameterLayoutNumberOfConsecutiveEmptyRowsExceedingLimit", objectType, objectName, propertyName, maxNumberOfConsecEmptyRows);
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x00172612 File Offset: 0x00170812
		public static string rsInvalidParameterLayoutParameterAppearsTwice(string objectType, string objectName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidParameterLayoutParameterAppearsTwice", objectType, objectName);
		}

		// Token: 0x06005A41 RID: 23105 RVA: 0x00172620 File Offset: 0x00170820
		public static string rsInvalidParameterLayoutParameterNotVisible(string objectType, string objectName)
		{
			return RPResWrapper.Keys.GetString("rsInvalidParameterLayoutParameterNotVisible", objectType, objectName);
		}

		// Token: 0x06005A42 RID: 23106 RVA: 0x0017262E File Offset: 0x0017082E
		public static string rsInvalidMustUnderstandNamespaces(string objectType, string objectName, string propertyName, string message)
		{
			return RPResWrapper.Keys.GetString("rsInvalidMustUnderstandNamespaces", objectType, objectName, propertyName, message);
		}

		// Token: 0x06005A43 RID: 23107 RVA: 0x0017263E File Offset: 0x0017083E
		public static string rsHasUserProfileDependencies(string reportName)
		{
			return RPResWrapper.Keys.GetString("rsHasUserProfileDependencies", reportName);
		}

		// Token: 0x02000C90 RID: 3216
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06008C3A RID: 35898 RVA: 0x0023A66C File Offset: 0x0023886C
			private Keys()
			{
			}

			// Token: 0x17002B26 RID: 11046
			// (get) Token: 0x06008C3B RID: 35899 RVA: 0x0023A674 File Offset: 0x00238874
			// (set) Token: 0x06008C3C RID: 35900 RVA: 0x0023A67B File Offset: 0x0023887B
			public static CultureInfo Culture
			{
				get
				{
					return RPResWrapper.Keys._culture;
				}
				set
				{
					RPResWrapper.Keys._culture = value;
				}
			}

			// Token: 0x06008C3D RID: 35901 RVA: 0x0023A683 File Offset: 0x00238883
			public static string GetString(string key)
			{
				return RPResWrapper.Keys.resourceManager.GetString(key, RPResWrapper.Keys._culture);
			}

			// Token: 0x06008C3E RID: 35902 RVA: 0x0023A695 File Offset: 0x00238895
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.resourceManager.GetString(key, RPResWrapper.Keys._culture), arg0);
			}

			// Token: 0x06008C3F RID: 35903 RVA: 0x0023A6B2 File Offset: 0x002388B2
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.resourceManager.GetString(key, RPResWrapper.Keys._culture), arg0, arg1);
			}

			// Token: 0x06008C40 RID: 35904 RVA: 0x0023A6D0 File Offset: 0x002388D0
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.resourceManager.GetString(key, RPResWrapper.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x06008C41 RID: 35905 RVA: 0x0023A6EF File Offset: 0x002388EF
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
			{
				return string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.resourceManager.GetString(key, RPResWrapper.Keys._culture), new object[] { arg0, arg1, arg2, arg3 });
			}

			// Token: 0x06008C42 RID: 35906 RVA: 0x0023A722 File Offset: 0x00238922
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4)
			{
				return string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.resourceManager.GetString(key, RPResWrapper.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4 });
			}

			// Token: 0x06008C43 RID: 35907 RVA: 0x0023A75A File Offset: 0x0023895A
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5)
			{
				return string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.resourceManager.GetString(key, RPResWrapper.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4, arg5 });
			}

			// Token: 0x06008C44 RID: 35908 RVA: 0x0023A798 File Offset: 0x00238998
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
			{
				return string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.resourceManager.GetString(key, RPResWrapper.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4, arg5, arg6 });
			}

			// Token: 0x06008C45 RID: 35909 RVA: 0x0023A7E8 File Offset: 0x002389E8
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7)
			{
				return string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.resourceManager.GetString(key, RPResWrapper.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
			}

			// Token: 0x04004A0D RID: 18957
			private static ResourceManager resourceManager = RPRes.ResourceManager;

			// Token: 0x04004A0E RID: 18958
			private static CultureInfo _culture = null;

			// Token: 0x04004A0F RID: 18959
			public const string rsObjectTypeReport = "rsObjectTypeReport";

			// Token: 0x04004A10 RID: 18960
			public const string rsObjectTypePage = "rsObjectTypePage";

			// Token: 0x04004A11 RID: 18961
			public const string rsObjectTypeLine = "rsObjectTypeLine";

			// Token: 0x04004A12 RID: 18962
			public const string rsObjectTypeRectangle = "rsObjectTypeRectangle";

			// Token: 0x04004A13 RID: 18963
			public const string rsObjectTypeCheckbox = "rsObjectTypeCheckbox";

			// Token: 0x04004A14 RID: 18964
			public const string rsObjectTypeTextbox = "rsObjectTypeTextbox";

			// Token: 0x04004A15 RID: 18965
			public const string rsObjectTypeImage = "rsObjectTypeImage";

			// Token: 0x04004A16 RID: 18966
			public const string rsObjectTypeSubreport = "rsObjectTypeSubreport";

			// Token: 0x04004A17 RID: 18967
			public const string rsObjectTypeActiveXControl = "rsObjectTypeActiveXControl";

			// Token: 0x04004A18 RID: 18968
			public const string rsObjectTypeList = "rsObjectTypeList";

			// Token: 0x04004A19 RID: 18969
			public const string rsObjectTypeMatrix = "rsObjectTypeMatrix";

			// Token: 0x04004A1A RID: 18970
			public const string rsObjectTypeTable = "rsObjectTypeTable";

			// Token: 0x04004A1B RID: 18971
			public const string rsObjectTypeOWCChart = "rsObjectTypeOWCChart";

			// Token: 0x04004A1C RID: 18972
			public const string rsObjectTypeGrouping = "rsObjectTypeGrouping";

			// Token: 0x04004A1D RID: 18973
			public const string rsObjectTypeReportParameter = "rsObjectTypeReportParameter";

			// Token: 0x04004A1E RID: 18974
			public const string rsObjectTypeDataSource = "rsObjectTypeDataSource";

			// Token: 0x04004A1F RID: 18975
			public const string rsObjectTypeDataSet = "rsObjectTypeDataSet";

			// Token: 0x04004A20 RID: 18976
			public const string rsObjectTypeField = "rsObjectTypeField";

			// Token: 0x04004A21 RID: 18977
			public const string rsObjectTypeQuery = "rsObjectTypeQuery";

			// Token: 0x04004A22 RID: 18978
			public const string rsObjectTypeQueryParameter = "rsObjectTypeQueryParameter";

			// Token: 0x04004A23 RID: 18979
			public const string rsObjectTypeEmbeddedImage = "rsObjectTypeEmbeddedImage";

			// Token: 0x04004A24 RID: 18980
			public const string rsObjectTypeReportItem = "rsObjectTypeReportItem";

			// Token: 0x04004A25 RID: 18981
			public const string rsObjectTypeSubtotal = "rsObjectTypeSubtotal";

			// Token: 0x04004A26 RID: 18982
			public const string rsObjectTypeCodeClass = "rsObjectTypeCodeClass";

			// Token: 0x04004A27 RID: 18983
			public const string rsObjectTypeCustomReportItem = "rsObjectTypeCustomReportItem";

			// Token: 0x04004A28 RID: 18984
			public const string rsObjectTypeChart = "rsObjectTypeChart";

			// Token: 0x04004A29 RID: 18985
			public const string rsObjectTypeGaugePanel = "rsObjectTypeGaugePanel";

			// Token: 0x04004A2A RID: 18986
			public const string rsObjectTypeMap = "rsObjectTypeMap";

			// Token: 0x04004A2B RID: 18987
			public const string rsObjectTypeMapDataRegion = "rsObjectTypeMapDataRegion";

			// Token: 0x04004A2C RID: 18988
			public const string rsObjectTypeTablix = "rsObjectTypeTablix";

			// Token: 0x04004A2D RID: 18989
			public const string rsObjectTypeParagraph = "rsObjectTypeParagraph";

			// Token: 0x04004A2E RID: 18990
			public const string rsObjectTypeTextRun = "rsObjectTypeTextRun";

			// Token: 0x04004A2F RID: 18991
			public const string rsObjectTypeReportSection = "rsObjectTypeReportSection";

			// Token: 0x04004A30 RID: 18992
			public const string rsObjectTypeSharedDataSet = "rsObjectTypeSharedDataSet";

			// Token: 0x04004A31 RID: 18993
			public const string rsObjectTypeParameter = "rsObjectTypeParameter";

			// Token: 0x04004A32 RID: 18994
			public const string rsObjectTypeTablixCell = "rsObjectTypeTablixCell";

			// Token: 0x04004A33 RID: 18995
			public const string rsObjectTypeChartDataPoint = "rsObjectTypeChartDataPoint";

			// Token: 0x04004A34 RID: 18996
			public const string rsObjectTypeDataCell = "rsObjectTypeDataCell";

			// Token: 0x04004A35 RID: 18997
			public const string rsObjectTypeGaugeCell = "rsObjectTypeGaugeCell";

			// Token: 0x04004A36 RID: 18998
			public const string rsObjectTypeMapCell = "rsObjectTypeMapCell";

			// Token: 0x04004A37 RID: 18999
			public const string rsObjectTypeDataShape = "rsObjectTypeDataShape";

			// Token: 0x04004A38 RID: 19000
			public const string rsObjectTypeDataShapeMember = "rsObjectTypeDataShapeMember";

			// Token: 0x04004A39 RID: 19001
			public const string rsObjectTypeDataShapeIntersection = "rsObjectTypeDataShapeIntersection";

			// Token: 0x04004A3A RID: 19002
			public const string rsObjectTypeDataBinding = "rsObjectTypeDataBinding";

			// Token: 0x04004A3B RID: 19003
			public const string rsObjectTypeCalculation = "rsObjectTypeCalculation";

			// Token: 0x04004A3C RID: 19004
			public const string rsObjectTypeParameterLayout = "rsObjectTypeParameterLayout";

			// Token: 0x04004A3D RID: 19005
			public const string rsObjectNameBody = "rsObjectNameBody";

			// Token: 0x04004A3E RID: 19006
			public const string rsObjectNameHeader = "rsObjectNameHeader";

			// Token: 0x04004A3F RID: 19007
			public const string rsObjectNameFooter = "rsObjectNameFooter";

			// Token: 0x04004A40 RID: 19008
			public const string rsPropertyNameSeries = "rsPropertyNameSeries";

			// Token: 0x04004A41 RID: 19009
			public const string rsDataTypeInteger = "rsDataTypeInteger";

			// Token: 0x04004A42 RID: 19010
			public const string rsDataTypeIntegerOrFloat = "rsDataTypeIntegerOrFloat";

			// Token: 0x04004A43 RID: 19011
			public const string rsAggregateInPreviousAggregate = "rsAggregateInPreviousAggregate";

			// Token: 0x04004A44 RID: 19012
			public const string rsRunningValueInPreviousAggregate = "rsRunningValueInPreviousAggregate";

			// Token: 0x04004A45 RID: 19013
			public const string rsPreviousInPreviousAggregate = "rsPreviousInPreviousAggregate";

			// Token: 0x04004A46 RID: 19014
			public const string rsRowNumberInPreviousAggregate = "rsRowNumberInPreviousAggregate";

			// Token: 0x04004A47 RID: 19015
			public const string rsInScopeOrLevelInPreviousAggregate = "rsInScopeOrLevelInPreviousAggregate";

			// Token: 0x04004A48 RID: 19016
			public const string rsInvalidScopeInInnerAggregateOfPreviousAggregate = "rsInvalidScopeInInnerAggregateOfPreviousAggregate";

			// Token: 0x04004A49 RID: 19017
			public const string rsVariableInPreviousAggregate = "rsVariableInPreviousAggregate";

			// Token: 0x04004A4A RID: 19018
			public const string rsVariableInCalculatedFieldExpression = "rsVariableInCalculatedFieldExpression";

			// Token: 0x04004A4B RID: 19019
			public const string rsVariableInGroupExpression = "rsVariableInGroupExpression";

			// Token: 0x04004A4C RID: 19020
			public const string rsVariableInQueryParameterExpression = "rsVariableInQueryParameterExpression";

			// Token: 0x04004A4D RID: 19021
			public const string rsVariableInReportParameterExpression = "rsVariableInReportParameterExpression";

			// Token: 0x04004A4E RID: 19022
			public const string rsVariableInReportLanguageExpression = "rsVariableInReportLanguageExpression";

			// Token: 0x04004A4F RID: 19023
			public const string rsVariableInDataRowSortExpression = "rsVariableInDataRowSortExpression";

			// Token: 0x04004A50 RID: 19024
			public const string rsVariableInJoinExpression = "rsVariableInJoinExpression";

			// Token: 0x04004A51 RID: 19025
			public const string rsAggregateofVariable = "rsAggregateofVariable";

			// Token: 0x04004A52 RID: 19026
			public const string rsLookupOfVariable = "rsLookupOfVariable";

			// Token: 0x04004A53 RID: 19027
			public const string rsReportItemInLookupDestinationOrResult = "rsReportItemInLookupDestinationOrResult";

			// Token: 0x04004A54 RID: 19028
			public const string rsAggregateInLookupDestinationOrResult = "rsAggregateInLookupDestinationOrResult";

			// Token: 0x04004A55 RID: 19029
			public const string rsRowNumberInLookupDestinationOrResult = "rsRowNumberInLookupDestinationOrResult";

			// Token: 0x04004A56 RID: 19030
			public const string rsPreviousInLookupDestinationOrResult = "rsPreviousInLookupDestinationOrResult";

			// Token: 0x04004A57 RID: 19031
			public const string rsVariableInDataRegionOrDataSetFilterExpression = "rsVariableInDataRegionOrDataSetFilterExpression";

			// Token: 0x04004A58 RID: 19032
			public const string rsAggregateInFilterExpression = "rsAggregateInFilterExpression";

			// Token: 0x04004A59 RID: 19033
			public const string rsAggregateInGroupExpression = "rsAggregateInGroupExpression";

			// Token: 0x04004A5A RID: 19034
			public const string rsAggregateInDataRowSortExpression = "rsAggregateInDataRowSortExpression";

			// Token: 0x04004A5B RID: 19035
			public const string rsAggregateInQueryParameterExpression = "rsAggregateInQueryParameterExpression";

			// Token: 0x04004A5C RID: 19036
			public const string rsAggregateInReportParameterExpression = "rsAggregateInReportParameterExpression";

			// Token: 0x04004A5D RID: 19037
			public const string rsAggregateInReportLanguageExpression = "rsAggregateInReportLanguageExpression";

			// Token: 0x04004A5E RID: 19038
			public const string rsAggregateInCalculatedFieldExpression = "rsAggregateInCalculatedFieldExpression";

			// Token: 0x04004A5F RID: 19039
			public const string rsAggregateInJoinExpression = "rsAggregateInJoinExpression";

			// Token: 0x04004A60 RID: 19040
			public const string rsAggregateofAggregate = "rsAggregateofAggregate";

			// Token: 0x04004A61 RID: 19041
			public const string rsNestedCustomAggregate = "rsNestedCustomAggregate";

			// Token: 0x04004A62 RID: 19042
			public const string rsAggregateReportItemInBody = "rsAggregateReportItemInBody";

			// Token: 0x04004A63 RID: 19043
			public const string rsBinaryConstant = "rsBinaryConstant";

			// Token: 0x04004A64 RID: 19044
			public const string rsChartSeriesPlotTypeIgnored = "rsChartSeriesPlotTypeIgnored";

			// Token: 0x04004A65 RID: 19045
			public const string rsCompilerErrorInExpression = "rsCompilerErrorInExpression";

			// Token: 0x04004A66 RID: 19046
			public const string rsCompilerErrorInCode = "rsCompilerErrorInCode";

			// Token: 0x04004A67 RID: 19047
			public const string rsCompilerErrorInClassInstanceDeclaration = "rsCompilerErrorInClassInstanceDeclaration";

			// Token: 0x04004A68 RID: 19048
			public const string rsUnexpectedCompilerError = "rsUnexpectedCompilerError";

			// Token: 0x04004A69 RID: 19049
			public const string rsConflictingRunningValueScopesInMatrix = "rsConflictingRunningValueScopesInMatrix";

			// Token: 0x04004A6A RID: 19050
			public const string rsConflictingRunningValueScopesInTablix = "rsConflictingRunningValueScopesInTablix";

			// Token: 0x04004A6B RID: 19051
			public const string rsCountRowsInPageSectionExpression = "rsCountRowsInPageSectionExpression";

			// Token: 0x04004A6C RID: 19052
			public const string rsCountStarNotSupported = "rsCountStarNotSupported";

			// Token: 0x04004A6D RID: 19053
			public const string rsCountStarRVNotSupported = "rsCountStarRVNotSupported";

			// Token: 0x04004A6E RID: 19054
			public const string rsCustomAggregateAndFilter = "rsCustomAggregateAndFilter";

			// Token: 0x04004A6F RID: 19055
			public const string rsDataRegionInDetailList = "rsDataRegionInDetailList";

			// Token: 0x04004A70 RID: 19056
			public const string rsDataRegionInPageSection = "rsDataRegionInPageSection";

			// Token: 0x04004A71 RID: 19057
			public const string rsDataRegionInTableDetailRow = "rsDataRegionInTableDetailRow";

			// Token: 0x04004A72 RID: 19058
			public const string rsDataRegionWithoutDataSet = "rsDataRegionWithoutDataSet";

			// Token: 0x04004A73 RID: 19059
			public const string rsDataSourceReferenceNotPublished = "rsDataSourceReferenceNotPublished";

			// Token: 0x04004A74 RID: 19060
			public const string rsDataSourceInPageSectionExpression = "rsDataSourceInPageSectionExpression";

			// Token: 0x04004A75 RID: 19061
			public const string rsDataSourceInQueryParameterExpression = "rsDataSourceInQueryParameterExpression";

			// Token: 0x04004A76 RID: 19062
			public const string rsDataSourceInReportLanguageExpression = "rsDataSourceInReportLanguageExpression";

			// Token: 0x04004A77 RID: 19063
			public const string rsDataSourceInReportParameterExpression = "rsDataSourceInReportParameterExpression";

			// Token: 0x04004A78 RID: 19064
			public const string rsDuplicateChartColumnName = "rsDuplicateChartColumnName";

			// Token: 0x04004A79 RID: 19065
			public const string rsDuplicateClassInstanceName = "rsDuplicateClassInstanceName";

			// Token: 0x04004A7A RID: 19066
			public const string rsDataSetReferenceNotPublished = "rsDataSetReferenceNotPublished";

			// Token: 0x04004A7B RID: 19067
			public const string rsPagePropertyInSubsequentReportSection = "rsPagePropertyInSubsequentReportSection";

			// Token: 0x04004A7C RID: 19068
			public const string rsInvalidPageSectionState = "rsInvalidPageSectionState";

			// Token: 0x04004A7D RID: 19069
			public const string rsNestedLookups = "rsNestedLookups";

			// Token: 0x04004A7E RID: 19070
			public const string rsLookupInFilterExpression = "rsLookupInFilterExpression";

			// Token: 0x04004A7F RID: 19071
			public const string rsInvalidLookupScope = "rsInvalidLookupScope";

			// Token: 0x04004A80 RID: 19072
			public const string rsDuplicateDataSourceName = "rsDuplicateDataSourceName";

			// Token: 0x04004A81 RID: 19073
			public const string rsInvalidDataSourceNameLength = "rsInvalidDataSourceNameLength";

			// Token: 0x04004A82 RID: 19074
			public const string rsInvalidDataSourceNameNotCLSCompliant = "rsInvalidDataSourceNameNotCLSCompliant";

			// Token: 0x04004A83 RID: 19075
			public const string rsInvalidEmbeddedImageNameNotCLSCompliant = "rsInvalidEmbeddedImageNameNotCLSCompliant";

			// Token: 0x04004A84 RID: 19076
			public const string rsInvalidEmbeddedImageNameLength = "rsInvalidEmbeddedImageNameLength";

			// Token: 0x04004A85 RID: 19077
			public const string rsDuplicateEmbeddedImageName = "rsDuplicateEmbeddedImageName";

			// Token: 0x04004A86 RID: 19078
			public const string rsDuplicateReportSectionName = "rsDuplicateReportSectionName";

			// Token: 0x04004A87 RID: 19079
			public const string rsDuplicateFieldName = "rsDuplicateFieldName";

			// Token: 0x04004A88 RID: 19080
			public const string rsDuplicateParameterName = "rsDuplicateParameterName";

			// Token: 0x04004A89 RID: 19081
			public const string rsDuplicateReportItemName = "rsDuplicateReportItemName";

			// Token: 0x04004A8A RID: 19082
			public const string rsDuplicateReportParameterName = "rsDuplicateReportParameterName";

			// Token: 0x04004A8B RID: 19083
			public const string rsDuplicateCaseInsensitiveReportParameterName = "rsDuplicateCaseInsensitiveReportParameterName";

			// Token: 0x04004A8C RID: 19084
			public const string rsDuplicateScopeName = "rsDuplicateScopeName";

			// Token: 0x04004A8D RID: 19085
			public const string rsExpressionMissingCloseParen = "rsExpressionMissingCloseParen";

			// Token: 0x04004A8E RID: 19086
			public const string rsFieldInPageSectionExpression = "rsFieldInPageSectionExpression";

			// Token: 0x04004A8F RID: 19087
			public const string rsFieldInQueryParameterExpression = "rsFieldInQueryParameterExpression";

			// Token: 0x04004A90 RID: 19088
			public const string rsFieldInReportParameterExpression = "rsFieldInReportParameterExpression";

			// Token: 0x04004A91 RID: 19089
			public const string rsFieldInReportLanguageExpression = "rsFieldInReportLanguageExpression";

			// Token: 0x04004A92 RID: 19090
			public const string rsGlobalNotDefined = "rsGlobalNotDefined";

			// Token: 0x04004A93 RID: 19091
			public const string rsDataSetInPageSectionExpression = "rsDataSetInPageSectionExpression";

			// Token: 0x04004A94 RID: 19092
			public const string rsDataSetInQueryParameterExpression = "rsDataSetInQueryParameterExpression";

			// Token: 0x04004A95 RID: 19093
			public const string rsDataSetInReportLanguageExpression = "rsDataSetInReportLanguageExpression";

			// Token: 0x04004A96 RID: 19094
			public const string rsDataSetInReportParameterExpression = "rsDataSetInReportParameterExpression";

			// Token: 0x04004A97 RID: 19095
			public const string rsInvalidActionLabel = "rsInvalidActionLabel";

			// Token: 0x04004A98 RID: 19096
			public const string rsInvalidAction = "rsInvalidAction";

			// Token: 0x04004A99 RID: 19097
			public const string rsInvalidAggregateScope = "rsInvalidAggregateScope";

			// Token: 0x04004A9A RID: 19098
			public const string rsInvalidNestedAggregateScope = "rsInvalidNestedAggregateScope";

			// Token: 0x04004A9B RID: 19099
			public const string rsNestedAggregateScopesFromDifferentAxes = "rsNestedAggregateScopesFromDifferentAxes";

			// Token: 0x04004A9C RID: 19100
			public const string rsIncompatibleNestedAggregateScopes = "rsIncompatibleNestedAggregateScopes";

			// Token: 0x04004A9D RID: 19101
			public const string rsNestedAggregateScopeRequired = "rsNestedAggregateScopeRequired";

			// Token: 0x04004A9E RID: 19102
			public const string rsInvalidNestedDataSetAggregate = "rsInvalidNestedDataSetAggregate";

			// Token: 0x04004A9F RID: 19103
			public const string rsDataSetAggregateOfAggregates = "rsDataSetAggregateOfAggregates";

			// Token: 0x04004AA0 RID: 19104
			public const string rsInvalidAltReportItem = "rsInvalidAltReportItem";

			// Token: 0x04004AA1 RID: 19105
			public const string rsInvalidBooleanConstant = "rsInvalidBooleanConstant";

			// Token: 0x04004AA2 RID: 19106
			public const string rsInvalidDateTimeConstant = "rsInvalidDateTimeConstant";

			// Token: 0x04004AA3 RID: 19107
			public const string rsInvalidFloatConstant = "rsInvalidFloatConstant";

			// Token: 0x04004AA4 RID: 19108
			public const string rsInvalidCategoryGrouping = "rsInvalidCategoryGrouping";

			// Token: 0x04004AA5 RID: 19109
			public const string rsInvalidChartHierarchy = "rsInvalidChartHierarchy";

			// Token: 0x04004AA6 RID: 19110
			public const string rsInvalidChartMemberMustContainGroupExpressions = "rsInvalidChartMemberMustContainGroupExpressions";

			// Token: 0x04004AA7 RID: 19111
			public const string rsInvalidChartMemberMustBeDynamic = "rsInvalidChartMemberMustBeDynamic";

			// Token: 0x04004AA8 RID: 19112
			public const string rsInvlaidAxisAngle = "rsInvlaidAxisAngle";

			// Token: 0x04004AA9 RID: 19113
			public const string rsInvalidCharacterInExpression = "rsInvalidCharacterInExpression";

			// Token: 0x04004AAA RID: 19114
			public const string rsInvalidChartColumnNameNotCLSCompliant = "rsInvalidChartColumnNameNotCLSCompliant";

			// Token: 0x04004AAB RID: 19115
			public const string rsInvalidChartColumnNameLength = "rsInvalidChartColumnNameLength";

			// Token: 0x04004AAC RID: 19116
			public const string rsInvalidChartGroupings = "rsInvalidChartGroupings";

			// Token: 0x04004AAD RID: 19117
			public const string rsInvalidChartSubType = "rsInvalidChartSubType";

			// Token: 0x04004AAE RID: 19118
			public const string rsInvalidColumnGrouping = "rsInvalidColumnGrouping";

			// Token: 0x04004AAF RID: 19119
			public const string rsInvalidColumnsInBody = "rsInvalidColumnsInBody";

			// Token: 0x04004AB0 RID: 19120
			public const string rsInvalidColumnsInReportSection = "rsInvalidColumnsInReportSection";

			// Token: 0x04004AB1 RID: 19121
			public const string rsInvalidCustomAggregateExpression = "rsInvalidCustomAggregateExpression";

			// Token: 0x04004AB2 RID: 19122
			public const string rsInvalidCustomAggregateScope = "rsInvalidCustomAggregateScope";

			// Token: 0x04004AB3 RID: 19123
			public const string rsInvalidCustomPropertyName = "rsInvalidCustomPropertyName";

			// Token: 0x04004AB4 RID: 19124
			public const string rsInvalidChartDataValueNameNotUnique = "rsInvalidChartDataValueNameNotUnique";

			// Token: 0x04004AB5 RID: 19125
			public const string rsInvalidObjectNameNotUnique = "rsInvalidObjectNameNotUnique";

			// Token: 0x04004AB6 RID: 19126
			public const string rsInvalidObjectNameNotCLSCompliant = "rsInvalidObjectNameNotCLSCompliant";

			// Token: 0x04004AB7 RID: 19127
			public const string rsInvalidChartDataValueNameNotConstant = "rsInvalidChartDataValueNameNotConstant";

			// Token: 0x04004AB8 RID: 19128
			public const string rsInvalidChartDataValueName = "rsInvalidChartDataValueName";

			// Token: 0x04004AB9 RID: 19129
			public const string rsInvalidSourceSeriesName = "rsInvalidSourceSeriesName";

			// Token: 0x04004ABA RID: 19130
			public const string rsInvalidDataElementNameNotCLSCompliant = "rsInvalidDataElementNameNotCLSCompliant";

			// Token: 0x04004ABB RID: 19131
			public const string rsInvalidDataElementNameLength = "rsInvalidDataElementNameLength";

			// Token: 0x04004ABC RID: 19132
			public const string rsInvalidDataSource = "rsInvalidDataSource";

			// Token: 0x04004ABD RID: 19133
			public const string rsInvalidDataSourceReference = "rsInvalidDataSourceReference";

			// Token: 0x04004ABE RID: 19134
			public const string rsInvalidDefaultValue = "rsInvalidDefaultValue";

			// Token: 0x04004ABF RID: 19135
			public const string rsInvalidDefaultValueValues = "rsInvalidDefaultValueValues";

			// Token: 0x04004AC0 RID: 19136
			public const string rsInvalidDataSetReferenceField = "rsInvalidDataSetReferenceField";

			// Token: 0x04004AC1 RID: 19137
			public const string rsInvalidDetailDataGrouping = "rsInvalidDetailDataGrouping";

			// Token: 0x04004AC2 RID: 19138
			public const string rsInvalidEmbeddedImage = "rsInvalidEmbeddedImage";

			// Token: 0x04004AC3 RID: 19139
			public const string rsInvalidField = "rsInvalidField";

			// Token: 0x04004AC4 RID: 19140
			public const string rsInvalidFieldNameNotCLSCompliant = "rsInvalidFieldNameNotCLSCompliant";

			// Token: 0x04004AC5 RID: 19141
			public const string rsInvalidFieldNameLength = "rsInvalidFieldNameLength";

			// Token: 0x04004AC6 RID: 19142
			public const string rsInvalidGroupExpressionScope = "rsInvalidGroupExpressionScope";

			// Token: 0x04004AC7 RID: 19143
			public const string rsInvalidGroupingNameNotCLSCompliant = "rsInvalidGroupingNameNotCLSCompliant";

			// Token: 0x04004AC8 RID: 19144
			public const string rsInvalidGroupingNameLength = "rsInvalidGroupingNameLength";

			// Token: 0x04004AC9 RID: 19145
			public const string rsInvalidHideDuplicateScope = "rsInvalidHideDuplicateScope";

			// Token: 0x04004ACA RID: 19146
			public const string rsInvalidURLProtocol = "rsInvalidURLProtocol";

			// Token: 0x04004ACB RID: 19147
			public const string rsInvalidIntegerConstant = "rsInvalidIntegerConstant";

			// Token: 0x04004ACC RID: 19148
			public const string rsInvalidNameNotCLSCompliant = "rsInvalidNameNotCLSCompliant";

			// Token: 0x04004ACD RID: 19149
			public const string rsInvalidNameLength = "rsInvalidNameLength";

			// Token: 0x04004ACE RID: 19150
			public const string rsInvalidNumberOfFilterValues = "rsInvalidNumberOfFilterValues";

			// Token: 0x04004ACF RID: 19151
			public const string rsInvalidFilterValueDataType = "rsInvalidFilterValueDataType";

			// Token: 0x04004AD0 RID: 19152
			public const string rsInvalidParameterNameNotCLSCompliant = "rsInvalidParameterNameNotCLSCompliant";

			// Token: 0x04004AD1 RID: 19153
			public const string rsInvalidParameterNameLength = "rsInvalidParameterNameLength";

			// Token: 0x04004AD2 RID: 19154
			public const string rsInvalidPreviousAggregateInMatrixCell = "rsInvalidPreviousAggregateInMatrixCell";

			// Token: 0x04004AD3 RID: 19155
			public const string rsInvalidRepeatWith = "rsInvalidRepeatWith";

			// Token: 0x04004AD4 RID: 19156
			public const string rsInvalidReportDefinition = "rsInvalidReportDefinition";

			// Token: 0x04004AD5 RID: 19157
			public const string rsInvalidReportParameterDependency = "rsInvalidReportParameterDependency";

			// Token: 0x04004AD6 RID: 19158
			public const string rsInvalidValidValuesDataSetReference = "rsInvalidValidValuesDataSetReference";

			// Token: 0x04004AD7 RID: 19159
			public const string rsInvalidDefaultValueDataSetReference = "rsInvalidDefaultValueDataSetReference";

			// Token: 0x04004AD8 RID: 19160
			public const string rsInvalidRowGrouping = "rsInvalidRowGrouping";

			// Token: 0x04004AD9 RID: 19161
			public const string rsInvalidRunningValueAggregate = "rsInvalidRunningValueAggregate";

			// Token: 0x04004ADA RID: 19162
			public const string rsInvalidScopeInMatrix = "rsInvalidScopeInMatrix";

			// Token: 0x04004ADB RID: 19163
			public const string rsInvalidSeriesGrouping = "rsInvalidSeriesGrouping";

			// Token: 0x04004ADC RID: 19164
			public const string rsInvalidStaticDataGrouping = "rsInvalidStaticDataGrouping";

			// Token: 0x04004ADD RID: 19165
			public const string rsInvalidReportName = "rsInvalidReportName";

			// Token: 0x04004ADE RID: 19166
			public const string rsInvalidReportNameCharacters = "rsInvalidReportNameCharacters";

			// Token: 0x04004ADF RID: 19167
			public const string rsInvalidReportUri = "rsInvalidReportUri";

			// Token: 0x04004AE0 RID: 19168
			public const string rsInvalidToggleItem = "rsInvalidToggleItem";

			// Token: 0x04004AE1 RID: 19169
			public const string rsInvalidValidValues = "rsInvalidValidValues";

			// Token: 0x04004AE2 RID: 19170
			public const string rsInvalidMatrixSubtotalReportItem = "rsInvalidMatrixSubtotalReportItem";

			// Token: 0x04004AE3 RID: 19171
			public const string rsInvalidGroupingParent = "rsInvalidGroupingParent";

			// Token: 0x04004AE4 RID: 19172
			public const string rsInvalidGroupingNaturalGroupFeature = "rsInvalidGroupingNaturalGroupFeature";

			// Token: 0x04004AE5 RID: 19173
			public const string rsInvalidGroupingContainerNotNaturalGroup = "rsInvalidGroupingContainerNotNaturalGroup";

			// Token: 0x04004AE6 RID: 19174
			public const string rsConflictingNaturalGroupRequirements = "rsConflictingNaturalGroupRequirements";

			// Token: 0x04004AE7 RID: 19175
			public const string rsInvalidDefaultRelationshipNotNaturalJoin = "rsInvalidDefaultRelationshipNotNaturalJoin";

			// Token: 0x04004AE8 RID: 19176
			public const string rsInvalidRelationshipGroupingContainerNotNaturalGroup = "rsInvalidRelationshipGroupingContainerNotNaturalGroup";

			// Token: 0x04004AE9 RID: 19177
			public const string rsInvalidRelationshipContainerNotNaturalJoin = "rsInvalidRelationshipContainerNotNaturalJoin";

			// Token: 0x04004AEA RID: 19178
			public const string rsInvalidDefaultRelationshipDuplicateRelatedDataset = "rsInvalidDefaultRelationshipDuplicateRelatedDataset";

			// Token: 0x04004AEB RID: 19179
			public const string rsInvalidDefaultRelationshipCircularReference = "rsInvalidDefaultRelationshipCircularReference";

			// Token: 0x04004AEC RID: 19180
			public const string rsInvalidRelationshipDataSetUsedMoreThanOnce = "rsInvalidRelationshipDataSetUsedMoreThanOnce";

			// Token: 0x04004AED RID: 19181
			public const string rsInvalidRelationshipDataSet = "rsInvalidRelationshipDataSet";

			// Token: 0x04004AEE RID: 19182
			public const string rsInvalidRelationshipTopLevelDataRegion = "rsInvalidRelationshipTopLevelDataRegion";

			// Token: 0x04004AEF RID: 19183
			public const string rsDefaultRelationshipIgnored = "rsDefaultRelationshipIgnored";

			// Token: 0x04004AF0 RID: 19184
			public const string rsInvalidDataSetScopedAggregate = "rsInvalidDataSetScopedAggregate";

			// Token: 0x04004AF1 RID: 19185
			public const string rsInvalidInnerDataSetName = "rsInvalidInnerDataSetName";

			// Token: 0x04004AF2 RID: 19186
			public const string rsInvalidCellDataSetName = "rsInvalidCellDataSetName";

			// Token: 0x04004AF3 RID: 19187
			public const string rsMissingIntersectionDataSetName = "rsMissingIntersectionDataSetName";

			// Token: 0x04004AF4 RID: 19188
			public const string rsMissingIntersectionRelationshipParentScope = "rsMissingIntersectionRelationshipParentScope";

			// Token: 0x04004AF5 RID: 19189
			public const string rsUnexpectedCellDataSetName = "rsUnexpectedCellDataSetName";

			// Token: 0x04004AF6 RID: 19190
			public const string rsInvalidRelationshipDuplicateParentScope = "rsInvalidRelationshipDuplicateParentScope";

			// Token: 0x04004AF7 RID: 19191
			public const string rsInvalidIntersectionNaturalJoin = "rsInvalidIntersectionNaturalJoin";

			// Token: 0x04004AF8 RID: 19192
			public const string rsInvalidNaturalCrossJoin = "rsInvalidNaturalCrossJoin";

			// Token: 0x04004AF9 RID: 19193
			public const string rsInvalidIntersectionNaturalCrossJoin = "rsInvalidIntersectionNaturalCrossJoin";

			// Token: 0x04004AFA RID: 19194
			public const string rsInvalidAggregateIndicatorField = "rsInvalidAggregateIndicatorField";

			// Token: 0x04004AFB RID: 19195
			public const string rsAggregateIndicatorFieldOnCalculatedField = "rsAggregateIndicatorFieldOnCalculatedField";

			// Token: 0x04004AFC RID: 19196
			public const string rsMissingOrInvalidAggregateIndicatorFieldValue = "rsMissingOrInvalidAggregateIndicatorFieldValue";

			// Token: 0x04004AFD RID: 19197
			public const string rsInvalidNaturalSortContainer = "rsInvalidNaturalSortContainer";

			// Token: 0x04004AFE RID: 19198
			public const string rsInvalidDeferredSortContainer = "rsInvalidDeferredSortContainer";

			// Token: 0x04004AFF RID: 19199
			public const string rsInvalidSortingContainerNotNaturalSort = "rsInvalidSortingContainerNotNaturalSort";

			// Token: 0x04004B00 RID: 19200
			public const string rsConflictingNaturalSortRequirements = "rsConflictingNaturalSortRequirements";

			// Token: 0x04004B01 RID: 19201
			public const string rsIncompatibleNaturalSortAndNaturalGroup = "rsIncompatibleNaturalSortAndNaturalGroup";

			// Token: 0x04004B02 RID: 19202
			public const string rsInvalidSortFlagCombination = "rsInvalidSortFlagCombination";

			// Token: 0x04004B03 RID: 19203
			public const string rsConflictingSortFlags = "rsConflictingSortFlags";

			// Token: 0x04004B04 RID: 19204
			public const string rsInvalidSortDirectionMustNotBeSpecified = "rsInvalidSortDirectionMustNotBeSpecified";

			// Token: 0x04004B05 RID: 19205
			public const string rsCantMakeTableGroupHeadersFixed = "rsCantMakeTableGroupHeadersFixed";

			// Token: 0x04004B06 RID: 19206
			public const string rsFixedHeadersInInnerDataRegion = "rsFixedHeadersInInnerDataRegion";

			// Token: 0x04004B07 RID: 19207
			public const string rsInvalidFixedTableColumnHeaderSpacing = "rsInvalidFixedTableColumnHeaderSpacing";

			// Token: 0x04004B08 RID: 19208
			public const string rsQueryCommandTextProcessingError = "rsQueryCommandTextProcessingError";

			// Token: 0x04004B09 RID: 19209
			public const string rsDataSourceConnectStringProcessingError = "rsDataSourceConnectStringProcessingError";

			// Token: 0x04004B0A RID: 19210
			public const string rsReportParameterProcessingError = "rsReportParameterProcessingError";

			// Token: 0x04004B0B RID: 19211
			public const string rsReportParameterQueryProcessingError = "rsReportParameterQueryProcessingError";

			// Token: 0x04004B0C RID: 19212
			public const string rsInvalidMultiValueParameter = "rsInvalidMultiValueParameter";

			// Token: 0x04004B0D RID: 19213
			public const string rsInvalidParameterDefaultValue = "rsInvalidParameterDefaultValue";

			// Token: 0x04004B0E RID: 19214
			public const string rsParameterPropertyTypeMismatch = "rsParameterPropertyTypeMismatch";

			// Token: 0x04004B0F RID: 19215
			public const string rsParameterValueDefinitionMismatch = "rsParameterValueDefinitionMismatch";

			// Token: 0x04004B10 RID: 19216
			public const string rsParameterValueNullOrBlank = "rsParameterValueNullOrBlank";

			// Token: 0x04004B11 RID: 19217
			public const string rsLabelExpressionOnChartScalarAxisIsIgnored = "rsLabelExpressionOnChartScalarAxisIsIgnored";

			// Token: 0x04004B12 RID: 19218
			public const string rsLineChartMightScatter = "rsLineChartMightScatter";

			// Token: 0x04004B13 RID: 19219
			public const string rsMissingAggregateScope = "rsMissingAggregateScope";

			// Token: 0x04004B14 RID: 19220
			public const string rsMissingAggregateScopeInPageSection = "rsMissingAggregateScopeInPageSection";

			// Token: 0x04004B15 RID: 19221
			public const string rsReportItemInScopedAggregate = "rsReportItemInScopedAggregate";

			// Token: 0x04004B16 RID: 19222
			public const string rsMissingChartDataPoints = "rsMissingChartDataPoints";

			// Token: 0x04004B17 RID: 19223
			public const string rsMissingChartDataValueName = "rsMissingChartDataValueName";

			// Token: 0x04004B18 RID: 19224
			public const string rsMissingCustomPropertyName = "rsMissingCustomPropertyName";

			// Token: 0x04004B19 RID: 19225
			public const string rsInvalidDataSetName = "rsInvalidDataSetName";

			// Token: 0x04004B1A RID: 19226
			public const string rsMissingDataSetName = "rsMissingDataSetName";

			// Token: 0x04004B1B RID: 19227
			public const string rsMissingMIMEType = "rsMissingMIMEType";

			// Token: 0x04004B1C RID: 19228
			public const string rsMissingParameterDefault = "rsMissingParameterDefault";

			// Token: 0x04004B1D RID: 19229
			public const string rsMultipleGroupExpressionsOnChartScalarAxis = "rsMultipleGroupExpressionsOnChartScalarAxis";

			// Token: 0x04004B1E RID: 19230
			public const string rsMultipleGroupingsOnChartScalarAxis = "rsMultipleGroupingsOnChartScalarAxis";

			// Token: 0x04004B1F RID: 19231
			public const string rsMultiReportItemsInMatrixSection = "rsMultiReportItemsInMatrixSection";

			// Token: 0x04004B20 RID: 19232
			public const string rsMultiReportItemsInTableCell = "rsMultiReportItemsInTableCell";

			// Token: 0x04004B21 RID: 19233
			public const string rsMultiReportItemsInPageSectionExpression = "rsMultiReportItemsInPageSectionExpression";

			// Token: 0x04004B22 RID: 19234
			public const string rsMultiReportItemsInCustomReportItem = "rsMultiReportItemsInCustomReportItem";

			// Token: 0x04004B23 RID: 19235
			public const string rsMultiStaticColumnsOrRows = "rsMultiStaticColumnsOrRows";

			// Token: 0x04004B24 RID: 19236
			public const string rsMultiStaticCategoriesOrSeries = "rsMultiStaticCategoriesOrSeries";

			// Token: 0x04004B25 RID: 19237
			public const string rsNegativeLeftWidth = "rsNegativeLeftWidth";

			// Token: 0x04004B26 RID: 19238
			public const string rsNegativeTopHeight = "rsNegativeTopHeight";

			// Token: 0x04004B27 RID: 19239
			public const string rsNonAggregateInMatrixCell = "rsNonAggregateInMatrixCell";

			// Token: 0x04004B28 RID: 19240
			public const string rsNotAReportDefinition = "rsNotAReportDefinition";

			// Token: 0x04004B29 RID: 19241
			public const string rsNotACurrentReportDefinition = "rsNotACurrentReportDefinition";

			// Token: 0x04004B2A RID: 19242
			public const string rsOverlappingReportItems = "rsOverlappingReportItems";

			// Token: 0x04004B2B RID: 19243
			public const string rsReportItemOutsideContainer = "rsReportItemOutsideContainer";

			// Token: 0x04004B2C RID: 19244
			public const string rsParameterInReportParameterExpression = "rsParameterInReportParameterExpression";

			// Token: 0x04004B2D RID: 19245
			public const string rsPageBreakOnMatrixColumnGroup = "rsPageBreakOnMatrixColumnGroup";

			// Token: 0x04004B2E RID: 19246
			public const string rsPageBreakOnChartGroup = "rsPageBreakOnChartGroup";

			// Token: 0x04004B2F RID: 19247
			public const string rsPageBreakOnGaugeGroup = "rsPageBreakOnGaugeGroup";

			// Token: 0x04004B30 RID: 19248
			public const string rsPageBreakOnMapGroup = "rsPageBreakOnMapGroup";

			// Token: 0x04004B31 RID: 19249
			public const string rsPreviousAggregateInFilterExpression = "rsPreviousAggregateInFilterExpression";

			// Token: 0x04004B32 RID: 19250
			public const string rsPreviousAggregateInGroupExpression = "rsPreviousAggregateInGroupExpression";

			// Token: 0x04004B33 RID: 19251
			public const string rsPreviousAggregateInPageSectionExpression = "rsPreviousAggregateInPageSectionExpression";

			// Token: 0x04004B34 RID: 19252
			public const string rsPreviousAggregateInQueryParameterExpression = "rsPreviousAggregateInQueryParameterExpression";

			// Token: 0x04004B35 RID: 19253
			public const string rsPreviousAggregateInReportParameterExpression = "rsPreviousAggregateInReportParameterExpression";

			// Token: 0x04004B36 RID: 19254
			public const string rsPreviousAggregateInSortExpression = "rsPreviousAggregateInSortExpression";

			// Token: 0x04004B37 RID: 19255
			public const string rsPreviousAggregateInReportLanguageExpression = "rsPreviousAggregateInReportLanguageExpression";

			// Token: 0x04004B38 RID: 19256
			public const string rsPreviousAggregateInVariableExpression = "rsPreviousAggregateInVariableExpression";

			// Token: 0x04004B39 RID: 19257
			public const string rsPreviousAggregateInJoinExpression = "rsPreviousAggregateInJoinExpression";

			// Token: 0x04004B3A RID: 19258
			public const string rsRepeatWithNotPeerDataRegion = "rsRepeatWithNotPeerDataRegion";

			// Token: 0x04004B3B RID: 19259
			public const string rsReportItemInFilterExpression = "rsReportItemInFilterExpression";

			// Token: 0x04004B3C RID: 19260
			public const string rsReportItemInGroupExpression = "rsReportItemInGroupExpression";

			// Token: 0x04004B3D RID: 19261
			public const string rsReportItemInQueryParameterExpression = "rsReportItemInQueryParameterExpression";

			// Token: 0x04004B3E RID: 19262
			public const string rsReportItemInReportParameterExpression = "rsReportItemInReportParameterExpression";

			// Token: 0x04004B3F RID: 19263
			public const string rsReportItemInSortExpression = "rsReportItemInSortExpression";

			// Token: 0x04004B40 RID: 19264
			public const string rsReportItemInReportLanguageExpression = "rsReportItemInReportLanguageExpression";

			// Token: 0x04004B41 RID: 19265
			public const string rsReportItemInVariableExpression = "rsReportItemInVariableExpression";

			// Token: 0x04004B42 RID: 19266
			public const string rsReportItemInJoinExpression = "rsReportItemInJoinExpression";

			// Token: 0x04004B43 RID: 19267
			public const string rsRowNumberInFilterExpression = "rsRowNumberInFilterExpression";

			// Token: 0x04004B44 RID: 19268
			public const string rsRowNumberInPageSectionExpression = "rsRowNumberInPageSectionExpression";

			// Token: 0x04004B45 RID: 19269
			public const string rsRowNumberInQueryParameterExpression = "rsRowNumberInQueryParameterExpression";

			// Token: 0x04004B46 RID: 19270
			public const string rsRowNumberInReportParameterExpression = "rsRowNumberInReportParameterExpression";

			// Token: 0x04004B47 RID: 19271
			public const string rsRowNumberInSortExpression = "rsRowNumberInSortExpression";

			// Token: 0x04004B48 RID: 19272
			public const string rsRowNumberInReportLanguageExpression = "rsRowNumberInReportLanguageExpression";

			// Token: 0x04004B49 RID: 19273
			public const string rsRowNumberInVariableExpression = "rsRowNumberInVariableExpression";

			// Token: 0x04004B4A RID: 19274
			public const string rsRunningValueInFilterExpression = "rsRunningValueInFilterExpression";

			// Token: 0x04004B4B RID: 19275
			public const string rsRunningValueInGroupExpression = "rsRunningValueInGroupExpression";

			// Token: 0x04004B4C RID: 19276
			public const string rsRunningValueInPageSectionExpression = "rsRunningValueInPageSectionExpression";

			// Token: 0x04004B4D RID: 19277
			public const string rsRunningValueInQueryParameterExpression = "rsRunningValueInQueryParameterExpression";

			// Token: 0x04004B4E RID: 19278
			public const string rsRunningValueInReportParameterExpression = "rsRunningValueInReportParameterExpression";

			// Token: 0x04004B4F RID: 19279
			public const string rsRunningValueInSortExpression = "rsRunningValueInSortExpression";

			// Token: 0x04004B50 RID: 19280
			public const string rsRunningValueInReportLanguageExpression = "rsRunningValueInReportLanguageExpression";

			// Token: 0x04004B51 RID: 19281
			public const string rsRunningValueInVariableExpression = "rsRunningValueInVariableExpression";

			// Token: 0x04004B52 RID: 19282
			public const string rsRunningValueInJoinExpression = "rsRunningValueInJoinExpression";

			// Token: 0x04004B53 RID: 19283
			public const string rsScopeInPageSectionExpression = "rsScopeInPageSectionExpression";

			// Token: 0x04004B54 RID: 19284
			public const string rsStaticGroupingOnChartScalarAxis = "rsStaticGroupingOnChartScalarAxis";

			// Token: 0x04004B55 RID: 19285
			public const string rsToggleInPageSection = "rsToggleInPageSection";

			// Token: 0x04004B56 RID: 19286
			public const string rsUnsortedCategoryInAreaChart = "rsUnsortedCategoryInAreaChart";

			// Token: 0x04004B57 RID: 19287
			public const string rsWrongNumberOfMatrixCells = "rsWrongNumberOfMatrixCells";

			// Token: 0x04004B58 RID: 19288
			public const string rsWrongNumberOfMatrixColumns = "rsWrongNumberOfMatrixColumns";

			// Token: 0x04004B59 RID: 19289
			public const string rsWrongNumberOfMatrixRows = "rsWrongNumberOfMatrixRows";

			// Token: 0x04004B5A RID: 19290
			public const string rsWrongNumberOfChartDataPoints = "rsWrongNumberOfChartDataPoints";

			// Token: 0x04004B5B RID: 19291
			public const string rsWrongNumberOfChartSeries = "rsWrongNumberOfChartSeries";

			// Token: 0x04004B5C RID: 19292
			public const string rsWrongNumberOfChartDataPointsInSeries = "rsWrongNumberOfChartDataPointsInSeries";

			// Token: 0x04004B5D RID: 19293
			public const string rsWrongNumberOfDataValues = "rsWrongNumberOfDataValues";

			// Token: 0x04004B5E RID: 19294
			public const string rsWrongNumberOfParameters = "rsWrongNumberOfParameters";

			// Token: 0x04004B5F RID: 19295
			public const string rsWrongNumberOfTableCells = "rsWrongNumberOfTableCells";

			// Token: 0x04004B60 RID: 19296
			public const string rsSingleHierarchyWithDataRows = "rsSingleHierarchyWithDataRows";

			// Token: 0x04004B61 RID: 19297
			public const string rsInvalidRecursiveAggregate = "rsInvalidRecursiveAggregate";

			// Token: 0x04004B62 RID: 19298
			public const string rsInvalidNestedRecursiveAggregate = "rsInvalidNestedRecursiveAggregate";

			// Token: 0x04004B63 RID: 19299
			public const string rsRecursiveAggregateOfAggregate = "rsRecursiveAggregateOfAggregate";

			// Token: 0x04004B64 RID: 19300
			public const string rsInvalidAggregateRecursiveFlag = "rsInvalidAggregateRecursiveFlag";

			// Token: 0x04004B65 RID: 19301
			public const string rsPostSortAggregateInGroupFilterExpression = "rsPostSortAggregateInGroupFilterExpression";

			// Token: 0x04004B66 RID: 19302
			public const string rsPostSortAggregateInSortExpression = "rsPostSortAggregateInSortExpression";

			// Token: 0x04004B67 RID: 19303
			public const string rsPostSortAggregateInVariableExpression = "rsPostSortAggregateInVariableExpression";

			// Token: 0x04004B68 RID: 19304
			public const string rsBookmarkInPageSection = "rsBookmarkInPageSection";

			// Token: 0x04004B69 RID: 19305
			public const string rsUnsupportedProtocol = "rsUnsupportedProtocol";

			// Token: 0x04004B6A RID: 19306
			public const string rsInvalidVariableCount = "rsInvalidVariableCount";

			// Token: 0x04004B6B RID: 19307
			public const string rsElementMustContainChildren = "rsElementMustContainChildren";

			// Token: 0x04004B6C RID: 19308
			public const string rsElementMustContainChild = "rsElementMustContainChild";

			// Token: 0x04004B6D RID: 19309
			public const string rsInvalidWritableVariable = "rsInvalidWritableVariable";

			// Token: 0x04004B6E RID: 19310
			public const string rsMissingExpression = "rsMissingExpression";

			// Token: 0x04004B6F RID: 19311
			public const string rsInvalidActionsCount = "rsInvalidActionsCount";

			// Token: 0x04004B70 RID: 19312
			public const string rsInvalidMeDotValueInExpression = "rsInvalidMeDotValueInExpression";

			// Token: 0x04004B71 RID: 19313
			public const string rsPostSortAggregateInAggregateExpression = "rsPostSortAggregateInAggregateExpression";

			// Token: 0x04004B72 RID: 19314
			public const string rsRunningValueInAggregateExpression = "rsRunningValueInAggregateExpression";

			// Token: 0x04004B73 RID: 19315
			public const string rsPreviousInAggregateExpression = "rsPreviousInAggregateExpression";

			// Token: 0x04004B74 RID: 19316
			public const string rsNestedAggregateViaLookup = "rsNestedAggregateViaLookup";

			// Token: 0x04004B75 RID: 19317
			public const string rsNestedAggregateInPageSection = "rsNestedAggregateInPageSection";

			// Token: 0x04004B76 RID: 19318
			public const string rsNestedAggregateInFilterExpression = "rsNestedAggregateInFilterExpression";

			// Token: 0x04004B77 RID: 19319
			public const string rsNestedAggregateInGroupVariable = "rsNestedAggregateInGroupVariable";

			// Token: 0x04004B78 RID: 19320
			public const string rsWrongNumberOfTablixCornerRows = "rsWrongNumberOfTablixCornerRows";

			// Token: 0x04004B79 RID: 19321
			public const string rsWrongNumberOfTablixCornerCells = "rsWrongNumberOfTablixCornerCells";

			// Token: 0x04004B7A RID: 19322
			public const string rsWrongNumberOfTablixColumns = "rsWrongNumberOfTablixColumns";

			// Token: 0x04004B7B RID: 19323
			public const string rsWrongNumberOfTablixCells = "rsWrongNumberOfTablixCells";

			// Token: 0x04004B7C RID: 19324
			public const string rsWrongNumberOfTablixRows = "rsWrongNumberOfTablixRows";

			// Token: 0x04004B7D RID: 19325
			public const string rsInvalidTablixCornerCellSpan = "rsInvalidTablixCornerCellSpan";

			// Token: 0x04004B7E RID: 19326
			public const string rsInvalidTablixCellCellSpan = "rsInvalidTablixCellCellSpan";

			// Token: 0x04004B7F RID: 19327
			public const string rsInvalidTablixCornerColumnSpans = "rsInvalidTablixCornerColumnSpans";

			// Token: 0x04004B80 RID: 19328
			public const string rsInvalidTablixCornerRowSpans = "rsInvalidTablixCornerRowSpans";

			// Token: 0x04004B81 RID: 19329
			public const string rsHiddenTablixCornerCellContents = "rsHiddenTablixCornerCellContents";

			// Token: 0x04004B82 RID: 19330
			public const string rsInvalidSortNotAllowed = "rsInvalidSortNotAllowed";

			// Token: 0x04004B83 RID: 19331
			public const string rsInvalidFixedHeaderOnOppositeHierarchy = "rsInvalidFixedHeaderOnOppositeHierarchy";

			// Token: 0x04004B84 RID: 19332
			public const string rsInvalidFixedDataColumnPosition = "rsInvalidFixedDataColumnPosition";

			// Token: 0x04004B85 RID: 19333
			public const string rsInvalidFixedDataRowPosition = "rsInvalidFixedDataRowPosition";

			// Token: 0x04004B86 RID: 19334
			public const string rsInvalidFixedDataNotContiguous = "rsInvalidFixedDataNotContiguous";

			// Token: 0x04004B87 RID: 19335
			public const string rsInvalidFixedDataInHierarchy = "rsInvalidFixedDataInHierarchy";

			// Token: 0x04004B88 RID: 19336
			public const string rsInvalidFixedDataBodyCellSpans = "rsInvalidFixedDataBodyCellSpans";

			// Token: 0x04004B89 RID: 19337
			public const string rsInvalidGroupAncestorIsDetail = "rsInvalidGroupAncestorIsDetail";

			// Token: 0x04004B8A RID: 19338
			public const string rsInvalidKeepWithGroupOnDynamicTablixMember = "rsInvalidKeepWithGroupOnDynamicTablixMember";

			// Token: 0x04004B8B RID: 19339
			public const string rsInvalidKeepWithGroup = "rsInvalidKeepWithGroup";

			// Token: 0x04004B8C RID: 19340
			public const string rsInvalidKeepWithGroupOnColumnTablixMember = "rsInvalidKeepWithGroupOnColumnTablixMember";

			// Token: 0x04004B8D RID: 19341
			public const string rsInvalidRepeatOnNewPageOnColumnTablixMember = "rsInvalidRepeatOnNewPageOnColumnTablixMember";

			// Token: 0x04004B8E RID: 19342
			public const string rsInvalidRepeatOnNewPage = "rsInvalidRepeatOnNewPage";

			// Token: 0x04004B8F RID: 19343
			public const string rsInvalidTablixCellColSpans = "rsInvalidTablixCellColSpans";

			// Token: 0x04004B90 RID: 19344
			public const string rsInvalidTablixCellColSpan = "rsInvalidTablixCellColSpan";

			// Token: 0x04004B91 RID: 19345
			public const string rsInvalidTablixCellRowSpan = "rsInvalidTablixCellRowSpan";

			// Token: 0x04004B92 RID: 19346
			public const string rsInvalidTablixHeaderColSpan = "rsInvalidTablixHeaderColSpan";

			// Token: 0x04004B93 RID: 19347
			public const string rsInvalidTablixHeaderRowSpan = "rsInvalidTablixHeaderRowSpan";

			// Token: 0x04004B94 RID: 19348
			public const string rsCellContentsNotOmitted = "rsCellContentsNotOmitted";

			// Token: 0x04004B95 RID: 19349
			public const string rsCellContentsRequired = "rsCellContentsRequired";

			// Token: 0x04004B96 RID: 19350
			public const string rsInconsistentNumberofCellsInRow = "rsInconsistentNumberofCellsInRow";

			// Token: 0x04004B97 RID: 19351
			public const string rsInvalidTablixHeaderSize = "rsInvalidTablixHeaderSize";

			// Token: 0x04004B98 RID: 19352
			public const string rsInvalidPreviousAggregateInTablixCell = "rsInvalidPreviousAggregateInTablixCell";

			// Token: 0x04004B99 RID: 19353
			public const string rsInvalidScopeInTablix = "rsInvalidScopeInTablix";

			// Token: 0x04004B9A RID: 19354
			public const string rsInvalidTablixHeaders = "rsInvalidTablixHeaders";

			// Token: 0x04004B9B RID: 19355
			public const string rsNonAggregateInTablixCell = "rsNonAggregateInTablixCell";

			// Token: 0x04004B9C RID: 19356
			public const string rsInvalidChartAxisNameNotCLSCompliant = "rsInvalidChartAxisNameNotCLSCompliant";

			// Token: 0x04004B9D RID: 19357
			public const string rsInvalidChartAxisNameLength = "rsInvalidChartAxisNameLength";

			// Token: 0x04004B9E RID: 19358
			public const string rsSpecifiedNonValueAxisName = "rsSpecifiedNonValueAxisName";

			// Token: 0x04004B9F RID: 19359
			public const string rsValueAxisNameNotFound = "rsValueAxisNameNotFound";

			// Token: 0x04004BA0 RID: 19360
			public const string rsDuplicateVariableName = "rsDuplicateVariableName";

			// Token: 0x04004BA1 RID: 19361
			public const string rsInvalidVariableNameNotCLSCompliant = "rsInvalidVariableNameNotCLSCompliant";

			// Token: 0x04004BA2 RID: 19362
			public const string rsInvalidVariableNameLength = "rsInvalidVariableNameLength";

			// Token: 0x04004BA3 RID: 19363
			public const string rsDuplicateGroupingVariableName = "rsDuplicateGroupingVariableName";

			// Token: 0x04004BA4 RID: 19364
			public const string rsInvalidGroupingVariableNameNotCLSCompliant = "rsInvalidGroupingVariableNameNotCLSCompliant";

			// Token: 0x04004BA5 RID: 19365
			public const string rsInvalidGroupingVariableNameLength = "rsInvalidGroupingVariableNameLength";

			// Token: 0x04004BA6 RID: 19366
			public const string rsInvalidVariableReference = "rsInvalidVariableReference";

			// Token: 0x04004BA7 RID: 19367
			public const string rsInvalidReportItemInPageSection = "rsInvalidReportItemInPageSection";

			// Token: 0x04004BA8 RID: 19368
			public const string rsInvalidTargetScope = "rsInvalidTargetScope";

			// Token: 0x04004BA9 RID: 19369
			public const string rsInvalidOmittedTargetScope = "rsInvalidOmittedTargetScope";

			// Token: 0x04004BAA RID: 19370
			public const string rsInvalidOmittedExpressionScope = "rsInvalidOmittedExpressionScope";

			// Token: 0x04004BAB RID: 19371
			public const string rsInvalidExpressionScope = "rsInvalidExpressionScope";

			// Token: 0x04004BAC RID: 19372
			public const string rsInvalidTextboxInPageSection = "rsInvalidTextboxInPageSection";

			// Token: 0x04004BAD RID: 19373
			public const string rsNonExistingScope = "rsNonExistingScope";

			// Token: 0x04004BAE RID: 19374
			public const string rsInvalidExpressionScopeDataSet = "rsInvalidExpressionScopeDataSet";

			// Token: 0x04004BAF RID: 19375
			public const string rsInvalidSortExpressionScope = "rsInvalidSortExpressionScope";

			// Token: 0x04004BB0 RID: 19376
			public const string rsIneffectiveSortExpressionScope = "rsIneffectiveSortExpressionScope";

			// Token: 0x04004BB1 RID: 19377
			public const string rsMissingDataGrouping = "rsMissingDataGrouping";

			// Token: 0x04004BB2 RID: 19378
			public const string rsWrongNumberOfDataRows = "rsWrongNumberOfDataRows";

			// Token: 0x04004BB3 RID: 19379
			public const string rsWrongNumberOfDataCellsInDataRow = "rsWrongNumberOfDataCellsInDataRow";

			// Token: 0x04004BB4 RID: 19380
			public const string rsMissingDataGroupings = "rsMissingDataGroupings";

			// Token: 0x04004BB5 RID: 19381
			public const string rsMissingDataCells = "rsMissingDataCells";

			// Token: 0x04004BB6 RID: 19382
			public const string rsCRIMultiStaticColumnsOrRows = "rsCRIMultiStaticColumnsOrRows";

			// Token: 0x04004BB7 RID: 19383
			public const string rsCRIStaticWithSubgroups = "rsCRIStaticWithSubgroups";

			// Token: 0x04004BB8 RID: 19384
			public const string rsCRIMultiNonStaticGroups = "rsCRIMultiNonStaticGroups";

			// Token: 0x04004BB9 RID: 19385
			public const string rsCRISubtotalNotSupported = "rsCRISubtotalNotSupported";

			// Token: 0x04004BBA RID: 19386
			public const string rsInvalidGrouping = "rsInvalidGrouping";

			// Token: 0x04004BBB RID: 19387
			public const string rsCRIInPageSection = "rsCRIInPageSection";

			// Token: 0x04004BBC RID: 19388
			public const string rsMapPropertyAlreadyDefined = "rsMapPropertyAlreadyDefined";

			// Token: 0x04004BBD RID: 19389
			public const string rsInvalidRowMapMemberCannotBeDynamic = "rsInvalidRowMapMemberCannotBeDynamic";

			// Token: 0x04004BBE RID: 19390
			public const string rsInvalidRowMapMemberCannotContainChildMember = "rsInvalidRowMapMemberCannotContainChildMember";

			// Token: 0x04004BBF RID: 19391
			public const string rsInvalidColumnMapMemberCannotContainMultipleChildMember = "rsInvalidColumnMapMemberCannotContainMultipleChildMember";

			// Token: 0x04004BC0 RID: 19392
			public const string rsCRIRenderItemNull = "rsCRIRenderItemNull";

			// Token: 0x04004BC1 RID: 19393
			public const string rsCRIRenderInstanceNull = "rsCRIRenderInstanceNull";

			// Token: 0x04004BC2 RID: 19394
			public const string rsCRIRenderItemInstanceType = "rsCRIRenderItemInstanceType";

			// Token: 0x04004BC3 RID: 19395
			public const string rsCRIRenderItemDefinitionName = "rsCRIRenderItemDefinitionName";

			// Token: 0x04004BC4 RID: 19396
			public const string rsCRIRenderItemProperties = "rsCRIRenderItemProperties";

			// Token: 0x04004BC5 RID: 19397
			public const string rsCRIRenderItemDuplicateStyle = "rsCRIRenderItemDuplicateStyle";

			// Token: 0x04004BC6 RID: 19398
			public const string rsCRIRenderItemInvalidStyleType = "rsCRIRenderItemInvalidStyleType";

			// Token: 0x04004BC7 RID: 19399
			public const string rsCRIRenderItemInvalidStyle = "rsCRIRenderItemInvalidStyle";

			// Token: 0x04004BC8 RID: 19400
			public const string rsCRIControlFailedToLoad = "rsCRIControlFailedToLoad";

			// Token: 0x04004BC9 RID: 19401
			public const string rsCRIControlNotInstalled = "rsCRIControlNotInstalled";

			// Token: 0x04004BCA RID: 19402
			public const string rsSandboxingCustomCodeNotAllowed = "rsSandboxingCustomCodeNotAllowed";

			// Token: 0x04004BCB RID: 19403
			public const string rsSandboxingExpressionExceedsMaximumLength = "rsSandboxingExpressionExceedsMaximumLength";

			// Token: 0x04004BCC RID: 19404
			public const string rsSandboxingStringResultExceedsMaximumLength = "rsSandboxingStringResultExceedsMaximumLength";

			// Token: 0x04004BCD RID: 19405
			public const string rsSandboxingArrayResultExceedsMaximumLength = "rsSandboxingArrayResultExceedsMaximumLength";

			// Token: 0x04004BCE RID: 19406
			public const string rsSandboxingInvalidExpression = "rsSandboxingInvalidExpression";

			// Token: 0x04004BCF RID: 19407
			public const string rsSandboxingInvalidTypeOrMemberName = "rsSandboxingInvalidTypeOrMemberName";

			// Token: 0x04004BD0 RID: 19408
			public const string rsSandboxingInvalidNewType = "rsSandboxingInvalidNewType";

			// Token: 0x04004BD1 RID: 19409
			public const string rsSandboxingInvalidClassName = "rsSandboxingInvalidClassName";

			// Token: 0x04004BD2 RID: 19410
			public const string rsSandboxingInvalidCodeModule = "rsSandboxingInvalidCodeModule";

			// Token: 0x04004BD3 RID: 19411
			public const string rsSandboxingCodeModuleUnavailableMode = "rsSandboxingCodeModuleUnavailableMode";

			// Token: 0x04004BD4 RID: 19412
			public const string rsSandboxingExternalResourceExceedsMaximumSize = "rsSandboxingExternalResourceExceedsMaximumSize";

			// Token: 0x04004BD5 RID: 19413
			public const string rsAggregateOfMixedDataTypes = "rsAggregateOfMixedDataTypes";

			// Token: 0x04004BD6 RID: 19414
			public const string rsAggregateOfNonNumericData = "rsAggregateOfNonNumericData";

			// Token: 0x04004BD7 RID: 19415
			public const string rsAggregateOfInvalidExpressionDataType = "rsAggregateOfInvalidExpressionDataType";

			// Token: 0x04004BD8 RID: 19416
			public const string rsLookupOfInvalidExpressionDataType = "rsLookupOfInvalidExpressionDataType";

			// Token: 0x04004BD9 RID: 19417
			public const string rsCyclicExpression = "rsCyclicExpression";

			// Token: 0x04004BDA RID: 19418
			public const string rsCyclicExpressionInReportVariable = "rsCyclicExpressionInReportVariable";

			// Token: 0x04004BDB RID: 19419
			public const string rsCyclicExpressionInGroupVariable = "rsCyclicExpressionInGroupVariable";

			// Token: 0x04004BDC RID: 19420
			public const string rsErrorExecutingSubreport = "rsErrorExecutingSubreport";

			// Token: 0x04004BDD RID: 19421
			public const string rsInvalidExpressionDataType = "rsInvalidExpressionDataType";

			// Token: 0x04004BDE RID: 19422
			public const string rsFieldErrorInExpression = "rsFieldErrorInExpression";

			// Token: 0x04004BDF RID: 19423
			public const string rsInvalidValidValueList = "rsInvalidValidValueList";

			// Token: 0x04004BE0 RID: 19424
			public const string rsMinMaxOfNonSortableData = "rsMinMaxOfNonSortableData";

			// Token: 0x04004BE1 RID: 19425
			public const string rsRuntimeErrorInExpression = "rsRuntimeErrorInExpression";

			// Token: 0x04004BE2 RID: 19426
			public const string NonClsCompliantException = "NonClsCompliantException";

			// Token: 0x04004BE3 RID: 19427
			public const string rsRuntimeUserProfileDependency = "rsRuntimeUserProfileDependency";

			// Token: 0x04004BE4 RID: 19428
			public const string rsMissingFieldInDataSet = "rsMissingFieldInDataSet";

			// Token: 0x04004BE5 RID: 19429
			public const string rsDataSetFieldTypeNotSupported = "rsDataSetFieldTypeNotSupported";

			// Token: 0x04004BE6 RID: 19430
			public const string rsErrorReadingDataSetField = "rsErrorReadingDataSetField";

			// Token: 0x04004BE7 RID: 19431
			public const string rsErrorReadingFieldProperty = "rsErrorReadingFieldProperty";

			// Token: 0x04004BE8 RID: 19432
			public const string rsUnexpectedErrorInExpression = "rsUnexpectedErrorInExpression";

			// Token: 0x04004BE9 RID: 19433
			public const string rsWarningExecutingSubreport = "rsWarningExecutingSubreport";

			// Token: 0x04004BEA RID: 19434
			public const string rsWarningFetchingExternalImages = "rsWarningFetchingExternalImages";

			// Token: 0x04004BEB RID: 19435
			public const string rsInvalidImageReference = "rsInvalidImageReference";

			// Token: 0x04004BEC RID: 19436
			public const string rsExternalImageLoadingDisabled = "rsExternalImageLoadingDisabled";

			// Token: 0x04004BED RID: 19437
			public const string rsInvalidDatabaseImage = "rsInvalidDatabaseImage";

			// Token: 0x04004BEE RID: 19438
			public const string rsComparisonError = "rsComparisonError";

			// Token: 0x04004BEF RID: 19439
			public const string rsComparisonTypeError = "rsComparisonTypeError";

			// Token: 0x04004BF0 RID: 19440
			public const string rsCollationDetectionFailed = "rsCollationDetectionFailed";

			// Token: 0x04004BF1 RID: 19441
			public const string rsErrorLoadingExprHostAssembly = "rsErrorLoadingExprHostAssembly";

			// Token: 0x04004BF2 RID: 19442
			public const string rsErrorInOnInit = "rsErrorInOnInit";

			// Token: 0x04004BF3 RID: 19443
			public const string rsUntrustedCodeModule = "rsUntrustedCodeModule";

			// Token: 0x04004BF4 RID: 19444
			public const string rsInvalidSortItemID = "rsInvalidSortItemID";

			// Token: 0x04004BF5 RID: 19445
			public const string rsExceededMaxRecursionLevel = "rsExceededMaxRecursionLevel";

			// Token: 0x04004BF6 RID: 19446
			public const string rsEngineMismatchSubReport = "rsEngineMismatchSubReport";

			// Token: 0x04004BF7 RID: 19447
			public const string rsEngineMismatchParentReport = "rsEngineMismatchParentReport";

			// Token: 0x04004BF8 RID: 19448
			public const string rsMissingSubReport = "rsMissingSubReport";

			// Token: 0x04004BF9 RID: 19449
			public const string rsSubReportDataRetrievalFailed = "rsSubReportDataRetrievalFailed";

			// Token: 0x04004BFA RID: 19450
			public const string rsSubReportDataNotRetrieved = "rsSubReportDataNotRetrieved";

			// Token: 0x04004BFB RID: 19451
			public const string rsSubReportParametersNotSpecified = "rsSubReportParametersNotSpecified";

			// Token: 0x04004BFC RID: 19452
			public const string rsInvalidRichTextParseFailed = "rsInvalidRichTextParseFailed";

			// Token: 0x04004BFD RID: 19453
			public const string rsInvalidCollationCultureName = "rsInvalidCollationCultureName";

			// Token: 0x04004BFE RID: 19454
			public const string rsParseErrorInvalidSize = "rsParseErrorInvalidSize";

			// Token: 0x04004BFF RID: 19455
			public const string rsParseErrorInvalidValue = "rsParseErrorInvalidValue";

			// Token: 0x04004C00 RID: 19456
			public const string rsParseErrorInvalidColor = "rsParseErrorInvalidColor";

			// Token: 0x04004C01 RID: 19457
			public const string rsParseErrorOutOfRangeSize = "rsParseErrorOutOfRangeSize";

			// Token: 0x04004C02 RID: 19458
			public const string rsInvalidEmptyImageReference = "rsInvalidEmptyImageReference";

			// Token: 0x04004C03 RID: 19459
			public const string rsFieldReference = "rsFieldReference";

			// Token: 0x04004C04 RID: 19460
			public const string rsFieldReferenceAmbiguous = "rsFieldReferenceAmbiguous";

			// Token: 0x04004C05 RID: 19461
			public const string rsInvalidBackgroundRepeat = "rsInvalidBackgroundRepeat";

			// Token: 0x04004C06 RID: 19462
			public const string rsInvalidBackgroundGradientType = "rsInvalidBackgroundGradientType";

			// Token: 0x04004C07 RID: 19463
			public const string rsInvalidBorderStyle = "rsInvalidBorderStyle";

			// Token: 0x04004C08 RID: 19464
			public const string rsInvalidCalendar = "rsInvalidCalendar";

			// Token: 0x04004C09 RID: 19465
			public const string rsInvalidCalendarForLanguage = "rsInvalidCalendarForLanguage";

			// Token: 0x04004C0A RID: 19466
			public const string rsInvalidColor = "rsInvalidColor";

			// Token: 0x04004C0B RID: 19467
			public const string rsInvalidDirection = "rsInvalidDirection";

			// Token: 0x04004C0C RID: 19468
			public const string rsInvalidDatabaseImageProperty = "rsInvalidDatabaseImageProperty";

			// Token: 0x04004C0D RID: 19469
			public const string rsInvalidEmbeddedImageProperty = "rsInvalidEmbeddedImageProperty";

			// Token: 0x04004C0E RID: 19470
			public const string rsInvalidExternalImageProperty = "rsInvalidExternalImageProperty";

			// Token: 0x04004C0F RID: 19471
			public const string rsInvalidEmbeddingModeImageProperty = "rsInvalidEmbeddingModeImageProperty";

			// Token: 0x04004C10 RID: 19472
			public const string rsInvalidFontStyle = "rsInvalidFontStyle";

			// Token: 0x04004C11 RID: 19473
			public const string rsInvalidFontWeight = "rsInvalidFontWeight";

			// Token: 0x04004C12 RID: 19474
			public const string rsInvalidFormatString = "rsInvalidFormatString";

			// Token: 0x04004C13 RID: 19475
			public const string rsInvalidLanguage = "rsInvalidLanguage";

			// Token: 0x04004C14 RID: 19476
			public const string rsInvalidMeasurementUnit = "rsInvalidMeasurementUnit";

			// Token: 0x04004C15 RID: 19477
			public const string rsInvalidMIMEType = "rsInvalidMIMEType";

			// Token: 0x04004C16 RID: 19478
			public const string rsInvalidNumeralVariant = "rsInvalidNumeralVariant";

			// Token: 0x04004C17 RID: 19479
			public const string rsInvalidNumeralVariantForLanguage = "rsInvalidNumeralVariantForLanguage";

			// Token: 0x04004C18 RID: 19480
			public const string rsInvalidSize = "rsInvalidSize";

			// Token: 0x04004C19 RID: 19481
			public const string rsInvalidTextAlign = "rsInvalidTextAlign";

			// Token: 0x04004C1A RID: 19482
			public const string rsInvalidTextDecoration = "rsInvalidTextDecoration";

			// Token: 0x04004C1B RID: 19483
			public const string rsInvalidUnicodeBiDi = "rsInvalidUnicodeBiDi";

			// Token: 0x04004C1C RID: 19484
			public const string rsInvalidVerticalAlign = "rsInvalidVerticalAlign";

			// Token: 0x04004C1D RID: 19485
			public const string rsInvalidWritingMode = "rsInvalidWritingMode";

			// Token: 0x04004C1E RID: 19486
			public const string rsNegativeSize = "rsNegativeSize";

			// Token: 0x04004C1F RID: 19487
			public const string rsOutOfRangeSize = "rsOutOfRangeSize";

			// Token: 0x04004C20 RID: 19488
			public const string rsOverallPageNumberInBody = "rsOverallPageNumberInBody";

			// Token: 0x04004C21 RID: 19489
			public const string rsPageNumberInBody = "rsPageNumberInBody";

			// Token: 0x04004C22 RID: 19490
			public const string rsOverallPageNumberInScopedAggregate = "rsOverallPageNumberInScopedAggregate";

			// Token: 0x04004C23 RID: 19491
			public const string rsPageNumberInScopedAggregate = "rsPageNumberInScopedAggregate";

			// Token: 0x04004C24 RID: 19492
			public const string rsParameterReference = "rsParameterReference";

			// Token: 0x04004C25 RID: 19493
			public const string rsReportItemReference = "rsReportItemReference";

			// Token: 0x04004C26 RID: 19494
			public const string rsReportItemReferenceInPageSection = "rsReportItemReferenceInPageSection";

			// Token: 0x04004C27 RID: 19495
			public const string rsDataSetReference = "rsDataSetReference";

			// Token: 0x04004C28 RID: 19496
			public const string rsDataSourceReference = "rsDataSourceReference";

			// Token: 0x04004C29 RID: 19497
			public const string rsInvalidDataSetQuery = "rsInvalidDataSetQuery";

			// Token: 0x04004C2A RID: 19498
			public const string rsErrorLoadingCodeModule = "rsErrorLoadingCodeModule";

			// Token: 0x04004C2B RID: 19499
			public const string rsInvalidTextEffect = "rsInvalidTextEffect";

			// Token: 0x04004C2C RID: 19500
			public const string rsInvalidBackgroundHatchType = "rsInvalidBackgroundHatchType";

			// Token: 0x04004C2D RID: 19501
			public const string rsInvalidBackgroundImagePosition = "rsInvalidBackgroundImagePosition";

			// Token: 0x04004C2E RID: 19502
			public const string rsInvalidTextOrientations = "rsInvalidTextOrientations";

			// Token: 0x04004C2F RID: 19503
			public const string rsInvalidListStyle = "rsInvalidListStyle";

			// Token: 0x04004C30 RID: 19504
			public const string rsInvalidMarkupType = "rsInvalidMarkupType";

			// Token: 0x04004C31 RID: 19505
			public const string rsInvalidRenderFormatUsage = "rsInvalidRenderFormatUsage";

			// Token: 0x04004C32 RID: 19506
			public const string rsNotASharedDataSetDefinition = "rsNotASharedDataSetDefinition";

			// Token: 0x04004C33 RID: 19507
			public const string rsInvalidSharedDataSetDefinition = "rsInvalidSharedDataSetDefinition";

			// Token: 0x04004C34 RID: 19508
			public const string rsMissingDataSetParameterDefault = "rsMissingDataSetParameterDefault";

			// Token: 0x04004C35 RID: 19509
			public const string rsInvalidCollationName = "rsInvalidCollationName";

			// Token: 0x04004C36 RID: 19510
			public const string rsCollationAndCollationCultureSpecified = "rsCollationAndCollationCultureSpecified";

			// Token: 0x04004C37 RID: 19511
			public const string rsDataSetWithoutFields = "rsDataSetWithoutFields";

			// Token: 0x04004C38 RID: 19512
			public const string rsInvalidFeatureRdlElement = "rsInvalidFeatureRdlElement";

			// Token: 0x04004C39 RID: 19513
			public const string rsInvalidFeatureRdlAttribute = "rsInvalidFeatureRdlAttribute";

			// Token: 0x04004C3A RID: 19514
			public const string rsInvalidFeatureRdlPropertyValue = "rsInvalidFeatureRdlPropertyValue";

			// Token: 0x04004C3B RID: 19515
			public const string rsInvalidFeatureRdlExpression = "rsInvalidFeatureRdlExpression";

			// Token: 0x04004C3C RID: 19516
			public const string rsInvalidFeatureRdlExpressionAggregatesOfAggregates = "rsInvalidFeatureRdlExpressionAggregatesOfAggregates";

			// Token: 0x04004C3D RID: 19517
			public const string rsInvalidPeerGroupsNotSupported = "rsInvalidPeerGroupsNotSupported";

			// Token: 0x04004C3E RID: 19518
			public const string rsInvalidComplexExpression = "rsInvalidComplexExpression";

			// Token: 0x04004C3F RID: 19519
			public const string rsInvalidComplexExpressionInReport = "rsInvalidComplexExpressionInReport";

			// Token: 0x04004C40 RID: 19520
			public const string rsRenderingChunksUnavailable = "rsRenderingChunksUnavailable";

			// Token: 0x04004C41 RID: 19521
			public const string rsInvalidScopeReference = "rsInvalidScopeReference";

			// Token: 0x04004C42 RID: 19522
			public const string rsInvalidScopeCollectionReference = "rsInvalidScopeCollectionReference";

			// Token: 0x04004C43 RID: 19523
			public const string rsScopeReferenceInComplexExpression = "rsScopeReferenceInComplexExpression";

			// Token: 0x04004C44 RID: 19524
			public const string rsScopeReferenceUsesDataSetMoreThanOnce = "rsScopeReferenceUsesDataSetMoreThanOnce";

			// Token: 0x04004C45 RID: 19525
			public const string rsInvalidRuntimeScopeReference = "rsInvalidRuntimeScopeReference";

			// Token: 0x04004C46 RID: 19526
			public const string rsVariableTypeNotSerializable = "rsVariableTypeNotSerializable";

			// Token: 0x04004C47 RID: 19527
			public const string rsUnexpectedSerializationError = "rsUnexpectedSerializationError";

			// Token: 0x04004C48 RID: 19528
			public const string rsDataSourcePrompt = "rsDataSourcePrompt";

			// Token: 0x04004C49 RID: 19529
			public const string rsNoRowsFieldAccess = "rsNoRowsFieldAccess";

			// Token: 0x04004C4A RID: 19530
			public const string rsNonExistingFieldReference = "rsNonExistingFieldReference";

			// Token: 0x04004C4B RID: 19531
			public const string rsNonExistingFieldReferenceByName = "rsNonExistingFieldReferenceByName";

			// Token: 0x04004C4C RID: 19532
			public const string rsNonExistingParameterReference = "rsNonExistingParameterReference";

			// Token: 0x04004C4D RID: 19533
			public const string rsNonExistingReportItemReference = "rsNonExistingReportItemReference";

			// Token: 0x04004C4E RID: 19534
			public const string rsNonExistingVariableReference = "rsNonExistingVariableReference";

			// Token: 0x04004C4F RID: 19535
			public const string rsNonExistingScopeReference = "rsNonExistingScopeReference";

			// Token: 0x04004C50 RID: 19536
			public const string rsNonExistingGlobalReference = "rsNonExistingGlobalReference";

			// Token: 0x04004C51 RID: 19537
			public const string rsNonExistingUserReference = "rsNonExistingUserReference";

			// Token: 0x04004C52 RID: 19538
			public const string rsNonExistingLookupReference = "rsNonExistingLookupReference";

			// Token: 0x04004C53 RID: 19539
			public const string rsNonExistingDataSetReference = "rsNonExistingDataSetReference";

			// Token: 0x04004C54 RID: 19540
			public const string rsNonExistingDataSourceReference = "rsNonExistingDataSourceReference";

			// Token: 0x04004C55 RID: 19541
			public const string rsFilterEvaluationError = "rsFilterEvaluationError";

			// Token: 0x04004C56 RID: 19542
			public const string rsFilterFieldError = "rsFilterFieldError";

			// Token: 0x04004C57 RID: 19543
			public const string rsProcessingAbortedByError = "rsProcessingAbortedByError";

			// Token: 0x04004C58 RID: 19544
			public const string rsProcessingAbortedByUser = "rsProcessingAbortedByUser";

			// Token: 0x04004C59 RID: 19545
			public const string rsParameterError = "rsParameterError";

			// Token: 0x04004C5A RID: 19546
			public const string rsParametersNotSpecified = "rsParametersNotSpecified";

			// Token: 0x04004C5B RID: 19547
			public const string rsDatasetParametersNotSpecified = "rsDatasetParametersNotSpecified";

			// Token: 0x04004C5C RID: 19548
			public const string rsCredentialsNotSpecified = "rsCredentialsNotSpecified";

			// Token: 0x04004C5D RID: 19549
			public const string rsExpressionErrorValue = "rsExpressionErrorValue";

			// Token: 0x04004C5E RID: 19550
			public const string rsRichTextParseErrorValue = "rsRichTextParseErrorValue";

			// Token: 0x04004C5F RID: 19551
			public const string rsDataSourceTypeNull = "rsDataSourceTypeNull";

			// Token: 0x04004C60 RID: 19552
			public const string rsNoFieldDataAtIndex = "rsNoFieldDataAtIndex";

			// Token: 0x04004C61 RID: 19553
			public const string rsErrorOpeningConnection = "rsErrorOpeningConnection";

			// Token: 0x04004C62 RID: 19554
			public const string rsErrorImpersonatingUser = "rsErrorImpersonatingUser";

			// Token: 0x04004C63 RID: 19555
			public const string rsErrorImpersonatingServiceAccount = "rsErrorImpersonatingServiceAccount";

			// Token: 0x04004C64 RID: 19556
			public const string rsErrorImpersonateServiceAccountNotAllowed = "rsErrorImpersonateServiceAccountNotAllowed";

			// Token: 0x04004C65 RID: 19557
			public const string rsDataExtensionWithoutConnectionExtension = "rsDataExtensionWithoutConnectionExtension";

			// Token: 0x04004C66 RID: 19558
			public const string rsInvalidSharedDataSetReference = "rsInvalidSharedDataSetReference";

			// Token: 0x04004C67 RID: 19559
			public const string rsManagedDataProviderWithoutConnectionExtension = "rsManagedDataProviderWithoutConnectionExtension";

			// Token: 0x04004C68 RID: 19560
			public const string rsErrorClosingConnection = "rsErrorClosingConnection";

			// Token: 0x04004C69 RID: 19561
			public const string rsErrorRollbackTransaction = "rsErrorRollbackTransaction";

			// Token: 0x04004C6A RID: 19562
			public const string rsErrorCommitTransaction = "rsErrorCommitTransaction";

			// Token: 0x04004C6B RID: 19563
			public const string rsErrorCreatingCommand = "rsErrorCreatingCommand";

			// Token: 0x04004C6C RID: 19564
			public const string rsErrorCreatingQueryParameter = "rsErrorCreatingQueryParameter";

			// Token: 0x04004C6D RID: 19565
			public const string rsErrorAddingMultiValueQueryParameter = "rsErrorAddingMultiValueQueryParameter";

			// Token: 0x04004C6E RID: 19566
			public const string rsErrorAddingQueryParameter = "rsErrorAddingQueryParameter";

			// Token: 0x04004C6F RID: 19567
			public const string rsErrorSettingCommandText = "rsErrorSettingCommandText";

			// Token: 0x04004C70 RID: 19568
			public const string rsErrorSettingCommandType = "rsErrorSettingCommandType";

			// Token: 0x04004C71 RID: 19569
			public const string rsErrorSettingTransaction = "rsErrorSettingTransaction";

			// Token: 0x04004C72 RID: 19570
			public const string rsErrorSettingQueryTimeout = "rsErrorSettingQueryTimeout";

			// Token: 0x04004C73 RID: 19571
			public const string rsErrorExecutingCommand = "rsErrorExecutingCommand";

			// Token: 0x04004C74 RID: 19572
			public const string rsQueryMemoryLimitExceeded = "rsQueryMemoryLimitExceeded";

			// Token: 0x04004C75 RID: 19573
			public const string rsQueryTimeoutExceeded = "rsQueryTimeoutExceeded";

			// Token: 0x04004C76 RID: 19574
			public const string rsErrorCancelingCommand = "rsErrorCancelingCommand";

			// Token: 0x04004C77 RID: 19575
			public const string rsErrorCreatingDataReader = "rsErrorCreatingDataReader";

			// Token: 0x04004C78 RID: 19576
			public const string rsErrorDisposingDataReader = "rsErrorDisposingDataReader";

			// Token: 0x04004C79 RID: 19577
			public const string rsErrorReadingNextDataRow = "rsErrorReadingNextDataRow";

			// Token: 0x04004C7A RID: 19578
			public const string rsErrorReadingDataField = "rsErrorReadingDataField";

			// Token: 0x04004C7B RID: 19579
			public const string rsErrorReadingDataAggregationField = "rsErrorReadingDataAggregationField";

			// Token: 0x04004C7C RID: 19580
			public const string rsRenderingExtensionNotFound = "rsRenderingExtensionNotFound";

			// Token: 0x04004C7D RID: 19581
			public const string rsInvalidChart = "rsInvalidChart";

			// Token: 0x04004C7E RID: 19582
			public const string rsErrorDuringChartRendering = "rsErrorDuringChartRendering";

			// Token: 0x04004C7F RID: 19583
			public const string rsOWCNotInstalled = "rsOWCNotInstalled";

			// Token: 0x04004C80 RID: 19584
			public const string rsUnsupportedURLProtocol = "rsUnsupportedURLProtocol";

			// Token: 0x04004C81 RID: 19585
			public const string rsMalformattedURL = "rsMalformattedURL";

			// Token: 0x04004C82 RID: 19586
			public const string rsCRIProcessingError = "rsCRIProcessingError";

			// Token: 0x04004C83 RID: 19587
			public const string rsErrorDuringROMWriteback = "rsErrorDuringROMWriteback";

			// Token: 0x04004C84 RID: 19588
			public const string rsErrorDuringROMDefinitionWriteback = "rsErrorDuringROMDefinitionWriteback";

			// Token: 0x04004C85 RID: 19589
			public const string rsErrorDuringROMWritebackStringExpected = "rsErrorDuringROMWritebackStringExpected";

			// Token: 0x04004C86 RID: 19590
			public const string rsErrorDuringROMWritebackNonDynamicAction = "rsErrorDuringROMWritebackNonDynamicAction";

			// Token: 0x04004C87 RID: 19591
			public const string rsErrorDuringROMWritebackDynamicAction = "rsErrorDuringROMWritebackDynamicAction";

			// Token: 0x04004C88 RID: 19592
			public const string rsSerializableTypeNotSupported = "rsSerializableTypeNotSupported";

			// Token: 0x04004C89 RID: 19593
			public const string rsInvalidReportArchiveFormat = "rsInvalidReportArchiveFormat";

			// Token: 0x04004C8A RID: 19594
			public const string rsInvalidDataExtension = "rsInvalidDataExtension";

			// Token: 0x04004C8B RID: 19595
			public const string rsMissingFieldInStartAt = "rsMissingFieldInStartAt";

			// Token: 0x04004C8C RID: 19596
			public const string rsUnexpectedError = "rsUnexpectedError";

			// Token: 0x04004C8D RID: 19597
			public const string rsGaugePanelInvalidData = "rsGaugePanelInvalidData";

			// Token: 0x04004C8E RID: 19598
			public const string rsGaugePanelInvalidMinMaxScale = "rsGaugePanelInvalidMinMaxScale";

			// Token: 0x04004C8F RID: 19599
			public const string rsGaugePanelInvalidStartEndRange = "rsGaugePanelInvalidStartEndRange";

			// Token: 0x04004C90 RID: 19600
			public const string rsInvalidRowGaugeMemberCannotBeDynamic = "rsInvalidRowGaugeMemberCannotBeDynamic";

			// Token: 0x04004C91 RID: 19601
			public const string rsInvalidRowGaugeMemberCannotContainChildMember = "rsInvalidRowGaugeMemberCannotContainChildMember";

			// Token: 0x04004C92 RID: 19602
			public const string rsInvalidColumnGaugeMemberCannotContainMultipleChildMember = "rsInvalidColumnGaugeMemberCannotContainMultipleChildMember";

			// Token: 0x04004C93 RID: 19603
			public const string rsDuplicateItemName = "rsDuplicateItemName";

			// Token: 0x04004C94 RID: 19604
			public const string rsDuplicateChartAxisName = "rsDuplicateChartAxisName";

			// Token: 0x04004C95 RID: 19605
			public const string rsDuplicateChartLegendItemName = "rsDuplicateChartLegendItemName";

			// Token: 0x04004C96 RID: 19606
			public const string rsDuplicateChartLegendCustomItemCellName = "rsDuplicateChartLegendCustomItemCellName";

			// Token: 0x04004C97 RID: 19607
			public const string rsDuplicateChartFormulaParameter = "rsDuplicateChartFormulaParameter";

			// Token: 0x04004C98 RID: 19608
			public const string rsInvalidEnumValue = "rsInvalidEnumValue";

			// Token: 0x04004C99 RID: 19609
			public const string rsTraceGaugePanelInitialized = "rsTraceGaugePanelInitialized";

			// Token: 0x04004C9A RID: 19610
			public const string rsInvalidOperation = "rsInvalidOperation";

			// Token: 0x04004C9B RID: 19611
			public const string rsInvalidParameterValue = "rsInvalidParameterValue";

			// Token: 0x04004C9C RID: 19612
			public const string rsInvalidParameterRange = "rsInvalidParameterRange";

			// Token: 0x04004C9D RID: 19613
			public const string rsNotInCollection = "rsNotInCollection";

			// Token: 0x04004C9E RID: 19614
			public const string rsRenderSubreportError = "rsRenderSubreportError";

			// Token: 0x04004C9F RID: 19615
			public const string rsLevelCallRecursiveHierarchyBothDimensions = "rsLevelCallRecursiveHierarchyBothDimensions";

			// Token: 0x04004CA0 RID: 19616
			public const string rsMapLayerMissingProperty = "rsMapLayerMissingProperty";

			// Token: 0x04004CA1 RID: 19617
			public const string rsMapMaximumSpatialElementCountReached = "rsMapMaximumSpatialElementCountReached";

			// Token: 0x04004CA2 RID: 19618
			public const string rsMapMaximumTotalPointCountReached = "rsMapMaximumTotalPointCountReached";

			// Token: 0x04004CA3 RID: 19619
			public const string rsMapInvalidSpatialFieldType = "rsMapInvalidSpatialFieldType";

			// Token: 0x04004CA4 RID: 19620
			public const string rsMapInvalidFieldName = "rsMapInvalidFieldName";

			// Token: 0x04004CA5 RID: 19621
			public const string rsMapFieldBindingExpressionTypeMismatch = "rsMapFieldBindingExpressionTypeMismatch";

			// Token: 0x04004CA6 RID: 19622
			public const string rsMapSpatialElementHasMoreThanOnMatchingGroupInstance = "rsMapSpatialElementHasMoreThanOnMatchingGroupInstance";

			// Token: 0x04004CA7 RID: 19623
			public const string rsMapInvalidShapefileReference = "rsMapInvalidShapefileReference";

			// Token: 0x04004CA8 RID: 19624
			public const string rsMapCannotLoadShapefile = "rsMapCannotLoadShapefile";

			// Token: 0x04004CA9 RID: 19625
			public const string rsMapShapefileTypeMismatch = "rsMapShapefileTypeMismatch";

			// Token: 0x04004CAA RID: 19626
			public const string rsCannotCompareSpatialType = "rsCannotCompareSpatialType";

			// Token: 0x04004CAB RID: 19627
			public const string rsMapUnsupportedValueFieldType = "rsMapUnsupportedValueFieldType";

			// Token: 0x04004CAC RID: 19628
			public const string rsUnionOfNonSpatialData = "rsUnionOfNonSpatialData";

			// Token: 0x04004CAD RID: 19629
			public const string rsUnionOfMixedSpatialTypes = "rsUnionOfMixedSpatialTypes";

			// Token: 0x04004CAE RID: 19630
			public const string rsInvalidMapDataRegionName = "rsInvalidMapDataRegionName";

			// Token: 0x04004CAF RID: 19631
			public const string rsInvalidGroupingDomainScopeWithParent = "rsInvalidGroupingDomainScopeWithParent";

			// Token: 0x04004CB0 RID: 19632
			public const string rsInvalidGroupingDomainScopeTargetWithParent = "rsInvalidGroupingDomainScopeTargetWithParent";

			// Token: 0x04004CB1 RID: 19633
			public const string rsInvalidGroupingDomainScopeWithDetailGroup = "rsInvalidGroupingDomainScopeWithDetailGroup";

			// Token: 0x04004CB2 RID: 19634
			public const string rsInvalidGroupingDomainScope = "rsInvalidGroupingDomainScope";

			// Token: 0x04004CB3 RID: 19635
			public const string rsInvalidGroupingDomainScopeDataSet = "rsInvalidGroupingDomainScopeDataSet";

			// Token: 0x04004CB4 RID: 19636
			public const string rsInvalidGroupingDomainScopeNotAncestor = "rsInvalidGroupingDomainScopeNotAncestor";

			// Token: 0x04004CB5 RID: 19637
			public const string rsInvalidGroupingDomainScopeNotLeaf = "rsInvalidGroupingDomainScopeNotLeaf";

			// Token: 0x04004CB6 RID: 19638
			public const string rsInvalidGroupingDomainScopeMap = "rsInvalidGroupingDomainScopeMap";

			// Token: 0x04004CB7 RID: 19639
			public const string rsInvalidSortExpressionScopeDomainScope = "rsInvalidSortExpressionScopeDomainScope";

			// Token: 0x04004CB8 RID: 19640
			public const string rsStateIndicatorInvalidAutoGenerateMinMaxExpression = "rsStateIndicatorInvalidAutoGenerateMinMaxExpression";

			// Token: 0x04004CB9 RID: 19641
			public const string rsStateIndicatorInvalidTransformationScope = "rsStateIndicatorInvalidTransformationScope";

			// Token: 0x04004CBA RID: 19642
			public const string rsStateIndicatorInvalidMinMax = "rsStateIndicatorInvalidMinMax";

			// Token: 0x04004CBB RID: 19643
			public const string rsInvalidAppPropsRootElement = "rsInvalidAppPropsRootElement";

			// Token: 0x04004CBC RID: 19644
			public const string rsUnrecognizedNonIgnorableNamespaces = "rsUnrecognizedNonIgnorableNamespaces";

			// Token: 0x04004CBD RID: 19645
			public const string rsUndefinedMustUnderstandNamespaces = "rsUndefinedMustUnderstandNamespaces";

			// Token: 0x04004CBE RID: 19646
			public const string rsNullReportArchiveStream = "rsNullReportArchiveStream";

			// Token: 0x04004CBF RID: 19647
			public const string rsParameterValueCastFailure = "rsParameterValueCastFailure";

			// Token: 0x04004CC0 RID: 19648
			public const string rsInvalidBandInvalidLayoutDirection = "rsInvalidBandInvalidLayoutDirection";

			// Token: 0x04004CC1 RID: 19649
			public const string rsInvalidBandPageBreakIsSet = "rsInvalidBandPageBreakIsSet";

			// Token: 0x04004CC2 RID: 19650
			public const string rsInvalidBandShouldNotBeTogglable = "rsInvalidBandShouldNotBeTogglable";

			// Token: 0x04004CC3 RID: 19651
			public const string rsBandKeepTogetherIgnored = "rsBandKeepTogetherIgnored";

			// Token: 0x04004CC4 RID: 19652
			public const string rsBandIgnoredProperties = "rsBandIgnoredProperties";

			// Token: 0x04004CC5 RID: 19653
			public const string rsInvalidBandNavigationReference = "rsInvalidBandNavigationReference";

			// Token: 0x04004CC6 RID: 19654
			public const string rsInvalidBandNavigationItem = "rsInvalidBandNavigationItem";

			// Token: 0x04004CC7 RID: 19655
			public const string rsInvalidBandNavigations = "rsInvalidBandNavigations";

			// Token: 0x04004CC8 RID: 19656
			public const string rsInvalidSliderDataSetReferenceField = "rsInvalidSliderDataSetReferenceField";

			// Token: 0x04004CC9 RID: 19657
			public const string rsInvalidSliderDataSetReference = "rsInvalidSliderDataSetReference";

			// Token: 0x04004CCA RID: 19658
			public const string rsNotSupportedInStreamingMode = "rsNotSupportedInStreamingMode";

			// Token: 0x04004CCB RID: 19659
			public const string rsInvalidScopeID = "rsInvalidScopeID";

			// Token: 0x04004CCC RID: 19660
			public const string rsInvalidScopeIDOrder = "rsInvalidScopeIDOrder";

			// Token: 0x04004CCD RID: 19661
			public const string rsInvalidScopeIDNotSet = "rsInvalidScopeIDNotSet";

			// Token: 0x04004CCE RID: 19662
			public const string rsDetailGroupsNotSupportedInStreamingMode = "rsDetailGroupsNotSupportedInStreamingMode";

			// Token: 0x04004CCF RID: 19663
			public const string rsRombasedRestartFailed = "rsRombasedRestartFailed";

			// Token: 0x04004CD0 RID: 19664
			public const string rsRombasedRestartFailedTypeMismatch = "rsRombasedRestartFailedTypeMismatch";

			// Token: 0x04004CD1 RID: 19665
			public const string rsErrorSettingStartAt = "rsErrorSettingStartAt";

			// Token: 0x04004CD2 RID: 19666
			public const string rsMissingDefaultRelationshipJoinCondition = "rsMissingDefaultRelationshipJoinCondition";

			// Token: 0x04004CD3 RID: 19667
			public const string rsNonExistingRelationshipRelatedScope = "rsNonExistingRelationshipRelatedScope";

			// Token: 0x04004CD4 RID: 19668
			public const string rsInvalidSelfJoinRelationship = "rsInvalidSelfJoinRelationship";

			// Token: 0x04004CD5 RID: 19669
			public const string rsInvalidNaturalSortGroupExpressionNotSimpleFieldReference = "rsInvalidNaturalSortGroupExpressionNotSimpleFieldReference";

			// Token: 0x04004CD6 RID: 19670
			public const string rsInvalidParameterLayoutNumberOfRowsOrColumnsExceedingLimit = "rsInvalidParameterLayoutNumberOfRowsOrColumnsExceedingLimit";

			// Token: 0x04004CD7 RID: 19671
			public const string rsInvalidParameterLayoutNumberOfConsecutiveEmptyRowsExceedingLimit = "rsInvalidParameterLayoutNumberOfConsecutiveEmptyRowsExceedingLimit";

			// Token: 0x04004CD8 RID: 19672
			public const string rsInvalidParameterLayoutZeroOrLessRowOrCol = "rsInvalidParameterLayoutZeroOrLessRowOrCol";

			// Token: 0x04004CD9 RID: 19673
			public const string rsInvalidParameterLayoutParametersMissingFromPanel = "rsInvalidParameterLayoutParametersMissingFromPanel";

			// Token: 0x04004CDA RID: 19674
			public const string rsInvalidParameterLayoutCellDefNotEqualsParameterCount = "rsInvalidParameterLayoutCellDefNotEqualsParameterCount";

			// Token: 0x04004CDB RID: 19675
			public const string rsInvalidParameterLayoutParameterAppearsTwice = "rsInvalidParameterLayoutParameterAppearsTwice";

			// Token: 0x04004CDC RID: 19676
			public const string rsInvalidParameterLayoutParameterNotVisible = "rsInvalidParameterLayoutParameterNotVisible";

			// Token: 0x04004CDD RID: 19677
			public const string rsInvalidParameterLayoutParameterNameMissing = "rsInvalidParameterLayoutParameterNameMissing";

			// Token: 0x04004CDE RID: 19678
			public const string rsInvalidParameterLayoutRowColOutOfBounds = "rsInvalidParameterLayoutRowColOutOfBounds";

			// Token: 0x04004CDF RID: 19679
			public const string rsInvalidParameterLayoutCellCollition = "rsInvalidParameterLayoutCellCollition";

			// Token: 0x04004CE0 RID: 19680
			public const string rsInvalidMustUnderstandNamespaces = "rsInvalidMustUnderstandNamespaces";

			// Token: 0x04004CE1 RID: 19681
			public const string rsHasUserProfileDependencies = "rsHasUserProfileDependencies";
		}
	}
}
