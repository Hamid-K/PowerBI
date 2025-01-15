using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200007E RID: 126
	[DefaultMember("item")]
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F577-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface DispDOMChildrenCollection
	{
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06000BE8 RID: 3048
		[DispId(1500)]
		int length
		{
			[DispId(1500)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06000BE9 RID: 3049
		[DispId(-4)]
		[TypeLibFunc(65)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x06000BEA RID: 3050
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object item([In] int index);
	}
}
