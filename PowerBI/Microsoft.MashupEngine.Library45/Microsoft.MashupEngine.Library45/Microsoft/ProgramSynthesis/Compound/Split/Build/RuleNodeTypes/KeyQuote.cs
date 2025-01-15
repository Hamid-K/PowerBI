using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200094A RID: 2378
	public struct KeyQuote : IProgramNodeBuilder, IEquatable<KeyQuote>
	{
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06003772 RID: 14194 RVA: 0x000ADE2A File Offset: 0x000AC02A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x000ADE32 File Offset: 0x000AC032
		private KeyQuote(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x000ADE3B File Offset: 0x000AC03B
		public static KeyQuote CreateUnsafe(ProgramNode node)
		{
			return new KeyQuote(node);
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000ADE44 File Offset: 0x000AC044
		public static KeyQuote? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KeyQuote)
			{
				return null;
			}
			return new KeyQuote?(KeyQuote.CreateUnsafe(node));
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000ADE79 File Offset: 0x000AC079
		public KeyQuote(GrammarBuilders g, key value0, records value1)
		{
			this._node = g.Rule.KeyQuote.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000ADE9F File Offset: 0x000AC09F
		public static implicit operator primarySelector(KeyQuote arg)
		{
			return primarySelector.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06003778 RID: 14200 RVA: 0x000ADEAD File Offset: 0x000AC0AD
		public key key
		{
			get
			{
				return key.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06003779 RID: 14201 RVA: 0x000ADEC1 File Offset: 0x000AC0C1
		public records records
		{
			get
			{
				return records.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000ADED5 File Offset: 0x000AC0D5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000ADEE8 File Offset: 0x000AC0E8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000ADF12 File Offset: 0x000AC112
		public bool Equals(KeyQuote other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A6A RID: 6762
		private ProgramNode _node;
	}
}
