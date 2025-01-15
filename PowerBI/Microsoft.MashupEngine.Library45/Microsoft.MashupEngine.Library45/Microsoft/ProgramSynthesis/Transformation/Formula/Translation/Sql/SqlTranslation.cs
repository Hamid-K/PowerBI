using System;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x0200182E RID: 6190
	public class SqlTranslation : FormulaTranslation
	{
		// Token: 0x0600CB1B RID: 51995 RVA: 0x002B62D0 File Offset: 0x002B44D0
		internal SqlTranslation(Program program, FormulaExpression translatedExpression, TranslationMeta meta)
			: base(program, translatedExpression, TargetLanguage.Sql, meta)
		{
		}
	}
}
