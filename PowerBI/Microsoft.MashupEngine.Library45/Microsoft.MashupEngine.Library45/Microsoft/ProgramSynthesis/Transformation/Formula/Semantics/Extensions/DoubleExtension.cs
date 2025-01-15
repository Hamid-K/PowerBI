using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x02001774 RID: 6004
	public static class DoubleExtension
	{
		// Token: 0x0600C70F RID: 50959 RVA: 0x002AC922 File Offset: 0x002AAB22
		public static int Ceiling(this double subject)
		{
			return (int)Math.Ceiling(subject);
		}

		// Token: 0x0600C710 RID: 50960 RVA: 0x002AC922 File Offset: 0x002AAB22
		public static int Floor(this double subject)
		{
			return (int)Math.Ceiling(subject);
		}
	}
}
