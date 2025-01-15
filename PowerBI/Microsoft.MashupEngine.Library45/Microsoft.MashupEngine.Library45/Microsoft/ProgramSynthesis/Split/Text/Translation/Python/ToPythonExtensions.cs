using System;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;

namespace Microsoft.ProgramSynthesis.Split.Text.Translation.Python
{
	// Token: 0x020013FA RID: 5114
	public static class ToPythonExtensions
	{
		// Token: 0x06009E0E RID: 40462 RVA: 0x00218614 File Offset: 0x00216814
		public static string ToPython(this SplitProgram p, OptimizeFor optimization, string programName = "split_text_program", string moduleName = "split_text", string headerModuleName = "prose_semantics")
		{
			PythonTranslator pythonTranslator = new PythonTranslator();
			PythonHeaderModule pythonHeaderModule = pythonTranslator.GenerateHeaderModule(p, headerModuleName);
			PythonModule pythonModule = pythonTranslator.CreateModule(moduleName, headerModuleName, "stext");
			GeneratedFunction generatedFunction = pythonTranslator.Translate(p, pythonModule, optimization);
			pythonModule.Bind(programName, generatedFunction);
			return pythonHeaderModule.GenerateCode(optimization) + pythonModule.GenerateCode(optimization);
		}
	}
}
