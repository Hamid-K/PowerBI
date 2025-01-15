using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Explanations;
using Microsoft.ProgramSynthesis.Transformation.Formula.Explanations.Default;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019E4 RID: 6628
	public abstract class TranslationConstraint : FormulaConstraint, ITranslationOptions
	{
		// Token: 0x17002423 RID: 9251
		// (get) Token: 0x0600D857 RID: 55383 RVA: 0x002DEB6C File Offset: 0x002DCD6C
		// (set) Token: 0x0600D858 RID: 55384 RVA: 0x002DEB74 File Offset: 0x002DCD74
		public bool EnableSubstringTokenLogging { get; set; }

		// Token: 0x17002424 RID: 9252
		// (get) Token: 0x0600D859 RID: 55385 RVA: 0x002DEB7D File Offset: 0x002DCD7D
		// (set) Token: 0x0600D85A RID: 55386 RVA: 0x002DEB85 File Offset: 0x002DCD85
		public IExplanationTemplateProvider ExplanationTemplateProvider { get; set; } = new DefaultExplanationTemplateProvider();

		// Token: 0x17002425 RID: 9253
		// (get) Token: 0x0600D85B RID: 55387 RVA: 0x002DEB8E File Offset: 0x002DCD8E
		// (set) Token: 0x0600D85C RID: 55388 RVA: 0x002DEB96 File Offset: 0x002DCD96
		public bool SuppressConstantOutput { get; set; } = true;

		// Token: 0x17002426 RID: 9254
		// (get) Token: 0x0600D85D RID: 55389 RVA: 0x002DEB9F File Offset: 0x002DCD9F
		// (set) Token: 0x0600D85E RID: 55390 RVA: 0x002DEBA7 File Offset: 0x002DCDA7
		public bool SuppressInconsistentOutput { get; set; }

		// Token: 0x17002427 RID: 9255
		// (get) Token: 0x0600D85F RID: 55391 RVA: 0x002DEBB0 File Offset: 0x002DCDB0
		// (set) Token: 0x0600D860 RID: 55392 RVA: 0x002DEBB8 File Offset: 0x002DCDB8
		public SuppressionBehavior SuppressLowAcceptance { get; set; }

		// Token: 0x17002428 RID: 9256
		// (get) Token: 0x0600D861 RID: 55393 RVA: 0x002DEBC1 File Offset: 0x002DCDC1
		// (set) Token: 0x0600D862 RID: 55394 RVA: 0x002DEBC9 File Offset: 0x002DCDC9
		public SuppressionBehavior SuppressLowPrecision { get; set; }

		// Token: 0x17002429 RID: 9257
		// (get) Token: 0x0600D863 RID: 55395 RVA: 0x002DEBD2 File Offset: 0x002DCDD2
		// (set) Token: 0x0600D864 RID: 55396 RVA: 0x002DEBDA File Offset: 0x002DCDDA
		public bool SuppressWholeColumnOutput { get; set; } = true;

		// Token: 0x0600D865 RID: 55397 RVA: 0x002DD65B File Offset: 0x002DB85B
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return ((other != null) ? other.GetType() : null) == base.GetType();
		}

		// Token: 0x0600D866 RID: 55398 RVA: 0x002DEBE4 File Offset: 0x002DCDE4
		internal override string ToEqualString()
		{
			return string.Concat(new string[]
			{
				string.Format("{0}={1};", "SuppressConstantOutput", this.SuppressConstantOutput),
				string.Format("{0}={1};", "SuppressWholeColumnOutput", this.SuppressWholeColumnOutput),
				string.Format("{0}={1};", "SuppressInconsistentOutput", this.SuppressInconsistentOutput),
				string.Format("{0}={1};", "SuppressLowAcceptance", this.SuppressLowAcceptance),
				string.Format("{0}={1};", "SuppressLowPrecision", this.SuppressLowPrecision)
			});
		}
	}
}
