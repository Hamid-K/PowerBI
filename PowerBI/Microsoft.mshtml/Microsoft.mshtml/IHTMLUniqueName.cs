using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000094 RID: 148
	[TypeLibType(4160)]
	[Guid("3050F4D0-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLUniqueName
	{
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06000D10 RID: 3344
		[DispId(-2147417058)]
		int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06000D11 RID: 3345
		[DispId(-2147417057)]
		string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
