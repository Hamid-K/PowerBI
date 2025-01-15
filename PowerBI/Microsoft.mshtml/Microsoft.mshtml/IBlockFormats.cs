using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CB8 RID: 3256
	[DefaultMember("item")]
	[Guid("3050F830-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4096)]
	[ComImport]
	public interface IBlockFormats : IEnumerable
	{
		// Token: 0x060162AD RID: 90797
		[TypeLibFunc(1)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x170075B3 RID: 30131
		// (get) Token: 0x060162AE RID: 90798
		[DispId(1)]
		int Count
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060162AF RID: 90799
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string item([MarshalAs(27)] [In] ref object pvarIndex);
	}
}
