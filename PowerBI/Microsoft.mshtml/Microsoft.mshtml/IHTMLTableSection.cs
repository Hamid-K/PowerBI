using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009F4 RID: 2548
	[Guid("3050F23B-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLTableSection
	{
		// Token: 0x17005558 RID: 21848
		// (get) Token: 0x06010388 RID: 66440
		// (set) Token: 0x06010387 RID: 66439
		[DispId(-2147418040)]
		string align
		{
			[DispId(-2147418040)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418040)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005559 RID: 21849
		// (get) Token: 0x0601038A RID: 66442
		// (set) Token: 0x06010389 RID: 66441
		[DispId(-2147413081)]
		string vAlign
		{
			[DispId(-2147413081)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413081)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700555A RID: 21850
		// (get) Token: 0x0601038C RID: 66444
		// (set) Token: 0x0601038B RID: 66443
		[DispId(-501)]
		object bgColor
		{
			[DispId(-501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700555B RID: 21851
		// (get) Token: 0x0601038D RID: 66445
		[DispId(1000)]
		IHTMLElementCollection rows
		{
			[DispId(1000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0601038E RID: 66446
		[DispId(1001)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object insertRow([In] int index = -1);

		// Token: 0x0601038F RID: 66447
		[DispId(1002)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void deleteRow([In] int index = -1);
	}
}
