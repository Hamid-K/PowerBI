using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015A7 RID: 5543
	public struct concatSuffix : IProgramNodeBuilder, IEquatable<concatSuffix>
	{
		// Token: 0x17001FCD RID: 8141
		// (get) Token: 0x0600B65A RID: 46682 RVA: 0x0027958A File Offset: 0x0027778A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B65B RID: 46683 RVA: 0x00279592 File Offset: 0x00277792
		private concatSuffix(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B65C RID: 46684 RVA: 0x0027959B File Offset: 0x0027779B
		public static concatSuffix CreateUnsafe(ProgramNode node)
		{
			return new concatSuffix(node);
		}

		// Token: 0x0600B65D RID: 46685 RVA: 0x002795A4 File Offset: 0x002777A4
		public static concatSuffix? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.concatSuffix)
			{
				return null;
			}
			return new concatSuffix?(concatSuffix.CreateUnsafe(node));
		}

		// Token: 0x0600B65E RID: 46686 RVA: 0x002795DE File Offset: 0x002777DE
		public static concatSuffix CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new concatSuffix(new Hole(g.Symbol.concatSuffix, holeId));
		}

		// Token: 0x0600B65F RID: 46687 RVA: 0x002795F6 File Offset: 0x002777F6
		public bool Is_concatSuffix_concatPrefix(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.concatSuffix_concatPrefix;
		}

		// Token: 0x0600B660 RID: 46688 RVA: 0x00279610 File Offset: 0x00277810
		public bool Is_concatSuffix_concatPrefix(GrammarBuilders g, out concatSuffix_concatPrefix value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatSuffix_concatPrefix)
			{
				value = concatSuffix_concatPrefix.CreateUnsafe(this.Node);
				return true;
			}
			value = default(concatSuffix_concatPrefix);
			return false;
		}

		// Token: 0x0600B661 RID: 46689 RVA: 0x00279648 File Offset: 0x00277848
		public concatSuffix_concatPrefix? As_concatSuffix_concatPrefix(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.concatSuffix_concatPrefix)
			{
				return null;
			}
			return new concatSuffix_concatPrefix?(concatSuffix_concatPrefix.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B662 RID: 46690 RVA: 0x00279688 File Offset: 0x00277888
		public concatSuffix_concatPrefix Cast_concatSuffix_concatPrefix(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatSuffix_concatPrefix)
			{
				return concatSuffix_concatPrefix.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_concatSuffix_concatPrefix is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B663 RID: 46691 RVA: 0x002796DD File Offset: 0x002778DD
		public bool Is_concatSuffix_concat(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.concatSuffix_concat;
		}

		// Token: 0x0600B664 RID: 46692 RVA: 0x002796F7 File Offset: 0x002778F7
		public bool Is_concatSuffix_concat(GrammarBuilders g, out concatSuffix_concat value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatSuffix_concat)
			{
				value = concatSuffix_concat.CreateUnsafe(this.Node);
				return true;
			}
			value = default(concatSuffix_concat);
			return false;
		}

		// Token: 0x0600B665 RID: 46693 RVA: 0x0027972C File Offset: 0x0027792C
		public concatSuffix_concat? As_concatSuffix_concat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.concatSuffix_concat)
			{
				return null;
			}
			return new concatSuffix_concat?(concatSuffix_concat.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B666 RID: 46694 RVA: 0x0027976C File Offset: 0x0027796C
		public concatSuffix_concat Cast_concatSuffix_concat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatSuffix_concat)
			{
				return concatSuffix_concat.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_concatSuffix_concat is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B667 RID: 46695 RVA: 0x002797C4 File Offset: 0x002779C4
		public T Switch<T>(GrammarBuilders g, Func<concatSuffix_concatPrefix, T> func0, Func<concatSuffix_concat, T> func1)
		{
			concatSuffix_concatPrefix concatSuffix_concatPrefix;
			if (this.Is_concatSuffix_concatPrefix(g, out concatSuffix_concatPrefix))
			{
				return func0(concatSuffix_concatPrefix);
			}
			concatSuffix_concat concatSuffix_concat;
			if (this.Is_concatSuffix_concat(g, out concatSuffix_concat))
			{
				return func1(concatSuffix_concat);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol concatSuffix");
		}

		// Token: 0x0600B668 RID: 46696 RVA: 0x0027981C File Offset: 0x00277A1C
		public void Switch(GrammarBuilders g, Action<concatSuffix_concatPrefix> func0, Action<concatSuffix_concat> func1)
		{
			concatSuffix_concatPrefix concatSuffix_concatPrefix;
			if (this.Is_concatSuffix_concatPrefix(g, out concatSuffix_concatPrefix))
			{
				func0(concatSuffix_concatPrefix);
				return;
			}
			concatSuffix_concat concatSuffix_concat;
			if (this.Is_concatSuffix_concat(g, out concatSuffix_concat))
			{
				func1(concatSuffix_concat);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol concatSuffix");
		}

		// Token: 0x0600B669 RID: 46697 RVA: 0x00279873 File Offset: 0x00277A73
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B66A RID: 46698 RVA: 0x00279888 File Offset: 0x00277A88
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B66B RID: 46699 RVA: 0x002798B2 File Offset: 0x00277AB2
		public bool Equals(concatSuffix other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004655 RID: 18005
		private ProgramNode _node;
	}
}
