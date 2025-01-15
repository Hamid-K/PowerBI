using System;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001914 RID: 6420
	public class PowerAutomateTranslation : FormulaTranslation
	{
		// Token: 0x0600D1D4 RID: 53716 RVA: 0x002CC344 File Offset: 0x002CA544
		internal PowerAutomateTranslation(Program program, FormulaExpression translatedExpression, TranslationMeta meta)
			: base(program, translatedExpression, TargetLanguage.PowerAutomate, meta)
		{
		}
	}
}
