using System;

namespace Microsoft.PowerBI.Packaging.Host
{
	// Token: 0x02000091 RID: 145
	public class HostContext
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		public HostContext(HostApplication hostApplication, IFeatureSwitches featureSwitches)
		{
			this.Application = hostApplication;
			this.FeatureSwitches = featureSwitches;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000A9BA File Offset: 0x00008BBA
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000A9C2 File Offset: 0x00008BC2
		public HostApplication Application { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000A9CB File Offset: 0x00008BCB
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000A9D3 File Offset: 0x00008BD3
		public IFeatureSwitches FeatureSwitches { get; private set; }
	}
}
