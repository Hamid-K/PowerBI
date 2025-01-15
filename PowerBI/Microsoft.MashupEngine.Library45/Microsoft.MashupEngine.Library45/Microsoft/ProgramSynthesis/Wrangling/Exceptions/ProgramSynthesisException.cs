using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Exceptions
{
	// Token: 0x0200019F RID: 415
	public abstract class ProgramSynthesisException : Exception
	{
		// Token: 0x06000918 RID: 2328 RVA: 0x0001B3DA File Offset: 0x000195DA
		protected ProgramSynthesisException(string message)
			: base(message)
		{
		}
	}
}
