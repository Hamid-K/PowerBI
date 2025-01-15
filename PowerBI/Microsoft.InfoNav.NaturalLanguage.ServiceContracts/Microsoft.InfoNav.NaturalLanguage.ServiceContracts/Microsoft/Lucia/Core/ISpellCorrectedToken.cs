using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000120 RID: 288
	public interface ISpellCorrectedToken : IToken
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005D9 RID: 1497
		SpellCorrectionSuggestion SpellCorrection { get; }
	}
}
