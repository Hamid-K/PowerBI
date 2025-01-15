using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007BB RID: 1979
	[TypeLibType(4160)]
	[Guid("3050F6CF-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLWindow4
	{
		// Token: 0x0600D742 RID: 55106
		[DispId(1180)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createPopup([MarshalAs(27)] [In] [Optional] ref object varArgIn);

		// Token: 0x170047A3 RID: 18339
		// (get) Token: 0x0600D743 RID: 55107
		[DispId(1181)]
		IHTMLFrameBase frameElement
		{
			[DispId(1181)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}
	}
}
