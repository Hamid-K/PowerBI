using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004AA RID: 1194
	[TypeLibType(4160)]
	[Guid("3050F1DE-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLOListElement
	{
		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x0600504B RID: 20555
		// (set) Token: 0x0600504A RID: 20554
		[DispId(1001)]
		bool compact
		{
			[TypeLibFunc(20)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x0600504D RID: 20557
		// (set) Token: 0x0600504C RID: 20556
		[DispId(1003)]
		int Start
		{
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x0600504F RID: 20559
		// (set) Token: 0x0600504E RID: 20558
		[DispId(-2147413095)]
		string type
		{
			[DispId(-2147413095)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413095)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
