using System;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FB0 RID: 4016
	public class FieldExtractionExample : Example<WebRegion, string[]>
	{
		// Token: 0x06006EF4 RID: 28404 RVA: 0x0016B026 File Offset: 0x00169226
		public FieldExtractionExample(WebRegion input, string[] output, int[] nodeIndexes = null)
			: base(input, output, false)
		{
			this.NodeIndexes = nodeIndexes ?? Enumerable.Repeat<int>(-1, output.Length).ToArray<int>();
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06006EF5 RID: 28405 RVA: 0x0016B04A File Offset: 0x0016924A
		public int[] NodeIndexes { get; }

		// Token: 0x06006EF6 RID: 28406 RVA: 0x0016B054 File Offset: 0x00169254
		public override bool Valid(Program<WebRegion, string[]> program)
		{
			string[] array = program.Run(base.Input);
			return array != null && base.Output.SequenceEqual(array);
		}
	}
}
