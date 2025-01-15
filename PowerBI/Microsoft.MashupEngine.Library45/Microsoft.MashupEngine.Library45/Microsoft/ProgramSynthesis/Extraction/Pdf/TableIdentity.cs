using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf
{
	// Token: 0x02000BD1 RID: 3025
	[NullableContext(1)]
	[Nullable(0)]
	public class TableIdentity
	{
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06004CB3 RID: 19635 RVA: 0x000F5136 File Offset: 0x000F3336
		public PdfAnalyzerOptions Options { get; }

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06004CB4 RID: 19636 RVA: 0x000F513E File Offset: 0x000F333E
		public string Identifier { get; }

		// Token: 0x06004CB5 RID: 19637 RVA: 0x000F5146 File Offset: 0x000F3346
		public TableIdentity(PdfAnalyzerOptions options, string identifier)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this.Options = options;
			this.Identifier = identifier;
		}
	}
}
