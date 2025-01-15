using System;

namespace Microsoft.AnalysisServices.Tabular.Utilities
{
	// Token: 0x0200019A RID: 410
	internal class TranslatablePropertyInfo
	{
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x000A8E09 File Offset: 0x000A7009
		// (set) Token: 0x06001969 RID: 6505 RVA: 0x000A8E11 File Offset: 0x000A7011
		public TranslatedProperty Property { get; set; }

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x000A8E1A File Offset: 0x000A701A
		// (set) Token: 0x0600196B RID: 6507 RVA: 0x000A8E22 File Offset: 0x000A7022
		public bool IsMultiline { get; set; }
	}
}
