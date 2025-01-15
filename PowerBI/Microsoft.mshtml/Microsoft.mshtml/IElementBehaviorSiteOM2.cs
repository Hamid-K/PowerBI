using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB2 RID: 3506
	[Guid("3050F659-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IElementBehaviorSiteOM2 : IElementBehaviorSiteOM
	{
		// Token: 0x060174B4 RID: 95412
		[MethodImpl(4096, MethodCodeType = 3)]
		int RegisterEvent([MarshalAs(21)] [In] string pchEvent, [In] int lFlags);

		// Token: 0x060174B5 RID: 95413
		[MethodImpl(4096, MethodCodeType = 3)]
		int GetEventCookie([MarshalAs(21)] [In] string pchEvent);

		// Token: 0x060174B6 RID: 95414
		[MethodImpl(4096, MethodCodeType = 3)]
		void FireEvent([In] int lCookie, [MarshalAs(28)] [In] IHTMLEventObj pEventObject);

		// Token: 0x060174B7 RID: 95415
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLEventObj CreateEventObject();

		// Token: 0x060174B8 RID: 95416
		[MethodImpl(4096, MethodCodeType = 3)]
		void RegisterName([MarshalAs(21)] [In] string pchName);

		// Token: 0x060174B9 RID: 95417
		[MethodImpl(4096, MethodCodeType = 3)]
		void RegisterUrn([MarshalAs(21)] [In] string pchUrn);

		// Token: 0x060174BA RID: 95418
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElementDefaults GetDefaults();
	}
}
