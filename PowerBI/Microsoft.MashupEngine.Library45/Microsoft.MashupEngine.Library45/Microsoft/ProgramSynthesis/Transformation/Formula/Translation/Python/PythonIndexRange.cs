using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001875 RID: 6261
	internal class PythonIndexRange : FormulaExpression
	{
		// Token: 0x0600CC2D RID: 52269 RVA: 0x002B8FAC File Offset: 0x002B71AC
		public PythonIndexRange(FormulaExpression start, FormulaExpression end)
		{
			this.Start = start;
			this.End = end;
			List<FormulaExpression> list = new List<FormulaExpression>();
			if (this.Start != null)
			{
				list.Add(this.Start);
			}
			if (this.End != null)
			{
				list.Add(this.End);
			}
			base.Children = list;
		}

		// Token: 0x17002279 RID: 8825
		// (get) Token: 0x0600CC2E RID: 52270 RVA: 0x002B900E File Offset: 0x002B720E
		public FormulaExpression End { get; }

		// Token: 0x1700227A RID: 8826
		// (get) Token: 0x0600CC2F RID: 52271 RVA: 0x002B9016 File Offset: 0x002B7216
		public FormulaExpression Start { get; }

		// Token: 0x0600CC30 RID: 52272 RVA: 0x002B901E File Offset: 0x002B721E
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			FormulaExpression start = this.Start;
			FormulaExpression formulaExpression = ((start != null) ? start.Accept<FormulaExpression>(visitor) : null);
			FormulaExpression end = this.End;
			return new PythonIndexRange(formulaExpression, (end != null) ? end.Accept<FormulaExpression>(visitor) : null);
		}

		// Token: 0x0600CC31 RID: 52273 RVA: 0x002B904B File Offset: 0x002B724B
		protected override string ToCodeString()
		{
			return string.Format("{0}:{1}", this.Start, this.End);
		}
	}
}
