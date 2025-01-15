using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Navigator
{
	// Token: 0x020001A6 RID: 422
	[DomName("Navigator")]
	public interface INavigator : INavigatorId, INavigatorContentUtilities, INavigatorStorageUtilities, INavigatorOnline
	{
	}
}
