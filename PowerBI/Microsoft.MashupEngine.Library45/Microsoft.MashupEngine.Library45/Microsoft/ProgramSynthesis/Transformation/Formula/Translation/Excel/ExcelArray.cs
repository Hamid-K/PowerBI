using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001929 RID: 6441
	internal class ExcelArray : FormulaExpression
	{
		// Token: 0x0600D20F RID: 53775 RVA: 0x002CC64E File Offset: 0x002CA84E
		public ExcelArray(IEnumerable<FormulaExpression> items)
		{
			this.Items = (items as List<FormulaExpression>) ?? items.ToList<FormulaExpression>();
			base.Children = this.Items;
		}

		// Token: 0x0600D210 RID: 53776 RVA: 0x002B2B28 File Offset: 0x002B0D28
		protected ExcelArray()
		{
		}

		// Token: 0x170022FF RID: 8959
		// (get) Token: 0x0600D211 RID: 53777 RVA: 0x002CC678 File Offset: 0x002CA878
		// (set) Token: 0x0600D212 RID: 53778 RVA: 0x002CC680 File Offset: 0x002CA880
		public IReadOnlyList<FormulaExpression> Items { get; protected set; }

		// Token: 0x0600D213 RID: 53779 RVA: 0x002CC689 File Offset: 0x002CA889
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelArray(this.Items.Accept(visitor));
		}

		// Token: 0x0600D214 RID: 53780 RVA: 0x002CC69C File Offset: 0x002CA89C
		protected override string ToCodeString()
		{
			string text = string.Join(",", this.Items.Select((FormulaExpression i) => i.ToString()));
			return "{" + text + "}";
		}
	}
}
