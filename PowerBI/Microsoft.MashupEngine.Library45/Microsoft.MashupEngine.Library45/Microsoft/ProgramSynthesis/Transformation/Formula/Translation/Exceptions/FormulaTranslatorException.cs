using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Exceptions
{
	// Token: 0x02001917 RID: 6423
	public class FormulaTranslatorException : Exception
	{
		// Token: 0x0600D1D7 RID: 53719 RVA: 0x0001B3DA File Offset: 0x000195DA
		public FormulaTranslatorException(string message)
			: base(message)
		{
		}

		// Token: 0x0600D1D8 RID: 53720 RVA: 0x002CC35A File Offset: 0x002CA55A
		public FormulaTranslatorException(string message, Exception innerEx)
			: base(message, innerEx)
		{
		}
	}
}
