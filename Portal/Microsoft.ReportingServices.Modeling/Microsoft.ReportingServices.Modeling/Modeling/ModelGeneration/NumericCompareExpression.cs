using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000EF RID: 239
	public sealed class NumericCompareExpression
	{
		// Token: 0x06000C29 RID: 3113 RVA: 0x000281FC File Offset: 0x000263FC
		public bool Parse(string expr)
		{
			string text = StringUtil.Join("|", NumericCompareExpression.ValidOps);
			string text2 = StringUtil.FormatInvariant("^(?'op'{0})?(?'value'\\d+(\\.\\d+)?)$", new object[] { text });
			Match match = Regex.Match(expr, text2);
			if (!match.Success)
			{
				return false;
			}
			this.m_compareNumber = Convert.ToDecimal(match.Groups["value"].Value, CultureInfo.InvariantCulture);
			this.m_compareOp = (match.Groups["op"].Success ? match.Groups["op"].Value : "=");
			return true;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000282A0 File Offset: 0x000264A0
		public bool Evaluate(decimal number)
		{
			string compareOp = this.m_compareOp;
			if (compareOp == "<=")
			{
				return number <= this.m_compareNumber;
			}
			if (compareOp == ">=")
			{
				return number >= this.m_compareNumber;
			}
			if (compareOp == "<")
			{
				return number < this.m_compareNumber;
			}
			if (compareOp == ">")
			{
				return number > this.m_compareNumber;
			}
			if (!(compareOp == "="))
			{
				throw new InvalidOperationException();
			}
			return number == this.m_compareNumber;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0002833D File Offset: 0x0002653D
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x00028345 File Offset: 0x00026545
		public decimal Number
		{
			get
			{
				return this.m_compareNumber;
			}
			set
			{
				this.m_compareNumber = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0002834E File Offset: 0x0002654E
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x00028356 File Offset: 0x00026556
		public string Operator
		{
			get
			{
				return this.m_compareOp;
			}
			set
			{
				if (!NumericCompareExpression.IsValidOperator(value))
				{
					throw new ArgumentException();
				}
				this.m_compareOp = value;
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002836D File Offset: 0x0002656D
		public static bool IsValidOperator(string op)
		{
			return NumericCompareExpression.ValidOps.Contains(op);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002837A File Offset: 0x0002657A
		public static bool IsValidExpression(string expr)
		{
			return new NumericCompareExpression().Parse(expr);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00028387 File Offset: 0x00026587
		public static bool Evaluate(string expr, decimal number)
		{
			NumericCompareExpression numericCompareExpression = new NumericCompareExpression();
			if (!numericCompareExpression.Parse(expr))
			{
				throw new ArgumentException();
			}
			return numericCompareExpression.Evaluate(number);
		}

		// Token: 0x0400050B RID: 1291
		private static readonly ReadOnlyCollection<string> ValidOps = new ReadOnlyCollection<string>(new string[] { "<=", ">=", "<", ">", "=" });

		// Token: 0x0400050C RID: 1292
		private decimal m_compareNumber;

		// Token: 0x0400050D RID: 1293
		private string m_compareOp = "=";
	}
}
