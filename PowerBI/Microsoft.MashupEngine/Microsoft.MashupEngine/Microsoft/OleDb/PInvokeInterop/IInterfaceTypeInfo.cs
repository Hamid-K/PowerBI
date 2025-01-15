using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F74 RID: 8052
	public interface IInterfaceTypeInfo
	{
		// Token: 0x0600C4B6 RID: 50358
		unsafe int GetVTable(int size, IntPtr* vtable, bool includeSupportsInterfaceCallback);
	}
}
