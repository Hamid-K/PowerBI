using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000962 RID: 2402
	public struct columnSelectorList : IProgramNodeBuilder, IEquatable<columnSelectorList>
	{
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x000AFBCE File Offset: 0x000ADDCE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000AFBD6 File Offset: 0x000ADDD6
		private columnSelectorList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000AFBDF File Offset: 0x000ADDDF
		public static columnSelectorList CreateUnsafe(ProgramNode node)
		{
			return new columnSelectorList(node);
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000AFBE8 File Offset: 0x000ADDE8
		public static columnSelectorList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.columnSelectorList)
			{
				return null;
			}
			return new columnSelectorList?(columnSelectorList.CreateUnsafe(node));
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000AFC22 File Offset: 0x000ADE22
		public static columnSelectorList CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new columnSelectorList(new Hole(g.Symbol.columnSelectorList, holeId));
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000AFC3A File Offset: 0x000ADE3A
		public bool Is_Empty(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Empty;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000AFC54 File Offset: 0x000ADE54
		public bool Is_Empty(GrammarBuilders g, out Empty value)
		{
			if (this.Node.GrammarRule == g.Rule.Empty)
			{
				value = Empty.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Empty);
			return false;
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000AFC8C File Offset: 0x000ADE8C
		public Empty? As_Empty(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Empty)
			{
				return null;
			}
			return new Empty?(Empty.CreateUnsafe(this.Node));
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000AFCCC File Offset: 0x000ADECC
		public Empty Cast_Empty(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Empty)
			{
				return Empty.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Empty is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000AFD21 File Offset: 0x000ADF21
		public bool Is_SelectorList(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SelectorList;
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000AFD3B File Offset: 0x000ADF3B
		public bool Is_SelectorList(GrammarBuilders g, out SelectorList value)
		{
			if (this.Node.GrammarRule == g.Rule.SelectorList)
			{
				value = SelectorList.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SelectorList);
			return false;
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000AFD70 File Offset: 0x000ADF70
		public SelectorList? As_SelectorList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SelectorList)
			{
				return null;
			}
			return new SelectorList?(SelectorList.CreateUnsafe(this.Node));
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000AFDB0 File Offset: 0x000ADFB0
		public SelectorList Cast_SelectorList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SelectorList)
			{
				return SelectorList.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SelectorList is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000AFE08 File Offset: 0x000AE008
		public T Switch<T>(GrammarBuilders g, Func<Empty, T> func0, Func<SelectorList, T> func1)
		{
			Empty empty;
			if (this.Is_Empty(g, out empty))
			{
				return func0(empty);
			}
			SelectorList selectorList;
			if (this.Is_SelectorList(g, out selectorList))
			{
				return func1(selectorList);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol columnSelectorList");
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000AFE60 File Offset: 0x000AE060
		public void Switch(GrammarBuilders g, Action<Empty> func0, Action<SelectorList> func1)
		{
			Empty empty;
			if (this.Is_Empty(g, out empty))
			{
				func0(empty);
				return;
			}
			SelectorList selectorList;
			if (this.Is_SelectorList(g, out selectorList))
			{
				func1(selectorList);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol columnSelectorList");
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000AFEB7 File Offset: 0x000AE0B7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000AFECC File Offset: 0x000AE0CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000AFEF6 File Offset: 0x000AE0F6
		public bool Equals(columnSelectorList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A82 RID: 6786
		private ProgramNode _node;
	}
}
