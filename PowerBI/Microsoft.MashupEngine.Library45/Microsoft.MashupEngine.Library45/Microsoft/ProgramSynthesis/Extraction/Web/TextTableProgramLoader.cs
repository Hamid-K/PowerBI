using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FD6 RID: 4054
	public class TextTableProgramLoader : SimpleSymbolProgramLoader<TextTableProgram, WebRegion, IEnumerable<IEnumerable<string>>>
	{
		// Token: 0x06006FE3 RID: 28643 RVA: 0x0016DA7B File Offset: 0x0016BC7B
		private TextTableProgramLoader()
		{
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06006FE4 RID: 28644 RVA: 0x0016DA83 File Offset: 0x0016BC83
		public static TextTableProgramLoader Instance { get; } = new TextTableProgramLoader();

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x06006FE5 RID: 28645 RVA: 0x0016DA8A File Offset: 0x0016BC8A
		protected override Symbol StartSymbol
		{
			get
			{
				return TextTableProgram.ProgramSymbol;
			}
		}

		// Token: 0x06006FE6 RID: 28646 RVA: 0x0016DA5F File Offset: 0x0016BC5F
		public override TextTableProgram Create(ProgramNode program)
		{
			return new TextTableProgram(Language.Build.Node.Cast.resultTable(program));
		}
	}
}
