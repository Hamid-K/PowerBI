using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001919 RID: 6425
	internal class ExcelFormat : ExcelStringLiteral
	{
		// Token: 0x0600D1DD RID: 53725 RVA: 0x002CC39C File Offset: 0x002CA59C
		public ExcelFormat(string format)
			: this(format, null)
		{
		}

		// Token: 0x0600D1DE RID: 53726 RVA: 0x002CC3A8 File Offset: 0x002CA5A8
		public ExcelFormat(string format, string locale)
		{
			this.Format = format;
			this.Locale = locale;
			base.Value = ((this.Locale == null) ? this.Format : ("[$-" + this.Locale + "]" + this.Format));
		}

		// Token: 0x170022F5 RID: 8949
		// (get) Token: 0x0600D1DF RID: 53727 RVA: 0x002CC3FA File Offset: 0x002CA5FA
		public string Format { get; }

		// Token: 0x170022F6 RID: 8950
		// (get) Token: 0x0600D1E0 RID: 53728 RVA: 0x002CC402 File Offset: 0x002CA602
		public string Locale { get; }

		// Token: 0x0600D1E1 RID: 53729 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}
	}
}
