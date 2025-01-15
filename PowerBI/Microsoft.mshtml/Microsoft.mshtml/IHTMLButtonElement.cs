using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200069B RID: 1691
	[TypeLibType(4160)]
	[Guid("3050F2BB-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLButtonElement
	{
		// Token: 0x170034CC RID: 13516
		// (get) Token: 0x0600A2B1 RID: 41649
		[DispId(2000)]
		string type
		{
			[DispId(2000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170034CD RID: 13517
		// (get) Token: 0x0600A2B3 RID: 41651
		// (set) Token: 0x0600A2B2 RID: 41650
		[DispId(-2147413011)]
		string value
		{
			[TypeLibFunc(20)]
			[DispId(-2147413011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170034CE RID: 13518
		// (get) Token: 0x0600A2B5 RID: 41653
		// (set) Token: 0x0600A2B4 RID: 41652
		[DispId(-2147418112)]
		string name
		{
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170034CF RID: 13519
		// (get) Token: 0x0600A2B7 RID: 41655
		// (set) Token: 0x0600A2B6 RID: 41654
		[DispId(8001)]
		object status
		{
			[DispId(8001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(8001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170034D0 RID: 13520
		// (get) Token: 0x0600A2B9 RID: 41657
		// (set) Token: 0x0600A2B8 RID: 41656
		[DispId(-2147418036)]
		bool disabled
		{
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170034D1 RID: 13521
		// (get) Token: 0x0600A2BA RID: 41658
		[DispId(-2147416108)]
		IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600A2BB RID: 41659
		[DispId(8002)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLTxtRange createTextRange();
	}
}
