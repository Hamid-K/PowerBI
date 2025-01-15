using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001362 RID: 4962
	public struct delimiterList : IProgramNodeBuilder, IEquatable<delimiterList>
	{
		// Token: 0x17001A6B RID: 6763
		// (get) Token: 0x0600995D RID: 39261 RVA: 0x00208722 File Offset: 0x00206922
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600995E RID: 39262 RVA: 0x0020872A File Offset: 0x0020692A
		private delimiterList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600995F RID: 39263 RVA: 0x00208733 File Offset: 0x00206933
		public static delimiterList CreateUnsafe(ProgramNode node)
		{
			return new delimiterList(node);
		}

		// Token: 0x06009960 RID: 39264 RVA: 0x0020873C File Offset: 0x0020693C
		public static delimiterList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.delimiterList)
			{
				return null;
			}
			return new delimiterList?(delimiterList.CreateUnsafe(node));
		}

		// Token: 0x06009961 RID: 39265 RVA: 0x00208776 File Offset: 0x00206976
		public static delimiterList CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new delimiterList(new Hole(g.Symbol.delimiterList, holeId));
		}

		// Token: 0x06009962 RID: 39266 RVA: 0x0020878E File Offset: 0x0020698E
		public bool Is_DelimitersList(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DelimitersList;
		}

		// Token: 0x06009963 RID: 39267 RVA: 0x002087A8 File Offset: 0x002069A8
		public bool Is_DelimitersList(GrammarBuilders g, out DelimitersList value)
		{
			if (this.Node.GrammarRule == g.Rule.DelimitersList)
			{
				value = DelimitersList.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DelimitersList);
			return false;
		}

		// Token: 0x06009964 RID: 39268 RVA: 0x002087E0 File Offset: 0x002069E0
		public DelimitersList? As_DelimitersList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DelimitersList)
			{
				return null;
			}
			return new DelimitersList?(DelimitersList.CreateUnsafe(this.Node));
		}

		// Token: 0x06009965 RID: 39269 RVA: 0x00208820 File Offset: 0x00206A20
		public DelimitersList Cast_DelimitersList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DelimitersList)
			{
				return DelimitersList.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DelimitersList is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06009966 RID: 39270 RVA: 0x00208875 File Offset: 0x00206A75
		public bool Is_EmptyDelimitersList(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.EmptyDelimitersList;
		}

		// Token: 0x06009967 RID: 39271 RVA: 0x0020888F File Offset: 0x00206A8F
		public bool Is_EmptyDelimitersList(GrammarBuilders g, out EmptyDelimitersList value)
		{
			if (this.Node.GrammarRule == g.Rule.EmptyDelimitersList)
			{
				value = EmptyDelimitersList.CreateUnsafe(this.Node);
				return true;
			}
			value = default(EmptyDelimitersList);
			return false;
		}

		// Token: 0x06009968 RID: 39272 RVA: 0x002088C4 File Offset: 0x00206AC4
		public EmptyDelimitersList? As_EmptyDelimitersList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.EmptyDelimitersList)
			{
				return null;
			}
			return new EmptyDelimitersList?(EmptyDelimitersList.CreateUnsafe(this.Node));
		}

		// Token: 0x06009969 RID: 39273 RVA: 0x00208904 File Offset: 0x00206B04
		public EmptyDelimitersList Cast_EmptyDelimitersList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.EmptyDelimitersList)
			{
				return EmptyDelimitersList.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_EmptyDelimitersList is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600996A RID: 39274 RVA: 0x0020895C File Offset: 0x00206B5C
		public T Switch<T>(GrammarBuilders g, Func<DelimitersList, T> func0, Func<EmptyDelimitersList, T> func1)
		{
			DelimitersList delimitersList;
			if (this.Is_DelimitersList(g, out delimitersList))
			{
				return func0(delimitersList);
			}
			EmptyDelimitersList emptyDelimitersList;
			if (this.Is_EmptyDelimitersList(g, out emptyDelimitersList))
			{
				return func1(emptyDelimitersList);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol delimiterList");
		}

		// Token: 0x0600996B RID: 39275 RVA: 0x002089B4 File Offset: 0x00206BB4
		public void Switch(GrammarBuilders g, Action<DelimitersList> func0, Action<EmptyDelimitersList> func1)
		{
			DelimitersList delimitersList;
			if (this.Is_DelimitersList(g, out delimitersList))
			{
				func0(delimitersList);
				return;
			}
			EmptyDelimitersList emptyDelimitersList;
			if (this.Is_EmptyDelimitersList(g, out emptyDelimitersList))
			{
				func1(emptyDelimitersList);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol delimiterList");
		}

		// Token: 0x0600996C RID: 39276 RVA: 0x00208A0B File Offset: 0x00206C0B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600996D RID: 39277 RVA: 0x00208A20 File Offset: 0x00206C20
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600996E RID: 39278 RVA: 0x00208A4A File Offset: 0x00206C4A
		public bool Equals(delimiterList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DD9 RID: 15833
		private ProgramNode _node;
	}
}
