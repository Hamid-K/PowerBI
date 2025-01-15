using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E78 RID: 7800
	public struct SequenceMap : IProgramNodeBuilder, IEquatable<SequenceMap>
	{
		// Token: 0x17002BCD RID: 11213
		// (get) Token: 0x0601071A RID: 67354 RVA: 0x0038AABA File Offset: 0x00388CBA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601071B RID: 67355 RVA: 0x0038AAC2 File Offset: 0x00388CC2
		private SequenceMap(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601071C RID: 67356 RVA: 0x0038AACB File Offset: 0x00388CCB
		public static SequenceMap CreateUnsafe(ProgramNode node)
		{
			return new SequenceMap(node);
		}

		// Token: 0x0601071D RID: 67357 RVA: 0x0038AAD4 File Offset: 0x00388CD4
		public static SequenceMap? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SequenceMap)
			{
				return null;
			}
			return new SequenceMap?(SequenceMap.CreateUnsafe(node));
		}

		// Token: 0x0601071E RID: 67358 RVA: 0x0038AB09 File Offset: 0x00388D09
		public SequenceMap(GrammarBuilders g, newDsl value0, parentChildren value1)
		{
			this._node = g.Rule.SequenceMap.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x0601071F RID: 67359 RVA: 0x0038AB3B File Offset: 0x00388D3B
		public static implicit operator sequenceMap(SequenceMap arg)
		{
			return sequenceMap.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BCE RID: 11214
		// (get) Token: 0x06010720 RID: 67360 RVA: 0x0038AB49 File Offset: 0x00388D49
		public newDsl newDsl
		{
			get
			{
				return newDsl.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17002BCF RID: 11215
		// (get) Token: 0x06010721 RID: 67361 RVA: 0x0038AB64 File Offset: 0x00388D64
		public parentChildren parentChildren
		{
			get
			{
				return parentChildren.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06010722 RID: 67362 RVA: 0x0038AB78 File Offset: 0x00388D78
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010723 RID: 67363 RVA: 0x0038AB8C File Offset: 0x00388D8C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010724 RID: 67364 RVA: 0x0038ABB6 File Offset: 0x00388DB6
		public bool Equals(SequenceMap other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062B7 RID: 25271
		private ProgramNode _node;
	}
}
