using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes
{
	// Token: 0x02000B63 RID: 2915
	public struct SequenceBody : IProgramNodeBuilder, IEquatable<SequenceBody>
	{
		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060049A5 RID: 18853 RVA: 0x000E82B6 File Offset: 0x000E64B6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x000E82BE File Offset: 0x000E64BE
		private SequenceBody(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x000E82C7 File Offset: 0x000E64C7
		public static SequenceBody CreateUnsafe(ProgramNode node)
		{
			return new SequenceBody(node);
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x000E82D0 File Offset: 0x000E64D0
		public static SequenceBody? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SequenceBody)
			{
				return null;
			}
			return new SequenceBody?(SequenceBody.CreateUnsafe(node));
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x000E8305 File Offset: 0x000E6505
		public SequenceBody(GrammarBuilders g, wrapStruct value0, selectSequence value1)
		{
			this._node = g.Rule.SequenceBody.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x000E8337 File Offset: 0x000E6537
		public static implicit operator sequenceBody(SequenceBody arg)
		{
			return sequenceBody.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x060049AB RID: 18859 RVA: 0x000E8345 File Offset: 0x000E6545
		public wrapStruct wrapStruct
		{
			get
			{
				return wrapStruct.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x060049AC RID: 18860 RVA: 0x000E8360 File Offset: 0x000E6560
		public selectSequence selectSequence
		{
			get
			{
				return selectSequence.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x000E8374 File Offset: 0x000E6574
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x000E8388 File Offset: 0x000E6588
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x000E83B2 File Offset: 0x000E65B2
		public bool Equals(SequenceBody other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400215E RID: 8542
		private ProgramNode _node;
	}
}
