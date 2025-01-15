using System;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;

namespace Microsoft.ProgramSynthesis.Matching.Text.Translation.Python
{
	// Token: 0x0200123E RID: 4670
	public static class ToPythonExtensions
	{
		// Token: 0x06008CA9 RID: 36009 RVA: 0x001D8F38 File Offset: 0x001D7138
		public static string ToPython(this Program p, string programName = "matching_text_program", string moduleName = "matching_text", string headerModuleName = "prose_semantics", string headerModuleNameAlias = null)
		{
			MatchingTextPythonModule.TranslationOptions translationOptions = new MatchingTextPythonModule.TranslationOptions(PythonTarget.Auto);
			return new MatchingTextPythonModule(p, moduleName, headerModuleName, headerModuleNameAlias, translationOptions).GenerateUnisolatedCode(OptimizeFor.Nothing);
		}
	}
}
