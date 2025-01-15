using System;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000F5 RID: 245
	internal struct QdmTranslationSettings
	{
		// Token: 0x06000E23 RID: 3619 RVA: 0x00023D8F File Offset: 0x00021F8F
		internal QdmTranslationSettings(bool composable = false, bool forceCrossJoinQueryPlan = false, bool allowSummarizeColumns = false)
		{
			this = default(QdmTranslationSettings);
			this.Composable = composable;
			this.ForceCrossJoinQueryPlan = forceCrossJoinQueryPlan;
			this.AllowSummarizeColumns = allowSummarizeColumns;
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x00023DAD File Offset: 0x00021FAD
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x00023DB5 File Offset: 0x00021FB5
		public bool AllowSummarizeColumns { readonly get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00023DBE File Offset: 0x00021FBE
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x00023DC6 File Offset: 0x00021FC6
		internal bool Composable { readonly get; private set; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00023DCF File Offset: 0x00021FCF
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x00023DD7 File Offset: 0x00021FD7
		internal bool ForceCrossJoinQueryPlan { readonly get; private set; }
	}
}
