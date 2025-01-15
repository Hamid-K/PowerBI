using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CAD RID: 3245
	[InterfaceType(1)]
	[Guid("3050F692-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface ISegmentListIterator
	{
		// Token: 0x0601625E RID: 90718
		[MethodImpl(4096, MethodCodeType = 3)]
		void Current([MarshalAs(28)] out ISegment ppISegment);

		// Token: 0x0601625F RID: 90719
		[MethodImpl(4096, MethodCodeType = 3)]
		void First();

		// Token: 0x06016260 RID: 90720
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsDone();

		// Token: 0x06016261 RID: 90721
		[MethodImpl(4096, MethodCodeType = 3)]
		void Advance();
	}
}
