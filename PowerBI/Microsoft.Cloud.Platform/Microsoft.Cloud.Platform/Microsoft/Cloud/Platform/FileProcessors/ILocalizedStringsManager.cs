using System;
using System.Globalization;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000FD RID: 253
	public interface ILocalizedStringsManager
	{
		// Token: 0x06000706 RID: 1798
		string GetLocalizedResource(string key, CultureInfo locale);
	}
}
