using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.Element;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.TreeOutput
{
	// Token: 0x02000133 RID: 307
	public interface ITreeOutput<TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006DE RID: 1758
		// (set) Token: 0x060006DF RID: 1759
		string Name { get; set; }

		// Token: 0x060006E0 RID: 1760
		IEnumerable<IReadOnlyList<TRegion>> ToTable(ISchemaElement<TRegion> schema, TreeToTableSemantics semantics);
	}
}
