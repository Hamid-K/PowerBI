using System;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001AF RID: 431
	internal class XmlaCommand
	{
		// Token: 0x06001A6A RID: 6762 RVA: 0x000AF566 File Offset: 0x000AD766
		internal XmlaCommand()
		{
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x000AF56E File Offset: 0x000AD76E
		// (set) Token: 0x06001A6C RID: 6764 RVA: 0x000AF576 File Offset: 0x000AD776
		public string CommandText { get; internal set; }

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x000AF57F File Offset: 0x000AD77F
		// (set) Token: 0x06001A6E RID: 6766 RVA: 0x000AF587 File Offset: 0x000AD787
		public string DatabaseName { get; internal set; }

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x000AF590 File Offset: 0x000AD790
		// (set) Token: 0x06001A70 RID: 6768 RVA: 0x000AF598 File Offset: 0x000AD798
		public bool IsTabular { get; internal set; }
	}
}
