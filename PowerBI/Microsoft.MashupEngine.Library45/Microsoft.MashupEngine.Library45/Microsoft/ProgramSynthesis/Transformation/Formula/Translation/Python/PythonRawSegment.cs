using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001877 RID: 6263
	internal class PythonRawSegment : FormulaExpression
	{
		// Token: 0x0600CC38 RID: 52280 RVA: 0x002B90E9 File Offset: 0x002B72E9
		public PythonRawSegment(string code)
		{
			this.Code = code;
			base.Children = new FormulaExpression[0];
		}

		// Token: 0x1700227E RID: 8830
		// (get) Token: 0x0600CC39 RID: 52281 RVA: 0x002B9104 File Offset: 0x002B7304
		public string Code { get; }

		// Token: 0x0600CC3A RID: 52282 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CC3B RID: 52283 RVA: 0x002B910C File Offset: 0x002B730C
		protected override string ToCodeString()
		{
			return this.Code;
		}
	}
}
