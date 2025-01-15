using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200107A RID: 4218
	public struct fexpr : IProgramNodeBuilder, IEquatable<fexpr>
	{
		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06007E61 RID: 32353 RVA: 0x001A98CA File Offset: 0x001A7ACA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007E62 RID: 32354 RVA: 0x001A98D2 File Offset: 0x001A7AD2
		private fexpr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007E63 RID: 32355 RVA: 0x001A98DB File Offset: 0x001A7ADB
		public static fexpr CreateUnsafe(ProgramNode node)
		{
			return new fexpr(node);
		}

		// Token: 0x06007E64 RID: 32356 RVA: 0x001A98E4 File Offset: 0x001A7AE4
		public static fexpr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fexpr)
			{
				return null;
			}
			return new fexpr?(fexpr.CreateUnsafe(node));
		}

		// Token: 0x06007E65 RID: 32357 RVA: 0x001A991E File Offset: 0x001A7B1E
		public static fexpr CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fexpr(new Hole(g.Symbol.fexpr, holeId));
		}

		// Token: 0x06007E66 RID: 32358 RVA: 0x001A9936 File Offset: 0x001A7B36
		public bool Is_fexpr_literalExpr(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.fexpr_literalExpr;
		}

		// Token: 0x06007E67 RID: 32359 RVA: 0x001A9950 File Offset: 0x001A7B50
		public bool Is_fexpr_literalExpr(GrammarBuilders g, out fexpr_literalExpr value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.fexpr_literalExpr)
			{
				value = fexpr_literalExpr.CreateUnsafe(this.Node);
				return true;
			}
			value = default(fexpr_literalExpr);
			return false;
		}

		// Token: 0x06007E68 RID: 32360 RVA: 0x001A9988 File Offset: 0x001A7B88
		public fexpr_literalExpr? As_fexpr_literalExpr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.fexpr_literalExpr)
			{
				return null;
			}
			return new fexpr_literalExpr?(fexpr_literalExpr.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E69 RID: 32361 RVA: 0x001A99C8 File Offset: 0x001A7BC8
		public fexpr_literalExpr Cast_fexpr_literalExpr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.fexpr_literalExpr)
			{
				return fexpr_literalExpr.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_fexpr_literalExpr is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E6A RID: 32362 RVA: 0x001A9A1D File Offset: 0x001A7C1D
		public bool Is_And(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.And;
		}

		// Token: 0x06007E6B RID: 32363 RVA: 0x001A9A37 File Offset: 0x001A7C37
		public bool Is_And(GrammarBuilders g, out And value)
		{
			if (this.Node.GrammarRule == g.Rule.And)
			{
				value = And.CreateUnsafe(this.Node);
				return true;
			}
			value = default(And);
			return false;
		}

		// Token: 0x06007E6C RID: 32364 RVA: 0x001A9A6C File Offset: 0x001A7C6C
		public And? As_And(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.And)
			{
				return null;
			}
			return new And?(And.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E6D RID: 32365 RVA: 0x001A9AAC File Offset: 0x001A7CAC
		public And Cast_And(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.And)
			{
				return And.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_And is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E6E RID: 32366 RVA: 0x001A9B04 File Offset: 0x001A7D04
		public T Switch<T>(GrammarBuilders g, Func<fexpr_literalExpr, T> func0, Func<And, T> func1)
		{
			fexpr_literalExpr fexpr_literalExpr;
			if (this.Is_fexpr_literalExpr(g, out fexpr_literalExpr))
			{
				return func0(fexpr_literalExpr);
			}
			And and;
			if (this.Is_And(g, out and))
			{
				return func1(and);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol fexpr");
		}

		// Token: 0x06007E6F RID: 32367 RVA: 0x001A9B5C File Offset: 0x001A7D5C
		public void Switch(GrammarBuilders g, Action<fexpr_literalExpr> func0, Action<And> func1)
		{
			fexpr_literalExpr fexpr_literalExpr;
			if (this.Is_fexpr_literalExpr(g, out fexpr_literalExpr))
			{
				func0(fexpr_literalExpr);
				return;
			}
			And and;
			if (this.Is_And(g, out and))
			{
				func1(and);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol fexpr");
		}

		// Token: 0x06007E70 RID: 32368 RVA: 0x001A9BB3 File Offset: 0x001A7DB3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007E71 RID: 32369 RVA: 0x001A9BC8 File Offset: 0x001A7DC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007E72 RID: 32370 RVA: 0x001A9BF2 File Offset: 0x001A7DF2
		public bool Equals(fexpr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003393 RID: 13203
		private ProgramNode _node;
	}
}
