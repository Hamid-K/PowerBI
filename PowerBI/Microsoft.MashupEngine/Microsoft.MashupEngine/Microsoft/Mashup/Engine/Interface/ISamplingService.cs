using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000AF RID: 175
	public interface ISamplingService
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002F2 RID: 754
		bool SamplingEnabled { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002F3 RID: 755
		int SampleRowCount { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002F4 RID: 756
		bool Sampled { get; }

		// Token: 0x060002F5 RID: 757
		void RecordSampling();
	}
}
