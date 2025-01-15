using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CD0 RID: 3280
	[TypeLibType(2)]
	[DefaultMember("item")]
	[ClassInterface(0)]
	[Guid("3050F831-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class BlockFormatsClass : IBlockFormats, BlockFormats, IEnumerable
	{
		// Token: 0x0601632C RID: 90924
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern BlockFormatsClass();

		// Token: 0x0601632D RID: 90925
		[TypeLibFunc(1)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator GetEnumerator();

		// Token: 0x170075B7 RID: 30135
		// (get) Token: 0x0601632E RID: 90926
		[DispId(1)]
		public virtual extern int Count
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601632F RID: 90927
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string item([MarshalAs(27)] [In] ref object pvarIndex);
	}
}
