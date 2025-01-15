using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200004F RID: 79
	[Guid("3050F817-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLRuleStyle4
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000297 RID: 663
		// (set) Token: 0x06000296 RID: 662
		[DispId(-2147412903)]
		string textOverflow
		{
			[TypeLibFunc(20)]
			[DispId(-2147412903)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412903)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000299 RID: 665
		// (set) Token: 0x06000298 RID: 664
		[DispId(-2147412901)]
		object minHeight
		{
			[DispId(-2147412901)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412901)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}
	}
}
