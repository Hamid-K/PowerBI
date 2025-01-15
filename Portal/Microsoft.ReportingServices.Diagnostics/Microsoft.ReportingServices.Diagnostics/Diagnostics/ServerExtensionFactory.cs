using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000065 RID: 101
	internal class ServerExtensionFactory : IExtensionFactory
	{
		// Token: 0x0600032A RID: 810 RVA: 0x0000D497 File Offset: 0x0000B697
		public bool IsRegisteredCustomReportItemExtension(string extensionType)
		{
			return ExtensionClassFactory.IsRegisteredCustomReportItemExtension(extensionType);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000D49F File Offset: 0x0000B69F
		public object GetNewCustomReportItemProcessingInstanceClass(string reportItemName)
		{
			return ExtensionClassFactory.GetNewCustomReportItemProcessingInstanceClass(reportItemName);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		public IExtension GetNewRendererExtensionClass(string format)
		{
			Extension extension = ProcessingContext.Configuration.Extensions.Renderer[format];
			IExtension extension2 = null;
			if (extension != null)
			{
				extension2 = ExtensionClassFactory.GetNewInstanceExtensionClass(format, "Render");
			}
			return extension2;
		}
	}
}
