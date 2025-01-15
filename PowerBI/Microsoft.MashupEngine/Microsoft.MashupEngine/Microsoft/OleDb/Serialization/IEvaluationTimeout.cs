using System;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FB6 RID: 8118
	public interface IEvaluationTimeout
	{
		// Token: 0x17003014 RID: 12308
		// (get) Token: 0x0600C60E RID: 50702
		bool TimedOut { get; }

		// Token: 0x0600C60F RID: 50703
		void DisableTimeout();

		// Token: 0x0600C610 RID: 50704
		void EnableTimeout();
	}
}
