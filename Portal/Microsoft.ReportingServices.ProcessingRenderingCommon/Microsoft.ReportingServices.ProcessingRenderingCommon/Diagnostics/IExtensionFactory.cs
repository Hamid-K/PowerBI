using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000075 RID: 117
	public interface IExtensionFactory
	{
		// Token: 0x06000347 RID: 839
		bool IsRegisteredCustomReportItemExtension(string extensionType);

		// Token: 0x06000348 RID: 840
		object GetNewCustomReportItemProcessingInstanceClass(string reportItemName);

		// Token: 0x06000349 RID: 841
		IExtension GetNewRendererExtensionClass(string format);
	}
}
