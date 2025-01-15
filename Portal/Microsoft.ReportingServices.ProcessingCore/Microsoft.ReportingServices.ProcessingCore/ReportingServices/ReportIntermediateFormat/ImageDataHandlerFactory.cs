using System;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000404 RID: 1028
	internal static class ImageDataHandlerFactory
	{
		// Token: 0x06002C30 RID: 11312 RVA: 0x000CBEC8 File Offset: 0x000CA0C8
		public static ImageDataHandler Create(ReportElement reportElement, IBaseImage image)
		{
			switch (image.Source)
			{
			case Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.External:
				return new ExternalImageDataHandler(reportElement, image);
			case Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.Embedded:
				return new EmbeddedImageDataHandler(reportElement, image);
			case Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.Database:
				return new DatabaseImageDataHandler(reportElement, image);
			default:
				Global.Tracer.Assert(false, "Invalid Image.SourceType: {0}", new object[] { image.Source });
				throw new InvalidOperationException("Invalid Image.SourceType");
			}
		}
	}
}
