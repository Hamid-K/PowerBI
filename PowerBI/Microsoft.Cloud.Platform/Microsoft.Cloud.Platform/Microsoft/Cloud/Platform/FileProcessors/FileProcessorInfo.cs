using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000E9 RID: 233
	public sealed class FileProcessorInfo
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00017C18 File Offset: 0x00015E18
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x00017C20 File Offset: 0x00015E20
		public CultureInfo Culture { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00017C29 File Offset: 0x00015E29
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x00017C31 File Offset: 0x00015E31
		public IDictionary<string, string> ReplacementStrings { get; private set; }

		// Token: 0x060006A2 RID: 1698 RVA: 0x00017C3A File Offset: 0x00015E3A
		public FileProcessorInfo(CultureInfo locale, IDictionary<string, string> replacementStrings)
		{
			this.Culture = locale;
			this.ReplacementStrings = replacementStrings;
		}
	}
}
