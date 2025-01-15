using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200185B RID: 6235
	internal class PythonDateTimeLiteral : FormulaDateTimeLiteral
	{
		// Token: 0x0600CBDB RID: 52187 RVA: 0x002B8A01 File Offset: 0x002B6C01
		public PythonDateTimeLiteral(DateTime value)
			: base(value)
		{
		}

		// Token: 0x0600CBDC RID: 52188 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CBDD RID: 52189 RVA: 0x002B8A0C File Offset: 0x002B6C0C
		protected override string ToCodeString()
		{
			if (!(base.Value == base.Value.Date))
			{
				return string.Format("datetime.strptime(\"{0:s}\", \"%Y-%m-%dT%H:%M:%S\")", base.Value);
			}
			return string.Format("datetime.strptime(\"{0:yyyy-MM-dd}\", \"%Y-%m-%d\")", base.Value);
		}
	}
}
