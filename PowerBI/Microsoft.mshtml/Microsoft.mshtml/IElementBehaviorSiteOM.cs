using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000009 RID: 9
	[InterfaceType(1)]
	[Guid("3050F489-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehaviorSiteOM
	{
		// Token: 0x0600012D RID: 301
		[MethodImpl(4096, MethodCodeType = 3)]
		int RegisterEvent([MarshalAs(21)] [In] string pchEvent, [In] int lFlags);

		// Token: 0x0600012E RID: 302
		[MethodImpl(4096, MethodCodeType = 3)]
		int GetEventCookie([MarshalAs(21)] [In] string pchEvent);

		// Token: 0x0600012F RID: 303
		[MethodImpl(4096, MethodCodeType = 3)]
		void FireEvent([In] int lCookie, [MarshalAs(28)] [In] IHTMLEventObj pEventObject);

		// Token: 0x06000130 RID: 304
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLEventObj CreateEventObject();

		// Token: 0x06000131 RID: 305
		[MethodImpl(4096, MethodCodeType = 3)]
		void RegisterName([MarshalAs(21)] [In] string pchName);

		// Token: 0x06000132 RID: 306
		[MethodImpl(4096, MethodCodeType = 3)]
		void RegisterUrn([MarshalAs(21)] [In] string pchUrn);
	}
}
