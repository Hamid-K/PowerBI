using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200006D RID: 109
	internal interface IReportDeserializationContext
	{
		// Token: 0x0600022A RID: 554
		void PushDataScope(DataRegion dataRegion);

		// Token: 0x0600022B RID: 555
		void PushDataScope(Group group);

		// Token: 0x0600022C RID: 556
		void PushDataScope(LabelData labelData);

		// Token: 0x0600022D RID: 557
		void PopDataScope();

		// Token: 0x0600022E RID: 558
		DataSet GetCurrentDataSet();

		// Token: 0x0600022F RID: 559
		DataSet GetDataSetByName(string dataSetName);

		// Token: 0x06000230 RID: 560
		void Register(PVVisual visual, Tuple<ReportItem, IRdlReportItemConverter> context);

		// Token: 0x06000231 RID: 561
		Field FindDataSetField(string fieldName);

		// Token: 0x06000232 RID: 562
		Tuple<ReportItem, IRdlReportItemConverter> GetCreationContext(PVVisual visual);

		// Token: 0x06000233 RID: 563
		void RegisterReportSectionState(ReportSectionState reportState);

		// Token: 0x06000234 RID: 564
		void UnregisterReportSectionState();

		// Token: 0x06000235 RID: 565
		ReportSectionState GetCurrentReportSectionState();
	}
}
