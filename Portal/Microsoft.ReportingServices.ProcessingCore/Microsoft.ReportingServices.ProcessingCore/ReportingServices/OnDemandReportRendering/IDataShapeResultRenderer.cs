using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000080 RID: 128
	internal interface IDataShapeResultRenderer
	{
		// Token: 0x06000782 RID: 1922
		void RenderDataShapeResult(DataShapeResult dataShapeResult, CreateJsonStreamWriter createAndRegisterStream);
	}
}
