using System;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.Translation.Python
{
	// Token: 0x02000B0A RID: 2826
	internal static class RichDataTypePythonCodeGenerator
	{
		// Token: 0x060046A2 RID: 18082 RVA: 0x000DCA74 File Offset: 0x000DAC74
		public static GeneratedCode GenerateParsingCode(this IRichDataType dataType, IPythonColumnInfo columnInfo, CodeGenerationMode codeGenerationMode)
		{
			return dataType.GenerateInlineParsingCode(columnInfo, codeGenerationMode).OrElseCompute(() => dataType.GenerateOutOfLineParsingCode(columnInfo, codeGenerationMode));
		}
	}
}
