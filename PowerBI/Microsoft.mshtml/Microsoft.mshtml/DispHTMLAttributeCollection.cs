using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200007B RID: 123
	[InterfaceType(2)]
	[TypeLibType(4112)]
	[DefaultMember("item")]
	[Guid("3050F56C-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface DispHTMLAttributeCollection
	{
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06000BD5 RID: 3029
		[DispId(1500)]
		int length
		{
			[DispId(1500)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06000BD6 RID: 3030
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x06000BD7 RID: 3031
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object item([MarshalAs(27)] [In] [Optional] ref object name);

		// Token: 0x06000BD8 RID: 3032
		[DispId(1501)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute getNamedItem([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06000BD9 RID: 3033
		[DispId(1502)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute setNamedItem([MarshalAs(28)] [In] IHTMLDOMAttribute ppNode);

		// Token: 0x06000BDA RID: 3034
		[DispId(1503)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute removeNamedItem([MarshalAs(19)] [In] string bstrName);
	}
}
