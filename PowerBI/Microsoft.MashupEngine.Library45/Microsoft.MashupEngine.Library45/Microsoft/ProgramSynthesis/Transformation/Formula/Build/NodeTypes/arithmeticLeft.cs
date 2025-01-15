using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015AF RID: 5551
	public struct arithmeticLeft : IProgramNodeBuilder, IEquatable<arithmeticLeft>
	{
		// Token: 0x17001FD5 RID: 8149
		// (get) Token: 0x0600B730 RID: 46896 RVA: 0x0027BFAE File Offset: 0x0027A1AE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B731 RID: 46897 RVA: 0x0027BFB6 File Offset: 0x0027A1B6
		private arithmeticLeft(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B732 RID: 46898 RVA: 0x0027BFBF File Offset: 0x0027A1BF
		public static arithmeticLeft CreateUnsafe(ProgramNode node)
		{
			return new arithmeticLeft(node);
		}

		// Token: 0x0600B733 RID: 46899 RVA: 0x0027BFC8 File Offset: 0x0027A1C8
		public static arithmeticLeft? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.arithmeticLeft)
			{
				return null;
			}
			return new arithmeticLeft?(arithmeticLeft.CreateUnsafe(node));
		}

		// Token: 0x0600B734 RID: 46900 RVA: 0x0027C002 File Offset: 0x0027A202
		public static arithmeticLeft CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new arithmeticLeft(new Hole(g.Symbol.arithmeticLeft, holeId));
		}

		// Token: 0x0600B735 RID: 46901 RVA: 0x0027C01A File Offset: 0x0027A21A
		public bool Is_arithmeticLeft_fromNumberCoalesced(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.arithmeticLeft_fromNumberCoalesced;
		}

		// Token: 0x0600B736 RID: 46902 RVA: 0x0027C034 File Offset: 0x0027A234
		public bool Is_arithmeticLeft_fromNumberCoalesced(GrammarBuilders g, out arithmeticLeft_fromNumberCoalesced value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.arithmeticLeft_fromNumberCoalesced)
			{
				value = arithmeticLeft_fromNumberCoalesced.CreateUnsafe(this.Node);
				return true;
			}
			value = default(arithmeticLeft_fromNumberCoalesced);
			return false;
		}

		// Token: 0x0600B737 RID: 46903 RVA: 0x0027C06C File Offset: 0x0027A26C
		public arithmeticLeft_fromNumberCoalesced? As_arithmeticLeft_fromNumberCoalesced(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.arithmeticLeft_fromNumberCoalesced)
			{
				return null;
			}
			return new arithmeticLeft_fromNumberCoalesced?(arithmeticLeft_fromNumberCoalesced.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B738 RID: 46904 RVA: 0x0027C0AC File Offset: 0x0027A2AC
		public arithmeticLeft_fromNumberCoalesced Cast_arithmeticLeft_fromNumberCoalesced(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.arithmeticLeft_fromNumberCoalesced)
			{
				return arithmeticLeft_fromNumberCoalesced.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_arithmeticLeft_fromNumberCoalesced is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B739 RID: 46905 RVA: 0x0027C101 File Offset: 0x0027A301
		public bool Is_arithmeticLeft_inumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.arithmeticLeft_inumber;
		}

		// Token: 0x0600B73A RID: 46906 RVA: 0x0027C11B File Offset: 0x0027A31B
		public bool Is_arithmeticLeft_inumber(GrammarBuilders g, out arithmeticLeft_inumber value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.arithmeticLeft_inumber)
			{
				value = arithmeticLeft_inumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(arithmeticLeft_inumber);
			return false;
		}

		// Token: 0x0600B73B RID: 46907 RVA: 0x0027C150 File Offset: 0x0027A350
		public arithmeticLeft_inumber? As_arithmeticLeft_inumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.arithmeticLeft_inumber)
			{
				return null;
			}
			return new arithmeticLeft_inumber?(arithmeticLeft_inumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B73C RID: 46908 RVA: 0x0027C190 File Offset: 0x0027A390
		public arithmeticLeft_inumber Cast_arithmeticLeft_inumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.arithmeticLeft_inumber)
			{
				return arithmeticLeft_inumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_arithmeticLeft_inumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B73D RID: 46909 RVA: 0x0027C1E8 File Offset: 0x0027A3E8
		public T Switch<T>(GrammarBuilders g, Func<arithmeticLeft_fromNumberCoalesced, T> func0, Func<arithmeticLeft_inumber, T> func1)
		{
			arithmeticLeft_fromNumberCoalesced arithmeticLeft_fromNumberCoalesced;
			if (this.Is_arithmeticLeft_fromNumberCoalesced(g, out arithmeticLeft_fromNumberCoalesced))
			{
				return func0(arithmeticLeft_fromNumberCoalesced);
			}
			arithmeticLeft_inumber arithmeticLeft_inumber;
			if (this.Is_arithmeticLeft_inumber(g, out arithmeticLeft_inumber))
			{
				return func1(arithmeticLeft_inumber);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol arithmeticLeft");
		}

		// Token: 0x0600B73E RID: 46910 RVA: 0x0027C240 File Offset: 0x0027A440
		public void Switch(GrammarBuilders g, Action<arithmeticLeft_fromNumberCoalesced> func0, Action<arithmeticLeft_inumber> func1)
		{
			arithmeticLeft_fromNumberCoalesced arithmeticLeft_fromNumberCoalesced;
			if (this.Is_arithmeticLeft_fromNumberCoalesced(g, out arithmeticLeft_fromNumberCoalesced))
			{
				func0(arithmeticLeft_fromNumberCoalesced);
				return;
			}
			arithmeticLeft_inumber arithmeticLeft_inumber;
			if (this.Is_arithmeticLeft_inumber(g, out arithmeticLeft_inumber))
			{
				func1(arithmeticLeft_inumber);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol arithmeticLeft");
		}

		// Token: 0x0600B73F RID: 46911 RVA: 0x0027C297 File Offset: 0x0027A497
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B740 RID: 46912 RVA: 0x0027C2AC File Offset: 0x0027A4AC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B741 RID: 46913 RVA: 0x0027C2D6 File Offset: 0x0027A4D6
		public bool Equals(arithmeticLeft other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400465D RID: 18013
		private ProgramNode _node;
	}
}
