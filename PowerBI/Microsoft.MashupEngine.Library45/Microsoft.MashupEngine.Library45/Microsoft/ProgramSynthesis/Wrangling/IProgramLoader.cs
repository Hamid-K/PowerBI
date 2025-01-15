using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000BE RID: 190
	public interface IProgramLoader<out TProgram, TInput, TOutput> where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x06000451 RID: 1105
		TProgram Load(string serializedProgram, ASTSerializationFormat serializationFormat = ASTSerializationFormat.XML, DeserializationContext context = default(DeserializationContext));

		// Token: 0x06000452 RID: 1106
		TProgram Load(string serializedProgram, ASTSerializationFormat serializationFormat, DeserializationContext context, ProgramNodeParser programNodeParser);

		// Token: 0x06000453 RID: 1107
		TProgram Create(ProgramNode program);
	}
}
