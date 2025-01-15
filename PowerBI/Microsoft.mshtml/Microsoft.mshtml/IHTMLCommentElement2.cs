using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009E1 RID: 2529
	[TypeLibType(4160)]
	[Guid("3050F813-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLCommentElement2
	{
		// Token: 0x170050A7 RID: 20647
		// (get) Token: 0x0600F779 RID: 63353
		// (set) Token: 0x0600F778 RID: 63352
		[DispId(1003)]
		string data
		{
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170050A8 RID: 20648
		// (get) Token: 0x0600F77A RID: 63354
		[DispId(1004)]
		int length
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F77B RID: 63355
		[DispId(1005)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string substringData([In] int offset, [In] int Count);

		// Token: 0x0600F77C RID: 63356
		[DispId(1006)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void appendData([MarshalAs(19)] [In] string bstrstring);

		// Token: 0x0600F77D RID: 63357
		[DispId(1007)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void insertData([In] int offset, [MarshalAs(19)] [In] string bstrstring);

		// Token: 0x0600F77E RID: 63358
		[DispId(1008)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void deleteData([In] int offset, [In] int Count);

		// Token: 0x0600F77F RID: 63359
		[DispId(1009)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void replaceData([In] int offset, [In] int Count, [MarshalAs(19)] [In] string bstrstring);
	}
}
