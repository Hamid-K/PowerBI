using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E61 RID: 7777
	public struct IsKind : IProgramNodeBuilder, IEquatable<IsKind>
	{
		// Token: 0x17002B84 RID: 11140
		// (get) Token: 0x06010619 RID: 67097 RVA: 0x0038932A File Offset: 0x0038752A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601061A RID: 67098 RVA: 0x00389332 File Offset: 0x00387532
		private IsKind(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601061B RID: 67099 RVA: 0x0038933B File Offset: 0x0038753B
		public static IsKind CreateUnsafe(ProgramNode node)
		{
			return new IsKind(node);
		}

		// Token: 0x0601061C RID: 67100 RVA: 0x00389344 File Offset: 0x00387544
		public static IsKind? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IsKind)
			{
				return null;
			}
			return new IsKind?(IsKind.CreateUnsafe(node));
		}

		// Token: 0x0601061D RID: 67101 RVA: 0x00389379 File Offset: 0x00387579
		public IsKind(GrammarBuilders g, x value0, path value1, kind value2)
		{
			this._node = g.Rule.IsKind.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0601061E RID: 67102 RVA: 0x003893A6 File Offset: 0x003875A6
		public static implicit operator pred(IsKind arg)
		{
			return pred.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B85 RID: 11141
		// (get) Token: 0x0601061F RID: 67103 RVA: 0x003893B4 File Offset: 0x003875B4
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002B86 RID: 11142
		// (get) Token: 0x06010620 RID: 67104 RVA: 0x003893C8 File Offset: 0x003875C8
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002B87 RID: 11143
		// (get) Token: 0x06010621 RID: 67105 RVA: 0x003893DC File Offset: 0x003875DC
		public kind kind
		{
			get
			{
				return kind.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06010622 RID: 67106 RVA: 0x003893F0 File Offset: 0x003875F0
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010623 RID: 67107 RVA: 0x00389404 File Offset: 0x00387604
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010624 RID: 67108 RVA: 0x0038942E File Offset: 0x0038762E
		public bool Equals(IsKind other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062A0 RID: 25248
		private ProgramNode _node;
	}
}
