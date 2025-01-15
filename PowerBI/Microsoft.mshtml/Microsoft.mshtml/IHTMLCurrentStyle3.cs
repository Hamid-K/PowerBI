using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000062 RID: 98
	[Guid("3050F818-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLCurrentStyle3
	{
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600098A RID: 2442
		[DispId(-2147412903)]
		string textOverflow
		{
			[TypeLibFunc(20)]
			[DispId(-2147412903)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600098B RID: 2443
		[DispId(-2147412901)]
		object minHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147412901)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600098C RID: 2444
		[DispId(-2147413065)]
		object wordSpacing
		{
			[TypeLibFunc(20)]
			[DispId(-2147413065)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600098D RID: 2445
		[DispId(-2147413036)]
		string whiteSpace
		{
			[DispId(-2147413036)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
