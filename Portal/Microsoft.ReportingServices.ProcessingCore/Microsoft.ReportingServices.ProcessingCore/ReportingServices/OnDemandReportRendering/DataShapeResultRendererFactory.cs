using System;
using Microsoft.ReportingServices.DataShapeResultRenderer;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000081 RID: 129
	internal static class DataShapeResultRendererFactory
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x0001BD3C File Offset: 0x00019F3C
		internal static IDataShapeResultRenderer CreateDataShapeResultRenderer()
		{
			return new DataShapeResultRenderer();
		}
	}
}
