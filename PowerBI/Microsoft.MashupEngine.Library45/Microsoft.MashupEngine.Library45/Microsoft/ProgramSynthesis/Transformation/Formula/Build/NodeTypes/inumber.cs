using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015B4 RID: 5556
	public struct inumber : IProgramNodeBuilder, IEquatable<inumber>
	{
		// Token: 0x17001FDA RID: 8154
		// (get) Token: 0x0600B78A RID: 46986 RVA: 0x0027CFDA File Offset: 0x0027B1DA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B78B RID: 46987 RVA: 0x0027CFE2 File Offset: 0x0027B1E2
		private inumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B78C RID: 46988 RVA: 0x0027CFEB File Offset: 0x0027B1EB
		public static inumber CreateUnsafe(ProgramNode node)
		{
			return new inumber(node);
		}

		// Token: 0x0600B78D RID: 46989 RVA: 0x0027CFF4 File Offset: 0x0027B1F4
		public static inumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.inumber)
			{
				return null;
			}
			return new inumber?(inumber.CreateUnsafe(node));
		}

		// Token: 0x0600B78E RID: 46990 RVA: 0x0027D02E File Offset: 0x0027B22E
		public static inumber CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new inumber(new Hole(g.Symbol.inumber, holeId));
		}

		// Token: 0x0600B78F RID: 46991 RVA: 0x0027D046 File Offset: 0x0027B246
		public bool Is_inumber_fromNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.inumber_fromNumber;
		}

		// Token: 0x0600B790 RID: 46992 RVA: 0x0027D060 File Offset: 0x0027B260
		public bool Is_inumber_fromNumber(GrammarBuilders g, out inumber_fromNumber value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.inumber_fromNumber)
			{
				value = inumber_fromNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(inumber_fromNumber);
			return false;
		}

		// Token: 0x0600B791 RID: 46993 RVA: 0x0027D098 File Offset: 0x0027B298
		public inumber_fromNumber? As_inumber_fromNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.inumber_fromNumber)
			{
				return null;
			}
			return new inumber_fromNumber?(inumber_fromNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B792 RID: 46994 RVA: 0x0027D0D8 File Offset: 0x0027B2D8
		public inumber_fromNumber Cast_inumber_fromNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.inumber_fromNumber)
			{
				return inumber_fromNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_inumber_fromNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B793 RID: 46995 RVA: 0x0027D12D File Offset: 0x0027B32D
		public bool Is_ParseNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ParseNumber;
		}

		// Token: 0x0600B794 RID: 46996 RVA: 0x0027D147 File Offset: 0x0027B347
		public bool Is_ParseNumber(GrammarBuilders g, out ParseNumber value)
		{
			if (this.Node.GrammarRule == g.Rule.ParseNumber)
			{
				value = ParseNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ParseNumber);
			return false;
		}

		// Token: 0x0600B795 RID: 46997 RVA: 0x0027D17C File Offset: 0x0027B37C
		public ParseNumber? As_ParseNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ParseNumber)
			{
				return null;
			}
			return new ParseNumber?(ParseNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B796 RID: 46998 RVA: 0x0027D1BC File Offset: 0x0027B3BC
		public ParseNumber Cast_ParseNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ParseNumber)
			{
				return ParseNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ParseNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B797 RID: 46999 RVA: 0x0027D214 File Offset: 0x0027B414
		public T Switch<T>(GrammarBuilders g, Func<inumber_fromNumber, T> func0, Func<ParseNumber, T> func1)
		{
			inumber_fromNumber inumber_fromNumber;
			if (this.Is_inumber_fromNumber(g, out inumber_fromNumber))
			{
				return func0(inumber_fromNumber);
			}
			ParseNumber parseNumber;
			if (this.Is_ParseNumber(g, out parseNumber))
			{
				return func1(parseNumber);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol inumber");
		}

		// Token: 0x0600B798 RID: 47000 RVA: 0x0027D26C File Offset: 0x0027B46C
		public void Switch(GrammarBuilders g, Action<inumber_fromNumber> func0, Action<ParseNumber> func1)
		{
			inumber_fromNumber inumber_fromNumber;
			if (this.Is_inumber_fromNumber(g, out inumber_fromNumber))
			{
				func0(inumber_fromNumber);
				return;
			}
			ParseNumber parseNumber;
			if (this.Is_ParseNumber(g, out parseNumber))
			{
				func1(parseNumber);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol inumber");
		}

		// Token: 0x0600B799 RID: 47001 RVA: 0x0027D2C3 File Offset: 0x0027B4C3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B79A RID: 47002 RVA: 0x0027D2D8 File Offset: 0x0027B4D8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B79B RID: 47003 RVA: 0x0027D302 File Offset: 0x0027B502
		public bool Equals(inumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004662 RID: 18018
		private ProgramNode _node;
	}
}
