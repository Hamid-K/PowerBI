using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000964 RID: 2404
	public struct primarySelector : IProgramNodeBuilder, IEquatable<primarySelector>
	{
		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060038C0 RID: 14528 RVA: 0x000B056A File Offset: 0x000AE76A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000B0572 File Offset: 0x000AE772
		private primarySelector(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x000B057B File Offset: 0x000AE77B
		public static primarySelector CreateUnsafe(ProgramNode node)
		{
			return new primarySelector(node);
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x000B0584 File Offset: 0x000AE784
		public static primarySelector? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.primarySelector)
			{
				return null;
			}
			return new primarySelector?(primarySelector.CreateUnsafe(node));
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000B05BE File Offset: 0x000AE7BE
		public static primarySelector CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new primarySelector(new Hole(g.Symbol.primarySelector, holeId));
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x000B05D6 File Offset: 0x000AE7D6
		public bool Is_BreakLine(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.BreakLine;
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000B05F0 File Offset: 0x000AE7F0
		public bool Is_BreakLine(GrammarBuilders g, out BreakLine value)
		{
			if (this.Node.GrammarRule == g.Rule.BreakLine)
			{
				value = BreakLine.CreateUnsafe(this.Node);
				return true;
			}
			value = default(BreakLine);
			return false;
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000B0628 File Offset: 0x000AE828
		public BreakLine? As_BreakLine(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.BreakLine)
			{
				return null;
			}
			return new BreakLine?(BreakLine.CreateUnsafe(this.Node));
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000B0668 File Offset: 0x000AE868
		public BreakLine Cast_BreakLine(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.BreakLine)
			{
				return BreakLine.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_BreakLine is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x000B06BD File Offset: 0x000AE8BD
		public bool Is_TwoLineKeyValue(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TwoLineKeyValue;
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x000B06D7 File Offset: 0x000AE8D7
		public bool Is_TwoLineKeyValue(GrammarBuilders g, out TwoLineKeyValue value)
		{
			if (this.Node.GrammarRule == g.Rule.TwoLineKeyValue)
			{
				value = TwoLineKeyValue.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TwoLineKeyValue);
			return false;
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000B070C File Offset: 0x000AE90C
		public TwoLineKeyValue? As_TwoLineKeyValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TwoLineKeyValue)
			{
				return null;
			}
			return new TwoLineKeyValue?(TwoLineKeyValue.CreateUnsafe(this.Node));
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000B074C File Offset: 0x000AE94C
		public TwoLineKeyValue Cast_TwoLineKeyValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TwoLineKeyValue)
			{
				return TwoLineKeyValue.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TwoLineKeyValue is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000B07A1 File Offset: 0x000AE9A1
		public bool Is_KeyValue(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.KeyValue;
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000B07BB File Offset: 0x000AE9BB
		public bool Is_KeyValue(GrammarBuilders g, out KeyValue value)
		{
			if (this.Node.GrammarRule == g.Rule.KeyValue)
			{
				value = KeyValue.CreateUnsafe(this.Node);
				return true;
			}
			value = default(KeyValue);
			return false;
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000B07F0 File Offset: 0x000AE9F0
		public KeyValue? As_KeyValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.KeyValue)
			{
				return null;
			}
			return new KeyValue?(KeyValue.CreateUnsafe(this.Node));
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x000B0830 File Offset: 0x000AEA30
		public KeyValue Cast_KeyValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.KeyValue)
			{
				return KeyValue.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_KeyValue is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000B0885 File Offset: 0x000AEA85
		public bool Is_KeyQuote(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.KeyQuote;
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000B089F File Offset: 0x000AEA9F
		public bool Is_KeyQuote(GrammarBuilders g, out KeyQuote value)
		{
			if (this.Node.GrammarRule == g.Rule.KeyQuote)
			{
				value = KeyQuote.CreateUnsafe(this.Node);
				return true;
			}
			value = default(KeyQuote);
			return false;
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x000B08D4 File Offset: 0x000AEAD4
		public KeyQuote? As_KeyQuote(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.KeyQuote)
			{
				return null;
			}
			return new KeyQuote?(KeyQuote.CreateUnsafe(this.Node));
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000B0914 File Offset: 0x000AEB14
		public KeyQuote Cast_KeyQuote(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.KeyQuote)
			{
				return KeyQuote.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_KeyQuote is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000B096C File Offset: 0x000AEB6C
		public T Switch<T>(GrammarBuilders g, Func<BreakLine, T> func0, Func<TwoLineKeyValue, T> func1, Func<KeyValue, T> func2, Func<KeyQuote, T> func3)
		{
			BreakLine breakLine;
			if (this.Is_BreakLine(g, out breakLine))
			{
				return func0(breakLine);
			}
			TwoLineKeyValue twoLineKeyValue;
			if (this.Is_TwoLineKeyValue(g, out twoLineKeyValue))
			{
				return func1(twoLineKeyValue);
			}
			KeyValue keyValue;
			if (this.Is_KeyValue(g, out keyValue))
			{
				return func2(keyValue);
			}
			KeyQuote keyQuote;
			if (this.Is_KeyQuote(g, out keyQuote))
			{
				return func3(keyQuote);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol primarySelector");
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000B09EC File Offset: 0x000AEBEC
		public void Switch(GrammarBuilders g, Action<BreakLine> func0, Action<TwoLineKeyValue> func1, Action<KeyValue> func2, Action<KeyQuote> func3)
		{
			BreakLine breakLine;
			if (this.Is_BreakLine(g, out breakLine))
			{
				func0(breakLine);
				return;
			}
			TwoLineKeyValue twoLineKeyValue;
			if (this.Is_TwoLineKeyValue(g, out twoLineKeyValue))
			{
				func1(twoLineKeyValue);
				return;
			}
			KeyValue keyValue;
			if (this.Is_KeyValue(g, out keyValue))
			{
				func2(keyValue);
				return;
			}
			KeyQuote keyQuote;
			if (this.Is_KeyQuote(g, out keyQuote))
			{
				func3(keyQuote);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol primarySelector");
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000B0A6B File Offset: 0x000AEC6B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000B0A80 File Offset: 0x000AEC80
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x000B0AAA File Offset: 0x000AECAA
		public bool Equals(primarySelector other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A84 RID: 6788
		private ProgramNode _node;
	}
}
