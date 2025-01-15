using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E84 RID: 7812
	public struct parentChildren : IProgramNodeBuilder, IEquatable<parentChildren>
	{
		// Token: 0x17002BDD RID: 11229
		// (get) Token: 0x060107EA RID: 67562 RVA: 0x0038CB12 File Offset: 0x0038AD12
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060107EB RID: 67563 RVA: 0x0038CB1A File Offset: 0x0038AD1A
		private parentChildren(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060107EC RID: 67564 RVA: 0x0038CB23 File Offset: 0x0038AD23
		public static parentChildren CreateUnsafe(ProgramNode node)
		{
			return new parentChildren(node);
		}

		// Token: 0x060107ED RID: 67565 RVA: 0x0038CB2C File Offset: 0x0038AD2C
		public static parentChildren? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.parentChildren)
			{
				return null;
			}
			return new parentChildren?(parentChildren.CreateUnsafe(node));
		}

		// Token: 0x060107EE RID: 67566 RVA: 0x0038CB66 File Offset: 0x0038AD66
		public static parentChildren CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new parentChildren(new Hole(g.Symbol.parentChildren, holeId));
		}

		// Token: 0x060107EF RID: 67567 RVA: 0x0038CB7E File Offset: 0x0038AD7E
		public Children Cast_Children()
		{
			return Children.CreateUnsafe(this.Node);
		}

		// Token: 0x060107F0 RID: 67568 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Children(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x060107F1 RID: 67569 RVA: 0x0038CB8B File Offset: 0x0038AD8B
		public bool Is_Children(GrammarBuilders g, out Children value)
		{
			value = Children.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x060107F2 RID: 67570 RVA: 0x0038CB9F File Offset: 0x0038AD9F
		public Children? As_Children(GrammarBuilders g)
		{
			return new Children?(Children.CreateUnsafe(this.Node));
		}

		// Token: 0x060107F3 RID: 67571 RVA: 0x0038CBB1 File Offset: 0x0038ADB1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060107F4 RID: 67572 RVA: 0x0038CBC4 File Offset: 0x0038ADC4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060107F5 RID: 67573 RVA: 0x0038CBEE File Offset: 0x0038ADEE
		public bool Equals(parentChildren other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062C3 RID: 25283
		private ProgramNode _node;
	}
}
