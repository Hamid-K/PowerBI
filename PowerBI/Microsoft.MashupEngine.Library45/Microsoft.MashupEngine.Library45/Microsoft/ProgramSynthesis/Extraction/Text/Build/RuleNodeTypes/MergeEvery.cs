using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F2F RID: 3887
	public struct MergeEvery : IProgramNodeBuilder, IEquatable<MergeEvery>
	{
		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06006BA9 RID: 27561 RVA: 0x00161636 File Offset: 0x0015F836
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006BAA RID: 27562 RVA: 0x0016163E File Offset: 0x0015F83E
		private MergeEvery(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006BAB RID: 27563 RVA: 0x00161647 File Offset: 0x0015F847
		public static MergeEvery CreateUnsafe(ProgramNode node)
		{
			return new MergeEvery(node);
		}

		// Token: 0x06006BAC RID: 27564 RVA: 0x00161650 File Offset: 0x0015F850
		public static MergeEvery? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MergeEvery)
			{
				return null;
			}
			return new MergeEvery?(MergeEvery.CreateUnsafe(node));
		}

		// Token: 0x06006BAD RID: 27565 RVA: 0x00161685 File Offset: 0x0015F885
		public MergeEvery(GrammarBuilders g, k value0, skip value1)
		{
			this._node = g.Rule.MergeEvery.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06006BAE RID: 27566 RVA: 0x001616AB File Offset: 0x0015F8AB
		public static implicit operator records(MergeEvery arg)
		{
			return records.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06006BAF RID: 27567 RVA: 0x001616B9 File Offset: 0x0015F8B9
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06006BB0 RID: 27568 RVA: 0x001616CD File Offset: 0x0015F8CD
		public skip skip
		{
			get
			{
				return skip.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006BB1 RID: 27569 RVA: 0x001616E1 File Offset: 0x0015F8E1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006BB2 RID: 27570 RVA: 0x001616F4 File Offset: 0x0015F8F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006BB3 RID: 27571 RVA: 0x0016171E File Offset: 0x0015F91E
		public bool Equals(MergeEvery other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F1A RID: 12058
		private ProgramNode _node;
	}
}
