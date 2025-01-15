using System;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004C4 RID: 1220
	public interface ICalibrator
	{
		// Token: 0x060018F9 RID: 6393
		float PredictProbability(float output);

		// Token: 0x060018FA RID: 6394
		string GetSummary();
	}
}
