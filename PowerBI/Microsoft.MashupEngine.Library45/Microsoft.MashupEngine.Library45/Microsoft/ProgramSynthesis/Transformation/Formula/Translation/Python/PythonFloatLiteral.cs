using System;
using System.Globalization;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001858 RID: 6232
	internal class PythonFloatLiteral : FormulaNumberLiteral
	{
		// Token: 0x0600CBCF RID: 52175 RVA: 0x002B8926 File Offset: 0x002B6B26
		public PythonFloatLiteral(double value)
			: base(value)
		{
			this.Type = typeof(double);
		}

		// Token: 0x17002261 RID: 8801
		// (get) Token: 0x0600CBD0 RID: 52176 RVA: 0x002B893F File Offset: 0x002B6B3F
		public override Type Type { get; }

		// Token: 0x0600CBD1 RID: 52177 RVA: 0x002B8947 File Offset: 0x002B6B47
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonFloatLiteral(base.Value);
		}

		// Token: 0x0600CBD2 RID: 52178 RVA: 0x002B8954 File Offset: 0x002B6B54
		protected override string ToCodeString()
		{
			return base.Value.ToString(PythonFloatLiteral._format, CultureInfo.InvariantCulture);
		}

		// Token: 0x04005004 RID: 20484
		private static readonly string _format = "0.0" + new string('#', 99);
	}
}
