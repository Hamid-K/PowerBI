using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Learners
{
	// Token: 0x0200047C RID: 1148
	public interface IPartitioner
	{
		// Token: 0x0600180A RID: 6154
		RoleMappedData[] GetPartitions(IHostEnvironment env, RoleMappedData data, int numPartitions);
	}
}
