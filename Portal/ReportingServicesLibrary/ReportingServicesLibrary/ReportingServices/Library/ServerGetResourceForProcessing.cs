using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002DE RID: 734
	internal sealed class ServerGetResourceForProcessing : IGetResource
	{
		// Token: 0x06001A33 RID: 6707 RVA: 0x0006938E File Offset: 0x0006758E
		public ServerGetResourceForProcessing(RSService service)
		{
			this.m_service = service;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0006939D File Offset: 0x0006759D
		public void GetResource(ICatalogItemContext reportContext, string path, out byte[] resource, out string mimeType, out bool registerExternalWarning, out bool registerInvalidSizeWarning)
		{
			this.m_service.ProcessingGetResource(reportContext, path, out resource, out mimeType, out registerExternalWarning, out registerInvalidSizeWarning);
		}

		// Token: 0x0400097A RID: 2426
		private readonly RSService m_service;
	}
}
