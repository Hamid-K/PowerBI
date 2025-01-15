using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Navigator
{
	// Token: 0x020001AA RID: 426
	[DomName("NavigatorStorageUtils")]
	[DomNoInterfaceObject]
	public interface INavigatorStorageUtilities
	{
		// Token: 0x06000F11 RID: 3857
		[DomName("yieldForStorageUpdates")]
		void WaitForStorageUpdates();
	}
}
