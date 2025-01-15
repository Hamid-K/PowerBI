using System;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Split.Translation.PowerQuery
{
	// Token: 0x02001410 RID: 5136
	public class PowerQueryTranslation : SplitTranslation
	{
		// Token: 0x06009E85 RID: 40581 RVA: 0x00219FEE File Offset: 0x002181EE
		internal PowerQueryTranslation(SplitProgram program, PowerQueryLet powerQueryProgram, PowerQueryTranslationConstraint translationConstraint, Metadata metadata)
			: base(program, powerQueryProgram, translationConstraint, metadata)
		{
		}

		// Token: 0x17001ADF RID: 6879
		// (get) Token: 0x06009E86 RID: 40582 RVA: 0x0001B224 File Offset: 0x00019424
		public override TargetLanguage Target
		{
			get
			{
				return TargetLanguage.PowerQueryM;
			}
		}

		// Token: 0x17001AE0 RID: 6880
		// (get) Token: 0x06009E87 RID: 40583 RVA: 0x00219FFB File Offset: 0x002181FB
		public PowerQueryLet PowerQueryProgram
		{
			get
			{
				return (PowerQueryLet)base.TranslatedExpression;
			}
		}
	}
}
