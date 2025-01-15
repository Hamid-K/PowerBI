using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform
{
	// Token: 0x0200001E RID: 30
	public interface IDaxDataTransformMetadata
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004F RID: 79
		string DaxFunctionName { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000050 RID: 80
		IReadOnlyList<IDaxDataTransformParameterMetadata> Parameters { get; }
	}
}
