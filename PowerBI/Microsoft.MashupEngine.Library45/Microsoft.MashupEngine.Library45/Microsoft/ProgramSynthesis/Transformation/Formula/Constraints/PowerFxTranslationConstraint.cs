using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019DB RID: 6619
	public class PowerFxTranslationConstraint : TranslationConstraint, IPowerFxTranslationOptions, ITranslationOptions, IUniqueConstraint<PowerFxTranslationConstraint>
	{
		// Token: 0x17002413 RID: 9235
		// (get) Token: 0x0600D828 RID: 55336 RVA: 0x002DE617 File Offset: 0x002DC817
		// (set) Token: 0x0600D829 RID: 55337 RVA: 0x002DE61F File Offset: 0x002DC81F
		public PowerFxOptimizations Optimizations { get; set; } = PowerFxOptimizations.All;

		// Token: 0x17002414 RID: 9236
		// (get) Token: 0x0600D82A RID: 55338 RVA: 0x002DE628 File Offset: 0x002DC828
		// (set) Token: 0x0600D82B RID: 55339 RVA: 0x002DE630 File Offset: 0x002DC830
		public CultureInfo UserInterfaceCulture { get; set; }

		// Token: 0x0600D82C RID: 55340 RVA: 0x002DE63C File Offset: 0x002DC83C
		internal override string ToEqualString()
		{
			string[] array = new string[5];
			array[0] = base.ToEqualString();
			array[1] = " UserInterfaceCulture=";
			int num = 2;
			CultureInfo userInterfaceCulture = this.UserInterfaceCulture;
			array[num] = ((userInterfaceCulture != null) ? userInterfaceCulture.Name : null);
			array[3] = ";";
			array[4] = string.Format(" {0}={1};", "Optimizations", this.Optimizations);
			return string.Concat(array);
		}
	}
}
