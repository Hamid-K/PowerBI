using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CE8 RID: 3304
	[Guid("3050F6DF-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHTMLPainterEventInfo
	{
		// Token: 0x06016360 RID: 90976
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetEventInfoFlags(out int plEventInfoFlags);

		// Token: 0x06016361 RID: 90977
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetEventTarget([MarshalAs(28)] [In] ref IHTMLElement ppElement);

		// Token: 0x06016362 RID: 90978
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCursor([In] int lPartID);

		// Token: 0x06016363 RID: 90979
		[MethodImpl(4096, MethodCodeType = 3)]
		void StringFromPartID([In] int lPartID, [MarshalAs(19)] out string pbstrPart);
	}
}
