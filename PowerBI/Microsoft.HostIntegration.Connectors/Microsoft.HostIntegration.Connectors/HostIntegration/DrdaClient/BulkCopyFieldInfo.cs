using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009AE RID: 2478
	internal struct BulkCopyFieldInfo
	{
		// Token: 0x06004CB6 RID: 19638 RVA: 0x001329B5 File Offset: 0x00130BB5
		internal BulkCopyFieldInfo(DrdaColumnBinding binding)
		{
			this._fieldType = null;
			this._binding = binding;
		}

		// Token: 0x04003CAC RID: 15532
		internal DrdaMetaType _fieldType;

		// Token: 0x04003CAD RID: 15533
		internal DrdaColumnBinding _binding;
	}
}
