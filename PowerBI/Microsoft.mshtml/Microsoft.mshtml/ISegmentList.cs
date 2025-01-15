using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CAC RID: 3244
	[InterfaceType(1)]
	[Guid("3050F605-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface ISegmentList
	{
		// Token: 0x0601625B RID: 90715
		[MethodImpl(4096, MethodCodeType = 3)]
		void CreateIterator([MarshalAs(28)] out ISegmentListIterator ppIIter);

		// Token: 0x0601625C RID: 90716
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetType(out _SELECTION_TYPE peType);

		// Token: 0x0601625D RID: 90717
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsEmpty(out int pfEmpty);
	}
}
