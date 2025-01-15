using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x02001784 RID: 6020
	public static class Int64Extension
	{
		// Token: 0x0600C784 RID: 51076 RVA: 0x00010FAF File Offset: 0x0000F1AF
		public static long AddFlags(this long subject, long mask)
		{
			return subject | mask;
		}

		// Token: 0x0600C785 RID: 51077 RVA: 0x002ADB0A File Offset: 0x002ABD0A
		public static bool HasFlags(this long subject, long mask)
		{
			return (subject & mask) == mask;
		}

		// Token: 0x0600C786 RID: 51078 RVA: 0x002ADB12 File Offset: 0x002ABD12
		public static bool AnyFlags(this long subject, long mask)
		{
			return (subject & mask) != 0L;
		}

		// Token: 0x0600C787 RID: 51079 RVA: 0x002ADB1B File Offset: 0x002ABD1B
		public static long RemoveFlags(this long subject, long mask)
		{
			return subject & ~mask;
		}
	}
}
