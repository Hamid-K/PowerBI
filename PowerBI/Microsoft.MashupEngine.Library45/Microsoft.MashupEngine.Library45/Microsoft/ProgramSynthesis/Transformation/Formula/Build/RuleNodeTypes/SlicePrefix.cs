using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200157C RID: 5500
	public struct SlicePrefix : IProgramNodeBuilder, IEquatable<SlicePrefix>
	{
		// Token: 0x17001F69 RID: 8041
		// (get) Token: 0x0600B3FA RID: 46074 RVA: 0x00274252 File Offset: 0x00272452
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B3FB RID: 46075 RVA: 0x0027425A File Offset: 0x0027245A
		private SlicePrefix(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B3FC RID: 46076 RVA: 0x00274263 File Offset: 0x00272463
		public static SlicePrefix CreateUnsafe(ProgramNode node)
		{
			return new SlicePrefix(node);
		}

		// Token: 0x0600B3FD RID: 46077 RVA: 0x0027426C File Offset: 0x0027246C
		public static SlicePrefix? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SlicePrefix)
			{
				return null;
			}
			return new SlicePrefix?(SlicePrefix.CreateUnsafe(node));
		}

		// Token: 0x0600B3FE RID: 46078 RVA: 0x002742A1 File Offset: 0x002724A1
		public SlicePrefix(GrammarBuilders g, x value0, pos value1)
		{
			this._node = g.Rule.SlicePrefix.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B3FF RID: 46079 RVA: 0x002742C7 File Offset: 0x002724C7
		public static implicit operator substring(SlicePrefix arg)
		{
			return substring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F6A RID: 8042
		// (get) Token: 0x0600B400 RID: 46080 RVA: 0x002742D5 File Offset: 0x002724D5
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F6B RID: 8043
		// (get) Token: 0x0600B401 RID: 46081 RVA: 0x002742E9 File Offset: 0x002724E9
		public pos pos
		{
			get
			{
				return pos.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B402 RID: 46082 RVA: 0x002742FD File Offset: 0x002724FD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B403 RID: 46083 RVA: 0x00274310 File Offset: 0x00272510
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B404 RID: 46084 RVA: 0x0027433A File Offset: 0x0027253A
		public bool Equals(SlicePrefix other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400462A RID: 17962
		private ProgramNode _node;
	}
}
