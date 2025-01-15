using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Azure.Common;

namespace Microsoft.AnalysisServices.Azure.CommonUtilities
{
	// Token: 0x0200002D RID: 45
	public interface ISampleModelInfo
	{
		// Token: 0x06000301 RID: 769
		IEnumerable<DatabaseMoniker> GetSampleModelMonikers();

		// Token: 0x06000302 RID: 770
		IEnumerable<string> GetSampleModelNames();

		// Token: 0x06000303 RID: 771
		IEnumerable<SampleModelProperty> GetSampleModelProperties();

		// Token: 0x06000304 RID: 772
		bool IsSampleModel(string modelName);

		// Token: 0x06000305 RID: 773
		bool TryGetSampleModelName(string inputModelName, out string modelName);

		// Token: 0x06000306 RID: 774
		bool IsSampleModel(DatabaseMoniker moniker);

		// Token: 0x06000307 RID: 775
		string GetModelName(string fileName);

		// Token: 0x06000308 RID: 776
		bool IsSampleModelVirtualServer(string virtualServer);
	}
}
