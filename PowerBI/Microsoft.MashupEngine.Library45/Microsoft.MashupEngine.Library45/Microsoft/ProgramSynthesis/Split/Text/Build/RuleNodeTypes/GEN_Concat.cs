using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001355 RID: 4949
	public struct GEN_Concat : IProgramNodeBuilder, IEquatable<GEN_Concat>
	{
		// Token: 0x17001A4B RID: 6731
		// (get) Token: 0x060098B6 RID: 39094 RVA: 0x0020729A File Offset: 0x0020549A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060098B7 RID: 39095 RVA: 0x002072A2 File Offset: 0x002054A2
		private GEN_Concat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060098B8 RID: 39096 RVA: 0x002072AB File Offset: 0x002054AB
		public static GEN_Concat CreateUnsafe(ProgramNode node)
		{
			return new GEN_Concat(node);
		}

		// Token: 0x060098B9 RID: 39097 RVA: 0x002072B4 File Offset: 0x002054B4
		public static GEN_Concat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.GEN_Concat)
			{
				return null;
			}
			return new GEN_Concat?(GEN_Concat.CreateUnsafe(node));
		}

		// Token: 0x060098BA RID: 39098 RVA: 0x002072E9 File Offset: 0x002054E9
		public GEN_Concat(GrammarBuilders g, obj value0, obj value1)
		{
			this._node = g.Rule.GEN_Concat.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060098BB RID: 39099 RVA: 0x0020730F File Offset: 0x0020550F
		public static implicit operator gen_Concat(GEN_Concat arg)
		{
			return gen_Concat.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A4C RID: 6732
		// (get) Token: 0x060098BC RID: 39100 RVA: 0x0020731D File Offset: 0x0020551D
		public obj obj1
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A4D RID: 6733
		// (get) Token: 0x060098BD RID: 39101 RVA: 0x00207331 File Offset: 0x00205531
		public obj obj2
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060098BE RID: 39102 RVA: 0x00207345 File Offset: 0x00205545
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060098BF RID: 39103 RVA: 0x00207358 File Offset: 0x00205558
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060098C0 RID: 39104 RVA: 0x00207382 File Offset: 0x00205582
		public bool Equals(GEN_Concat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DCC RID: 15820
		private ProgramNode _node;
	}
}
