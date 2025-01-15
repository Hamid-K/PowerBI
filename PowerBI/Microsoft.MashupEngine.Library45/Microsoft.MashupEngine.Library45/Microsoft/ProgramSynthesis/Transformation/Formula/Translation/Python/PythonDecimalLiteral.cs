using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001859 RID: 6233
	internal class PythonDecimalLiteral : FormulaNumberLiteral
	{
		// Token: 0x0600CBD4 RID: 52180 RVA: 0x002B8993 File Offset: 0x002B6B93
		public PythonDecimalLiteral(decimal value)
			: base(Convert.ToDouble(value))
		{
		}

		// Token: 0x17002262 RID: 8802
		// (get) Token: 0x0600CBD5 RID: 52181 RVA: 0x002B89A1 File Offset: 0x002B6BA1
		public override Type Type
		{
			get
			{
				return typeof(decimal);
			}
		}

		// Token: 0x0600CBD6 RID: 52182 RVA: 0x002B89AD File Offset: 0x002B6BAD
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonDecimalLiteral(Convert.ToDecimal(base.Value));
		}

		// Token: 0x0600CBD7 RID: 52183 RVA: 0x002B89BF File Offset: 0x002B6BBF
		protected override string ToCodeString()
		{
			return "Decimal(str(" + base.ToCodeString() + "))";
		}
	}
}
