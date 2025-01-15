using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x020001A5 RID: 421
	[DomName("WindowTimers")]
	[DomNoInterfaceObject]
	public interface IWindowTimers
	{
		// Token: 0x06000F02 RID: 3842
		[DomName("setTimeout")]
		int SetTimeout(Action<IWindow> handler, int timeout = 0);

		// Token: 0x06000F03 RID: 3843
		[DomName("clearTimeout")]
		void ClearTimeout(int handle = 0);

		// Token: 0x06000F04 RID: 3844
		[DomName("setInterval")]
		int SetInterval(Action<IWindow> handler, int timeout = 0);

		// Token: 0x06000F05 RID: 3845
		[DomName("clearInterval")]
		void ClearInterval(int handle = 0);
	}
}
