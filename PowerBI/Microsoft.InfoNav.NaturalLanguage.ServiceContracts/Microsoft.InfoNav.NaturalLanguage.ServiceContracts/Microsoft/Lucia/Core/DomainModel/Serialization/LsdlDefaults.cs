using System;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001A0 RID: 416
	public static class LsdlDefaults
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x00011122 File Offset: 0x0000F322
		public static bool IsDefaultWeight(this double weight)
		{
			return weight == 1.0;
		}

		// Token: 0x04000730 RID: 1840
		public const double Weight = 1.0;
	}
}
