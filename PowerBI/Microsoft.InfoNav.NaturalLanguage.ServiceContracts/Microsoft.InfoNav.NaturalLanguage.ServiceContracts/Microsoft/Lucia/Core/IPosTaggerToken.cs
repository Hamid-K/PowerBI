using System;
using System.Collections.ObjectModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200011F RID: 287
	public interface IPosTaggerToken : ISpellCorrectedToken, IToken
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005D8 RID: 1496
		ReadOnlyCollection<PosTag> PosTags { get; }
	}
}
