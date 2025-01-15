using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200006A RID: 106
	[TypeLibType(4160)]
	[Guid("3050F4B0-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLDOMAttribute
	{
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06000B02 RID: 2818
		[DispId(1000)]
		string nodeName
		{
			[DispId(1000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06000B04 RID: 2820
		// (set) Token: 0x06000B03 RID: 2819
		[DispId(1002)]
		object nodeValue
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06000B05 RID: 2821
		[DispId(1001)]
		bool specified
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
