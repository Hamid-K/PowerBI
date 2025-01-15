using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001969 RID: 6505
	internal class CSharpRawLine : FormulaExpression
	{
		// Token: 0x0600D40A RID: 54282 RVA: 0x002D2454 File Offset: 0x002D0654
		public CSharpRawLine(string code)
		{
			this.Code = code;
			base.Children = new FormulaExpression[0];
		}

		// Token: 0x17002350 RID: 9040
		// (get) Token: 0x0600D40B RID: 54283 RVA: 0x002D246F File Offset: 0x002D066F
		public string Code { get; }

		// Token: 0x0600D40C RID: 54284 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D40D RID: 54285 RVA: 0x002D2477 File Offset: 0x002D0677
		protected override string ToCodeString()
		{
			return this.Code;
		}
	}
}
