using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003FA RID: 1018
	public interface INgramExtractorFactory
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06001564 RID: 5476
		bool UseHashingTrick { get; }

		// Token: 0x06001565 RID: 5477
		IDataTransform Create(IHostEnvironment env, IDataView input, ExtractorColumn[] cols);
	}
}
