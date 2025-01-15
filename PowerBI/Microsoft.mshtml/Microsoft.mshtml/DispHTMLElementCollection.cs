using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004D5 RID: 1237
	[Guid("3050F56B-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(2)]
	[DefaultMember("item")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLElementCollection
	{
		// Token: 0x06007AB1 RID: 31409
		[DispId(1501)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x170029AC RID: 10668
		// (get) Token: 0x06007AB3 RID: 31411
		// (set) Token: 0x06007AB2 RID: 31410
		[DispId(1500)]
		int length
		{
			[DispId(1500)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1500)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06007AB4 RID: 31412
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x06007AB5 RID: 31413
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x06007AB6 RID: 31414
		[DispId(1502)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object tags([MarshalAs(27)] [In] object tagName);

		// Token: 0x06007AB7 RID: 31415
		[DispId(1505)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object urns([MarshalAs(27)] [In] object urn);

		// Token: 0x06007AB8 RID: 31416
		[DispId(1506)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object namedItem([MarshalAs(19)] [In] string name);
	}
}
