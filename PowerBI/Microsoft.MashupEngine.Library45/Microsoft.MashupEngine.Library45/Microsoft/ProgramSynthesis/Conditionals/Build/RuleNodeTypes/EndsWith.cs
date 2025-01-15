using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A46 RID: 2630
	public struct EndsWith : IProgramNodeBuilder, IEquatable<EndsWith>
	{
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x000CB14A File Offset: 0x000C934A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x000CB152 File Offset: 0x000C9352
		private EndsWith(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x000CB15B File Offset: 0x000C935B
		public static EndsWith CreateUnsafe(ProgramNode node)
		{
			return new EndsWith(node);
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x000CB164 File Offset: 0x000C9364
		public static EndsWith? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.EndsWith)
			{
				return null;
			}
			return new EndsWith?(EndsWith.CreateUnsafe(node));
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x000CB199 File Offset: 0x000C9399
		public EndsWith(GrammarBuilders g, s value0, r value1)
		{
			this._node = g.Rule.EndsWith.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x000CB1BF File Offset: 0x000C93BF
		public static implicit operator match(EndsWith arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060040B2 RID: 16562 RVA: 0x000CB1CD File Offset: 0x000C93CD
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x000CB1E1 File Offset: 0x000C93E1
		public r r
		{
			get
			{
				return r.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x000CB1F5 File Offset: 0x000C93F5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x000CB208 File Offset: 0x000C9408
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x000CB232 File Offset: 0x000C9432
		public bool Equals(EndsWith other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D81 RID: 7553
		private ProgramNode _node;
	}
}
