using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Navigator
{
	// Token: 0x020001A8 RID: 424
	[DomName("NavigatorID")]
	[DomNoInterfaceObject]
	public interface INavigatorId
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000F0C RID: 3852
		[DomName("appName")]
		string Name { get; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000F0D RID: 3853
		[DomName("appVersion")]
		string Version { get; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000F0E RID: 3854
		[DomName("platform")]
		string Platform { get; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000F0F RID: 3855
		[DomName("userAgent")]
		string UserAgent { get; }
	}
}
