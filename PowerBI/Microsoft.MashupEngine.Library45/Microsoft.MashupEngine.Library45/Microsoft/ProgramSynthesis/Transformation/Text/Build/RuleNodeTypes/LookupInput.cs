using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001BFF RID: 7167
	public struct LookupInput : IProgramNodeBuilder, IEquatable<LookupInput>
	{
		// Token: 0x17002825 RID: 10277
		// (get) Token: 0x0600F0FC RID: 61692 RVA: 0x0033EEB6 File Offset: 0x0033D0B6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F0FD RID: 61693 RVA: 0x0033EEBE File Offset: 0x0033D0BE
		private LookupInput(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F0FE RID: 61694 RVA: 0x0033EEC7 File Offset: 0x0033D0C7
		public static LookupInput CreateUnsafe(ProgramNode node)
		{
			return new LookupInput(node);
		}

		// Token: 0x0600F0FF RID: 61695 RVA: 0x0033EED0 File Offset: 0x0033D0D0
		public static LookupInput? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LookupInput)
			{
				return null;
			}
			return new LookupInput?(LookupInput.CreateUnsafe(node));
		}

		// Token: 0x0600F100 RID: 61696 RVA: 0x0033EF05 File Offset: 0x0033D105
		public LookupInput(GrammarBuilders g, vs value0, columnName value1)
		{
			this._node = g.Rule.LookupInput.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F101 RID: 61697 RVA: 0x0033EF2B File Offset: 0x0033D12B
		public static implicit operator lookupInput(LookupInput arg)
		{
			return lookupInput.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002826 RID: 10278
		// (get) Token: 0x0600F102 RID: 61698 RVA: 0x0033EF39 File Offset: 0x0033D139
		public vs vs
		{
			get
			{
				return vs.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002827 RID: 10279
		// (get) Token: 0x0600F103 RID: 61699 RVA: 0x0033EF4D File Offset: 0x0033D14D
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F104 RID: 61700 RVA: 0x0033EF61 File Offset: 0x0033D161
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F105 RID: 61701 RVA: 0x0033EF74 File Offset: 0x0033D174
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F106 RID: 61702 RVA: 0x0033EF9E File Offset: 0x0033D19E
		public bool Equals(LookupInput other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AEE RID: 23278
		private ProgramNode _node;
	}
}
