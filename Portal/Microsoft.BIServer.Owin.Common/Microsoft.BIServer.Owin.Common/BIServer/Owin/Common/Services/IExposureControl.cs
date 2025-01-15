using System;

namespace Microsoft.BIServer.Owin.Common.Services
{
	// Token: 0x02000012 RID: 18
	public interface IExposureControl
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000046 RID: 70
		bool PreviewEnabled { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000047 RID: 71
		bool DogfoodEnabled { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000048 RID: 72
		bool DevelopmentEnabled { get; }
	}
}
