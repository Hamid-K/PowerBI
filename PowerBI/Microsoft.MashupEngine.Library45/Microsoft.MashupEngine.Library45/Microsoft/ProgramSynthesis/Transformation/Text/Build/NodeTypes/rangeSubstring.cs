using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C43 RID: 7235
	public struct rangeSubstring : IProgramNodeBuilder, IEquatable<rangeSubstring>
	{
		// Token: 0x170028D9 RID: 10457
		// (get) Token: 0x0600F448 RID: 62536 RVA: 0x00344DB2 File Offset: 0x00342FB2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F449 RID: 62537 RVA: 0x00344DBA File Offset: 0x00342FBA
		private rangeSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F44A RID: 62538 RVA: 0x00344DC3 File Offset: 0x00342FC3
		public static rangeSubstring CreateUnsafe(ProgramNode node)
		{
			return new rangeSubstring(node);
		}

		// Token: 0x0600F44B RID: 62539 RVA: 0x00344DCC File Offset: 0x00342FCC
		public static rangeSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.rangeSubstring)
			{
				return null;
			}
			return new rangeSubstring?(rangeSubstring.CreateUnsafe(node));
		}

		// Token: 0x0600F44C RID: 62540 RVA: 0x00344E06 File Offset: 0x00343006
		public static rangeSubstring CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new rangeSubstring(new Hole(g.Symbol.rangeSubstring, holeId));
		}

		// Token: 0x0600F44D RID: 62541 RVA: 0x00344E1E File Offset: 0x0034301E
		public bool Is_RangeConstStr(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RangeConstStr;
		}

		// Token: 0x0600F44E RID: 62542 RVA: 0x00344E38 File Offset: 0x00343038
		public bool Is_RangeConstStr(GrammarBuilders g, out RangeConstStr value)
		{
			if (this.Node.GrammarRule == g.Rule.RangeConstStr)
			{
				value = RangeConstStr.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RangeConstStr);
			return false;
		}

		// Token: 0x0600F44F RID: 62543 RVA: 0x00344E70 File Offset: 0x00343070
		public RangeConstStr? As_RangeConstStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RangeConstStr)
			{
				return null;
			}
			return new RangeConstStr?(RangeConstStr.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F450 RID: 62544 RVA: 0x00344EB0 File Offset: 0x003430B0
		public RangeConstStr Cast_RangeConstStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RangeConstStr)
			{
				return RangeConstStr.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RangeConstStr is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F451 RID: 62545 RVA: 0x00344F05 File Offset: 0x00343105
		public bool Is_RangeFormatNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RangeFormatNumber;
		}

		// Token: 0x0600F452 RID: 62546 RVA: 0x00344F1F File Offset: 0x0034311F
		public bool Is_RangeFormatNumber(GrammarBuilders g, out RangeFormatNumber value)
		{
			if (this.Node.GrammarRule == g.Rule.RangeFormatNumber)
			{
				value = RangeFormatNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RangeFormatNumber);
			return false;
		}

		// Token: 0x0600F453 RID: 62547 RVA: 0x00344F54 File Offset: 0x00343154
		public RangeFormatNumber? As_RangeFormatNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RangeFormatNumber)
			{
				return null;
			}
			return new RangeFormatNumber?(RangeFormatNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F454 RID: 62548 RVA: 0x00344F94 File Offset: 0x00343194
		public RangeFormatNumber Cast_RangeFormatNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RangeFormatNumber)
			{
				return RangeFormatNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RangeFormatNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F455 RID: 62549 RVA: 0x00344FEC File Offset: 0x003431EC
		public T Switch<T>(GrammarBuilders g, Func<RangeConstStr, T> func0, Func<RangeFormatNumber, T> func1)
		{
			RangeConstStr rangeConstStr;
			if (this.Is_RangeConstStr(g, out rangeConstStr))
			{
				return func0(rangeConstStr);
			}
			RangeFormatNumber rangeFormatNumber;
			if (this.Is_RangeFormatNumber(g, out rangeFormatNumber))
			{
				return func1(rangeFormatNumber);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol rangeSubstring");
		}

		// Token: 0x0600F456 RID: 62550 RVA: 0x00345044 File Offset: 0x00343244
		public void Switch(GrammarBuilders g, Action<RangeConstStr> func0, Action<RangeFormatNumber> func1)
		{
			RangeConstStr rangeConstStr;
			if (this.Is_RangeConstStr(g, out rangeConstStr))
			{
				func0(rangeConstStr);
				return;
			}
			RangeFormatNumber rangeFormatNumber;
			if (this.Is_RangeFormatNumber(g, out rangeFormatNumber))
			{
				func1(rangeFormatNumber);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol rangeSubstring");
		}

		// Token: 0x0600F457 RID: 62551 RVA: 0x0034509B File Offset: 0x0034329B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F458 RID: 62552 RVA: 0x003450B0 File Offset: 0x003432B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F459 RID: 62553 RVA: 0x003450DA File Offset: 0x003432DA
		public bool Equals(rangeSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B32 RID: 23346
		private ProgramNode _node;
	}
}
