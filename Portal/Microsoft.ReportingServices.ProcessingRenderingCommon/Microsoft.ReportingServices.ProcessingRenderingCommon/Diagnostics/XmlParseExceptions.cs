using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000041 RID: 65
	internal static class XmlParseExceptions
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00007838 File Offset: 0x00005A38
		internal static void ThrowInvalidFormat(string element)
		{
			throw new Exception(ErrorStringsWrapper.InvalidConfigElement(element));
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007845 File Offset: 0x00005A45
		internal static void ThrowElementMissing(string element)
		{
			throw new Exception(ErrorStringsWrapper.CouldNotFindElement(element));
		}
	}
}
