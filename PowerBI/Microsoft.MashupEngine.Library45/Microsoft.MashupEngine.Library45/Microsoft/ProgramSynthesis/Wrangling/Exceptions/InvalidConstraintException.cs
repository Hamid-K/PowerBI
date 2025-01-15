using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Exceptions
{
	// Token: 0x0200019C RID: 412
	public class InvalidConstraintException : LearningException
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x0001B3B7 File Offset: 0x000195B7
		public InvalidConstraintException()
			: base("A provided constraint is invalid.")
		{
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001B3AE File Offset: 0x000195AE
		public InvalidConstraintException(string message)
			: base(message)
		{
		}
	}
}
