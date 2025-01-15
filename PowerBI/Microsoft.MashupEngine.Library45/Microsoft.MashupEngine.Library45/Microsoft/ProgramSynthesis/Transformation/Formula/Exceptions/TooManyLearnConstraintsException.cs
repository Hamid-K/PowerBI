using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Exceptions
{
	// Token: 0x020019CB RID: 6603
	internal class TooManyLearnConstraintsException : Exception
	{
		// Token: 0x0600D77C RID: 55164 RVA: 0x0001B3DA File Offset: 0x000195DA
		public TooManyLearnConstraintsException(string message)
			: base(message)
		{
		}
	}
}
