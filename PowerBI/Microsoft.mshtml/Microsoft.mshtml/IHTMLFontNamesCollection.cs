using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BF2 RID: 3058
	[DefaultMember("item")]
	[TypeLibType(4160)]
	[Guid("3050F376-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLFontNamesCollection : IEnumerable
	{
		// Token: 0x170073EC RID: 29676
		// (get) Token: 0x06015AFA RID: 88826
		[DispId(1501)]
		int length
		{
			[TypeLibFunc(64)]
			[DispId(1501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015AFB RID: 88827
		[DispId(-4)]
		[TypeLibFunc(65)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x06015AFC RID: 88828
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string item([In] int index);
	}
}
