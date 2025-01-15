using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E6D RID: 3693
	public struct titleOf : IProgramNodeBuilder, IEquatable<titleOf>
	{
		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x0600649F RID: 25759 RVA: 0x00146A6E File Offset: 0x00144C6E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060064A0 RID: 25760 RVA: 0x00146A76 File Offset: 0x00144C76
		private titleOf(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060064A1 RID: 25761 RVA: 0x00146A7F File Offset: 0x00144C7F
		public static titleOf CreateUnsafe(ProgramNode node)
		{
			return new titleOf(node);
		}

		// Token: 0x060064A2 RID: 25762 RVA: 0x00146A88 File Offset: 0x00144C88
		public static titleOf? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.titleOf)
			{
				return null;
			}
			return new titleOf?(titleOf.CreateUnsafe(node));
		}

		// Token: 0x060064A3 RID: 25763 RVA: 0x00146AC2 File Offset: 0x00144CC2
		public static titleOf CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new titleOf(new Hole(g.Symbol.titleOf, holeId));
		}

		// Token: 0x060064A4 RID: 25764 RVA: 0x00146ADA File Offset: 0x00144CDA
		public bool Is_WrapOutputForTitle(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.WrapOutputForTitle;
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x00146AF4 File Offset: 0x00144CF4
		public bool Is_WrapOutputForTitle(GrammarBuilders g, out WrapOutputForTitle value)
		{
			if (this.Node.GrammarRule == g.Rule.WrapOutputForTitle)
			{
				value = WrapOutputForTitle.CreateUnsafe(this.Node);
				return true;
			}
			value = default(WrapOutputForTitle);
			return false;
		}

		// Token: 0x060064A6 RID: 25766 RVA: 0x00146B2C File Offset: 0x00144D2C
		public WrapOutputForTitle? As_WrapOutputForTitle(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.WrapOutputForTitle)
			{
				return null;
			}
			return new WrapOutputForTitle?(WrapOutputForTitle.CreateUnsafe(this.Node));
		}

		// Token: 0x060064A7 RID: 25767 RVA: 0x00146B6C File Offset: 0x00144D6C
		public WrapOutputForTitle Cast_WrapOutputForTitle(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.WrapOutputForTitle)
			{
				return WrapOutputForTitle.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_WrapOutputForTitle is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060064A8 RID: 25768 RVA: 0x00146BC1 File Offset: 0x00144DC1
		public bool Is_IncludeEmptyToLeft(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IncludeEmptyToLeft;
		}

		// Token: 0x060064A9 RID: 25769 RVA: 0x00146BDB File Offset: 0x00144DDB
		public bool Is_IncludeEmptyToLeft(GrammarBuilders g, out IncludeEmptyToLeft value)
		{
			if (this.Node.GrammarRule == g.Rule.IncludeEmptyToLeft)
			{
				value = IncludeEmptyToLeft.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IncludeEmptyToLeft);
			return false;
		}

		// Token: 0x060064AA RID: 25770 RVA: 0x00146C10 File Offset: 0x00144E10
		public IncludeEmptyToLeft? As_IncludeEmptyToLeft(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IncludeEmptyToLeft)
			{
				return null;
			}
			return new IncludeEmptyToLeft?(IncludeEmptyToLeft.CreateUnsafe(this.Node));
		}

		// Token: 0x060064AB RID: 25771 RVA: 0x00146C50 File Offset: 0x00144E50
		public IncludeEmptyToLeft Cast_IncludeEmptyToLeft(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IncludeEmptyToLeft)
			{
				return IncludeEmptyToLeft.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IncludeEmptyToLeft is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060064AC RID: 25772 RVA: 0x00146CA8 File Offset: 0x00144EA8
		public T Switch<T>(GrammarBuilders g, Func<WrapOutputForTitle, T> func0, Func<IncludeEmptyToLeft, T> func1)
		{
			WrapOutputForTitle wrapOutputForTitle;
			if (this.Is_WrapOutputForTitle(g, out wrapOutputForTitle))
			{
				return func0(wrapOutputForTitle);
			}
			IncludeEmptyToLeft includeEmptyToLeft;
			if (this.Is_IncludeEmptyToLeft(g, out includeEmptyToLeft))
			{
				return func1(includeEmptyToLeft);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol titleOf");
		}

		// Token: 0x060064AD RID: 25773 RVA: 0x00146D00 File Offset: 0x00144F00
		public void Switch(GrammarBuilders g, Action<WrapOutputForTitle> func0, Action<IncludeEmptyToLeft> func1)
		{
			WrapOutputForTitle wrapOutputForTitle;
			if (this.Is_WrapOutputForTitle(g, out wrapOutputForTitle))
			{
				func0(wrapOutputForTitle);
				return;
			}
			IncludeEmptyToLeft includeEmptyToLeft;
			if (this.Is_IncludeEmptyToLeft(g, out includeEmptyToLeft))
			{
				func1(includeEmptyToLeft);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol titleOf");
		}

		// Token: 0x060064AE RID: 25774 RVA: 0x00146D57 File Offset: 0x00144F57
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060064AF RID: 25775 RVA: 0x00146D6C File Offset: 0x00144F6C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060064B0 RID: 25776 RVA: 0x00146D96 File Offset: 0x00144F96
		public bool Equals(titleOf other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C17 RID: 11287
		private ProgramNode _node;
	}
}
