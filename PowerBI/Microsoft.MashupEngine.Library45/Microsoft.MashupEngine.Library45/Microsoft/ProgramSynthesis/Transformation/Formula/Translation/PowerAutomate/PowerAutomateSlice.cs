using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001902 RID: 6402
	internal class PowerAutomateSlice : PowerAutomateFunc
	{
		// Token: 0x0600D0DC RID: 53468 RVA: 0x002C8C50 File Offset: 0x002C6E50
		public PowerAutomateSlice(FormulaExpression subject, FormulaExpression startIndex, FormulaExpression endIndex)
			: base("Slice", Array.Empty<FormulaExpression>())
		{
			this.Subject = subject;
			this.StartIndex = startIndex;
			this.EndIndex = endIndex;
			IReadOnlyList<FormulaExpression> readOnlyList;
			if (!(this.EndIndex == null))
			{
				FormulaExpression[] array = new FormulaExpression[3];
				array[0] = this.Subject;
				array[1] = this.StartIndex;
				readOnlyList = array;
				array[2] = this.EndIndex;
			}
			else
			{
				FormulaExpression[] array2 = new FormulaExpression[2];
				array2[0] = this.Subject;
				readOnlyList = array2;
				array2[1] = this.StartIndex;
			}
			base.Arguments = readOnlyList;
			base.Children = base.Arguments;
		}

		// Token: 0x170022E4 RID: 8932
		// (get) Token: 0x0600D0DD RID: 53469 RVA: 0x002C8CDD File Offset: 0x002C6EDD
		public FormulaExpression EndIndex { get; }

		// Token: 0x170022E5 RID: 8933
		// (get) Token: 0x0600D0DE RID: 53470 RVA: 0x002C8CE5 File Offset: 0x002C6EE5
		public FormulaExpression StartIndex { get; }

		// Token: 0x170022E6 RID: 8934
		// (get) Token: 0x0600D0DF RID: 53471 RVA: 0x002C8CED File Offset: 0x002C6EED
		public FormulaExpression Subject { get; }

		// Token: 0x0600D0E0 RID: 53472 RVA: 0x002C8CF5 File Offset: 0x002C6EF5
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			FormulaExpression formulaExpression = this.Subject.Accept<FormulaExpression>(visitor);
			FormulaExpression formulaExpression2 = this.StartIndex.Accept<FormulaExpression>(visitor);
			FormulaExpression endIndex = this.EndIndex;
			return new PowerAutomateSlice(formulaExpression, formulaExpression2, (endIndex != null) ? endIndex.Accept<FormulaExpression>(visitor) : null);
		}
	}
}
