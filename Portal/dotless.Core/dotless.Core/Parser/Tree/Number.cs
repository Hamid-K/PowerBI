using System;
using System.Globalization;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000041 RID: 65
	public class Number : Node, IOperable, IComparable
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000C289 File Offset: 0x0000A489
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000C291 File Offset: 0x0000A491
		public double Value { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000C29A File Offset: 0x0000A49A
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000C2A2 File Offset: 0x0000A4A2
		public string Unit { get; set; }

		// Token: 0x06000280 RID: 640 RVA: 0x0000C2AB File Offset: 0x0000A4AB
		public Number(string value, string unit)
		{
			this.Value = double.Parse(value, CultureInfo.InvariantCulture);
			this.Unit = unit;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C2CB File Offset: 0x0000A4CB
		public Number(double value, string unit)
		{
			this.Value = value;
			this.Unit = unit;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C2E1 File Offset: 0x0000A4E1
		public Number(double value)
			: this(value, "")
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		private string FormatValue()
		{
			return this.Value.ToString("0." + new string('#', this.GetPrecision()), CultureInfo.InvariantCulture);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C327 File Offset: 0x0000A527
		private int GetPrecision()
		{
			return 9;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C32B File Offset: 0x0000A52B
		protected override Node CloneCore()
		{
			return new Number(this.Value, this.Unit);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C33E File Offset: 0x0000A53E
		public override void AppendCSS(Env env)
		{
			env.Output.Append(this.FormatValue()).Append(this.Unit);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C360 File Offset: 0x0000A560
		public Node Operate(Operation op, Node other)
		{
			Guard.ExpectNode<Number>(other, "right hand side of " + op.Operator, op.Location);
			Number number = (Number)other;
			string text = this.Unit;
			string unit = number.Unit;
			if (this.preferUnitFromSecondOperand && !string.IsNullOrEmpty(unit))
			{
				text = unit;
			}
			else if (string.IsNullOrEmpty(text))
			{
				text = unit;
			}
			else
			{
				string.IsNullOrEmpty(unit);
			}
			return new Number(Operation.Operate(op.Operator, this.Value, number.Value), text)
			{
				preferUnitFromSecondOperand = (text == unit && op.Operator == "/")
			}.ReducedFrom<Node>(new Node[] { this, other });
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000C418 File Offset: 0x0000A618
		public Color ToColor()
		{
			return new Color(this.Value, this.Value, this.Value);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000C431 File Offset: 0x0000A631
		public double ToNumber()
		{
			return this.ToNumber(1.0);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C442 File Offset: 0x0000A642
		public double ToNumber(double max)
		{
			if (!(this.Unit == "%"))
			{
				return this.Value;
			}
			return this.Value * max / 100.0;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000C46F File Offset: 0x0000A66F
		public static Number operator -(Number n)
		{
			return new Number(-n.Value, n.Unit);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000C484 File Offset: 0x0000A684
		public int CompareTo(object obj)
		{
			Number number = obj as Number;
			if (!number)
			{
				return -1;
			}
			if (number.Value > this.Value)
			{
				return -1;
			}
			if (number.Value < this.Value)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04000096 RID: 150
		private bool preferUnitFromSecondOperand;
	}
}
