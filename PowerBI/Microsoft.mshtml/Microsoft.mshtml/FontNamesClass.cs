using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CD2 RID: 3282
	[ClassInterface(0)]
	[Guid("3050F83A-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[DefaultMember("item")]
	[ComImport]
	public class FontNamesClass : IFontNames, FontNames, IEnumerable
	{
		// Token: 0x06016330 RID: 90928
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern FontNamesClass();

		// Token: 0x06016331 RID: 90929
		[TypeLibFunc(1)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator GetEnumerator();

		// Token: 0x170075B8 RID: 30136
		// (get) Token: 0x06016332 RID: 90930
		[DispId(1)]
		public virtual extern int Count
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016333 RID: 90931
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string item([MarshalAs(27)] [In] ref object pvarIndex);
	}
}
