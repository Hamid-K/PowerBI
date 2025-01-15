using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007A3 RID: 1955
	public struct BindInfo
	{
		// Token: 0x04002944 RID: 10564
		public string collection;

		// Token: 0x04002945 RID: 10565
		public string package;

		// Token: 0x04002946 RID: 10566
		public byte[] token;

		// Token: 0x04002947 RID: 10567
		public string version;

		// Token: 0x04002948 RID: 10568
		public string defaultCollection;

		// Token: 0x04002949 RID: 10569
		public string title;

		// Token: 0x0400294A RID: 10570
		public IsolationLevel isoLevel;

		// Token: 0x0400294B RID: 10571
		public int numSections;

		// Token: 0x0400294C RID: 10572
		public int options;
	}
}
