using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200194F RID: 6479
	internal class CSharpDateTimeLiteral : FormulaDateTimeLiteral
	{
		// Token: 0x0600D3AF RID: 54191 RVA: 0x002B8A01 File Offset: 0x002B6C01
		public CSharpDateTimeLiteral(DateTime value)
			: base(value)
		{
		}

		// Token: 0x0600D3B0 RID: 54192 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D3B1 RID: 54193 RVA: 0x002D1CAC File Offset: 0x002CFEAC
		protected override string ToCodeString()
		{
			if (base.Value == base.Value.Date)
			{
				return string.Format("DateTime.Parse(\"{0:yyyy-MM-dd}\")", base.Value);
			}
			DateTime value = base.Value;
			if (value.Second == 0 && value.Millisecond == 0)
			{
				return string.Format("DateTime.Parse(\"{0:yyyy-MM-ddTHH:mm}\")", base.Value);
			}
			if (base.Value.Millisecond != 0)
			{
				return string.Format("DateTime.Parse(\"{0:yyyy-MM-ddTHH:mm:ss.fff}\")", base.Value);
			}
			return string.Format("DateTime.Parse(\"{0:s}\")", base.Value);
		}
	}
}
