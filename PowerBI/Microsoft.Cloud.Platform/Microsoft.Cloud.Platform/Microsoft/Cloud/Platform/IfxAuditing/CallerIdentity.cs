using System;

namespace Microsoft.Cloud.Platform.IfxAuditing
{
	// Token: 0x0200032A RID: 810
	public class CallerIdentity
	{
		// Token: 0x060017EB RID: 6123 RVA: 0x00058300 File Offset: 0x00056500
		public CallerIdentity(CallerIdentityType callerIdentityType, string callerIdentityValue)
		{
			this.CallerIdentityType = callerIdentityType;
			this.CallerIdentityValue = callerIdentityValue;
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x00058316 File Offset: 0x00056516
		public CallerIdentityType CallerIdentityType { get; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0005831E File Offset: 0x0005651E
		public string CallerIdentityValue { get; }
	}
}
