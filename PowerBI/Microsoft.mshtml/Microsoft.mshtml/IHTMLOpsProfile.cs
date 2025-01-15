using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000798 RID: 1944
	[Guid("3050F401-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLOpsProfile
	{
		// Token: 0x0600D487 RID: 54407
		[DispId(1)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool addRequest([MarshalAs(19)] [In] string name, [MarshalAs(27)] [In] [Optional] object reserved);

		// Token: 0x0600D488 RID: 54408
		[DispId(2)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void clearRequest();

		// Token: 0x0600D489 RID: 54409
		[DispId(3)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void doRequest([MarshalAs(27)] [In] object usage, [MarshalAs(27)] [In] [Optional] object fname, [MarshalAs(27)] [In] [Optional] object domain, [MarshalAs(27)] [In] [Optional] object path, [MarshalAs(27)] [In] [Optional] object expire, [MarshalAs(27)] [In] [Optional] object reserved);

		// Token: 0x0600D48A RID: 54410
		[DispId(4)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string getAttribute([MarshalAs(19)] [In] string name);

		// Token: 0x0600D48B RID: 54411
		[DispId(5)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool setAttribute([MarshalAs(19)] [In] string name, [MarshalAs(19)] [In] string value, [MarshalAs(27)] [In] [Optional] object prefs);

		// Token: 0x0600D48C RID: 54412
		[DispId(6)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool commitChanges();

		// Token: 0x0600D48D RID: 54413
		[DispId(7)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool addReadRequest([MarshalAs(19)] [In] string name, [MarshalAs(27)] [In] [Optional] object reserved);

		// Token: 0x0600D48E RID: 54414
		[DispId(8)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void doReadRequest([MarshalAs(27)] [In] object usage, [MarshalAs(27)] [In] [Optional] object fname, [MarshalAs(27)] [In] [Optional] object domain, [MarshalAs(27)] [In] [Optional] object path, [MarshalAs(27)] [In] [Optional] object expire, [MarshalAs(27)] [In] [Optional] object reserved);

		// Token: 0x0600D48F RID: 54415
		[DispId(9)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool doWriteRequest();
	}
}
