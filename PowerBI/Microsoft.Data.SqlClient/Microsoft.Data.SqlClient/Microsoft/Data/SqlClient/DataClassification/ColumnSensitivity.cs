using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Data.SqlClient.DataClassification
{
	// Token: 0x02000158 RID: 344
	public sealed class ColumnSensitivity
	{
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0006BAFB File Offset: 0x00069CFB
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x0006BB03 File Offset: 0x00069D03
		public ReadOnlyCollection<SensitivityProperty> SensitivityProperties { get; private set; }

		// Token: 0x06001A48 RID: 6728 RVA: 0x0006BB0C File Offset: 0x00069D0C
		public ColumnSensitivity(IList<SensitivityProperty> sensitivityProperties)
		{
			this.SensitivityProperties = new ReadOnlyCollection<SensitivityProperty>(sensitivityProperties);
		}
	}
}
