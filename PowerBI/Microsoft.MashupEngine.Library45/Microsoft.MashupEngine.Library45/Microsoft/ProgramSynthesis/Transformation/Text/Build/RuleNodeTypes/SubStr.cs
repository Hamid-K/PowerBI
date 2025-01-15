using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C13 RID: 7187
	public struct SubStr : IProgramNodeBuilder, IEquatable<SubStr>
	{
		// Token: 0x17002861 RID: 10337
		// (get) Token: 0x0600F1D8 RID: 61912 RVA: 0x003402BE File Offset: 0x0033E4BE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F1D9 RID: 61913 RVA: 0x003402C6 File Offset: 0x0033E4C6
		private SubStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F1DA RID: 61914 RVA: 0x003402CF File Offset: 0x0033E4CF
		public static SubStr CreateUnsafe(ProgramNode node)
		{
			return new SubStr(node);
		}

		// Token: 0x0600F1DB RID: 61915 RVA: 0x003402D8 File Offset: 0x0033E4D8
		public static SubStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SubStr)
			{
				return null;
			}
			return new SubStr?(SubStr.CreateUnsafe(node));
		}

		// Token: 0x0600F1DC RID: 61916 RVA: 0x0034030D File Offset: 0x0033E50D
		public SubStr(GrammarBuilders g, x value0, PP value1)
		{
			this._node = g.Rule.SubStr.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F1DD RID: 61917 RVA: 0x00340333 File Offset: 0x0033E533
		public static implicit operator SS(SubStr arg)
		{
			return SS.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002862 RID: 10338
		// (get) Token: 0x0600F1DE RID: 61918 RVA: 0x00340341 File Offset: 0x0033E541
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002863 RID: 10339
		// (get) Token: 0x0600F1DF RID: 61919 RVA: 0x00340355 File Offset: 0x0033E555
		public PP PP
		{
			get
			{
				return PP.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F1E0 RID: 61920 RVA: 0x00340369 File Offset: 0x0033E569
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F1E1 RID: 61921 RVA: 0x0034037C File Offset: 0x0033E57C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F1E2 RID: 61922 RVA: 0x003403A6 File Offset: 0x0033E5A6
		public bool Equals(SubStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B02 RID: 23298
		private ProgramNode _node;
	}
}
