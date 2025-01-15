using System;
using System.Runtime.InteropServices;
using ATL;

namespace MsolapWrapper
{
	// Token: 0x02000079 RID: 121
	internal interface ICAccessorWrapper
	{
		// Token: 0x06000166 RID: 358
		[return: MarshalAs(UnmanagedType.U1)]
		bool IsOpen();

		// Token: 0x06000167 RID: 359
		void Close();

		// Token: 0x06000168 RID: 360
		[return: MarshalAs(UnmanagedType.U1)]
		bool SupportsNextResult();

		// Token: 0x06000169 RID: 361
		[return: MarshalAs(UnmanagedType.U1)]
		bool NextResult();

		// Token: 0x0600016A RID: 362
		unsafe CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* GetAccessor();
	}
}
