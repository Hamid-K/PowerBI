using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A22 RID: 6690
	public struct ConstValue : IProgramNodeBuilder, IEquatable<ConstValue>
	{
		// Token: 0x170024C9 RID: 9417
		// (get) Token: 0x0600DBBE RID: 56254 RVA: 0x002EDEDA File Offset: 0x002EC0DA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DBBF RID: 56255 RVA: 0x002EDEE2 File Offset: 0x002EC0E2
		private ConstValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DBC0 RID: 56256 RVA: 0x002EDEEB File Offset: 0x002EC0EB
		public static ConstValue CreateUnsafe(ProgramNode node)
		{
			return new ConstValue(node);
		}

		// Token: 0x0600DBC1 RID: 56257 RVA: 0x002EDEF4 File Offset: 0x002EC0F4
		public static ConstValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConstValue)
			{
				return null;
			}
			return new ConstValue?(ConstValue.CreateUnsafe(node));
		}

		// Token: 0x0600DBC2 RID: 56258 RVA: 0x002EDF29 File Offset: 0x002EC129
		public ConstValue(GrammarBuilders g, str value0)
		{
			this._node = g.Rule.ConstValue.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DBC3 RID: 56259 RVA: 0x002EDF48 File Offset: 0x002EC148
		public static implicit operator value(ConstValue arg)
		{
			return value.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024CA RID: 9418
		// (get) Token: 0x0600DBC4 RID: 56260 RVA: 0x002EDF56 File Offset: 0x002EC156
		public str str
		{
			get
			{
				return str.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DBC5 RID: 56261 RVA: 0x002EDF6A File Offset: 0x002EC16A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DBC6 RID: 56262 RVA: 0x002EDF80 File Offset: 0x002EC180
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DBC7 RID: 56263 RVA: 0x002EDFAA File Offset: 0x002EC1AA
		public bool Equals(ConstValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005413 RID: 21523
		private ProgramNode _node;
	}
}
