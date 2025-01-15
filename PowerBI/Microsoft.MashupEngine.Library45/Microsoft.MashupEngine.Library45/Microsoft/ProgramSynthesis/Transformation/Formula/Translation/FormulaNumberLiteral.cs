using System;
using System.Globalization;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017EE RID: 6126
	internal abstract class FormulaNumberLiteral : FormulaExpression, IFormulaLiteral<double>, IFormulaTyped
	{
		// Token: 0x0600C9BC RID: 51644 RVA: 0x002B2C15 File Offset: 0x002B0E15
		protected FormulaNumberLiteral(double value)
		{
			this.Value = value;
			base.Children = new FormulaExpression[0];
		}

		// Token: 0x170021F6 RID: 8694
		// (get) Token: 0x0600C9BD RID: 51645 RVA: 0x002B2C30 File Offset: 0x002B0E30
		public virtual Type Type
		{
			get
			{
				return typeof(double);
			}
		}

		// Token: 0x170021F7 RID: 8695
		// (get) Token: 0x0600C9BE RID: 51646 RVA: 0x002B2C3C File Offset: 0x002B0E3C
		public double Value { get; }

		// Token: 0x0600C9BF RID: 51647 RVA: 0x002B2C44 File Offset: 0x002B0E44
		protected override string ToCodeString()
		{
			return this.Value.ToString(FormulaNumberLiteral._format, CultureInfo.InvariantCulture);
		}

		// Token: 0x04004F35 RID: 20277
		private static readonly string _format = "0." + new string('#', 100);
	}
}
