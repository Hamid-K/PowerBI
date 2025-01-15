using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000095 RID: 149
	[TypeLibType(4160)]
	[Guid("3050F3F2-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLDatabinding
	{
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06000D13 RID: 3347
		// (set) Token: 0x06000D12 RID: 3346
		[DispId(-2147417091)]
		string dataFld
		{
			[DispId(-2147417091)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417091)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06000D15 RID: 3349
		// (set) Token: 0x06000D14 RID: 3348
		[DispId(-2147417090)]
		string dataSrc
		{
			[DispId(-2147417090)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417090)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06000D17 RID: 3351
		// (set) Token: 0x06000D16 RID: 3350
		[DispId(-2147417089)]
		string dataFormatAs
		{
			[DispId(-2147417089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
