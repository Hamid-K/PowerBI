using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001828 RID: 6184
	internal class SqlTryParse : SqlFunc
	{
		// Token: 0x0600CAB7 RID: 51895 RVA: 0x002B4966 File Offset: 0x002B2B66
		public SqlTryParse(FormulaExpression subject, FormulaExpression typeName, FormulaExpression locale)
			: base("TryParse", new FormulaExpression[] { subject, typeName, locale })
		{
			this.Subject = subject;
			this.TypeName = typeName;
			this.Locale = locale;
		}

		// Token: 0x17002231 RID: 8753
		// (get) Token: 0x0600CAB8 RID: 51896 RVA: 0x002B499A File Offset: 0x002B2B9A
		public FormulaExpression Locale { get; }

		// Token: 0x17002232 RID: 8754
		// (get) Token: 0x0600CAB9 RID: 51897 RVA: 0x002B49A2 File Offset: 0x002B2BA2
		public FormulaExpression Subject { get; }

		// Token: 0x17002233 RID: 8755
		// (get) Token: 0x0600CABA RID: 51898 RVA: 0x002B49AA File Offset: 0x002B2BAA
		public FormulaExpression TypeName { get; }

		// Token: 0x0600CABB RID: 51899 RVA: 0x002B49B2 File Offset: 0x002B2BB2
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new SqlTryParse(this.Subject.Accept<FormulaExpression>(visitor), this.TypeName.Accept<FormulaExpression>(visitor), this.Locale.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CABC RID: 51900 RVA: 0x002B49E0 File Offset: 0x002B2BE0
		protected override string ToCodeString()
		{
			FormulaExpression formulaExpression;
			FormulaExpression formulaExpression2;
			FormulaExpression formulaExpression3;
			base.Deconstruct(out formulaExpression, out formulaExpression2, out formulaExpression3);
			FormulaExpression formulaExpression4 = formulaExpression;
			object obj = formulaExpression2;
			FormulaExpression formulaExpression5 = formulaExpression3;
			string text = obj.ToString().Replace("\"", "");
			return string.Format("TRY_PARSE({0} AS {1} USING {2})", formulaExpression4, text, formulaExpression5);
		}
	}
}
