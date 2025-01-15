using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000AB RID: 171
	public interface IExtractionProgram<TRegion> : IProgram where TRegion : IRegion<TRegion>
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003F6 RID: 1014
		ExtractionKind ExtractionKind { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003F7 RID: 1015
		ReferenceKind ReferenceKind { get; }

		// Token: 0x060003F8 RID: 1016
		IEnumerable<IEnumerable<TRegion>> RunExtraction(IEnumerable<TRegion> references);
	}
}
