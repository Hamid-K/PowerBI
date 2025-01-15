using System;

namespace Microsoft.OleDb.PInvokeInterop.Test
{
	// Token: 0x02001FA3 RID: 8099
	internal interface ITestAccessorPInvokeInteropServices
	{
		// Token: 0x0600C560 RID: 50528
		int Initialize(IntPtr pUnknown);

		// Token: 0x0600C561 RID: 50529
		int Uninitialize(IntPtr pUnknown);

		// Token: 0x0600C562 RID: 50530
		MarshalledObjectHandle GetMarshalledObjectHandle(IntPtr pUnknown);

		// Token: 0x0600C563 RID: 50531
		object GetObjectForIUnknown(IntPtr pUnknown);
	}
}
