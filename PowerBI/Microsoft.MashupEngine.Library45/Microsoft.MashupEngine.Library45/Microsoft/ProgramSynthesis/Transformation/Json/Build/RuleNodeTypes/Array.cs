using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A27 RID: 6695
	public struct Array : IProgramNodeBuilder, IEquatable<Array>
	{
		// Token: 0x170024D6 RID: 9430
		// (get) Token: 0x0600DBF3 RID: 56307 RVA: 0x002EE396 File Offset: 0x002EC596
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DBF4 RID: 56308 RVA: 0x002EE39E File Offset: 0x002EC59E
		private Array(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DBF5 RID: 56309 RVA: 0x002EE3A7 File Offset: 0x002EC5A7
		public static Array CreateUnsafe(ProgramNode node)
		{
			return new Array(node);
		}

		// Token: 0x0600DBF6 RID: 56310 RVA: 0x002EE3B0 File Offset: 0x002EC5B0
		public static Array? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Array)
			{
				return null;
			}
			return new Array?(Array.CreateUnsafe(node));
		}

		// Token: 0x0600DBF7 RID: 56311 RVA: 0x002EE3E5 File Offset: 0x002EC5E5
		public Array(GrammarBuilders g, elements value0)
		{
			this._node = g.Rule.Array.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DBF8 RID: 56312 RVA: 0x002EE404 File Offset: 0x002EC604
		public static implicit operator array(Array arg)
		{
			return array.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024D7 RID: 9431
		// (get) Token: 0x0600DBF9 RID: 56313 RVA: 0x002EE412 File Offset: 0x002EC612
		public elements elements
		{
			get
			{
				return elements.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DBFA RID: 56314 RVA: 0x002EE426 File Offset: 0x002EC626
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DBFB RID: 56315 RVA: 0x002EE43C File Offset: 0x002EC63C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DBFC RID: 56316 RVA: 0x002EE466 File Offset: 0x002EC666
		public bool Equals(Array other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005418 RID: 21528
		private ProgramNode _node;
	}
}
