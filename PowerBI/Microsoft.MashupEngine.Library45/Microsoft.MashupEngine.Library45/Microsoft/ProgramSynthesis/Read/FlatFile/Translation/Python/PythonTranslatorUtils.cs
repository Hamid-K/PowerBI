using System;
using Microsoft.ProgramSynthesis.Translation.Python;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Translation.Python
{
	// Token: 0x020012D6 RID: 4822
	public static class PythonTranslatorUtils
	{
		// Token: 0x06009175 RID: 37237 RVA: 0x001EAB16 File Offset: 0x001E8D16
		public static PythonSnippet ToPython(this Program program, PythonTarget target, string encoding, string input, TranslationOptions options = null)
		{
			return new PythonTranslator(program, options).GenerateCode(target, encoding, input);
		}
	}
}
