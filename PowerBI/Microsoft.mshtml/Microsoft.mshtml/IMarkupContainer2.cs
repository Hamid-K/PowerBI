using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C97 RID: 3223
	[InterfaceType(1)]
	[Guid("3050F648-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IMarkupContainer2 : IMarkupContainer
	{
		// Token: 0x06016202 RID: 90626
		[MethodImpl(4096, MethodCodeType = 3)]
		void OwningDoc([MarshalAs(28)] out IHTMLDocument2 ppDoc);

		// Token: 0x06016203 RID: 90627
		[MethodImpl(4096, MethodCodeType = 3)]
		void CreateChangeLog([MarshalAs(28)] [In] IHTMLChangeSink pChangeSink, [MarshalAs(28)] out IHTMLChangeLog ppChangeLog, [In] int fForward, [In] int fBackward);

		// Token: 0x06016204 RID: 90628
		[MethodImpl(4096, MethodCodeType = 3)]
		void RegisterForDirtyRange([MarshalAs(28)] [In] IHTMLChangeSink pChangeSink, out uint pdwCookie);

		// Token: 0x06016205 RID: 90629
		[MethodImpl(4096, MethodCodeType = 3)]
		void UnRegisterForDirtyRange([In] uint dwCookie);

		// Token: 0x06016206 RID: 90630
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetAndClearDirtyRange([In] uint dwCookie, [MarshalAs(28)] [In] IMarkupPointer pIPointerBegin, [MarshalAs(28)] [In] IMarkupPointer pIPointerEnd);

		// Token: 0x06016207 RID: 90631
		[MethodImpl(4224, MethodCodeType = 3)]
		int GetVersionNumber();

		// Token: 0x06016208 RID: 90632
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetMasterElement([MarshalAs(28)] out IHTMLElement ppElementMaster);
	}
}
