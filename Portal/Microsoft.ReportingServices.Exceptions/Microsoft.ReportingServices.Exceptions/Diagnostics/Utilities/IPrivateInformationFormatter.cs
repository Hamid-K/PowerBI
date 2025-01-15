using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000D0 RID: 208
	public interface IPrivateInformationFormatter
	{
		// Token: 0x0600048E RID: 1166
		string MarkAsEUII(string plainString);

		// Token: 0x0600048F RID: 1167
		string MarkAsEUPI(string plainString);

		// Token: 0x06000490 RID: 1168
		string MarkAsOII(string plainString);
	}
}
