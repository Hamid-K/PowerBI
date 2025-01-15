using System;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000654 RID: 1620
	internal sealed class ReportRendererFactory
	{
		// Token: 0x060057CD RID: 22477 RVA: 0x0016FF9D File Offset: 0x0016E19D
		private ReportRendererFactory()
		{
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x0016FFA8 File Offset: 0x0016E1A8
		internal static IRenderingExtension GetNewRenderer(string format, IExtensionFactory extFactory)
		{
			IRenderingExtension renderingExtension = null;
			try
			{
				renderingExtension = (IRenderingExtension)extFactory.GetNewRendererExtensionClass(format);
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				throw new ReportProcessingException(ErrorCode.rsRenderingExtensionNotFound);
			}
			return renderingExtension;
		}
	}
}
