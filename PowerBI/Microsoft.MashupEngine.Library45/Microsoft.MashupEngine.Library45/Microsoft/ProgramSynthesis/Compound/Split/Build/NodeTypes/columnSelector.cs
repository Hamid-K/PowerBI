using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000963 RID: 2403
	public struct columnSelector : IProgramNodeBuilder, IEquatable<columnSelector>
	{
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060038A2 RID: 14498 RVA: 0x000AFF0A File Offset: 0x000AE10A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000AFF12 File Offset: 0x000AE112
		private columnSelector(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000AFF1B File Offset: 0x000AE11B
		public static columnSelector CreateUnsafe(ProgramNode node)
		{
			return new columnSelector(node);
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000AFF24 File Offset: 0x000AE124
		public static columnSelector? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.columnSelector)
			{
				return null;
			}
			return new columnSelector?(columnSelector.CreateUnsafe(node));
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000AFF5E File Offset: 0x000AE15E
		public static columnSelector CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new columnSelector(new Hole(g.Symbol.columnSelector, holeId));
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000AFF76 File Offset: 0x000AE176
		public bool Is_KthLine(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.KthLine;
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x000AFF90 File Offset: 0x000AE190
		public bool Is_KthLine(GrammarBuilders g, out KthLine value)
		{
			if (this.Node.GrammarRule == g.Rule.KthLine)
			{
				value = KthLine.CreateUnsafe(this.Node);
				return true;
			}
			value = default(KthLine);
			return false;
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000AFFC8 File Offset: 0x000AE1C8
		public KthLine? As_KthLine(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.KthLine)
			{
				return null;
			}
			return new KthLine?(KthLine.CreateUnsafe(this.Node));
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000B0008 File Offset: 0x000AE208
		public KthLine Cast_KthLine(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.KthLine)
			{
				return KthLine.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_KthLine is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000B005D File Offset: 0x000AE25D
		public bool Is_KthKeyValue(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.KthKeyValue;
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000B0077 File Offset: 0x000AE277
		public bool Is_KthKeyValue(GrammarBuilders g, out KthKeyValue value)
		{
			if (this.Node.GrammarRule == g.Rule.KthKeyValue)
			{
				value = KthKeyValue.CreateUnsafe(this.Node);
				return true;
			}
			value = default(KthKeyValue);
			return false;
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000B00AC File Offset: 0x000AE2AC
		public KthKeyValue? As_KthKeyValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.KthKeyValue)
			{
				return null;
			}
			return new KthKeyValue?(KthKeyValue.CreateUnsafe(this.Node));
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000B00EC File Offset: 0x000AE2EC
		public KthKeyValue Cast_KthKeyValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.KthKeyValue)
			{
				return KthKeyValue.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_KthKeyValue is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000B0141 File Offset: 0x000AE341
		public bool Is_KthTwoLineKeyValue(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.KthTwoLineKeyValue;
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000B015B File Offset: 0x000AE35B
		public bool Is_KthTwoLineKeyValue(GrammarBuilders g, out KthTwoLineKeyValue value)
		{
			if (this.Node.GrammarRule == g.Rule.KthTwoLineKeyValue)
			{
				value = KthTwoLineKeyValue.CreateUnsafe(this.Node);
				return true;
			}
			value = default(KthTwoLineKeyValue);
			return false;
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000B0190 File Offset: 0x000AE390
		public KthTwoLineKeyValue? As_KthTwoLineKeyValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.KthTwoLineKeyValue)
			{
				return null;
			}
			return new KthTwoLineKeyValue?(KthTwoLineKeyValue.CreateUnsafe(this.Node));
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000B01D0 File Offset: 0x000AE3D0
		public KthTwoLineKeyValue Cast_KthTwoLineKeyValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.KthTwoLineKeyValue)
			{
				return KthTwoLineKeyValue.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_KthTwoLineKeyValue is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000B0225 File Offset: 0x000AE425
		public bool Is_KthKeyQuote(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.KthKeyQuote;
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000B023F File Offset: 0x000AE43F
		public bool Is_KthKeyQuote(GrammarBuilders g, out KthKeyQuote value)
		{
			if (this.Node.GrammarRule == g.Rule.KthKeyQuote)
			{
				value = KthKeyQuote.CreateUnsafe(this.Node);
				return true;
			}
			value = default(KthKeyQuote);
			return false;
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000B0274 File Offset: 0x000AE474
		public KthKeyQuote? As_KthKeyQuote(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.KthKeyQuote)
			{
				return null;
			}
			return new KthKeyQuote?(KthKeyQuote.CreateUnsafe(this.Node));
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000B02B4 File Offset: 0x000AE4B4
		public KthKeyQuote Cast_KthKeyQuote(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.KthKeyQuote)
			{
				return KthKeyQuote.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_KthKeyQuote is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000B0309 File Offset: 0x000AE509
		public bool Is_KthKeyValueFw(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.KthKeyValueFw;
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000B0323 File Offset: 0x000AE523
		public bool Is_KthKeyValueFw(GrammarBuilders g, out KthKeyValueFw value)
		{
			if (this.Node.GrammarRule == g.Rule.KthKeyValueFw)
			{
				value = KthKeyValueFw.CreateUnsafe(this.Node);
				return true;
			}
			value = default(KthKeyValueFw);
			return false;
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000B0358 File Offset: 0x000AE558
		public KthKeyValueFw? As_KthKeyValueFw(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.KthKeyValueFw)
			{
				return null;
			}
			return new KthKeyValueFw?(KthKeyValueFw.CreateUnsafe(this.Node));
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x000B0398 File Offset: 0x000AE598
		public KthKeyValueFw Cast_KthKeyValueFw(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.KthKeyValueFw)
			{
				return KthKeyValueFw.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_KthKeyValueFw is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000B03F0 File Offset: 0x000AE5F0
		public T Switch<T>(GrammarBuilders g, Func<KthLine, T> func0, Func<KthKeyValue, T> func1, Func<KthTwoLineKeyValue, T> func2, Func<KthKeyQuote, T> func3, Func<KthKeyValueFw, T> func4)
		{
			KthLine kthLine;
			if (this.Is_KthLine(g, out kthLine))
			{
				return func0(kthLine);
			}
			KthKeyValue kthKeyValue;
			if (this.Is_KthKeyValue(g, out kthKeyValue))
			{
				return func1(kthKeyValue);
			}
			KthTwoLineKeyValue kthTwoLineKeyValue;
			if (this.Is_KthTwoLineKeyValue(g, out kthTwoLineKeyValue))
			{
				return func2(kthTwoLineKeyValue);
			}
			KthKeyQuote kthKeyQuote;
			if (this.Is_KthKeyQuote(g, out kthKeyQuote))
			{
				return func3(kthKeyQuote);
			}
			KthKeyValueFw kthKeyValueFw;
			if (this.Is_KthKeyValueFw(g, out kthKeyValueFw))
			{
				return func4(kthKeyValueFw);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol columnSelector");
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x000B0484 File Offset: 0x000AE684
		public void Switch(GrammarBuilders g, Action<KthLine> func0, Action<KthKeyValue> func1, Action<KthTwoLineKeyValue> func2, Action<KthKeyQuote> func3, Action<KthKeyValueFw> func4)
		{
			KthLine kthLine;
			if (this.Is_KthLine(g, out kthLine))
			{
				func0(kthLine);
				return;
			}
			KthKeyValue kthKeyValue;
			if (this.Is_KthKeyValue(g, out kthKeyValue))
			{
				func1(kthKeyValue);
				return;
			}
			KthTwoLineKeyValue kthTwoLineKeyValue;
			if (this.Is_KthTwoLineKeyValue(g, out kthTwoLineKeyValue))
			{
				func2(kthTwoLineKeyValue);
				return;
			}
			KthKeyQuote kthKeyQuote;
			if (this.Is_KthKeyQuote(g, out kthKeyQuote))
			{
				func3(kthKeyQuote);
				return;
			}
			KthKeyValueFw kthKeyValueFw;
			if (this.Is_KthKeyValueFw(g, out kthKeyValueFw))
			{
				func4(kthKeyValueFw);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol columnSelector");
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x000B0518 File Offset: 0x000AE718
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x000B052C File Offset: 0x000AE72C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x000B0556 File Offset: 0x000AE756
		public bool Equals(columnSelector other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A83 RID: 6787
		private ProgramNode _node;
	}
}
