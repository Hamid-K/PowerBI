using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001968 RID: 6504
	internal class CSharpRawSegment : FormulaExpression
	{
		// Token: 0x0600D406 RID: 54278 RVA: 0x002D2429 File Offset: 0x002D0629
		public CSharpRawSegment(string code)
		{
			this.Code = code;
			base.Children = new FormulaExpression[0];
		}

		// Token: 0x1700234F RID: 9039
		// (get) Token: 0x0600D407 RID: 54279 RVA: 0x002D2444 File Offset: 0x002D0644
		public string Code { get; }

		// Token: 0x0600D408 RID: 54280 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D409 RID: 54281 RVA: 0x002D244C File Offset: 0x002D064C
		protected override string ToCodeString()
		{
			return this.Code;
		}
	}
}
