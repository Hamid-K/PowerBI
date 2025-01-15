using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x0200001D RID: 29
	public interface IString
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000082 RID: 130
		int Length { get; }

		// Token: 0x1700000B RID: 11
		char this[int i] { get; }
	}
}
