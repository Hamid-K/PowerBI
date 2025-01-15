using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Navigator
{
	// Token: 0x020001A9 RID: 425
	[DomName("NavigatorOnLine")]
	[DomNoInterfaceObject]
	public interface INavigatorOnline
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000F10 RID: 3856
		[DomName("onLine")]
		bool IsOnline { get; }
	}
}
