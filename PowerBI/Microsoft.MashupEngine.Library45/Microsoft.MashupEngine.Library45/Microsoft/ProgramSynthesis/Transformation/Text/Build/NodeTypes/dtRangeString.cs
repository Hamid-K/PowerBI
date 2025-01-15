using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C45 RID: 7237
	public struct dtRangeString : IProgramNodeBuilder, IEquatable<dtRangeString>
	{
		// Token: 0x170028DB RID: 10459
		// (get) Token: 0x0600F466 RID: 62566 RVA: 0x003451DE File Offset: 0x003433DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F467 RID: 62567 RVA: 0x003451E6 File Offset: 0x003433E6
		private dtRangeString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F468 RID: 62568 RVA: 0x003451EF File Offset: 0x003433EF
		public static dtRangeString CreateUnsafe(ProgramNode node)
		{
			return new dtRangeString(node);
		}

		// Token: 0x0600F469 RID: 62569 RVA: 0x003451F8 File Offset: 0x003433F8
		public static dtRangeString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.dtRangeString)
			{
				return null;
			}
			return new dtRangeString?(dtRangeString.CreateUnsafe(node));
		}

		// Token: 0x0600F46A RID: 62570 RVA: 0x00345232 File Offset: 0x00343432
		public static dtRangeString CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new dtRangeString(new Hole(g.Symbol.dtRangeString, holeId));
		}

		// Token: 0x0600F46B RID: 62571 RVA: 0x0034524A File Offset: 0x0034344A
		public bool Is_dtRangeString_dtRangeSubstring(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.dtRangeString_dtRangeSubstring;
		}

		// Token: 0x0600F46C RID: 62572 RVA: 0x00345264 File Offset: 0x00343464
		public bool Is_dtRangeString_dtRangeSubstring(GrammarBuilders g, out dtRangeString_dtRangeSubstring value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.dtRangeString_dtRangeSubstring)
			{
				value = dtRangeString_dtRangeSubstring.CreateUnsafe(this.Node);
				return true;
			}
			value = default(dtRangeString_dtRangeSubstring);
			return false;
		}

		// Token: 0x0600F46D RID: 62573 RVA: 0x0034529C File Offset: 0x0034349C
		public dtRangeString_dtRangeSubstring? As_dtRangeString_dtRangeSubstring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.dtRangeString_dtRangeSubstring)
			{
				return null;
			}
			return new dtRangeString_dtRangeSubstring?(dtRangeString_dtRangeSubstring.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F46E RID: 62574 RVA: 0x003452DC File Offset: 0x003434DC
		public dtRangeString_dtRangeSubstring Cast_dtRangeString_dtRangeSubstring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.dtRangeString_dtRangeSubstring)
			{
				return dtRangeString_dtRangeSubstring.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_dtRangeString_dtRangeSubstring is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F46F RID: 62575 RVA: 0x00345331 File Offset: 0x00343531
		public bool Is_DtRangeConcat(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DtRangeConcat;
		}

		// Token: 0x0600F470 RID: 62576 RVA: 0x0034534B File Offset: 0x0034354B
		public bool Is_DtRangeConcat(GrammarBuilders g, out DtRangeConcat value)
		{
			if (this.Node.GrammarRule == g.Rule.DtRangeConcat)
			{
				value = DtRangeConcat.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DtRangeConcat);
			return false;
		}

		// Token: 0x0600F471 RID: 62577 RVA: 0x00345380 File Offset: 0x00343580
		public DtRangeConcat? As_DtRangeConcat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DtRangeConcat)
			{
				return null;
			}
			return new DtRangeConcat?(DtRangeConcat.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F472 RID: 62578 RVA: 0x003453C0 File Offset: 0x003435C0
		public DtRangeConcat Cast_DtRangeConcat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DtRangeConcat)
			{
				return DtRangeConcat.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DtRangeConcat is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F473 RID: 62579 RVA: 0x00345418 File Offset: 0x00343618
		public T Switch<T>(GrammarBuilders g, Func<dtRangeString_dtRangeSubstring, T> func0, Func<DtRangeConcat, T> func1)
		{
			dtRangeString_dtRangeSubstring dtRangeString_dtRangeSubstring;
			if (this.Is_dtRangeString_dtRangeSubstring(g, out dtRangeString_dtRangeSubstring))
			{
				return func0(dtRangeString_dtRangeSubstring);
			}
			DtRangeConcat dtRangeConcat;
			if (this.Is_DtRangeConcat(g, out dtRangeConcat))
			{
				return func1(dtRangeConcat);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol dtRangeString");
		}

		// Token: 0x0600F474 RID: 62580 RVA: 0x00345470 File Offset: 0x00343670
		public void Switch(GrammarBuilders g, Action<dtRangeString_dtRangeSubstring> func0, Action<DtRangeConcat> func1)
		{
			dtRangeString_dtRangeSubstring dtRangeString_dtRangeSubstring;
			if (this.Is_dtRangeString_dtRangeSubstring(g, out dtRangeString_dtRangeSubstring))
			{
				func0(dtRangeString_dtRangeSubstring);
				return;
			}
			DtRangeConcat dtRangeConcat;
			if (this.Is_DtRangeConcat(g, out dtRangeConcat))
			{
				func1(dtRangeConcat);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol dtRangeString");
		}

		// Token: 0x0600F475 RID: 62581 RVA: 0x003454C7 File Offset: 0x003436C7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F476 RID: 62582 RVA: 0x003454DC File Offset: 0x003436DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F477 RID: 62583 RVA: 0x00345506 File Offset: 0x00343706
		public bool Equals(dtRangeString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B34 RID: 23348
		private ProgramNode _node;
	}
}
