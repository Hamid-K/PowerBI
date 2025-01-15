using System;

namespace Microsoft.PowerBI.Packaging.Host
{
	// Token: 0x02000093 RID: 147
	public interface IFeatureSwitches
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600042E RID: 1070
		bool DatasetToSemanticModelRename { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600042F RID: 1071
		bool Tmdl { get; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000430 RID: 1072
		bool EnhancedReportFormat { get; }
	}
}
