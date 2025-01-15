using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019DF RID: 6623
	public class PySparkTranslationConstraint : PandasTranslationConstraint, IPySparkTranslationOptions, IPandasTranslationOptions, IPythonTranslationOptions, ITranslationOptions, IRenderingOptions, IUniqueConstraint<PySparkTranslationConstraint>
	{
		// Token: 0x0600D838 RID: 55352 RVA: 0x002DE7A7 File Offset: 0x002DC9A7
		public PySparkTranslationConstraint()
		{
			base.DerivedColumnIndex = new int?(1);
		}

		// Token: 0x17002418 RID: 9240
		// (get) Token: 0x0600D839 RID: 55353 RVA: 0x002DE7CD File Offset: 0x002DC9CD
		// (set) Token: 0x0600D83A RID: 55354 RVA: 0x002DE7D5 File Offset: 0x002DC9D5
		public bool ImportPySpark { get; set; }

		// Token: 0x17002419 RID: 9241
		// (get) Token: 0x0600D83B RID: 55355 RVA: 0x002DE7DE File Offset: 0x002DC9DE
		// (set) Token: 0x0600D83C RID: 55356 RVA: 0x002DE7E6 File Offset: 0x002DC9E6
		public PySparkOptimizations PySparkOptimizations { get; set; } = PySparkOptimizations.All;

		// Token: 0x1700241A RID: 9242
		// (get) Token: 0x0600D83D RID: 55357 RVA: 0x002DE7EF File Offset: 0x002DC9EF
		// (set) Token: 0x0600D83E RID: 55358 RVA: 0x002DE7F7 File Offset: 0x002DC9F7
		public bool UseSqlDataFrame { get; set; } = true;

		// Token: 0x0600D83F RID: 55359 RVA: 0x002DE800 File Offset: 0x002DCA00
		internal override string ToEqualString()
		{
			return base.ToEqualString() + string.Format(" {0}={1};", "UseSqlDataFrame", this.UseSqlDataFrame) + string.Format(" {0}={1};", "ImportPySpark", this.ImportPySpark) + string.Format(" {0}={1};", "PySparkOptimizations", this.PySparkOptimizations);
		}
	}
}
