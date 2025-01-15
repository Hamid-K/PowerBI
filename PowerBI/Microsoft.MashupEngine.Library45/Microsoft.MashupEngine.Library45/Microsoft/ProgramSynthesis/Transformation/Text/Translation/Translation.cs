using System;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation
{
	// Token: 0x02001D8F RID: 7567
	public class Translation : ITranslation<Program, string>
	{
		// Token: 0x0600FE4B RID: 65099 RVA: 0x00364E41 File Offset: 0x00363041
		internal Translation(Program program, TargetLanguage targetLanguage, string expression)
		{
			this.Program = program;
			this.Target = targetLanguage;
			this.TranslatedExpression = expression;
		}

		// Token: 0x17002A5C RID: 10844
		// (get) Token: 0x0600FE4C RID: 65100 RVA: 0x00364E5E File Offset: 0x0036305E
		public Program Program { get; }

		// Token: 0x17002A5D RID: 10845
		// (get) Token: 0x0600FE4D RID: 65101 RVA: 0x00364E66 File Offset: 0x00363066
		public TargetLanguage Target { get; }

		// Token: 0x17002A5E RID: 10846
		// (get) Token: 0x0600FE4E RID: 65102 RVA: 0x00364E6E File Offset: 0x0036306E
		public string TranslatedExpression { get; }

		// Token: 0x0600FE4F RID: 65103 RVA: 0x00364E76 File Offset: 0x00363076
		public override string ToString()
		{
			return this.TranslatedExpression;
		}
	}
}
