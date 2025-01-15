using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017E4 RID: 6116
	internal interface IFormulaBlock
	{
		// Token: 0x0600C975 RID: 51573
		void AppendCodeString(CodeBuilder codeBuilder);

		// Token: 0x0600C976 RID: 51574
		string ToString(uint indentLevel, uint indentSize);
	}
}
