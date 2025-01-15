using System;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018FE RID: 6398
	public class PowerFxTranslation : FormulaTranslation
	{
		// Token: 0x0600D0D6 RID: 53462 RVA: 0x002C8C42 File Offset: 0x002C6E42
		internal PowerFxTranslation(Program program, FormulaExpression translatedExpression, TranslationMeta metadata)
			: base(program, translatedExpression, TargetLanguage.PowerApps, metadata)
		{
		}
	}
}
