using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000D1 RID: 209
	public class PassThroughPrivateInformationFormatter : IPrivateInformationFormatter
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x000082DE File Offset: 0x000064DE
		public string MarkAsOII(string plainString)
		{
			return plainString;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000082E1 File Offset: 0x000064E1
		public string MarkAsEUII(string plainString)
		{
			return plainString;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000082E4 File Offset: 0x000064E4
		public string MarkAsEUPI(string plainString)
		{
			return plainString;
		}
	}
}
