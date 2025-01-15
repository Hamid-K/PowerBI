using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200035E RID: 862
	internal static class DSD2ErrorHandlingUtils
	{
		// Token: 0x06001C84 RID: 7300 RVA: 0x000733DE File Offset: 0x000715DE
		internal static void ThrowIfInavalidElement(string elementName)
		{
			throw new InvalidElementException(elementName);
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x000733E6 File Offset: 0x000715E6
		internal static void ThrowIfUnrecognizedElement(string elementName)
		{
			throw new UnrecognizedXmlElementException(elementName);
		}
	}
}
