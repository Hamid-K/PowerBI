using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Exceptions
{
	// Token: 0x0200019D RID: 413
	public class InvalidInputException : LearningException
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0001B3C4 File Offset: 0x000195C4
		public InvalidInputException()
			: base("A provided input is invalid.")
		{
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001B3AE File Offset: 0x000195AE
		public InvalidInputException(string message)
			: base(message)
		{
		}
	}
}
