using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200187B RID: 6267
	internal class PythonReturn : FormulaExpression, IFormulaUnaryOperator, IFormulaOperator
	{
		// Token: 0x0600CC46 RID: 52294 RVA: 0x002B919E File Offset: 0x002B739E
		public PythonReturn(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { this.Subject };
		}

		// Token: 0x17002280 RID: 8832
		// (get) Token: 0x0600CC47 RID: 52295 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002281 RID: 8833
		// (get) Token: 0x0600CC48 RID: 52296 RVA: 0x002B91C2 File Offset: 0x002B73C2
		public FormulaExpression Subject { get; }

		// Token: 0x0600CC49 RID: 52297 RVA: 0x002B91CA File Offset: 0x002B73CA
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonReturn(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CC4A RID: 52298 RVA: 0x002B91DD File Offset: 0x002B73DD
		protected override string ToCodeString()
		{
			return string.Format("return {0}", this.Subject);
		}
	}
}
