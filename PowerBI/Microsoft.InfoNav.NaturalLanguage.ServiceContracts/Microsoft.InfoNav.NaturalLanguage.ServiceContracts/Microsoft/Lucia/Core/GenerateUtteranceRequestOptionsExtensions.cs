using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000145 RID: 325
	public static class GenerateUtteranceRequestOptionsExtensions
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x0000B514 File Offset: 0x00009714
		internal static bool ShouldGenerateSpans(this GenerateUtteranceRequestOptions option)
		{
			return GenerateUtteranceRequestOptionsExtensions.HasFlag(option, GenerateUtteranceRequestOptions.GenerateSpans);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0000B51D File Offset: 0x0000971D
		internal static bool ShouldVerifyResult(this GenerateUtteranceRequestOptions option)
		{
			return GenerateUtteranceRequestOptionsExtensions.HasFlag(option, GenerateUtteranceRequestOptions.VerifyResult);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0000B526 File Offset: 0x00009726
		private static bool HasFlag(GenerateUtteranceRequestOptions option, GenerateUtteranceRequestOptions flag)
		{
			return (option & flag) == flag;
		}
	}
}
