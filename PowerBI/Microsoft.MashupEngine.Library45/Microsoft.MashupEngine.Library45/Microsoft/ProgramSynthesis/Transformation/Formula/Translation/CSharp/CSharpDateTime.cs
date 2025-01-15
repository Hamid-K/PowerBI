using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200194D RID: 6477
	internal class CSharpDateTime : FormulaExpression
	{
		// Token: 0x0600D3A2 RID: 54178 RVA: 0x002D1AC0 File Offset: 0x002CFCC0
		public CSharpDateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day, FormulaExpression hour, FormulaExpression minute, FormulaExpression second, FormulaExpression millisecond)
		{
			this.Year = year;
			this.Month = month;
			this.Day = day;
			this.Hour = hour;
			this.Minute = minute;
			this.Second = second;
			this.Millisecond = millisecond;
			List<FormulaExpression> list = new List<FormulaExpression> { this.Year, this.Month, this.Day };
			if (hour != null && minute != null && second != null && millisecond != null)
			{
				list.Add(this.Hour);
				list.Add(this.Minute);
				list.Add(this.Second);
				list.Add(this.Millisecond);
			}
			base.Children = list;
		}

		// Token: 0x1700232B RID: 9003
		// (get) Token: 0x0600D3A3 RID: 54179 RVA: 0x002D1B91 File Offset: 0x002CFD91
		public FormulaExpression Day { get; }

		// Token: 0x1700232C RID: 9004
		// (get) Token: 0x0600D3A4 RID: 54180 RVA: 0x002D1B99 File Offset: 0x002CFD99
		public FormulaExpression Hour { get; }

		// Token: 0x1700232D RID: 9005
		// (get) Token: 0x0600D3A5 RID: 54181 RVA: 0x002D1BA1 File Offset: 0x002CFDA1
		public FormulaExpression Millisecond { get; }

		// Token: 0x1700232E RID: 9006
		// (get) Token: 0x0600D3A6 RID: 54182 RVA: 0x002D1BA9 File Offset: 0x002CFDA9
		public FormulaExpression Minute { get; }

		// Token: 0x1700232F RID: 9007
		// (get) Token: 0x0600D3A7 RID: 54183 RVA: 0x002D1BB1 File Offset: 0x002CFDB1
		public FormulaExpression Month { get; }

		// Token: 0x17002330 RID: 9008
		// (get) Token: 0x0600D3A8 RID: 54184 RVA: 0x002D1BB9 File Offset: 0x002CFDB9
		public FormulaExpression Second { get; }

		// Token: 0x17002331 RID: 9009
		// (get) Token: 0x0600D3A9 RID: 54185 RVA: 0x002D1BC1 File Offset: 0x002CFDC1
		public FormulaExpression Year { get; }

		// Token: 0x0600D3AA RID: 54186 RVA: 0x002D1BCC File Offset: 0x002CFDCC
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			FormulaExpression formulaExpression = this.Year.Accept<FormulaExpression>(visitor);
			FormulaExpression formulaExpression2 = this.Month.Accept<FormulaExpression>(visitor);
			FormulaExpression formulaExpression3 = this.Day.Accept<FormulaExpression>(visitor);
			FormulaExpression hour = this.Hour;
			FormulaExpression formulaExpression4 = ((hour != null) ? hour.Accept<FormulaExpression>(visitor) : null);
			FormulaExpression minute = this.Minute;
			FormulaExpression formulaExpression5 = ((minute != null) ? minute.Accept<FormulaExpression>(visitor) : null);
			FormulaExpression second = this.Second;
			FormulaExpression formulaExpression6 = ((second != null) ? second.Accept<FormulaExpression>(visitor) : null);
			FormulaExpression millisecond = this.Millisecond;
			return new CSharpDateTime(formulaExpression, formulaExpression2, formulaExpression3, formulaExpression4, formulaExpression5, formulaExpression6, (millisecond != null) ? millisecond.Accept<FormulaExpression>(visitor) : null);
		}

		// Token: 0x0600D3AB RID: 54187 RVA: 0x002D1C50 File Offset: 0x002CFE50
		protected override string ToCodeString()
		{
			return "new DateTime(" + string.Join(", ", base.Children.Select((FormulaExpression c) => c.ToString())) + ")";
		}
	}
}
