using System;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002E2 RID: 738
	public interface ITranslation<out TProgram, out TExpression>
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000FED RID: 4077
		TProgram Program { get; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000FEE RID: 4078
		TargetLanguage Target { get; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000FEF RID: 4079
		TExpression TranslatedExpression { get; }

		// Token: 0x06000FF0 RID: 4080
		string ToString();
	}
}
