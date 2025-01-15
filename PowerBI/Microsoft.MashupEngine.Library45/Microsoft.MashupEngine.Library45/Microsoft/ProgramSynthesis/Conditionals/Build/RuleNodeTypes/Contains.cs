using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A47 RID: 2631
	public struct Contains : IProgramNodeBuilder, IEquatable<Contains>
	{
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060040B7 RID: 16567 RVA: 0x000CB246 File Offset: 0x000C9446
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x000CB24E File Offset: 0x000C944E
		private Contains(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x000CB257 File Offset: 0x000C9457
		public static Contains CreateUnsafe(ProgramNode node)
		{
			return new Contains(node);
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x000CB260 File Offset: 0x000C9460
		public static Contains? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Contains)
			{
				return null;
			}
			return new Contains?(Contains.CreateUnsafe(node));
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x000CB295 File Offset: 0x000C9495
		public Contains(GrammarBuilders g, s value0, r value1, k value2)
		{
			this._node = g.Rule.Contains.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x000CB2C2 File Offset: 0x000C94C2
		public static implicit operator match(Contains arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060040BD RID: 16573 RVA: 0x000CB2D0 File Offset: 0x000C94D0
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060040BE RID: 16574 RVA: 0x000CB2E4 File Offset: 0x000C94E4
		public r r
		{
			get
			{
				return r.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060040BF RID: 16575 RVA: 0x000CB2F8 File Offset: 0x000C94F8
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x000CB30C File Offset: 0x000C950C
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x000CB320 File Offset: 0x000C9520
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x000CB34A File Offset: 0x000C954A
		public bool Equals(Contains other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D82 RID: 7554
		private ProgramNode _node;
	}
}
