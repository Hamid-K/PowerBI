using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000BF RID: 191
	public abstract class SimpleSymbolProgramLoader<TProgram, TInput, TOutput> : IProgramLoader<TProgram, TInput, TOutput> where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000454 RID: 1108
		protected abstract Symbol StartSymbol { get; }

		// Token: 0x06000455 RID: 1109 RVA: 0x0000E9DD File Offset: 0x0000CBDD
		public virtual TProgram Load(string serializedProgram, ASTSerializationFormat serializationFormat = ASTSerializationFormat.XML, DeserializationContext context = default(DeserializationContext))
		{
			ProgramNodeParser programNodeParser;
			if ((programNodeParser = SimpleSymbolProgramLoader<TProgram, TInput, TOutput>.<>O.<0>__Parse) == null)
			{
				programNodeParser = (SimpleSymbolProgramLoader<TProgram, TInput, TOutput>.<>O.<0>__Parse = new ProgramNodeParser(ProgramNode.Parse));
			}
			return this.Load(serializedProgram, serializationFormat, context, programNodeParser);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000EA04 File Offset: 0x0000CC04
		public TProgram Load(string serializedProgram, ASTSerializationFormat serializationFormat, DeserializationContext context, ProgramNodeParser programNodeParser)
		{
			ParseSettings parseSettings = new ParseSettings(context, null);
			ProgramNode programNode = programNodeParser(serializedProgram, this.StartSymbol, serializationFormat, parseSettings);
			if (!(programNode == null))
			{
				return this.Create(programNode);
			}
			return default(TProgram);
		}

		// Token: 0x06000457 RID: 1111
		public abstract TProgram Create(ProgramNode program);

		// Token: 0x020000C0 RID: 192
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040001E7 RID: 487
			public static ProgramNodeParser <0>__Parse;
		}
	}
}
