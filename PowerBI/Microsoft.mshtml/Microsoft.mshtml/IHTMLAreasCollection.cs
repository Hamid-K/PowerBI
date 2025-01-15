using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020008CC RID: 2252
	[Guid("3050F383-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[DefaultMember("item")]
	[ComImport]
	public interface IHTMLAreasCollection : IEnumerable
	{
		// Token: 0x17004BC0 RID: 19392
		// (get) Token: 0x0600E60A RID: 58890
		// (set) Token: 0x0600E609 RID: 58889
		[DispId(1500)]
		int length
		{
			[DispId(1500)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1500)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x0600E60B RID: 58891
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x0600E60C RID: 58892
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x0600E60D RID: 58893
		[DispId(1502)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object tags([MarshalAs(27)] [In] object tagName);

		// Token: 0x0600E60E RID: 58894
		[DispId(1503)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void add([MarshalAs(28)] [In] IHTMLElement element, [MarshalAs(27)] [In] [Optional] object before);

		// Token: 0x0600E60F RID: 58895
		[DispId(1504)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void remove([In] int index = -1);
	}
}
