using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000A7E RID: 2686
	public interface IRichDataType : IEquatable<IRichDataType>
	{
		// Token: 0x060042C3 RID: 17091
		Optional<string> Canonicalize(string value);

		// Token: 0x060042C4 RID: 17092
		Optional<object> MaybeCastAsType(string value);

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060042C5 RID: 17093
		DataKind BaseKind { get; }

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060042C6 RID: 17094
		DataKind Kind { get; }

		// Token: 0x060042C7 RID: 17095
		bool IsValueOfType(string value);

		// Token: 0x060042C8 RID: 17096
		bool AddSample(string value);

		// Token: 0x060042C9 RID: 17097
		void Finish(long numSamples);

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060042CA RID: 17098
		int RejectionCount { get; }

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060042CB RID: 17099
		int SampleCount { get; }

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060042CC RID: 17100
		int AcceptanceCount { get; }

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060042CD RID: 17101
		int NaValueCount { get; }

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060042CE RID: 17102
		bool SuccessOnFinish { get; }

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060042CF RID: 17103
		bool EarlyFailure { get; }

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060042D0 RID: 17104
		long MinRequiredSamplesForSuccess { get; }

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060042D1 RID: 17105
		bool IsFinalized { get; }

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060042D2 RID: 17106
		// (set) Token: 0x060042D3 RID: 17107
		bool EmptyStringsExpectedInData { get; set; }

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060042D4 RID: 17108
		// (set) Token: 0x060042D5 RID: 17109
		bool NormalizableStringsExpectedInData { get; set; }

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060042D6 RID: 17110
		// (set) Token: 0x060042D7 RID: 17111
		bool NullsExpectedInData { get; set; }
	}
}
