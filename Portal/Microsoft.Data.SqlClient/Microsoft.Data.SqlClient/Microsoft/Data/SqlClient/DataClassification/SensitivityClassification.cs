using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Data.SqlClient.DataClassification
{
	// Token: 0x02000159 RID: 345
	public sealed class SensitivityClassification
	{
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x0006BB20 File Offset: 0x00069D20
		// (set) Token: 0x06001A4A RID: 6730 RVA: 0x0006BB28 File Offset: 0x00069D28
		public ReadOnlyCollection<Label> Labels { get; private set; }

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x0006BB31 File Offset: 0x00069D31
		// (set) Token: 0x06001A4C RID: 6732 RVA: 0x0006BB39 File Offset: 0x00069D39
		public ReadOnlyCollection<InformationType> InformationTypes { get; private set; }

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x0006BB42 File Offset: 0x00069D42
		// (set) Token: 0x06001A4E RID: 6734 RVA: 0x0006BB4A File Offset: 0x00069D4A
		public SensitivityRank SensitivityRank { get; private set; }

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x0006BB53 File Offset: 0x00069D53
		// (set) Token: 0x06001A50 RID: 6736 RVA: 0x0006BB5B File Offset: 0x00069D5B
		public ReadOnlyCollection<ColumnSensitivity> ColumnSensitivities { get; private set; }

		// Token: 0x06001A51 RID: 6737 RVA: 0x0006BB64 File Offset: 0x00069D64
		public SensitivityClassification(IList<Label> labels, IList<InformationType> informationTypes, IList<ColumnSensitivity> columnSensitivity, SensitivityRank sensitivityRank = SensitivityRank.NOT_DEFINED)
		{
			this.Labels = new ReadOnlyCollection<Label>(labels);
			this.InformationTypes = new ReadOnlyCollection<InformationType>(informationTypes);
			this.ColumnSensitivities = new ReadOnlyCollection<ColumnSensitivity>(columnSensitivity);
			this.SensitivityRank = sensitivityRank;
		}
	}
}
