using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000774 RID: 1908
	[TypeLibType(4160)]
	[Guid("3050F204-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLBaseElement
	{
		// Token: 0x17003857 RID: 14423
		// (get) Token: 0x0600AF89 RID: 44937
		// (set) Token: 0x0600AF88 RID: 44936
		[DispId(1003)]
		string href
		{
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003858 RID: 14424
		// (get) Token: 0x0600AF8B RID: 44939
		// (set) Token: 0x0600AF8A RID: 44938
		[DispId(1004)]
		string target
		{
			[DispId(1004)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
