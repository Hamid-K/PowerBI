using System;
using System.Collections.Generic;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000003 RID: 3
	public interface IEvaluator
	{
		// Token: 0x06000001 RID: 1
		Dictionary<string, IDataView> Evaluate(RoleMappedData data);

		// Token: 0x06000002 RID: 2
		IDataTransform GetPerInstanceMetrics(RoleMappedData data);

		// Token: 0x06000003 RID: 3
		IEnumerable<MetricColumn> GetOverallMetricColumns();
	}
}
