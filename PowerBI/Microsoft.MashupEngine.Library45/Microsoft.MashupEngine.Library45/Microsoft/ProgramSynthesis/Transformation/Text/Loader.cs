using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001B97 RID: 7063
	public class Loader : IProgramLoader<Program, IRow, object>
	{
		// Token: 0x0600E74B RID: 59211 RVA: 0x003105C1 File Offset: 0x0030E7C1
		private Loader()
		{
		}

		// Token: 0x1700269B RID: 9883
		// (get) Token: 0x0600E74C RID: 59212 RVA: 0x003105EE File Offset: 0x0030E7EE
		public static Loader Instance { get; } = new Loader();

		// Token: 0x0600E74D RID: 59213 RVA: 0x003105F5 File Offset: 0x0030E7F5
		public Program Load(string serializedProgram, ASTSerializationFormat serializationFormat = ASTSerializationFormat.XML, DeserializationContext context = default(DeserializationContext))
		{
			ProgramNodeParser programNodeParser;
			if ((programNodeParser = Loader.<>O.<0>__Parse) == null)
			{
				programNodeParser = (Loader.<>O.<0>__Parse = new ProgramNodeParser(ProgramNode.Parse));
			}
			return this.Load(serializedProgram, serializationFormat, context, programNodeParser);
		}

		// Token: 0x0600E74E RID: 59214 RVA: 0x0031061C File Offset: 0x0030E81C
		public Program Load(string serializedProgram, ASTSerializationFormat serializationFormat, DeserializationContext context, ProgramNodeParser programNodeParser)
		{
			ParseSettings parseSettings = new ParseSettings(context, Language.Build.Rule.Predicate);
			ProgramNode programNode = programNodeParser(serializedProgram, Language.Grammar.StartSymbol, serializationFormat, parseSettings);
			@switch @switch;
			if (programNode != null)
			{
				@switch = Language.Build.Node.Cast.@switch(programNode);
			}
			else
			{
				ProgramNode programNode2 = programNodeParser(serializedProgram, this._sym.st, serializationFormat, parseSettings);
				if (programNode2 != null)
				{
					@switch = this._rule.SingleBranch(Language.Build.Node.Cast.st(programNode2));
				}
				else
				{
					ProgramNode programNode3 = programNodeParser(serializedProgram, this._sym.e, serializationFormat, parseSettings);
					if (!(programNode3 != null))
					{
						return null;
					}
					@switch = this._rule.SingleBranch(this._rule.Transformation(Language.Build.Node.Cast.e(programNode3)));
				}
			}
			return new Program(@switch, null);
		}

		// Token: 0x0600E74F RID: 59215 RVA: 0x00310720 File Offset: 0x0030E920
		public Program Create(ProgramNode program)
		{
			return new Program(Language.Build.Node.Cast.@switch(program), null);
		}

		// Token: 0x04005811 RID: 22545
		private readonly GrammarBuilders.GrammarSymbols _sym = Language.Build.Symbol;

		// Token: 0x04005812 RID: 22546
		private readonly GrammarBuilders.Nodes.NodeRules _rule = Language.Build.Node.Rule;

		// Token: 0x02001B98 RID: 7064
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005813 RID: 22547
			public static ProgramNodeParser <0>__Parse;
		}
	}
}
