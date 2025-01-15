using System;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Translation.Python
{
	// Token: 0x02000B9C RID: 2972
	public static class JsonExtractionToPythonExtensions
	{
		// Token: 0x06004B88 RID: 19336 RVA: 0x000EEA14 File Offset: 0x000ECC14
		internal static JsonExtractionPythonGeneratedFunction ToJsonExtractionPythonGeneratedFunction(this SSAGeneratedFunction fun, Program p, string headerModuleName)
		{
			string text = SchemaTranslator.Translate(p, headerModuleName);
			return new JsonExtractionPythonGeneratedFunction(fun.Parameters, fun.ReturnType, fun.SSASequence, p, text);
		}
	}
}
