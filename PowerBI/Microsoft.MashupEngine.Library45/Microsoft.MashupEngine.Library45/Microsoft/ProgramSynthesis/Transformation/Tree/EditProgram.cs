using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Tree.Learning;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree
{
	// Token: 0x02001E31 RID: 7729
	[DebuggerDisplay("ProgramNode")]
	public class EditProgram : TransformationProgram<EditProgram, Node, Node>
	{
		// Token: 0x06010202 RID: 66050 RVA: 0x00375A6E File Offset: 0x00373C6E
		public EditProgram(ProgramNode node, double score)
			: base(node, score, null)
		{
		}

		// Token: 0x06010203 RID: 66051 RVA: 0x00375A79 File Offset: 0x00373C79
		public EditProgram(ProgramNode node, Feature<double> scoreFeature = null)
			: base(node, node.GetFeatureValue<double>(scoreFeature ?? new RankingScore(Language.Grammar, null), null), null)
		{
		}

		// Token: 0x06010204 RID: 66052 RVA: 0x00375A9C File Offset: 0x00373C9C
		public override Node Run(Node input)
		{
			KeyValuePair<Symbol, object> keyValuePair = new KeyValuePair<Symbol, object>(Language.Grammar.InputSymbol, input);
			KeyValuePair<Symbol, object> keyValuePair2 = new KeyValuePair<Symbol, object>(Language.Build.Symbol.selectedNode, input);
			State state = State.CreateForExecution(new KeyValuePair<Symbol, object>[] { keyValuePair, keyValuePair2 });
			return base.ProgramNode.Invoke(state) as Node;
		}

		// Token: 0x06010205 RID: 66053 RVA: 0x00375AFF File Offset: 0x00373CFF
		public string Serialize()
		{
			return base.ProgramNode.PrintAST(ASTSerializationFormat.XML);
		}

		// Token: 0x06010206 RID: 66054 RVA: 0x00375B0D File Offset: 0x00373D0D
		public bool Equals(EditProgram other)
		{
			return other != null && (this == other || object.Equals(base.ProgramNode, other.ProgramNode));
		}

		// Token: 0x06010207 RID: 66055 RVA: 0x00375B2B File Offset: 0x00373D2B
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((EditProgram)obj)));
		}

		// Token: 0x06010208 RID: 66056 RVA: 0x00375B59 File Offset: 0x00373D59
		public override int GetHashCode()
		{
			ProgramNode programNode = base.ProgramNode;
			if (programNode == null)
			{
				return 0;
			}
			return programNode.GetHashCode();
		}

		// Token: 0x06010209 RID: 66057 RVA: 0x00375B6C File Offset: 0x00373D6C
		public override string ToString()
		{
			return string.Join(Environment.NewLine, base.ProgramNode.AcceptVisitor<IReadOnlyList<string>>(new PrettyPrintVisitor()));
		}
	}
}
