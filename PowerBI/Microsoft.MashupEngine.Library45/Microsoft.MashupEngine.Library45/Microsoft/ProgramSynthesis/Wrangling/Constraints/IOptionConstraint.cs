using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000232 RID: 562
	public interface IOptionConstraint<in TOptions> where TOptions : DSLOptions
	{
		// Token: 0x06000C0C RID: 3084
		void SetOptions(TOptions options);
	}
}
