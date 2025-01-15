using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001906 RID: 6406
	internal class PowerAutomateDateTimeLiteral : FormulaDateTimeLiteral
	{
		// Token: 0x0600D0EB RID: 53483 RVA: 0x002B8A01 File Offset: 0x002B6C01
		public PowerAutomateDateTimeLiteral(DateTime value)
			: base(value)
		{
		}

		// Token: 0x0600D0EC RID: 53484 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D0ED RID: 53485 RVA: 0x002C8DEC File Offset: 0x002C6FEC
		protected override string ToCodeString()
		{
			if (!(base.Value == base.Value.Date))
			{
				return string.Format("parseDateTime('{0:yyyy-MM-dd HH:mm:ss}')", base.Value);
			}
			return string.Format("parseDateTime('{0:yyyy-MM-dd}')", base.Value);
		}
	}
}
