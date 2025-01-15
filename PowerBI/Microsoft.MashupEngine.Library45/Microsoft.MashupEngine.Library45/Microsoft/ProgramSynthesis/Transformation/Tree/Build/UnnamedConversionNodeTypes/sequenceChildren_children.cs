using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001E5B RID: 7771
	public struct sequenceChildren_children : IProgramNodeBuilder, IEquatable<sequenceChildren_children>
	{
		// Token: 0x17002B76 RID: 11126
		// (get) Token: 0x060105DB RID: 67035 RVA: 0x00388DA2 File Offset: 0x00386FA2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060105DC RID: 67036 RVA: 0x00388DAA File Offset: 0x00386FAA
		private sequenceChildren_children(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060105DD RID: 67037 RVA: 0x00388DB3 File Offset: 0x00386FB3
		public static sequenceChildren_children CreateUnsafe(ProgramNode node)
		{
			return new sequenceChildren_children(node);
		}

		// Token: 0x060105DE RID: 67038 RVA: 0x00388DBC File Offset: 0x00386FBC
		public static sequenceChildren_children? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.sequenceChildren_children)
			{
				return null;
			}
			return new sequenceChildren_children?(sequenceChildren_children.CreateUnsafe(node));
		}

		// Token: 0x060105DF RID: 67039 RVA: 0x00388DF1 File Offset: 0x00386FF1
		public sequenceChildren_children(GrammarBuilders g, children value0)
		{
			this._node = g.UnnamedConversion.sequenceChildren_children.BuildASTNode(value0.Node);
		}

		// Token: 0x060105E0 RID: 67040 RVA: 0x00388E10 File Offset: 0x00387010
		public static implicit operator sequenceChildren(sequenceChildren_children arg)
		{
			return sequenceChildren.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B77 RID: 11127
		// (get) Token: 0x060105E1 RID: 67041 RVA: 0x00388E1E File Offset: 0x0038701E
		public children children
		{
			get
			{
				return children.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060105E2 RID: 67042 RVA: 0x00388E32 File Offset: 0x00387032
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060105E3 RID: 67043 RVA: 0x00388E48 File Offset: 0x00387048
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060105E4 RID: 67044 RVA: 0x00388E72 File Offset: 0x00387072
		public bool Equals(sequenceChildren_children other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400629A RID: 25242
		private ProgramNode _node;
	}
}
