using System;

namespace Microsoft.ProgramSynthesis.Detection.Encoding.Detectors
{
	// Token: 0x02000AE1 RID: 2785
	internal interface IEncodingDetector
	{
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060045C2 RID: 17858
		int Precedence { get; }

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x060045C3 RID: 17859
		float Confidence { get; }

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060045C4 RID: 17860
		EncodingType DetectedType { get; }

		// Token: 0x060045C5 RID: 17861
		void ConsumeHeader(byte[] buffer);
	}
}
