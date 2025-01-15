using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000B1 RID: 177
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F57E-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface DispHTCDescBehavior
	{
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06000D9F RID: 3487
		[DispId(-2147417612)]
		string urn
		{
			[TypeLibFunc(4)]
			[DispId(-2147417612)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06000DA0 RID: 3488
		[DispId(-2147417611)]
		string name
		{
			[TypeLibFunc(4)]
			[DispId(-2147417611)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
