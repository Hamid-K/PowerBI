using System;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018CF RID: 6351
	public class PowerQueryTranslation : FormulaTranslation
	{
		// Token: 0x0600CF6B RID: 53099 RVA: 0x002C4004 File Offset: 0x002C2204
		internal PowerQueryTranslation(Program program, FormulaExpression translatedExpression, TranslationMeta meta)
			: base(program, translatedExpression, TargetLanguage.PowerQueryM, meta)
		{
		}
	}
}
