using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Exceptions
{
	// Token: 0x020001A0 RID: 416
	public abstract class RunningException : ProgramSynthesisException
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x0001B3D1 File Offset: 0x000195D1
		protected RunningException(string message)
			: base(message)
		{
		}
	}
}
