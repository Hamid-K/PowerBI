using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019DD RID: 6621
	public class PowerQueryTranslationConstraint : TranslationConstraint, IPowerQueryTranslationOptions, ITranslationOptions, IUniqueConstraint<PowerQueryTranslationConstraint>
	{
		// Token: 0x17002415 RID: 9237
		// (get) Token: 0x0600D82F RID: 55343 RVA: 0x002DE6DE File Offset: 0x002DC8DE
		// (set) Token: 0x0600D830 RID: 55344 RVA: 0x002DE6E6 File Offset: 0x002DC8E6
		public PowerQueryOptimizations Optimizations { get; set; } = PowerQueryOptimizations.All;

		// Token: 0x17002416 RID: 9238
		// (get) Token: 0x0600D831 RID: 55345 RVA: 0x002DE6EF File Offset: 0x002DC8EF
		// (set) Token: 0x0600D832 RID: 55346 RVA: 0x002DE6F7 File Offset: 0x002DC8F7
		public CultureInfo UserInterfaceCulture { get; set; }

		// Token: 0x17002417 RID: 9239
		// (get) Token: 0x0600D833 RID: 55347 RVA: 0x002DE700 File Offset: 0x002DC900
		// (set) Token: 0x0600D834 RID: 55348 RVA: 0x002DE708 File Offset: 0x002DC908
		public OutputType OutputType { get; set; }

		// Token: 0x0600D835 RID: 55349 RVA: 0x002DE714 File Offset: 0x002DC914
		internal override string ToEqualString()
		{
			string[] array = new string[6];
			array[0] = base.ToEqualString();
			array[1] = " UserInterfaceCulture=";
			int num = 2;
			CultureInfo userInterfaceCulture = this.UserInterfaceCulture;
			array[num] = ((userInterfaceCulture != null) ? userInterfaceCulture.Name : null);
			array[3] = ";";
			array[4] = string.Format(" {0}={1};", "Optimizations", this.Optimizations);
			array[5] = string.Format(" {0}={1};", "OutputType", this.OutputType);
			return string.Concat(array);
		}
	}
}
