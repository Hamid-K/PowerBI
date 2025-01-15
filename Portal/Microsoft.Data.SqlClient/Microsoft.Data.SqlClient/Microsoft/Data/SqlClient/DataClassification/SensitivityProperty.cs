using System;

namespace Microsoft.Data.SqlClient.DataClassification
{
	// Token: 0x02000157 RID: 343
	public sealed class SensitivityProperty
	{
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0006BAAB File Offset: 0x00069CAB
		// (set) Token: 0x06001A40 RID: 6720 RVA: 0x0006BAB3 File Offset: 0x00069CB3
		public Label Label { get; private set; }

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x0006BABC File Offset: 0x00069CBC
		// (set) Token: 0x06001A42 RID: 6722 RVA: 0x0006BAC4 File Offset: 0x00069CC4
		public InformationType InformationType { get; private set; }

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x0006BACD File Offset: 0x00069CCD
		// (set) Token: 0x06001A44 RID: 6724 RVA: 0x0006BAD5 File Offset: 0x00069CD5
		public SensitivityRank SensitivityRank { get; private set; }

		// Token: 0x06001A45 RID: 6725 RVA: 0x0006BADE File Offset: 0x00069CDE
		public SensitivityProperty(Label label, InformationType informationType, SensitivityRank sensitivityRank = SensitivityRank.NOT_DEFINED)
		{
			this.Label = label;
			this.InformationType = informationType;
			this.SensitivityRank = sensitivityRank;
		}
	}
}
