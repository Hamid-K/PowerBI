using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Transformation.Table.Constraints;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.Pandas
{
	// Token: 0x02001B3D RID: 6973
	public class PandasTranslation : TransformationTableTranslation
	{
		// Token: 0x0600E4EE RID: 58606 RVA: 0x00307CE1 File Offset: 0x00305EE1
		public PandasTranslation(Program program, FormulaExpression expression, PandasTranslationConstraint translationConstraint, Metadata metadata)
			: base(program, expression, translationConstraint, metadata)
		{
		}

		// Token: 0x17002624 RID: 9764
		// (get) Token: 0x0600E4EF RID: 58607 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override TargetLanguage Target
		{
			get
			{
				return TargetLanguage.Pandas;
			}
		}
	}
}
