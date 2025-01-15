using System;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FB1 RID: 4017
	public class FieldProgram : Program<WebRegion, string[]>
	{
		// Token: 0x06006EF7 RID: 28407 RVA: 0x0016B07F File Offset: 0x0016927F
		public FieldProgram(ProgramNode programNode)
			: base(programNode, 0.0, null)
		{
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x06006EF8 RID: 28408 RVA: 0x0016B092 File Offset: 0x00169292
		public static Symbol ProgramSymbol { get; } = Language.Build.Symbol.resultFields;

		// Token: 0x06006EF9 RID: 28409 RVA: 0x0016B09C File Offset: 0x0016929C
		public override string[] Run(WebRegion input)
		{
			State state = State.CreateForExecution(base.ProgramNode.Grammar.InputSymbol, input.GetAllChildrenAndSelf().ToArray<IDomNode>());
			return base.ProgramNode.Invoke(state) as string[];
		}
	}
}
