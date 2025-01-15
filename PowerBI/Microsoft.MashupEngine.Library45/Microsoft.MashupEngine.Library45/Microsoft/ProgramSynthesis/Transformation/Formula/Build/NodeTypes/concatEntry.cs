using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015A2 RID: 5538
	public struct concatEntry : IProgramNodeBuilder, IEquatable<concatEntry>
	{
		// Token: 0x17001FC8 RID: 8136
		// (get) Token: 0x0600B5FA RID: 46586 RVA: 0x00278486 File Offset: 0x00276686
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B5FB RID: 46587 RVA: 0x0027848E File Offset: 0x0027668E
		private concatEntry(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B5FC RID: 46588 RVA: 0x00278497 File Offset: 0x00276697
		public static concatEntry CreateUnsafe(ProgramNode node)
		{
			return new concatEntry(node);
		}

		// Token: 0x0600B5FD RID: 46589 RVA: 0x002784A0 File Offset: 0x002766A0
		public static concatEntry? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.concatEntry)
			{
				return null;
			}
			return new concatEntry?(concatEntry.CreateUnsafe(node));
		}

		// Token: 0x0600B5FE RID: 46590 RVA: 0x002784DA File Offset: 0x002766DA
		public static concatEntry CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new concatEntry(new Hole(g.Symbol.concatEntry, holeId));
		}

		// Token: 0x0600B5FF RID: 46591 RVA: 0x002784F2 File Offset: 0x002766F2
		public bool Is_concatEntry_concatCase(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.concatEntry_concatCase;
		}

		// Token: 0x0600B600 RID: 46592 RVA: 0x0027850C File Offset: 0x0027670C
		public bool Is_concatEntry_concatCase(GrammarBuilders g, out concatEntry_concatCase value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatEntry_concatCase)
			{
				value = concatEntry_concatCase.CreateUnsafe(this.Node);
				return true;
			}
			value = default(concatEntry_concatCase);
			return false;
		}

		// Token: 0x0600B601 RID: 46593 RVA: 0x00278544 File Offset: 0x00276744
		public concatEntry_concatCase? As_concatEntry_concatCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.concatEntry_concatCase)
			{
				return null;
			}
			return new concatEntry_concatCase?(concatEntry_concatCase.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B602 RID: 46594 RVA: 0x00278584 File Offset: 0x00276784
		public concatEntry_concatCase Cast_concatEntry_concatCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatEntry_concatCase)
			{
				return concatEntry_concatCase.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_concatEntry_concatCase is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B603 RID: 46595 RVA: 0x002785D9 File Offset: 0x002767D9
		public bool Is_concatEntry_constString(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.concatEntry_constString;
		}

		// Token: 0x0600B604 RID: 46596 RVA: 0x002785F3 File Offset: 0x002767F3
		public bool Is_concatEntry_constString(GrammarBuilders g, out concatEntry_constString value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatEntry_constString)
			{
				value = concatEntry_constString.CreateUnsafe(this.Node);
				return true;
			}
			value = default(concatEntry_constString);
			return false;
		}

		// Token: 0x0600B605 RID: 46597 RVA: 0x00278628 File Offset: 0x00276828
		public concatEntry_constString? As_concatEntry_constString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.concatEntry_constString)
			{
				return null;
			}
			return new concatEntry_constString?(concatEntry_constString.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B606 RID: 46598 RVA: 0x00278668 File Offset: 0x00276868
		public concatEntry_constString Cast_concatEntry_constString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatEntry_constString)
			{
				return concatEntry_constString.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_concatEntry_constString is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B607 RID: 46599 RVA: 0x002786C0 File Offset: 0x002768C0
		public T Switch<T>(GrammarBuilders g, Func<concatEntry_concatCase, T> func0, Func<concatEntry_constString, T> func1)
		{
			concatEntry_concatCase concatEntry_concatCase;
			if (this.Is_concatEntry_concatCase(g, out concatEntry_concatCase))
			{
				return func0(concatEntry_concatCase);
			}
			concatEntry_constString concatEntry_constString;
			if (this.Is_concatEntry_constString(g, out concatEntry_constString))
			{
				return func1(concatEntry_constString);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol concatEntry");
		}

		// Token: 0x0600B608 RID: 46600 RVA: 0x00278718 File Offset: 0x00276918
		public void Switch(GrammarBuilders g, Action<concatEntry_concatCase> func0, Action<concatEntry_constString> func1)
		{
			concatEntry_concatCase concatEntry_concatCase;
			if (this.Is_concatEntry_concatCase(g, out concatEntry_concatCase))
			{
				func0(concatEntry_concatCase);
				return;
			}
			concatEntry_constString concatEntry_constString;
			if (this.Is_concatEntry_constString(g, out concatEntry_constString))
			{
				func1(concatEntry_constString);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol concatEntry");
		}

		// Token: 0x0600B609 RID: 46601 RVA: 0x0027876F File Offset: 0x0027696F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B60A RID: 46602 RVA: 0x00278784 File Offset: 0x00276984
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B60B RID: 46603 RVA: 0x002787AE File Offset: 0x002769AE
		public bool Equals(concatEntry other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004650 RID: 18000
		private ProgramNode _node;
	}
}
