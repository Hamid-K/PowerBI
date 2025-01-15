using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009FE RID: 2558
	[TypeLibType(4160)]
	[Guid("3050F82C-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLTableRow3
	{
		// Token: 0x17005586 RID: 21894
		// (get) Token: 0x060103EC RID: 66540
		// (set) Token: 0x060103EB RID: 66539
		[DispId(1009)]
		string ch
		{
			[DispId(1009)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1009)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005587 RID: 21895
		// (get) Token: 0x060103EE RID: 66542
		// (set) Token: 0x060103ED RID: 66541
		[DispId(1010)]
		string chOff
		{
			[DispId(1010)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
