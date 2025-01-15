using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Translation.Python
{
	// Token: 0x02000B97 RID: 2967
	public class JsonExtractionPythonGeneratedFunction : PythonGeneratedFunction
	{
		// Token: 0x06004B68 RID: 19304 RVA: 0x000EDA8B File Offset: 0x000EBC8B
		public JsonExtractionPythonGeneratedFunction(IEnumerable<Record<string, Type>> parameters, Type returnType, IEnumerable<SSAStep> ssaSequence, Program program, string generatedSchemaCode)
			: base(parameters, returnType, ssaSequence)
		{
			this.GenerateSchemaCode = generatedSchemaCode;
			this.Program = program;
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06004B69 RID: 19305 RVA: 0x000EDAA6 File Offset: 0x000EBCA6
		public Program Program { get; }

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06004B6A RID: 19306 RVA: 0x000EDAAE File Offset: 0x000EBCAE
		public string GenerateSchemaCode { get; }
	}
}
