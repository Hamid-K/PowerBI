using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019D9 RID: 6617
	public class PowerAutomateTranslationConstraint : TranslationConstraint, IPowerAutomateTranslationOptions, ITranslationOptions, IUniqueConstraint<PowerAutomateTranslationConstraint>
	{
		// Token: 0x17002411 RID: 9233
		// (get) Token: 0x0600D821 RID: 55329 RVA: 0x002DE5AB File Offset: 0x002DC7AB
		// (set) Token: 0x0600D822 RID: 55330 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		public PowerAutomateOptimizations Optimizations { get; set; } = PowerAutomateOptimizations.All;

		// Token: 0x17002412 RID: 9234
		// (get) Token: 0x0600D823 RID: 55331 RVA: 0x002DE5BC File Offset: 0x002DC7BC
		// (set) Token: 0x0600D824 RID: 55332 RVA: 0x002DE5C4 File Offset: 0x002DC7C4
		public CultureInfo UserInterfaceCulture { get; set; } = new CultureInfo("en-US");

		// Token: 0x0600D825 RID: 55333 RVA: 0x002DE5CD File Offset: 0x002DC7CD
		internal override string ToEqualString()
		{
			return base.ToEqualString() + string.Format(" {0}={1};", "Optimizations", this.Optimizations);
		}
	}
}
