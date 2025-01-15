using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Intent
{
	// Token: 0x020019B6 RID: 6582
	internal static class ProgramIntentExtension
	{
		// Token: 0x0600D6E1 RID: 55009 RVA: 0x002DACF4 File Offset: 0x002D8EF4
		public static bool AnyFlags(this ProgramIntent subject, ProgramIntent mask)
		{
			return ((long)subject).AnyFlags((long)mask);
		}

		// Token: 0x0600D6E2 RID: 55010 RVA: 0x002DACFD File Offset: 0x002D8EFD
		public static bool HasFlags(this long subject, ProgramIntent mask)
		{
			return subject.HasFlags((long)mask);
		}
	}
}
