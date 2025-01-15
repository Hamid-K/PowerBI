using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C15 RID: 7189
	public struct RSubStr : IProgramNodeBuilder, IEquatable<RSubStr>
	{
		// Token: 0x17002867 RID: 10343
		// (get) Token: 0x0600F1EE RID: 61934 RVA: 0x003404B6 File Offset: 0x0033E6B6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F1EF RID: 61935 RVA: 0x003404BE File Offset: 0x0033E6BE
		private RSubStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F1F0 RID: 61936 RVA: 0x003404C7 File Offset: 0x0033E6C7
		public static RSubStr CreateUnsafe(ProgramNode node)
		{
			return new RSubStr(node);
		}

		// Token: 0x0600F1F1 RID: 61937 RVA: 0x003404D0 File Offset: 0x0033E6D0
		public static RSubStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RSubStr)
			{
				return null;
			}
			return new RSubStr?(RSubStr.CreateUnsafe(node));
		}

		// Token: 0x0600F1F2 RID: 61938 RVA: 0x00340505 File Offset: 0x0033E705
		public RSubStr(GrammarBuilders g, x value0, pl1 value1)
		{
			this._node = g.Rule.RSubStr.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F1F3 RID: 61939 RVA: 0x0034052B File Offset: 0x0033E72B
		public static implicit operator _LetB5(RSubStr arg)
		{
			return _LetB5.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002868 RID: 10344
		// (get) Token: 0x0600F1F4 RID: 61940 RVA: 0x00340539 File Offset: 0x0033E739
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002869 RID: 10345
		// (get) Token: 0x0600F1F5 RID: 61941 RVA: 0x0034054D File Offset: 0x0033E74D
		public pl1 pl1
		{
			get
			{
				return pl1.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F1F6 RID: 61942 RVA: 0x00340561 File Offset: 0x0033E761
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F1F7 RID: 61943 RVA: 0x00340574 File Offset: 0x0033E774
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F1F8 RID: 61944 RVA: 0x0034059E File Offset: 0x0033E79E
		public bool Equals(RSubStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B04 RID: 23300
		private ProgramNode _node;
	}
}
