using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000B3 RID: 179
	public interface IFourierDistributionSampler : ICanSaveModel
	{
		// Token: 0x0600039B RID: 923
		float Next(IRandom rand);
	}
}
