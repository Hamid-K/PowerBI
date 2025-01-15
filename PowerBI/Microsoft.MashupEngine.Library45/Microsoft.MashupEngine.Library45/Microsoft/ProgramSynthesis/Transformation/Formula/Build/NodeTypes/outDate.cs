using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x0200159C RID: 5532
	public struct outDate : IProgramNodeBuilder, IEquatable<outDate>
	{
		// Token: 0x17001FC2 RID: 8130
		// (get) Token: 0x0600B57E RID: 46462 RVA: 0x00276CEE File Offset: 0x00274EEE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B57F RID: 46463 RVA: 0x00276CF6 File Offset: 0x00274EF6
		private outDate(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B580 RID: 46464 RVA: 0x00276CFF File Offset: 0x00274EFF
		public static outDate CreateUnsafe(ProgramNode node)
		{
			return new outDate(node);
		}

		// Token: 0x0600B581 RID: 46465 RVA: 0x00276D08 File Offset: 0x00274F08
		public static outDate? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.outDate)
			{
				return null;
			}
			return new outDate?(outDate.CreateUnsafe(node));
		}

		// Token: 0x0600B582 RID: 46466 RVA: 0x00276D42 File Offset: 0x00274F42
		public static outDate CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new outDate(new Hole(g.Symbol.outDate, holeId));
		}

		// Token: 0x0600B583 RID: 46467 RVA: 0x00276D5A File Offset: 0x00274F5A
		public bool Is_outDate_date(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.outDate_date;
		}

		// Token: 0x0600B584 RID: 46468 RVA: 0x00276D74 File Offset: 0x00274F74
		public bool Is_outDate_date(GrammarBuilders g, out outDate_date value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outDate_date)
			{
				value = outDate_date.CreateUnsafe(this.Node);
				return true;
			}
			value = default(outDate_date);
			return false;
		}

		// Token: 0x0600B585 RID: 46469 RVA: 0x00276DAC File Offset: 0x00274FAC
		public outDate_date? As_outDate_date(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.outDate_date)
			{
				return null;
			}
			return new outDate_date?(outDate_date.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B586 RID: 46470 RVA: 0x00276DEC File Offset: 0x00274FEC
		public outDate_date Cast_outDate_date(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outDate_date)
			{
				return outDate_date.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_outDate_date is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B587 RID: 46471 RVA: 0x00276E41 File Offset: 0x00275041
		public bool Is_outDate_constDate(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.outDate_constDate;
		}

		// Token: 0x0600B588 RID: 46472 RVA: 0x00276E5B File Offset: 0x0027505B
		public bool Is_outDate_constDate(GrammarBuilders g, out outDate_constDate value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outDate_constDate)
			{
				value = outDate_constDate.CreateUnsafe(this.Node);
				return true;
			}
			value = default(outDate_constDate);
			return false;
		}

		// Token: 0x0600B589 RID: 46473 RVA: 0x00276E90 File Offset: 0x00275090
		public outDate_constDate? As_outDate_constDate(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.outDate_constDate)
			{
				return null;
			}
			return new outDate_constDate?(outDate_constDate.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B58A RID: 46474 RVA: 0x00276ED0 File Offset: 0x002750D0
		public outDate_constDate Cast_outDate_constDate(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outDate_constDate)
			{
				return outDate_constDate.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_outDate_constDate is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B58B RID: 46475 RVA: 0x00276F28 File Offset: 0x00275128
		public T Switch<T>(GrammarBuilders g, Func<outDate_date, T> func0, Func<outDate_constDate, T> func1)
		{
			outDate_date outDate_date;
			if (this.Is_outDate_date(g, out outDate_date))
			{
				return func0(outDate_date);
			}
			outDate_constDate outDate_constDate;
			if (this.Is_outDate_constDate(g, out outDate_constDate))
			{
				return func1(outDate_constDate);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol outDate");
		}

		// Token: 0x0600B58C RID: 46476 RVA: 0x00276F80 File Offset: 0x00275180
		public void Switch(GrammarBuilders g, Action<outDate_date> func0, Action<outDate_constDate> func1)
		{
			outDate_date outDate_date;
			if (this.Is_outDate_date(g, out outDate_date))
			{
				func0(outDate_date);
				return;
			}
			outDate_constDate outDate_constDate;
			if (this.Is_outDate_constDate(g, out outDate_constDate))
			{
				func1(outDate_constDate);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol outDate");
		}

		// Token: 0x0600B58D RID: 46477 RVA: 0x00276FD7 File Offset: 0x002751D7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B58E RID: 46478 RVA: 0x00276FEC File Offset: 0x002751EC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B58F RID: 46479 RVA: 0x00277016 File Offset: 0x00275216
		public bool Equals(outDate other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400464A RID: 17994
		private ProgramNode _node;
	}
}
