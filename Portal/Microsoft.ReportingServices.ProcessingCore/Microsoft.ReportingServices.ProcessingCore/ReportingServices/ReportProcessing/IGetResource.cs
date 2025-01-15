using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200064C RID: 1612
	public interface IGetResource
	{
		// Token: 0x060057B2 RID: 22450
		void GetResource(ICatalogItemContext reportContext, string path, out byte[] resource, out string mimeType, out bool registerExternalWarning, out bool registerInvalidSizeWarning);
	}
}
