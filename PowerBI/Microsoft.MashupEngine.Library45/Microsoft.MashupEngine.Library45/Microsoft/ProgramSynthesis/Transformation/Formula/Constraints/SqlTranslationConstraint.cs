using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019E3 RID: 6627
	public class SqlTranslationConstraint : TranslationConstraint, ISqlTranslationOptions, ITranslationOptions, IUniqueConstraint<SqlTranslationConstraint>
	{
		// Token: 0x17002422 RID: 9250
		// (get) Token: 0x0600D853 RID: 55379 RVA: 0x002DEB21 File Offset: 0x002DCD21
		// (set) Token: 0x0600D854 RID: 55380 RVA: 0x002DEB29 File Offset: 0x002DCD29
		public SqlOptimizations Optimizations { get; set; } = SqlOptimizations.All;

		// Token: 0x0600D855 RID: 55381 RVA: 0x002DEB32 File Offset: 0x002DCD32
		internal override string ToEqualString()
		{
			return base.ToEqualString() + string.Format(" {0}={1}", "Optimizations", this.Optimizations);
		}
	}
}
