using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes
{
	// Token: 0x02000B60 RID: 2912
	public struct DummySequence : IProgramNodeBuilder, IEquatable<DummySequence>
	{
		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x000E7FDA File Offset: 0x000E61DA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x000E7FE2 File Offset: 0x000E61E2
		private DummySequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x000E7FEB File Offset: 0x000E61EB
		public static DummySequence CreateUnsafe(ProgramNode node)
		{
			return new DummySequence(node);
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x000E7FF4 File Offset: 0x000E61F4
		public static DummySequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DummySequence)
			{
				return null;
			}
			return new DummySequence?(DummySequence.CreateUnsafe(node));
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x000E8029 File Offset: 0x000E6229
		public DummySequence(GrammarBuilders g, sequenceBody value0)
		{
			this._node = g.Rule.DummySequence.BuildASTNode(value0.Node);
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x000E8048 File Offset: 0x000E6248
		public static implicit operator sequence(DummySequence arg)
		{
			return sequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x0600498B RID: 18827 RVA: 0x000E8056 File Offset: 0x000E6256
		public sequenceBody sequenceBody
		{
			get
			{
				return sequenceBody.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x000E806A File Offset: 0x000E626A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x000E8080 File Offset: 0x000E6280
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x000E80AA File Offset: 0x000E62AA
		public bool Equals(DummySequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400215B RID: 8539
		private ProgramNode _node;
	}
}
