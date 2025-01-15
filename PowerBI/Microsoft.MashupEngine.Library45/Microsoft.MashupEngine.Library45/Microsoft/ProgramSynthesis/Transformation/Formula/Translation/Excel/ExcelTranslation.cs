using System;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001937 RID: 6455
	public class ExcelTranslation : FormulaTranslation
	{
		// Token: 0x0600D33C RID: 54076 RVA: 0x002D0EA5 File Offset: 0x002CF0A5
		internal ExcelTranslation(Program program, FormulaExpression translatedExpression, TranslationMeta meta)
			: base(program, translatedExpression, TargetLanguage.Excel, meta)
		{
		}
	}
}
