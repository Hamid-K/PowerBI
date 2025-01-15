using System;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200004B RID: 75
	public sealed class LuciaSessionFileSavedArgs
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00004E29 File Offset: 0x00003029
		public LuciaSessionFileSavedArgs(string oldFilePath, string newFilePath)
		{
			this.OldFilePath = oldFilePath;
			this.NewFilePath = newFilePath;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00004E3F File Offset: 0x0000303F
		public string OldFilePath { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00004E47 File Offset: 0x00003047
		public string NewFilePath { get; }
	}
}
