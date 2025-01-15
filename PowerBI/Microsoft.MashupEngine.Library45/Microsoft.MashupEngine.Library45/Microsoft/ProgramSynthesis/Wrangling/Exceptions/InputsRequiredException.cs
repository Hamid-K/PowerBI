using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Exceptions
{
	// Token: 0x0200019B RID: 411
	public class InputsRequiredException : LearningException
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x0001B3A1 File Offset: 0x000195A1
		public InputsRequiredException()
			: base("The constraints provided to this learning call cannot be used to learn unless inputs are also provided.")
		{
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001B3AE File Offset: 0x000195AE
		public InputsRequiredException(string message)
			: base(message)
		{
		}
	}
}
