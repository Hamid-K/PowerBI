using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200007D RID: 125
	internal static class DataSegmentRendererFactory
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x0001BD21 File Offset: 0x00019F21
		internal static IDataSegmentRenderer CreateDataSegmentRenderer()
		{
			return (IDataSegmentRenderer)Activator.CreateInstance("Microsoft.ReportingServices.DataSegmentRendering", "Microsoft.ReportingServices.Rendering.DataSegmentRenderer.DataSegmentRenderer").Unwrap();
		}
	}
}
