using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Exceptions
{
	// Token: 0x020019C9 RID: 6601
	internal class NoExamplesProvidedException : Exception
	{
		// Token: 0x0600D77A RID: 55162 RVA: 0x002DD03E File Offset: 0x002DB23E
		public NoExamplesProvidedException()
			: base("One or more examples are required.")
		{
		}
	}
}
