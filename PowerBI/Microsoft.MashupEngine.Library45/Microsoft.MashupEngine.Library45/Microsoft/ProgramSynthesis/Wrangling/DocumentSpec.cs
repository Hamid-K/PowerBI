using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x0200009F RID: 159
	[DebuggerDisplay("{Document}")]
	public class DocumentSpec<TRegion> : DocumentSpecInterface where TRegion : IRegion<TRegion>
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000D7A6 File Offset: 0x0000B9A6
		public TRegion Document { get; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000D7AE File Offset: 0x0000B9AE
		public IDictionary<string, IEnumerable<TRegion>> PositiveExamples { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000D7B6 File Offset: 0x0000B9B6
		public IDictionary<string, IEnumerable<TRegion>> NegativeExamples { get; }

		// Token: 0x060003C3 RID: 963 RVA: 0x0000D7BE File Offset: 0x0000B9BE
		public DocumentSpec(TRegion input, IDictionary<string, IEnumerable<TRegion>> examples, IDictionary<string, IEnumerable<TRegion>> negatives)
		{
			this.Document = input;
			this.PositiveExamples = examples;
			this.NegativeExamples = negatives;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000D7DB File Offset: 0x0000B9DB
		public override bool PositiveContainsKey(string name)
		{
			return this.PositiveExamples != null && this.PositiveExamples.ContainsKey(name);
		}
	}
}
