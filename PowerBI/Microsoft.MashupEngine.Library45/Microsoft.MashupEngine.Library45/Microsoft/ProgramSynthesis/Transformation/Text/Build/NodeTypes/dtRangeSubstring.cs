using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C46 RID: 7238
	public struct dtRangeSubstring : IProgramNodeBuilder, IEquatable<dtRangeSubstring>
	{
		// Token: 0x170028DC RID: 10460
		// (get) Token: 0x0600F478 RID: 62584 RVA: 0x0034551A File Offset: 0x0034371A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F479 RID: 62585 RVA: 0x00345522 File Offset: 0x00343722
		private dtRangeSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F47A RID: 62586 RVA: 0x0034552B File Offset: 0x0034372B
		public static dtRangeSubstring CreateUnsafe(ProgramNode node)
		{
			return new dtRangeSubstring(node);
		}

		// Token: 0x0600F47B RID: 62587 RVA: 0x00345534 File Offset: 0x00343734
		public static dtRangeSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.dtRangeSubstring)
			{
				return null;
			}
			return new dtRangeSubstring?(dtRangeSubstring.CreateUnsafe(node));
		}

		// Token: 0x0600F47C RID: 62588 RVA: 0x0034556E File Offset: 0x0034376E
		public static dtRangeSubstring CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new dtRangeSubstring(new Hole(g.Symbol.dtRangeSubstring, holeId));
		}

		// Token: 0x0600F47D RID: 62589 RVA: 0x00345586 File Offset: 0x00343786
		public bool Is_DtRangeConstStr(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DtRangeConstStr;
		}

		// Token: 0x0600F47E RID: 62590 RVA: 0x003455A0 File Offset: 0x003437A0
		public bool Is_DtRangeConstStr(GrammarBuilders g, out DtRangeConstStr value)
		{
			if (this.Node.GrammarRule == g.Rule.DtRangeConstStr)
			{
				value = DtRangeConstStr.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DtRangeConstStr);
			return false;
		}

		// Token: 0x0600F47F RID: 62591 RVA: 0x003455D8 File Offset: 0x003437D8
		public DtRangeConstStr? As_DtRangeConstStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DtRangeConstStr)
			{
				return null;
			}
			return new DtRangeConstStr?(DtRangeConstStr.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F480 RID: 62592 RVA: 0x00345618 File Offset: 0x00343818
		public DtRangeConstStr Cast_DtRangeConstStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DtRangeConstStr)
			{
				return DtRangeConstStr.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DtRangeConstStr is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F481 RID: 62593 RVA: 0x0034566D File Offset: 0x0034386D
		public bool Is_RangeFormatDateTime(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RangeFormatDateTime;
		}

		// Token: 0x0600F482 RID: 62594 RVA: 0x00345687 File Offset: 0x00343887
		public bool Is_RangeFormatDateTime(GrammarBuilders g, out RangeFormatDateTime value)
		{
			if (this.Node.GrammarRule == g.Rule.RangeFormatDateTime)
			{
				value = RangeFormatDateTime.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RangeFormatDateTime);
			return false;
		}

		// Token: 0x0600F483 RID: 62595 RVA: 0x003456BC File Offset: 0x003438BC
		public RangeFormatDateTime? As_RangeFormatDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RangeFormatDateTime)
			{
				return null;
			}
			return new RangeFormatDateTime?(RangeFormatDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F484 RID: 62596 RVA: 0x003456FC File Offset: 0x003438FC
		public RangeFormatDateTime Cast_RangeFormatDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RangeFormatDateTime)
			{
				return RangeFormatDateTime.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RangeFormatDateTime is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F485 RID: 62597 RVA: 0x00345754 File Offset: 0x00343954
		public T Switch<T>(GrammarBuilders g, Func<DtRangeConstStr, T> func0, Func<RangeFormatDateTime, T> func1)
		{
			DtRangeConstStr dtRangeConstStr;
			if (this.Is_DtRangeConstStr(g, out dtRangeConstStr))
			{
				return func0(dtRangeConstStr);
			}
			RangeFormatDateTime rangeFormatDateTime;
			if (this.Is_RangeFormatDateTime(g, out rangeFormatDateTime))
			{
				return func1(rangeFormatDateTime);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol dtRangeSubstring");
		}

		// Token: 0x0600F486 RID: 62598 RVA: 0x003457AC File Offset: 0x003439AC
		public void Switch(GrammarBuilders g, Action<DtRangeConstStr> func0, Action<RangeFormatDateTime> func1)
		{
			DtRangeConstStr dtRangeConstStr;
			if (this.Is_DtRangeConstStr(g, out dtRangeConstStr))
			{
				func0(dtRangeConstStr);
				return;
			}
			RangeFormatDateTime rangeFormatDateTime;
			if (this.Is_RangeFormatDateTime(g, out rangeFormatDateTime))
			{
				func1(rangeFormatDateTime);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol dtRangeSubstring");
		}

		// Token: 0x0600F487 RID: 62599 RVA: 0x00345803 File Offset: 0x00343A03
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F488 RID: 62600 RVA: 0x00345818 File Offset: 0x00343A18
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F489 RID: 62601 RVA: 0x00345842 File Offset: 0x00343A42
		public bool Equals(dtRangeSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B35 RID: 23349
		private ProgramNode _node;
	}
}
