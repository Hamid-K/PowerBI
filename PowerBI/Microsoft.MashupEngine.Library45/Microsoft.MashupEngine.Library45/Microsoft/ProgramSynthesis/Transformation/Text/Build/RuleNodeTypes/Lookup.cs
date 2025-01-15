using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C05 RID: 7173
	public struct Lookup : IProgramNodeBuilder, IEquatable<Lookup>
	{
		// Token: 0x17002834 RID: 10292
		// (get) Token: 0x0600F13B RID: 61755 RVA: 0x0033F456 File Offset: 0x0033D656
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F13C RID: 61756 RVA: 0x0033F45E File Offset: 0x0033D65E
		private Lookup(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F13D RID: 61757 RVA: 0x0033F467 File Offset: 0x0033D667
		public static Lookup CreateUnsafe(ProgramNode node)
		{
			return new Lookup(node);
		}

		// Token: 0x0600F13E RID: 61758 RVA: 0x0033F470 File Offset: 0x0033D670
		public static Lookup? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Lookup)
			{
				return null;
			}
			return new Lookup?(Lookup.CreateUnsafe(node));
		}

		// Token: 0x0600F13F RID: 61759 RVA: 0x0033F4A5 File Offset: 0x0033D6A5
		public Lookup(GrammarBuilders g, x value0, lookupDictionary value1)
		{
			this._node = g.Rule.Lookup.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F140 RID: 61760 RVA: 0x0033F4CB File Offset: 0x0033D6CB
		public static implicit operator conv(Lookup arg)
		{
			return conv.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002835 RID: 10293
		// (get) Token: 0x0600F141 RID: 61761 RVA: 0x0033F4D9 File Offset: 0x0033D6D9
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002836 RID: 10294
		// (get) Token: 0x0600F142 RID: 61762 RVA: 0x0033F4ED File Offset: 0x0033D6ED
		public lookupDictionary lookupDictionary
		{
			get
			{
				return lookupDictionary.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F143 RID: 61763 RVA: 0x0033F501 File Offset: 0x0033D701
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F144 RID: 61764 RVA: 0x0033F514 File Offset: 0x0033D714
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F145 RID: 61765 RVA: 0x0033F53E File Offset: 0x0033D73E
		public bool Equals(Lookup other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AF4 RID: 23284
		private ProgramNode _node;
	}
}
