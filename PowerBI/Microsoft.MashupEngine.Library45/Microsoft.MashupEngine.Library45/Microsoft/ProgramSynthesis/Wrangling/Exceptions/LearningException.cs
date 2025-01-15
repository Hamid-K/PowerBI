using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Exceptions
{
	// Token: 0x0200019E RID: 414
	public abstract class LearningException : ProgramSynthesisException
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x0001B3D1 File Offset: 0x000195D1
		protected LearningException(string message)
			: base(message)
		{
		}
	}
}
