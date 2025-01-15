using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000A1 RID: 161
	[TypeLibType(4160)]
	[Guid("3050F5DC-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTCDescBehavior
	{
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06000D79 RID: 3449
		[DispId(-2147417612)]
		string urn
		{
			[DispId(-2147417612)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06000D7A RID: 3450
		[DispId(-2147417611)]
		string name
		{
			[TypeLibFunc(4)]
			[DispId(-2147417611)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
