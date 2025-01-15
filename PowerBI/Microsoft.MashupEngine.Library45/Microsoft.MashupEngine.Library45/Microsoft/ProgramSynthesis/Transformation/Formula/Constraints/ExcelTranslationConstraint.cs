using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019D0 RID: 6608
	public class ExcelTranslationConstraint : TranslationConstraint, IExcelTranslationOptions, ITranslationOptions, IUniqueConstraint<ExcelTranslationConstraint>
	{
		// Token: 0x170023DD RID: 9181
		// (get) Token: 0x0600D79C RID: 55196 RVA: 0x002DD38C File Offset: 0x002DB58C
		// (set) Token: 0x0600D79D RID: 55197 RVA: 0x002DD394 File Offset: 0x002DB594
		public bool EnableFindN { get; set; } = true;

		// Token: 0x170023DE RID: 9182
		// (get) Token: 0x0600D79E RID: 55198 RVA: 0x002DD39D File Offset: 0x002DB59D
		// (set) Token: 0x0600D79F RID: 55199 RVA: 0x002DD3A5 File Offset: 0x002DB5A5
		public bool EnableTextAfter { get; set; } = true;

		// Token: 0x170023DF RID: 9183
		// (get) Token: 0x0600D7A0 RID: 55200 RVA: 0x002DD3AE File Offset: 0x002DB5AE
		// (set) Token: 0x0600D7A1 RID: 55201 RVA: 0x002DD3B6 File Offset: 0x002DB5B6
		public bool EnableTextBefore { get; set; } = true;

		// Token: 0x170023E0 RID: 9184
		// (get) Token: 0x0600D7A2 RID: 55202 RVA: 0x002DD3BF File Offset: 0x002DB5BF
		// (set) Token: 0x0600D7A3 RID: 55203 RVA: 0x002DD3C7 File Offset: 0x002DB5C7
		public bool EnableTextSlice { get; set; } = true;

		// Token: 0x170023E1 RID: 9185
		// (get) Token: 0x0600D7A4 RID: 55204 RVA: 0x002DD3D0 File Offset: 0x002DB5D0
		// (set) Token: 0x0600D7A5 RID: 55205 RVA: 0x002DD3D8 File Offset: 0x002DB5D8
		public ExcelOptimizations Optimizations { get; set; } = ExcelOptimizations.Default;

		// Token: 0x170023E2 RID: 9186
		// (get) Token: 0x0600D7A6 RID: 55206 RVA: 0x002DD3E1 File Offset: 0x002DB5E1
		// (set) Token: 0x0600D7A7 RID: 55207 RVA: 0x002DD3E9 File Offset: 0x002DB5E9
		public string ReferenceRowExpression { get; set; }

		// Token: 0x170023E3 RID: 9187
		// (get) Token: 0x0600D7A8 RID: 55208 RVA: 0x002DD3F2 File Offset: 0x002DB5F2
		// (set) Token: 0x0600D7A9 RID: 55209 RVA: 0x002DD3FA File Offset: 0x002DB5FA
		public CultureInfo UserInterfaceCulture { get; set; }

		// Token: 0x0600D7AA RID: 55210 RVA: 0x002DD404 File Offset: 0x002DB604
		internal override string ToEqualString()
		{
			string[] array = new string[12];
			array[0] = base.ToEqualString();
			array[1] = " UserInterfaceCulture=";
			int num = 2;
			CultureInfo userInterfaceCulture = this.UserInterfaceCulture;
			array[num] = ((userInterfaceCulture != null) ? userInterfaceCulture.Name : null);
			array[3] = ";";
			array[4] = string.Format(" {0}={1};", "EnableFindN", this.EnableFindN);
			array[5] = string.Format(" {0}={1};", "EnableTextSlice", this.EnableTextSlice);
			array[6] = string.Format(" {0}={1};", "EnableTextBefore", this.EnableTextBefore);
			array[7] = string.Format(" {0}={1};", "EnableTextAfter", this.EnableTextAfter);
			array[8] = string.Format(" {0}={1};", "Optimizations", this.Optimizations);
			array[9] = " ReferenceRowExpression=";
			array[10] = this.ReferenceRowExpression;
			array[11] = ";";
			return string.Concat(array);
		}
	}
}
