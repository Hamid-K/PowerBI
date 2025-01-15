using System;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000732 RID: 1842
	public class CommonContextControlInformation
	{
		// Token: 0x040022FA RID: 8954
		public int version;

		// Token: 0x040022FB RID: 8955
		public bool connectionIsViable;

		// Token: 0x040022FC RID: 8956
		public int contextControlInformationType;

		// Token: 0x040022FD RID: 8957
		public Guid? persistentCorrelator;

		// Token: 0x040022FE RID: 8958
		public string resolverInfo;
	}
}
