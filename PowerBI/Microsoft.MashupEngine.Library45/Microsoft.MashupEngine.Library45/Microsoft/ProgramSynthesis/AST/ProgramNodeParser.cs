using System;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008DC RID: 2268
	// (Invoke) Token: 0x060030F4 RID: 12532
	public delegate ProgramNode ProgramNodeParser(string ast, Symbol symbol, ASTSerializationFormat format, ParseSettings settings);
}
