using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E73 RID: 7795
	public struct Prepend : IProgramNodeBuilder, IEquatable<Prepend>
	{
		// Token: 0x17002BC0 RID: 11200
		// (get) Token: 0x060106E5 RID: 67301 RVA: 0x0038A5DE File Offset: 0x003887DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060106E6 RID: 67302 RVA: 0x0038A5E6 File Offset: 0x003887E6
		private Prepend(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060106E7 RID: 67303 RVA: 0x0038A5EF File Offset: 0x003887EF
		public static Prepend CreateUnsafe(ProgramNode node)
		{
			return new Prepend(node);
		}

		// Token: 0x060106E8 RID: 67304 RVA: 0x0038A5F8 File Offset: 0x003887F8
		public static Prepend? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Prepend)
			{
				return null;
			}
			return new Prepend?(Prepend.CreateUnsafe(node));
		}

		// Token: 0x060106E9 RID: 67305 RVA: 0x0038A62D File Offset: 0x0038882D
		public Prepend(GrammarBuilders g, interval value0, children value1)
		{
			this._node = g.Rule.Prepend.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060106EA RID: 67306 RVA: 0x0038A653 File Offset: 0x00388853
		public static implicit operator children(Prepend arg)
		{
			return children.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BC1 RID: 11201
		// (get) Token: 0x060106EB RID: 67307 RVA: 0x0038A661 File Offset: 0x00388861
		public interval interval
		{
			get
			{
				return interval.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002BC2 RID: 11202
		// (get) Token: 0x060106EC RID: 67308 RVA: 0x0038A675 File Offset: 0x00388875
		public children children
		{
			get
			{
				return children.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060106ED RID: 67309 RVA: 0x0038A689 File Offset: 0x00388889
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060106EE RID: 67310 RVA: 0x0038A69C File Offset: 0x0038889C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060106EF RID: 67311 RVA: 0x0038A6C6 File Offset: 0x003888C6
		public bool Equals(Prepend other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062B2 RID: 25266
		private ProgramNode _node;
	}
}
