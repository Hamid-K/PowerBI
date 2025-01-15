using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep;

namespace Microsoft.ProgramSynthesis.Extraction.Json
{
	// Token: 0x02000B27 RID: 2855
	internal sealed class ExtractionKindVisitor : ProgramNodeVisitor<Program.ExtractionKind>
	{
		// Token: 0x06004743 RID: 18243 RVA: 0x000DF538 File Offset: 0x000DD738
		public override Program.ExtractionKind VisitNonterminal(NonterminalNode node)
		{
			if (Language.Build.Node.Is.@struct(node))
			{
				return Program.ExtractionKind.Object;
			}
			sequence sequence;
			if (Language.Build.Node.Is.sequence(node, out sequence))
			{
				DummySequence dummySequence;
				if (sequence.Is_DummySequence(Language.Build, out dummySequence))
				{
					JPath value = dummySequence.sequenceBody.Cast_SequenceBody().selectSequence.Cast_SelectSequence().path.Value;
					if (value == null || value.Steps.Length == 0)
					{
						return Program.ExtractionKind.Unknown;
					}
					if (value.Steps[0].Kind == JPathStepKind.Access)
					{
						return Program.ExtractionKind.SingleArrayObject;
					}
				}
				return Program.ExtractionKind.Array;
			}
			return this.VisitChildren(node);
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x000DF5E3 File Offset: 0x000DD7E3
		public override Program.ExtractionKind VisitLet(LetNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x000DF5E3 File Offset: 0x000DD7E3
		public override Program.ExtractionKind VisitLambda(LambdaNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x0001B159 File Offset: 0x00019359
		public override Program.ExtractionKind VisitLiteral(LiteralNode node)
		{
			return Program.ExtractionKind.Unknown;
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x0001B159 File Offset: 0x00019359
		public override Program.ExtractionKind VisitVariable(VariableNode node)
		{
			return Program.ExtractionKind.Unknown;
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x0001B159 File Offset: 0x00019359
		public override Program.ExtractionKind VisitHole(Hole node)
		{
			return Program.ExtractionKind.Unknown;
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x000DF5EC File Offset: 0x000DD7EC
		private Program.ExtractionKind VisitChildren(ProgramNode node)
		{
			ProgramNode[] children = node.Children;
			for (int i = 0; i < children.Length; i++)
			{
				Program.ExtractionKind extractionKind = children[i].AcceptVisitor<Program.ExtractionKind>(this);
				if (extractionKind != Program.ExtractionKind.Unknown)
				{
					return extractionKind;
				}
			}
			return Program.ExtractionKind.Unknown;
		}
	}
}
