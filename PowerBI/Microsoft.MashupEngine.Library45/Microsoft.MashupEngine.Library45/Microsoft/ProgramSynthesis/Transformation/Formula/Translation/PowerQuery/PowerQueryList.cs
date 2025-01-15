using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018A2 RID: 6306
	internal class PowerQueryList : FormulaExpression
	{
		// Token: 0x0600CDFB RID: 52731 RVA: 0x002BF80F File Offset: 0x002BDA0F
		public PowerQueryList(IEnumerable<FormulaExpression> items)
		{
			this.Items = items.ToList<FormulaExpression>();
			base.Children = this.Items;
		}

		// Token: 0x0600CDFC RID: 52732 RVA: 0x002BF82F File Offset: 0x002BDA2F
		public PowerQueryList(params FormulaExpression[] items)
			: this(items)
		{
		}

		// Token: 0x1700229E RID: 8862
		// (get) Token: 0x0600CDFD RID: 52733 RVA: 0x002BF838 File Offset: 0x002BDA38
		public List<FormulaExpression> Items { get; }

		// Token: 0x0600CDFE RID: 52734 RVA: 0x002BF840 File Offset: 0x002BDA40
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryList(this.Items.Select((FormulaExpression item) => item.Accept<FormulaExpression>(visitor)));
		}

		// Token: 0x0600CDFF RID: 52735 RVA: 0x002BF876 File Offset: 0x002BDA76
		protected override string ToCodeString()
		{
			return "{" + string.Join<FormulaExpression>(", ", this.Items) + "}";
		}
	}
}
