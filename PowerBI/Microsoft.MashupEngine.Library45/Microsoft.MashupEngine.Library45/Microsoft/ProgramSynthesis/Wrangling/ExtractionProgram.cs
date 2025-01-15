using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000A6 RID: 166
	public abstract class ExtractionProgram<TExtractionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x060003EC RID: 1004
		public abstract IEnumerable<TRegion> Run(TRegion reference);

		// Token: 0x060003ED RID: 1005
		public abstract IEnumerable<Example<TRegion, TRegion>> Run(IEnumerable<TRegion> references);

		// Token: 0x060003EE RID: 1006 RVA: 0x0000DF44 File Offset: 0x0000C144
		public IEnumerable<TRegion> OutputRun(IEnumerable<TRegion> references)
		{
			return from r in this.Run(references)
				select r.Output;
		}
	}
}
