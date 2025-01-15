using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200015A RID: 346
	public class SchemaElementProgram<TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x0001853D File Offset: 0x0001673D
		public SchemaElementProgram(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> e, int i)
		{
			this._element = e;
			this._programIndex = i;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00018553 File Offset: 0x00016753
		public IEnumerable<TRegion> Run(TRegion s)
		{
			return this._element.Run(s, this._programIndex);
		}

		// Token: 0x04000367 RID: 871
		private readonly SchemaElement<TSequenceProgram, TRegionProgram, TRegion> _element;

		// Token: 0x04000368 RID: 872
		private readonly int _programIndex;
	}
}
