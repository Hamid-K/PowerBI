using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E79 RID: 7801
	public struct ConvertSequence : IProgramNodeBuilder, IEquatable<ConvertSequence>
	{
		// Token: 0x17002BD0 RID: 11216
		// (get) Token: 0x06010725 RID: 67365 RVA: 0x0038ABCA File Offset: 0x00388DCA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010726 RID: 67366 RVA: 0x0038ABD2 File Offset: 0x00388DD2
		private ConvertSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010727 RID: 67367 RVA: 0x0038ABDB File Offset: 0x00388DDB
		public static ConvertSequence CreateUnsafe(ProgramNode node)
		{
			return new ConvertSequence(node);
		}

		// Token: 0x06010728 RID: 67368 RVA: 0x0038ABE4 File Offset: 0x00388DE4
		public static ConvertSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConvertSequence)
			{
				return null;
			}
			return new ConvertSequence?(ConvertSequence.CreateUnsafe(node));
		}

		// Token: 0x06010729 RID: 67369 RVA: 0x0038AC19 File Offset: 0x00388E19
		public ConvertSequence(GrammarBuilders g, select value0, sequenceMap value1)
		{
			this._node = new LetNode(g.Rule.ConvertSequence, value0.Node, value1.Node);
		}

		// Token: 0x0601072A RID: 67370 RVA: 0x0038AC3F File Offset: 0x00388E3F
		public static implicit operator convertSequence(ConvertSequence arg)
		{
			return convertSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BD1 RID: 11217
		// (get) Token: 0x0601072B RID: 67371 RVA: 0x0038AC4D File Offset: 0x00388E4D
		public select select
		{
			get
			{
				return select.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002BD2 RID: 11218
		// (get) Token: 0x0601072C RID: 67372 RVA: 0x0038AC61 File Offset: 0x00388E61
		public sequenceMap sequenceMap
		{
			get
			{
				return sequenceMap.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0601072D RID: 67373 RVA: 0x0038AC75 File Offset: 0x00388E75
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601072E RID: 67374 RVA: 0x0038AC88 File Offset: 0x00388E88
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601072F RID: 67375 RVA: 0x0038ACB2 File Offset: 0x00388EB2
		public bool Equals(ConvertSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062B8 RID: 25272
		private ProgramNode _node;
	}
}
