using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Transformation.Table.Constraints;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.PowerQuery
{
	// Token: 0x02001B34 RID: 6964
	public class PowerQueryTranslation : TransformationTableTranslation
	{
		// Token: 0x0600E4C9 RID: 58569 RVA: 0x00307CE1 File Offset: 0x00305EE1
		internal PowerQueryTranslation(Program program, PowerQueryLet translatedExpression, PowerQueryTranslationConstraint constraint, Metadata metadata)
			: base(program, translatedExpression, constraint, metadata)
		{
		}

		// Token: 0x17002622 RID: 9762
		// (get) Token: 0x0600E4CA RID: 58570 RVA: 0x0001B224 File Offset: 0x00019424
		public override TargetLanguage Target
		{
			get
			{
				return TargetLanguage.PowerQueryM;
			}
		}

		// Token: 0x17002623 RID: 9763
		// (get) Token: 0x0600E4CB RID: 58571 RVA: 0x00307CEE File Offset: 0x00305EEE
		public new PowerQueryLet TranslatedExpression
		{
			get
			{
				return (PowerQueryLet)base.TranslatedExpression;
			}
		}
	}
}
