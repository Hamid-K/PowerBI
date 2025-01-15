using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001876 RID: 6262
	internal class PythonIndex : FormulaExpression, IFormulaTyped
	{
		// Token: 0x0600CC32 RID: 52274 RVA: 0x002B9063 File Offset: 0x002B7263
		public PythonIndex(FormulaExpression subject, FormulaExpression index, Type type)
		{
			base.Children = new FormulaExpression[] { subject, index };
			this.Subject = subject;
			this.Index = index;
			this.Type = type;
		}

		// Token: 0x1700227B RID: 8827
		// (get) Token: 0x0600CC33 RID: 52275 RVA: 0x002B9094 File Offset: 0x002B7294
		public FormulaExpression Index { get; }

		// Token: 0x1700227C RID: 8828
		// (get) Token: 0x0600CC34 RID: 52276 RVA: 0x002B909C File Offset: 0x002B729C
		public FormulaExpression Subject { get; }

		// Token: 0x1700227D RID: 8829
		// (get) Token: 0x0600CC35 RID: 52277 RVA: 0x002B90A4 File Offset: 0x002B72A4
		public Type Type { get; }

		// Token: 0x0600CC36 RID: 52278 RVA: 0x002B90AC File Offset: 0x002B72AC
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonIndex(this.Subject.Accept<FormulaExpression>(visitor), this.Index.Accept<FormulaExpression>(visitor), this.Type);
		}

		// Token: 0x0600CC37 RID: 52279 RVA: 0x002B90D1 File Offset: 0x002B72D1
		protected override string ToCodeString()
		{
			return string.Format("{0}[{1}]", this.Subject, this.Index);
		}
	}
}
